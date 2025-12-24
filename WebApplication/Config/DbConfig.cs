using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication.Config
{
    public class DbConfig
    {
        public static string PgConn =>
            ConfigurationManager.ConnectionStrings["PgConn"].ConnectionString;
    }
}