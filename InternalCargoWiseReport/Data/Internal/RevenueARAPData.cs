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
    public class RevenueARAPData
    {
        public DataTable GetByARAP(string Company, string Period, string ARAP)
        {
            OutResult or = QueryForDashboard.GetRevenueARAP(ARAP, "201812", Company);
            return or.dt;
        }
        public DataTable GetByPLSBHS(List<string> Company, string Period, string type)
        {
            OutResult or = QueryForPLSBHS.GetPLSBHS(Company, Period, type);
            return or.dt;
        }
        public DataTable GetByPLSBHS_Step1(List<string> Company,string Period, string currencyWant=null)
        {
            OutResult or =  QueryForDrillPLSBHS.GetPLSStep1(Company, Period,currencyWant);
            return or.dt;
        }

        public DataTable GetByPLSBHS_Step2(List<string> Company, string Period, string currencyWant = null,string AccountPk = null)
        {
            OutResult or = QueryForDrillPLSBHS.GetPLSStep2(Company, Period, currencyWant,AccountPk);
            return or.dt;
        }

        public DataTable GetByPLSBHS_Step3(string Company, string Period, string currencyWant = null, string AccountPk = null)
        {
            OutResult or = QueryForDrillPLSBHS.GetPLSStep3(Company, Period, currencyWant, AccountPk);
            
            return or.dt;
        }

        public DataTable GetByPLSBHS_Step4(string Company, string Period, string currencyWant = null, string AccountPk = null,string chargeCode=null)
        {
            OutResult or = QueryForDrillPLSBHS.GetPLSStep4(Company, Period, currencyWant, AccountPk,chargeCode);
            return or.dt;
        }

        public DataTable GetByPLSBHS_Step5(string Company, string Period, string currencyWant = null, string AccountPk = null, string chargeCode = null)
        {
            OutResult or = QueryForDrillPLSBHS.GetPLSStep5(Company, Period, currencyWant, AccountPk, chargeCode);
            return or.dt;
        }
    }
}