using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ICWR.QueryInternal;
namespace ICWR.Data
{
    public class MonthlyCostRevenueData
    {
        //public DataTable GetEstimateActual(string DateFrom, string DateTo, string CompanyCode)
        //{
        //    DataTable dt = Query.EstimateVsActual(DateFrom, DateTo, CompanyCode);
        //    return dt;
        //}

        public DataTable MonthlyCostRevenueF(string DateFrom, string DateTo, string CompanyCode)
        {
            InnerOutResult or = QueryInternal.QueryInternal.MonthlyCostRevenue(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }
    }
}