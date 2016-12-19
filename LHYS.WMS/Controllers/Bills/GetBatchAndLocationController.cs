using DBModel;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class GetBatchAndLocationController : Controller
    {
        private IInWarehouseService InWarehouseService { get; set; }
       public ActionResult GetBatchAndLocations(string itemcode,int warehouseid) {
            var warehouselist = InWarehouseService.LoadEntities(a=>a.ItemCode==itemcode.ToString().Trim()&&a.WarehouseId==warehouseid);
            return Json(warehouselist);
        }
    }
}