using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNHS_RegSys.Models
{
    public class ByGradeLevelDTO
    {
        public string CompleteName { get; set; }
        public int? GradeLevelID { get; set; }
    }
    public class StudentListGradeLevel
    {
        public static List<ByGradeLevelDTO> GradeLevelIDBoys1
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var Grade11 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 11")).SingleOrDefault().GradeLevelID;
                var data = (from a in db.tbl_Student
                            where a.GradeLevelID == Grade11 && a.GenderID == 1
                            select new ByGradeLevelDTO
                            {
                                CompleteName = a.CompleteName,
                                GradeLevelID = a.GradeLevelID
                            }).OrderBy(a => a.GradeLevelID).ToList();
                return data;
            }
        }
        public static List<ByGradeLevelDTO> GradeLevelIDGirls1
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var Grade11 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 11")).SingleOrDefault().GradeLevelID;
                var data = (from a in db.tbl_Student
                            where a.GradeLevelID == Grade11 && a.GenderID == 2
                            select new ByGradeLevelDTO
                            {
                                CompleteName = a.CompleteName,
                                GradeLevelID = a.GradeLevelID
                            }).OrderBy(a => a.GradeLevelID).ToList();
                return data;
            }
        }

        public static List<ByGradeLevelDTO> GradeLevelIDBoys2
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var Grade12 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 12")).SingleOrDefault().GradeLevelID;
                var data = (from a in db.tbl_Student
                            where a.GradeLevelID == Grade12 && a.GenderID == 1
                            select new ByGradeLevelDTO
                            {
                                CompleteName = a.CompleteName,
                                GradeLevelID = a.GradeLevelID
                            }).OrderBy(a => a.GradeLevelID).ToList();
                return data;
            }
        }

        public static List<ByGradeLevelDTO> GradeLevelIDGirls2
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var Grade12 = db.tbl_GradeLevel.Where(a => a.Description.ToLower().Contains("grade 12")).SingleOrDefault().GradeLevelID;
                var data = (from a in db.tbl_Student
                            where a.GradeLevelID == Grade12 && a.GenderID == 2
                            select new ByGradeLevelDTO
                            {
                                CompleteName = a.CompleteName,
                                GradeLevelID = a.GradeLevelID
                            }).OrderBy(a => a.GradeLevelID).ToList();
                return data;
            }
        }

    }
}
