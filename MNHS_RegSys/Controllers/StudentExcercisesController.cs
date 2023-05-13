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
    public class StudentExcercisesController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        ExcercisesRepository ER = new ExcercisesRepository();

        public ActionResult StudentExcercises()
        {
            LoadDropdowns();
            return View();
        }

        public ActionResult LoadDropdowns()
        {
            ViewBag.ListOfQuestionsArray = ER.ListOfQuestionsArray();
            ViewBag.ListOfQuestions = ER.ListOfQuestions();
            ViewBag.ListOfAnswers = ER.ListOfAnswers();

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

    }//end
}
