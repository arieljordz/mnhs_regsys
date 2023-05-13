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
using MNHS_RegSys;
using MNHS_RegSys.Reports;

namespace MNHS_RegSys.Controllers
{
    public class ReportsController : Controller
    {
        mnhsregsysEntities db = new mnhsregsysEntities();
        //
        // GET: /UsersRegistration/

        public ActionResult Reports()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);
            var AccessType = db.tbl_User.Where(a => a.UserID == UserID).SingleOrDefault().AccessType;
            if (AccessType == "Admin")
            {
                var ABMcnt = db.tbl_Student.Where(a => a.StrandID == 1).Count();
                var HUMSScnt = db.tbl_Student.Where(a => a.StrandID == 2).Count();
                var STEMcnt = db.tbl_Student.Where(a => a.StrandID == 3).Count();
                var GAScnt = db.tbl_Student.Where(a => a.StrandID == 4).Count();
                int[] _counter = new int[4];
                _counter[0] = ABMcnt;
                _counter[1] = HUMSScnt;
                _counter[2] = STEMcnt;
                _counter[3] = GAScnt;

                var Highiestcnt = 0;
                for (int i = 0; i < _counter.Length; i++)
                {
                    if (Highiestcnt <= _counter[i])
                        Highiestcnt = _counter[i];
                }
                var sss = db.tbl_Student
                    .Select(x => new ByStrandDTO()
                    {
                        CompleteName = x.CompleteName,
                        StrandID = x.StrandID == null ? 0 : (int)x.StrandID,
                        Highiestcnt = Highiestcnt,
                        ABMcnt = ABMcnt,
                        HUMSScnt = HUMSScnt,
                        STEMcnt = STEMcnt,
                        GAScnt = GAScnt,
                    }).ToList().OrderBy(x => x.StrandID).Distinct();

                ViewBag.StudentByStrand = sss;

                ViewBag.StudentByTrack = db.tbl_Student
                    .Select(x => new ByTrackDTO()
                    {
                        CompleteName = x.CompleteName,
                        TrackID = x.TrackID == null ? 0 : (int)x.TrackID,
                    }).ToList().OrderBy(x => x.TrackID).Distinct();

                ViewBag.StudentBySpecial = db.tbl_Student
                    .Select(x => new BySpecializationDTO()
                    {
                        CompleteName = x.CompleteName,
                        SpecID = x.SpecID == null ? 0 : (int)x.SpecID,
                    }).ToList().OrderBy(x => x.SpecID).Distinct();

                ViewBag.DataFromDashboard = db.sp_GenerateReportsBy().Select(a => new DataFromDashboardDTO()
                {
                    TotalStudents = a.TotalStudents == null ? 0 : (int)a.TotalStudents,
                    TotalBoys = a.TotalBoys == null ? 0 : (int)a.TotalBoys,
                    TotalGirls = a.TotalGirls == null ? 0 : (int)a.TotalGirls,
                    TeacherStudent = Math.Round((decimal)a.TeacherStudent),
                    TotalTeachers = a.TotalTeachers == null ? 0 : (int)a.TotalTeachers,
                    ClassRoomStudent = Math.Round((decimal)a.ClassRoomStudent),
                    TotalClassRooms = a.TotalClassRooms == null ? 0 : (int)a.TotalClassRooms,
                    ComputerStudent = Math.Round((decimal)a.ComputerStudent),
                    TotalComputers = a.TotalComputers == null ? 0 : (int)a.TotalComputers,
                    ToiletStudent = Math.Round((decimal)a.ToiletStudent),
                    TotalToilets = a.TotalToilets == null ? 0 : (int)a.TotalToilets,
                    SeatStudent = a.SeatStudent == null ? 0 : (int)a.SeatStudent,
                    TotalSeats = a.TotalSeats == null ? 0 : (int)a.TotalSeats,
                    MaleABM = a.MaleABM == null ? 0 : (int)a.MaleABM,
                    MaleHUMSS = a.MaleHUMSS == null ? 0 : (int)a.MaleHUMSS,
                    MaleSTEM = a.MaleSTEM == null ? 0 : (int)a.MaleSTEM,
                    MaleGAS = a.MaleGAS == null ? 0 : (int)a.MaleGAS,
                    FemaleABM = a.FemaleABM == null ? 0 : (int)a.FemaleABM,
                    FemaleHUMSS = a.FemaleHUMSS == null ? 0 : (int)a.FemaleHUMSS,
                    FemaleSTEM = a.FemaleSTEM == null ? 0 : (int)a.FemaleSTEM,
                    FemaleGAS = a.FemaleGAS == null ? 0 : (int)a.FemaleGAS,
                    MaleAcademic = a.MaleAcademic == null ? 0 : (int)a.MaleAcademic,
                    MaleTVL = a.MaleTVL == null ? 0 : (int)a.MaleTVL,
                    MaleSports = a.MaleSports == null ? 0 : (int)a.MaleSports,
                    MaleArtsDesign = a.MaleArtsDesign == null ? 0 : (int)a.MaleArtsDesign,
                    FemaleAcademic = a.FemaleAcademic == null ? 0 : (int)a.FemaleAcademic,
                    FemaleTVL = a.FemaleTVL == null ? 0 : (int)a.FemaleTVL,
                    FemaleSports = a.FemaleSports == null ? 0 : (int)a.FemaleSports,
                    FemaleArtsDesign = a.FemaleArtsDesign == null ? 0 : (int)a.FemaleArtsDesign,
                    MaleRegular = a.MaleRegular == null ? 0 : (int)a.MaleRegular,
                    MaleIrregular = a.MaleIrregular == null ? 0 : (int)a.MaleIrregular,
                    MaleTransferee = a.MaleTransferee == null ? 0 : (int)a.MaleTransferee,
                    MaleBalikAral = a.MaleBalikAral == null ? 0 : (int)a.MaleBalikAral,
                    FemaleRegular = a.FemaleRegular == null ? 0 : (int)a.FemaleRegular,
                    FemaleIrregular = a.FemaleIrregular == null ? 0 : (int)a.FemaleIrregular,
                    FemaleTransferee = a.FemaleTransferee == null ? 0 : (int)a.FemaleTransferee,
                    FemaleBalikAral = a.FemaleBalikAral == null ? 0 : (int)a.FemaleBalikAral,
                    Islam = a.Islam == null ? 0 : (int)a.Islam,
                    Catholic = a.Catholic == null ? 0 : (int)a.Catholic,
                    Evangelical = a.Evangelical == null ? 0 : (int)a.Evangelical,
                    INC = a.INC == null ? 0 : (int)a.INC,
                    SDA = a.SDA == null ? 0 : (int)a.SDA,
                    UCCP = a.UCCP == null ? 0 : (int)a.UCCP,
                    Jehova = a.Jehova == null ? 0 : (int)a.Jehova,
                    Methodist = a.Methodist == null ? 0 : (int)a.Methodist,
                    Baptists = a.Baptists == null ? 0 : (int)a.Baptists,
                    Tribal = a.Tribal == null ? 0 : (int)a.Tribal,
                    Ilongo = a.Ilongo == null ? 0 : (int)a.Ilongo,
                    Ilocano = a.Ilocano == null ? 0 : (int)a.Ilocano,
                    Blaan = a.Blaan == null ? 0 : (int)a.Blaan,
                    Cebuano = a.Cebuano == null ? 0 : (int)a.Cebuano,
                    Manobo = a.Manobo == null ? 0 : (int)a.Manobo,
                    Mangyan = a.Mangyan == null ? 0 : (int)a.Mangyan,
                    Tboli = a.Tboli == null ? 0 : (int)a.Tboli,
                    Others = a.Others == null ? 0 : (int)a.Others
                }).ToList();
                return View();
            }
            else
            {
                return View("~/Views/Reports/NoAccessforView.cshtml");
            }
        }

        public ActionResult ReportFromDashboard()
        {
            GenerateReportsBy rpt = new GenerateReportsBy();

            //ODBCConfiguration odbc_config = new ODBCConfiguration();
            //ODBCAccount odbc_account = odbc_config.Get();
            //rpt.SetDatabaseLogon(odbc_account.UserName, odbc_account.Password, odbc_account.ODBCName, odbc_account.DatabaseName);
            Report rptMgnt = new Report(rpt);
            rpt = (GenerateReportsBy)rptMgnt.CrystalReportConnection();
            var result = rptMgnt.ConvertCrystalToFormat("PDF");
            return new FileContentResult(result.array, result.contentype);
        }

    }///end of controller
}
