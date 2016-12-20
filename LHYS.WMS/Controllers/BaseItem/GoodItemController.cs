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
    public class GoodItemController : Controller
    {


        // GET: GoodItem
        private  IGoodItemService GoodItemService { get; set; }
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetData() {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                var list = GoodItemService.LoadEntities(a => Power.Contains(a.DepartmentId));                return Json(list);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData()
        {
            string result = "";
            string dataitem = Request["GoodItem"];
            string savetype = Request["savetype"].ToString().Trim();
            GoodItem model_lgt = JsonConvert.DeserializeObject<GoodItem>(dataitem);
            model_lgt.CreateDate = DateTime.Now;
            model_lgt.CreateBy = Session["UserName"].ToString().Trim();
            if (savetype == "add")
            {
                var gooditemlist = GoodItemService.LoadEntities(a=>a.ItemCode==model_lgt.ItemCode).FirstOrDefault();
                if (gooditemlist == null)
                {
                    GoodItemService.AddEntity(model_lgt);
                    result = "保存成功！";
                }
                else {
                    result = "编码重复，请重新填写！";
                }
            }
            else if (savetype == "edit")
            {
                if (GoodItemService.EditEntity(model_lgt))
                {
                    result = "编辑成功！";
                }
                else {
                    result = "编辑失败";
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
                GoodItem itemLine = JsonConvert.DeserializeObject<GoodItem>(item);
                res = GoodItemService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}