using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public partial interface IRecordService : IBaseService<Record>
    {
        IQueryable<Record> RecordSearch(string DepartmentId, int? WarehouseId, string ItemLocationId, string ItemCode, int?[] InOutTypeId, DateTime? timestart, DateTime? timeend);
    }
}
