using Common;
using DBModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LHYS.WMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private IUserService UserService { get; set; }
        private IUserFrameworkService UserFrameworkService { get; set; }
        private IMenuService MenuService { get; set; }
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登录提交
        /// </summary>
        /// <returns></returns>
        public string Login()
        {
            string uid = Request.Form["uid"];
            string pwd = Request.Form["pwd"];
            pwd = SecurityEncrypt.EncryptMD5(pwd);
            string code = Request.Form["checkcode"];
            if (string.IsNullOrEmpty(code) || (Session["vCode"] == null) || (code.Trim() != Session["vCode"].ToString()))//校验验证码是否正确
            {
                return "4";//验证码错误
            }
            DBModel.User user = UserService.LoadEntities(u => u.UserCode == uid).FirstOrDefault();
            //验证用户名密码判断
            if (user == null)
            {
                return "1";
            }
            if (user.UserPassword != pwd)
            {
                return "2";
            }
            //保存Session
            Session["UserCode"] = user.UserCode;
            Session["UserName"] = user.UserName;
            //获取权限
            List<UserFramework> list = UserFrameworkService.LoadEntities(u => u.UserCode == user.UserCode).ToList();
            string powerStr = "";
            for (int i = 0; i < list.Count; i++)
            {
                powerStr += list[i].DeptCode + ",";
            }
            Session["Power"] = list.Count > 0 ? powerStr.Substring(0, powerStr.Length - 1) : powerStr;
            //获取按钮权限
            var buttons = MenuService.GetButtonPower(user.UserCode).ToList();
            if (buttons.Count == 0)
            {
                Session["btnPower"] = "[]";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < buttons.Count; i++)
                {
                    sb.Append(buttons[i]);
                    sb.Append(",");
                }
                string temp = sb.ToString();
                temp = "["+temp.Substring(0, temp.Length - 1)+"]";
                Session["btnPower"] = temp;
            }
            return "0";//登录成功
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVcode()
        {
            var code = VerifyCode.GenerateCheckCode();
            Session["vCode"] = code;
            var byt = VerifyCode.CreateCheckCodeImage(code);
            return File(byt, "image/jpeg");
        }
        /// <summary>
        /// 退出登录 清空Session
        /// </summary>
        /// <returns></returns>
        public ActionResult Quit()
        {
            Session["UserCode"] = null;
            Session["UserName"] = null;
            return Content("ok");
        }
    }
}
