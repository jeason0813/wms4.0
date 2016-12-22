using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TransferBillService : BaseService<TransferBill>, ITransferBillService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(TransferBill bill)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillCode = GetBillCode("ZC");//生成单号
                if (bill.BillCode == "no")
                {
                    bill.BillCode = GetBillCode("ZC");//再次生成单号
                }
                bill.BillState = 1;//保存状态
                foreach (var item in bill.Record)//生成子表id
                {
                    if (item.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.Id = Guid.NewGuid();
                    }
                    item.CreateDate = DateTime.Now;
                    item.State = 1;
                    item.Warehouse = bill.Warehouse;
                    item.WarehouseId = bill.WarehouseId;
                    item.Department = bill.Department;
                    item.DepartmentId = bill.DepartmentId;
                    item.InOrOut = 1;
                    item.InOutTypeId = 14;
                    item.MainTableType = "TransferBill";
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
                foreach (Record record in bill.Record)
                {
                    if (record.Id == Guid.Parse("00000000-0000-0000-0000-000000000000") || record.Id == Guid.Empty)
                    {
                        record.Id = Guid.NewGuid();//设置子表id
                    }
                    record.MainTableId = bill.Id;//设置主表id
                    record.CreateDate = DateTime.Now;
                    record.State = 1;
                    record.Warehouse = bill.Warehouse;
                    record.WarehouseId = bill.WarehouseId;
                    record.Department = bill.Department;
                    record.DepartmentId = bill.DepartmentId;
                    record.InOrOut = 1;
                    record.MainTableType = "TransferBill";
                    CurrentDBSession.RecordDal.AddEntity(record);
                }
                //修改主表数据
                CurrentDal.EditEntity(bill);
            }
            //保存成功返回单据Id
            return CurrentDBSession.SaveChanges() ? bill.Id.ToString() : "no";
        }
        /// <summary>
        /// 审核表单
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <returns></returns>
        public bool Examine(Guid TransferBillId, string UserName)
        {
            TransferBill bill = CurrentDal.LoadEntities(a => a.Id == TransferBillId).FirstOrDefault();
            List<Record> list = bill.Record.ToList();
            foreach (Record item in list)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null)//如果没有库存  新建一条记录
                {
                    inWarehouse = new InWarehouse()
                    {
                        Id = Guid.NewGuid(),
                        ItemBatch = item.ItemBatch,
                        ItemCode = item.ItemCode,
                        ItemLine = item.ItemLine,
                        ItemLocation = item.ItemLocation,
                        Warehouse = item.Warehouse,
                        WarehouseId = item.WarehouseId,
                        ItemName = item.ItemName,
                        ItemSpecifications = item.ItemSpecifications,
                        ItemUnit = item.ItemUnit,
                        Count = item.Count,
                        ItemLocationId = item.ItemLocationId,
                        Company = bill.Company,
                        CompanyId = bill.CompanyId,
                        Department = bill.Department,
                        DepartmentId = bill.DepartmentId

                    };
                    CurrentDBSession.InWarehouseDal.AddEntity(inWarehouse);
                }
                else //有库存
                {
                    inWarehouse.Count += item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                }
            }
            bill.BillState = 2;//改成已审核状态
            bill.ExaminePerson = UserName;//审核人
            bill.ExamineDate = DateTime.Now;//审核时间
            CurrentDal.EditEntity(bill);
            return CurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId">单据Id</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public string GiveupExamine(Guid TransferBillId, string UserName)
        {
            TransferBill bill = CurrentDal.LoadEntities(a => a.Id == TransferBillId).FirstOrDefault();
            List<Record> list = bill.Record.ToList();
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
                if (inWarehouse == null || inWarehouse.Count < item.Count) //当前库存不够 ，不允许做弃审操作
                {
                    res += "物料编号：" + item.ItemCode + "物料名称：" + item.ItemName + "库存不足，弃审失败";

                }
                else if (inWarehouse.Count == item.Count) // 库存正好相等  删除这条记录
                {
                    CurrentDBSession.InWarehouseDal.DeleteEntity(inWarehouse);
                }
                else if (inWarehouse.Count > item.Count)//库存大于记录  相减
                {
                    inWarehouse.Count = inWarehouse.Count - item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
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
