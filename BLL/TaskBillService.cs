using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class TaskBillService
    {
        public string SaveData(TaskBill bill, string[] records)
        {
            //主表id为0 新增一条记录  此时子表数据会自动插入
            if (bill.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                bill.Id = Guid.NewGuid();//生成一个id
                //bill.BillCode = GetBillCode("RW");//生成单号
                //if (bill.BillCode == "no")
                //{
                //    bill.BillCode = GetBillCode("RW");//再次生成单号
                //}
                bill.BillState = 1;//保存状态
                if (records != null) {
                    //修改对应交货单的任务单字段
                    List<GiveBill> list = CurrentDBSession.GiveBillDal.LoadEntities(a => records.Contains(a.BillCode)).ToList();
                    foreach (GiveBill item in list)
                    {
                        item.TaskBillCode = bill.BillCode;
                        CurrentDBSession.GiveBillDal.EditEntity(item);//设置交货单的任务单号
                    }
                }
                CurrentDal.AddEntity(bill);
            }
            else  //主表有id  说明是修改的
            {
                List<GiveBill> list; //把原来属于但是现在不属于这张任务单的交货单清空
                if (records != null) //修改后子表有记录
                {
                    list = CurrentDBSession.GiveBillDal.LoadEntities(a => (a.TaskBillCode == bill.BillCode) && (!records.Contains(a.BillCode))).ToList();
                }
                else //修改后子表全删除了
                {
                    list = CurrentDBSession.GiveBillDal.LoadEntities(a => a.TaskBillCode == bill.BillCode).ToList();
                }
                foreach (var item in list)
                {
                    item.TaskBillCode = null;
                    CurrentDBSession.GiveBillDal.EditEntity(item); 
                }
                if (records != null) {
                    //设置交货单的任务单号
                    var list1 = CurrentDBSession.GiveBillDal.LoadEntities(a => records.Contains(a.BillCode));
                    foreach (var item in list1)
                    {
                        item.TaskBillCode = bill.BillCode;
                        CurrentDBSession.GiveBillDal.EditEntity(item);
                    }
                }
                //修改主表数据
                CurrentDal.EditEntity(bill);
            }
            //保存成功返回单据Id
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
            //    //清除交货单中任务单号字段  
            //    var list = CurrentDBSession.GiveBillDal.LoadEntities(a => a.LBTaskBillCode == bill.BillCode);
            //    foreach (var item in list)
            //    {
            //        item.LBTaskBillCode = null;
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
                //清除交货单中任务单号字段  
                var list = CurrentDBSession.GiveBillDal.LoadEntities(a => a.LBTaskBillCode == bill.BillCode);
                foreach (var item in list)
                {
                    item.LBTaskBillCode = null;
                    CurrentDBSession.GiveBillDal.EditEntity(item);
                }
                //删除主表
                bill.BillState = 4;
                CurrentDal.EditEntity(bill);
                return CurrentDBSession.SaveChanges() ? "删除成功！" : "删除失败！";
            }
            #endregion

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
