using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class StateData
    {
        SqlParameter[] Param(StateDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@StateId", SqlDbType.BigInt);
            Param[0].Value = obj.StateId;

            Param[1] = new SqlParameter("@StateName", SqlDbType.NVarChar);
            Param[1].Value = obj.StateName;

            Param[2] = new SqlParameter("@StateCountryId", SqlDbType.Int);
            Param[2].Value = obj.StateCountryId;

            Param[3] = new SqlParameter("@StateIsActive", SqlDbType.Bit);
            Param[3].Value = obj.StateIsActive;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(StateDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

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
        public bool Update(StateDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

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
        public StateDto GetById(long? id)
        {
            DataSet _ds = null;

            StateDto _state = new StateDto();
            _state.StateId = id;
            var Params = Param(_state, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        StateDto result = new StateDto();
                        {
                            result.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            result.StateName = row["StateName"].ToString();
                            result.StateCountryId = row["StateCountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["StateCountryId"]);
                            result.StateIsActive = row["StateIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["StateIsActive"]);
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
        public IList<StateDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            StateDto _state = new StateDto();
            _state.StateIsActive = Type;
            var Params = Param(_state, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<StateDto> results = new List<StateDto>();

                        foreach (DataRow row in rows)
                        {
                            StateDto obj = new StateDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.StateName = row["StateName"].ToString();
                            obj.CountryName = row["CountryName"].ToString();
                            obj.StateCountryId = row["StateCountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["StateCountryId"]);
                            obj.StateIsActive = row["StateIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["StateIsActive"]);

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
        public bool IsActiveOnOff(StateDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

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
        public IList<StateDto> GetAllByCountryId(int CountryId)
        {
            DataSet _ds = null;
            StateDto _stateDto = new StateDto();
            _stateDto.StateCountryId = CountryId;
            var Params = Param(_stateDto, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspState", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<StateDto> results = new List<StateDto>();

                        foreach (DataRow row in rows)
                        {
                            StateDto obj = new StateDto();

                            obj.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["StateId"]);
                            obj.StateName = row["StateName"].ToString();
                            obj.StateCountryId = row["StateCountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["StateCountryId"]);
                            obj.StateIsActive = row["StateIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["StateIsActive"]);

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