using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
using System.IO;

namespace LHYS.WMS.Controllers
{
    public class ExportBillController : Controller
    {

        private IBillUpLoadService BillUpLoadService { get; set; }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="billType">单据类型</param>
        /// <param name="billId">单据ID</param>
        /// <returns>生成文件路径</returns>
        public ActionResult ExprotExcel(/*string billType, Guid billId*/)
        {
            string billType = Request["billType"];
            Guid billId = Guid.Parse(Request["billId"]);
            //模板路径
            string url;
            switch (billType)
            {
                case "transferBill":
                    url = Request.MapPath("/Template/入库单模板.xls");
                    break;
                case "backOutput":
                    url = Request.MapPath("/Template/退货单模板.xls");

                    break;
                case "backInput":
                    url = Request.MapPath("/Template/退仓单模板.xls");
                    break;
                case "giveBill":
                    url = Request.MapPath("/Template/交货单模板.xls");
                    break;
                default: return null;
            }
            MemoryStream file = BillUpLoadService.ExprotExcel(billType, billId, url);

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet";// "application/ms-excel/msword";
            Response.AddHeader("Content-Disposition", "attachment;fileName="+billType+".xls");
            Response.BinaryWrite(file.ToArray());
            return new EmptyResult();
        }
    }
}