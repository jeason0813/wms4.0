using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LHYS.WMS.Controllers
{
    public class MenuManagerController:Controller
    {
        private IMenuService MenuService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
           var list= MenuService.GetData("test");
           return Json(list);
        }
    }
}