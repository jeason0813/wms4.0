using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class TaskBillController : Controller
    {
        private ITaskBillService TaskBillService { get; set; }
        // GET: TaskBill
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["TaskBillId"] != null)
            {
                ViewBag.TaskBillId = Request["TaskBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.TaskBillId = "";
            }
            return View();
        }

        /// <summary>
        /// 任务单查看
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexShow()
        {
            if (Request["TaskBillId"] != null)
            {
                ViewBag.TaskBillId = Request["TaskBillId"].ToString();
            }
            else //没传参数 放入空
            {
                return Content("参数错误");
            }
            return View();


        }

        public ActionResult GetData()
        {
            string str = Request.Params["TaskBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return Json(new TaskBill());//返回一个新建的空对象
            }
            //如果有数据
            Guid TaskBillId = new Guid(Request["TaskBillId"]);//单据编号
            TaskBill bill = TaskBillService.LoadEntities(t => t.Id == TaskBillId).FirstOrDefault();//获取表单
            return Json(bill);
        }


        //保存表单数据
        public ActionResult SaveData(TaskBill TaskBill, string[] records)
        {
            //参数对象可以对应接受数据
            TaskBill.MakePerson = Session["UserName"].ToString();//保存制单人
            TaskBill.CreateDate = DateTime.Now;//保存时间
            string result = TaskBillService.SaveData(TaskBill, records);//保存数据
            //保存交货单数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string TaskBillId = Request.Params["TaskBillId"];
            if (string.IsNullOrEmpty(TaskBillId))
            {
                res = "参数错误";
            }
            else
            {
                Guid id = Guid.Parse(TaskBillId);
                TaskBill bill = TaskBillService.LoadEntities(a => a.Id == id).FirstOrDefault();
                if (bill == null) {
                    return Content("找不到单号为"+ TaskBillId + "的记录！");
                }
                if (Session["UserName"] == null) {
                    return Content("您没有登录！");
                }
                bill.BillState = 2;
                bill.ExaminePerson = Session["UserName"].ToString();
                bill.ExamineDate = DateTime.Now;
               res= TaskBillService.EditEntity(bill)?"审核成功":"审核失败";
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
                Guid id = Guid.Parse(billCode);
                TaskBill bill = TaskBillService.LoadEntities(a => a.Id == id).FirstOrDefault();
                if (bill == null)
                {
                    return Content("找不到单号为" + billCode + "的记录！");
                }
                bill.BillState = 1;
                bill.ExaminePerson = null;
                bill.ExamineDate = null;
                res = TaskBillService.EditEntity(bill) ? "操作成功" : "操作失败";
            }
            return Content(res);
        }


    }
}