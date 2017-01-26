using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class LoadingExpensesBillController : Controller
    {
        private ILoadingExpensesBillService LoadingExpensesBillService { get; set; }
        public ActionResult Index()
        {
            if (Request["billType"] == null)
            {
                return Content("没有单据类型！");
            }
            string billType = Request["billType"];
            switch (billType)
            {
                case "LoadingCostReceiveList":
                case "1":
                    ViewBag.billType = 1;//单据类型
                    ViewBag.billTypeName = "装卸费应收单";//单据类型名
                    break;
                case "LoadingCostGiveList":
                case "2":
                    ViewBag.billType = 2;//单据类型
                    ViewBag.billTypeName = "装卸费成本单";//单据类型名
                    break;
                case "LaborReceiveList":
                case "3":
                    ViewBag.billType = 3;//单据类型
                    ViewBag.billTypeName = "力资费应收单";//单据类型名
                    break;
            }
            //如果传入单号参数  放入ViewBag
            if (Request["LoadingExpensesBillId"] != null)
            {
                ViewBag.LoadingExpensesBillId = Request["LoadingExpensesBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.LoadingExpensesBillId = "";
            }
            return View();
        }

        /// <summary>
        /// 单据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShow()
        {
            ViewBag.billType = Request["billType"];//单据类型
            switch (Request["billType"])
            {
                case "1": case "LoadingCostReceiveList": ViewBag.billTypeName="装卸费应收单"; break;
                case "2": case "LoadingCostGiveList": ViewBag.billTypeName="装卸费成本单"; break;
                case "3": case "LaborReceiveList": ViewBag.billTypeName="力资费应收单"; break;
            }
            if (Request["LoadingExpensesBillId"] != null)
            {
                ViewBag.LoadingExpensesBillId = Request["LoadingExpensesBillId"].ToString();
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
            if (Request["billType"] == null)//是否有单据类型
            {
                return Content("没有单据类型！");
            }
            int billtype=0;
            switch (Request["billType"])
            {
                case "1": case "LoadingCostReceiveList": billtype = 1;break;
                case "2": case "LoadingCostGiveList": billtype = 2; break;
                case "3": case "LaborReceiveList": billtype = 3; break;
            }
            string str = Request.Params["LoadingExpensesBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new LoadingExpensesBill());//返回一个新建的空对象
            }
            //如果有数据
            Guid LoadingExpensesBillId = new Guid(Request["LoadingExpensesBillId"]);//单据编号
            LoadingExpensesBill bill = LoadingExpensesBillService.LoadEntities(t => t.Id == LoadingExpensesBillId && t.BillType == billtype).FirstOrDefault();//获取表单
            return Json(bill);
        }
        public ActionResult GetBillDetail(LoadingAndLaborQueryView A, List<LaborAndLoading3QueryConditions> Businesstype, List<LaborAndLoading3QueryConditions> LodingType , List<LaborAndLoading3QueryConditions>  Warehouseid,int billType) {

            if (billType == 0)//是否有单据类型
            {
                return Content("没有单据类型！");
            }
            LoadingExpensesBill bill = LoadingExpensesBillService.GetData(A, billType, Session["Power"].ToString().Trim(), Businesstype, LodingType, Warehouseid);
            return Json(bill);
        }
        //保存表单数据
        public ActionResult SaveData(LoadingExpensesBill LoadingExpensesBill,int billType)
        {
            if (billType == 0)//是否有单据类型
            {
                return Content("没有单据类型！");
            }
            //参数对象可以对应接受数据
            LoadingExpensesBill.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = LoadingExpensesBillService.SaveData(LoadingExpensesBill, billType);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="billCode">单号</param>
        /// <returns></returns>
        public ActionResult DeleteBill(Guid BillId)
        {
            return Content(LoadingExpensesBillService.DeleteBill(BillId));
        }

        /// <summary>
        /// 初审表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine1()
        {
            string res = "";
            string LoadingExpensesBillId = Request.Params["LoadingExpensesBillId"];
            if (string.IsNullOrEmpty(LoadingExpensesBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = LoadingExpensesBillService.Examine1(Guid.Parse(LoadingExpensesBillId), Session["UserName"].ToString());
            }
            return Content(res);
        }
        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine2()
        {
            string res = "";
            string LoadingExpensesBillId = Request.Params["LoadingExpensesBillId"];
            if (string.IsNullOrEmpty(LoadingExpensesBillId))
            {
                res = "参数错误";
            }
            else
            {
                res = LoadingExpensesBillService.Examine2(Guid.Parse(LoadingExpensesBillId), Session["UserName"].ToString());
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
                res = LoadingExpensesBillService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}