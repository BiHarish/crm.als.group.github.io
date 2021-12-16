using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ICWR.Data.Utility
{
    public static class LovelyPair
    {
        public static string connection { get { return ConfigurationManager.ConnectionStrings["InernalConnection"].ConnectionString; } }

        public static string Internalconnection { get { return ConfigurationManager.ConnectionStrings["CargoWiseConnection"].ConnectionString; } }

        public static string Csvconnection { get { return ConfigurationManager.ConnectionStrings["Csvconnection"].ConnectionString; } }
    }
}