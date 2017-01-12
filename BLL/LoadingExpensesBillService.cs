using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;
using IBLL;
namespace BLL
{
    public partial class LoadingExpensesBillService : BaseService<LoadingExpensesBill>, ILoadingExpensesBillService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(LoadingExpensesBill bill, int billType)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {

                bill.Id = Guid.NewGuid();//生成一个id
                if (billType == 1)
                {
                    bill.BillCode = GetBillCode("ZS");//生成单号  装卸费收入
                    if (bill.BillCode == "no")
                    {
                        bill.BillCode = GetBillCode("ZS");//再次生成单号
                    }
                }
                if (billType == 2)
                {
                    bill.BillCode = GetBillCode("ZF");//生成单号 成本
                    if (bill.BillCode == "no")
                    {
                        bill.BillCode = GetBillCode("ZF");//再次生成单号
                    }
                }
                if (billType == 3)
                {
                    bill.BillCode = GetBillCode("LS");//生成单号 力资费收入
                    if (bill.BillCode == "no")
                    {
                        bill.BillCode = GetBillCode("LS");//再次生成单号
                    }
                }
                bill.BillState = 1;//保存状态
                bill.BillType = billType;
                bill.CreateDate = DateTime.Now;
                foreach (var item in bill.LoadingAndLaborCostDetail)//生成子表id
                {
                    if (item.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.Id = Guid.NewGuid();
                    }
                    item.MainTableId = bill.Id;
                    item.Department = bill.Department;
                    item.DepartmentId = bill.DepartmentId;
                    item.Company = bill.Company;
                    item.CompanyId = bill.CompanyId;
                    item.BillState = 1;
                    item.BillType = billType;
                    item.CreateDate = DateTime.Now;
                }
                CurrentDal.AddEntity(bill);
            }
            //主表有id  说明是修改的
            else
            {
                //删除原子表数据
                List<LoadingAndLaborCostDetail> list = CurrentDBSession.LoadingAndLaborCostDetailDal.LoadEntities(a => a.MainTableId == bill.Id).ToList();
                foreach (LoadingAndLaborCostDetail item in list)
                {
                    CurrentDBSession.LoadingAndLaborCostDetailDal.DeleteEntity(item);
                }
                //添加子表数据
                foreach (LoadingAndLaborCostDetail record in bill.LoadingAndLaborCostDetail)
                {
                    if (record.Id == Guid.Parse("00000000-0000-0000-0000-000000000000") || record.Id == Guid.Empty)
                    {
                        record.Id = Guid.NewGuid();//设置子表id
                    }
                    record.MainTableId = bill.Id;//设置主表id
                    record.Department = bill.Department;
                    record.DepartmentId = bill.DepartmentId;
                    record.Company = bill.Company;
                    record.CompanyId = bill.CompanyId;
                    record.BillState = 1;
                    record.BillType = billType;
                    record.CreateDate = DateTime.Now;
                    CurrentDBSession.LoadingAndLaborCostDetailDal.AddEntity(record);
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
            //    var list = CurrentDBSession.LoadingAndLaborCostDetailDal.LoadEntities(a => a.MainTableId == BillId);
            //    foreach (var item in list)
            //    {
            //        CurrentDBSession.LoadingAndLaborCostDetailDal.DeleteEntity(item);
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
                var list = CurrentDBSession.LoadingAndLaborCostDetailDal.LoadEntities(a => a.MainTableId == BillId);
                foreach (var item in list)
                {
                    item.BillState = 4;
                    CurrentDBSession.LoadingAndLaborCostDetailDal.EditEntity(item);
                }
                //删除主表
                bill.BillState = 4;
                CurrentDal.EditEntity(bill);
                return CurrentDBSession.SaveChanges() ? "删除成功！" : "删除失败！";
            }
            #endregion

        }
        /// <summary>
        /// 初审表单
        /// </summary>
        /// <param name="LoadingExpensesBilltId"></param>
        /// <returns></returns>
        public string Examine1(Guid LoadingExpensesBilltId, string UserName)
        {
            string result = "";
            LoadingExpensesBill bill = CurrentDal.LoadEntities(a => a.Id == LoadingExpensesBilltId).FirstOrDefault();
            List<LoadingAndLaborCostDetail> list = bill.LoadingAndLaborCostDetail.ToList();
            foreach (LoadingAndLaborCostDetail item in list)
            {
                item.BillState = 2;
                item.ExaminaDate = DateTime.Now;
            }
            bill.BillState = 2;//改成已审核状态
            bill.ExaminePerson = UserName;//审核人
            bill.ExamineDate = DateTime.Now;
            CurrentDal.EditEntity(bill);
            result = CurrentDBSession.SaveChanges() ? "审核成功！" : "审核失败！";
            return result;
        }
        /// <summary>
        /// 审核表单
        /// </summary>
        /// <param name="LoadingExpensesBilltId"></param>
        /// <returns></returns>
        public string Examine2(Guid LoadingExpensesBilltId, string UserName)
        {
            string result = "";
            LoadingExpensesBill bill = CurrentDal.LoadEntities(a => a.Id == LoadingExpensesBilltId).FirstOrDefault();
            List<LoadingAndLaborCostDetail> list = bill.LoadingAndLaborCostDetail.ToList();
            foreach (LoadingAndLaborCostDetail item in list)
            {
                item.BillState = 3;
                item.ExaminaDate = DateTime.Now;
            }
            bill.BillState = 3;//改成已审核状态
            bill.ExaminePerson = UserName;//审核人
            bill.ExamineDate = DateTime.Now;
            CurrentDal.EditEntity(bill);
            result = CurrentDBSession.SaveChanges() ? "审核成功！" : "审核失败！";
            return result;
        }
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="LoadingExpensesBilltId">单据Id</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public string GiveupExamine(Guid LoadingExpensesBilltId, string UserName)
        {
            LoadingExpensesBill bill = CurrentDal.LoadEntities(a => a.Id == LoadingExpensesBilltId).FirstOrDefault();
            List<LoadingAndLaborCostDetail> list = bill.LoadingAndLaborCostDetail.ToList();
            string res = "";
            if (bill.BillState != 2 && bill.BillState != 3)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            //对每一条记录做逆操作
            foreach (LoadingAndLaborCostDetail item in list)
            {
                item.BillState = 1;//修改记录状态
                item.ExaminaDate = null;//清空审核时间
            }
            bill.BillState = 1;//改成编辑状态
            bill.ExaminePerson = null;//清除审核人
            bill.ExamineDate = null;//清除审核时间
            CurrentDal.EditEntity(bill);
            return CurrentDBSession.SaveChanges() ? "操作成功" : "操作失败";
        }
        /// <summary>
        /// 根据条件查询单据信息（转仓单、退仓单、交货单、退货单、交货回执单）
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public LoadingExpensesBill GetData(LoadingAndLaborQueryView A, int billtype, string power, List<LaborAndLoading3QueryConditions> Businesstype, List<LaborAndLoading3QueryConditions> LodingType, List<LaborAndLoading3QueryConditions> Warehouseid)
        {
            LoadingExpensesBill bill = new LoadingExpensesBill(); ;//五个list合成一个返回
            if (!A.BackInput && A.BackInput && A.GiveBill && A.GiveBackBill && A.GiveBill)
            {
                return bill;
            }
            else
            {
                if (A.TransferBill)//转仓单
                {
                    var transferbillList = CurrentDBSession.TransferBillDal.LoadEntities(a => a.ExamineDate != null);
                    if (string.IsNullOrWhiteSpace(A.Department2Id))
                    {
                        transferbillList = transferbillList.Where(a => power.Contains(a.DepartmentId));
                    }
                    else
                    {
                        transferbillList = transferbillList.Where(c => c.DepartmentId == A.Department2Id);
                    }
                    if (Businesstype != null)
                    {
                        string[] businesstype = new string[Businesstype.Count()];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Businesstype)
                        {
                            businesstype[i] = item.Name.ToString().Trim();
                            i++;
                    }
                        transferbillList = transferbillList.Where(c => businesstype.Contains(c.BusinessType));
                    }
                    if (LodingType != null)
                    {
                        string[] loadingtype = new string[LodingType.Count()];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in LodingType)
                        {
                            loadingtype[i] = item.Name.ToString().Trim();
                            i++;
                    }
                        transferbillList = transferbillList.Where(c => loadingtype.Contains(c.LoadGoodsType));
                    }
                    if (Warehouseid != null)
                    {
                        int counts = Warehouseid.Count();
                        int?[] warehouseidArray = new int?[counts];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Warehouseid)
                        {
                            warehouseidArray[i] = item.Id;
                            i++;
                    }
                        transferbillList = transferbillList.Where(c => warehouseidArray.Contains(c.WarehouseId));
                    }
                    if (A.dateStart != null)
                    {
                        transferbillList = transferbillList.Where(c => c.ExamineDate >= A.dateStart);
                    }
                    if (A.dateEnd != null)
                    {
                        A.dateEnd = A.dateEnd.AddDays(1);
                        transferbillList = transferbillList.Where(c => c.ExamineDate < A.dateEnd);
                    }
                    if (transferbillList != null)
                    {
                        foreach (TransferBill item in transferbillList)
                        {
                            foreach (Record record in item.Record)
                            {
                                LoadingAndLaborCostDetail detail = new LoadingAndLaborCostDetail();
                                detail.ListType = "转仓单";
                                detail.BillCode = item.BillCode;
                                detail.BillExamineDate = item.ExamineDate;
                                detail.Company = item.Company;
                                detail.CompanyId = item.CompanyId;
                                detail.Department = item.Department;
                                detail.DepartmentId = item.DepartmentId;
                                detail.Warehouse = record.Warehouse;
                                detail.WarehouseId = record.WarehouseId;
                                detail.BusinessType = item.BusinessType;
                                detail.LoadingType = item.LoadGoodsType;
                                detail.ItemCode = record.ItemCode;
                                detail.ItemLine = record.ItemLine;
                                detail.ItemName = record.ItemName;
                                detail.ItemBatch = record.ItemBatch;
                                detail.ItemSpecifications = record.ItemSpecifications;
                                detail.ItemUnit = record.ItemUnit;
                                detail.Count = record.Count;
                                detail.UnitWeight = Convert.ToDouble(record.UnitWeight);
                                detail.Weight = record.Weight;
                                bill.LoadingAndLaborCostDetail.Add(detail);
                            }
                        }
                    }
                }
                if (A.BackInput)//退仓单
                {
                    var BackInputList = CurrentDBSession.BackInputDal.LoadEntities(a => a.ExamineDate != null);
                    if (string.IsNullOrWhiteSpace(A.Department2Id))
                    {
                        BackInputList = BackInputList.Where(a => power.Contains(a.DepartmentId));
                    }
                    else
                    {
                        BackInputList = BackInputList.Where(c => c.DepartmentId == A.Department2Id);
                    }
                    if (Businesstype != null)
                    {
                        string[] businesstype = new string[Businesstype.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Businesstype)
                        {
                            businesstype[i] = item.Name.ToString().Trim();
                            i++;
                        }
                        BackInputList = BackInputList.Where(c => businesstype.Contains(c.BusinessType));
                    }
                    if (LodingType != null)
                    {
                        string[] loadingtype = new string[LodingType.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in LodingType)
                    {
                            loadingtype[i] = item.Name.ToString().Trim();
                            i++;
                        }
                        BackInputList = BackInputList.Where(c => loadingtype.Contains(c.LoadGoodsType));
                    }
                    if (Warehouseid != null)
                    {
                        int counts = Warehouseid.Count;
                        int?[] warehouseidArray = new int?[counts];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Warehouseid)
                    {
                            warehouseidArray[i] = item.Id;
                            i++;
                        }
                        BackInputList = BackInputList.Where(c => warehouseidArray.Contains(c.WarehouseId));
                    }
                    if (A.dateStart != null)
                    {
                        BackInputList = BackInputList.Where(c => c.ExamineDate >= A.dateStart);
                    }
                    if (A.dateEnd != null)
                    {
                        A.dateEnd = A.dateEnd.AddDays(1);
                        BackInputList = BackInputList.Where(c => c.ExamineDate < A.dateEnd);
                    }
                    if (BackInputList != null)
                    {
                        foreach (BackInput item in BackInputList)
                        {
                            foreach (Record record in item.Record)
                            {
                                LoadingAndLaborCostDetail detail = new LoadingAndLaborCostDetail();
                                detail.ListType = "退仓单";
                                detail.BillCode = item.BillCode;
                                detail.BillExamineDate = item.ExamineDate;
                                detail.Company = item.Company;
                                detail.CompanyId = item.CompanyId;
                                detail.Department = item.Department;
                                detail.DepartmentId = item.DepartmentId;
                                detail.Warehouse = record.Warehouse;
                                detail.WarehouseId = record.WarehouseId;
                                detail.BusinessType = item.BusinessType;
                                detail.LoadingType = item.LoadGoodsType;
                                detail.ItemCode = record.ItemCode;
                                detail.ItemLine = record.ItemLine;
                                detail.ItemName = record.ItemName;
                                detail.ItemBatch = record.ItemBatch;
                                detail.ItemSpecifications = record.ItemSpecifications;
                                detail.ItemUnit = record.ItemUnit;
                                detail.Count = record.Count;
                                detail.UnitWeight = Convert.ToDouble(record.UnitWeight);
                                detail.Weight = record.Weight;
                                bill.LoadingAndLaborCostDetail.Add(detail);
                            }
                        }
                    }
                }
                if (A.GiveBill)//交货单
                {
                    var GiveBillList = CurrentDBSession.GiveBillDal.LoadEntities(a => a.ExamineDate != null);
                    if (string.IsNullOrWhiteSpace(A.Department2Id))
                    {
                        GiveBillList = GiveBillList.Where(a => power.Contains(a.DepartmentId));
                    }
                    else
                    {
                        GiveBillList = GiveBillList.Where(c => c.DepartmentId == A.Department2Id);
                    }
                    if (Businesstype != null)
                    {
                        string[] businesstype = new string[Businesstype.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Businesstype)
                    {
                            businesstype[i] = item.Name.ToString().Trim();
                            i++;
                        }
                        GiveBillList = GiveBillList.Where(c => businesstype.Contains(c.BusinessType));
                    }
                    if (LodingType != null)
                    {
                        string[] loadingtype = new string[LodingType.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in LodingType)
                    {
                            loadingtype[i] = item.Name.ToString().Trim();
                            i++;
                    }
                        GiveBillList = GiveBillList.Where(c => loadingtype.Contains(c.LoadGoodsType));
                    }
                    if (Warehouseid != null)
                    {
                        int counts = Warehouseid.Count;
                        int?[] warehouseidArray = new int?[counts];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Warehouseid)
                        {
                            warehouseidArray[i] = item.Id;
                            i++;
                    }
                        GiveBillList = GiveBillList.Where(c => warehouseidArray.Contains(c.WarehouseId));
                    }
                    if (A.dateStart != null)
                    {
                        GiveBillList = GiveBillList.Where(c => c.ExamineDate >= A.dateStart);
                    }
                    if (A.dateEnd != null)
                    {
                        A.dateEnd = A.dateEnd.AddDays(1);
                        GiveBillList = GiveBillList.Where(c => c.ExamineDate < A.dateEnd);
                    }
                    if (GiveBillList != null)
                    {
                        foreach (GiveBill item in GiveBillList)
                        {
                            foreach (Record record in item.Record)
                            {
                                LoadingAndLaborCostDetail detail = new LoadingAndLaborCostDetail();
                                detail.ListType = "交货单";
                                detail.BillCode = item.BillCode;
                                detail.BillExamineDate = item.ExamineDate;
                                detail.Company = item.Company;
                                detail.CompanyId = item.CompanyId;
                                detail.Department = item.Department;
                                detail.DepartmentId = item.DepartmentId;
                                detail.Warehouse = record.Warehouse;
                                detail.WarehouseId = record.WarehouseId;
                                detail.BusinessType = item.BusinessType;
                                detail.LoadingType = item.LoadGoodsType;
                                detail.ItemCode = record.ItemCode;
                                detail.ItemLine = record.ItemLine;
                                detail.ItemBatch = record.ItemBatch;
                                detail.ItemName = record.ItemName;
                                detail.ItemSpecifications = record.ItemSpecifications;
                                detail.ItemUnit = record.ItemUnit;
                                detail.Count = record.Count;
                                detail.UnitWeight = Convert.ToDouble(record.UnitWeight);
                                detail.Weight = record.Weight;
                                bill.LoadingAndLaborCostDetail.Add(detail);
                            }
                        }
                    }
                }
                if (A.BackOutput)//退货单
                {
                    var BackOutputList = CurrentDBSession.BackOutputDal.LoadEntities(a => a.ExamineDate != null);
                    if (string.IsNullOrWhiteSpace(A.Department2Id))
                    {
                        BackOutputList = BackOutputList.Where(a => power.Contains(a.DepartmentId));
                    }
                    else
                    {
                        BackOutputList = BackOutputList.Where(c => c.DepartmentId == A.Department2Id);
                    }
                    if (Businesstype != null)
                    {
                        string[] businesstype = new string[Businesstype.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Businesstype)
                        {
                            businesstype[i] = item.Name.ToString().Trim();
                            i++;
                    }
                        BackOutputList = BackOutputList.Where(c => businesstype.Contains(c.BusinessType));
                    }
                    if (LodingType != null)
                    {
                        string[] loadingtype = new string[LodingType.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in LodingType)
                        {
                            loadingtype[i] = item.Name.ToString().Trim();
                            i++;
                    }
                        BackOutputList = BackOutputList.Where(c => loadingtype.Contains(c.LoadGoodsType));
                    }
                    if (Warehouseid != null)
                    {
                        int counts = Warehouseid.Count;
                        int?[] warehouseidArray = new int?[counts];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Warehouseid)
                    {
                            warehouseidArray[i] = item.Id;
                            i++;
                        }
                        BackOutputList = BackOutputList.Where(c => warehouseidArray.Contains(c.WarehouseId));
                    }
                    if (A.dateStart != null)
                    {
                        BackOutputList = BackOutputList.Where(c => c.ExamineDate >= A.dateStart);
                    }
                    if (A.dateEnd != null)
                    {
                        A.dateEnd = A.dateEnd.AddDays(1);
                        BackOutputList = BackOutputList.Where(c => c.ExamineDate < A.dateEnd);
                    }
                    if (BackOutputList != null)
                    {
                        foreach (BackOutput item in BackOutputList)
                        {
                            foreach (Record record in item.Record)
                            {
                                LoadingAndLaborCostDetail detail = new LoadingAndLaborCostDetail();
                                detail.ListType = "退货单";
                                detail.BillCode = item.BillCode;
                                detail.BillExamineDate = item.ExamineDate;
                                detail.Company = item.Company;
                                detail.CompanyId = item.CompanyId;
                                detail.Department = item.Department;
                                detail.DepartmentId = item.DepartmentId;
                                detail.Warehouse = record.Warehouse;
                                detail.WarehouseId = record.WarehouseId;
                                detail.BusinessType = item.BusinessType;
                                detail.LoadingType = item.LoadGoodsType;
                                detail.ItemCode = record.ItemCode;
                                detail.ItemLine = record.ItemLine;
                                detail.ItemName = record.ItemName;
                                detail.ItemBatch = record.ItemBatch;
                                detail.ItemSpecifications = record.ItemSpecifications;
                                detail.ItemUnit = record.ItemUnit;
                                detail.Count = record.Count;
                                detail.UnitWeight = Convert.ToDouble(record.UnitWeight);
                                detail.Weight = record.Weight;
                                bill.LoadingAndLaborCostDetail.Add(detail);
                            }
                        }
                    }
                }
                if (A.GiveBackBill)//交货回执单
                {
                    var GiveBillList = CurrentDBSession.GiveBillDal.LoadEntities(a => a.ExamineDate != null);
                    if (string.IsNullOrWhiteSpace(A.Department2Id))
                    {
                        GiveBillList = GiveBillList.Where(a => power.Contains(a.DepartmentId));
                    }
                    else
                    {
                        GiveBillList = GiveBillList.Where(c => c.DepartmentId == A.Department2Id);
                    }
                    if (Businesstype != null)
                    {
                        string[] businesstype = new string[Businesstype.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Businesstype)
                    {
                            businesstype[i] = item.Name.ToString().Trim();
                            i++;
                        }
                        GiveBillList = GiveBillList.Where(c => businesstype.Contains(c.BusinessType));
                    }
                    if (LodingType != null)
                    {
                        string[] loadingtype = new string[LodingType.Count];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in LodingType)
                    {
                            loadingtype[i] = item.Name.ToString().Trim();
                            i++;
                        }
                        GiveBillList = GiveBillList.Where(c => loadingtype.Contains(c.LoadGoodsType));
                    }
                    if (Warehouseid != null)
                    {
                        int counts = Warehouseid.Count;
                        int?[] warehouseidArray = new int?[counts];
                        int i = 0;
                        foreach (LaborAndLoading3QueryConditions item in Warehouseid)
                    {
                            warehouseidArray[i] = item.Id;
                            i++;
                        }
                        GiveBillList = GiveBillList.Where(c => warehouseidArray.Contains(c.WarehouseId));
                    }
                    if (A.dateStart != null)
                    {
                        GiveBillList = GiveBillList.Where(c => c.ExamineDate >= A.dateStart);
                    }
                    if (A.dateEnd != null)
                    {
                        A.dateEnd = A.dateEnd.AddDays(1);
                        GiveBillList = GiveBillList.Where(c => c.ExamineDate < A.dateEnd);
                    }
                    if (GiveBillList != null)
                    {
                        foreach (GiveBill item in GiveBillList)
                        {
                            foreach (Record record in item.Record)
                            {
                                if (!string.IsNullOrWhiteSpace(record.ReceiveCount.ToString().Trim()) && !string.IsNullOrWhiteSpace(record.ReceiveWeight.ToString().Trim()))
                                {
                                    LoadingAndLaborCostDetail detail = new LoadingAndLaborCostDetail();
                                    detail.ListType = "交货回执单";
                                    detail.BillCode = item.BillCode;
                                    detail.BillExamineDate = item.ExamineDate;
                                    detail.Company = item.Company;
                                    detail.CompanyId = item.CompanyId;
                                    detail.Department = item.Department;
                                    detail.DepartmentId = item.DepartmentId;
                                    detail.Warehouse = record.Warehouse;
                                    detail.WarehouseId = record.WarehouseId;
                                    detail.BusinessType = item.BusinessType;
                                    detail.LoadingType = item.LoadGoodsType;
                                    detail.ItemCode = record.ItemCode;
                                    detail.ItemLine = record.ItemLine;
                                    detail.ItemName = record.ItemName;
                                    detail.ItemBatch = record.ItemBatch;
                                    detail.ItemSpecifications = record.ItemSpecifications;
                                    detail.ItemUnit = record.ItemUnit;
                                    detail.Count = record.ReceiveCount;
                                    detail.UnitWeight = Convert.ToDouble(record.UnitWeight);
                                    detail.Weight = record.ReceiveWeight;
                                    bill.LoadingAndLaborCostDetail.Add(detail);
                                }
                            }
                        }
                    }
                }
                switch (billtype)
                {
                   
                    case 1:  //装卸费应收
                        var list1 = CurrentDBSession.CostUnitListDetailDal.LoadEntities(a => a.RecordType == 2 && a.ExamineDate != null && a.CostItemCode.Trim() == "60010303" && a.CostItemName.Trim() == "装卸收入");
                        foreach (LoadingAndLaborCostDetail item in bill.LoadingAndLaborCostDetail)
                        {
                            item.CostItemCode = "60010303";//装卸费  费用项目和编号只有一个写死
                            item.CostItemName = "装卸收入";
                           var temp= list1.Where(a => a.BuniessTypeName == item.BusinessType&&a.LoadGoodsTypeName==item.LoadingType&&(a.ItemLineName==null||a.ItemLineName==item.ItemLine)).OrderByDescending(a=>a.ItemLineName).FirstOrDefault();
                            if (temp != null) {
                                item.CostItemCode = temp.CostItemCode;
                                item.CostItemName = temp.CostItemName;
                                item.LoadingUnitPrice = temp.UnitPrice;
                                                    item.AllAmount = item.Weight * item.LoadingUnitPrice / 1000;
                            }
                        }
                        break;
                    case 2://装卸费成本

                        var list2 = CurrentDBSession.CostUnitListDetailDal.LoadEntities(a => a.RecordType == 1 && a.ExamineDate != null && a.CostItemCode.Trim() == "6401080105" && a.CostItemName.Trim() == "人工成本-装卸费");
                        foreach (LoadingAndLaborCostDetail item in bill.LoadingAndLaborCostDetail)
                        {
                            item.CostItemCode = "6401080105";//装卸费  费用项目和编号只有一个写死
                            item.CostItemName = "人工成本-装卸费";
                            var temp = list2.Where(a => a.BuniessTypeName == item.BusinessType && a.LoadGoodsTypeName == item.LoadingType && (a.ItemLineName == null || a.ItemLineName == item.ItemLine)).OrderByDescending(a => a.ItemLineName).FirstOrDefault();
                            if (temp != null)
                                                {
                                item.CostItemCode = temp.CostItemCode;
                                item.CostItemName = temp.CostItemName;
                                item.LoadingUnitPrice = temp.UnitPrice;
                                                    item.AllAmount = item.Weight * item.LoadingUnitPrice / 1000;
                                                }
                                            }
                        break;
                    case 3: //力资费应收

                        var list3 = CurrentDBSession.CostUnitListDetailDal.LoadEntities(a => a.RecordType == 2 && a.ExamineDate != null && a.CostItemCode.Trim() == "60010302" && a.CostItemName.Trim() == "力资费收入");
                        foreach (LoadingAndLaborCostDetail item in bill.LoadingAndLaborCostDetail)
                        {
                            item.CostItemCode = "60010302";//装卸费  费用项目和编号只有一个写死
                            item.CostItemName = "力资费收入";
                            var temp = list3.Where(a => a.BuniessTypeName == item.BusinessType && a.LoadGoodsTypeName == item.LoadingType && (a.ItemLineName == null || a.ItemLineName == item.ItemLine)).OrderByDescending(a => a.ItemLineName).FirstOrDefault();
                            if (temp != null)
                            {
                                item.CostItemCode = temp.CostItemCode;
                                item.CostItemName = temp.CostItemName;
                                item.LaborUnitPrice = temp.UnitPrice;
                                item.AllAmount = item.Weight * item.LaborUnitPrice / 1000;
                            }
                        }
                        break;
                }
            }
            return bill;
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
                temp = head + temp;
                int totalCount;//没用
                string flowid;
                var lastbill = CurrentDal.LoadPageEntities(a => a.BillCode.Substring(0, 8) == temp, a => a.BillCode, 1, 1, false, out totalCount).FirstOrDefault();
                if (lastbill == null)
                {
                    flowid = "00001";
                }
                else
                {
                    flowid = string.Format("{0:00000}", (Convert.ToInt32(lastbill.BillCode.Substring(8, 5)) + 1));
                }
                res = temp + flowid;
            }
            catch
            {
                res = "no";
            }
            return res;
        }
    }
}
