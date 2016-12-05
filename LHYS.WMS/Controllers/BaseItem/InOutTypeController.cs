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
    public class InOutTypeController : Controller
    {
        // GET: InOutType
        private IInOutTypeService InOutTypeService { get; set; }
        public ActionResult Index()
        {
            Session["UserName"] = "朱洲";
            Session["UserCode"] = "HR1406";
            Session["Power"] = "LH6220,BM0067";
            return View();
        }
        /// <summary>
        /// 从数据库加载数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                var list = InOutTypeService.LoadEntities(a => Power.Contains(a.DepartmentId));                return Json(list);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData()
        {
            bool result = false;
            string dataitem = Request["InOutType"];
            string savetype = Request["savetype"].ToString().Trim();
            InOutType model_lgt = JsonConvert.DeserializeObject<InOutType>(dataitem);
            if (savetype == "add")
            {
                InOutTypeService.AddEntity(model_lgt);
                result = true;
            }
            else if (savetype == "edit")
            {
                result = InOutTypeService.EditEntity(model_lgt);
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
                InOutType itemLine = JsonConvert.DeserializeObject<InOutType>(item);
                res = InOutTypeService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}