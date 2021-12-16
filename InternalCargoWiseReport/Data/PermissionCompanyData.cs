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
    public class PermissionCompanyData
    {
        SqlParameter[] Param(PermissionCompanyDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@PermissionCompanyId", SqlDbType.BigInt);
            Param[0].Value = obj.PermissionCompanyId;

            Param[1] = new SqlParameter("@PermissionCompanyUserRoleId", SqlDbType.BigInt);
            Param[1].Value = obj.PermissionCompanyUserRoleId;

            Param[2] = new SqlParameter("@PermissionCompanyview", SqlDbType.Bit);
            Param[2].Value = obj.PermissionCompanyview;

            Param[3] = new SqlParameter("@PermissionCompanyCompanyId", SqlDbType.BigInt);
            Param[3].Value = obj.PermissionCompanyCompanyId;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }

        public bool Insert(PermissionCompanyDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionCompany", Params);

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
        public List<PermissionCompanyDto> GetByUserRoleMenu(long UserRoleId)
        {
            DataSet _ds = null;
            var Params = Param(new PermissionCompanyDto() { PermissionCompanyUserRoleId = UserRoleId }, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(Utility.LovelyPair.connection, CommandType.StoredProcedure, "uspPermissionCompany", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        List<PermissionCompanyDto> results = new List<PermissionCompanyDto>();

                        foreach (DataRow row in rows)
                        {
                            PermissionCompanyDto obj = new PermissionCompanyDto();
                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.PermissionCompanyId = row["PermissionCompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionCompanyId"]);
                            obj.PermissionCompanyCompanyId = row["PermissionCompanyCompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionCompanyCompanyId"]);
                            obj.PermissionCompanyUserRoleId = row["PermissionCompanyUserRoleId"] == DBNull.Value ? 0 : Convert.ToInt64(row["PermissionCompanyUserRoleId"]);
                            obj.PermissionCompanyview = row["PermissionCompanyview"] == DBNull.Value ? false : Convert.ToBoolean(row["PermissionCompanyview"]);
                            obj.CompanyId = row["CompanyId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CompanyId"]);
                            obj.CompanyName = row["CompanyName"].ToString();
                            obj.CompanyCode = row["CompanyCode"].ToString();        

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