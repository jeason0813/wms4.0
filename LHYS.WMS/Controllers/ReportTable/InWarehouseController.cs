using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class InWarehouseController : Controller
    {
        private IInWarehouseService InWarehouseService { get; set; }
        // GET: InWarehouse
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据仓库查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataByWarehouseId(int WarehouseId)
        {
            var list = InWarehouseService.LoadEntities(a=>a.WarehouseId==WarehouseId);
            return Json(list);
        }
    }
}