using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNHS_RegSys.Models
{
    public class BySpecializationDTO
    {
        public string CompleteName { get; set; }
        public int? SpecID { get; set; }
    }

    public class StudentListSpec
    {
        public static List<BySpecializationDTO> SpecID1
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 1
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID2
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 2
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID3
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 3
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID4
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 4
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID5
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 5
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID6
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 6
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID7
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 7
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID8
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 8
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID9
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 9
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID10
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 10
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }

        public static List<BySpecializationDTO> SpecID11
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.SpecID == 11
                            select new BySpecializationDTO
                            {
                                CompleteName = a.CompleteName,
                                SpecID = a.SpecID
                            }).OrderBy(a => a.SpecID).ToList();
                return data;
            }
        }
    }
}