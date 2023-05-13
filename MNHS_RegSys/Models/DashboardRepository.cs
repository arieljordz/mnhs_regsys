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

namespace MNHS_RegSys.Models
{
    public class DashboardRepository
    {
        mnhsregsysEntities db = new mnhsregsysEntities();

        public void LogOut(int UserID)
        {
            HttpCookie httpCookie = HttpContext.Current.Response.Cookies[UserID];
            httpCookie.Expires = DateTime.Today.AddDays(-1);
            HttpContext.Current.Response.SetCookie(httpCookie);
            HttpContext.Current.Session.Clear();
        }

        public int CountStudentByEthnicity(int EthnicityID)
        {
            int[] EthnicityIDs = db.tbl_Ethnicity.Select(a => a.EthnicityID).ToArray();
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.EthnicityID == EthnicityID
                && ((DateTime)(x.DateRegistered)).Year == currentYear).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountStudentByReligion(int ReligionID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.ReligionID == ReligionID
                && ((DateTime)(x.DateRegistered)).Year == currentYear).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountStudentByGender(int GenderID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.GenderID == GenderID
                && ((DateTime)(x.DateRegistered)).Year == currentYear).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountFemaleByTrack(int TrackID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.TrackID == TrackID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 2).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountMaleByTrack(int TrackID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.TrackID == TrackID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 1).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountFemaleByStrand(int StrandID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.StrandID == StrandID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 2).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountMaleByStrand(int StrandID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.StrandID == StrandID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 1).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountFemaleByAcadStatus(int AcadStatusID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.AcadStatusID == AcadStatusID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 2).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountMaleByAcadStatus(int AcadStatusID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.AcadStatusID == AcadStatusID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 1).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountFemaleByGradeLevel(int GradeLevelID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.GradeLevelID == GradeLevelID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 2).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }

        public int CountMaleByGradeLevel(int GradeLevelID)
        {
            var currentYear = DateTime.Now.Year;
            var List = db.tbl_Student.Where(x => x.GradeLevelID == GradeLevelID
                && ((DateTime)(x.DateRegistered)).Year == currentYear && x.GenderID == 1).OrderBy(x => x.StudentID);
            int count = 0;
            long tempStudID = 0;
            foreach (var item in List)
            {
                if (item.StudentID != tempStudID)
                {
                    tempStudID = Convert.ToInt64(item.StudentID);
                    count++;
                }
            }
            return count;
        }


    }///end
}