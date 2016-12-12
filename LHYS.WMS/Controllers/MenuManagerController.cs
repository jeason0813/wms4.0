using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LHYS.WMS.Controllers
{
    public class MenuManagerController : Controller
    {
        private IMenuService MenuService { get; set; }
        private IRoleService RoleService { get; set; }

        /// <summary>
        /// 角色-菜单 设置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户-角色设置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRole()
        {
            return View();
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData(int RoleCode)
        {
            var list = MenuService.GetData(RoleCode);
            return Json(list);
        }
        /// <summary>
        /// 获取某人的菜单权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOnesMenu(string UserCode)
        {
            return Json(MenuService.GetOnesMenu(UserCode));


        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRoles()
        {
            var list = RoleService.LoadEntities(a => a.IsDelete == false); //查询未删除的
            return Json(list);
        }
        /// <summary>
        /// 获取某人的角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOnesRoles(string UserCode)
        {
            return Json(RoleService.GetOnesRole(UserCode));
        }
        /// <summary>
        /// 保存某人的角色
        /// </summary>
        /// <param name="UserCode">工号</param>
        /// <param name="Roles">角色ID</param>
        /// <returns></returns>
        public ActionResult SaveRoles(string UserCode, int[] Roles)
        {
            return Content(RoleService.SaveRoles(UserCode, Roles));
        }
        /// <summary>
        /// 保存菜单设置
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="checkListId"></param>
        /// <param name="indeterminateListId"></param>
        /// <returns></returns>
        public ActionResult SaveMenu(int RoleId, int[] checkListId, int[] indeterminateListId)
        {
            string res = MenuService.SaveRoleMenu(RoleId, checkListId, indeterminateListId);
            return Content(res);
        }
    }
}