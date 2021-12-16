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
    public class CtcMemberData
    {
        SqlParameter[] Param(CtcMemberDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@Id", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@CTCMemberId", SqlDbType.BigInt);
            Param[1].Value = obj.CTCMemberId;

            Param[2] = new SqlParameter("@CTCMemberPeriodId", SqlDbType.Float);
            Param[2].Value = obj.CTCMemberPeriodId;

            Param[3] = new SqlParameter("@CTCMemberAmount", SqlDbType.Float);
            Param[3].Value = obj.CTCMemberAmount;

            Param[4] = new SqlParameter("@flag", SqlDbType.Float);
            Param[4].Value = flag;

            return Param;
        }
        public bool Insert(CtcMemberDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);

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
        public bool Update(CtcMemberDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);

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
        public CtcMemberDto GetById(int id)
        {
            DataSet _ds = null;

            CtcMemberDto _CtcMemberDto = new CtcMemberDto();
            _CtcMemberDto.ID = id;
            var Params = Param(_CtcMemberDto, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        CtcMemberDto result = new CtcMemberDto();
                        {
                            result.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            result.CTCMemberId = row["CTCMemberId"].ToDataConvertNullInt64();
                            result.CTCMemberPeriodId = row["CTCMemberPeriodId"].ToDataConvertNullInt64();
                            result.CTCMemberAmount = row["CTCMemberAmount"].ToDataConvertNullDouble();

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
        public IList<CtcMemberDto> GetAll()
        {
            DataSet _ds = null;
            CtcMemberDto _CtcMemberDto = new CtcMemberDto();

            var Params = Param(_CtcMemberDto, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CtcMemberDto> results = new List<CtcMemberDto>();

                        foreach (DataRow row in rows)
                        {
                            CtcMemberDto obj = new CtcMemberDto();

                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.CTCMemberId = row["CTCMemberId"].ToDataConvertNullInt64();
                            obj.CTCMemberPeriodId = row["CTCMemberPeriodId"].ToDataConvertNullInt64();
                            obj.CTCMemberAmount = row["CTCMemberAmount"].ToDataConvertNullDouble();
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
        public IList<CtcMemberDto> GetByYear(string YearID)
        {
            DataSet _ds = null;
            CtcMemberDto _CtcMemberDto = new CtcMemberDto();
            if(YearID != string.Empty)
            {
                _CtcMemberDto.CTCMemberPeriodId = YearID.ToLong();
            }
            var Params = Param(_CtcMemberDto, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);
                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CtcMemberDto> results = new List<CtcMemberDto>();

                        foreach (DataRow row in rows)
                        {
                            CtcMemberDto obj = new CtcMemberDto();
                            obj.ID = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]);
                            obj.CTCMemberId = row["CTCMemberID"].ToDataConvertNullInt64();
                            obj.CTCMemberPeriodId = row["CTCMemberPeriodId"].ToDataConvertNullInt64();
                            obj.CTCMemberAmount = row["CTCMemberAmount"].ToDataConvertNullDouble();
                            obj.CTCMemberName = row["Name"].ToString();
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
        public IList<CtcMemberDto> GetAllYear()
        {
            DataSet _ds = null;
            CtcMemberDto _CtcMemberDto = new CtcMemberDto();

            var Params = Param(_CtcMemberDto, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CtcMemberDto> results = new List<CtcMemberDto>();

                        foreach (DataRow row in rows)
                        {
                            CtcMemberDto obj = new CtcMemberDto();
                            obj.AssessmentYearID = row["AssessmentMasterId"].ToDataConvertInt64();
                            obj.AssessmentYear = row["AssessmentMasterPeriod"].ToString();
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