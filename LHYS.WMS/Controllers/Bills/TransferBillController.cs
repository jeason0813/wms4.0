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
    public class TransferBillController : Controller
    {
        // GET: TransferBill
        private ITransferBillService TransferBillService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["TransferBillId"] != null)
            {
                ViewBag.TransferBillId = Request["TransferBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.TransferBillId = "";
            }
            return View();
        }

        public ActionResult IndexShow()
        {
            if (Request["TransferBillId"] != null)
            {
                ViewBag.TransferBillId = Request["TransferBillId"].ToString();
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
            string str = Request.Params["TransferBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new TransferBill());//返回一个新建的空对象
            }
            //如果有数据
            Guid TransferBillId = new Guid(Request["TransferBillId"]);//单据编号
            TransferBill bill = TransferBillService.LoadEntities(t => t.Id == TransferBillId).FirstOrDefault();//获取表单
            return Json(bill);
        }

        //保存表单数据
        public ActionResult SaveData(TransferBill TransferBill)
        {
            //参数对象可以对应接受数据
            TransferBill.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = TransferBillService.SaveData(TransferBill);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string transferBillId = Request.Params["TransferBillId"];
            if (string.IsNullOrEmpty(transferBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = TransferBillService.Examine(Guid.Parse(transferBillId), Session["UserName"].ToString()) ? "审核成功" : "审核失败";
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
                res = TransferBillService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
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
            return Content(TransferBillService.DeleteBill(BillId));
        }
    }
}