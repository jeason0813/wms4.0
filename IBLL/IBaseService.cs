using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    
        public interface IBaseService<T> where T : class, new()
        {
            IDBSession CurrentDBSession { get; }
            IDAL.IBaseDal<T> CurrentDal { get; set; }
            T AddEntity(T entity);
            bool DeleteEntity(T entity);
            bool EditEntity(T entity);
            IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
            IQueryable<T> LoadPageEntities<s>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, int pageIndex, int pageSize, bool isAsc, out int totalCount);

        }
    }
