﻿using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class InWarehouseController : Controller
    {
        private IInWarehouseService InWarehouseService { get; set; }
        // GET: InWarehouse
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据仓库查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataByWarehouseId(int WarehouseId)
        {
            var list = InWarehouseService.LoadEntities(a => a.WarehouseId == WarehouseId);
            //查询库存中是否有重复项
            var listBad = from a in list
                          group a by new { a.ItemCode, a.ItemBatch, a.ItemLocationId } into g
                          where g.Count()>1
                          select new { g.Key.ItemBatch, g.Key.ItemCode, g.Key.ItemLocationId, TotalCount = g.Count() } ;
            if (listBad.Count() > 0) {
                string res = "库存中有重复项：";
                foreach (var item in listBad)
                {
                    res += item.ItemCode + '-' + item.ItemBatch + '-' + item.ItemLocationId+',';
                }
                return Content(res);
            }
            return Json(list);
        }
        /// <summary>
        /// 查询库存的某物料比指定数量多的
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public ActionResult GetInWarehouseByItemCodeAndCount(string ItemCode, int Count)
        {
            if (Session["Power"] == null) { return null; }
            string Power = Session["Power"].ToString();
            var list = InWarehouseService.LoadEntities(a => a.ItemCode == ItemCode && a.Count >= Count && Power.Contains(a.DepartmentId));
            return Json(list);
        }
    }
}