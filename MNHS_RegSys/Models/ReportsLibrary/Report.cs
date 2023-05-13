using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using MNHS_RegSys.Models;

namespace MNHS_RegSys.Models
{
    public class Report
    {
        private ReportClass concrete_rpt;
        private ODBCConfiguration odbc_config = null;
        private ODBCAccount odbc_account = null;

        public class ContentAndType
        {
            public byte[] array { get; set; }
            public string contentype { get; set; }
            public Stream stream { get; set; }
        }

        public Report(ReportClass rpt)
        {
            concrete_rpt = rpt;
        }

        public ReportClass CrystalReportConnection()
        {
            odbc_config = new ODBCConfiguration();
            odbc_account = odbc_config.Get();
            SetCrystalDatabaseLogOn(odbc_account);
            UpdateCrytalODBC(odbc_config, odbc_account);
            return concrete_rpt;

        }

        private void SetCrystalDatabaseLogOn(ODBCAccount odbc_account)
        {
            concrete_rpt.SetDatabaseLogon(odbc_account.UserName, odbc_account.Password, odbc_account.ODBCName, odbc_account.DatabaseName);
        }

        private void UpdateCrytalODBC(ODBCConfiguration odbc_config, ODBCAccount odbc_account)
        {
            //-- updates the logon info to crystal reports
            TableLogOnInfo tableLogOnInfo = new TableLogOnInfo();
            tableLogOnInfo.ConnectionInfo = odbc_config.UpdateConnectionInfo(odbc_account);
            for (int i = 0; i < concrete_rpt.Database.Tables.Count; i++)
            {
                concrete_rpt.Database.Tables[i].ApplyLogOnInfo(tableLogOnInfo);
            }

        }

        public ContentAndType ConvertCrystalToFormat(string format)
        {
            MemoryStream oStream = null;
            byte[] arr;
            ContentAndType pdfArr = null;
            try
            {
                oStream = (MemoryStream)concrete_rpt.ExportToStream(ReportFormatFactory.CreateByFormat(format));
                arr = oStream.ToArray();
                pdfArr = new ContentAndType();
                pdfArr.array = arr;
                pdfArr.contentype = ContentTypeFactory.CreateByFormat(format);
            }
            catch (Exception ex)
            {
                try

                {
                    pdfArr = new ContentAndType();
                    var stream = concrete_rpt.ExportToStream(ReportFormatFactory.CreateByFormat(format));
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        pdfArr.array = br.ReadBytes((int)stream.Length);
                    }
                    pdfArr.contentype = ContentTypeFactory.CreateByFormat(format);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }
            finally
            {

                concrete_rpt.Close();
                concrete_rpt.Dispose();
                if (oStream != null)
                {
                    oStream.Close();
                    oStream.Dispose();
                }
            }
            return pdfArr;

        }
    }
    public static class ContentTypeFactory
    {
        private static Dictionary<string, string> contentTypeByFormat = new Dictionary<string, string>();
        private static Dictionary<string, string> contentTypeByExtension = new Dictionary<string, string>();

        static ContentTypeFactory()
        {
            if (contentTypeByFormat.Count() == 0)
            {
                contentTypeByFormat.Add("pdf", "application/pdf");
                contentTypeByFormat.Add("excel", "application/vnd.ms-excel");
            }
            if (contentTypeByExtension.Count() == 0)
            {
                contentTypeByExtension.Add("jpg", "image/jpeg");
                contentTypeByExtension.Add("jpeg", "image/jpeg");
                contentTypeByExtension.Add("png", "image/png");
                contentTypeByExtension.Add("gif", "image/gif");
                contentTypeByExtension.Add("txt", "text/html");
                contentTypeByExtension.Add("doc", "application/msword");
                contentTypeByExtension.Add("docx", "application/msword");
                contentTypeByExtension.Add("ppt", "application/vnd.ms-powerpoint");
                contentTypeByExtension.Add("pptx", "application/vnd.ms-powerpoint");
                contentTypeByExtension.Add("pdf", "application/pdf");
                contentTypeByExtension.Add("mp3", "audio/mpeg");
                contentTypeByExtension.Add("mp4", "video/mp4");
                contentTypeByExtension.Add("flv", "video/x-flv");
                contentTypeByExtension.Add("wmv", "application/x-ms-wmv");
            }
        }

        public static string CreateByFormat(string format)
        {
            return contentTypeByFormat[format.ToLower()];
        }

        public static string CreateByExtension(string extension)
        {

            return contentTypeByExtension[extension.ToLower()];
        }
    }

}