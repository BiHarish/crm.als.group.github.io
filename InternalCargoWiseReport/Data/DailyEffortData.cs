using ICWR.Data.Utility;
using InternalCargoWiseReport.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Data
{
    public class DailyEffortData
    {
        SqlParameter[] Param(DailyEffortDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[18];
            Param[0] = new SqlParameter("@sed_id", SqlDbType.BigInt);
            Param[0].Value = obj.sed_id;
            Param[1] = new SqlParameter("@sed_om_id", SqlDbType.BigInt);
            Param[1].Value = obj.sed_om_id;
            Param[2] = new SqlParameter("@sed_requestdate", SqlDbType.DateTime);
            Param[2].Value = obj.sed_requestdate;
            Param[3] = new SqlParameter("@sed_requestedby", SqlDbType.NVarChar);
            Param[3].Value = obj.sed_requestedby;
            Param[4] = new SqlParameter("@sed_applicationmodule", SqlDbType.NVarChar);
            Param[4].Value = obj.sed_applicationmodule;
            Param[5] = new SqlParameter("@sed_businessjustification", SqlDbType.NVarChar);
            Param[5].Value = obj.sed_businessjustification;
            Param[6] = new SqlParameter("@sed_effortestimate", SqlDbType.NVarChar);
            Param[6].Value = obj.sed_effortestimate;
            Param[7] = new SqlParameter("@sed_approvedby", SqlDbType.NVarChar);
            Param[7].Value = obj.sed_approvedby;
            Param[8] = new SqlParameter("@sed_createdby", SqlDbType.BigInt);
            Param[8].Value = obj.sed_createdby;
            Param[9] = new SqlParameter("@sed_createdon", SqlDbType.DateTime);
            Param[9].Value = obj.sed_createdon;
            Param[10] = new SqlParameter("@sed_modifiedby", SqlDbType.BigInt);
            Param[10].Value = obj.sed_modifiedby;
            Param[11] = new SqlParameter("@modifiedon", SqlDbType.DateTime);
            Param[11].Value = obj.modifiedon;
            Param[12] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[12].Value = obj.ID;
            Param[13] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[13].Value = obj.Name;
            Param[14] = new SqlParameter("@Application", SqlDbType.NVarChar);
            Param[14].Value = obj.Application;
            Param[15] = new SqlParameter("@sed_filename", SqlDbType.NVarChar);
            Param[15].Value = obj.sed_filename;
            Param[16] = new SqlParameter("@sed_effortcreatedby", SqlDbType.NVarChar);
            Param[16].Value = obj.sed_effortcreatedby;
            Param[17] = new SqlParameter("@flag", SqlDbType.BigInt);
            Param[17].Value = flag;
            return Param;
        }
        public long Insert(DailyEffortDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

                    if (i != null)
                    {
                        return i.ToDataConvertInt64();
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {

                }
            }
            return 0;
        }
        public bool Update(DailyEffortDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

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
        public DataTable GetOrganisation(DailyEffortDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds.Tables[0];
                    }
                    return null;
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

        public DataTable GetAll(int? Id)
        {
            DailyEffortDto obj = new DailyEffortDto();
            obj.sed_om_id = Id;
            DataSet _ds = null;
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds.Tables[0];
                    }
                    return null;
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

        public DataTable GetAllById(int? Id)
        {
            DailyEffortDto obj = new DailyEffortDto();
            obj.sed_id = Id;
            DataSet _ds = null;
            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds.Tables[0];
                    }
                    return null;
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
        public DataTable GetDetails(DailyEffortDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSupportEffortDetails", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds.Tables[0];
                    }
                    return null;
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
    }
}