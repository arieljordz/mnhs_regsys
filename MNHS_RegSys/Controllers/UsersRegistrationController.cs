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

namespace MNHS_RegSys.Controllers
{
    public class UsersRegistrationController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        //
        // GET: /UsersRegistration/

        public ActionResult UsersRegistration()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/UsersRegistration/URNoAccessforView.cshtml");
            }
        }

        public JsonResult fnSaveUser(tbl_User tblUser)
        {
            try
            {
                tblUser.DateCreated = DateTime.Now.Date;
                db.tbl_User.AddObject(tblUser);
                db.SaveChanges();
                return Json(new { success = true, UserID = tblUser.UserID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadUsersTable()
        {
            try
            {
                var ListUsers = db.tbl_User.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListUsers)
                {
                    string pass = "";
                    for (int i = 0; i < item.Password.Length; i++)
                    {
                        pass += ".";
                    }
                    var obj = new
                    {
                        UserID = item.UserID,
                        CompleteName = item.CompleteName,
                        UserName = item.UserName,
                        Password = pass,
                        AccessType = item.AccessType,
                        DateCreated = item.DateCreated.Value.ToShortDateString(),
                    };
                    objList.Add(obj);

                }
                return Json(new { data = objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message });
            }

        }

        public JsonResult fnDeleteUser(int UserID)
        {
            var qry = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault();
            db.tbl_User.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult fnRetrieveUser(int UserID)
        {
            try
            {
                var qry = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault();
                return Json(new
                {
                    success = true,
                    CompleteName = qry.CompleteName,
                    UserName = qry.UserName,
                    Password = qry.Password,
                    AccessType = qry.AccessType,
                    DateCreated = qry.DateCreated.Value.ToShortDateString()
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public JsonResult fnUpdateUser(tbl_User tblUser, int UserID)
        {
            try
            {
                var User = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault();
                User.CompleteName = tblUser.CompleteName;
                User.UserName = tblUser.UserName;
                User.Password = tblUser.Password;
                User.AccessType = tblUser.AccessType;
                db.SaveChanges();

                return Json(new { success = true, UserID = tblUser.UserID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

    }
}
