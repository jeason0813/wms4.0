using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
   public class LoadingAndLaborQueryView
    {
        public bool TransferBill { get; set; }
        public bool BackInput { get; set; }
        public bool GiveBill { get; set; }
        public bool BackOutput { get; set; }
        public bool GiveBackBill { get; set; }
        public string Department2Id { get; set; }
        public string BusinessType { get; set; }
        public string Warehouse2Id { get; set; }
        public string LoadGoodsType { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
    }
}
