using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class RecordService : BaseService<Record>, IRecordService
    {
        /// <summary>
        /// 查找出入库记录
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="WarehouseId"></param>
        /// <param name="ItemLocationId"></param>
        /// <param name="ItemCode"></param>
        /// <param name="InOutTypeId"></param>
        /// <param name="timestart"></param>
        /// <param name="timeend"></param>
        /// <returns></returns>
        public IQueryable<InOutRecordDetail> RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, DateTime? timestart, DateTime? timeend,int PageIndex,int PageSize,out int totalCount)
        {
            var list = CurrentDBSession.InOutRecordDetailDal.LoadEntities(a => a.State == 2 && a.DepartmentId == DepartmentId);
            //var list = CurrentDal.LoadEntities(a => a.State == 2 && a.DepartmentId == DepartmentId);
            if (WarehouseId != null)
            {
                list = list.Where(a => a.WarehouseId == WarehouseId);
            }
            if (!string.IsNullOrEmpty(ItemLocationId))
            {
                list = list.Where(a => a.ItemLocationId == ItemLocationId);
            }
            if (!string.IsNullOrEmpty(ItemCode))
            {

                list = list.Where(a => a.ItemCode == ItemCode);
            }
            if (InOutTypeId != null && InOutTypeId.Length != 0)
            {
                list = list.Where(a => InOutTypeId.Contains(a.InOutTypeId));
            }
            if (timestart != null)
            {
                list = list.Where(a => a.ExamineDate >= timestart);
            }
            if (timeend != null)
            {
                list = list.Where(a => a.ExamineDate <= timeend);
            }
            totalCount = list.Count();
            list = list.OrderByDescending(a => a.ExamineDate).Skip((PageIndex-1)*PageSize).Take(PageSize);//按审核时间排序
            return list;
        }
    }
}
