using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
using System.Linq.Expressions;

namespace DAL
{
    public class BaseDal<T> where T : class, new()
    {
        //Model1Container Db = new Model1Container();
        public DBContainer Db = (DBContainer)DBContextFactory.CreateDbContext();
        // public Model1Container Db = (Model1Container)DBContextFactory.CreateDbContext();
        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }

        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }

        public bool EditEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, int pageIndex, int pageSize, bool isAsc, out int totalCount)
        {
            var temp = Db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

    }
}
