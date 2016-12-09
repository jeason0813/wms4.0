using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class MenuService : BaseService<Menu>, IMenuService
    {
        /// <summary>
        /// 获取用户的菜单集合对象
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public List<MenuView> GetData(string UserCode)
        {
            var list = CurrentDal.LoadEntities(a => true).OrderByDescending(a=>a.Type);//获取数据库所有的菜单(按type降序排列)
            //数据库模型转换成视图模型
            List<MenuView> listRes = new List<MenuView>();
            foreach (var item in list)
            {
                MenuView menu = new MenuView() { id = item.id, pid = item.pid, icon = item.icon, text = item.text, expanded = false,items=new List<MenuView>() };
                listRes.Add(menu);
            }
            //根据用户菜单权限设置菜单的选中状态 

            //把模型嵌套起来
            foreach (MenuView item in listRes)
            {
                if (item.pid == 0) //当查找到最上层 停止循环
                {
                    break;
                }
                listRes.Find(a => a.id == item.pid).items.Add(item);
            }
            listRes = listRes.Where(a => a.pid == 0).ToList(); //只取最上层的元素
            return listRes;
        }

       

    }
}
