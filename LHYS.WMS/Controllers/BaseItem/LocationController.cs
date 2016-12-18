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
    public class LocationController : Controller
    {
        // GET: Location
        public ILocationService LocationService { get; set; }
        public IWarehouseService WarehouseService { get; set; }
        public ActionResult Index()
        {
         
            return View();
        }

        /// <summary>
        /// 获取仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWareHouse() {
            if (Session["Power"] == null)
            {
                return null;
            }
            //查询有权限部门的数据
            else
            {
                string Power = Session["Power"].ToString();
                var list = WarehouseService.LoadEntities(a => Power.Contains(a.DepartmentId));
                return Json(list);
            }
        }
        public ActionResult GetData()
        {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                List<Location> list = LocationService.LoadEntities(a => Power.Contains(a.DepartmentId)).ToList();                List<Warehouse> listwarehouse = WarehouseService.LoadEntities(a => Power.Contains(a.DepartmentId)).ToList();                var listnew = from a in list
                              select new {
                                  warehouseName = listwarehouse.Where(j => j.Id == a.WarehouseId).FirstOrDefault().Name,
                                  Id = a.Id,
                                  Name=a.Name,
                                  WarehouseId=a.WarehouseId,
                                  Remark=a.Remark,
                                  CreateBy=a.CreateBy,
                                  CreateDate = a.CreateDate,
                                  Department=a.Department,
                                  DepartmentId=a.DepartmentId,
                                  CompanyId=a.CompanyId,
                                  Company=a.Company
                              };                return Json(listnew);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData()
        {
            bool result = false;
            string dataitem = Request["Location"];
            string savetype = Request["savetype"].ToString().Trim();
            Location model_lgt = JsonConvert.DeserializeObject<Location>(dataitem);
            model_lgt.CreateDate = DateTime.Now;
            model_lgt.CreateBy = Session["UserName"].ToString().Trim();
            if (savetype == "add")
            {
                LocationService.AddEntity(model_lgt);
                result = true;
            }
            else if (savetype == "edit")
            {
                result = LocationService.EditEntity(model_lgt);
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
                Location itemLine = JsonConvert.DeserializeObject<Location>(item);
                res = LocationService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}