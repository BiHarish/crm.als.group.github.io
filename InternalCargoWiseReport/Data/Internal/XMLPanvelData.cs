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
    public class XMLPanvelData
    {
        //public DataTable GetUserActivityByDateFromTo(string Datefrom, string Dateto)
        //{
        //    DataTable dt = Query.UserActivityQuery(Datefrom, Dateto);
        //    return dt;
        //}

        public DataSet GetTariffCustomerCode(string chargeType)
        {
            OutResult or = Query.GetTeriffCode(chargeType);
            return or.ds; 
        }
    }
}