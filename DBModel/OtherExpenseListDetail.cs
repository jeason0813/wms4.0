//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OtherExpenseListDetail
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> MainTableId { get; set; }
        public Nullable<int> CostItemId { get; set; }
        public string CostItemName { get; set; }
        public Nullable<System.DateTime> HappenDate { get; set; }
        public string CostDescription { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RecorkType { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string CompanyId { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<System.DateTime> ExamineDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CostItemCode { get; set; }
    }
}
