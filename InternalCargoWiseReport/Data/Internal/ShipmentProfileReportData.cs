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
    public class ShipmentProfileReportData
    {
        //public DataTable GetEstimateActual(string DateFrom, string DateTo, string CompanyCode)
        //{
        //    DataTable dt = Query.EstimateVsActual(DateFrom, DateTo, CompanyCode);
        //    return dt;
        //}

        public DataTable ShipmentProfile(string DateFrom, string DateTo, string CompanyCode)
        {
            OutResult or = Query.ShipmentProfileReport(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }
    }
}