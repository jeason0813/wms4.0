using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class OtherInputController : Controller
    {
        // GET: OtherInput
        private IOtherInputService OtherInputService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["OtherInputId"] != null)
            {
                ViewBag.OtherInputId = Request["OtherInputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.OtherInputId = "";
            }
            return View();
        }
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["OtherInputId"] != null)
            {
                ViewBag.OtherInputId = Request["OtherInputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.OtherInputId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["OtherInputId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new OtherInput());//返回一个新建的空对象
            }
            //如果有数据
            Guid OtherInputId = new Guid(Request["OtherInputId"]);//单据编号
            OtherInput bill = OtherInputService.LoadEntities(t => t.Id == OtherInputId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //保存表单数据
        public ActionResult SaveData(OtherInput OtherInput)
        {
            //参数对象可以对应接受数据
            OtherInput.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = OtherInputService.SaveData(OtherInput);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string OtherInputId = Request.Params["OtherInputId"];
            if (string.IsNullOrEmpty(OtherInputId))
            {
                res = "参数错误";
            }
            else
            {
                res = OtherInputService.Examine(Guid.Parse(OtherInputId), Session["UserName"].ToString());
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
                res = OtherInputService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}