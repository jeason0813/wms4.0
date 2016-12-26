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
            string result = "";
            string dataitem = Request["Warehouse"];
            string savetype = Request["savetype"].ToString().Trim();
            Warehouse model_lgt = JsonConvert.DeserializeObject<Warehouse>(dataitem);
            model_lgt.CreateBy = Session["UserName"].ToString().Trim();
            model_lgt.CreateDate = DateTime.Now;
            if (savetype == "add")
            {
                var warehouse = WarehouseService.LoadEntities(a=>a.Name.Trim()==model_lgt.Name.Trim()).FirstOrDefault();
                if (warehouse!=null) {
                    result = "仓库名称与数据库相同，请重新填写！";
                }
                else
                {
                    WarehouseService.AddEntity(model_lgt);
                    result = "保存成功！";
                }
            }
            else if (savetype == "edit")
            {
                if (WarehouseService.EditEntity(model_lgt))
                {
                    result = "编辑成功！";
                }
                else {
                    result = "编辑失败！";
                }
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