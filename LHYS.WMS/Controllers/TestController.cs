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
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
                int c = 2;
                int d = 0;
            return View();
        }

        public ActionResult Test1()
        {
            return PartialView();
        }

        public ActionResult Test2()
        {

            return View();
        }
    }
}