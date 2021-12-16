using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public class CommonData
    {
        SqlParameter[] Param(CommonDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[6];
            Param[0] = new SqlParameter("@Code", SqlDbType.VarChar);
            Param[0].Value = obj.Code;

            Param[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
            Param[1].Value = obj.DateFrom;

            Param[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
            Param[2].Value = obj.DateTo;

            Param[3] = new SqlParameter("@Position", SqlDbType.Char);
            Param[3].Value = obj.Position;

            Param[4] = new SqlParameter("@Group", SqlDbType.Char);
            Param[4].Value = obj.Group;

            Param[5] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[5].Value = flag;

            return Param;
        }
        public IList<UserDto> GetCashTally(CommonDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommonReport", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Name = row["Name"].ToString();
                            obj.Code = row["Code"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.Amount = row["Amount"] == DBNull.Value ? 0 : float.Parse(row["Amount"].ToString());

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
        public IList<UserDto> GetGeneology(CommonDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommonReport", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Name = row["Name"].ToString();
                            obj.Code = row["Code"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.Position = row["Position"].ToString();
                            obj.PinNumber = row["PinNumber"].ToString();
                            obj.PinId = row["PV"] == DBNull.Value ? 0 : Convert.ToInt32(row["PV"]);
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? Utility.Utility.GetIndianDateTime() : Convert.ToDateTime(row["JoiningDate"]);

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
        public DataTable GetChainList(CommonDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommonReport", Params);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds.Tables[0];
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
        public IList<UserDto> GetSaleReport(CommonDto data)
        {
            DataSet _ds = null;
            var Params = Param(data, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommonReport", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<UserDto> results = new List<UserDto>();

                        foreach (DataRow row in rows)
                        {
                            UserDto obj = new UserDto();

                            obj.SrNo = row["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(row["SrNo"]);
                            obj.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);
                            obj.Name = row["Name"].ToString();
                            obj.Code = row["Code"].ToString();
                            obj.MobileNo = row["MobileNo"].ToString();
                            obj.IntroducerCode = row["IntroducerCode"].ToString();
                            obj.JoiningDate = row["JoiningDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["JoiningDate"]);
                            obj.BV = row["BV"] == DBNull.Value ? 0 : Convert.ToInt32(row["BV"]);

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
        public DateTime GetLastProcessDate()
        {
            var Params = Param(new CommonDto(), 5);
            if (Params != null)
            {
                try
                {
                    Object obj = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspCommonReport", Params);
                    if (obj != null)
                    {
                        return (DateTime)obj;
                    }
                }
                catch (Exception ex)
                {
                    return Utility.Utility.GetIndianDateTime();
                }
                finally
                {

                }
            }
            return Utility.Utility.GetIndianDateTime();
        }
        public bool PayoutProcess(CommonDto obj)
        {
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
            Param[0].Value = obj.DateFrom;

            Param[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
            Param[1].Value = obj.DateTo;

            if (Param != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspProcessOrc", Param);

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
    }
}