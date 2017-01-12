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

       
        public ActionResult RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, DateTime? timestart, DateTime? timeend)
        {
            var list = RecordService.RecordSearch(DepartmentId, WarehouseId, ItemLocationId, ItemCode, InOutTypeId, timestart, timeend).OrderByDescending(a=>a.ExamineDate);
            return Json(list);
        }

    }
}