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
        /// <summary>
        /// 旧版请求菜单 已弃用
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenu()
        {
            List<MenuA> list = MenuAService.LoadEntities(a => true).ToList();//获取所有菜单记录
            for (int i = 0; i < list.Count; i++)
            {
                list[i].MenuB = list[i].MenuB.OrderBy(a => a.OrderNumber).ToList();//按顺序排列
            }
            string res = JsonConvert.SerializeObject(list).Replace("Name", "text").Replace("MenuB", "nodes");//修改菜单属性
            return Content(res);
        }




    }
}