using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNHS_RegSys.Models
{
    public class ByTrackDTO
    {
        public string CompleteName { get; set; }
        public int? TrackID { get; set; }
    }

    public class StudentListTrack
    {
        public static List<ByTrackDTO> TrackID1
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.TrackID == 1
                            select new ByTrackDTO
                            {
                                CompleteName = a.CompleteName,
                                TrackID = a.TrackID
                            }).OrderBy(a => a.TrackID).ToList();
                return data;
            }
        }

        public static List<ByTrackDTO> TrackID2
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.TrackID == 2
                            select new ByTrackDTO
                            {
                                CompleteName = a.CompleteName,
                                TrackID = a.TrackID
                            }).OrderBy(a => a.TrackID).ToList();
                return data;
            }
        }

        public static List<ByTrackDTO> TrackID3
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.TrackID == 3
                            select new ByTrackDTO
                            {
                                CompleteName = a.CompleteName,
                                TrackID = a.TrackID
                            }).OrderBy(a => a.TrackID).ToList();
                return data;
            }
        }

        public static List<ByTrackDTO> TrackID4
        {
            get
            {
                mnhsregsysEntities db = new mnhsregsysEntities();
                var data = (from a in db.tbl_Student
                            where a.TrackID == 4
                            select new ByTrackDTO
                            {
                                CompleteName = a.CompleteName,
                                TrackID = a.TrackID
                            }).OrderBy(a => a.TrackID).ToList();
                return data;
            }
        }
    }
}