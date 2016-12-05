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
    public class HomeController : Controller
    {
        private IMenuAService MenuAService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMenu()
        {
            List<MenuA> list = MenuAService.LoadEntities(a => true).ToList();//获取所有菜单记录
            string res = JsonConvert.SerializeObject(list).Replace("Name", "text").Replace("MenuB", "nodes");
            return Content(res);

        }




    }
}