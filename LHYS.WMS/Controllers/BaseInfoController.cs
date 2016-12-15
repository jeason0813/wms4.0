using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class BaseInfoController : Controller
    {
        private IWarehouseService WarehouseService { get; set; }
        private IGoodItemService GoodItemService { get; set; }
        private IUserFrameworkService UserFrameworkService { get; set; }
        private ILoadGoodsTypeService LoadGoodsTypeService { get; set; }
        private IBusinessTypeService BusinessTypeService { get; set; }
        private IInOutTypeService InOutTypeService { get; set; }
        // GET: BaseInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBaseInfo(string inoutType)
        {
            //测试用Session
            Session["UserName"] = "朱洲";
            Session["UserCode"] = "HR1406";
            Session["Power"] = "LH6220";
            if (Session["Power"] == null || Session["UserCode"] == null)
            {
                return null;
            }
            else
            {
                string UserCode = Session["UserCode"].ToString();//员工编号
                string Power = Session["Power"].ToString();//权限字符串
                var list = WarehouseService.LoadEntities(a => Power.Contains(a.DepartmentId));//仓库 货位
                var list1 = GoodItemService.LoadEntities(a => Power.Contains(a.DepartmentId));//物料档案
                var temp = UserFrameworkService.LoadEntities(u => Power.Contains(u.DeptCode));
                var list2 = (from a in temp
                             select new
                             {
                                 a.UserCode,
                                 a.UserName
                             }).Distinct();  //人员信息
                var list3 = LoadGoodsTypeService.LoadEntities(a => Power.Contains(a.DepartmentId));//装卸类型
                var list4 = BusinessTypeService.LoadEntities(a => Power.Contains(a.DepartmentId));//业务类型
                //出入库类别
                IQueryable<DBModel.InOutType> list5;
                if (inoutType == "in")
                {
                     list5 = InOutTypeService.LoadEntities(a => Power.Contains(a.DepartmentId) && a.InoutType == "入库");

                }
                else if (inoutType == "out")
                {
                     list5 = InOutTypeService.LoadEntities(a => Power.Contains(a.DepartmentId) && a.InoutType == "出库");
                }
                else
                {
                     list5 = InOutTypeService.LoadEntities(a => Power.Contains(a.DepartmentId));
                }
                var list6 = UserFrameworkService.LoadEntities(u => u.UserCode == UserCode);//公司部门信息
                var res = new
                {
                    Warehouse = list.Select(a => new { a.Id, a.Name, a.Department, a.Location }),
                    GoodItem = list1.Select(a => new { a.ItemCode, a.ItemLine, a.ItemName, a.ItemSpecifications, a.ItemUnit, a.UnitWeight }),
                    UserFramework = list2.Select(a => new { a.UserCode, a.UserName }),
                    LoadGoodsType = list3.Select(a => a.Name),
                    BusinessType = list4.Select(a => a.Name),
                    InOutType = list5.Select(a => new { a.Id, a.Name }),
                    Departent = list6.Select(a => new { a.DeptCode, a.DeptName, a.CmopanyCode, a.CompanyAbbr })
                };
                return Json(res);
            }


        }
    }
}