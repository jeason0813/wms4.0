using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
    public partial interface IBackInputService : IBaseService<BackInput>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(BackInput bill);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="BackInputId"></param>
        /// <returns></returns>
        string Examine(Guid BackInputId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="BackInputId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid BackInputId, string UserName);
    }
}
