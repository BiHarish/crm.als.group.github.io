using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InternalCargoWiseReport.Models;
using ICWR.Data;

namespace ICWR.Data
{
    public class EmailTaskData
    {
        SqlParameter[] Param(EmailTaskDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[23];
            Param[0] = new SqlParameter("@CompanyCode", SqlDbType.VarChar);
            Param[0].Value = obj.CompanyCode;
            Param[1] = new SqlParameter("@flag", SqlDbType.Int);
            Param[1].Value = flag;
            Param[2] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
            Param[2].Value = obj.CustomerName;
            Param[3] = new SqlParameter("@CustomerCode", SqlDbType.VarChar);
            Param[3].Value = obj.CustomerCode;
            Param[4] = new SqlParameter("@TMID", SqlDbType.VarChar);
            Param[4].Value = obj.TMID;
            Param[5] = new SqlParameter("@Timming", SqlDbType.VarChar);
            Param[5].Value = obj.Timing;
            Param[6] = new SqlParameter("@IsMonday", SqlDbType.Bit);
            Param[6].Value = obj.IsMonday;
            Param[7] = new SqlParameter("@IsTuesday", SqlDbType.Bit);
            Param[7].Value = obj.IsTuesday;
            Param[8] = new SqlParameter("@IsWednesday", SqlDbType.Bit);
            Param[8].Value = obj.IsWednesday;
            Param[9] = new SqlParameter("@IsThursday", SqlDbType.Bit);
            Param[9].Value = obj.IsThursday;
            Param[10] = new SqlParameter("@IsFriday", SqlDbType.Bit);
            Param[10].Value = obj.IsFriday;
            Param[11] = new SqlParameter("@IsSaturday", SqlDbType.Bit);
            Param[11].Value = obj.IsSaturday;
            Param[12] = new SqlParameter("@IsSunday", SqlDbType.Bit);
            Param[12].Value = obj.IsSunday;
            Param[13] = new SqlParameter("@IsEmail", SqlDbType.Bit);
            Param[13].Value = obj.IsEmail;
            Param[14] = new SqlParameter("@IsMsg", SqlDbType.Bit);
            Param[14].Value = obj.IsMsg;
            Param[15] = new SqlParameter("@TaskName", SqlDbType.NVarChar);
            Param[15].Value = obj.TaskName;
            Param[16] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[16].Value = obj.IsActive;
            Param[17] = new SqlParameter("@MailTo", SqlDbType.NVarChar);
            Param[17].Value = obj.MailTo;
            Param[18] = new SqlParameter("@MailCC", SqlDbType.NVarChar);
            Param[18].Value = obj.MailCC;
            Param[19] = new SqlParameter("@MailBCC", SqlDbType.NVarChar);
            Param[19].Value = obj.MailBCC;
            Param[20] = new SqlParameter("@MobileNo", SqlDbType.NVarChar);
            Param[20].Value = obj.MobileNo;
            Param[21] = new SqlParameter("@GroupBy", SqlDbType.NVarChar);
            Param[21].Value = obj.GroupBy;
            Param[22] = new SqlParameter("@IsActiveT", SqlDbType.Bit);
            Param[22].Value = obj.IsActiveT;



            return Param;
        }

        #region GETTaskList
        public IList<EmailTaskDto> GetTaskListByCompanyCode(string CompanyCode)
        {
            DataSet _ds = null;
            EmailTaskDto _emailTaskDto = new EmailTaskDto();
            _emailTaskDto.CompanyCode = CompanyCode;
            var Params = Param(_emailTaskDto, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<EmailTaskDto> results = new List<EmailTaskDto>();

                        foreach (DataRow row in rows)
                        {
                            EmailTaskDto obj1 = new EmailTaskDto();
                            obj1.TaskID = row["TaskID"].ToString();
                            obj1.TaskName = row["TaskName"].ToString();
                            results.Add(obj1);
                          
                        }
                        return results;
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

        public IList<EmailTaskDto> GetByCompanyCodeAnsTaskID(string CompanyCode,string TaskID)
        {
            DataSet _ds = null;
            EmailTaskDto _emailTaskDto = new EmailTaskDto();
            _emailTaskDto.CompanyCode = CompanyCode;
            _emailTaskDto.TaskName = TaskID;
            var Params = Param(_emailTaskDto, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<EmailTaskDto> results = new List<EmailTaskDto>();

                        foreach (DataRow row in rows)
                        {
                            EmailTaskDto obj1 = new EmailTaskDto();
                            obj1.TMID = row["TMID"].ToString();
                            obj1.CustomerCode = row["CustomerCode"].ToString();
                            obj1.CustomerName = row["CustomerName"].ToString();
                            obj1.Timing = row["Timing"].ToString();
                            obj1.IsMonday = row["IsMonday"].ToDataConvertBool();
                            obj1.IsTuesday = row["IsTuesday"].ToDataConvertBool();
                            obj1.IsWednesday = row["IsWednesday"].ToDataConvertBool();
                            obj1.IsThursday = row["IsThursday"].ToDataConvertBool();
                            obj1.IsFriday = row["IsFriday"].ToDataConvertBool();
                            obj1.IsSaturday = row["IsSaturday"].ToDataConvertBool();
                            obj1.IsSunday = row["IsSunday"].ToDataConvertBool();
                            obj1.IsEmail = row["IsEmail"].ToDataConvertBool();
                            obj1.IsMsg = row["IsMsg"].ToDataConvertBool();
                            obj1.IsActive = row["IsActiveT"].ToDataConvertNullBool();
                            obj1.MailTo = row["MailTo"].ToString();
                            obj1.MailCC = row["MailCC"].ToString();
                            obj1.MailBCC = row["MailBCC"].ToString();
                            obj1.MobileNo = row["MobileNo"].ToString();
                            obj1.GroupBy = row["GroupBy"].ToString();
                            results.Add(obj1);
                        }
                        return results;
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


        public int Insert(EmailTaskDto _emailTaskDto)
        {
            //EmailTaskDto _emailTaskDto = new EmailTaskDto();
            //_emailTaskDto.CompanyCode = CompanyCode;
            //_emailTaskDto.TaskName = TaskID;
            //_emailTaskDto.IsActive = IsActive;
            var Params = Param(_emailTaskDto, 3);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

                    if (i != null)
                    {
                      return  Convert.ToInt32(i);
                        
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
            return -1;
        }
        public bool InsertIntoTrans(EmailTaskDto obj)
        {
            
            var Params = Param(obj, 4);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

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

        public bool DeleteOldRecord(EmailTaskDto obj)
        {

            var Params = Param(obj, 5);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

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

        public int UpdateEmailMaster(EmailTaskDto obj)
        {
            //EmailTaskDto _emailTaskDto = new EmailTaskDto();
            //_emailTaskDto.TaskName = obj.TaskName ;
            //_emailTaskDto.TMID = obj.id.ToString();
            //_emailTaskDto.IsActive = obj.IsActive;
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTask", Params);

                    if (i != null)
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
            return -1;
        }

       

        public long UploadFileData(EmailTaskDto obj)
        {
            SqlParameter[] Param = new SqlParameter[2];

            Param[0] = new SqlParameter("@EmailTaskTransDt", obj.EmailTaskTransDt);
            Param[1] = new SqlParameter("@TmID", obj.TMID);

            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspEmailTaskUploadFile", Param);

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

        #endregion
        //public bool Insert(GeneratePayDto obj)
        //{
        //    var Params = Param(obj, 2);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);

        //            if (i > 0)
        //            {
        //                return true;
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
        //    return false;
        //}
        //public QuaterDateDto GetStartEndDate(GeneratePayDto obj)
        //{
        //    DataSet _ds = null;

        //    var Params = Param(obj, 1);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            QuaterDateDto QObj = new QuaterDateDto();

        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);

        //            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = _ds.Tables[0].Rows[0];

        //                QObj.datefrom = row["IAM_StartDate"].ToDataConvertDateTime();
        //            }

        //            if (_ds != null && _ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
        //            {
        //                DataRow row = _ds.Tables[1].Rows[0];

        //                QObj.dateto = row["IAM_EndDate"].ToDataConvertDateTime();
        //            }

        //            if (_ds != null && _ds.Tables.Count > 2 && _ds.Tables[2].Rows.Count > 0)
        //            {
        //                DataRow row = _ds.Tables[2].Rows[0];

        //                QObj.firstdate = row["IAM_StartDate"].ToDataConvertDateTime();
        //            }

        //            return QObj;
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
        //public DataSet GenerateBySendProfit(GeneratePayDto obj)
        //{
        //    DataSet _ds = null;

        //    var Params = Param(obj, 2);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);
        //            return _ds;
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
        //
        //
        //

        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //

        //
        //
        //
        //
        //public DataSet DownloadGenerate(GeneratePayDto obj)
        //{
        //    DataSet _ds = null;

        //    var Params = Param(obj, 3);
        //    if (Params != null)
        //    {
        //        try
        //        {
        //            _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "GeneratePay", Params);
        //            return _ds;
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
        //public DataSet getIncentiveDetails(string Code, string AssYear)
        //{
        //    SqlParameter[] sp = new SqlParameter[3];

        //    sp[0] = new SqlParameter("@CargoCode", SqlDbType.VarChar);
        //    sp[0].Value =Code;
        //    sp[1] = new SqlParameter("@AssYear", SqlDbType.NVarChar);
        //    sp[1].Value = AssYear;
        //    sp[2] = new SqlParameter("@Flag", SqlDbType.BigInt);
        //    sp[2].Value = 8;


        //    if (sp != null )
        //    {
        //        try
        //        {
        //            DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCtcMember", sp);

        //            if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //            {
        //                return _ds;
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
}