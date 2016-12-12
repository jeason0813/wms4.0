using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public  partial interface IRoleService: IBaseService<Role>
    {
         IQueryable<Role> GetOnesRole(string userCode);
        string SaveRoles(string UserCode, int[] Roles);
    }
}
