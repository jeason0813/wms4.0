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
    
    public partial class OtherInput
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OtherInput()
        {
            this.Record = new HashSet<Record>();
        }
    
        public System.Guid Id { get; set; }
        public string BillCode { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ChargePerson { get; set; }
        public string ExaminePerson { get; set; }
        public Nullable<System.DateTime> ExamineDate { get; set; }
        public string MakePerson { get; set; }
        public string Warehouse { get; set; }
        public Nullable<int> WarehouseId { get; set; }
        public string InputType { get; set; }
        public string BusinessType { get; set; }
        public string LineWay { get; set; }
        public string Remark { get; set; }
        public Nullable<int> BillState { get; set; }
        public string Department { get; set; }
        public string DepartmentId { get; set; }
        public string Company { get; set; }
        public string CompanyId { get; set; }
        public Nullable<int> InputTypeId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Record { get; set; }
    }
}
