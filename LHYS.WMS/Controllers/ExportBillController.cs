using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
using System.IO;

namespace LHYS.WMS.Controllers
{
    public class ExportBillController : Controller
    {
        public ActionResult ExprotExcel(string billType,string billId)
        {
            return Content("/Template/交货单模板.xlsx");
        }
    }
}