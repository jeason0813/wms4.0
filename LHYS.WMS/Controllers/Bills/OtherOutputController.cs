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
    public class OtherOutputController : Controller
    {
        // GET: OtherOutput
        private IOtherOutputService OtherOutputService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["OtherOutputId"] != null)
            {
                ViewBag.OtherOutputId = Request["OtherOutputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.OtherOutputId = "";
            }
            return View();
        }
        public ActionResult IndexShow() {
            //如果传入单号参数  放入ViewBag
            if (Request["OtherOutputId"] != null)
            {
                ViewBag.OtherOutputId = Request["OtherOutputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.OtherOutputId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["OtherOutputId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new OtherOutput());//返回一个新建的空对象
            }
            //如果有数据
            Guid OtherOutputId = new Guid(Request["OtherOutputId"]);//单据编号
            OtherOutput bill = OtherOutputService.LoadEntities(t => t.Id == OtherOutputId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //保存表单数据
        public ActionResult SaveData(OtherOutput OtherOutput)
        {
            //参数对象可以对应接受数据
            OtherOutput.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = OtherOutputService.SaveData(OtherOutput);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string OtherOutputId = Request.Params["OtherOutputId"];
            if (string.IsNullOrEmpty(OtherOutputId))
            {
                res = "参数错误";
            }
            else
            {
                res = OtherOutputService.Examine(Guid.Parse(OtherOutputId), Session["UserName"].ToString());
            }
            return Content(res);
        }
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="billCode"></param>
        /// <returns></returns>

        public ActionResult GiveupExamine(string billCode)
        {
            string res = "";
            if (string.IsNullOrEmpty(billCode))
            {
                res = "参数错误";
            }
            else
            {
                res = OtherOutputService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="billCode">单号</param>
        /// <returns></returns>
        public ActionResult DeleteBill(Guid BillId)
        {
            return Content(OtherOutputService.DeleteBill(BillId));
        }
    }
}