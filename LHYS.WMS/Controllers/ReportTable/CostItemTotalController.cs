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
        private ILoadingAndLaborCostDetailService LoadingAndLaborCostDetailService { get; set; }
        private IOtherExpenseListDetailService OtherExpenseListDetailService { get; set; }
        /// <summary>
        /// 费用项目汇总表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        //查询记录
        public ActionResult Search(int pageIndex, int pageSize, DateTime? timestart, DateTime? timeend, string DepartmentId)
        {
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string Power = Session["Power"].ToString();//权限字符串
                //获取装卸费力资费明细
                var list1 = LoadingAndLaborCostDetailService.LoadEntities(a => Power.Contains(a.DepartmentId)).ToList();
                if (timestart != null)
                {
                    list1 = list1.Where(a => a.ExaminaDate >= timestart).ToList();
                }
                if (timeend != null)
                {
                    list1 = list1.Where(a => a.ExaminaDate < timeend.GetValueOrDefault().AddDays(1)).ToList();
                }
                //获取其他明细
                var list2 = LoadingAndLaborCostDetailService.LoadEntities(a => Power.Contains(a.DepartmentId)).ToList();
                if (timestart != null)
                {
                    list2 = list2.Where(a => a.ExaminaDate >= timestart).ToList();
                }
                if (timeend != null)
                {
                    list2 = list2.Where(a => a.ExaminaDate < timeend.GetValueOrDefault().AddDays(1)).ToList();
                }
                //合并list1  list2   提取有用字段  返回
                var res = list1.Select(a => new { a.CostItemCode, a.CostItemName, a.AllAmount }).ToList();
                var res2 = list2.Select(a => new { a.CostItemCode, a.CostItemName, a.AllAmount }).ToList();
                //合并成一个list
                res.AddRange(res2);
                //分组求和
                var temp = from a in res
                           group a by new { a.CostItemCode, a.CostItemName } into g
                           select new { g.Key.CostItemCode, g.Key.CostItemName, Price = g.Sum(a => a.AllAmount) };
                var tempNew= temp.ToList().Select(a => new { a.CostItemCode, a.CostItemName, a.Price, Type = a.CostItemCode.Substring(0, 2) == "64" ? "成本" : "收入" });
                int count = tempNew.Count();
                //分页
                var result= tempNew.OrderBy(a=>a.CostItemCode).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Json(new
                {
                    data = result,
                    totalCount = count
                });
            }
        }
    }
}