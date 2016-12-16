using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DBModel;
namespace LHYS.WMS.Controllers
{
    public class LocationChangeController : Controller
    {
        // GET: LocationChange
        private ILocationChangeService LocationChangeService { get; set; }
        private IRecordService RecordService { get; set; }
        public ActionResult Index()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["LocationChangeId"] != null)
            {
                ViewBag.LocationChangeId = Request["LocationChangeId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.LocationChangeId = "";
            }
            return View();
        }
        public ActionResult IndexShow()
        {
            //如果传入单号参数  放入ViewBag
            if (Request["LocationChangeId"] != null)
            {
                ViewBag.LocationChangeId = Request["LocationChangeId"].ToString();
            }
            else //没传参数 放入空
            {
                ViewBag.LocationChangeId = "";
            }
            return View();
        }
        //获取表单数据
        public ActionResult GetData()
        {
            string str = Request.Params["LocationChangeId"];//单号
            //如果新单据 没有数据
            if (string.IsNullOrEmpty(str))
            {
                LocationChange locationlist = new LocationChange();//获取表单
                List<Record> recordlist = new List<Record>();
                var res = new
                {
                    LocationChanges = locationlist,
                    Records = recordlist
                };
                return Json(res);//返回一个新建的空对象
            }
            else {
                //如果有数据
                Guid LocationChangeId = new Guid(Request["LocationChangeId"]);//单据编号
                LocationChange locationlist = LocationChangeService.LoadEntities(t => t.Id == LocationChangeId).FirstOrDefault();//获取表单
                List<Record> recordlist = RecordService.LoadEntities(a => a.MainTableId == LocationChangeId).ToList();
                List<Record> recordlist2 = (from rec in recordlist where rec.InOrOut == 1 select rec).ToList();
                List<Record> recordlist3 = (from rec in recordlist where rec.InOrOut == 0 select rec).ToList();
                List < LocationChangeRecordView > locationviewlist = new List<LocationChangeRecordView>();
                for (int i = 0; i < recordlist3.Count; i++)//把recordlist中的没两条合成locationviewlist中的一条放回前台
                {
                    for (int j = 0; j < recordlist2.Count; j++)
                    {
                        if (recordlist3[i].ItemCode == recordlist2[j].ItemCode && recordlist3[i].Count == recordlist2[j].Count && recordlist3[i].ItemBatch == recordlist2[j].ItemBatch && recordlist3[i].Weight == recordlist2[j].Weight)
                        {
                            LocationChangeRecordView _view = new LocationChangeRecordView();
                            var temp = recordlist3[i];
                            _view.Id = temp.Id;
                            _view.Count = temp.Count;
                            _view.CreateDate = temp.CreateDate;
                            _view.Department = temp.Department;
                            _view.DepartmentId = temp.DepartmentId;
                            _view.ExamineDate = temp.ExamineDate;
                            _view.ItemBatch = temp.ItemBatch;
                            _view.ItemCode = temp.ItemCode;
                            _view.ItemLine = temp.ItemLine;
                            _view.ItemName = temp.ItemName;
                            _view.ItemSpecifications = temp.ItemSpecifications;
                            _view.ItemUnit = temp.ItemUnit;
                            _view.MainTableId = temp.MainTableId;
                            _view.Remark = temp.Remark;
                            _view.State = temp.State;
                            _view.UnitWeight = temp.UnitWeight;
                            _view.Weight = temp.Weight;
                            _view.LocationChangeAfter = recordlist2[j].ItemLocation;
                            _view.LocationChangeAfterId = recordlist2[j].ItemLocationId;
                            _view.WarehouseAfter = recordlist2[j].Warehouse;
                            _view.WarehouseAfterId = recordlist2[j].WarehouseId;
                            _view.LocationChangeBefore = temp.ItemLocation;
                            _view.LocationChangeBeforeId = temp.ItemLocationId;
                            _view.WarehouseBefore = temp.Warehouse;
                            _view.WarehouseBeforeId = temp.WarehouseId;
                            locationviewlist.Add(_view);
                            recordlist2.RemoveAt(j);
                            break;
                        }
                    }
                }
                var res = new
                {
                    LocationChanges = locationlist,
                    Records = locationviewlist
                };
                return Json(res);
            }
            
        }
        //保存表单数据
        public ActionResult SaveData(LocationChange LocationChange123, List<LocationChangeRecordView> LocationChangeRecordView)
        {
            //参数对象可以对应接受数据
            LocationChange123.MakePerson = Session["UserName"].ToString();//保存制单人
            string result = LocationChangeService.SaveData(LocationChange123, LocationChangeRecordView);//保存数据
            return Content(result.ToString());
        }

        /// <summary>
        /// 审核表单
        /// </summary>
        /// <returns></returns>
        public ActionResult Examine()
        {
            string res = "";
            string LocationChangeId = Request.Params["LocationChangeId"];
            if (string.IsNullOrEmpty(LocationChangeId))
            {
                res = "参数错误";
            }
            else
            {
                res = LocationChangeService.Examine(Guid.Parse(LocationChangeId), Session["UserName"].ToString());
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
                res = LocationChangeService.GiveupExamine(Guid.Parse(billCode), Session["UserName"].ToString());
            }
            return Content(res);

        }
    }
}