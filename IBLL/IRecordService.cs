using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IRecordService : IBaseService<Record>
    {
        IQueryable<InOutRecordDetail> RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, DateTime? timestart, DateTime? timeend,int PageIndex,int PageSize,out int totalCount);
    }
}
