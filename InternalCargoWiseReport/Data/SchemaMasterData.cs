using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class SchemaMasterData
    {
        SqlParameter[] Param(SchemaMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@SchemaMasterId", SqlDbType.Int);
            Param[0].Value = obj.SchemaMasterId;

            Param[1] = new SqlParameter("@SchemaMasterName", SqlDbType.NVarChar);
            Param[1].Value = obj.SchemaMasterName;

            Param[2] = new SqlParameter("@SchemaMasterIsActive", SqlDbType.Bit);
            Param[2].Value = obj.SchemaMasterIsActive;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            return Param;
        }
        public bool Insert(SchemaMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSchemaMaster", Params);

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
        public bool Update(SchemaMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSchemaMaster", Params);

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
        public SchemaMasterDto GetById(int id)
        {
            DataSet _ds = null;

            SchemaMasterDto _SchemaMaster = new SchemaMasterDto();
            _SchemaMaster.SchemaMasterId = id;
            var Params = Param(_SchemaMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSchemaMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        SchemaMasterDto result = new SchemaMasterDto();
                        {
                            result.SchemaMasterId = row["SchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SchemaMasterId"]);
                            result.SchemaMasterName = row["SchemaMasterName"].ToString();
                            result.SchemaMasterIsActive = row["SchemaMasterIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["SchemaMasterIsActive"]);
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
        public IList<SchemaMasterDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            SchemaMasterDto _SchemaMaster = new SchemaMasterDto();
            _SchemaMaster.SchemaMasterIsActive = Type;
            var Params = Param(_SchemaMaster, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSchemaMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<SchemaMasterDto> results = new List<SchemaMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            SchemaMasterDto obj = new SchemaMasterDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.SchemaMasterId = row["SchemaMasterId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SchemaMasterId"]);
                            obj.SchemaMasterName = row["SchemaMasterName"].ToString();
                            obj.SchemaMasterIsActive = row["SchemaMasterIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["SchemaMasterIsActive"]);

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
        public bool IsActiveOnOff(SchemaMasterDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSchemaMaster", Params);

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