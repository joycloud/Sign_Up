using Sign_Up.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sign_Up.Controllers
{
    public class AccountController : Controller
    {
        TESTEntities db = new TESTEntities();
        public ActionResult Index()
        {
            return View(new UserDataModel());
        }

        [HttpPost]
        public ActionResult Index(UserDataModel data)
        {
            if (string.IsNullOrWhiteSpace(data.password1) || data.password1 != data.password2)
            {
                ViewBag.Msg = "密碼輸入錯誤";
                return View(data);
            }
            else
            {
                if (CreateUSER(data))
                {
                    Response.Redirect("~/Account/Login");
                    return new EmptyResult();
                }
                else
                {
                    ViewBag.Msg = "註冊失敗...";
                    return View(data);
                }
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection post)
        {
            string account = post["account"];
            string password = post["password"];

            //驗證帳號密碼
            if (new UserDataModel().Check_USER(account, password))
            {
                // Session預設有效時間是20分鐘 
                Session["account"] = account;

                return RedirectToAction("Test", "Account");

                //FormsAuthentication.SetAuthCookie(account, false);//將用戶名放入Cookie中
                //System.Web.HttpContext.Current.Session["account"] = account; //將用戶名放入session中
                
                // 會重複要求再登一次
                //Response.Redirect("~/Account/Test");
                //return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = "登入失敗...";
                return View();
            }
        }

        // 登出，清除session
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["usrName"] = null;
            return RedirectToAction("Login");
        }

        public bool CreateUSER(UserDataModel data)
        {
            try
            {
                // save
                TUSER tuser = new TUSER();
                tuser.id = new UserDataModel().Find_id();
                tuser.account = data.account;
                tuser.password = data.password1;                

                new UserDataModel().Create_USER(tuser);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}