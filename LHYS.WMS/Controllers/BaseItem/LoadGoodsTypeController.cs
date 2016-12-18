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
    public class LoadGoodsTypeController : Controller
    {
        // GET: LoadGoodsType
        private ILoadGoodsTypeService LoadGoodsTypeService { get; set; }
        public ActionResult Index()
        {
           
            return View();
        }
        /// <summary>
        /// 从数据库加载数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoadGoodsType() {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                var list = LoadGoodsTypeService.LoadEntities(a => Power.Contains(a.DepartmentId));                return Json(list);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData() {
            bool result = false;
            string dataitem = Request["LoadGoodsType"];
            string savetype = Request["savetype"].ToString().Trim();
            LoadGoodsType model_lgt = JsonConvert.DeserializeObject<LoadGoodsType>(dataitem);
            if (savetype == "add")
            {
                LoadGoodsTypeService.AddEntity(model_lgt);
                result = true;
            }
            else if(savetype=="edit") {
                result = LoadGoodsTypeService.EditEntity(model_lgt);
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
                LoadGoodsType itemLine = JsonConvert.DeserializeObject<LoadGoodsType>(item);
                res = LoadGoodsTypeService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}