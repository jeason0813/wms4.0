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
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        private IWarehouseService WarehouseService { get; set; }
        public ActionResult Index()
        {
            Session["UserName"] = "朱洲";
            Session["UserCode"] = "HR1406";
            Session["Power"] = "LH6220";
            return View();
        }
        public ActionResult GetData()
        {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                var list = WarehouseService.LoadEntities(a => Power.Contains(a.DepartmentId));                return Json(list);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData()
        {
            bool result = false;
            string dataitem = Request["Warehouse"];
            string savetype = Request["savetype"].ToString().Trim();
            Warehouse model_lgt = JsonConvert.DeserializeObject<Warehouse>(dataitem);
            if (savetype == "add")
            {
                WarehouseService.AddEntity(model_lgt);
                result = true;
            }
            else if (savetype == "edit")
            {
                result = WarehouseService.EditEntity(model_lgt);
            }
            return Content(result.ToString());
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteData()
        {
            string res = "";
            if (Request["DeleteItem"] == null)
            {
                res = "参数错误！";
            }
            else
            {
                string item = Request["DeleteItem"].ToString();
                Warehouse itemLine = JsonConvert.DeserializeObject<Warehouse>(item);
                res = WarehouseService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}