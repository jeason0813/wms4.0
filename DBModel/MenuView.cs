using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
    /// <summary>
    /// 菜单视图类
    /// </summary>
    public  class MenuView
    {
        public int id { get; set; }
        public int? pid { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool expanded { get; set; }
        public string checkState { get; set; }
        public List<MenuView> items { get; set; }
        public string Url { get; set; }
        
    }
}
