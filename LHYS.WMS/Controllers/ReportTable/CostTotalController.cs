using Common;
using IBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public ActionResult Search(int pageIndex, int pageSize, DateTime? timestart, DateTime? timeend, string DepartmentId, string CostItemCode)
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
                if (CostItemCode != null&&CostItemCode!="")
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
                list = list.OrderBy(a => a.OrderNumber).ThenBy(a => a.BillCode).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                return Json(new
                {
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

        public ActionResult ExportTable(DateTime? timestart, DateTime? timeend, string DepartmentId, string CostItemCode)
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
                if (CostItemCode != null&& CostItemCode!="")
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
                //生成Datatable
                DataTable dt = new DataTable();
                dt.Columns.Add("单据类型");
                dt.Columns.Add("单据编码");
                dt.Columns.Add("单据日期");
                dt.Columns.Add("制单人");
                dt.Columns.Add("审核人");
                dt.Columns.Add("费用编码");
                dt.Columns.Add("费用项目");
                dt.Columns.Add("金额");
                dt.Columns.Add("备注");
                foreach (var item in list)
                {
                    DataRow dr = dt.NewRow();
                    dr["单据类型"] = item.billType;
                    dr["单据编码"] = item.BillCode;
                    dr["单据日期"] = item.BillDate;
                    dr["制单人"] = item.MakePerson;
                    dr["审核人"] = item.ExaminePerson;
                    dr["费用编码"] = item.CostItemCode;
                    dr["费用项目"] = item.CostItemName;
                    dr["金额"] = item.AllAmount;
                    dr["备注"] = item.Remark;
                    dt.Rows.Add(dr);
                }
                MemoryStream file = NPIOHelper.RenderToMemory(dt, "sheet1");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Response.ContentType = "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet";// "application/ms-excel/msword";
                Response.AddHeader("Content-Disposition", "attachment;fileName=费用项目明细表.xls");
                Response.BinaryWrite(file.ToArray());
                return new EmptyResult();
            }
        }
    }
}