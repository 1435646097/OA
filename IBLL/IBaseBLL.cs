using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBLL<T> where T:class,new()
    {
        IDbSession DbSession { get; }
        IBaseDAL<T> CurrentDAL { get; set; }
        T AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        IQueryable<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda);
        IQueryable<T> LoadPageEntity<s>(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda, System.Linq.Expressions.Expression<Func<T, s>> wherePageLamda, int pageSize, int pageIndex, out int totalCount, bool isAsc);
    }
}
