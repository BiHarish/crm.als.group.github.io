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
    public class EstimatevsActualData
    {
        //public DataTable GetEstimateActual(string DateFrom, string DateTo, string CompanyCode)
        //{
        //    DataTable dt = Query.EstimateVsActual(DateFrom, DateTo, CompanyCode);
        //    return dt;
        //}

        public DataTable GetEstimateActual(string DateFrom, string DateTo, string CompanyCode)
        {
            OutResult or  = Query.EstimateVsActual(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }

        public DataTable GetOrderDiff(string DateFrom, string DateTo, string CompanyCode)
        {
            OutResult or = Query.DiffEstimateVsActual(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }

        public DataTable GetGrossProfitList(string DateFrom, string DateTo, string CompanyCode)
        {
            OutResult or = Query.GetGrossProfit(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }

        public DataTable GetInterestList(string DateFrom, string DateTo, string CompanyCode)
        {
            OutResult or = Query.GetInterestCalicationTable(DateFrom, DateTo, CompanyCode);
            return or.dt;
        }
    }
}