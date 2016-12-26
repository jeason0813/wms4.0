using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
using IBLL;
namespace BLL
{
    public partial class LocationChangeService : BaseService<LocationChange>, ILocationChangeService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(LocationChange bill, List<LocationChangeRecordView> LocationChangeRecordView)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillCode = GetBillCode("HW");//生成单号
                if (bill.BillCode == "no")
                {
                    bill.BillCode = GetBillCode("HW");//再次生成单号
                }
                bill.BillState = 1;//保存状态
                if (LocationChangeRecordView!=null) {
                    foreach (var lcrv in LocationChangeRecordView)
                    {
                        Record cd = new Record();
                        cd.Id = Guid.NewGuid();
                        cd.MainTableId = bill.Id;
                        cd.Count = lcrv.Count;
                        cd.CreateDate = DateTime.Now;
                        cd.Department = bill.Department;
                        cd.DepartmentId = bill.DepartmentId;
                        cd.State = 1;
                        cd.ItemBatch = lcrv.ItemBatch;
                        cd.ItemCode = lcrv.ItemCode;
                        cd.ItemLine = lcrv.ItemLine;
                        cd.ItemName = lcrv.ItemName;
                        cd.ItemSpecifications = lcrv.ItemSpecifications;
                        cd.ItemUnit = lcrv.ItemUnit;
                        cd.UnitWeight = lcrv.UnitWeight;
                        cd.Weight = lcrv.Weight;
                        cd.ItemLocation = lcrv.LocationChangeAfter;
                        cd.ItemLocationId = lcrv.LocationChangeAfterId;
                        cd.InOrOut = 1;
                        cd.MainTableType = "LocationChange";
                        cd.Warehouse = lcrv.WarehouseAfter;
                        cd.WarehouseId = lcrv.WarehouseAfterId;
                        cd.InOutTypeId = bill.InputTypeId;
                        cd.InOutTypeName = bill.InputType;
                        CurrentDBSession.RecordDal.AddEntity(cd);
                        Record cd2 = new Record();
                        cd2.Id = Guid.NewGuid();
                        cd2.MainTableId = bill.Id;
                        cd2.Count = lcrv.Count;
                        cd2.CreateDate = DateTime.Now;
                        cd2.Department = bill.Department;
                        cd2.DepartmentId = bill.DepartmentId;
                        cd2.State = 1;
                        cd2.ItemBatch = lcrv.ItemBatch;
                        cd2.ItemCode = lcrv.ItemCode;
                        cd2.ItemLine = lcrv.ItemLine;
                        cd2.ItemName = lcrv.ItemName;
                        cd2.ItemSpecifications = lcrv.ItemSpecifications;
                        cd2.ItemUnit = lcrv.ItemUnit;
                        cd2.UnitWeight = lcrv.UnitWeight;
                        cd2.Weight = lcrv.Weight;
                        cd2.ItemLocation = lcrv.LocationChangeBefore;
                        cd2.ItemLocationId = lcrv.LocationChangeBeforeId;
                        cd2.InOrOut = 0;
                        cd2.MainTableType = "LocationChange";
                        cd2.Warehouse = lcrv.WarehouseBefore;
                        cd2.WarehouseId = lcrv.WarehouseBeforeId;
                        cd2.InOutTypeId = bill.OutputTypeId;
                        cd2.InOutTypeName = bill.OutputType;
                        CurrentDBSession.RecordDal.AddEntity(cd2);
                    }
                }
                CurrentDal.AddEntity(bill);
            }
            //主表有id  说明是修改的
            else
            {
                //删除原子表数据
                List<Record> list = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == bill.Id).ToList();
                foreach (Record item in list)
                {
                    CurrentDBSession.RecordDal.DeleteEntity(item);
                    //RecordService.DeleteEntity(item);
                }
                //添加子表数据
                if (LocationChangeRecordView!=null) {
                    foreach (var lcrv in LocationChangeRecordView)
                    {
                        Record cd = new Record();
                        cd.Id = Guid.NewGuid();
                        cd.MainTableId = bill.Id;
                        cd.Count = lcrv.Count;
                        cd.CreateDate = DateTime.Now;
                        cd.Department = bill.Department;
                        cd.DepartmentId = bill.DepartmentId;
                        cd.State = 1;
                        cd.ItemBatch = lcrv.ItemBatch;
                        cd.ItemCode = lcrv.ItemCode;
                        cd.ItemLine = lcrv.ItemLine;
                        cd.ItemName = lcrv.ItemName;
                        cd.ItemSpecifications = lcrv.ItemSpecifications;
                        cd.ItemUnit = lcrv.ItemUnit;
                        cd.UnitWeight = lcrv.UnitWeight;
                        cd.Weight = lcrv.Weight;
                        cd.ItemLocation = lcrv.LocationChangeAfter;
                        cd.ItemLocationId = lcrv.LocationChangeAfterId;
                        cd.InOrOut = 1;
                        cd.Warehouse = lcrv.WarehouseAfter;
                        cd.WarehouseId = lcrv.WarehouseAfterId;
                        cd.InOutTypeId = bill.InputTypeId;
                        cd.InOutTypeName = bill.InputType;
                        CurrentDBSession.RecordDal.AddEntity(cd);
                        Record cd2 = new Record();
                        cd2.Id = Guid.NewGuid();
                        cd2.MainTableId = bill.Id;
                        cd2.Count = lcrv.Count;
                        cd2.CreateDate = DateTime.Now;
                        cd2.Department = bill.Department;
                        cd2.DepartmentId = bill.DepartmentId;
                        cd2.State = 1;
                        cd2.ItemBatch = lcrv.ItemBatch;
                        cd2.ItemCode = lcrv.ItemCode;
                        cd2.ItemLine = lcrv.ItemLine;
                        cd2.ItemName = lcrv.ItemName;
                        cd2.ItemSpecifications = lcrv.ItemSpecifications;
                        cd2.ItemUnit = lcrv.ItemUnit;
                        cd2.UnitWeight = lcrv.UnitWeight;
                        cd2.Weight = lcrv.Weight;
                        cd2.ItemLocation = lcrv.LocationChangeBefore;
                        cd2.ItemLocationId = lcrv.LocationChangeBeforeId;
                        cd2.InOrOut = 0;
                        cd2.Warehouse = lcrv.WarehouseBefore;
                        cd2.WarehouseId = lcrv.WarehouseBeforeId;
                        cd2.InOutTypeId = bill.OutputTypeId;
                        cd2.InOutTypeName = bill.OutputType;
                        CurrentDBSession.RecordDal.AddEntity(cd2);
                    }
                }
                    //修改主表数据
                    CurrentDal.EditEntity(bill);
                }
                return CurrentDBSession.SaveChanges() ? bill.Id.ToString() : "no";

            }
        /// <summary>
        /// 审核表单
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <returns></returns>
        public string Examine(Guid LocationChangeId, string UserName)
        {
            string result = "";
            LocationChange bill = CurrentDal.LoadEntities(a => a.Id == LocationChangeId).FirstOrDefault();
            List<Record> list = CurrentDBSession.RecordDal.LoadEntities(b=>b.MainTableId==bill.Id).ToList();
            foreach (Record item in list)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (item.InOrOut == 1)
                { //等于1入库
                    if (inWarehouse == null)//如果没有库存  新建一条记录
                    {
                        InWarehouse newInWarehouse = new InWarehouse();
                        newInWarehouse.Id = Guid.NewGuid();
                        newInWarehouse.ItemCode = item.ItemCode;
                        newInWarehouse.ItemLine = item.ItemLine;
                        newInWarehouse.ItemName = item.ItemName;
                        newInWarehouse.ItemSpecifications = item.ItemSpecifications;
                        newInWarehouse.ItemLocation = item.ItemLocation;
                        newInWarehouse.ItemLocationId = item.ItemLocationId;
                        newInWarehouse.Warehouse = item.Warehouse;
                        newInWarehouse.WarehouseId = item.WarehouseId;
                        newInWarehouse.ItemBatch = item.ItemBatch;
                        newInWarehouse.ItemUnit = item.ItemUnit;
                        newInWarehouse.Count = item.Count;
                        newInWarehouse.Department = bill.Department;
                        newInWarehouse.DepartmentId = bill.DepartmentId;
                        newInWarehouse.Company = bill.Company;
                        newInWarehouse.CompanyId = bill.CompanyId;
                        CurrentDBSession.InWarehouseDal.AddEntity(newInWarehouse);
                        item.CurrentCount = item.Count;
                    }
                    else //有库存
                    {
                        inWarehouse.Count += item.Count;
                        CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                        item.CurrentCount = inWarehouse.Count;
                    }
                }
                else
                {  //出库
                    if (inWarehouse == null || inWarehouse.Count < item.Count)
                    {
                        result += "物料编号：" + item.ItemCode + "物料名称：" + item.ItemName + "库存不足，审核失败";
                       
                    }
                    else if (inWarehouse.Count == item.Count)
                    {
                        CurrentDBSession.InWarehouseDal.DeleteEntity(inWarehouse);
                        item.CurrentCount = 0;
                    }
                    else //有库存
                    {
                        inWarehouse.Count -= item.Count;
                        CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                        item.CurrentCount = inWarehouse.Count;
                    }
                }
            }
            if (result != "")
            {
                return result;
            }
            bill.BillState = 2;//改成已审核状态
            bill.ExaminePerson = UserName;//审核人
            bill.ExamineDate = DateTime.Now;
            CurrentDal.EditEntity(bill);
            result = CurrentDBSession.SaveChanges() ? "审核成功！" : "审核失败！";
            return result;
        }
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId">单据Id</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public string GiveupExamine(Guid LocationChangeId, string UserName)
        {
            LocationChange bill = CurrentDal.LoadEntities(a => a.Id == LocationChangeId).FirstOrDefault();
            List<Record> list = CurrentDBSession.RecordDal.LoadEntities(b => b.MainTableId == bill.Id).ToList();
            string res = "";
            if (bill.BillState != 2)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            //对每一条记录做逆操作
            foreach (Record item in list)
            {
                item.State = 1;//修改记录状态
                item.ExamineDate = null;//清空审核时间
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (item.InOrOut == 0)
                {
                    if (inWarehouse == null)//如果没有库存  新建一条记录
                    {
                        InWarehouse newInWarehouse = new InWarehouse();
                        newInWarehouse.Id = Guid.NewGuid();
                        newInWarehouse.ItemCode = item.ItemCode;
                        newInWarehouse.ItemLine = item.ItemLine;
                        newInWarehouse.ItemName = item.ItemName;
                        newInWarehouse.ItemSpecifications = item.ItemSpecifications;
                        newInWarehouse.ItemLocation = item.ItemLocation;
                        newInWarehouse.ItemLocationId = item.ItemLocationId;
                        newInWarehouse.Warehouse = item.Warehouse;
                        newInWarehouse.WarehouseId = item.WarehouseId;
                        newInWarehouse.ItemBatch = item.ItemBatch;
                        newInWarehouse.ItemUnit = item.ItemUnit;
                        newInWarehouse.Count = item.Count;
                        newInWarehouse.Department = bill.Department;
                        newInWarehouse.DepartmentId = bill.DepartmentId;
                        newInWarehouse.Company = bill.Company;
                        newInWarehouse.CompanyId = bill.CompanyId;
                        CurrentDBSession.InWarehouseDal.AddEntity(newInWarehouse);
                        item.CurrentCount = item.Count;
                    }
                    else //有库存
                    {
                        inWarehouse.Count += item.Count;
                        CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    }
                }
                else
                {
                    if (inWarehouse == null || inWarehouse.Count < item.Count) //当前库存不够 ，不允许做弃审操作
                    {
                         res += "物料编号：" + item.ItemCode + "物料名称：" + item.ItemName + "库存不足，弃审失败";
                    }
                    else if (inWarehouse.Count == item.Count)
                    {
                        CurrentDBSession.InWarehouseDal.DeleteEntity(inWarehouse);
                        item.CurrentCount = 0;
                    }
                    else //有库存
                    {
                        inWarehouse.Count -= item.Count;
                        CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    }
                }
            }
            if (res != "")
            {
                return res;
            }
            bill.BillState = 1;//改成编辑状态
            bill.ExaminePerson = null;//清除审核人
            bill.ExamineDate = null;//清除审核时间
            CurrentDal.EditEntity(bill);
            return CurrentDBSession.SaveChanges() ? "操作成功" : "操作失败";
        }
        /// <summary>
        /// 生成单号
        /// </summary>
        /// <returns></returns>
        public string GetBillCode(string head)
        {
            string res = "";
            try
            {
                string temp = DateTime.Now.ToString("yyyyMM");//六位年月
                int totalCount;//没用
                string flowid;
                var lastbill = CurrentDal.LoadPageEntities(a => a.BillCode.Substring(2, 6) == temp, a => a.BillCode, 1, 1, false, out totalCount).FirstOrDefault();
                if (lastbill == null)
                {
                    flowid = "00001";
                }
                else
                {
                    flowid = string.Format("{0:00000}", (Convert.ToInt32(lastbill.BillCode.Substring(8, 5)) + 1));
                }
                res = head + temp + flowid;
            }
            catch
            {
                res = "no";
            }
            return res;
        }
    }
}
