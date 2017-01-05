using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class GiveBillController : Controller
    {
        // GET: GiveBill
        private IGiveBillService GiveBillService { get; set; }
        private IRecordService RecordService { get; set; }
        private ILineWayService LineWayService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["GiveBillId"] != null)
            {
                ViewBag.GiveBillId = Request["GiveBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.GiveBillId = "";
            }
            return View();
        }
        public ActionResult IndexShow() {
            //如果传入单号参数  放入ViewBag
            if (Request["GiveBillId"] != null)
            {
                ViewBag.GiveBillId = Request["GiveBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.GiveBillId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["GiveBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new GiveBill());//返回一个新建的空对象
            }
            //如果有数据
            Guid GiveBillId = new Guid(Request["GiveBillId"]);//单据编号
            GiveBill bill = GiveBillService.LoadEntities(t => t.Id == GiveBillId).FirstOrDefault();//获取表单
            return Json(bill);
        }
       /// <summary>
       /// 根据立邦交货单号获取数据
       /// </summary>
       /// <returns></returns>
        public ActionResult GetDataByBillCode(string BillCode)
        {
            GiveBill bill = GiveBillService.LoadEntities(t => t.BillCode == BillCode).FirstOrDefault();//获取表单
            if (bill == null)
            {
                return Content("no");
            }
            else {
                return Json(bill);
            }
           
        }
        /// <summary>
        /// 查找任务单下的交货单
        /// </summary>
        /// <param name="taskBillId"></param>
        /// <returns></returns>
        public ActionResult GetTaskBillsGiveBill( string TaskBillCode)
        {
           var list= GiveBillService.LoadEntities(a => a.TaskBillCode== TaskBillCode);
            return Json(list);
        }
        public ActionResult GetLineWay() {
            if (Session["Power"] == null)
            {
                return null;
            }
            else
            {
                string Power = Session["Power"].ToString();//权限字符串
                var list1 = LineWayService.LoadEntities(a => Power.Contains(a.DepartmentId));//线路
                return Json(list1);
            }
        }
        //保存表单数据
        public ActionResult SaveData(GiveBill givebill)
        {
            //参数对象可以对应接受数据
            givebill.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = GiveBillService.SaveData(givebill);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string GiveBillId = Request.Params["GiveBillId"];
            if (string.IsNullOrEmpty(GiveBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = GiveBillService.Examine(Guid.Parse(GiveBillId), Session["UserName"].ToString());
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
                res = GiveBillService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
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
            return Content(GiveBillService.DeleteBill(BillId));
        }
    }
}