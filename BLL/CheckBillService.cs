using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class CheckBillService
    {
        public string Save(CheckBill CheckBill, string userName)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (CheckBill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                CheckBill.Id = Guid.NewGuid();//生成一个id
                CheckBill.BillCode = GetBillCode("PD");//生成单号
                if (CheckBill.BillCode == "no")
                {
                    CheckBill.BillCode = GetBillCode("PD");//再次生成单号
                }
                CheckBill.BillState = 1;//保存状态
                CheckBill.MakePerson = userName;
                CheckBill.CreateDate = DateTime.Now;
                foreach (var item in CheckBill.CheckDetail)//生成子表id
                {
                    item.Id = Guid.NewGuid(); //重置子表Id

                    item.CreateDate = DateTime.Now;
                    item.State = 1;
                    item.MainTableId = CheckBill.Id;
                }
                CurrentDal.AddEntity(CheckBill);
            }
            //主表有id  说明是修改的
            else
            {
                //删除原子表数据
                List<CheckDetail> list = CurrentDBSession.CheckDetailDal.LoadEntities(a => a.MainTableId == CheckBill.Id).ToList();
                foreach (CheckDetail item in list)
                {
                    CurrentDBSession.CheckDetailDal.DeleteEntity(item);
                }
                //添加子表数据
                foreach (CheckDetail item in CheckBill.CheckDetail)
                {
                    item.Id = Guid.NewGuid();//设置子表id
                    item.MainTableId = CheckBill.Id;//设置主表id
                    item.CreateDate = DateTime.Now;
                    item.State = 1;
                    CurrentDBSession.CheckDetailDal.AddEntity(item);
                }
                //修改主表数据
                CheckBill.MakePerson = userName;
                CheckBill.CreateDate = DateTime.Now;
                CurrentDal.EditEntity(CheckBill);
            }
            //保存成功返回单据Id
            return CurrentDBSession.SaveChanges() ? CheckBill.Id.ToString() : "no";
        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        public string DeleteBill(Guid BillId)
        {
            var bill = CurrentDal.LoadEntities(a => a.Id == BillId).FirstOrDefault();
            if (bill == null)
            {
                return "单据不存在！";
            }
            if (bill.BillState != 1)
            {
                return "只有保存状态才可以删除！";
            }
            else
            {
                //删除子表
                var list = CurrentDBSession.CheckDetailDal.LoadEntities(a => a.MainTableId == BillId);
                foreach (var item in list)
                {
                    CurrentDBSession.CheckDetailDal.DeleteEntity(item);
                }
                //删除主表
                CurrentDal.DeleteEntity(bill);
                return CurrentDBSession.SaveChanges() ? "删除成功！" : "删除失败！";
            }
        }

        public string Examine(Guid CheckBillId, string userName)
        {
            try
            {
                var checkBill = CurrentDal.LoadEntities(a => a.Id == CheckBillId).FirstOrDefault();
                var list = CurrentDBSession.CheckDetailDal.LoadEntities(a => a.MainTableId == CheckBillId && a.Count != a.ChangeCount);//获取有变化的记录
                //删除原Record
                var oldRecordList = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == CheckBillId);
                foreach (var item in oldRecordList)
                {
                    CurrentDBSession.RecordDal.DeleteEntity(item);
                }
                //生成新Record
                List<Record> listRecord = new List<Record>();
                foreach (var item in list)
                {
                    item.State = 2;
                    Record record = new Record()
                    {
                        CreateDate = item.CreateDate,
                        Department = checkBill.Department,
                        DepartmentId = checkBill.DepartmentId,
                        ExamineDate = DateTime.Now,
                        ItemBatch = item.ItemBatch,
                        ItemCode = item.ItemCode,
                        ItemLine = item.ItemLine,
                        ItemLocation = item.ItemLocation,
                        ItemName = item.ItemName,
                        ItemSpecifications = item.ItemSpecifications,
                        ItemUnit = item.ItemUnit,
                        MainTableId = checkBill.Id,
                        State = 2,
                        Warehouse = checkBill.Warehouse,
                        WarehouseId = checkBill.WarehouseId,
                        InOrOut = item.ChangeCount - item.Count > 0 ? 1 : 0,
                        Count = item.ChangeCount - item.Count > 0 ? item.ChangeCount - item.Count : item.Count - item.ChangeCount,
                        ItemLocationId = item.ItemLocationId,
                        Remark = item.Remark,
                        UnitWeight = item.UnitWeight,
                        MainTableType = "CheckBill",
                        Id = Guid.NewGuid()
                    };
                    listRecord.Add(record);
                    //操作库存
                    var inwarehouselist = CurrentDBSession.InWarehouseDal.LoadEntities(a => a.ItemCode == item.ItemCode && a.ItemLocationId == item.ItemLocationId && a.ItemBatch == item.ItemBatch);
                    if (inwarehouselist.Count() > 1)
                    {
                        return  "品项:"+item.ItemCode+ "批次:"+item.ItemBatch+"有多条数据！";
                    }
                    if (inwarehouselist.Count() == 0)
                    {
                        return "品项:"+item.ItemCode + "批次:" + item.ItemBatch + "没有库存记录！";
                    }
                    var inwarehouse = inwarehouselist.FirstOrDefault();
                    if (inwarehouse.Count != item.Count)
                    {
                        return "库存在盘点期间有变化，请重新盘点！";
                    }
                    inwarehouse.Count = item.ChangeCount;
                    CurrentDBSession.InWarehouseDal.EditEntity(inwarehouse);//修改库存
                }
                checkBill.BillState = 2;
                checkBill.ExaminePerson = userName;//审核人
                checkBill.ExamineDate = DateTime.Now;//审核时间
                checkBill.Record = listRecord;
                CurrentDal.EditEntity(checkBill);
                return CurrentDBSession.SaveChanges() ? "审核成功！" : "审核失败！";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="CheckBillId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GiveupExamine(Guid CheckBillId, string userName)
        {
            try
            {
                var checkBill = CurrentDal.LoadEntities(a => a.Id == CheckBillId).FirstOrDefault();
                var list = CurrentDBSession.CheckDetailDal.LoadEntities(a => a.MainTableId == CheckBillId && a.Count != a.ChangeCount);//获取有变化的记录
                //删除原Record
                var oldRecordList = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == CheckBillId);
                foreach (var item in oldRecordList)
                {
                    CurrentDBSession.RecordDal.DeleteEntity(item);
                }
                //生成新Record
                foreach (var item in list)
                {
                    item.State = 1;
                    //操作库存
                    var inwarehouselist = CurrentDBSession.InWarehouseDal.LoadEntities(a => a.ItemCode == item.ItemCode && a.ItemLocationId == item.ItemLocationId && a.ItemBatch == item.ItemBatch);
                    if (inwarehouselist.Count() > 1)
                    {
                        return "品项:" + item.ItemCode + "批次:" + item.ItemBatch + "有多条数据！";
                    }
                    if (inwarehouselist.Count() == 0)
                    {
                        return "品项:" + item.ItemCode + "批次:" + item.ItemBatch + "没有库存记录！";
                    }
                    var inwarehouse = inwarehouselist.FirstOrDefault();
                    if (inwarehouse.Count != item.ChangeCount)
                    {
                        return "库存有变化，弃审失败！";
                    }
                    inwarehouse.Count = item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inwarehouse);//修改库存
                }
                checkBill.BillState = 1;
                CurrentDal.EditEntity(checkBill);
                return CurrentDBSession.SaveChanges() ? "弃审成功！" : "弃审失败！";
            }
            catch (Exception e)
            {
                return e.ToString();
            }




           
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
