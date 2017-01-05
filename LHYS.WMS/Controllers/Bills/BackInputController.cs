using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class BackInputController : Controller
    {
        // GET: BackInput
        private IBackInputService BackInputService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["BackInputId"] != null)
            {
                ViewBag.BackInputId = Request["BackInputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.BackInputId = "";
            }
            return View();
        }
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["BackInputId"] != null)
            {
                ViewBag.BackInputId = Request["BackInputId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.BackInputId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["BackInputId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new BackInput());//返回一个新建的空对象
            }
            //如果有数据
            Guid BackInputId = new Guid(Request["BackInputId"]);//单据编号
            BackInput bill = BackInputService.LoadEntities(t => t.Id == BackInputId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //保存表单数据
        public ActionResult SaveData(BackInput BackInput)
        {
            //参数对象可以对应接受数据
            BackInput.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = BackInputService.SaveData(BackInput);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string BackInputId = Request.Params["BackInputId"];
            if (string.IsNullOrEmpty(BackInputId))
            {
                res = "参数错误";
            }
            else
            {
                res = BackInputService.Examine(Guid.Parse(BackInputId), Session["UserName"].ToString());
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
                res = BackInputService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
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
            return Content(BackInputService.DeleteBill(BillId));
        }
    }
}