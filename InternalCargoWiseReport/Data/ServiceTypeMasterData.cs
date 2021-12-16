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
    public class ServiceTypeMasterData
    {
        SqlParameter[] Param(ServiceTypeMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@Id", SqlDbType.Int);
            Param[0].Value = obj.Id;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[2].Value = obj.IsActive;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@CreateBy", SqlDbType.Int);
            Param[4].Value = LovelySession.Lovely.User.Id;

            return Param;
        }

        SqlParameter[] BUParam(WhBuMasterDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@Id", SqlDbType.Int);
            Param[0].Value = obj.Id;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[2].Value = flag;

            Param[3] = new SqlParameter("@CreateBy", SqlDbType.Int);
            Param[3].Value = LovelySession.Lovely.User.Id;

            return Param;
        }
        //public bool Insert(ServiceTypeMasterDto obj)
        //{
        //    var Params = Param(obj, 1);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

        //            if (i > 0)
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return false;
        //}
        //public bool Update(ServiceTypeMasterDto obj)
        //{
        //    var Params = Param(obj, 2);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

        //            if (i > 0)
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return false;
        //}
        //public ServiceTypeMasterDto GetById(int id)
        //{
        //    DataSet _ds = null;

        //    ServiceTypeMasterDto _ServiceTypeMaster = new ServiceTypeMasterDto();
        //    _ServiceTypeMaster.Id = id;
        //    var Params = Param(_ServiceTypeMaster, 3);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHMLocation", Params);

        //            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = _ds.Tables[0].Rows[0];
        //                ServiceTypeMasterDto result = new ServiceTypeMasterDto();
        //                {
        //                    result.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
        //                    result.LocationName = row["LocationName"].ToString();
        //                    result.Region = row["Region"].ToString();
        //                }
        //                return result;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return null;
        //}
        public IList<ServiceTypeMasterDto> GetAllLocation()
        {
            DataSet _ds = null;
            ServiceTypeMasterDto _ServiceTypeMaster = new ServiceTypeMasterDto();
            
            var Params = Param(_ServiceTypeMaster, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspServiceTypeMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<ServiceTypeMasterDto> results = new List<ServiceTypeMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            ServiceTypeMasterDto obj = new ServiceTypeMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
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

        public IList<ServiceTypeMasterDto> GetAllWarehouseServiceType()
        {
            DataSet _ds = null;
            ServiceTypeMasterDto _ServiceTypeMaster = new ServiceTypeMasterDto();

            var Params = Param(_ServiceTypeMaster, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspServiceTypeMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<ServiceTypeMasterDto> results = new List<ServiceTypeMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            ServiceTypeMasterDto obj = new ServiceTypeMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
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

        public IList<ServiceTypeMasterDto> GetAllTransportationServiceType()
        {
            DataSet _ds = null;
            ServiceTypeMasterDto _ServiceTypeMaster = new ServiceTypeMasterDto();

            var Params = Param(_ServiceTypeMaster, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspServiceTypeMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<ServiceTypeMasterDto> results = new List<ServiceTypeMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            ServiceTypeMasterDto obj = new ServiceTypeMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
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

        public IList<ServiceTypeMasterDto> GetAllVehicleType()
        {
            DataSet _ds = null;
            ServiceTypeMasterDto _ServiceTypeMaster = new ServiceTypeMasterDto();

            var Params = Param(_ServiceTypeMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspServiceTypeMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<ServiceTypeMasterDto> results = new List<ServiceTypeMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            ServiceTypeMasterDto obj = new ServiceTypeMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["VehicleType"].ToString();
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




        //********************************************************************WhBuMaster********************************************************

        public WhBuMasterDto BUGetByID()
        {
            DataSet _ds = null;
            WhBuMasterDto _whBuMaster = new WhBuMasterDto();

            var Params = BUParam(_whBuMaster, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBuMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                       // DataRowCollection rows = _ds.Tables[0].Rows;
                        DataRow row = _ds.Tables[0].Rows[0];
                     
                            WhBuMasterDto obj = new WhBuMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
                          
                     
                        return obj;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public IList<WhBuMasterDto> BUGetAll()
        {
            DataSet _ds = null;
            WhBuMasterDto _whBuMaster = new WhBuMasterDto();

            var Params = BUParam(_whBuMaster, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBuMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhBuMasterDto> results = new List<WhBuMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhBuMasterDto obj = new WhBuMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
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

        public IList<WhBuMasterDto> GetOnlyCustomBrokerage()
        {
            DataSet _ds = null;
            WhBuMasterDto _whBuMaster = new WhBuMasterDto();

            var Params = BUParam(_whBuMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhBuMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhBuMasterDto> results = new List<WhBuMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WhBuMasterDto obj = new WhBuMasterDto();

                            obj.Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.Name = row["Name"].ToString();
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