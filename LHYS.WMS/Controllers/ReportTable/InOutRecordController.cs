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
    public class InOutRecordController : Controller
    {
        private IRecordService RecordService { get; set; }
        // GET: InWarehouse
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 出入库总量查看表
        /// </summary>
        /// <returns></returns>
        public ActionResult TotalIndex()
        {
            return View();
        }

        public ActionResult TotalSearch(string DepartmentId, int? WarehouseId, DateTime? timestart, DateTime? timeend, int PageIndex, int PageSize)
        {
            int count = 0;
            double? sumCountIn = 0;
            double? sumWeightIn = 0;
            double? sumCountOut = 0;
            double? sumWeightOut = 0;
            //处理结束时间
            if (timeend.HasValue)
            {
                timeend = timeend.GetValueOrDefault().AddDays(1);
            }
            var list = RecordService.TotalSearch(DepartmentId, WarehouseId, timestart, timeend, PageIndex, PageSize, out count, out sumCountIn, out sumWeightIn, out sumCountOut, out sumWeightOut);
            return Json(new
            {
                sumCountIn = sumCountIn,
                sumWeightIn = sumWeightIn,
                sumCountOut = sumCountOut,
                sumWeightOut = sumWeightOut,
                totalCount = count,
                data = list
            });

        }

        public ActionResult RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, string[] BillTypes, string ItemBatch, string LBBillCode, string BillCode, DateTime? timestart, DateTime? timeend, int PageIndex, int PageSize)
        {
            int count = 0;
            double? sumCount = 0;
            double? sumWeight = 0;
            //处理结束时间
            if (timeend.HasValue)
            {
                timeend = timeend.GetValueOrDefault().AddDays(1);
            }
            var list = RecordService.RecordSearch(DepartmentId, WarehouseId, ItemLocationId, ItemCode, InOutTypeId, BillTypes, ItemBatch, LBBillCode, BillCode, timestart, timeend, PageIndex, PageSize, out count, out sumCount, out sumWeight);
            return Json(new
            {
                sumCount = sumCount,
                sumWeight = sumWeight,
                totalCount = count,
                data = list
            });
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(string DepartmentId, string WarehouseId, string ItemLocationId, string ItemCode, string InOutTypeId, string BillTypes, string ItemBatch, string LBBillCode, string BillCode, string timestart, string timeend)
        {
            int? WarehouseId1 = null;
            if (WarehouseId != "undefined")
            {
                WarehouseId1 = Convert.ToInt32(WarehouseId);
            }
          
            string[] temp = InOutTypeId.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);//分割出入库类型id
            int?[] InOutTypeIds = new int?[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                InOutTypeIds[i] = Convert.ToInt32(temp[i]);
            }
            string[] BillTypes1 = BillTypes.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);//分割单据类型

            DateTime? timestart1 = null;//开始时间
            DateTime? timeend1 = null;//结束时间
            if (timestart != "")
            {
                timestart1 = Convert.ToDateTime(timestart);
            }
            if (timeend != "")
            {
                timeend1 = Convert.ToDateTime(timeend).AddDays(1);
            }
            if (ItemLocationId == "") {
                ItemLocationId = null;
            }
            if (ItemCode=="") {
                ItemCode = null;
            }
            if (ItemBatch=="")
            {
                ItemBatch = null;
            }
            if (LBBillCode=="")
            {
                LBBillCode = null;
            }
            if (BillCode=="")
            {
                BillCode = null;
            }

            //获取全部数据
            var list = RecordService.ExportExcel(DepartmentId, WarehouseId1, ItemLocationId, ItemCode, InOutTypeIds, BillTypes1, ItemBatch, LBBillCode, BillCode, timestart1, timeend1);
            //转换成DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("单据类型");
            dt.Columns.Add("系统单号");
            dt.Columns.Add("客户单号");
            dt.Columns.Add("物料编码");
            dt.Columns.Add("物料名称");
            dt.Columns.Add("仓库名称");
            dt.Columns.Add("货位编码");
            dt.Columns.Add("批次");
            dt.Columns.Add("数量");
            dt.Columns.Add("出入库类型");
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["单据类型"] = ChangeTableType(item.MainTableType);
                dr["系统单号"] = item.BillCode;
                dr["客户单号"] = item.LBBillCode;
                dr["物料编码"] = item.ItemCode;
                dr["物料名称"] = item.ItemName;
                dr["仓库名称"] = item.Warehouse;
                dr["货位编码"] = item.ItemLocationId;
                dr["批次"] = item.ItemBatch;
                dr["数量"] = item.Count;
                dr["出入库类型"] = item.InOutTypeName;
                dt.Rows.Add(dr);
            }
            MemoryStream file = NPIOHelper.RenderToMemory(dt, "sheet1");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet";// "application/ms-excel/msword";
            Response.AddHeader("Content-Disposition", "attachment;fileName=出入库记录.xls");
            Response.BinaryWrite(file.ToArray());
            return new EmptyResult();
        }

        /// <summary>
        /// 修改单据类型名称
        /// </summary>
        /// <param name="english"></param>
        /// <returns></returns>
        private static  string ChangeTableType(string  english)
        {
            string res = "";
            switch (english)
            {
                case "TransferBill":
                    res = "入库单";
                    break;
                case "BackOutput":
                    res = "退货单";
                    break;
                case "OtherInput":
                    res = "其他入库单";
                    break;
                case "GiveBill":
                    res = "交货单";
                    break;
                case "BackInput":
                    res = "退库单";
                    break;
                case "OtherOutput":
                    res = "其他出库单";
                    break;
                case "DIYBill":
                    res = "加工单";
                    break;
                case "CheckBill":
                    res = "盘点单";
                    break;
                case "LocationChange":
                    res = "货位调整单";
                    break;
                default: res = english;
                    break;
            }
            return res;
        }
    }
}