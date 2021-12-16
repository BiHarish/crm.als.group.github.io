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
    public class BudgetUploadData
    {
        SqlParameter[] Param(BudgetDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = flag;
            Param[1] = new SqlParameter("@BudgetUploadDt", SqlDbType.Structured);
            Param[1].Value = obj.dt;

            return Param;
        }

        SqlParameter[] ParamBudget(BudgetDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = flag;


            return Param;
        }
        public bool Insert(BudgetDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {

                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspDownloadBudgetFormat", Params);

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

        public DataSet BudgetUploadFormat()
        {
            BudgetDto obj = new BudgetDto();
            DataSet ds = new DataSet();

            var Params = ParamBudget(obj, 1);
            if (Params != null)
            {
                try
                {
                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBudgetData", Params);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            return ds;
        }


        public BudgetDto getQueryForUpload(BudgetDto obj)
        {
            DataSet _ds = null;

            var Params = ParamBudget(obj, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBudgetData", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        BudgetDto result = new BudgetDto();
                        {
                            result.query = row["Query"].ToString();
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

        public DataSet getAll()
        {
            BudgetDto obj = new BudgetDto();

            var Params = ParamBudget(obj, 3);

            if (Params != null )
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspBudgetData", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
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

    }
}