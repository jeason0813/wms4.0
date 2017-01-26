using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class CostTotalController : Controller
    {
        private IUserFrameworkService UserFrameworkService { get; set; }
        private ICostItemService CostItemService { get; set; }
        private ICostTotalService CostTotalService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        //查询记录
        public ActionResult Search(int pageIndex,int pageSize,DateTime? timestart,DateTime? timeend, string DepartmentId,string CostItemCode)
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
                if (CostItemCode != null)
                {
                    list = list.Where(a => a.CostItemCode == CostItemCode);
                }
                if (timestart != null)
                {
                    list = list.Where(a => a.BillDate >= timestart);
                }
                if (timeend != null)
                {
                    list = list.Where(a => a.BillDate < timeend.GetValueOrDefault().AddDays(1));
                }
                int count = list.Count();
                list = list.OrderBy(a=>a.OrderNumber).ThenBy(a=>a.BillCode).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                return Json(new {
                    data = list,
                    totalCount = count
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
                var list2 = CostItemService.LoadEntities(u => Power.Contains(u.DepartmentId));//费用项目
                var res = new
                {
                    Departent = list1,
                    CostItem = list2,
                };
                return Json(res);
            }

        }
    }
}