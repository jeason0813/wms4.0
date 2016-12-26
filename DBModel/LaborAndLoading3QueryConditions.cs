using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
  public  class LaborAndLoading3QueryConditions
    {
        /// <summary>
        /// 装卸类型和业务类型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>
        public int Id { get; set; }
        public bool ticked { get; set; }
    }
}
