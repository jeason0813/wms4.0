﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;
namespace IBLL
{
    public partial interface IOtherExpenseListService : IBaseService<OtherExpenseList>
    {
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(OtherExpenseList bill, int billType);
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="BillId"></param>
        /// <returns></returns>
        string DeleteBill(Guid BillId);
        /// <summary>
        /// 初审表单数据
        /// </summary>
        /// <param name="OtherExpenseListId"></param>
        /// <returns></returns>
        string Examine1(Guid OtherExpenseListId, string UserName);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="OtherExpenseListId"></param>
        /// <returns></returns>
        string Examine2(Guid OtherExpenseListId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="OtherExpenseListId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid OtherExpenseListId, string UserName);
    }
}
