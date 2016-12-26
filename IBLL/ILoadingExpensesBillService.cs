using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;
namespace IBLL
{
    public partial interface ILoadingExpensesBillService:IBaseService<LoadingExpensesBill>
    {
        /// <summary>
        /// 根据条件查询单据信息（转仓单、退仓单、交货单、退货单、交货回执单）
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        LoadingExpensesBill GetData(LoadingAndLaborQueryView A,int billtype,string power, List<LaborAndLoading3QueryConditions> Businesstype, List<LaborAndLoading3QueryConditions> LodingType, List<LaborAndLoading3QueryConditions> Warehouseid);
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        string SaveData(LoadingExpensesBill bill,int billType);
        /// <summary>
        /// 初审单数据
        /// </summary>
        /// <param name="LoadingExpensesBillId"></param>
        /// <returns></returns>
        string Examine1(Guid LoadingExpensesBillId, string UserName);
        /// <summary>
        /// 审核表单数据
        /// </summary>
        /// <param name="LoadingExpensesBillId"></param>
        /// <returns></returns>
        string Examine2(Guid LoadingExpensesBillId, string UserName);
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="LoadingExpensesBillId"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        string GiveupExamine(Guid LoadingExpensesBillId, string UserName);
    }
}
