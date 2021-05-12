using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class DbConnString
    {
        public static string DBCONN_STRING = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
    }
}