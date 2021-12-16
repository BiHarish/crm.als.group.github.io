using Microsoft.ApplicationBlocks.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class PermissionSchemaMasterData
    {
        SqlParameter[] Param(PermissionSchemaMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@PermissionSchemaMasterId", SqlDbType.BigInt);
            Param[0].Value = obj.PermissionSchemaMasterId;

            Param[1] = new SqlParameter("@PermissionSchemaMasterUserRoleId", SqlDbType.BigInt);
            Param[1].Value = obj.PermissionSchemaMasterUserRoleId;

            Param[2] = new SqlParameter("@PermissionSchemaMasterview", SqlDbType.Bit);
            Param[2].Value = obj.PermissionSchemaMasterview;

            Param[3] = new SqlParameter("@PermissionSchemaSchemaMasterId", SqlDbType.BigInt);
            Param[3].Value = obj.PermissionSchemaSchemaMasterId;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }

        public bool Insert(PermissionSchemaMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionSchemaMaster", Params);

                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            return false;
        }
        public List<PermissionSchemaMasterDto> GetByUserRoleMenu(long UserRoleId)
        {
            DataSet _ds = null;
            var Params = Param(new PermissionSchemaMasterDto() { PermissionSchemaMasterUserRoleId = UserRoleId }, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionSchemaMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        List<PermissionSchemaMasterDto> results = new List<PermissionSchemaMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            PermissionSchemaMasterDto obj = new PermissionSchemaMasterDto();
                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.PermissionSchemaMasterId = row["PermissionSchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionSchemaMasterId"]);
                            obj.PermissionSchemaSchemaMasterId = row["PermissionSchemaSchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionSchemaSchemaMasterId"]);
                            obj.PermissionSchemaMasterUserRoleId = row["PermissionSchemaMasterUserRoleId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionSchemaMasterUserRoleId"]);
                            obj.PermissionSchemaMasterview = row["PermissionSchemaMasterview"] == DBNull.Value ? false : Convert.ToBoolean(row["PermissionSchemaMasterview"]);
                            obj.SchemaMasterId = row["SchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt64(row["SchemaMasterId"]);
                            obj.SchemaMasterName = row["SchemaMasterName"].ToString();          

                            results.Add(obj);
                        }
                        return results;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}