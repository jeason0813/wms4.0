using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class OtherExpenseListController : Controller
    {
        private IOtherExpenseListService OtherExpenseListService { get; set; }
        private IOtherExpenseListDetailService OtherExpenseListDetailService { get; set; }
        public ActionResult Index()
        {
            if (Request["billType"] == null)
            {
                return Content("没有单据类型！");
            }
            string billType = Request["billType"];
            switch (billType)
            {
                case "OtherExpenseReceiveList":
                case "1":
                    Session["billType"] = 1;
                    Session["billTypeName"] = "其他费用应收单";
                    break;
                case "OtherExpenseGiveList":
                case "2":
                    Session["billType"] = 2;
                    Session["billTypeName"] = "其他费用应付单";
                    break;
                default:
                    Session["billType"] = null;
                    Session["billTypeName"] = "";
                    break;
            }
            ViewBag.billType = Session["billType"];//单据类型
            ViewBag.billTypeName = Session["billTypeName"];//单据类型名
            //如果传入单号参数  放入ViewBag
            if (Request["OtherExpenseListId"] != null)
            {
                ViewBag.OtherExpenseListId = Request["OtherExpenseListId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.OtherExpenseListId = "";
            }
            return View();
        }

        /// <summary>
        /// 单据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShow()
        {
            ViewBag.billType = Session["billType"];//单据类型
            ViewBag.billTypeName = Session["billTypeName"];//单据类型名
            if (Request["OtherExpenseListId"] != null)
            {
                ViewBag.OtherExpenseListId = Request["OtherExpenseListId"].ToString();
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
            if (Session["billType"] == null)//是否有单据类型
            {
                return Content("没有单据类型！");
            }
            string str = Request.Params["OtherExpenseListId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new OtherExpenseList());//返回一个新建的空对象
            }
            int billtype = Convert.ToInt32(Session["billType"].ToString().Trim());
            //如果有数据
            Guid OtherExpenseListId = new Guid(Request["OtherExpenseListId"]);//单据编号
            OtherExpenseList bill = OtherExpenseListService.LoadEntities(t => t.Id == OtherExpenseListId && t.BillType == billtype).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //保存表单数据
        public ActionResult SaveData(OtherExpenseList OtherExpenseList)
        {
            if (Session["billType"] == null)//是否有单据类型
            {
                return Content("没有单据类型！");
            }
            int billtype = Convert.ToInt32(Session["billType"].ToString().Trim());
            //参数对象可以对应接受数据
            OtherExpenseList.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = OtherExpenseListService.SaveData(OtherExpenseList, billtype);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string OtherExpenseListId = Request.Params["OtherExpenseListId"];
            if (string.IsNullOrEmpty(OtherExpenseListId))
            {
                res = "参数错误";
            }
            else
            {
                res = OtherExpenseListService.Examine(Guid.Parse(OtherExpenseListId), Session["UserName"].ToString());
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
                res = OtherExpenseListService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}