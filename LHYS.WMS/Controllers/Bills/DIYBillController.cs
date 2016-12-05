using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class DIYBillController : Controller
    {
        // GET: DIYBill
        private IDIYBillService DIYBillService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["DIYBillId"] != null)
            {
                ViewBag.DIYBillId = Request["DIYBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.DIYBillId = "";
            }
            return View();
        }
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["DIYBillId"] != null)
            {
                ViewBag.DIYBillId = Request["DIYBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.DIYBillId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["DIYBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                DIYBill billdiy = new DIYBill();
                List<Record> list1 = new List<Record>();
                List<Record> list2 = new List<Record>();
                var ress = new {
                    diybill=billdiy,
                    recordlist1=list1,
                    recordlist2= list2
                };
                return Json(ress);//返回一个新建的空对象
            }
            else
            {
                //如果有数据
                Guid DIYBillId = new Guid(Request["DIYBillId"]);//单据编号
                DIYBill bill = DIYBillService.LoadEntities(t => t.Id == DIYBillId).FirstOrDefault();//获取表单
                List<Record> list1 = RecordService.LoadEntities(a => a.MainTableId == bill.Id&&a.InOrOut==0).ToList();
                List<Record> list2 = RecordService.LoadEntities(b => b.MainTableId == bill.Id&&b.InOrOut==1).ToList();
                var ress = new
                {
                    diybill = bill,
                    recordlist1 = list1,
                    recordlist2 = list2
                };
                return Json(ress);
            }
        }

        //保存表单数据
        public ActionResult SaveData(DIYBill DIYBill, List<Record> recordlist1, List<Record> recordlist2)
        {
            //参数对象可以对应接受数据
            DIYBill.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = DIYBillService.SaveData(DIYBill,recordlist1,recordlist2);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string DIYBillId = Request.Params["DIYBillId"];
            if (string.IsNullOrEmpty(DIYBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = DIYBillService.Examine(Guid.Parse(DIYBillId), Session["UserName"].ToString());
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
                res = DIYBillService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}