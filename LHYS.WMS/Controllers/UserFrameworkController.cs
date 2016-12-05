using DBModel;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class UserFrameworkController : Controller
    {
        private IUserService UserService { get; set; }
        private ICompanyService CompanyService { get; set; }

        private IUserFrameworkService UserFrameworkService { get; set; }

        // GET: UserFramework
        public ActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// 获取人员数据（所有）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            var list = UserService.LoadEntities(a => true);
            return Json(list);
        }

        /// <summary>
        /// 根据权限获取用户（获取此人权限范围内的用户）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsersBasePower()
        {
            if (Session["Power"] == null)
            {
                return null;
            }
            else
            {
                string power = Session["Power"].ToString();
                var temp = UserFrameworkService.LoadEntities(u => power.Contains(u.DeptCode));
                var list = (from a in temp
                            select new
                            {
                                a.UserCode,
                                a.UserName
                            }).Distinct();
                return Json(list);
            }
        }
        /// <summary>
        /// 获取公司(带部门)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCompanyAndDept()
        {
            var list = CompanyService.LoadEntities(c => true);
            return Json(list);
        }

        /// <summary>
        /// 获取部门【根据权限】
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCompanyAndDeptBaseUser()
        {
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string UserCode = Session["UserCode"].ToString();
                var list = UserFrameworkService.LoadEntities(a=>a.UserCode==UserCode);
                return Json(list);
            }
        }

        /// <summary>
        /// 获取某个人的公司和部门
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOnesCompanyAndDept()
        {
            string userCode = Session["UserCode"].ToString();
            var list = UserFrameworkService.LoadEntities(u => u.UserCode == userCode);
            return Json(list);
        }

        /// <summary>
        /// 获取某个用户的权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPower()
        {
            string userCode = Request["UserCode"].ToString();
            var list = UserFrameworkService.LoadEntities(u => u.UserCode == userCode);
            return Json(list);
        }
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <returns></returns>
        public ActionResult SavePower()
        {
            string userCode = Request["UserCode"].ToString();//当前用户编号
            string powersStr = Request["Powers"].ToString();
            string res = UserFrameworkService.SavePower(userCode, powersStr);
            return Content(res);
        }
    }
}