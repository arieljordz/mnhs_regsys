using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared;

namespace MNHS_RegSys.Models
{
    public class ReportFormatFactory
    {
        private static Dictionary<string, ExportFormatType> reportFormat = new Dictionary<string, ExportFormatType>();


        static ReportFormatFactory()
        {
            if (reportFormat.Count() == 0)
            {
                reportFormat.Add("pdf", ExportFormatType.PortableDocFormat);
                reportFormat.Add("excel", ExportFormatType.Excel);
            }

        }

        public static ExportFormatType CreateByFormat(string format)
        {
            return reportFormat[format.ToLower()];
        }
    }
}