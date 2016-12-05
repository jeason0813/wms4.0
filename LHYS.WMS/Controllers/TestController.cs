using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;

namespace LHYS.WMS.Controllers
{
    public class TestController : Controller
    {
        private IMenuAService MenuAService { get; set; }
        // GET: Test
        public ActionResult Index()
        {
            var res=MenuAService.LoadEntities(a=>true);
            return View();
        }

        public ActionResult Test1() {
            return PartialView();
        }
        public ActionResult Test2()
        {
            return PartialView();
        }
        public ActionResult Test4()
        {
            return PartialView();
        }
    }
}