using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class MaxValData
    {
        SqlParameter[] Param(MaxValDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Description", SqlDbType.NVarChar);
            Param[1].Value = obj.Description;

            Param[2] = new SqlParameter("@Value", SqlDbType.BigInt);
            Param[2].Value = obj.Value;
         

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            return Param;
        }
       
        public bool Update(MaxValDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspMaxVal", Params);

                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {

                }
            }
            return false;
        }
      
      

        public MaxValDto GetByDescription(string Description)
        {
            DataSet _ds = null;

            MaxValDto _MaxVal = new MaxValDto();
            _MaxVal.Description = Description;
            var Params = Param(_MaxVal, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMaxVal", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        MaxValDto result = new MaxValDto();
                        {
                            result.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt64(row["ID"]);
                            result.Description = row["Description"].ToString();
                            result.Value = row["Value"] == DBNull.Value ? 0 : Convert.ToInt64(row["Value"]);
                        }
                        return result;
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