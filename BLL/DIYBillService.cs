using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
using IBLL;
namespace BLL
{
    public partial class DIYBillService : BaseService<DIYBill>, IDIYBillService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(DIYBill bill,List<Record> recordlist1,List<Record> recordlist2)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillCode = GetBillCode("JG");//生成单号
                if (bill.BillCode == "no")
                {
                    bill.BillCode = GetBillCode("JG");//再次生成单号
                }
                bill.BillState = 1;//保存状态
                if (recordlist1 != null&& recordlist1!=null)
                {
                    foreach (Record record1 in recordlist1)
                    {
                        record1.Id = Guid.NewGuid();
                        record1.MainTableId = bill.Id;
                        record1.CreateDate = DateTime.Now;
                        record1.State = 1;
                        record1.Department = bill.Department;
                        record1.DepartmentId = bill.DepartmentId;
                        record1.InOrOut = 0;
                        record1.MainTableType = "DIYBill";
                        record1.InOutTypeId = bill.OutputTypeId;
                        record1.InOutTypeName = bill.OutputType;
                        CurrentDBSession.RecordDal.AddEntity(record1);
                    }
                    foreach (Record record2 in recordlist2)
                    {
                        record2.Id = Guid.NewGuid();
                        record2.MainTableId = bill.Id;
                        record2.CreateDate = DateTime.Now;
                        record2.State = 1;
                        record2.Department = bill.Department;
                        record2.DepartmentId = bill.DepartmentId;
                        record2.InOrOut = 1;
                        record2.MainTableType = "DIYBill";
                        record2.InOutTypeName = bill.InputType;
                        record2.InOutTypeId = bill.InputTypeId;
                        CurrentDBSession.RecordDal.AddEntity(record2);
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
                }
                //添加子表数据
                if (recordlist1 != null && recordlist1 != null)
                {
                    foreach (Record record1 in recordlist1)
                    {
                        record1.Id = Guid.NewGuid();
                        record1.MainTableId = bill.Id;
                        record1.CreateDate = DateTime.Now;
                        record1.State = 1;
                        record1.Department = bill.Department;
                        record1.DepartmentId = bill.DepartmentId;
                        record1.InOrOut = 0;
                        record1.MainTableType = "DIYBill";
                        record1.InOutTypeId = bill.OutputTypeId;
                        record1.InOutTypeName = bill.OutputType;
                        CurrentDBSession.RecordDal.AddEntity(record1);
                        CurrentDBSession.RecordDal.AddEntity(record1);
                    }
                    foreach (Record record2 in recordlist2)
                    {
                        record2.Id = Guid.NewGuid();
                        record2.MainTableId = bill.Id;
                        record2.CreateDate = DateTime.Now;
                        record2.State = 1;
                        record2.Department = bill.Department;
                        record2.DepartmentId = bill.DepartmentId;
                        record2.InOrOut = 1;
                        record2.MainTableType = "DIYBill";
                        record2.InOutTypeName = bill.InputType;
                        record2.InOutTypeId = bill.InputTypeId;
                        CurrentDBSession.RecordDal.AddEntity(record2);
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
        /// <param name="DIYBillId"></param>
        /// <returns></returns>
        public string Examine(Guid DIYBillId, string UserName)
        {
            string result = "";
            DIYBill bill = CurrentDal.LoadEntities(a => a.Id == DIYBillId).FirstOrDefault();
            List<Record> list1 = CurrentDBSession.RecordDal.LoadEntities(b=>b.MainTableId==bill.Id&&b.InOrOut==1).ToList();
            List<Record> list0= CurrentDBSession.RecordDal.LoadEntities(b => b.MainTableId == bill.Id && b.InOrOut == 0).ToList();
            foreach (Record item in list1)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null)//如果没有库存  新建一条记录
                {
                    inWarehouse = new InWarehouse() {
                        Id = Guid.NewGuid(),
                        ItemBatch = item.ItemBatch,
                        ItemCode = item.ItemCode,
                        ItemLine = item.ItemLine,
                        ItemLocation = item.ItemLocation,
                        ItemName = item.ItemName,
                        ItemSpecifications = item.ItemSpecifications,
                        ItemUnit = item.ItemUnit,
                        Count = item.Count,
                        Company = bill.Company,
                        CompanyId = bill.CompanyId,
                        Department = bill.Department,
                        DepartmentId=bill.DepartmentId,
                        Warehouse=item.Warehouse,
                        WarehouseId=item.WarehouseId,
                        ItemLocationId=item.ItemLocationId
                    };
                    CurrentDBSession.InWarehouseDal.AddEntity(inWarehouse);
                    item.CurrentCount = item.Count;
                }
                else//库存大于记录  相减
                {
                    inWarehouse.Count = inWarehouse.Count +item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
            }
            foreach (Record item in list0)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null || inWarehouse.Count < item.Count)//如果没有库存  新建一条记录
                {
                    result += "物料编号：" + item.ItemCode + "物料名称：" + item.ItemName + "库存不足，审核失败";
                }
                else if (inWarehouse.Count == item.Count) // 库存正好相等  删除这条记录
                {
                    CurrentDBSession.InWarehouseDal.DeleteEntity(inWarehouse);
                }
                else if (inWarehouse.Count > item.Count)//库存大于记录  相减
                {
                    inWarehouse.Count = inWarehouse.Count - item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
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
        public string GiveupExamine(Guid DIYBillId, string UserName)
        {
            DIYBill bill = CurrentDal.LoadEntities(a => a.Id == DIYBillId).FirstOrDefault();
            List<Record> list1 = CurrentDBSession.RecordDal.LoadEntities(b => b.MainTableId == bill.Id && b.InOrOut == 1).ToList();
            List<Record> list0 = CurrentDBSession.RecordDal.LoadEntities(b => b.MainTableId == bill.Id && b.InOrOut == 0).ToList();
            string result = "";
            if (bill.BillState != 2)
            {
                result = "单据状态不对，无法弃审！";
                return result;
            }
            //对每一条记录做逆操作
            foreach (Record item in list0)
            {
                item.State = 1;
                item.ExamineDate = null;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null)//如果没有库存  新建一条记录
                {
                    inWarehouse = new InWarehouse()
                    {
                        Id = Guid.NewGuid(),
                        ItemBatch = item.ItemBatch,
                        ItemCode = item.ItemCode,
                        ItemLine = item.ItemLine,
                        ItemLocation = item.ItemLocation,
                        ItemName = item.ItemName,
                        ItemSpecifications = item.ItemSpecifications,
                        ItemUnit = item.ItemUnit,
                        Count = item.Count,
                        Company = bill.Company,
                        CompanyId = bill.CompanyId,
                        Department = bill.Department,
                        DepartmentId = bill.DepartmentId,
                        Warehouse = item.Warehouse,
                        WarehouseId = item.WarehouseId,
                        ItemLocationId = item.ItemLocationId
                    };
                    CurrentDBSession.InWarehouseDal.AddEntity(inWarehouse);
                    item.CurrentCount = item.Count;
                }
                else//库存大于记录  相减
                {
                    inWarehouse.Count = inWarehouse.Count + item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
            }
            foreach (Record item in list1)
            {
                item.State = 1;
                item.ExamineDate = null;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null || inWarehouse.Count < item.Count)//如果没有库存  新建一条记录
                {
                    result += "物料编号：" + item.ItemCode + "物料名称：" + item.ItemName + "库存不足，弃审失败";
                   
                }
                else if (inWarehouse.Count == item.Count) // 库存正好相等  删除这条记录
                {
                    CurrentDBSession.InWarehouseDal.DeleteEntity(inWarehouse);
                }
                else if (inWarehouse.Count > item.Count)//库存大于记录  相减
                {
                    inWarehouse.Count = inWarehouse.Count - item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
            }
            if (result != "")
            {
                return result;
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
