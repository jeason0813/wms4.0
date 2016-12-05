using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LHYS.WMS.Models.Home
{
    public class MenuAShow
    {
        public int Id { get; set; }
        public string text { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public string Url { get; set; }
        public List<MenuB> nodes { get; set; }
    }
}