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
    public class DashboardGraphData
    {
        public DataSet GetDashBoardGraphByARAP(string Company, string Period, string ARAP)
        {
            OutResult or = QueryForDashboardGraph.GetCountOfRevenueARAP(ARAP, Period);
            return or.ds;
        }
    }
}