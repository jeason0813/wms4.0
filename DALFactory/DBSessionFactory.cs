using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public static class DBSessionFactory
    {
        public static IDBSession CreateDBSession()
        {
            IDBSession dbSession = (IDBSession)CallContext.GetData("dbsession");
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData("dbsession", dbSession);
            }
            return dbSession;
        }
    }
}
