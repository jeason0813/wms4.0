using LHYS.WMS.Models;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExecptionAttribute());//注册异常处理类
        }
    }
}