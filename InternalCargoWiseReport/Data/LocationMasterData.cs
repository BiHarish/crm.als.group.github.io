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
    public class LocationMasterData
    {
        SqlParameter[] Param(LocationMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@Id", SqlDbType.Int);
            Param[0].Value = obj.Id;

            Param[1] = new SqlParameter("@LocationName", SqlDbType.NVarChar);
            Param[1].Value = obj.LocationName;

            Param[2] = new SqlParameter("@Region", SqlDbType.NVarChar);
            Param[2].Value = obj.Region;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@CreateBy", SqlDbType.Int);
            Param[4].Value = LovelySession.Lovely.User.Id;

            return Param;
        }
        public bool Insert(LocationMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

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
        public bool Update(LocationMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

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
        public LocationMasterDto GetById(int id)
        {
            DataSet _ds = null;

            LocationMasterDto _LocationMaster = new LocationMasterDto();
            _LocationMaster.Id = id;
            var Params = Param(_LocationMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        LocationMasterDto result = new LocationMasterDto();
                        {
                            result.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            result.LocationName = row["LocationName"].ToString();
                            result.Region = row["Region"].ToString();
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
        public IList<LocationMasterDto> GetAll()
        {
            DataSet _ds = null;
            LocationMasterDto _LocationMaster = new LocationMasterDto();
            
            var Params = Param(_LocationMaster, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<LocationMasterDto> results = new List<LocationMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            LocationMasterDto obj = new LocationMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.LocationName = row["LocationName"].ToString();
                            obj.Region = row["Region"].ToString();
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

        public IList<LocationMasterDto> GetBySearch(string LocationID,string Region)
        {
            DataSet _ds = null;
            LocationMasterDto _LocationMaster = new LocationMasterDto();
            if(LocationID != string.Empty)
            {
                _LocationMaster.Id = LocationID.ToNullLong();
            }
            _LocationMaster.Region = Region;
            var Params = Param(_LocationMaster,5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<LocationMasterDto> results = new List<LocationMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            LocationMasterDto obj = new LocationMasterDto();
                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.LocationName = row["LocationName"].ToString();
                            obj.Region = row["Region"].ToString();
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