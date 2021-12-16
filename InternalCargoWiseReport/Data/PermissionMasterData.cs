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
    public class PermissionMasterData
    {
        SqlParameter[] Param(PermissionMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[10];
            Param[0] = new SqlParameter("@PermissionMasterMenuId", SqlDbType.BigInt);
            Param[0].Value = obj.PermissionMasterMenuId;

            Param[1] = new SqlParameter("@PermissionMasterUserRoleId", SqlDbType.BigInt);
            Param[1].Value = obj.PermissionMasterUserRoleId;

            Param[2] = new SqlParameter("@PermissionMasterAdd", SqlDbType.Bit);
            Param[2].Value = obj.PermissionMasterAdd;

            Param[3] = new SqlParameter("@PermissionMasterView", SqlDbType.Bit);
            Param[3].Value = obj.PermissionMasterView;

            Param[4] = new SqlParameter("@PermissionMasterDelete", SqlDbType.Bit);
            Param[4].Value = obj.PermissionMasterDelete;

            Param[5] = new SqlParameter("@PermissionMasterUpdate", SqlDbType.Bit);
            Param[5].Value = obj.PermissionMasterUpdate;

            Param[6] = new SqlParameter("@PermissionMasterPrint", SqlDbType.Bit);
            Param[6].Value = obj.PermissionMasterPrint;

            Param[7] = new SqlParameter("@PermissionMasterSelf", SqlDbType.Bit);
            Param[7].Value = obj.PermissionMasterSelf;

            Param[8] = new SqlParameter("@PermissionMasterMenuShow", SqlDbType.Bit);
            Param[8].Value = obj.PermissionMasterMenuShow;

            Param[9] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[9].Value = flag;

            return Param;
        }

        public bool Insert(PermissionMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionMaster", Params);

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

        public List<PermissionMasterDto> GetByUserRoleMenu(long MenuId, long UserRoleId)
        {
            DataSet _ds = null;
            var Params = Param(new PermissionMasterDto() { PermissionMasterMenuId = MenuId,PermissionMasterUserRoleId = UserRoleId }, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        List<PermissionMasterDto> results = new List<PermissionMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            PermissionMasterDto obj = new PermissionMasterDto();

                            obj.PermissionMasterMenuId = row["BackMenuId"] == DBNull.Value? 0 : Convert.ToInt64(row["BackMenuId"]);
                            obj.PermissionMasterUserRoleId = row["RoleId"] == DBNull.Value? 0 : Convert.ToInt32(row["RoleId"]);
                            obj.PermissionMasterMenuName = row["BackMenuName"].ToString();
                            obj.PermissionMasterMenuShow = row["PermissionMastermenushow"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMastermenushow"]);
                            obj.PermissionMasterView= row["PermissionMasterview"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasterview"]);
                            obj.PermissionMasterAdd= row["PermissionMasteradd"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasteradd"]);
                            obj.PermissionMasterUpdate= row["PermissionMasterupdate"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasterupdate"]);
                            obj.PermissionMasterDelete= row["PermissionMasterdelete"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasterdelete"]);
                            obj.PermissionMasterPrint = row["PermissionMasterprint"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasterprint"]);
                            obj.PermissionMasterSelf = row["PermissionMasterself"] == DBNull.Value? false : Convert.ToBoolean(row["PermissionMasterself"]);
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