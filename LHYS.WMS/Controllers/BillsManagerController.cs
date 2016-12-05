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
    public class BillsManagerController : Controller
    {
        private ITransferBillService TransferBillService { get; set; }
        private IBackInputService BackInputService { get; set; }
        private IGiveBillService GiveBillService { get; set; }
        private ITaskBillService TaskBillService { get; set; }
        private IBackOutputService BackOutputService { get; set; }
        private IOtherInputService OtherInputService { get; set; }
        private IOtherOutputService OtherOutputService { get; set; }
        private IDIYBillService DIYBillService { get; set; }
        private ICheckBillService CheckBillService { get; set; }
        private ILocationChangeService LocationChangeService { get; set; }

        // GET: BillsManager
        public ActionResult Index()
        {
            if (Request["billType"] == null)
            {
                return Content("没有单据类型！");
            }
            string billType = Request["billType"];
            string billTypeName = "";
            switch (billType)
            {
                case "TransferBill":
                    billTypeName = "转仓单";
                    break;
                case "BackInput":
                    billTypeName = "退仓单";
                    break;
                case "GiveBill":
                    billTypeName = "交货单";
                    break;
                case "TaskBill":
                    billTypeName = "任务单";
                    break;
                case "GiveBackBill":
                    billTypeName = "交货回执单";
                    break;
                case "BackOutput":
                    billTypeName = "退货单";
                    break;
                case "OtherInput":
                    billTypeName = "其他入库单";
                    break;
                case "OtherOutput":
                    billTypeName = "其他出库单";
                    break;
                case "DIYBill":
                    billTypeName = "加工单";
                    break;
                case "CheckBill":
                    billTypeName = "盘点单";
                    break;
                case "LocationChange":
                    billTypeName = "货位调整单";
                    break;
                default:
                    billTypeName = "";
                    break;
            }
            ViewBag.billType = billType;//单据类型
            ViewBag.billTypeName = billTypeName;//单据类型名
            return View();
        }

        public ActionResult SearchBills(string billType, int pageIndex, int pageSize, string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode, string LBBillCode)
        {
            int totalCount = 0; //总记录数
            switch (billType)
            {
                case "TransferBill":
                    Expression<Func<TransferBill, bool>> exp1 = TransferBillSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<TransferBill> res1 = TransferBillService.LoadPageEntities(exp1.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res1,
                        totalCount = totalCount

                    });

                case "BackInput":
                    Expression<Func<BackInput, bool>> exp2 = BackInputSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<BackInput> res2 = BackInputService.LoadPageEntities(exp2.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res2,
                        totalCount = totalCount
                    });
                case "GiveBill":
                case "GiveBackBill":
                    Expression<Func<GiveBill, bool>> exp3 = GiveBillSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode, LBBillCode);
                    List<GiveBill> res3 = GiveBillService.LoadPageEntities(exp3.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res3,
                        totalCount = totalCount
                    });
                case "TaskBill":
                    Expression<Func<TaskBill, bool>> exp4 = TaskBillSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<TaskBill> res4 = TaskBillService.LoadPageEntities(exp4.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res4,
                        totalCount = totalCount
                    });
                case "BackOutput":
                    Expression<Func<BackOutput, bool>> exp5 = BackOutputSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<BackOutput> res5 = BackOutputService.LoadPageEntities(exp5.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res5,
                        totalCount = totalCount
                    });
                case "OtherInput":
                    Expression<Func<OtherInput, bool>> exp6 = OtherInputSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<OtherInput> res6 = OtherInputService.LoadPageEntities(exp6.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res6,
                        totalCount = totalCount
                    });
                case "OtherOutput":
                    Expression<Func<OtherOutput, bool>> exp7 = OtherOutputSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<OtherOutput> res7 = OtherOutputService.LoadPageEntities(exp7.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res7,
                        totalCount = totalCount
                    });
                case "DIYBill":
                    Expression<Func<DIYBill, bool>> exp8 = DIYBillSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<DIYBill> res8 = DIYBillService.LoadPageEntities(exp8.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res8,
                        totalCount = totalCount
                    });
                case "CheckBill":
                    Expression<Func<CheckBill, bool>> exp9 = CheckBillSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<CheckBill> res9 = CheckBillService.LoadPageEntities(exp9.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res9,
                        totalCount = totalCount
                    });
                case "LocationChange":
                    Expression<Func<LocationChange, bool>> exp10 = LocationChangeSearchCondition(timestart, timeend, BillState, DepartmentId, WarehouseId, BillCode);
                    List<LocationChange> res10 = LocationChangeService.LoadPageEntities(exp10.Expand(), a => a.CreateDate, pageIndex, pageSize, false, out totalCount).ToList();
                    return Json(new
                    {
                        data = res10,
                        totalCount = totalCount
                    });
            }
            return Content("参数错误");
        }
        /// <summary>
        /// 转仓单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<TransferBill, bool>> TransferBillSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<TransferBill, bool>>> list = new List<Expression<Func<TransferBill, bool>>>();
            Expression<Func<TransferBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<TransferBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<TransferBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<TransferBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<TransferBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<TransferBill, bool>> last5 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<TransferBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<TransferBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<TransferBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }

        /// <summary>
        /// 退仓单查询条件
        /// </summary>
        /// <returns></returns>
        private static Expression<Func<BackInput, bool>> BackInputSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<BackInput, bool>>> list = new List<Expression<Func<BackInput, bool>>>();
            Expression<Func<BackInput, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<BackInput, bool>> last1 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<BackInput, bool>> last2 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<BackInput, bool>> last3 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<BackInput, bool>> last4 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<BackInput, bool>> last5 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<BackInput, bool>> last6 = list[list.Count - 1];
                Expression<Func<BackInput, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<BackInput, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }

        /// <summary>
        /// 交货单 、交货回执单查询条件
        /// </summary>
        private static Expression<Func<GiveBill, bool>> GiveBillSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode, string LBBillCode)
        {
            List<Expression<Func<GiveBill, bool>>> list = new List<Expression<Func<GiveBill, bool>>>();
            Expression<Func<GiveBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<GiveBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<GiveBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<GiveBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<GiveBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<GiveBill, bool>> last5 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<GiveBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }

            if (!string.IsNullOrEmpty(LBBillCode))
            {
                Expression<Func<GiveBill, bool>> last7 = list[list.Count - 1];
                Expression<Func<GiveBill, bool>> exp7 = a => last7.Invoke(a) && a.LBBillCode.Contains(LBBillCode);
                list.Add(exp7);
            }
            Expression<Func<GiveBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }

        private static Expression<Func<TaskBill, bool>> TaskBillSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<TaskBill, bool>>> list = new List<Expression<Func<TaskBill, bool>>>();
            Expression<Func<TaskBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<TaskBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<TaskBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<TaskBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<TaskBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<TaskBill, bool>> last5 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<TaskBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<TaskBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<TaskBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<BackOutput, bool>> BackOutputSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<BackOutput, bool>>> list = new List<Expression<Func<BackOutput, bool>>>();
            Expression<Func<BackOutput, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<BackOutput, bool>> last1 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<BackOutput, bool>> last2 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<BackOutput, bool>> last3 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<BackOutput, bool>> last4 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<BackOutput, bool>> last5 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<BackOutput, bool>> last6 = list[list.Count - 1];
                Expression<Func<BackOutput, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<BackOutput, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<OtherInput, bool>> OtherInputSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<OtherInput, bool>>> list = new List<Expression<Func<OtherInput, bool>>>();
            Expression<Func<OtherInput, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<OtherInput, bool>> last1 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<OtherInput, bool>> last2 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<OtherInput, bool>> last3 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<OtherInput, bool>> last4 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<OtherInput, bool>> last5 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<OtherInput, bool>> last6 = list[list.Count - 1];
                Expression<Func<OtherInput, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<OtherInput, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<OtherOutput, bool>> OtherOutputSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<OtherOutput, bool>>> list = new List<Expression<Func<OtherOutput, bool>>>();
            Expression<Func<OtherOutput, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<OtherOutput, bool>> last1 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<OtherOutput, bool>> last2 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<OtherOutput, bool>> last3 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<OtherOutput, bool>> last4 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<OtherOutput, bool>> last5 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<OtherOutput, bool>> last6 = list[list.Count - 1];
                Expression<Func<OtherOutput, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<OtherOutput, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<DIYBill, bool>> DIYBillSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<DIYBill, bool>>> list = new List<Expression<Func<DIYBill, bool>>>();
            Expression<Func<DIYBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<DIYBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<DIYBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<DIYBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<DIYBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<DIYBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<DIYBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<DIYBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<DIYBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            //没这个条件
            //if (WarehouseId != null)
            //{
            //    Expression<Func<DIYBill, bool>> last5 = list[list.Count - 1];
            //    Expression<Func<DIYBill, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
            //    list.Add(exp5);
            //}
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<DIYBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<DIYBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<DIYBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<CheckBill, bool>> CheckBillSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<CheckBill, bool>>> list = new List<Expression<Func<CheckBill, bool>>>();
            Expression<Func<CheckBill, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<CheckBill, bool>> last1 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<CheckBill, bool>> last2 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<CheckBill, bool>> last3 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<CheckBill, bool>> last4 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            if (WarehouseId != null)
            {
                Expression<Func<CheckBill, bool>> last5 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
                list.Add(exp5);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<CheckBill, bool>> last6 = list[list.Count - 1];
                Expression<Func<CheckBill, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<CheckBill, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
        private static Expression<Func<LocationChange, bool>> LocationChangeSearchCondition(string timestart, string timeend, string BillState, string DepartmentId, int? WarehouseId, string BillCode)
        {
            List<Expression<Func<LocationChange, bool>>> list = new List<Expression<Func<LocationChange, bool>>>();
            Expression<Func<LocationChange, bool>> exp = a => true; //初始条件
            list.Add(exp);
            if (!string.IsNullOrEmpty(timestart))
            {
                DateTime dt1 = Convert.ToDateTime(timestart);
                Expression<Func<LocationChange, bool>> last1 = list[list.Count - 1];
                Expression<Func<LocationChange, bool>> exp1 = a => last1.Invoke(a) && (a.CreateDate >= dt1);
                list.Add(exp1);
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                DateTime dt2 = Convert.ToDateTime(timeend);
                Expression<Func<LocationChange, bool>> last2 = list[list.Count - 1];
                Expression<Func<LocationChange, bool>> exp2 = a => last2.Invoke(a) && (a.CreateDate <= dt2);
                list.Add(exp2);
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                int state = Convert.ToInt32(BillState);
                Expression<Func<LocationChange, bool>> last3 = list[list.Count - 1];
                Expression<Func<LocationChange, bool>> exp3 = a => last3.Invoke(a) && a.BillState == state;
                list.Add(exp3);

            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                Expression<Func<LocationChange, bool>> last4 = list[list.Count - 1];
                Expression<Func<LocationChange, bool>> exp4 = a => last4.Invoke(a) && a.DepartmentId == DepartmentId;
                list.Add(exp4);
            }
            //没有货位
            //if (WarehouseId != null)
            //{
            //    Expression<Func<LocationChange, bool>> last5 = list[list.Count - 1];
            //    Expression<Func<LocationChange, bool>> exp5 = a => last5.Invoke(a) && a.WarehouseId == WarehouseId;
            //    list.Add(exp5);
            //}
            if (!string.IsNullOrEmpty(BillCode))
            {
                Expression<Func<LocationChange, bool>> last6 = list[list.Count - 1];
                Expression<Func<LocationChange, bool>> exp6 = a => last6.Invoke(a) && a.BillCode.Contains(BillCode);
                list.Add(exp6);
            }
            Expression<Func<LocationChange, bool>> final = list[list.Count - 1]; //最终表达式
            return final;

        }
    }
}