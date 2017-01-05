using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class GiveBackBillController : Controller
    {
        private IGiveBillService GiveBillService { get; set; }
        private IRecordService RecordService { get; set; }
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
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["GiveBackBillId"] != null)
            {
                ViewBag.GiveBackBillId = Request["GiveBackBillId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.GiveBackBillId = "";
            }
            return View();
        }
        //获取表单数据[根据id]
        public ActionResult GetData()
        {
            string str = Request.Params["GiveBillId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            //如果有数据
            Guid GiveBillId = new Guid(Request["GiveBillId"]);//单据编号
            GiveBill bill = GiveBillService.LoadEntities(t => t.Id == GiveBillId).FirstOrDefault();//获取表单
            return Json(bill);
        }
        //获取表单数据[根据单号]
        public ActionResult GetDataBaseBillCode(string billCode)
        {
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(billCode))
            {
                return null;
            }
            //如果有数据
            var list  = GiveBillService.LoadEntities(t => t.BillCode == billCode);//获取表单
            if (list.Count() == 0) { return null; }
            return Json(list.FirstOrDefault());
        }

        //保存子表单数据
        public ActionResult SaveData(List<Record> record)
        {
            bool res = true;
            if (record == null) { res = false; }
            foreach (var item in record)
            {
               res= RecordService.EditEntity(item);
            }
            return Content(res?record[0].MainTableId.ToString():"保存失败！");
        }
        
    }
}