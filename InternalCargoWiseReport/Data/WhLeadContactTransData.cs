using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhLeadContactTransData
    {
        SqlParameter[] Param(WhLeadContactTransDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@Designation", SqlDbType.NVarChar);
            Param[2].Value = obj.Designation;

            Param[3] = new SqlParameter("@MailID", SqlDbType.NVarChar);
            Param[3].Value = obj.MailID;

            Param[4] = new SqlParameter("@PhoneNo", SqlDbType.NVarChar);
            Param[4].Value = obj.PhoneNo;

            Param[5] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[5].Value = flag;

            Param[6] = new SqlParameter("@WhLeadID", SqlDbType.BigInt);
            Param[6].Value = obj.WhLeadID;

            return Param;
        }
        public bool Insert(WhLeadContactTransDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadContactTrans", Params);

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
        public bool Update(WhLeadContactTransDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadContactTrans", Params);

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
        public WhLeadContactTransDto GetById(long id)
        {
            DataSet _ds = null;

            WhLeadContactTransDto _WhLeadContactTrans = new WhLeadContactTransDto();
            _WhLeadContactTrans.ID = id;
            var Params = Param(_WhLeadContactTrans, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadContactTrans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhLeadContactTransDto result = new WhLeadContactTransDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            result.Name = row["Name"].ToString();
                            result.Designation = row["Designation"].ToString();
                            result.MailID = row["MailID"].ToString();
                            result.PhoneNo = row["PhoneNo"].ToString();
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
        public IList<WhLeadContactTransDto> GetAll(bool Type)
        {
            DataSet _ds = null;
            WhLeadContactTransDto _WhLeadContactTrans = new WhLeadContactTransDto();
            var Params = Param(_WhLeadContactTrans, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadContactTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhLeadContactTransDto> results = new List<WhLeadContactTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WhLeadContactTransDto obj = new WhLeadContactTransDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Designation = row["Designation"].ToString();
                            obj.MailID = row["MailID"].ToString();
                            obj.PhoneNo = row["PhoneNo"].ToString();

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

        public IList<WhLeadContactTransDto> getByWhLeadID(long WhLeadID)
        {
            DataSet _ds = null;
            WhLeadContactTransDto _WhLeadContactTrans = new WhLeadContactTransDto();
            _WhLeadContactTrans.WhLeadID = WhLeadID;
            var Params = Param(_WhLeadContactTrans, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadContactTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhLeadContactTransDto> results = new List<WhLeadContactTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WhLeadContactTransDto obj = new WhLeadContactTransDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Designation = row["Designation"].ToString();
                            obj.MailID = row["MailID"].ToString();
                            obj.PhoneNo = row["PhoneNo"].ToString();
                            obj.RowNumber = row["RowNumber"].ToString();

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