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


    }
}
