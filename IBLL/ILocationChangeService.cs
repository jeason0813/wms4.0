using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
   public partial interface ILocationChangeService : IBaseService<LocationChange>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(LocationChange bill,List<LocationChangeRecordView> LocationChangeRecordView);
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
        string Examine(Guid LocationChangeId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="TransferBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid LocationChangeId, string UserName);
    }
}
