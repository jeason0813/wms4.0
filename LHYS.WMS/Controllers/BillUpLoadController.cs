using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class BillUpLoadController : Controller
    {

        private IBillUpLoadService BillUpLoadService { get; set; }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>返回路径</returns>
        public ActionResult UpLoadBill(HttpPostedFileBase file)
        {
            string path = Request.MapPath("/BillUpLoad/" + DateTime.Now.ToString("yyyyMMddHHMMss") + "-" + file.FileName);
            file.SaveAs(path);
            return Content(path);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public ActionResult Save(string[] files)
        {
            string res = "";
           
                res = BillUpLoadService.Save(files);
           
            return Content(res);
        }

      

      
    }
}