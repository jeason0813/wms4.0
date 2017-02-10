using Common;
using IBLL;
using LHYS.WMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                var tempNew = temp.ToList().Select(a => new { a.CostItemCode, a.CostItemName, a.Price, Type = a.CostItemCode.Substring(0, 2) == "64" ? "成本" : "收入" });
                int count = tempNew.Count();
                //分页
                var result = tempNew.OrderBy(a => a.CostItemCode).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Json(new
                {
                    data = result,
                    totalCount = count
                });
            }
        }

        public ActionResult ExportTable(DateTime? timestart, DateTime? timeend, string DepartmentId)
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
                var tempNew = temp.ToList().Select(a => new { a.CostItemCode, a.CostItemName, a.Price, Type = a.CostItemCode.Substring(0, 2) == "64" ? "成本" : "收入" });
                //转换成DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("科目编码");
                dt.Columns.Add("科目名称");
                dt.Columns.Add("科目分类");
                dt.Columns.Add("发生金额");
                foreach (var item in tempNew)
                {
                    DataRow dr = dt.NewRow();
                    dr["科目编码"] = item.CostItemCode;
                    dr["科目名称"] = item.CostItemName;
                    dr["科目分类"] = item.Type;
                    dr["发生金额"] = item.Price;
                    dt.Rows.Add(dr);
                }
                MemoryStream file = NPIOHelper.RenderToMemory(dt, "sheet1");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Response.ContentType = "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet";// "application/ms-excel/msword";
                Response.AddHeader("Content-Disposition", "attachment;fileName=费用项目汇总表.xls");
                Response.BinaryWrite(file.ToArray());
                return new EmptyResult();
            }
        }
    }
}