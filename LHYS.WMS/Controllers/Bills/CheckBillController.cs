using BLL;
using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class CheckBillController : Controller
    {

        private ICheckBillService CheckBillService { get; set; }
        // GET: CheckBill
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["CheckBillId"] != null)
            {
                ViewBag.CheckBillId = Request["CheckBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.CheckBillId = "";
            }
            return View();
        }

        /// <summary>
        /// 单据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShow()
        {
            if (Request["CheckBillId"] != null)
            {
                ViewBag.CheckBillId = Request["CheckBillId"].ToString();
            }
            else //没传参数 放入空
            {
                return Content("参数错误");
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["CheckBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new CheckBill());//返回一个新建的空对象
            }
            //如果有数据
            Guid CheckBillId = new Guid(Request["CheckBillId"]);//单据编号
            CheckBill bill = CheckBillService.LoadEntities(t => t.Id == CheckBillId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //保存数据
        public ActionResult Save(CheckBill CheckBill)
        {
            string res = CheckBillService.Save(CheckBill, Session["UserName"].ToString());
            return Content(res);
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string CheckBillId = Request.Params["CheckBillId"];
            if (string.IsNullOrEmpty(CheckBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = CheckBillService.Examine(Guid.Parse(CheckBillId), Session["UserName"].ToString());
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
                res = CheckBillService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}