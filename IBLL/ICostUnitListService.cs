using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel;
namespace IBLL
{
    public partial interface ICostUnitListService: IBaseService<CostUnitList>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(CostUnitList bill, int billType);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="CostUnitListId"></param>
        /// <returns></returns>
        string Examine(Guid CostUnitListId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="CostUnitListId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid CostUnitListId, string UserName);
    }
}
