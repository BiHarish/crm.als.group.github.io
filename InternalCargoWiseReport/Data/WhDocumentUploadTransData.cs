using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhDocumentUploadTransData
    {
        SqlParameter[] Param(WhDocumentUploadTransDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[6];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@WhLeadID", SqlDbType.NVarChar);
            Param[1].Value = obj.WhLeadID;

            Param[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
            Param[2].Value = obj.FileName;

            Param[3] = new SqlParameter("@FileType", SqlDbType.NVarChar);
            Param[3].Value = obj.FileType;

            Param[4] = new SqlParameter("@CreateBy", SqlDbType.Float);
            Param[4].Value = LovelySession.Lovely.User.Id;
            Param[5] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[5].Value = flag;

            return Param;
        }
        public bool Insert(WhDocumentUploadTransDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

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
        public bool Update(WhDocumentUploadTransDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

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
        public WhDocumentUploadTransDto GetById(int id)
        {
            DataSet _ds = null;

            WhDocumentUploadTransDto _WhItsystemMaster = new WhDocumentUploadTransDto();
            _WhItsystemMaster.ID = id;
            var Params = Param(_WhItsystemMaster, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhDocumentUploadTransDto result = new WhDocumentUploadTransDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.WhLeadID = row["WhLeadID"].ToString();
                            result.FileName = row["FileName"].ToString();
                            result.FileType = row["FileType"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                           
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
        public IList<WhDocumentUploadTransDto> GetAllByID(string RqID)
        {
            DataSet _ds = null;
            WhDocumentUploadTransDto obj1 = new WhDocumentUploadTransDto();
            obj1.WhLeadID = RqID;
            var Params = Param(obj1, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhDocumentUploadTransDto> results = new List<WhDocumentUploadTransDto>();

                        foreach (DataRow row in rows)
                        {
                            WhDocumentUploadTransDto obj = new WhDocumentUploadTransDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.WhLeadID = row["WhLeadID"].ToString();
                            obj.FileName = row["FileName"].ToString();
                            obj.FileType = row["FileType"].ToString();
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
        public WhDocumentUploadTransDto GetByRqId(int id)
        {
            DataSet _ds = null;

            WhDocumentUploadTransDto _WhItsystemMaster = new WhDocumentUploadTransDto();
            _WhItsystemMaster.WhLeadID = id.ToString();
            var Params = Param(_WhItsystemMaster, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhDocumentUploadTransDto result = new WhDocumentUploadTransDto();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.WhLeadID = row["WhLeadID"].ToString();
                            result.FileName = row["FileName"].ToString();
                            result.FileType = row["FileType"].ToString();
                            result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();

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
    }

    public class WhPostNegotitationData
    {
        SqlParameter[] Param(WhPostNegotitationStageDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Stage", SqlDbType.NVarChar);
            Param[1].Value = obj.Stage;
            Param[2] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[2].Value = flag;

            return Param;
        }
  
        public IList<WhPostNegotitationStageDto> GetAllStage()
        {
            DataSet _ds = null;
            WhPostNegotitationStageDto obj1 = new WhPostNegotitationStageDto();
            var Params = Param(obj1, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspWhPostNegotitationStage", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhPostNegotitationStageDto> results = new List<WhPostNegotitationStageDto>();

                        foreach (DataRow row in rows)
                        {
                            WhPostNegotitationStageDto obj = new WhPostNegotitationStageDto();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Stage = row["Stage"].ToString();

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
