using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalCargoWiseReport.Models;

namespace ICWR.Data
{
    public class WHMTransData
    {
        SqlParameter[] Param(WHMTransDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@Id", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@WHMID", SqlDbType.BigInt);
            Param[1].Value = obj.WHMID;

            Param[2] = new SqlParameter("@TotArea", SqlDbType.Float);
            Param[2].Value = obj.TotArea;

            Param[3] = new SqlParameter("@OccupiedArea", SqlDbType.Float);
            Param[3].Value = obj.OccupiedArea;

            Param[4] = new SqlParameter("@Rate", SqlDbType.Float);
            Param[4].Value = obj.Rate;

            Param[5] = new SqlParameter("@flag", SqlDbType.BigInt);
            Param[5].Value = flag;

            Param[6] = new SqlParameter("@CustomerName", SqlDbType.NVarChar);
            Param[6].Value = obj.CustomerName;

            Param[7] = new SqlParameter("@Vacant", SqlDbType.NVarChar);
            Param[7].Value = obj.Vacant;
            

            return Param;
        }
        public bool Insert(WHMTransDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

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
        public bool Update(WHMTransDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

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
        public WHMTransDto GetById(int id)
        {
            DataSet _ds = null;

            WHMTransDto _WHMDetailDto = new WHMTransDto();
            _WHMDetailDto.ID = id;
            var Params = Param(_WHMDetailDto, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHMTransDto result = new WHMTransDto();
                        {
                            result.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            result.WHMID = row["WHMID"].ToDataConvertNullInt64();
                            result.CustomerName = row["CustomerName"].ToString();
                            result.TotArea = row["TotArea"].ToDataConvertNullDouble();
                            result.OccupiedArea = row["OccupiedArea"].ToDataConvertNullDouble();
                            result.Rate = row["Rate"].ToDataConvertNullDouble();
                            result.Vacant= row["Vacant"].ToDataConvertNullDouble();
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
        public IList<WHMTransDto> GetAll()
        {
            DataSet _ds = null;
            WHMTransDto _WHMDetailDto = new WHMTransDto();

            var Params = Param(_WHMDetailDto, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMTransDto> results = new List<WHMTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMTransDto obj = new WHMTransDto();

                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.WHMID = row["WHMID"].ToDataConvertNullInt64();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.TotArea = row["TotArea"].ToDataConvertNullDouble();
                            obj.OccupiedArea = row["OccupiedArea"].ToDataConvertNullDouble();
                            obj.Rate = row["Rate"].ToDataConvertNullDouble();
                            obj.LocationName = row["LocationName"].ToString();
                            obj.Vacant = row["Vacant"].ToDataConvertNullDouble();
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
        public IList<WHMTransDto> GetBySearch(string CustomerName,string LocationID)
        {
            DataSet _ds = null;
            WHMTransDto _WHMDetailDto = new WHMTransDto();
            if (CustomerName != string.Empty)
            {
                _WHMDetailDto.CustomerName = CustomerName.ToString();
            }
            if (LocationID != string.Empty)
            {
                _WHMDetailDto.WHMID = LocationID.ToNullLong();
            }
            var Params = Param(_WHMDetailDto, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMTransDto> results = new List<WHMTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMTransDto obj = new WHMTransDto();

                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.WHMID = row["WHMID"].ToDataConvertNullInt64();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.TotArea = row["TotArea"].ToDataConvertNullDouble();
                            obj.OccupiedArea = row["OccupiedArea"].ToDataConvertNullDouble();
                            obj.Rate = row["Rate"].ToDataConvertNullDouble();
                            obj.LocationName = row["LocationName"].ToString();
                            obj.Vacant = row["Vacant"].ToDataConvertNullDouble();
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
        public IList<WHMTransDto> GetAllCustomers()
        {
            DataSet _ds = null;
            WHMTransDto _WHMDetailDto = new WHMTransDto();

            var Params = Param(_WHMDetailDto, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMTransDto> results = new List<WHMTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMTransDto obj = new WHMTransDto();
                            obj.CustomerName = row["CustomerName"].ToString();
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