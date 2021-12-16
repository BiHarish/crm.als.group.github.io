using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace ICWR.Data
{
    public class WhCreditNoteData
    {
        SqlParameter[] Param(WhCreditNoteDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[9];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@WhLeadID", SqlDbType.BigInt);
            Param[1].Value = obj.WhLeadID;

            Param[2] = new SqlParameter("@CustomerID", SqlDbType.BigInt);
            Param[2].Value = obj.CustomerID;

            Param[3] = new SqlParameter("@CreditRating", SqlDbType.Float);
            Param[3].Value = obj.CreditRating;

            Param[4] = new SqlParameter("@CreditLimit", SqlDbType.Float);
            Param[4].Value = obj.CreditLimit;

            Param[5] = new SqlParameter("@CreditDays", SqlDbType.Float);
            Param[5].Value = obj.CreditDays;

           

            Param[6] = new SqlParameter("@CreditFileName", SqlDbType.NVarChar);
            Param[6].Value = obj.CreditFileName;

            Param[7] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[7].Value = obj.CreateBy;
            Param[8] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[8].Value = flag;


           



            return Param;
        }


        public long Insert(WhCreditNoteDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCreditNote", Params);

                    if (i != null)
                    {
                        return Convert.ToInt64(i);
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
                finally
                {

                }
            }
            return -1;
        }

        public bool Update(WhCreditNoteDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCreditNote", Params);

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
        public WhCreditNoteDto GetById(long id)
        {
            DataSet _ds = null;

            WhCreditNoteDto _WhCreditNote = new WhCreditNoteDto();
            _WhCreditNote.ID = id;
            var Params = Param(_WhCreditNote, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCreditNote", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhCreditNoteDto result = new WhCreditNoteDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            result.CustomerID = row["CustomerID"].ToDataConvertNullInt64();
                            result.CreditRating = row["CreditRating"].ToDataConvertDouble();
                            result.CreditLimit = row["CreditLimit"].ToDataConvertDouble();
                            result.CreditDays = row["CreditDays"].ToDataConvertDouble();
                            result.CreditFileName = row["CreditFileName"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
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
        public IList<WhCreditNoteDto> GetAll()
        {
            DataSet _ds = null;
            WhCreditNoteDto _WhCreditNote = new WhCreditNoteDto();
            var Params = Param(_WhCreditNote, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCreditNote", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhCreditNoteDto> results = new List<WhCreditNoteDto>();

                        foreach (DataRow row in rows)
                        {
                            WhCreditNoteDto obj = new WhCreditNoteDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            obj.CustomerID = row["CustomerID"].ToDataConvertNullInt64();
                            obj.CreditRating = row["CreditRating"].ToDataConvertDouble();
                            obj.CreditLimit = row["CreditLimit"].ToDataConvertDouble();
                            obj.CreditDays = row["CreditDays"].ToDataConvertDouble();
                            obj.CreditFileName = row["CreditFileName"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();

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
        public WhCreditNoteDto getbyWhleadIDAndCustomerID(string CustomerID,long WhLeadID)
        {
            DataSet _ds = null;

            WhCreditNoteDto _WhCreditNote = new WhCreditNoteDto();
            _WhCreditNote.CustomerID = CustomerID.ToLong();
            _WhCreditNote.WhLeadID = WhLeadID.ToDataConvertNullInt64();
            var Params = Param(_WhCreditNote, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhCreditNote", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhCreditNoteDto result = new WhCreditNoteDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.WhLeadID = row["WhLeadID"].ToDataConvertNullInt64();
                            result.CustomerID = row["CustomerID"].ToDataConvertNullInt64();
                            result.CreditRating = row["CreditRating"].ToDataConvertDouble();
                            result.CreditLimit = row["CreditLimit"].ToDataConvertDouble();
                            result.CreditDays = row["CreditDays"].ToDataConvertDouble();
                            result.CreditFileName = row["CreditFileName"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
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

       

    }
}

