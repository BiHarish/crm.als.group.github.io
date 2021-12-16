using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class CountryData
    {
        SqlParameter[] Param(CountryDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@CountryId", SqlDbType.Int);
            Param[0].Value = obj.CountryId;

            Param[1] = new SqlParameter("@CountryName", SqlDbType.NVarChar);
            Param[1].Value = obj.CountryName;

            Param[2] = new SqlParameter("@CountryIsActive", SqlDbType.Bit);
            Param[2].Value = obj.CountryIsActive;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            return Param;
        }
        public bool Insert(CountryDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Params);

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
        public bool Update(CountryDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Params);

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
        public CountryDto GetById(int id)
        {
            DataSet _ds = null;

            CountryDto _country = new CountryDto();
            _country.CountryId = id;
            var Params = Param(_country, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        CountryDto result = new CountryDto();
                        {
                            result.CountryId = row["CountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CountryId"]);
                            result.CountryName = row["CountryName"].ToString();
                            result.CountryIsActive = row["CountryIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CountryIsActive"]);
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
        public IList<CountryDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            CountryDto _country = new CountryDto();
            _country.CountryIsActive = Type;
            var Params = Param(_country, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CountryDto> results = new List<CountryDto>();

                        foreach (DataRow row in rows)
                        {
                            CountryDto obj = new CountryDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.CountryId = row["CountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CountryId"]);
                            obj.CountryName = row["CountryName"].ToString();
                            obj.CountryIsActive = row["CountryIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CountryIsActive"]);

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
        public bool IsActiveOnOff(CountryDto obj)
        {
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Params);

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