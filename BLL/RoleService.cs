using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class RoleService
    {
        /// <summary>
        /// 获取某人的角色
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IQueryable<Role> GetOnesRole(string userCode)
        {
            var roleIds = CurrentDBSession.UserRoleRelationDal.LoadEntities(a => a.UserCode == userCode).Select(a => a.RoleId);//角色编号
            var list = CurrentDal.LoadEntities(a => roleIds.Contains(a.Id));
            return list;
        }
        /// <summary>
        /// 保存某人角色
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public string SaveRoles(string UserCode, int[] Roles)
        {
            try
            {
                //删除原有的
                var temp = CurrentDBSession.UserRoleRelationDal.LoadEntities(a => a.UserCode == UserCode);
                foreach (var item in temp)
                {
                    CurrentDBSession.UserRoleRelationDal.DeleteEntity(item);
                }
                //添加新的
                foreach (int item in Roles)
                {
                    CurrentDBSession.UserRoleRelationDal.AddEntity(new UserRoleRelation() { RoleId = item, UserCode = UserCode });
                }
                return CurrentDBSession.SaveChanges() ? "保存成功！" : "保存失败！";

            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

    }
}
