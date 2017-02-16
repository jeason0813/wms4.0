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
        public IQueryable<InOutRecordDetail> RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, string[] BillTypes, string ItemBatch, string LBBillCode, string BillCode, DateTime? timestart, DateTime? timeend,int PageIndex,int PageSize,out int totalCount,out double? sumCount,out double? sumWeight)
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
            if (BillTypes != null && BillTypes.Length != 0)
            {
                list = list.Where(a => BillTypes.Contains(a.MainTableType));
            }
            if (ItemBatch != null)
            {
                list = list.Where(a => a.ItemBatch == ItemBatch);
            }
            if (LBBillCode != null)
            {
                list = list.Where(a => a.LBBillCode == LBBillCode);
            }
            if (BillCode != null)
            {
                list = list.Where(a => a.BillCode == BillCode);
            }
            if (timestart != null)
            {
                list = list.Where(a => a.ExamineDate >= timestart);
            }
            if (timeend != null)
            {
                list = list.Where(a => a.ExamineDate < timeend);
            }
            totalCount = list.Count();
            //计算总数量 总质量
            sumCount =list.Sum(a => a.Count);
            sumWeight = list.Sum(a => a.Weight);
            list = list.OrderByDescending(a => a.ExamineDate).Skip((PageIndex-1)*PageSize).Take(PageSize);//按审核时间排序
            return list;
        }
        public object TotalSearch(string DepartmentId, int? WarehouseId, DateTime? timestart, DateTime? timeend, int PageIndex, int PageSize, out int totalCount, out double? sumCountIn, out double? sumWeightIn, out double? sumCountOut, out double? sumWeightOut)
        {
            var list = CurrentDBSession.InOutRecordDetailDal.LoadEntities(a => a.State == 2 && a.DepartmentId == DepartmentId);
          
            if (WarehouseId != null)
            {
                list = list.Where(a => a.WarehouseId == WarehouseId);
            }
            if (timestart != null)
            {
                list = list.Where(a => a.ExamineDate >= timestart);
            }
            if (timeend != null)
            {
                list = list.Where(a => a.ExamineDate < timeend);
            }
            var ListNew=list.Select(a => new { a.Count, a.ExamineDate, a.Weight ,a.InOrOut}).ToList().Select(a=>new { a.Count, ExamineDate =a.ExamineDate.Value.ToString("yyyy-MM-dd"),a.Weight,a.InOrOut});
            
            //计算总数量 总质量
            sumCountIn = ListNew.Where(a => a.InOrOut == 1).Sum(a => a.Count);
            sumWeightIn = ListNew.Where(a => a.InOrOut == 1).Sum(a => a.Weight);
            sumCountOut = ListNew.Where(a => a.InOrOut == 0).Sum(a => a.Count);
            sumWeightOut = ListNew.Where(a => a.InOrOut == 0).Sum(a => a.Weight);
            //按日期汇总
            var res = from a in ListNew
                      group a by a.ExamineDate into g
                      select new
                      {
                          Date= g.Key,
                          CountIn=g.Where(b => b.InOrOut == 1).Sum(b=>b.Count),
                          WeightIn=g.Where(b=>b.InOrOut==1).Sum(b=>b.Weight),
                          CountOut = g.Where(b=>b.InOrOut==0).Sum(b => b.Count),
                          WeightOut = g.Where(b => b.InOrOut == 0).Sum(b => b.Weight)
                      };
            totalCount = res.Count();
            res = res.OrderByDescending(a=>a.Date).Skip((PageIndex - 1) * PageSize).Take(PageSize);
            return res;
        }
    }
}
