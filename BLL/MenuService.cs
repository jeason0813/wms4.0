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
        /// 获取某人有权限的菜单 只两层
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<MenuView> GetOnesMenu(string userCode)
        {
            var RoleIds = CurrentDBSession.UserRoleRelationDal.LoadEntities(a => a.UserCode == userCode).Select(a => a.RoleId); //权限ID集合
            var MenuIds= CurrentDBSession.RoleMenuRelationDal.LoadEntities(a => RoleIds.Contains(a.RoleId)).Select(a=>a.MenuId);//菜单ID集合
            var list= CurrentDal.LoadEntities(a => MenuIds.Contains(a.id)&&a.Type<3).OrderByDescending(a => a.Type);//Type将序排列
            List<MenuView> listRes = new List<MenuView>();
            foreach (var item in list)
            {
                MenuView menu = new MenuView() { id = item.id, pid = item.pid, Url=item.Url, text = item.text, items = new List<MenuView>() };
                listRes.Add(menu);
            }
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
        /// <summary>
        /// 获取角色的菜单集合
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public List<MenuView> GetData(int RoleCode)
        {
            var list = CurrentDal.LoadEntities(a => true).OrderByDescending(a => a.Type);//获取数据库所有的菜单(按type降序排列)
            var rolesList = CurrentDBSession.RoleMenuRelationDal.LoadEntities(a => a.RoleId == RoleCode);//获取该角色的菜单权限
            //数据库模型转换成视图模型  
            List<MenuView> listRes = new List<MenuView>();
            foreach (var item in list)
            {
                MenuView menu = new MenuView() { id = item.id, pid = item.pid, icon = item.icon, text = item.text, expanded = false, items = new List<MenuView>() };
                //设置选中属性
                var temp = rolesList.Where(a => a.MenuId == item.id).FirstOrDefault();
                if (temp != null)
                {
                    menu.checkState = temp.CheckState;
                }
                listRes.Add(menu);
            }
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

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="RoleId">要修改的角色</param>
        /// <param name="checkListId">选中的ID</param>
        /// <param name="indeterminateListId">半选中的Id</param>
        /// <returns></returns>
        public string SaveRoleMenu(int RoleId, int[] checkListId, int[] indeterminateListId)
        {
            //删除旧数据
            var oldList = CurrentDBSession.RoleMenuRelationDal.LoadEntities(a => a.RoleId == RoleId);
            foreach (var item in oldList)
            {
                CurrentDBSession.RoleMenuRelationDal.DeleteEntity(item);
            }
            //插入新数据
            if (checkListId != null)
            {
                foreach (var item in checkListId)
                {
                    CurrentDBSession.RoleMenuRelationDal.AddEntity(new RoleMenuRelation() { RoleId = RoleId, MenuId = item, CheckState = "checked" });
                }
            }
            if (indeterminateListId != null)
            {
                foreach (var item in indeterminateListId)
                {
                    CurrentDBSession.RoleMenuRelationDal.AddEntity(new RoleMenuRelation() { RoleId = RoleId, MenuId = item, CheckState = "indeterminate" });
                }

            }
            return CurrentDBSession.SaveChanges() ? "保存成功" : "保存失败";
        }
    }
}
