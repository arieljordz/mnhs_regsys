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
    public class FileMaintenanceController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        //
        // GET: /FileMaintenance/

        public ActionResult EncodedDataForm()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            //var AccessType = Session["AccessType"];
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/FileMaintenance/EDNoAccessforView.cshtml");
            }
        }
        public ActionResult SchoolYearForm()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            //var AccessType = Session["AccessType"];
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/FileMaintenance/SYNoAccessforView.cshtml");
            }
        }
        public ActionResult GradeLevelForm()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            //var AccessType = Session["AccessType"];
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/FileMaintenance/GFNoAccessforView.cshtml");
            }
        }
        public ActionResult ReligionForm()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            //var AccessType = Session["AccessType"];
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/FileMaintenance/RFNoAccessforView.cshtml");
            }
        }
        public ActionResult EthnicityForm()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            //var AccessType = Session["AccessType"];
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                return View();
            }
            else
            {
                return View("~/Views/FileMaintenance/ETFNoAccessforView.cshtml");
            }
        }

        public JsonResult fnSaveEncodedData(tbl_EncodedData tblEncodedData)
        {
            try
            {
                var qry = db.tbl_EncodedData.SingleOrDefault();
                if (qry == null)
                {
                    db.tbl_EncodedData.AddObject(tblEncodedData);
                    db.SaveChanges();
                    return Json(new { success = true }); 
                }
                else
                {
                    return Json(new { success = false }); 
                }
           
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadEncodedDataTable()
        {
            try
            {
                var ListEncodedData = db.tbl_EncodedData.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListEncodedData)
                {
                    var obj = new
                    {
                        EncodedDataID = item.EncodedDataID,
                        NoOfTeacher = item.NoOfTeacher,
                        NoOfClassRoom = item.NoOfClassRoom,
                        NoOfComfortRoom = item.NoOfComfortRoom,
                        NoOfComputer = item.NoOfComputer,
                        NoOfSeat = item.NoOfSeat,
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

        public JsonResult fnRetrieveEncodedData(int EncodedDataID)
        {
            try
            {
                var qry = db.tbl_EncodedData.Where(a => a.EncodedDataID == EncodedDataID).SingleOrDefault();
                return Json(new
                {
                    success = true,
                    NoOfTeacher = qry.NoOfTeacher,
                    NoOfClassRoom = qry.NoOfClassRoom,
                    NoOfComfortRoom = qry.NoOfComfortRoom,
                    NoOfComputer = qry.NoOfComputer,
                    NoOfSeat = qry.NoOfSeat
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public JsonResult fnUpdateEncodedData(tbl_EncodedData tblEncodedData, int EncodedDataID)
        {
            try
            {
                var EncodedData = db.tbl_EncodedData.Where(a => a.EncodedDataID == EncodedDataID).SingleOrDefault();
                EncodedData.NoOfTeacher = tblEncodedData.NoOfTeacher;
                EncodedData.NoOfClassRoom = tblEncodedData.NoOfClassRoom;
                EncodedData.NoOfComfortRoom = tblEncodedData.NoOfComfortRoom;
                EncodedData.NoOfComputer = tblEncodedData.NoOfComputer;
                EncodedData.NoOfSeat = tblEncodedData.NoOfSeat;
                db.SaveChanges();

                return Json(new { success = true, EncodedDataID = tblEncodedData.EncodedDataID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnDeleteEncodedData(int EncodedDataID)
        {
            var qry = db.tbl_EncodedData.Where(a => a.EncodedDataID == EncodedDataID).SingleOrDefault();
            db.tbl_EncodedData.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////////

        public JsonResult fnSaveGradeLevel(tbl_GradeLevel tblGradeLevel)
        {
            try
            {
                db.tbl_GradeLevel.AddObject(tblGradeLevel);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadGradeLevelsTable()
        {
            try
            {
                var ListGradeLevels = db.tbl_GradeLevel.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListGradeLevels)
                {
                    var obj = new
                    {
                        GradeLevelID = item.GradeLevelID,
                        Description = item.Description,
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

        public JsonResult fnDeleteGradeLevel(int GradeLevelID)
        {
            var qry = db.tbl_GradeLevel.Where(a => a.GradeLevelID == GradeLevelID).SingleOrDefault();
            db.tbl_GradeLevel.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////////

        public JsonResult fnSaveSchoolYear(tbl_SchoolYear tblSchoolYear)
        {
            try
            {
                var SchoolYear = tblSchoolYear.StartYear + "-" + tblSchoolYear.EndYear;
                tblSchoolYear.IsActive = tblSchoolYear.IsActive == null ? false : true;
                tblSchoolYear.SchoolYear = SchoolYear;
                db.tbl_SchoolYear.AddObject(tblSchoolYear);
                db.SaveChanges();

                tbl_PrevSchoolYear tblPrevSY = new tbl_PrevSchoolYear();

                var Prevqry = db.tbl_PrevSchoolYear.Where(a => a.SchoolYear == SchoolYear).SingleOrDefault();
                if (Prevqry == null)
                {
                    tblPrevSY.SchoolYear = SchoolYear;
                    db.tbl_PrevSchoolYear.AddObject(tblPrevSY);
                    db.SaveChanges(); 
                }


                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadSchoolYearsTable()
        {
            try
            {
                var ListSchoolYears = db.tbl_SchoolYear.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListSchoolYears)
                {
                    var obj = new
                    {
                        SchoolYearID = item.SY_ID,
                        IsActive = item.IsActive,
                        SchoolYear = item.SchoolYear,
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

        public JsonResult fnDeleteSchoolYear(int SchoolYearID)
        {
            var qry = db.tbl_SchoolYear.Where(a => a.SY_ID == SchoolYearID).SingleOrDefault();
            db.tbl_SchoolYear.DeleteObject(qry);
            db.SaveChanges();

            var Prevqry = db.tbl_PrevSchoolYear.Where(a => a.SchoolYear == qry.SchoolYear).SingleOrDefault();
            db.tbl_PrevSchoolYear.DeleteObject(Prevqry);
            db.SaveChanges();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxUpdateActiveSY(int SY_ID)
        {
            var result = new JsonResult();
            try
            {
                var data = db.tbl_SchoolYear.Where(a => a.SY_ID == SY_ID).SingleOrDefault();
                var qryy = db.tbl_SchoolYear.Where(a => a.SY_ID != SY_ID).ToList();
                foreach (var item in qryy)
                {
                    if (item.IsActive == true)
                    {
                        item.IsActive = false;
                    }
                }
                data.IsActive = true;
                db.SaveChanges();

                result.Data = new { success = true };
            }
            catch (Exception)
            {
                result.Data = new { success = false };
            }
            return Json(result);
        }
        //////////////////////////////////////////////////////////////////////

        public JsonResult fnSaveEthnicity(tbl_Ethnicity tblEthnicity)
        {
            try
            {
                db.tbl_Ethnicity.AddObject(tblEthnicity);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadEthnicityTable()
        {
            try
            {
                var ListEthnicitys = db.tbl_Ethnicity.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListEthnicitys)
                {
                    var obj = new
                    {
                        EthnicityID = item.EthnicityID,
                        Description = item.Description,
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

        public JsonResult fnDeleteEthnicity(int EthnicityID)
        {
            var qry = db.tbl_Ethnicity.Where(a => a.EthnicityID == EthnicityID).SingleOrDefault();
            db.tbl_Ethnicity.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////////

        public JsonResult fnSaveReligion(tbl_Religion tblReligion)
        {
            try
            {
                db.tbl_Religion.AddObject(tblReligion);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadReligionTable()
        {
            try
            {
                var ListReligions = db.tbl_Religion.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListReligions)
                {
                    var obj = new
                    {
                        ReligionID = item.ReligionID,
                        Description = item.Description,
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

        public JsonResult fnDeleteReligion(int ReligionID)
        {
            var qry = db.tbl_Religion.Where(a => a.ReligionID == ReligionID).SingleOrDefault();
            db.tbl_Religion.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////////

    }
}
