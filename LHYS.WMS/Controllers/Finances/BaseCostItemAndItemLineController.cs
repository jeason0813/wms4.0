using BLL;
using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LHYS.WMS.Controllers
{
    public class BaseCostItemAndItemLineController:Controller
    {
        private ICostItemService CostItemService { get; set; }
        private IItemLineService ItemLineService { get; set; }
        public ActionResult GetBaseData()
        {
            //测试用Session
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string UserCode = Session["UserCode"].ToString();//员工编号
                string Power = Session["Power"].ToString();//权限字符串
                var list = CostItemService.LoadEntities(a => Power.Contains(a.DepartmentId));//仓库 货位
                var list1 = ItemLineService.LoadEntities(a => Power.Contains(a.DepartmentId));//物料档案
                var res = new
                {
                    CostItem = list.Select(a => new { a.Id, a.Name,a.Code }),
                    ItemLine = list1.Select(a => new { a.Id, a.Name })
                };
                return Json(res);
            }


        }
    }
}