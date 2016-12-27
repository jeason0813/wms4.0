

using BLL;
using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Models
{
    public class MyExecptionAttribute : HandleErrorAttribute, System.Web.SessionState.IReadOnlySessionState
    {
        // private static DBContainer db = new DBContainer();
        //重写这个虚方法  捕获异常
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);//执行原来的错误处理步骤
            ////获取异常信息
            Exception ex = filterContext.Exception;
            //DBModel.ErrorRecort err = new DBModel.ErrorRecort();
            //err.ErrorText = ex.ToString();
            //err.Time = DateTime.Now;
            //err.UserCode = HttpContext.Current.Session["UserCode"]!=null? HttpContext.Current.Session["UserCode"].ToString():"";
            //err.UserName = HttpContext.Current.Session["UserName"]!=null ? HttpContext.Current.Session["UserName"].ToString():"";
            //db.ErrorRecort.Add(err);
            //db.SaveChanges();

            string userCode = HttpContext.Current.Session["UserCode"] != null ? HttpContext.Current.Session["UserCode"].ToString() : "";
            string userName = HttpContext.Current.Session["UserName"] != null ? HttpContext.Current.Session["UserName"].ToString() : "";
            log4net.ILog log = log4net.LogManager.GetLogger("myLogger");
            log.Error(new Log4NetApply.LogContent(userCode,userName,DateTime.Now,ex.ToString()));
        }
    }
}