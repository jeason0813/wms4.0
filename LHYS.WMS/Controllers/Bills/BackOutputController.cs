using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class BackOutputController : Controller
    {
        // GET: BackOutput
        private IBackOutputService BackOutputService { get; set; }
        private IRecordService RecordService { get; set; }
        private ILineWayService LineWayService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["BackOutputId"] != null)
            {
                ViewBag.BackOutputId = Request["BackOutputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.BackOutputId = "";
            }
            return View();
        }
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["BackOutputId"] != null)
            {
                ViewBag.BackOutputId = Request["BackOutputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.BackOutputId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["BackOutputId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new BackOutput());//返回一个新建的空对象
            }
            //如果有数据
            Guid BackOutputId = new Guid(Request["BackOutputId"]);//单据编号
            BackOutput bill = BackOutputService.LoadEntities(t => t.Id == BackOutputId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        public ActionResult GetLineWay()
        {
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
        public ActionResult SaveData(BackOutput BackOutput)
        {
            //参数对象可以对应接受数据
            BackOutput.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = BackOutputService.SaveData(BackOutput);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string BackOutputId = Request.Params["BackOutputId"];
            if (string.IsNullOrEmpty(BackOutputId))
            {
                res = "参数错误";
            }
            else
            {
                res = BackOutputService.Examine(Guid.Parse(BackOutputId), Session["UserName"].ToString());
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
                res = BackOutputService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
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
            return Content(BackOutputService.DeleteBill(BillId));
        }
    }
}