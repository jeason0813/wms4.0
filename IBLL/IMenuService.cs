using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IMenuService : IBaseService<Menu>
    {
         List<MenuView> GetData(string UserCode);
    }
}
