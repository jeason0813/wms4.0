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
            //    //删除子表
            //    var list = CurrentDBSession.RecordDal.LoadEntities(a => a.MainTableId == BillId);
            //    foreach (var item in list)
            //    {
            //        CurrentDBSession.RecordDal.DeleteEntity(item);
            //    }
            //    //删除主表
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
        /// <param name="DIYBillId"></param>
        /// <returns></returns>
        public string Examine(Guid DIYBillId, string UserName)
        {
            string res = "";
            DIYBill bill = CurrentDal.LoadEntities(a => a.Id == DIYBillId).FirstOrDefault();
            List<Record> list1 = CurrentDBSession.RecordDal.LoadEntities(b=>b.MainTableId==bill.Id&&b.InOrOut==1).ToList();
            List<Record> list0= CurrentDBSession.RecordDal.LoadEntities(b => b.MainTableId == bill.Id && b.InOrOut == 0).ToList();
            #region 检查是否有重复项 
            //List<Record> list = new List<Record>();
            //list.AddRange(list1);
            //list.AddRange(list0);
            //var listBad = from a in list
            //              group a by new { a.ItemCode, a.ItemBatch, a.ItemLocationId } into g
            //              where g.Count() > 1
            //              select new { g.Key.ItemBatch, g.Key.ItemCode, g.Key.ItemLocationId, TotalCount = g.Count() };
            //if (listBad.Count() > 0)
            //{
            //    string res = "记录中发现重复项：";
            //    foreach (var item in listBad)
            //    {
            //        res += item.ItemCode + '-' + item.ItemBatch + '-' + item.ItemLocationId + ',';
            //    }
            //    return (res);
            //}
            #endregion
            List<InWarehouse> listAdd = new List<InWarehouse>();//新增列表
            List<InWarehouse> listDel = new List<InWarehouse>();//删除列表
            foreach (Record item in list1)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                InWarehouse tempAdd = listAdd.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存加  此时缓存减里肯定没有东西
                if (inWarehouse == null && tempAdd == null)// 都没有
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
                        Company = bill.Company,
                        CompanyId = bill.CompanyId,
                        Department = bill.Department,
                        DepartmentId = bill.DepartmentId
                    };
                    item.CurrentCount = item.Count;
                    listAdd.Add(inWarehouse);//添加到暂存区
                }
                else if (inWarehouse == null && tempAdd != null)//只有缓存加有
                {
                    tempAdd.Count += item.Count;
                    item.CurrentCount = tempAdd.Count;
                }
                else if (inWarehouse != null && tempAdd == null )//只有库存有
                {
                    inWarehouse.Count += item.Count;
                    item.CurrentCount = inWarehouse.Count;
                }
                else //有库存
                {
                    return "入库记录有问题(" + item.ItemCode + "-" + item.ItemBatch + "-" + item.ItemLocationId + ")，请联系管理员检查！";
                }
            }
            foreach (Record item in list0)
            {
                item.State = 2;
                item.ExamineDate = DateTime.Now;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                InWarehouse tempAdd = listAdd.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存加
                InWarehouse tempDel = listDel.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存删
                //报库存不足的几种情况
                if ((inWarehouse == null && tempAdd == null && tempDel == null) || (tempAdd == null && tempDel != null && inWarehouse != null) || (inWarehouse != null && tempAdd == null && tempDel == null && item.Count > inWarehouse.Count) || (tempAdd != null && tempDel == null && inWarehouse == null && item.Count > tempAdd.Count))
                {
                   res += "物料编号：" + item.ItemCode + "，物料名称：" + item.ItemName + "，批次："+item.ItemBatch+"，库存不足，审核失败";

                }
                //增加到删除项缓存中
                else if (tempAdd == null && tempDel == null && inWarehouse != null && item.Count == inWarehouse.Count)
                {
                    listDel.Add(inWarehouse);
                    item.CurrentCount = 0;
                }
                //从库存中减
                else if (tempAdd == null && tempDel == null && inWarehouse != null && item.Count < inWarehouse.Count)
                {
                    inWarehouse.Count -= item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
                //从缓存加中删除
                else if (tempAdd != null && tempDel == null && inWarehouse == null && tempAdd.Count == item.Count)
                {
                    listAdd.Remove(tempAdd);
                    item.CurrentCount = 0;
                }
                //改缓存加中数据
                else if (tempAdd != null && tempDel == null && inWarehouse == null && tempAdd.Count > item.Count)
                {
                    tempAdd.Count -= item.Count;
                    item.CurrentCount = tempAdd.Count;
                }
                //其他异常情况
                else
                {
                    return "出库记录有问题(" + item.ItemCode + "-" + item.ItemBatch + "-" + item.ItemLocationId + ")，请联系管理员检查！";
                }
            }
            if (res != "")
            {
                return res;
            }
            foreach (var item in listAdd)
            {
                CurrentDBSession.InWarehouseDal.AddEntity(item);
            }
            foreach (var item in listDel)
            {
                CurrentDBSession.InWarehouseDal.DeleteEntity(item);
            }
            bill.BillState = 2;//改成已审核状态
            bill.ExaminePerson = UserName;//审核人
            bill.ExamineDate = DateTime.Now;
            CurrentDal.EditEntity(bill);
            res = CurrentDBSession.SaveChanges() ? "审核成功！" : "审核失败！";
            return res;
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
            string res = "";
            if (bill.BillState != 2)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            List<InWarehouse> listAdd = new List<InWarehouse>();//新增列表
            List<InWarehouse> listDel = new List<InWarehouse>();//删除列表
            //对每一条记录做逆操作
            foreach (Record item in list0)
            {
                item.State = 1;
                item.ExamineDate = null;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                InWarehouse tempAdd = listAdd.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存加
                if (inWarehouse == null && tempAdd == null)// 都没有
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
                        Company = bill.Company,
                        CompanyId = bill.CompanyId,
                        Department = bill.Department,
                        DepartmentId = bill.DepartmentId
                    };
                    item.CurrentCount = item.Count;
                    listAdd.Add(inWarehouse);//添加到暂存区
                }
                else if (inWarehouse == null && tempAdd != null)//只有缓存加有
                {
                    tempAdd.Count += item.Count;
                    item.CurrentCount = tempAdd.Count;
                }
                else if (inWarehouse != null && tempAdd == null)//只有库存有
                {
                    inWarehouse.Count += item.Count;
                    item.CurrentCount = inWarehouse.Count;
                }
                else //有库存
                {
                    return "入库记录有问题(" + item.ItemCode + "-" + item.ItemBatch + "-" + item.ItemLocationId + ")，请联系管理员检查！";
                }
            }
            foreach (Record item in list1)
            {
                item.State = 1;
                item.ExamineDate = null;
                InWarehouse inWarehouse = CurrentDBSession.InWarehouseDal.LoadEntities(i => i.ItemCode == item.ItemCode && i.ItemLocation == item.ItemLocation && i.ItemBatch == item.ItemBatch).FirstOrDefault();
                InWarehouse tempAdd = listAdd.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存加
                InWarehouse tempDel = listDel.Where(i => i.ItemCode == item.ItemCode && i.ItemLocationId == item.ItemLocationId && i.ItemBatch == item.ItemBatch).FirstOrDefault();//缓存删
                //报库存不足的几种情况
                if ((inWarehouse == null && tempAdd == null && tempDel == null) || (tempAdd == null && tempDel != null && inWarehouse != null) || (inWarehouse != null && tempAdd == null && tempDel == null && item.Count > inWarehouse.Count) || (tempAdd != null && tempDel == null && inWarehouse == null && item.Count > tempAdd.Count))
                {
                   res += "物料编号：" + item.ItemCode + "，物料名称：" + item.ItemName + "，批次："+item.ItemBatch+"，库存不足，审核失败";

                }
                //增加到删除项缓存中
                else if (tempAdd == null && tempDel == null && inWarehouse != null && item.Count == inWarehouse.Count)
                {
                    listDel.Add(inWarehouse);
                    item.CurrentCount = 0;
                }
                //从库存中减
                else if (tempAdd == null && tempDel == null && inWarehouse != null && item.Count < inWarehouse.Count)
                {
                    inWarehouse.Count -= item.Count;
                    CurrentDBSession.InWarehouseDal.EditEntity(inWarehouse);
                    item.CurrentCount = inWarehouse.Count;
                }
                //从缓存加中删除
                else if (tempAdd != null && tempDel == null && inWarehouse == null && tempAdd.Count == item.Count)
                {
                    listAdd.Remove(tempAdd);
                    item.CurrentCount = 0;
                }
                //改缓存加中数据
                else if (tempAdd != null && tempDel == null && inWarehouse == null && tempAdd.Count > item.Count)
                {
                    tempAdd.Count -= item.Count;
                    item.CurrentCount = tempAdd.Count;
                }
                //其他异常情况
                else
                {
                    return "出库记录有问题(" + item.ItemCode + "-" + item.ItemBatch + "-" + item.ItemLocationId + ")，请联系管理员检查！";
                }
            }
            if (res != "")
            {
                return res;
            }
            foreach (var item in listAdd)
            {
                CurrentDBSession.InWarehouseDal.AddEntity(item);
            }
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
