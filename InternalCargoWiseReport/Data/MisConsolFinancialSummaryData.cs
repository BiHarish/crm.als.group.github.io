using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class MisConsolFinancialSummaryData
    {
        SqlParameter[] Param(MisConsolFinancialSummaryDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@FinancialYear", SqlDbType.NVarChar);
            Param[0].Value = obj.FinancialYear;
            Param[1] = new SqlParameter("@Month", SqlDbType.BigInt);
            Param[1].Value = obj.Month;
            Param[2] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[2].Value = flag;
            return Param;
        }
        public DataSet GetGridData(MisConsolFinancialSummaryDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisConsolFinancialSummary", Params);

                    if (result != null && result.Tables[0].Rows.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {

                }
            }
            return null;
        }
    }
}