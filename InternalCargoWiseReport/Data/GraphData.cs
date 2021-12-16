using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ICWR.Data
{
    public class GraphData
    {
        SqlParameter[] Param(GraphDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@Fyear", SqlDbType.NVarChar);
            Param[0].Value = obj.Fyear;
            Param[1] = new SqlParameter("@divisionid", SqlDbType.BigInt);
            Param[1].Value = obj.divisionid;
            Param[2] = new SqlParameter("@typeid", SqlDbType.BigInt);
            Param[2].Value = obj.typeid;
            Param[3] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[3].Value = flag;

            return Param;
        }
        public DataSet GetAll(string year, int? divisionid)
        {

            GraphDto obj = new GraphDto();
            obj.Fyear = year;
            obj.divisionid = divisionid;
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspgraph", Params);

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

        public DataSet GetActualAndBudget(string year, int? divisionid)
        {
            GraphDto obj = new GraphDto();
            obj.Fyear = year;
            obj.divisionid = divisionid;
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    DataSet result = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspgraph", Params);

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