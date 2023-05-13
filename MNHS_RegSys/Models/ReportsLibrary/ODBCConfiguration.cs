using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using CrystalDecisions.Shared;

namespace MNHS_RegSys
{
    public class ODBCAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ODBCName { get; set; }
        public string DatabaseName { get; set; }

    }

    public class ODBCConfiguration
    {
        public ODBCAccount Get() {
            ODBCAccount account = new ODBCAccount();
            try
            {
                account.UserName = ConfigurationManager.AppSettings.Get("rptUserID");
                account.Password = ConfigurationManager.AppSettings.Get("rptPassword");
                account.ODBCName = ConfigurationManager.AppSettings.Get("rptODBCName");
                account.DatabaseName = ConfigurationManager.AppSettings.Get("rptDatabaseName");
                return account;   
            }
            catch (Exception e)
            {
                return account;
            }            
        }

        public ConnectionInfo UpdateConnectionInfo(ODBCAccount odbc_account)
        {
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ServerName = odbc_account.ODBCName;
            connInfo.DatabaseName = odbc_account.DatabaseName;
            connInfo.UserID = odbc_account.UserName;
            connInfo.Password = odbc_account.Password;
            return connInfo;
        }
    
    }
}