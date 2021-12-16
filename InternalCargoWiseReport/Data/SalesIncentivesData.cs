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
    public class SalesIncentivesData
    {
        SqlParameter[] Param(GeneratePayDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@AssmentYear", SqlDbType.VarChar);
            Param[0].Value = obj.AssmentYear;

            Param[1] = new SqlParameter("@Q", SqlDbType.Int);
            Param[1].Value = obj.Q;

            Param[2] = new SqlParameter("@grossDt", SqlDbType.Structured);
            Param[2].Value = obj.grossDt;

            Param[3] = new SqlParameter("@interestDt", SqlDbType.Structured);
            Param[3].Value = obj.interestDt;

            Param[4] = new SqlParameter("@Close", SqlDbType.Int);
            Param[4].Value = obj.Close;

            Param[5] = new SqlParameter("@isFinal", SqlDbType.Bit);
            Param[5].Value = obj.isFinal;

            Param[6] = new SqlParameter("@flag", SqlDbType.Int);
            Param[6].Value = flag;

            return Param;
        }
        public bool Insert(GeneratePayDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);

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
        public QuaterDateDto GetStartEndDate(GeneratePayDto obj)
        {
            DataSet _ds = null;

            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    QuaterDateDto QObj = new QuaterDateDto();

                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];

                        QObj.datefrom = row["IAM_StartDate"].ToDataConvertDateTime();
                    }

                    if (_ds != null && _ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[1].Rows[0];

                        QObj.dateto = row["IAM_EndDate"].ToDataConvertDateTime();
                    }

                    if (_ds != null && _ds.Tables.Count > 2 && _ds.Tables[2].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[2].Rows[0];

                        QObj.firstdate = row["IAM_StartDate"].ToDataConvertDateTime();
                    }

                    return QObj;
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
        public DataSet GenerateBySendProfit(GeneratePayDto obj)
        {
            DataSet _ds = null;

            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);
                    return _ds;
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
        public DataSet GenerateByInterest(GeneratePayDto obj)
        {
            DataSet _ds = null;

            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);
                    return _ds;
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
        public DataSet DownloadGenerate(GeneratePayDto obj)
        {
            DataSet _ds = null;

            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);
                    return _ds;
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
        public DataSet getIncentiveDetails(string Code, string AssYear)
        {
            SqlParameter[] sp = new SqlParameter[3];

            sp[0] = new SqlParameter("@CargoCode", SqlDbType.VarChar);
            sp[0].Value =Code;
            sp[1] = new SqlParameter("@AssYear", SqlDbType.NVarChar);
            sp[1].Value = AssYear;
            sp[2] = new SqlParameter("@Flag", SqlDbType.BigInt);
            sp[2].Value = 8;
           

            if (sp != null )
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", sp);

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