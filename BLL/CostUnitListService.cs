﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;
using IBLL;
namespace BLL
{
    public partial class CostUnitListService : BaseService<CostUnitList>,ICostUnitListService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(CostUnitList bill,int billType)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillState = 1;//保存状态
                bill.BillType = billType;
                bill.CreateDate = DateTime.Now;
                foreach (var item in bill.CostUnitListDetail)//生成子表id
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
                    item.state = 1;
                    item.CreatePerson = bill.ExaminePerson;
                    item.ExaminePerson = bill.ExaminePerson;
                    item.CreateDate = DateTime.Now;
                    item.RecordType = billType;
                }
                CurrentDal.AddEntity(bill);
            }
            //主表有id  说明是修改的
            else
            {
                //删除原子表数据
                List<CostUnitListDetail> list = CurrentDBSession.CostUnitListDetailDal.LoadEntities(a => a.MainTableId == bill.Id).ToList();
                foreach (CostUnitListDetail item in list)
                {
                    CurrentDBSession.CostUnitListDetailDal.DeleteEntity(item);
                }
                foreach (CostUnitListDetail record in bill.CostUnitListDetail)
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
                    record.state = 1;
                    record.CreateDate = DateTime.Now;
                    record.RecordType = billType;
                    record.CreatePerson = bill.ExaminePerson;
                    record.ExaminePerson = bill.ExaminePerson;
                    CurrentDBSession.CostUnitListDetailDal.AddEntity(record);
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
        public string Examine(Guid CostUnitListId, string UserName)
        {
            string result = "";
            CostUnitList bill = CurrentDal.LoadEntities(a => a.Id == CostUnitListId).FirstOrDefault();
            List<CostUnitListDetail> list = bill.CostUnitListDetail.ToList();
            foreach (CostUnitListDetail item in list)
            {
                item.state = 2;
                item.ExamineDate = DateTime.Now;
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
        public string GiveupExamine(Guid CostUnitListId, string UserName)
        {
            CostUnitList bill = CurrentDal.LoadEntities(a => a.Id == CostUnitListId).FirstOrDefault();
            List<CostUnitListDetail> list = bill.CostUnitListDetail.ToList();
            string res = "";
            if (bill.BillState != 2)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            //对每一条记录做逆操作
            foreach (CostUnitListDetail item in list)
            {
                item.state = 1;
                item.ExamineDate = null;
            }
            bill.BillState = 1;//改成编辑状态
            bill.ExaminePerson = null;//清除审核人
            bill.ExamineDate = null;//清除审核时间
            CurrentDal.EditEntity(bill);
            return CurrentDBSession.SaveChanges() ? "操作成功" : "操作失败";
        }
    }
}
