using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class CityData
    {
        SqlParameter[] Param(CityDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@CityId", SqlDbType.BigInt);
            Param[0].Value = obj.CityId;

            Param[1] = new SqlParameter("@CityName", SqlDbType.NVarChar);
            Param[1].Value = obj.CityName;

            Param[2] = new SqlParameter("@CityStateId", SqlDbType.BigInt);
            Param[2].Value = obj.CityStateId;

            Param[3] = new SqlParameter("@CityIsActive", SqlDbType.Bit);
            Param[3].Value = obj.CityIsActive;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(CityDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

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
        public bool Update(CityDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

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
        public CityDto GetById(long? id)
        {
            DataSet _ds = null;

            CityDto _city = new CityDto();
            _city.CityId = id;
            var Params = Param(_city, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        CityDto result = new CityDto();
                        {
                            result.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            result.CityName = row["CityName"].ToString();
                            result.CityStateId = row["CityStateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityStateId"]);
                            result.CityIsActive = row["CityIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CityIsActive"]);
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
        public IList<CityDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            CityDto _city = new CityDto();
            _city.CityIsActive = Type;
            var Params = Param(_city, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CityDto> results = new List<CityDto>();

                        foreach (DataRow row in rows)
                        {
                            CityDto obj = new CityDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.CityName = row["CityName"].ToString();
                            obj.CityStateId = row["CityStateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityStateId"]);
                            obj.CityIsActive = row["CityIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CityIsActive"]);
                            obj.StateName = row["StateName"].ToString();

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
        public IList<CityDto> GetAllByStateId(long StateId)
        {
            DataSet _ds = null;
            CityDto _cityDto = new CityDto();
            _cityDto.CityStateId = StateId;
            var Params = Param(_cityDto, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CityDto> results = new List<CityDto>();

                        foreach (DataRow row in rows)
                        {
                            CityDto obj = new CityDto();

                            obj.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityId"]);
                            obj.CityName = row["CityName"].ToString();
                            obj.CityStateId = row["CityStateId"] == DBNull.Value ? 0 : Convert.ToInt64(row["CityStateId"]);
                            obj.CityIsActive = row["CityIsActive"] == DBNull.Value ? false : Convert.ToBoolean(row["CityIsActive"]);

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
        public bool IsActiveOnOff(CityDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCity", Params);

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
        //glb Company
        SqlParameter[] ComParam(GlbCompanyDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@GC_Code", SqlDbType.NVarChar);
            Param[1].Value = obj.GC_Code;

            Param[2] = new SqlParameter("@GC_Name", SqlDbType.NVarChar);
            Param[2].Value = obj.GC_Name;

            Param[3] = new SqlParameter("@GC_Address1", SqlDbType.NVarChar);
            Param[3].Value = obj.GC_Address1;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;

            return Param;
        }

        public IList<GlbCompanyDto> GetAllGlbCompany()
        {
            DataSet _ds = null;
            GlbCompanyDto _com = new GlbCompanyDto();
            var Params = ComParam(_com, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspGlbCompany", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<GlbCompanyDto> results = new List<GlbCompanyDto>();

                        foreach (DataRow row in rows)
                        {
                            GlbCompanyDto obj = new GlbCompanyDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.GC_Code = row["GC_Code"].ToString();
                            obj.GC_Name = row["GC_Name"].ToString();

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