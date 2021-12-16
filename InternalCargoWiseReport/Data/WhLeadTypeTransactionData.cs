using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhLeadTypeTransactionData
    {
        SqlParameter[] Param(WhLeadTypeTransactionDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@LeadID", SqlDbType.NVarChar);
            Param[1].Value = obj.LeadID;

            Param[2] = new SqlParameter("@LeadType", SqlDbType.NVarChar);
            Param[2].Value = obj.LeadType;

            Param[3] = new SqlParameter("@UOM", SqlDbType.NVarChar);
            Param[3].Value = obj.UOM;

            Param[4] = new SqlParameter("@Qty_Nos", SqlDbType.Float);
            Param[4].Value = obj.Qty_Nos;

            Param[5] = new SqlParameter("@PerUnitRevenue", SqlDbType.Float);
            Param[5].Value = obj.PerUnitRevenue;

            Param[6] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[6].Value = flag;

            return Param;
        }
        public bool Insert(WhLeadTypeTransactionDto obj) 
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadTypeTransaction", Params);

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
        public bool Update(WhLeadTypeTransactionDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadTypeTransaction", Params);

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
        public WhLeadTypeTransactionDto GetById(int id)
        {
            DataSet _ds = null;

            WhLeadTypeTransactionDto _WhItsystemMaster = new WhLeadTypeTransactionDto();
            _WhItsystemMaster.ID = id;
            var Params = Param(_WhItsystemMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadTypeTransaction", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhLeadTypeTransactionDto result = new WhLeadTypeTransactionDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.LeadID = row["LeadID"].ToString();
                            result.LeadType = row["LeadType"].ToString();
                            result.UOM = row["UOM"].ToString();
                            result.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            result.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
                           
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
        public IList<WhLeadTypeTransactionDto> GetAllByID(string RqID)
        {
            DataSet _ds = null;
            WhLeadTypeTransactionDto obj1 = new WhLeadTypeTransactionDto();
            obj1.LeadID = RqID;
            var Params = Param(obj1, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadTypeTransaction", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhLeadTypeTransactionDto> results = new List<WhLeadTypeTransactionDto>();

                        foreach (DataRow row in rows)
                        {
                            WhLeadTypeTransactionDto obj = new WhLeadTypeTransactionDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.LeadID = row["LeadID"].ToString();
                            obj.LeadType = row["LeadType"].ToString();
                            obj.UOM = row["UOM"].ToString();
                            obj.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            obj.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
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
        public IList<WhLeadTypeTransactionDto> getbyWhLeadID(string WhLeadID)
        {
            DataSet _ds = null;
            WhLeadTypeTransactionDto obj1 = new WhLeadTypeTransactionDto();
            obj1.LeadID = WhLeadID;
            var Params = Param(obj1, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadTypeTransaction", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhLeadTypeTransactionDto> results = new List<WhLeadTypeTransactionDto>();

                        foreach (DataRow row in rows)
                        {
                            WhLeadTypeTransactionDto obj = new WhLeadTypeTransactionDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.LeadID = row["LeadID"].ToString();
                            obj.LeadType = row["LeadType"].ToString();
                            obj.UOM = row["UOM"].ToString();
                            obj.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            obj.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
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
