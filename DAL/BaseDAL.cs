using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDAL<T> where T : class, new()
    {
        public DbContext Db { get => DbContextFactory.CreateDbContext(); }

        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            return entity;
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
        }
        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Modified;
            return true;
        }
        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereLamda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return Db.Set<T>().Where<T>(whereLamda);
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
            IQueryable<T> list = Db.Set<T>().Where<T>(whereLamda);
            totalCount = list.Count<T>();
            if (isAsc)
            {
                list = list.OrderBy<T, s>(wherePageLamda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                list = list.OrderByDescending<T, s>(wherePageLamda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return list;
        }
    }
}
