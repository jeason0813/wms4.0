using DALFactory;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        //数据会话层对象
        public IDBSession CurrentDBSession
        {
            get
            {
                //return new DALFactory.DBSession();//暂时
                return DBSessionFactory.CreateDBSession();//创建线程内唯一对象
            }
        }
        public IDAL.IBaseDal<T> CurrentDal { get; set; }//数据操作类对象
        public abstract void SetCurrentDal();//抽象方法 子类必须实现
        public BaseService()//构造方法  执行上面的抽象方法
        {
            SetCurrentDal();
        }


        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            CurrentDBSession.SaveChanges();
            return entity;
        }

        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return CurrentDBSession.SaveChanges();
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, int pageIndex, int pageSize, bool isAsc, out int totalCount)
        {
            return CurrentDal.LoadPageEntities(whereLambda, orderByLambda, pageIndex, pageSize, isAsc, out totalCount);
        }
    }
}
