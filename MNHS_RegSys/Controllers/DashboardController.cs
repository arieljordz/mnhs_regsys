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
    public class DashboardController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        DashboardRepository DR = new DashboardRepository();

        public ActionResult Dashboard()
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
                return View("~/Views/Dashboard/NoAccessforView.cshtml");
            }
        }

        public ActionResult GetStatistics()
        {
            var result = new JsonResult();
            var SchoolYr = db.tbl_SchoolYear.Where(a => a.IsActive == true).Select(x => x.SchoolYear).SingleOrDefault();
            var CountStudent = db.tbl_Student.Count();
            var GetItems = db.tbl_EncodedData.SingleOrDefault();

            double StudentAdviser = 0;
            double StudentClassRoom = 0;
            double StudentComfortRoom = 0;
            double StudentComputer = 0;
            double StudentSeat = 0;

            if (GetItems != null)
            {
                StudentAdviser = Math.Round(Convert.ToDouble(CountStudent) / Convert.ToDouble(GetItems.NoOfTeacher));
                StudentClassRoom = Math.Round(Convert.ToDouble(CountStudent) / Convert.ToDouble(GetItems.NoOfClassRoom));
                StudentComfortRoom = Math.Round(Convert.ToDouble(CountStudent) / Convert.ToDouble(GetItems.NoOfComfortRoom));
                StudentComputer = Math.Round(Convert.ToDouble(GetItems.NoOfComputer) / Convert.ToDouble(CountStudent));
                StudentSeat = Math.Round(Convert.ToDouble(GetItems.NoOfSeat) / Convert.ToDouble(CountStudent));
            }

            var IsAssistanceMale = db.tbl_Student.Where(a => a.IsCityAssistant == true && a.GenderID == 1).Count();
            var IsAssistanceFemale = db.tbl_Student.Where(a => a.IsCityAssistant == true && a.GenderID == 2).Count();
            var Is4PsMale = db.tbl_Student.Where(a => a.Is4Ps == true && a.GenderID == 1).Count();
            var Is4PsFemale = db.tbl_Student.Where(a => a.Is4Ps == true && a.GenderID == 2).Count();
            var IsProvisinaryMale = db.tbl_Student.Where(a => a.IsProvisionary == true && a.GenderID == 1).Count();
            var IsProvisinaryFemale = db.tbl_Student.Where(a => a.IsProvisionary == true && a.GenderID == 2).Count();

            var Grade11 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 11")).SingleOrDefault().GradeLevelID;
            var Grade12 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 12")).SingleOrDefault().GradeLevelID;

            result.Data = new
            {
                //Dashboard Ratio
                TotalStudents = new { Students = CountStudent },
                TotalAdvisers = new { Advisers = GetItems.NoOfTeacher },
                TotalClassRooms = new { ClassRooms = GetItems.NoOfClassRoom },
                TotalComfortRooms = new { ComfortRooms = GetItems.NoOfComfortRoom },
                TotalComputers = new { Computers = GetItems.NoOfComputer },
                TotalSeats = new { Seats = GetItems.NoOfSeat },
                RatioStudentAdviser = new { StudentAdviserRatio = StudentAdviser },
                RatioStudentClassRoom = new { StudentClassRoomRatio = StudentClassRoom },
                RatioStudentComfortRoom = new { StudentComfortRoomRatio = StudentComfortRoom },
                RatioStudentComputer = new { StudentComputerRatio = StudentComputer },
                RatioStudentSeat = new { StudentSeatRatio = StudentSeat },

                //Student By Is
                IsAssistanceMale = new { IsAssistance = IsAssistanceMale },
                IsAssistanceFemale = new { IsAssistance = IsAssistanceFemale },
                Is4PsMale = new { Is4Ps = Is4PsMale },
                Is4PsFemale = new { Is4Ps = Is4PsFemale },
                IsProvisinaryMale = new { IsProvisinary = IsProvisinaryMale },
                IsProvisinaryFemale = new { IsProvisinary = IsProvisinaryFemale },

                //Hor Bar Chart Grils and Boys
                SchoolYear = new { ActiveSchoolYear = SchoolYr },

                GirlsStudents = new
                {
                    Boys = DR.CountStudentByGender(1),
                    Girls = DR.CountStudentByGender(2)
                },
                BoysStudents = new
                {
                    Boys = DR.CountStudentByGender(1),
                    Girls = DR.CountStudentByGender(2)
                },

                //Pie Chart By Ethnicity

                ByIlonggo = new
                {
                    Ilonggo = DR.CountStudentByEthnicity(1),
                },
                ByIlocano = new
                {
                    Ilocano = DR.CountStudentByEthnicity(2),
                },
                ByBLaan = new
                {
                    BLaan = DR.CountStudentByEthnicity(3),
                },
                ByCebuano = new
                {
                    Cebuano = DR.CountStudentByEthnicity(4),
                },
                ByManobo = new
                {
                    Manobo = DR.CountStudentByEthnicity(5),
                },
                ByMangyan = new
                {
                    Mangyan = DR.CountStudentByEthnicity(6),
                },
                ByTBoli = new
                {
                    TBoli = DR.CountStudentByEthnicity(7),
                },
                ByOthers = new
                {
                    Others = DR.CountStudentByEthnicity(8),
                },

                //Pie Chart By Religion
                ByIslam = new
                {
                    Islam = DR.CountStudentByReligion(1),
                },
                ByCatholic = new
                {
                    Catholic = DR.CountStudentByReligion(2),
                },
                ByEvangelical = new
                {
                    Evangelical = DR.CountStudentByReligion(3),
                },
                ByINC = new
                {
                    INC = DR.CountStudentByReligion(4),
                },
                BySDA = new
                {
                    SDA = DR.CountStudentByReligion(5),
                },
                ByUCCP = new
                {
                    UCCP = DR.CountStudentByReligion(6),
                },
                ByJehova = new
                {
                    Jehova = DR.CountStudentByReligion(7),
                },
                ByMethodist = new
                {
                    Methodist = DR.CountStudentByReligion(8),
                },
                ByBaptist = new
                {
                    Baptist = DR.CountStudentByReligion(9),
                },
                ByTribal = new
                {
                    Tribal = DR.CountStudentByReligion(10),
                },

                //By Track
                MaleTrack = new
                {
                    MaleAcademic = DR.CountMaleByTrack(1),
                    MaleTVL = DR.CountMaleByTrack(2),
                    MaleSports = DR.CountMaleByTrack(3),
                    MaleArtsDesign = DR.CountMaleByTrack(4),
                },
                FemaleTrack = new
                {
                    FemaleAcademic = DR.CountFemaleByTrack(1),
                    FemaleTVL = DR.CountFemaleByTrack(2),
                    FemaleSports = DR.CountFemaleByTrack(3),
                    FemaleArtsDesign = DR.CountFemaleByTrack(4),
                },

                //By Strand
                MaleStrand = new
                {
                    MaleABM = DR.CountMaleByStrand(1),
                    MaleHUMSS = DR.CountMaleByStrand(2),
                    MaleSTEM = DR.CountMaleByStrand(3),
                    MaleGAS = DR.CountMaleByStrand(4),
                },
                FemaleStrand = new
                {
                    FemaleABM = DR.CountFemaleByStrand(1),
                    FemaleHUMSS = DR.CountFemaleByStrand(2),
                    FemaleSTEM = DR.CountFemaleByStrand(3),
                    FemaleGAS = DR.CountFemaleByStrand(4),
                },

                //By AcadStatus
                MaleAcadStatus = new
                {
                    MaleRegular = DR.CountMaleByAcadStatus(1),
                    MaleIrregular = DR.CountMaleByAcadStatus(2),
                    MaleTransferee = DR.CountMaleByAcadStatus(3),
                    MaleBalikAral = DR.CountMaleByAcadStatus(4),
                },
                FemaleAcadStatus = new
                {
                    FemaleRegular = DR.CountFemaleByAcadStatus(1),
                    FemaleIrregular = DR.CountFemaleByAcadStatus(2),
                    FemaleTransferee = DR.CountFemaleByAcadStatus(3),
                    FemaleBalikAral = DR.CountFemaleByAcadStatus(4),
                },
                //By Grade Level
                MaleGradeLevel = new
                {
                    MaleGradeLevel11 = DR.CountMaleByGradeLevel(Grade11),
                    MaleGradeLevel12 = DR.CountMaleByGradeLevel(Grade12),
                },
                FemaleGradeLevel = new
                {
                    FemaleGradeLevel11 = DR.CountFemaleByGradeLevel(Grade11),
                    FemaleGradeLevel12 = DR.CountFemaleByGradeLevel(Grade12),
                }



            };
            return result;
        }

        //public ActionResult GetByEthnicity()
        //{
        //    string[] EthnicityDesc = db.tbl_Ethnicity.Select(a => a.Description).ToArray();
        //    return Json(new
        //      {
        //          success = true,
        //          EthnicityCount = DR.CountByEthnicity(),
        //          EthnicityDesc = EthnicityDesc,
        //      }, JsonRequestBehavior.AllowGet);
        //}


    }
}
