using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
   public partial interface IOtherInputService : IBaseService<OtherInput>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(OtherInput bill);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="OtherInputId"></param>
        /// <returns></returns>
        string Examine(Guid OtherInputId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid TransferBillId, string UserName);
    }
}
