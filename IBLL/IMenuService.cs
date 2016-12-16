using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IMenuService : IBaseService<Menu>
    {
        List<MenuViewForMainPage> GetOnesMenu(string userCode);
        List<MenuView> GetData(int RoleCode);
        string SaveRoleMenu(int RoleId, int[] checkListId, int[] indeterminateListId);
        IQueryable<int> GetButtonPower(string userCode);
    }
}
