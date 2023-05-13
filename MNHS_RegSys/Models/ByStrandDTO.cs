using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNHS_RegSys.Models
{
    public class ByStrandDTO
    {
        public string CompleteName { get; set; }
        public int? StrandID { get; set; }
        public int Highiestcnt { get; set; }
        public int ABMcnt { get; set; }
        public int HUMSScnt { get; set; }
        public int STEMcnt { get; set; }
        public int GAScnt { get; set; }
    }
    public class StudentListClass
    {
        public static List<ByStrandDTO> StrandID1
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student where a.StrandID == 1
                            select new ByStrandDTO
                            {
                                CompleteName = a.CompleteName,
                                StrandID = a.StrandID
                            }).OrderBy(a => a.StrandID).ToList();
                return data;
            }
        }

        public static List<ByStrandDTO> StrandID2
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.StrandID == 2
                            select new ByStrandDTO
                            {
                                CompleteName = a.CompleteName,
                                StrandID = a.StrandID
                            }).OrderBy(a => a.StrandID).ToList();
                return data;
            }
        }

        public static List<ByStrandDTO> StrandID3
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.StrandID == 3
                            select new ByStrandDTO
                            {
                                CompleteName = a.CompleteName,
                                StrandID = a.StrandID
                            }).OrderBy(a => a.StrandID).ToList();
                return data;
            }
        }

        public static List<ByStrandDTO> StrandID4
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.StrandID == 4
                            select new ByStrandDTO
                            {
                                CompleteName = a.CompleteName,
                                StrandID = a.StrandID
                            }).OrderBy(a => a.StrandID).ToList();
                return data;
            }
        }
    }
}
