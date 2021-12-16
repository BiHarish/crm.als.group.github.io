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
    public class WHMDetailData
    {
        SqlParameter[] Param(WHMDetailDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@Id", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@LocationID", SqlDbType.BigInt);
            Param[1].Value = obj.LocationID;

            Param[2] = new SqlParameter("@TotalArea", SqlDbType.Float);
            Param[2].Value = obj.TotalArea;

            Param[3] = new SqlParameter("@AreaUtilised", SqlDbType.Float);
            Param[3].Value = obj.AreaUtilised;

            Param[4] = new SqlParameter("@AreaVacant", SqlDbType.Float);
            Param[4].Value = obj.AreaVacant;

            Param[5] = new SqlParameter("@Rate", SqlDbType.Float);
            Param[5].Value = obj.Rate;

            Param[6] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[6].Value = flag;

            Param[7] = new SqlParameter("@CreateBy", SqlDbType.Int);
            Param[7].Value = LovelySession.Lovely.User.Id;

            return Param;
        }
        public bool Insert(WHMDetailDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

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
        public bool Update(WHMDetailDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

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
        public WHMDetailDto GetById(int id)
        {
            DataSet _ds = null;

            WHMDetailDto _WHMDetailDto = new WHMDetailDto();
            _WHMDetailDto.ID = id;
            var Params = Param(_WHMDetailDto, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHMDetailDto result = new WHMDetailDto();
                        {
                            result.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            result.LocationID = row["LocationID"].ToDataConvertNullInt64();
                            result.TotalArea = row["TotalArea"].ToDataConvertNullDouble();
                            result.AreaUtilised = row["AreaUtilised"].ToDataConvertNullDouble();
                            result.AreaVacant = row["AreaVacant"].ToDataConvertNullDouble();
                            result.Rate = row["Rate"].ToDataConvertNullDouble();
                            result.CreateBy = row["CreateBy"].ToDataConvertInt64();
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
        public IList<WHMDetailDto> GetAll()
        {
            DataSet _ds = null;
            WHMDetailDto _WHMDetailDto = new WHMDetailDto();

            var Params = Param(_WHMDetailDto, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMDetailDto> results = new List<WHMDetailDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMDetailDto obj = new WHMDetailDto();

                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.LocationID = row["LocationID"].ToDataConvertNullInt64();
                            obj.TotalArea = row["TotalArea"].ToDataConvertNullDouble();
                            obj.AreaUtilised = row["AreaUtilised"].ToDataConvertNullDouble();
                            obj.AreaVacant = row["AreaVacant"].ToDataConvertNullDouble();
                            obj.Rate = row["Rate"].ToDataConvertNullDouble();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
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
        public IList<WHMDetailDto> GetBySearch(string LocationID)
        {
            DataSet _ds = null;
            WHMDetailDto _WHMDetailDto = new WHMDetailDto();
            if(LocationID !=string.Empty)
            {
                _WHMDetailDto.LocationID = LocationID.ToNullLong();
            }
            var Params = Param(_WHMDetailDto, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMDetailDto> results = new List<WHMDetailDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMDetailDto obj = new WHMDetailDto();

                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Location = row["LocationName"].ToString();
                            obj.TotalArea = row["TotalArea"].ToDataConvertNullDouble();
                            obj.AreaUtilised = row["AreaUtilised"].ToDataConvertNullDouble();
                            obj.AreaVacant = row["AreaVacant"].ToDataConvertNullDouble();
                            obj.Rate = row["Rate"].ToDataConvertNullDouble();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
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

        public IList<WHMDetailDto> GetAllSum()
        {
            DataSet _ds = null;
            WHMDetailDto _WHMDetailDto = new WHMDetailDto();

            var Params = Param(_WHMDetailDto, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMDetail", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHMDetailDto> results = new List<WHMDetailDto>();

                        foreach (DataRow row in rows)
                        {
                            WHMDetailDto obj = new WHMDetailDto();
                            obj.TotalArea = row["TotalArea"].ToDataConvertNullDouble();
                            obj.AreaUtilised = row["AreaUtilised"].ToDataConvertNullDouble();
                            obj.AreaVacant = row["AreaVacant"].ToDataConvertNullDouble();
                          
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