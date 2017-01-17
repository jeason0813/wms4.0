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

    }
}
