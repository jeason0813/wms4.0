using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
    public partial interface IBackOutputService : IBaseService<BackOutput>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(BackOutput bill);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="OtherOutputId"></param>
        /// <returns></returns>
        string Examine(Guid BackOutputId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="BackOutputId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid BackOutputId, string UserName);
    }
}
