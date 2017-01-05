using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface ICheckBillService
    {
        string Save(CheckBill CheckBill, string userName);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        string Examine(Guid CheckBillId, string userName);
        string  GiveupExamine(Guid CheckBillId, string userName);

    }
}
