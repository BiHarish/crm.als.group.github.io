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
    public class CargoOrgHeaderData
    {
        public DataTable GetOrgHeader(string CompanyCode)
        {
            OutResult or = Query.GetAllOrgHeader(CompanyCode);
            return or.dt;
        }
    }
}