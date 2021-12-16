using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhItsystemMasterData
    {
        SqlParameter[] Param(WhItsystemMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[2].Value = obj.IsActive;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = obj.CreateBy;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(WhItsystemMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhItsystemMaster", Params);

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
        public bool Update(WhItsystemMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhItsystemMaster", Params);

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
        public WhItsystemMasterDto GetById(int id)
        {
            DataSet _ds = null;

            WhItsystemMasterDto _WhItsystemMaster = new WhItsystemMasterDto();
            _WhItsystemMaster.ID = id;
            var Params = Param(_WhItsystemMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhItsystemMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhItsystemMasterDto result = new WhItsystemMasterDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.Name = row["Name"].ToString();
                            result.IsActive = row["IsActive"].ToDataConvertBool();
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
        public IList<WhItsystemMasterDto> GetAll()
        {
            DataSet _ds = null;
            var Params = Param(new WhItsystemMasterDto(), 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhItsystemMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhItsystemMasterDto> results = new List<WhItsystemMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhItsystemMasterDto obj = new WhItsystemMasterDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.IsActive = row["IsActive"].ToDataConvertBool();
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
