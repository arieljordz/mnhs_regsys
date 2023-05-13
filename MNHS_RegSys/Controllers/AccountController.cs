using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MNHS_RegSys.Models;
using Newtonsoft.Json;


namespace MNHS_RegSys.Controllers
{
    public class AccountController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        DashboardRepository DR = new DashboardRepository();
        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut(int? UserID)
        {
            return Redirect(Url.Action("Login", "Account"));
        }

        public ActionResult AccountSignIn(string Username, string Password)
        {
            try
            {
            var result = new JsonResult();

            var AccessType = db.tbl_User.Where(a => a.UserName == Username && a.Password == Password).Select(x => x.AccessType).SingleOrDefault();
            var chkUser = db.tbl_User.Where(a => a.UserName.Contains(Username) && a.Password.Contains(Password)).Count();
            var UserID = db.tbl_User.Where(a => a.UserName.Contains(Username) && a.Password.Contains(Password)).Select(x => x.UserID).SingleOrDefault();
            var CompleteName = db.tbl_User.Where(a => a.UserName.Contains(Username) && a.Password.Contains(Password)).Select(x => x.CompleteName).SingleOrDefault();
            if (chkUser != 0 && AccessType == "Admin")
            {
                result.Data = new { success = true, UserID = UserID, AccessType= AccessType,redirect = Url.Action("Dashboard", "Dashboard") };
                Session["UserID"] = UserID;
                Session["CompleteName"] = CompleteName;
                Session["AccessType"] = AccessType;
                return Json(result);
            }
            else if (chkUser != 0 && AccessType == "User")
            {
                result.Data = new { success = true, UserID = UserID, AccessType= AccessType, redirect = Url.Action("StudentRegistration", "StudentRegistration") };
                Session["UserID"] = UserID;
                Session["CompleteName"] = CompleteName;
                Session["AccessType"] = AccessType;

                return Json(result);
            }
            else
            {
                result.Data = new { success = false, error = "Invalid Username/Password" };
                return Json(result);
            }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }


    }//
}
