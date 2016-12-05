using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseDal<T> where T :class, new()
    {
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<s>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, int pageIndex, int pageSize, bool isAsc, out int totalCount);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);
    }
}
