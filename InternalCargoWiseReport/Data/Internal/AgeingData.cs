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
    public class AgeingData
    {
        //public DataTable GetAgeing(string DateFrom, string CompanyCode)
        //{
        //    DataTable dt = Query.AgeingQuery(DateFrom, CompanyCode);
        //    return dt;
        //}

        public DataTable GetAgeing(string DateFrom, string CompanyCode,string partyCode)
        {
            OutResult or = Query.AgeingQuery(DateFrom, CompanyCode,partyCode);
            return or.dt;
        }

        public DataTable GetAgeingPartyWise(string DateFrom, string CompanyCode, string partyCode)
        {
            OutResult or = Query.AgeingQueryPartyWise(DateFrom, CompanyCode, partyCode);
            return or.dt;
        }

        public DataTable GetDebtor(string CompanyCode)
        {
            OutResult or = QueryForDrop.GetCargoWiseDebtor(CompanyCode);
            return or.dt;
        }
    }
}