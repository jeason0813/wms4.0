using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class DBContextFactory
    {
        //保证线程内唯一EF (EF对象存储在HttpContext中的CallContext中，如果存在就返回 ，如果不存在就创建)
        public static  DBContainer CreateDbContext()
        {
            DBContainer dbContext = (DBContainer)CallContext.GetData("DbContext");
            if (dbContext == null)
            {
                dbContext = new DBContainer();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}
