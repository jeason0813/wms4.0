﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
using Newtonsoft.Json;
namespace LHYS.WMS.Controllers
{
    public class ReceiveFileController : Controller
    {
        // GET: ReceiveFile
        public IReceiveFileService ReceiveFileService { get; set; }
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetData()
        {
            if (Session["Power"] == null)            {                return null;            }
            //查询有权限部门的数据
            else
            {                string Power = Session["Power"].ToString();                var list = ReceiveFileService.LoadEntities(a => Power.Contains(a.DepartmentId));                return Json(list);            }
        }
        /// <summary>
        /// 添加和修改
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveData()
        {
            bool result = false;
            string dataitem = Request["ReceiveFile"];
            string savetype = Request["savetype"].ToString().Trim();
            ReceiveFile model_lgt = JsonConvert.DeserializeObject<ReceiveFile>(dataitem);
            model_lgt.CreateBy = Session["UserName"].ToString().Trim();
            model_lgt.CreateDate = DateTime.Now;
            if (savetype == "add")
            {
                ReceiveFileService.AddEntity(model_lgt);
                result = true;
            }
            else if (savetype == "edit")
            {
                result = ReceiveFileService.EditEntity(model_lgt);
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
                ReceiveFile itemLine = JsonConvert.DeserializeObject<ReceiveFile>(item);
                res = ReceiveFileService.DeleteEntity(itemLine) ? "删除成功" : "删除失败";
            }
            return Content(res);
        }
    }
}