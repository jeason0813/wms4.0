using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public  partial interface ITaskBillService: IBaseService<TaskBill>
    {
        string SaveData(TaskBill bill, string[] records);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);

    }
}
