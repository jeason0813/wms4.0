using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
   public partial interface IDIYBillService:IBaseService<DIYBill>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(DIYBill bill, List<Record> recordlist1, List<Record> recordlist2);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="DIYBillId"></param>
        /// <returns></returns>
        string Examine(Guid DIYBillId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="DIYBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid DIYBillId, string UserName);
    }
}
