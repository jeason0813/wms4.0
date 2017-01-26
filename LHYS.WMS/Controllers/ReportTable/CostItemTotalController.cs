using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class CostItemTotalController : Controller
    {
        private IUserFrameworkService UserFrameworkService { get; set; }
        private ICostItemService CostItemService { get; set; }
        private ICostTotalService CostTotalService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        //查询记录
        public ActionResult Search(DateTime? timestart,DateTime? timeend, string DepartmentId)
        {
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string UserCode = Session["UserCode"].ToString();//员工编号
                string Power = Session["Power"].ToString();//权限字符串
                var list = CostTotalService.LoadEntities(a => Power.Contains(a.DepartmentId));
                if (timestart != null)
                {
                    list = list.Where(a => a.BillDate >= timestart);
                }
                if (timeend != null)
                {
                    list = list.Where(a => a.BillDate < timeend.GetValueOrDefault().AddDays(1));
                }
                return Json(new {
                    data = list
                });
            }
        }
        //获取基础数据：部门、费用项目
        public ActionResult GetDepartmentAndCostItems()
        {
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string UserCode = Session["UserCode"].ToString();//员工编号
                string Power = Session["Power"].ToString();//权限字符串
                var list1 = UserFrameworkService.LoadEntities(u => u.UserCode == UserCode);//部门
                return Json(list1);
            }

        }
    }
}