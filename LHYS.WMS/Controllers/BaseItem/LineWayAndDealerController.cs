using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
using Newtonsoft.Json;

namespace LHYS.WMS.Controllers
{
    public class LineWayAndDealerController : Controller
    {
        private ILineWayAndDealerService LineWayAndDealerService { get; set; }
        /// <summary>
        /// 从数据库加载数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();
                //主键有NULL 和 "" 需要过滤
                var list = LineWayAndDealerService.LoadEntities(a=>Power.Contains(a.DeptCode)&&a.DealerCode!=null&&a.DealerCode!="");                return Json(list);            }
        }
    }
}