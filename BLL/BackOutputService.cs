﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
using IBLL;
namespace BLL
{
    public partial class BackOutputService : BaseService<BackOutput>, IBackOutputService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(BackOutput bill)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillCode = GetBillCode("TH");//生成单号
                if (bill.BillCode == "no") {
                    bill.BillCode = GetBillCode("TH");//再次生成单号
                }
                bill.BillState = 1;//保存状态
                foreach (var item in bill.Record)//生成子表id
                {
                    if (item.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.Id = Guid.NewGuid();
                    }
                    item.Department = bill.Department;
                    item.DepartmentId = bill.DepartmentId;
                    item.WarehouseId = bill.WarehouseId;
                    item.Warehouse = bill.Warehouse;
                    item.State = bill.BillState;
                    item.CreateDate = DateTime.Now;
                    item.InOrOut = 1;
                    item.MainTableType = "BackOutput";
                    item.InOutTypeId = bill.InputTypeId;
                    item.InOutTypeName = bill.InputType;
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
                foreach (Record record in bill.Record)
                {
                    if (record.Id == Guid.Parse("00000000-0000-0000-0000-000000000000") || record.Id == Guid.Empty)
                    {
                        record.Id = Guid.NewGuid();//设置子表id
                    }
                    record.MainTableId = bill.Id;//设置主表id
                    record.Department = bill.Department;
                    record.DepartmentId = bill.DepartmentId;
                    record.WarehouseId = bill.WarehouseId;
                    record.Warehouse = bill.Warehouse;
                    record.State = bill.BillState;
                    record.CreateDate = DateTime.Now;
                    record.InOrOut = 1;
                    record.MainTableType = "BackOutput";
                    record.InOutTypeId = bill.InputTypeId;
                    record.InOutTypeName = bill.InputType;

                    CurrentDBSession.RecordDal.AddEntity(record);
                    //RecordService.AddEntity(record);
                }
                //修改主表数据
                CurrentDal.EditEntity(bill);
            }
            return CurrentDBSession.SaveChanges() ? bill.Id.ToString() : "no";

        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        public string DeleteBill(Guid BillId)
        {
            #region 物理删除
            //var bill = CurrentDal.LoadEntities(a => a.Id == BillId).FirstOrDefault();
            //if (bill == null)
            //{
            //    return "单据不存在！";
            //}
            //if (bill.BillState != 1)
            //{
            //    return "只有保存状态才可以删除！";
            //}
            //else
            //{
            //    删除子表
            //    var list = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == BillId);
            //    foreach (var item in list)
            //    {
            //        CurrentDBSession.RecordDal.DeleteEntity(item);
            //    }
            //    删除主表
            //    CurrentDal.DeleteEntity(bill);
            //    return CurrentDBSession.SaveChanges() ? "删除成功！" : "删除失败！";
            //}
            #endregion
            #region 逻辑删除
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
                var list = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == BillId);
                foreach (var item in list)
                {
                    item.State = 4;
                    CurrentDBSession.RecordDal.EditEntity(item);
                }
                //删除主表
                bill.BillState = 4;
                CurrentDal.EditEntity(bill);
                return CurrentDBSession.SaveChanges() ? "删除成功！" : "删除失败！";
            }
            #endregion

        }
        /// <summary>
        /// 审核表单
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <returns></returns>
        public string Examine(Guid BackOutputId, string UserName)
        {
            string result = "";
            BackOutput bill = CurrentDal.LoadEntities(a => a.Id == BackOutputId).FirstOrDefault();
            List<Record> list = bill.Record.ToList();
            List<InWarehouse> listAdd = new List<InWarehouse>();//新增列表
            foreach (Record item in list)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                //查暂存
                InWarehouse temp = listAdd.Where(p => p.ItemCode == item.ItemCode && p.ItemLocationId == item.ItemLocationId && p.ItemBatch == item.ItemBatch).FirstOrDefault();
                if (inWarehouse == null && temp == null) //当前库存不够 ，不允许做弃审操作
                {
                    inWarehouse = new InWarehouse()
                    {
                        Id = Guid.NewGuid(),
                        ItemBatch = item.ItemBatch,
                        ItemCode = item.ItemCode,
                        ItemLine = item.ItemLine,
                        ItemLocation = item.ItemLocation,
                        ItemLocationId = item.ItemLocationId,
                        Warehouse = item.Warehouse,
                        WarehouseId = item.WarehouseId,
                        ItemName = item.ItemName,
                        ItemSpecifications = item.ItemSpecifications,
                        ItemUnit = item.ItemUnit,
                        Count = item.Count,
                        Department = item.Department,
                        DepartmentId = item.DepartmentId,
                        Company = bill.Company,
                        CompanyId = bill.CompanyId
                    };
                    item.CurrentCount = item.Count;
                    listAdd.Add(inWarehouse);//添加到暂存区
                }
                else if (inWarehouse == null && temp != null)//只有暂存区有库存
                {
                    temp.Count += item.Count;
                    item.CurrentCount = temp.Count;
                }
                else //数据库有库存
                {
                    inWarehouse.Count += item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
            }
            //最后添加新增列表中数据
            foreach (var item in listAdd)
            {
                CurrentDBSession.InWarehouseDal.AddEntity(item);
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
        public string GiveupExamine(Guid BackOutputId, string UserName)
        {
            BackOutput bill = CurrentDal.LoadEntities(a => a.Id == BackOutputId).FirstOrDefault();
            List<Record> list = bill.Record.ToList();
            string res = "";
            if (bill.BillState != 2)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            List<InWarehouse> listDel = new List<InWarehouse>();//暂存区
            //对每一条记录做逆操作
            foreach (Record item in list)
            {
                item.State = 1;//修改记录状态
                item.ExamineDate = null;//清空审核时间
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//库存
                InWarehouse temp = listDel.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存区
                if (temp != null) //缓存区有  说明数量不足
                {
                   res += "物料编号：" + item.ItemCode + "，物料名称：" + item.ItemName + "，批次："+item.ItemBatch+"，库存不足，弃审失败";
                    continue;
                }
                if (inWarehouse == null || inWarehouse.Count < item.Count)//缓存区没有   数量为空或者小于
                {
                   res += "物料编号：" + item.ItemCode + "，物料名称：" + item.ItemName + "，批次："+item.ItemBatch+"，库存不足，弃审失败";
                    continue;
                }

                //缓存区没有  数量相等 应该删除 加入缓存区
                if (inWarehouse.Count == item.Count) // 暂存区中没有 库存正好相等  删除这条记录
                {
                    listDel.Add(inWarehouse);//添加到删除暂存区
                    item.CurrentCount = 0;//当前库存量
                    continue;
                }
                if (inWarehouse.Count > item.Count)//库存大于记录 暂存区中没有 则相减
                {
                    inWarehouse.Count = inWarehouse.Count - item.Count;
                    item.CurrentCount = inWarehouse.Count;//当前库存量
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                }
                else //数据异常了  应该进不来 记录错误
                {
                    res += "物料编号：" + item.ItemCode + ",数量：" + item.Count + "数据异常！请记下物料编号联系管理员检查！";
                }
            }
            if (res != "")
            {
                return res;
            }
            //删除缓存区数据
            foreach (var item in listDel)
            {
                CurrentDBSession.InWarehouseDal.DeleteEntity(item);
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
        public string GetBillCode(string head) {
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
                res= head + temp + flowid;
            }
            catch {
                res = "no";
            }
            return res;
        }
    }
}
