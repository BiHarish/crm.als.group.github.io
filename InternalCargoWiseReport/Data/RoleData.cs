using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class RoleData
    {
        SqlParameter[] Param(RoleDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@RoleId", SqlDbType.Int);
            Param[0].Value = obj.RoleId;

            Param[1] = new SqlParameter("@RoleName", SqlDbType.NVarChar);
            Param[1].Value = obj.RoleName;

            Param[2] = new SqlParameter("@RoleIsActive", SqlDbType.Bit);
            Param[2].Value = obj.RoleIsActive;

            Param[3] = new SqlParameter("@RoleAmount", SqlDbType.NVarChar);
            Param[3].Value = obj.RoleAmount;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(RoleDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspRole", Params);

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
        public bool Update(RoleDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspRole", Params);

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
        public RoleDto GetById(int id)
        {
            DataSet _ds = null;

            RoleDto _role = new RoleDto();
            _role.RoleId = id;
            var Params = Param(_role, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspRole", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        RoleDto result = new RoleDto();
                        {
                            result.RoleId = row["RoleId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RoleId"]);
                            result.RoleName = row["RoleName"].ToString();
                            result.RoleIsActive = row["RoleIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["RoleIsActive"]);
                            result.RoleAmount = row["RoleAmount"].ToString();
                        }
                        return result;
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
            return null;
        }
        public IList<RoleDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            RoleDto _role = new RoleDto();
            _role.RoleIsActive = Type;
            var Params = Param(_role, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspRole", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<RoleDto> results = new List<RoleDto>();

                        foreach (DataRow row in rows)
                        {
                            RoleDto obj = new RoleDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.RoleId = row["RoleId"] == DBNull.Value ? 0 : Convert.ToInt32(row["RoleId"]);
                            obj.RoleName = row["RoleName"].ToString();
                            obj.RoleIsActive = row["RoleIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["RoleIsActive"]);
                            obj.RoleAmount = row["RoleAmount"].ToString();

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
        public bool IsActiveOnOff(RoleDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspRole", Params);

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
    }
}