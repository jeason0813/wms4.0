using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface IUserFrameworkService:IBaseService<UserFramework>
    {
        string SavePower(string userCode, string powersStr);
    }
}
