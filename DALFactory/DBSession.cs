using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
   
        public partial class DBSession : IDBSession
        {
            // Model1Container Db = new Model1Container();

            public DbContext Db
            {
                get
                {
                    return DAL.DBContextFactory.CreateDbContext();
                }
            }


            //private IUserInfoDal _UserInfoDal;

            //public IUserInfoDal UserInfoDal
            //{
            //    get
            //    {
            //        if (_UserInfoDal == null)
            //        {
            //            // _UserInfoDal = new DAL.UserInfoDAL();
            //            _UserInfoDal = AbstractFactory.CreateUserInfoDal();//通过抽象工厂类创建数据操作的实例
            //        }
            //        return _UserInfoDal;
            //    }

            //    set
            //    {
            //        _UserInfoDal = value;
            //    }
            //}


            public bool SaveChanges()
            {
                return Db.SaveChanges() > 0;
            }

        }
    }

