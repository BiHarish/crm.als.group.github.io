using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalQuery;
using System.Linq;

namespace ICWR.Data
{
    public class RevenueARAPReportData
    {
        public DataTable GetByPLSBHS_Step1(List<string> Company,string Period, string currencyWant=null,string currencyMode = null)
        {
            OutResult or = QueryForDrillPLSBHSReport.GetPLSStep1(Company, Period, currencyWant, currencyMode);
            return or.dt;
        }
    }
}