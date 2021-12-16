using ICWR.Data.Utility;
using ICWR.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class MISRunRateData
    {
        SqlParameter[] Param(MISRunRateDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@CYfinancialYear", SqlDbType.NVarChar);
            Param[0].Value = obj.CYfinancialYear;
            Param[1] = new SqlParameter("@month", SqlDbType.BigInt);
            Param[1].Value = obj.month;
            Param[2] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[2].Value = flag;
            return Param;
        }
        public DataSet GetGridData(MISRunRateDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMisRunRate", Params);

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