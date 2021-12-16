using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class WhInvoiceUploadData
    {
        SqlParameter[] Param(WhInvoiceUploadDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[10];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Message", SqlDbType.NVarChar);
            Param[1].Value = obj.Message;

            Param[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
            Param[2].Value = obj.FileName;

            
            Param[3] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[3].Value = LovelySession.Lovely.User.Id;

            Param[4] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[4].Value = flag;
            Param[5] = new SqlParameter("@Path", SqlDbType.NVarChar);
            Param[5].Value = obj.Path;
            Param[6] = new SqlParameter("@Password", SqlDbType.NVarChar);
            Param[6].Value = obj.Password;
            Param[7] = new SqlParameter("@CustomerID", SqlDbType.NVarChar);
            Param[7].Value = obj.CustomerID;
            Param[8] = new SqlParameter("@Bu", SqlDbType.NVarChar);
            Param[8].Value = obj.Bu;
            Param[9] = new SqlParameter("@InvoiceNO", SqlDbType.NVarChar);
            Param[9].Value = obj.InvoiceNO;
            return Param;
        }

        SqlParameter[] Param1(WhApproverDTo obj, int flag)
        {
            SqlParameter[] Param1 = new SqlParameter[4];
            Param1[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param1[0].Value = obj.ID;
            Param1[1] = new SqlParameter("@Bu", SqlDbType.NVarChar);
            Param1[1].Value = obj.BU;
            Param1[2] = new SqlParameter("@SeqNo", SqlDbType.NVarChar);
            Param1[2].Value = obj.SeqNo;
            Param1[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param1[3].Value = flag;
         


            return Param1;
        }
        SqlParameter[] TransParam1(WhInvoiceUploadTransDTo obj, int flag)
        {
            SqlParameter[] TransParam1 = new SqlParameter[7];
            TransParam1[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            TransParam1[0].Value = obj.ID;
            TransParam1[1] = new SqlParameter("@Inv_Upl_Mas_ID", SqlDbType.NVarChar);
            TransParam1[1].Value = obj.Inv_Upl_Mas_ID;
            TransParam1[2] = new SqlParameter("@Status", SqlDbType.NVarChar);
            TransParam1[2].Value = obj.Status;
            TransParam1[3] = new SqlParameter("@Flag", SqlDbType.Int);
            TransParam1[3].Value = flag;
            
            if(LovelySession.Lovely != null && LovelySession.Lovely.User != null &&LovelySession.Lovely.User.Id != null)
            {
                TransParam1[4] = new SqlParameter("@CreateBy", SqlDbType.Int);
                TransParam1[4].Value = LovelySession.Lovely.User.Id;
            }
            else
            {
                TransParam1[4] = new SqlParameter("@CreateBy", SqlDbType.Int);
                TransParam1[4].Value = obj.ApproverID;
            }
            
            TransParam1[5] = new SqlParameter("@ApproverID", SqlDbType.BigInt);
            TransParam1[5].Value = obj.ApproverID;
            TransParam1[6] = new SqlParameter("@Password", SqlDbType.NVarChar);
            TransParam1[6].Value = obj.Password;



            return TransParam1;
        }

        SqlParameter[] BindBUParam(WhBindBuDTo obj, int flag)
        {
            SqlParameter[] BindBUParam = new SqlParameter[2];

            if (LovelySession.Lovely != null && LovelySession.Lovely.User != null && LovelySession.Lovely.User.Id != null)
            {
                BindBUParam[0] = new SqlParameter("@MemberID", SqlDbType.BigInt);
                BindBUParam[0].Value = LovelySession.Lovely.User.Id; ;
            }
            BindBUParam[1] = new SqlParameter("@Flag", SqlDbType.Int);
            BindBUParam[1].Value = flag;

            return BindBUParam;
        }
        public int Insert(WhInvoiceUploadDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Master", Params);

                    if ( i != null && i.ToDataConvertInt64() > 0)
                    {
                        return Convert.ToInt32(i);
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
            return 0;
        }

        public int InsertTrans(WhInvoiceUploadTransDTo obj)
        {
            var Params = TransParam1(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Trans", Params);
                    if (i != null && i.ToDataConvertInt64() > 0)
                    {
                        return Convert.ToInt32(i);
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
            return 0;
        }
        public bool Update(WhInvoiceUploadDto obj)
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


        public DataSet UpdateStatus(WhInvoiceUploadTransDTo obj)
        {
            DataSet _ds = null;
            var Params = TransParam1(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Trans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public DataSet ValidateRequest(WhInvoiceUploadTransDTo obj)
        {
            DataSet _ds = null;
            var Params = TransParam1(obj, 4);
            if (Params != null && obj.ApproverID  != null && obj.Password != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Trans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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
        public WhApproverDTo GetApproverIDByBU(string BU,string SeqNo)
        {
            DataSet _ds = null;

            WhInvoiceUploadData _WhInvoiceUploadData = new WhInvoiceUploadData();
            WhApproverDTo _WhInvoiceUploadDto = new WhApproverDTo();
            _WhInvoiceUploadDto.BU = BU;
            _WhInvoiceUploadDto.SeqNo = SeqNo;
            var Param = Param1(_WhInvoiceUploadDto, 2);
            if (Param != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Master", Param);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhApproverDTo result = new WhApproverDTo();
                        {
                            result.ID = row["ID"].ToDataConvertNullInt64();
                            result.EmailID = row["EmailID"].ToString();
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

        public DataSet GetListUserWise(WhInvoiceUploadTransDTo obj)
        {
            DataSet _ds = null;
            var Params = TransParam1(obj, 5);
            if (Params != null && obj.ApproverID != null && obj.Status != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Trans", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public DataSet BindBU()
        {
            DataSet _ds = null;
            WhBindBuDTo obj = new WhBindBuDTo();
            var Params = BindBUParam(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInvoice_Upload_Master", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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


        //public IList<WhDocumentUploadTransDto> GetAllByID(string RqID)
        //{
        //    DataSet _ds = null;
        //    WhDocumentUploadTransDto obj1 = new WhDocumentUploadTransDto();
        //    obj1.WhLeadID = RqID;
        //    var Params = Param(obj1, 4);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

        //            if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRowCollection rows = _ds.Tables[0].Rows;
        //                IList<WhDocumentUploadTransDto> results = new List<WhDocumentUploadTransDto>();

        //                foreach (DataRow row in rows)
        //                {
        //                    WhDocumentUploadTransDto obj = new WhDocumentUploadTransDto();

        //                    obj.ID = row["ID"].ToDataConvertNullInt64();
        //                    obj.WhLeadID = row["WhLeadID"].ToString();
        //                    obj.FileName = row["FileName"].ToString();
        //                    obj.FileType = row["FileType"].ToString();
        //                    obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();

        //                    results.Add(obj);
        //                }
        //                return results;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //    return null;
        //}
        //public WhDocumentUploadTransDto GetByRqId(int id)
        //{
        //    DataSet _ds = null;

        //    WhDocumentUploadTransDto _WhItsystemMaster = new WhDocumentUploadTransDto();
        //    _WhItsystemMaster.WhLeadID = id.ToString();
        //    var Params = Param(_WhItsystemMaster, 5);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhDocumentUploadTrans", Params);

        //            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = _ds.Tables[0].Rows[0];
        //                WhDocumentUploadTransDto result = new WhDocumentUploadTransDto();
        //                {
        //                    result.ID = row["ID"].ToDataConvertNullInt64();
        //                    result.WhLeadID = row["WhLeadID"].ToString();
        //                    result.FileName = row["FileName"].ToString();
        //                    result.FileType = row["FileType"].ToString();
        //                    result.CreateBy = row["CreateBy"].ToDataConvertNullInt64();

        //                }
        //                return result;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return null;
        //}
    }

    //public class WhPostNegotitationData
    //{
    //    SqlParameter[] Param(WhPostNegotitationStageDto obj, int flag)
    //    {
    //        SqlParameter[] Param = new SqlParameter[3];
    //        Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
    //        Param[0].Value = obj.ID;

    //        Param[1] = new SqlParameter("@Stage", SqlDbType.NVarChar);
    //        Param[1].Value = obj.Stage;
    //        Param[2] = new SqlParameter("@Flag", SqlDbType.Int);
    //        Param[2].Value = flag;

    //        return Param;
    //    }

    //    public IList<WhPostNegotitationStageDto> GetAllStage()
    //    {
    //        DataSet _ds = null;
    //        WhPostNegotitationStageDto obj1 = new WhPostNegotitationStageDto();
    //        var Params = Param(obj1, 1);
    //        if (Params != null)
    //        {
    //            try
    //            {
    //                _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "UspWhPostNegotitationStage", Params);

    //                if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
    //                {
    //                    DataRowCollection rows = _ds.Tables[0].Rows;
    //                    IList<WhPostNegotitationStageDto> results = new List<WhPostNegotitationStageDto>();

    //                    foreach (DataRow row in rows)
    //                    {
    //                        WhPostNegotitationStageDto obj = new WhPostNegotitationStageDto();

    //                        obj.ID = row["ID"].ToDataConvertNullInt64();
    //                        obj.Stage = row["Stage"].ToString();

    //                        results.Add(obj);
    //                    }
    //                    return results;
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }

    //}
}
