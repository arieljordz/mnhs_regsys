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
    public class StudentRegistrationController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();

        public ActionResult StudentRegistration()
        {
            LoadDropdowns();
            return View();
        }

        public ActionResult LoadDropdowns()
        {
            var Suffix = db.tbl_Suffix;
            ViewBag.cmbSuffix = new SelectList(Suffix, "SuffixID", "Description");

            var Gender = db.tbl_Gender;
            ViewBag.cmbGender = new SelectList(Gender, "GenderID", "Description");

            var Relation = db.tbl_Relation;
            ViewBag.cmbRelation = new SelectList(Relation, "RelationID", "Description");

            var Specialization = db.tbl_Specialization;
            ViewBag.cmbSpecialization = new SelectList(Specialization, "SpecID", "Description");

            var GradeLevel = db.tbl_GradeLevel;
            ViewBag.cmbGradeLevel = new SelectList(GradeLevel, "GradeLevelID", "Description");

            var SchoolYear = db.tbl_SchoolYear.Where(a => a.IsActive == true);
            ViewBag.cmbSchoolYear = new SelectList(SchoolYear, "SY_ID", "SchoolYear");

            var Religion = db.tbl_Religion;
            ViewBag.cmbReligion = new SelectList(Religion, "ReligionID", "Description");

            var Ethnicity = db.tbl_Ethnicity;
            ViewBag.cmbEthnicity = new SelectList(Ethnicity, "EthnicityID", "Description");

            var Track = db.tbl_Track.OrderBy(a => a.Description);
            ViewBag.cmbTrack = new SelectList(Track, "TrackID", "Description");

            var Strand = db.tbl_Strand.OrderBy(a => a.Description);
            ViewBag.cmbStrand = new SelectList(Strand, "StrandID", "Description");

            var AcadStatus = db.tbl_AcademicStatus;
            ViewBag.cmbAcadStatus = new SelectList(AcadStatus, "AcadStatusID", "Description");

            var PrevSchool = db.tbl_PrevSchool;
            ViewBag.cmbPrevSchool = new SelectList(PrevSchool, "PrevSchoolID", "Description");

            var PrevSY = db.tbl_PrevSchoolYear.OrderByDescending(a => a.SchoolYear);
            ViewBag.cmbPrevSY = new SelectList(PrevSY, "PrevSYGraduatedID", "SchoolYear");

            return View();
        }

        public JsonResult fnSaveStudent(tbl_Student tblStudent, int Age)
        {
            try
            {
                int UserID = Convert.ToInt32(Session["UserID"]);
                tblStudent.CompleteAddress = tblStudent.Street + ", " + tblStudent.Barangay + ", " + tblStudent.Mun_City + ", " + tblStudent.Province;
                tblStudent.CompleteName = tblStudent.LastName + ", " + tblStudent.FirstName + " " + tblStudent.MiddleName.ToCharArray()[0].ToString() + "." + " " + tblStudent.SuffixID;
                tblStudent.IsCityAssistant = tblStudent.IsCityAssistant == null ? false : true;
                tblStudent.Is4Ps = tblStudent.Is4Ps == null ? false : true;
                tblStudent.IsProvisionary = tblStudent.IsProvisionary == null ? false : true;
                tblStudent.UserID = UserID;
                tblStudent.Age = Age;
                tblStudent.IsRegistered = true;
                tblStudent.DateRegistered = DateTime.Now.Date;
                db.tbl_Student.AddObject(tblStudent);
                db.SaveChanges();

                return Json(new { success = true, UserID = tblStudent.StudentID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public JsonResult fnLoadStudentTable()
        {
            try
            {
                var ListStudents = db.tbl_Student.ToList();
                List<object> objList = new List<object>();
                foreach (var item in ListStudents)
                {
                    var Strand = "";
                    var Specialization = "";
                    if (item.StrandID == null)
                    {
                        Strand = "";
                    }
                    else
                    {
                        Strand = db.tbl_Strand.Where(x => x.StrandID == item.StrandID).FirstOrDefault().Description;
                    }
                    if (item.SpecID == null)
                    {
                        Specialization = "";
                    }
                    else
                    {
                        Specialization = db.tbl_Specialization.Where(x => x.SpecID == item.SpecID).FirstOrDefault().Description;
                    }

                    var obj = new
                    {
                        StudentID = item.StudentID,
                        GradeLevel = db.tbl_GradeLevel.Where(x => x.GradeLevelID == item.GradeLevelID).FirstOrDefault().Description,
                        //GradeLevel = item.GradeLevelID == 1 ? "11" : "12",
                        SchoolYear = db.tbl_SchoolYear.Where(x => x.SY_ID == item.SY_ID).FirstOrDefault().SchoolYear,
                        UserID = item.UserID,
                        Specialization = Specialization,
                        Street = item.Street,
                        Barangay = item.Barangay,
                        Mun_City = item.Mun_City,
                        Province = item.Province,
                        CompleteAddress = item.CompleteAddress,
                        LRN = item.LRN,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        CompleteName = item.CompleteName,
                        SuffixID = item.SuffixID,
                        BirthDate = item.BirthDate.Value.ToShortDateString(),
                        Age = item.Age,
                        Gender = db.tbl_Gender.Where(x => x.GenderID == item.GenderID).FirstOrDefault().Description,
                        FathersName = item.FathersName,
                        MothersName = item.MothersName,
                        Guardian = item.Guardian,
                        MobileNo = item.MobileNo,
                        Relation = db.tbl_Relation.Where(x => x.RelationID == item.RelationID).FirstOrDefault().Description,
                        Religion = db.tbl_Religion.Where(x => x.ReligionID == item.ReligionID).FirstOrDefault().Description,
                        Ethnicity = db.tbl_Ethnicity.Where(x => x.EthnicityID == item.EthnicityID).FirstOrDefault().Description,
                        IsCityAssistant = item.IsCityAssistant,
                        Is4Ps = item.Is4Ps,
                        Track = db.tbl_Track.Where(x => x.TrackID == item.TrackID).FirstOrDefault().Description,
                        Strand = Strand,
                        AcadStatus = db.tbl_AcademicStatus.Where(x => x.AcadStatusID == item.AcadStatusID).FirstOrDefault().Description,
                        IsProvisionary = item.IsProvisionary,
                        PrevSchool = db.tbl_PrevSchool.Where(x => x.PrevSchoolID == item.PrevSchoolID).FirstOrDefault().Description,
                        PrevSchoolName = item.PrevSchoolName,
                        PrevSchoolAddress = item.PrevSchoolAddress,
                        PrevSYGraduated = db.tbl_PrevSchoolYear.Where(x => x.PrevSYGraduatedID == item.PrevSYGraduatedID).FirstOrDefault().SchoolYear,
                        IsRegistered = item.IsRegistered,
                        DateRegistered = item.DateRegistered.Value.ToShortDateString(),
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

        public JsonResult fnDeleteStudent(int StudentID)
        {
            var qry = db.tbl_Student.Where(a => a.StudentID == StudentID).SingleOrDefault();
            db.tbl_Student.DeleteObject(qry);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult fnRetrieveStudent(int StudentID)
        {
            try
            {

                var qry = db.tbl_Student.Where(a => a.StudentID == StudentID).SingleOrDefault();
                return Json(new
                {
                    success = true,
                    LRN = qry.LRN,
                    LastName = qry.LastName,
                    FirstName = qry.FirstName,
                    MiddleName = qry.MiddleName,
                    SuffixID = qry.SuffixID,
                    BirthDate = qry.BirthDate.Value.ToShortDateString(),
                    Age = qry.Age,
                    GenderID = qry.GenderID,
                    FathersName = qry.FathersName,
                    MothersName = qry.MothersName,
                    Guardian = qry.Guardian,
                    MobileNo = qry.MobileNo,
                    RelationID = qry.RelationID,
                    Street = qry.Street,
                    Barangay = qry.Barangay,
                    Mun_City = qry.Mun_City,
                    Province = qry.Province,
                    CompleteAddress = qry.CompleteAddress,
                    SpecID = qry.SpecID,
                    GradeLevelID = qry.GradeLevelID,
                    SY_ID = db.tbl_SchoolYear.Where(a => a.IsActive == true).SingleOrDefault().SY_ID,
                    ReligionID = qry.ReligionID,
                    EthnicityID = qry.EthnicityID,
                    IsCityAssistance = qry.IsCityAssistant,
                    Is4Ps = qry.Is4Ps,
                    TrackID = qry.TrackID,
                    StrandID = qry.StrandID,
                    AcadStatusID = qry.AcadStatusID,
                    IsProvisionary = qry.IsProvisionary,
                    PrevSchoolID = qry.PrevSchoolID,
                    PrevSchoolName = qry.PrevSchoolName,
                    PrevSchoolAddress = qry.PrevSchoolAddress,
                    PrevSYGraduatedID = qry.PrevSYGraduatedID
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public JsonResult fnUpdateStudent(tbl_Student tblStudent, int StudentID)
        {
            try
            {
                int UserID = Convert.ToInt32(Session["UserID"]);

                var Student = db.tbl_Student.Where(a => a.StudentID == StudentID).SingleOrDefault();

                Student.LRN = tblStudent.LRN;
                Student.LastName = tblStudent.LastName;
                Student.FirstName = tblStudent.FirstName;
                Student.MiddleName = tblStudent.MiddleName;
                Student.SuffixID = tblStudent.SuffixID;
                Student.BirthDate = tblStudent.BirthDate;
                Student.Age = tblStudent.Age;
                Student.Street = tblStudent.Street;
                Student.Barangay = tblStudent.Barangay;
                Student.Mun_City = tblStudent.Mun_City;
                Student.Province = tblStudent.Province;
                Student.CompleteAddress = Student.Street + ", " + Student.Barangay + ", " + Student.Mun_City + ", " + Student.Province;
                Student.GenderID = tblStudent.GenderID;
                Student.FathersName = tblStudent.FathersName;
                Student.MothersName = tblStudent.MothersName;
                Student.Guardian = tblStudent.Guardian;
                Student.MobileNo = tblStudent.MobileNo;
                Student.RelationID = tblStudent.RelationID;
                Student.SpecID = tblStudent.SpecID;
                Student.GradeLevelID = tblStudent.GradeLevelID;
                Student.SY_ID = tblStudent.SY_ID;
                Student.ReligionID = tblStudent.ReligionID;
                Student.EthnicityID = tblStudent.EthnicityID;
                Student.IsCityAssistant = tblStudent.IsCityAssistant;
                Student.Is4Ps = tblStudent.Is4Ps;
                Student.TrackID = tblStudent.TrackID;
                Student.StrandID = tblStudent.StrandID;
                Student.AcadStatusID = tblStudent.AcadStatusID;
                Student.IsProvisionary = tblStudent.IsProvisionary;
                Student.PrevSchoolID = tblStudent.PrevSchoolID;
                Student.PrevSchoolName = tblStudent.PrevSchoolName;
                Student.PrevSchoolAddress = tblStudent.PrevSchoolAddress;
                Student.PrevSYGraduatedID = tblStudent.PrevSYGraduatedID;
                tblStudent.UserID = tblStudent.UserID;
                db.SaveChanges();

                return Json(new { success = true, UserID = tblStudent.StudentID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

    }//end
}
