using DAL;
using DALFactory;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T> where T : class, new()
    {
        public BaseBLL()
        {
            GetCurrentDAL();
        }
        public abstract void GetCurrentDAL();
        public IDbSession DbSession { get => DbSessionFactory.CreateDbSession(); }

        public IBaseDAL<T> CurrentDAL { get; set; }
        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            CurrentDAL.AddEntity(entity);
            DbSession.SaveChanges();
            return entity;
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDAL.DeleteEntity(entity);
            return DbSession.SaveChanges();
        }
        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            CurrentDAL.EditEntity(entity);
            return DbSession.SaveChanges();
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereLamda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDAL.LoadEntity(whereLamda);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="whereLamda"></param>
        /// <param name="wherePageLamda"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntity<s>(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda, System.Linq.Expressions.Expression<Func<T, s>> wherePageLamda, int pageSize, int pageIndex, out int totalCount, bool isAsc)
        {
            return CurrentDAL.LoadPageEntity(whereLamda, wherePageLamda, pageSize, pageIndex, out totalCount, isAsc);
        }
    }
}
