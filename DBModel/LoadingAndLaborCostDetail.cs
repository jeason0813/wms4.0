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
    
    public partial class LoadingAndLaborCostDetail
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> MainTableId { get; set; }
        public Nullable<int> CostItemId { get; set; }
        public string CostItemName { get; set; }
        public string ListType { get; set; }
        public string ListCode { get; set; }
        public Nullable<System.DateTime> ExaminaDate { get; set; }
        public string Company { get; set; }
        public string CompanyId { get; set; }
        public string Department { get; set; }
        public string DepartmentId { get; set; }
        public string Warehouse { get; set; }
        public Nullable<int> WarehouseId { get; set; }
        public string BusinessTypeName { get; set; }
        public Nullable<int> BusinessTypeId { get; set; }
        public string LoadingTypeName { get; set; }
        public Nullable<int> LoadingTypeId { get; set; }
        public string ItemCode { get; set; }
        public string ItemLine { get; set; }
        public string ItemName { get; set; }
        public string ItemSpecifications { get; set; }
        public string ItemUnit { get; set; }
        public string ItemBatch { get; set; }
        public Nullable<double> UnitWeight { get; set; }
        public Nullable<double> Count { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> AllAmount { get; set; }
        public string Remark { get; set; }
        public Nullable<int> BillState { get; set; }
        public Nullable<int> BillType { get; set; }
        public Nullable<double> CurrentCount { get; set; }
        public Nullable<double> LaborCost { get; set; }
    }
}