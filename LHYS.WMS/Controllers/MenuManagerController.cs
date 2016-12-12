using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LHYS.WMS.Controllers
{
    public class MenuManagerController:Controller
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

        public ActionResult UserRole()
        {
            return View();
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
           var list= MenuService.GetData("test");
           return Json(list);
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRoles()
        {
            var list = RoleService.LoadEntities(a => a.IsDelete==false); //查询未删除的
            return Json(list);
        }
        /// <summary>
        /// 获取某人的角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOnesRoles(string UserCode)
        {

            return null;
        }
    }
}