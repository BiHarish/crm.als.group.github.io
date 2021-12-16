using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalQuery;
using ICWR.QueryInternal;

namespace ICWR.Data
{
    public class DispatchData
    {
        //public DataTable GetUserActivityByDateFromTo(string Datefrom, string Dateto)
        //{
        //    DataTable dt = Query.UserActivityQuery(Datefrom, Dateto);
        //    return dt;
        //}

        public DataTable GetDispatchDataByDateFromTo(string Datefrom, string Dateto,string company)
        {
            InnerOutResult or = QueryInternal.QueryInternal.UserRDisptach(Datefrom, Dateto, company);
            return or.dt;
        }

        public DataTable GetCreditNoteDataByDateFromTo(string Datefrom, string Dateto, string company)
        {
            InnerOutResult or = QueryInternal.QueryInternal.UserRDisptachCreditNote(Datefrom, Dateto, company);
            return or.dt;
        }

        public DataTable GetColorDashboard()
        {
            InnerOutResult or = QueryInternal.QueryInternal.ColorDashboard();
            return or.dt;
        }
    }
}