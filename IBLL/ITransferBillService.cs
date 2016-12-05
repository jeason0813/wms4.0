using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface ITransferBillService : IBaseService<TransferBill>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(TransferBill bill);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <returns></returns>
        bool Examine(Guid TransferBillId,string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid TransferBillId, string UserName);
    }
}
