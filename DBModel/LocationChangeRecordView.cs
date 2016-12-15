using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public class LocationChangeRecordView:Record
    {
        /// <summary>
        /// 调整后货位
        /// </summary>
        public string LocationChangeAfter { get; set; }
        /// <summary>
        /// 调整后货位id
        /// </summary>
        public string LocationChangeAfterId { get; set; }
        /// <summary>
        /// 调整前货位
        /// </summary>
        public string LocationChangeBefore { get; set; }
        /// <summary>
        /// 调整前货位id
        /// </summary>
        public string LocationChangeBeforeId { get; set; }
        /// <summary>
        /// 调整后仓库
        /// </summary>
        public string WarehouseAfter { get; set; }
        /// <summary>
        /// 调整后仓库id
        /// </summary>
        public int? WarehouseAfterId { get; set; }
        /// <summary>
        /// 调整前仓库
        /// </summary>
        public string WarehouseBefore { get; set; }
        /// <summary>
        /// 调整前仓库
        /// </summary>
        public int? WarehouseBeforeId { get; set; }

    }
}
