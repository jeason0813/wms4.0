using IBLL;
using System;
using System.Collections.Generic;
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
            var list = RecordService.TotalSearch(DepartmentId, WarehouseId, timestart, timeend, PageIndex, PageSize, out count, out sumCountIn, out sumWeightIn,out  sumCountOut,out sumWeightOut);
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

        public ActionResult RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId,string[] BillTypes,string ItemBatch, string LBBillCode,string BillCode, DateTime? timestart, DateTime? timeend,int PageIndex,int PageSize)
        {
            int count = 0;
            double? sumCount = 0;
            double? sumWeight = 0;
            //处理结束时间
            if (timeend.HasValue) {
               timeend= timeend.GetValueOrDefault().AddDays(1);
            }
            var list = RecordService.RecordSearch(DepartmentId, WarehouseId, ItemLocationId, ItemCode, InOutTypeId, BillTypes,ItemBatch,LBBillCode,BillCode ,timestart, timeend, PageIndex, PageSize,out count,out sumCount,out sumWeight);
            return Json(new {
                sumCount=sumCount,
                sumWeight=sumWeight,
                totalCount = count,
                data=list
            });
        }
    }
}