using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;
using IBLL;
namespace BLL
{
   public partial class OtherExpenseListService : BaseService<OtherExpenseList>, IOtherExpenseListService
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <returns></returns>
        public string SaveData(OtherExpenseList bill, int billType)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {

                bill.Id = Guid.NewGuid();//生成一个id
                bill.BillState = 1;//保存状态
                bill.BillType = billType;
                bill.CreateDate = DateTime.Now;
                foreach (var item in bill.OtherExpenseListDetail)//生成子表id
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
                    item.State = 1;
                    item.RecorkType = billType;
                    item.CreateDate = DateTime.Now;
                }
                CurrentDal.AddEntity(bill);
            }
            //主表有id  说明是修改的
            else
            {
                //删除原子表数据
                List<OtherExpenseListDetail> list = CurrentDBSession.OtherExpenseListDetailDal.LoadEntities(a => a.MainTableId == bill.Id).ToList();
                foreach (OtherExpenseListDetail item in list)
                {
                    CurrentDBSession.OtherExpenseListDetailDal.DeleteEntity(item);
                }
                //添加子表数据
                foreach (OtherExpenseListDetail record in bill.OtherExpenseListDetail)
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
                    record.State = 1;
                    record.RecorkType = billType;
                    record.CreateDate = DateTime.Now;
                    CurrentDBSession.OtherExpenseListDetailDal.AddEntity(record);
                }
                //修改主表数据
                CurrentDal.EditEntity(bill);
            }
            return CurrentDBSession.SaveChanges() ? bill.Id.ToString() : "no";

        }
        /// <summary>
        /// 初审表单
        /// </summary>
        /// <param name="OtherExpenseListId"></param>
        /// <returns></returns>
        public string Examine1(Guid OtherExpenseListId, string UserName)
        {
            string result = "";
            OtherExpenseList bill = CurrentDal.LoadEntities(a => a.Id == OtherExpenseListId).FirstOrDefault();
            List<OtherExpenseListDetail> list = bill.OtherExpenseListDetail.ToList();
            foreach (OtherExpenseListDetail item in list)
            {
                item.State = 2;
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
        /// 审核表单
        /// </summary>
        /// <param name="OtherExpenseListId"></param>
        /// <returns></returns>
        public string Examine2(Guid OtherExpenseListId, string UserName)
        {
            string result = "";
            OtherExpenseList bill = CurrentDal.LoadEntities(a => a.Id == OtherExpenseListId).FirstOrDefault();
            List<OtherExpenseListDetail> list = bill.OtherExpenseListDetail.ToList();
            foreach (OtherExpenseListDetail item in list)
            {
                item.State = 3;
                item.ExamineDate = DateTime.Now;
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
        /// <param name="OtherExpenseListId">单据Id</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public string GiveupExamine(Guid OtherExpenseListId, string UserName)
        {
            OtherExpenseList bill = CurrentDal.LoadEntities(a => a.Id == OtherExpenseListId).FirstOrDefault();
            List<OtherExpenseListDetail> list = bill.OtherExpenseListDetail.ToList();
            string res = "";
            if (bill.BillState != 2&&bill.BillState!=3)
            {
                res = "单据状态不对，无法弃审！";
                return res;
            }
            //对每一条记录做逆操作
            foreach (OtherExpenseListDetail item in list)
            {
                item.State = 1;//修改记录状态
                item.ExamineDate = null;//清空审核时间
            }
            bill.BillState = 1;//改成编辑状态
            bill.ExaminePerson = null;//清除审核人
            bill.ExamineDate = null;//清除审核时间
            CurrentDal.EditEntity(bill);
            return CurrentDBSession.SaveChanges() ? "操作成功" : "操作失败";
        }
    }
}
