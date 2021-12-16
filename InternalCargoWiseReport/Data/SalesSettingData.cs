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
    public class SalesSettingData
    {
        SqlParameter[] Param(SalesSettingDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[11];
            Param[0] = new SqlParameter("@SalesSettingId", SqlDbType.BigInt);
            Param[0].Value = obj.SalesSettingId;

            Param[1] = new SqlParameter("@SalesSettingPeriodId", SqlDbType.BigInt);
            Param[1].Value = obj.SalesSettingPeriodId;

            Param[2] = new SqlParameter("@SalesSettingCompanyDivisionId", SqlDbType.Float);
            Param[2].Value = obj.SalesSettingCompanyDivisionId;

            Param[3] = new SqlParameter("@SalesSettingEligibleOnCTC", SqlDbType.Float);
            Param[3].Value = obj.SalesSettingEligibleOnCTC;

            Param[4] = new SqlParameter("@SalesSettingPercentOnCTC", SqlDbType.Float);
            Param[4].Value = obj.SalesSettingPercentOnCTC;

            Param[5] = new SqlParameter("@SalesSettingPercentOnOverAmount", SqlDbType.Float);
            Param[5].Value = obj.SalesSettingPercentOnOverAmount;

            Param[6] = new SqlParameter("@SalesSettingPercentOnNext", SqlDbType.Float);
            Param[6].Value = obj.SalesSettingPercentOnNext;

            Param[7] = new SqlParameter("@SalesSettingPercentOnAfterSettle", SqlDbType.Float);
            Param[7].Value = obj.SalesSettingPercentOnAfterSettle;

            Param[8] = new SqlParameter("@SalesSettingPercentOnNextYear", SqlDbType.Float);
            Param[8].Value = obj.SalesSettingPercentOnNextYear;

            Param[9] = new SqlParameter("@flag", SqlDbType.Float);
            Param[9].Value = flag;

            Param[10] = new SqlParameter("@SalesSettingQ", SqlDbType.BigInt);
            Param[10].Value = obj.SalesSettingQ;
            



            return Param;
        }
        public bool Insert(SalesSettingDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

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
        public bool Update(SalesSettingDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

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
        public SalesSettingDto GetById(int id)
        {
            DataSet _ds = null;

            SalesSettingDto _CtcMemberDto = new SalesSettingDto();
            _CtcMemberDto.SalesSettingId = id;
            var Params = Param(_CtcMemberDto, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        SalesSettingDto result = new SalesSettingDto();
                        {
                            result.SalesSettingId = row["SalesSettingId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SalesSettingId"]);
                            result.SalesSettingPeriodId = row["SalesSettingPeriodId"].ToDataConvertNullInt64();
                            result.SalesSettingCompanyDivisionId = row["SalesSettingCompanyDivisionId"].ToDataConvertNullInt64();
                            result.SalesSettingEligibleOnCTC = row["SalesSettingEligibleOnCTC"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnCTC = row["SalesSettingPercentOnCTC"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnOverAmount = row["SalesSettingPercentOnOverAmount"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnNext = row["SalesSettingPercentOnNext"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnAfterSettle = row["SalesSettingPercentOnAfterSettle"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnNextYear = row["SalesSettingPercentOnNextYear"].ToDataConvertNullDouble();
                            result.SalesSettingQ = row["SalesSettingQ"].ToDataConvertNullInt64();

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
        public IList<SalesSettingDto> GetAll()
        {
            DataSet _ds = null;
            SalesSettingDto _SalesSettingDto = new SalesSettingDto();

            var Params = Param(_SalesSettingDto, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<SalesSettingDto> results = new List<SalesSettingDto>();

                        foreach (DataRow row in rows)
                        {
                            SalesSettingDto obj = new SalesSettingDto();

                            obj.SalesSettingId = row["SalesSettingId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SalesSettingId"]);
                            obj.SalesSettingPeriodId = row["SalesSettingPeriodId"].ToDataConvertNullInt64();
                            obj.SalesSettingCompanyDivisionId = row["SalesSettingCompanyDivisionId"].ToDataConvertNullInt64();
                            obj.SalesSettingEligibleOnCTC = row["SalesSettingEligibleOnCTC"].ToDataConvertNullDouble();
                            obj.SalesSettingPercentOnCTC = row["SalesSettingPercentOnCTC"].ToDataConvertNullDouble();
                            obj.SalesSettingPercentOnOverAmount = row["SalesSettingPercentOnOverAmount"].ToDataConvertNullDouble();
                            obj.SalesSettingPercentOnNext = row["SalesSettingPercentOnNext"].ToDataConvertNullDouble();
                            obj.SalesSettingPercentOnAfterSettle = row["SalesSettingPercentOnAfterSettle"].ToDataConvertNullDouble();
                            obj.SalesSettingPercentOnNextYear = row["SalesSettingPercentOnNextYear"].ToDataConvertNullDouble();
                            obj.SalesSettingQ = row["SalesSettingQ"].ToDataConvertNullInt64();
                            
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
        public SalesSettingDto GetByYearAndDivisionIDandQuarter(long YearID, long CompanyID, long QuarterID)
        {
            DataSet _ds = null;
            SalesSettingDto _SalesSettingDto = new SalesSettingDto();
            _SalesSettingDto.SalesSettingPeriodId=YearID;
            _SalesSettingDto.SalesSettingCompanyDivisionId = CompanyID;
            _SalesSettingDto.SalesSettingQ = QuarterID;

            var Params = Param(_SalesSettingDto, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        SalesSettingDto result = new SalesSettingDto();
                        {
                            result.SalesSettingId = row["SalesSettingId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SalesSettingId"]);
                            result.SalesSettingPeriodId = row["SalesSettingPeriodId"].ToDataConvertNullInt64();
                            result.SalesSettingCompanyDivisionId = row["SalesSettingCompanyDivisionId"].ToDataConvertNullInt64();
                            result.SalesSettingEligibleOnCTC = row["SalesSettingEligibleOnCTC"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnCTC = row["SalesSettingPercentOnCTC"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnOverAmount = row["SalesSettingPercentOnOverAmount"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnNext = row["SalesSettingPercentOnNext"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnAfterSettle = row["SalesSettingPercentOnAfterSettle"].ToDataConvertNullDouble();
                            result.SalesSettingPercentOnNextYear = row["SalesSettingPercentOnNextYear"].ToDataConvertNullDouble();
                            result.SalesSettingQ = row["SalesSettingQ"].ToDataConvertNullInt64();

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

        public IList<SalesSettingDto> GetAllCompany()
        {
            DataSet _ds = null;
            SalesSettingDto _SalesSettingDto = new SalesSettingDto();

            var Params = Param(_SalesSettingDto, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesSetting", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<SalesSettingDto> results = new List<SalesSettingDto>();

                        foreach (DataRow row in rows)
                        {
                            SalesSettingDto obj = new SalesSettingDto();

                            obj.CompanyDivisionId = row["CompanyDivisionId"].ToDataConvertInt64();
                            obj.CompanyDivisionName = row["CompanyDivisionName"].ToString();
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