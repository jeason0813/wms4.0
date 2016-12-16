using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
    public class MenuViewForMainPage
    {
        public int Id { get; set; }
        public int? pid { get; set; }
        public string text { get; set; }
        public List<MenuViewForMainPage> nodes { get; set; }
        public string Url { get; set; }
    }
}
