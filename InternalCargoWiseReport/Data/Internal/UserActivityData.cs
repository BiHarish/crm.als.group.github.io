using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalQuery;

namespace ICWR.Data
{
    public class UserActivityData
    {
        //public DataTable GetUserActivityByDateFromTo(string Datefrom, string Dateto)
        //{
        //    DataTable dt = Query.UserActivityQuery(Datefrom, Dateto);
        //    return dt;
        //}

        public DataTable GetUserActivityByDateFromTo(string Datefrom, string Dateto)
        {
            OutResult or = Query.UserActivityQuery(Datefrom, Dateto);
            return or.dt;
        }
    }
}