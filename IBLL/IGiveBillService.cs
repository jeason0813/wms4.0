using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
    public partial interface IGiveBillService : IBaseService<GiveBill>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(GiveBill bill);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="OtherOutputId"></param>
        /// <returns></returns>
        string Examine(Guid GiveBillId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid GiveBillId, string UserName);
    }
}
