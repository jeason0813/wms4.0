using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LinqKit;

namespace LHYS.WMS.Controllers
{
    public class FinanceManagerController : Controller
    {
        private IOtherExpenseListService OtherExpenseListService { get; set; }
        private ILoadingExpensesBillService LoadingExpensesBillService { get; set; }

        // GET: BillsManager
        public ActionResult Index()
        {
            if (Request["billType"] == null)
            {
                return Content("没有单据类型！");
            }
            string billType = Request["billType"];
            string billTypeName = "";
            string listType="";
            string listFileName = "";
            switch (billType)
            {
                case "OtherExpenseReceive":
                    billTypeName = "其他费用应收单";
                    listFileName = "OtherExpenseList";
                    listType = "OtherExpenseReceiveList";
                    break;
                case "OtherExpenseGive":
                    billTypeName = "其他费用应付单";
                    listFileName = "OtherExpenseList";
                    listType = "OtherExpenseGiveList"; 
                    break;
                case "LoadingCostRceive":
                    billTypeName = "装卸费应收单";
                    listFileName = "LoadingExpensesBill";
                    listType = "LoadingCostReceiveList";
                    break;
                case "LoadingCostGive":
                    billTypeName = "装卸费成本单";
                    listFileName = "LoadingExpensesBill";
                    listType = "LoadingCostGiveList";
                    break;
                case "LaborCostReceive":
                    billTypeName = "力资费应收单";
                    listFileName = "LoadingExpensesBill";
                    listType = "LaborReceiveList";
                    break;
                default:
                    billTypeName = "";
                    break;
            }
            ViewBag.billType = billType;//单据类型
            ViewBag.billTypeName = billTypeName;//单据类型名
            ViewBag.listType = listType;
            ViewBag.listFileName = listFileName;
            return View();
        }

        public ActionResult SearchBills(string billType, int pageIndex, int pageSize, string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode, string LBBillCode)
        {
            int totalCount = 0; //总记录数
            switch (billType)
            {
                case "OtherExpenseReceiveList":
                    Expression<Func<OtherExpenseList, bool>> exp1 = OtherExpenseReceiveSearchCondition(timestart, timeend, BillState, DepartmentId, BillCode);
                    List<OtherExpenseList> res1 = OtherExpenseListService.LoadPageEntities(exp1.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res1,
                        totalCount = totalCount

                    });
                case "OtherExpenseGiveList":
                    Expression<Func<OtherExpenseList, bool>> exp2 = OtherExpenseGiveSearchCondition(timestart, timeend, BillState, DepartmentId, BillCode);
                    List<OtherExpenseList> res2 = OtherExpenseListService.LoadPageEntities(exp2.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res2,
                        totalCount = totalCount

                    });
                case "LoadingCostReceiveList":
                    Expression<Func<LoadingExpensesBill, bool>> exp3 = LoadingCostRceiveSearchCondition(timestart, timeend, BillState, DepartmentId, BillCode);
                    List<LoadingExpensesBill> res3 = LoadingExpensesBillService.LoadPageEntities(exp3.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res3,
                        totalCount = totalCount

                    });
                case "LoadingCostGiveList":
                    Expression<Func<LoadingExpensesBill, bool>> exp4 = LoadingCostGiveSearchCondition(timestart, timeend, BillState, DepartmentId, BillCode);
                    List<LoadingExpensesBill> res4= LoadingExpensesBillService.LoadPageEntities(exp4.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res4,
                        totalCount = totalCount

                    });
                case "LaborReceiveList":
                    Expression<Func<LoadingExpensesBill, bool>> exp5 = LaborCostReceiveSearchCondition(timestart, timeend, BillState, DepartmentId, BillCode);
                    List<LoadingExpensesBill> res5 = LoadingExpensesBillService.LoadPageEntities(exp5.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res5,
                        totalCount = totalCount

                    });

            }
            return Content("参数错误");
        }
        /// <summary>
        /// 其他费用应收单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<OtherExpenseList, bool>> OtherExpenseReceiveSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, string BillCode)
        {
            List<Expression<Func<OtherExpenseList, bool>>> list = new List<Expression<Func<OtherExpenseList, bool>>>();
            Expression<Func<OtherExpenseList, bool>> exp = a => true; //初始条件
            list.Add(exp);
            Expression<Func<OtherExpenseList, bool>> last17 = list[list.Count - 1];
            Expression<Func<OtherExpenseList, bool>> exp17 = a => last17.Invoke(a) && (a.BillType== 1);
            list.Add(exp17);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<OtherExpenseList, bool>> last1 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<OtherExpenseList, bool>> last2 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<OtherExpenseList, bool>> last3 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<OtherExpenseList, bool>> last4 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<OtherExpenseList, bool>> last6 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<OtherExpenseList, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        /// <summary>
        /// 其他费用应付单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<OtherExpenseList, bool>> OtherExpenseGiveSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, string BillCode)
        {
            List<Expression<Func<OtherExpenseList, bool>>> list = new List<Expression<Func<OtherExpenseList, bool>>>();
            Expression<Func<OtherExpenseList, bool>> exp = a => true; //初始条件
            list.Add(exp);
            Expression<Func<OtherExpenseList, bool>> last17 = list[list.Count - 1];
            Expression<Func<OtherExpenseList, bool>> exp17 = a => last17.Invoke(a) && (a.BillType == 2);
            list.Add(exp17);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<OtherExpenseList, bool>> last1 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<OtherExpenseList, bool>> last2 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<OtherExpenseList, bool>> last3 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<OtherExpenseList, bool>> last4 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<OtherExpenseList, bool>> last6 = list[list.Count - 1];
                Expression<Func<OtherExpenseList, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<OtherExpenseList, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        /// <summary>
        /// 装卸费用应收单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<LoadingExpensesBill, bool>> LoadingCostRceiveSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, string BillCode)
        {
            List<Expression<Func<LoadingExpensesBill, bool>>> list = new List<Expression<Func<LoadingExpensesBill, bool>>>();
            Expression<Func<LoadingExpensesBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            Expression<Func<LoadingExpensesBill, bool>> last17 = list[list.Count - 1];
            Expression<Func<LoadingExpensesBill, bool>> exp17 = a => last17.Invoke(a) && (a.BillType == 1);
            list.Add(exp17);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<LoadingExpensesBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<LoadingExpensesBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<LoadingExpensesBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<LoadingExpensesBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<LoadingExpensesBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<LoadingExpensesBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        /// <summary>
        /// 装卸费用应收单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<LoadingExpensesBill, bool>> LoadingCostGiveSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, string BillCode)
        {
            List<Expression<Func<LoadingExpensesBill, bool>>> list = new List<Expression<Func<LoadingExpensesBill, bool>>>();
            Expression<Func<LoadingExpensesBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            Expression<Func<LoadingExpensesBill, bool>> last17 = list[list.Count - 1];
            Expression<Func<LoadingExpensesBill, bool>> exp17 = a => last17.Invoke(a) && (a.BillType == 2);
            list.Add(exp17);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<LoadingExpensesBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<LoadingExpensesBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<LoadingExpensesBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<LoadingExpensesBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<LoadingExpensesBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<LoadingExpensesBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        /// <summary>
        /// 力资费费用应收单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<LoadingExpensesBill, bool>> LaborCostReceiveSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, string BillCode)
        {
            List<Expression<Func<LoadingExpensesBill, bool>>> list = new List<Expression<Func<LoadingExpensesBill, bool>>>();
            Expression<Func<LoadingExpensesBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            Expression<Func<LoadingExpensesBill, bool>> last17 = list[list.Count - 1];
            Expression<Func<LoadingExpensesBill, bool>> exp17 = a => last17.Invoke(a) && (a.BillType == 3);
            list.Add(exp17);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<LoadingExpensesBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<LoadingExpensesBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<LoadingExpensesBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<LoadingExpensesBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<LoadingExpensesBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<LoadingExpensesBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<LoadingExpensesBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
    }
}