using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public  partial interface IDBSession
    {
        //EF数据操作对象的属性
        DbContext Db { get; }

        //IUserInfoDal UserInfoDal { get; set; }   //T4模板里有
        bool SaveChanges();
    }
}
