using IBLL;
using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LHYS.WMS.Controllers
{
    public class ItemLineController : Controller
    {
        private IItemLineService ItemLineService { get; set; }

        // GET: ItemLine
        public ActionResult Index()
        {
            //测试数据用Session 
            Session["UserCode"] = "HR1406";
            Session["UserName"] = "朱洲";
            Session["Power"] = "LH6220";
            return View();
        }


        public ActionResult GetData()
        {
            if (Session["Power"] == null)
            {
                return null;
            }
            //查询有权限部门的数据
            else
            {
                string Power = Session["Power"].ToString();
                var list = ItemLineService.LoadEntities(a => Power.Contains(a.DepartmentId));
                return Json(list);
            }
        }

        public ActionResult SaveData()
        {
            string res = "";
            if (Request["SaveItem"] == null || Request["state"] == null)
            {
                res = "缺少参数";
            }
            else
            {
                string state = Request["state"].ToString();
                string item = Request["SaveItem"].ToString();
                ItemLine itemLine = JsonConvert.DeserializeObject<ItemLine>(item);
                if (state == "add")
                {
                    res = ItemLineService.AddEntity(itemLine) != null ? "添加成功" : "添加失败";
                }
                else if (state == "edit")//修改
                {
                    res = ItemLineService.EditEntity(itemLine) ? "修改成功" : "修改失败";
                }
            }
            return Content(res);

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
                ItemLine itemLine = JsonConvert.DeserializeObject<ItemLine>(item);
                res = ItemLineService.DeleteEntity(itemLine) ? "删除成功": "删除失败";
            }
            return Content(res);
        }
    }
}