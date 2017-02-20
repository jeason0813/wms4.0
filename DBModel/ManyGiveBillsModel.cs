using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
    public class ManyGiveBillsModel
    {
       public string LBBillCode { get; set; }
        public string LBCustomerCode { get; set; }
        public string LBCustomerName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemBatch { get; set; }
        public double Count{get;set;}
        public double Weight { get; set; }
        public string ItemLocationId { get; set; }
        public string ItemLocation { get; set; }
        public string LBContacts { get; set; }
        public string LBPhone { get; set; }
        public string LBSendAddress { get; set; }
        public string LBMailCode { get; set; }
        public DateTime? LBBillDate{get;set;}
        public int WarehouseId { get; set; }
        public string Warehouse { get; set; }
        public string ChargePerson { get; set; }






    }
}
