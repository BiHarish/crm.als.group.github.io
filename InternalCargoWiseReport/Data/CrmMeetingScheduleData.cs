using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICWR.Data
{
    public class CrmMeetingScheduleData
    {
        SqlParameter[] Param(CrmMeetingScheduleDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[26];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@LeadID", SqlDbType.BigInt);
            Param[1].Value = obj.LeadID;

            Param[2] = new SqlParameter("@Subject", SqlDbType.NVarChar);
            Param[2].Value = obj.Subject;

            Param[3] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            Param[3].Value = obj.StartDate;

            Param[4] = new SqlParameter("@StartTime", SqlDbType.NVarChar);
            Param[4].Value = obj.StartTime;


            Param[5] = new SqlParameter("@EndTime", SqlDbType.NVarChar);
            Param[5].Value = obj.EndTime;

            Param[6] = new SqlParameter("@Duration", SqlDbType.NVarChar);
            Param[6].Value = obj.Duration;

            Param[7] = new SqlParameter("@JointCaller", SqlDbType.NVarChar);
            Param[7].Value = obj.JointCaller;

            Param[8] = new SqlParameter("@Priority", SqlDbType.NVarChar);
            Param[8].Value = obj.Priority;

            Param[9] = new SqlParameter("@Description", SqlDbType.NVarChar);
            Param[9].Value = obj.Description;

            Param[10] = new SqlParameter("@Status", SqlDbType.NVarChar);
            Param[10].Value = obj.Status;

            Param[11] = new SqlParameter("@RelatedTo", SqlDbType.NVarChar);
            Param[11].Value = obj.RelatedTo;

            Param[12] = new SqlParameter("@Location", SqlDbType.NVarChar);
            Param[12].Value = obj.Location;

            Param[13] = new SqlParameter("@Products", SqlDbType.NVarChar);
            Param[13].Value = obj.Products;

            Param[14] = new SqlParameter("@Visibility", SqlDbType.NVarChar);
            Param[14].Value = obj.Visibility;

            Param[15] = new SqlParameter("@AssignedTo", SqlDbType.NVarChar);
            Param[15].Value = obj.AssignedTo;

            Param[16] = new SqlParameter("@TotalKM", SqlDbType.Float);
            Param[16].Value = obj.TotalKM;

            Param[17] = new SqlParameter("@TotalAmt", SqlDbType.Float);
            Param[17].Value = obj.TotalAmt;

            Param[18] = new SqlParameter("@RelatedToName", SqlDbType.NVarChar);
            Param[18].Value = obj.RelatedToName;

            Param[19] = new SqlParameter("@IsPrint", SqlDbType.Bit);
            Param[19].Value = obj.IsPrint;

            Param[20] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[20].Value = obj.CreateBy;

            Param[21] = new SqlParameter("@Flag", SqlDbType.BigInt);
            Param[21].Value = flag;
            Param[22] = new SqlParameter("@FromDate", SqlDbType.DateTime);
            Param[22].Value = obj.FromDate;
            Param[23] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            Param[23].Value = obj.ToDate;
            Param[24] = new SqlParameter("@ClaimType", SqlDbType.NVarChar);
            Param[24].Value = obj.ClaimType;
            Param[25] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
            Param[25].Value = obj.Remarks;

            return Param;
        }

        public bool Insert(CrmMeetingScheduleDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

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

        public bool Update(CrmMeetingScheduleDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

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
      
        public IList<CrmMeetingScheduleDto> GetAll()
        {
            DataSet _ds = null;
            CrmMeetingScheduleDto _CrmMeetingScheduleData = new CrmMeetingScheduleDto();
           
            var Params = Param(_CrmMeetingScheduleData, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CrmMeetingScheduleDto> results = new List<CrmMeetingScheduleDto>();

                        foreach (DataRow row in rows)
                        {
                            CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.LeadID = row["LeadID"].ToDataConvertNullInt64();
                            obj.Subject = row["Subject"].ToString();
                            obj.StartDate = row["StartDate"].ToDataConvertNullDateTime();
                            obj.StartTime = row["StartTime"].ToString();
                            obj.EndTime = row["EndTime"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.JointCaller = row["JointCaller"].ToString();
                            obj.Priority = row["Priority"].ToString();
                            obj.Description = row["Description"].ToString();
                            obj.Status = row["Status"].ToString();
                            obj.RelatedTo = row["RelatedTo"].ToString();
                            obj.Location = row["Location"].ToString();
                            obj.Products = row["Products"].ToString();
                            obj.Visibility = row["Visibility"].ToString();
                            obj.AssignedTo = row["AssignedTo"].ToString();
                            obj.TotalKM = row["TotalKM"].ToDataConvertDouble();
                            obj.TotalAmt = row["TotalAmt"].ToDataConvertDouble();
                            obj.IsPrint = row["IsPrint"].ToDataConvertBool();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
                            obj.RelatedToName = row["RelatedToName"].ToString();

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

        public CrmMeetingScheduleDto GetById(long id)
        {
            DataSet _ds = null;

            CrmMeetingScheduleDto _CrmMeetingScheduleData = new CrmMeetingScheduleDto();
            _CrmMeetingScheduleData.ID = id;
            var Params = Param(_CrmMeetingScheduleData, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();
                        {
                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.LeadID = row["LeadID"].ToDataConvertNullInt64();
                            obj.Subject = row["Subject"].ToString();
                            obj.StartDate = row["StartDate"].ToDataConvertNullDateTime();
                            obj.StartTime = row["StartTime"].ToString();
                            obj.EndTime = row["EndTime"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.JointCaller = row["JointCaller"].ToString();
                            obj.Priority = row["Priority"].ToString();
                            obj.Description = row["Description"].ToString();
                            obj.Status = row["Status"].ToString();
                            obj.RelatedTo = row["RelatedTo"].ToString();
                            obj.Location = row["Location"].ToString();
                            obj.Products = row["Products"].ToString();
                            obj.Visibility = row["Visibility"].ToString();
                            obj.AssignedTo = row["AssignedTo"].ToString();
                            obj.TotalKM = row["TotalKM"].ToDataConvertNullDouble();
                            obj.TotalAmt = row["TotalAmt"].ToDataConvertNullDouble();
                            obj.IsPrint = row["IsPrint"].ToDataConvertBool();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
                            obj.RelatedToName = row["RelatedToName"].ToString();
                            obj.ClaimType = row["ClaimType"].ToString();
                            obj.Remarks = row["Remarks"].ToString();

                        }
                        return obj;
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

        public IList<CrmMeetingScheduleDto> GetAllDataForList(string Subject,string RelatedTo,string Status,string AssignedTo,long UserID)
        {
            DataSet _ds = null;
            CrmMeetingScheduleDto _CrmMeetingScheduleData = new CrmMeetingScheduleDto();
            
            _CrmMeetingScheduleData.Subject = Subject;
            _CrmMeetingScheduleData.RelatedTo = RelatedTo;
            _CrmMeetingScheduleData.Status = Status;
            _CrmMeetingScheduleData.AssignedTo = AssignedTo;
            _CrmMeetingScheduleData.CreateBy = UserID;

            var Params = Param(_CrmMeetingScheduleData,8); 
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CrmMeetingScheduleDto> results = new List<CrmMeetingScheduleDto>();

                        foreach (DataRow row in rows)
                        {
                            CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();

                            obj.SrNo = row["SrNo"].ToDataConvertInt64();
                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.LeadID = row["LeadID"].ToDataConvertNullInt64();
                            obj.Subject = row["Subject"].ToString();
                            obj.StartDate = row["StartDate"].ToDataConvertNullDateTime();
                            obj.StartTime = row["StartTime"].ToString();
                            obj.EndTime = row["EndTime"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.JointCaller = row["JointCaller"].ToString();
                            obj.Priority = row["Priority"].ToString();
                            obj.Description = row["Description"].ToString();
                            obj.Status = row["Status"].ToString();
                            obj.RelatedTo = row["RelatedTo"].ToString();
                            obj.Location = row["Location"].ToString();
                            obj.Products = row["Products"].ToString();
                            obj.Visibility = row["Visibility"].ToString();
                            obj.AssignedTo = row["AssignedTo"].ToString();
                            obj.TotalKM = row["TotalKM"].ToDataConvertDouble();
                            obj.TotalAmt = row["TotalAmt"].ToDataConvertDouble();
                            obj.IsPrint = row["IsPrint"].ToDataConvertBool();
                            obj.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            obj.RelatedToName = row["RelatedToName"].ToString();

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

        public IList<CrmMeetingScheduleDto> GetAllSubject(long UserID)
        {
            DataSet _ds = null;
            CrmMeetingScheduleDto _CrmMeetingScheduleData = new CrmMeetingScheduleDto();
            _CrmMeetingScheduleData.CreateBy = UserID;
            var Params = Param(_CrmMeetingScheduleData, 9);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CrmMeetingScheduleDto> results = new List<CrmMeetingScheduleDto>();

                        foreach (DataRow row in rows)
                        {
                            CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();

                            //obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Subject = row["Subject"].ToString();
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

        public IList<CrmMeetingScheduleDto> GetAllForClaim(DateTime? FromDate,DateTime? ToDate, long UserID)
        { 
            DataSet _ds = null;
            CrmMeetingScheduleDto _CrmMeetingScheduleDto = new CrmMeetingScheduleDto();
            _CrmMeetingScheduleDto.FromDate = FromDate;
            _CrmMeetingScheduleDto.ToDate = ToDate;
            _CrmMeetingScheduleDto.CreateBy = UserID;

            var Params = Param(_CrmMeetingScheduleDto, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CrmMeetingScheduleDto> results = new List<CrmMeetingScheduleDto>();

                        foreach (DataRow row in rows)
                        {
                            CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.LeadID = row["LeadID"].ToDataConvertNullInt64();
                            obj.Subject = row["Subject"].ToString();
                            obj.StartDate = row["StartDate"].ToDataConvertNullDateTime();
                            obj.StartTime = row["StartTime"].ToString();
                            obj.EndTime = row["EndTime"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.JointCaller = row["JointCaller"].ToString();
                            obj.Priority = row["Priority"].ToString();
                            obj.Description = row["Description"].ToString();
                            obj.Status = row["Status"].ToString();
                            obj.RelatedTo = row["RelatedTo"].ToString();
                            obj.Location = row["Location"].ToString();
                            obj.Products = row["Products"].ToString();
                            obj.Visibility = row["Visibility"].ToString();
                            obj.AssignedTo = row["AssignedTo"].ToString();
                            obj.TotalKM = row["TotalKM"].ToDataConvertDouble();
                            obj.TotalAmt = row["TotalAmt"].ToDataConvertDouble();
                            obj.IsPrint = row["IsPrint"].ToDataConvertBool();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
                            obj.RelatedToName = row["RelatedToName"].ToString();
                            obj.PerKm = row["PerKm"].ToDataConvertDouble();
                            obj.EmpCode = row["EmpCode"].ToString();
                            obj.Designation = row["Designation"].ToString();
                            obj.Branch = row["Location"].ToString();
                            obj.Name = row["Name"].ToString();
                            obj.SalesPersonname = row["SalesPerson"].ToString();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.ContactPersonName = row["ContactPersonName"].ToString();
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

        public bool UpdateIsPrint(CrmMeetingScheduleDto obj)
        {
            CrmMeetingScheduleDto _CrmMeetingScheduleDto = new CrmMeetingScheduleDto();
            var Params = Param(obj,12);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

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

        public IList<CrmMeetingScheduleDto> getAllWithMultipleID(string ID)
        {
            DataSet _ds = null;
            CrmMeetingScheduleDto _CrmMeetingScheduleDto = new CrmMeetingScheduleDto();

            _CrmMeetingScheduleDto.RelatedTo = ID;

            var Params = Param(_CrmMeetingScheduleDto, 13);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspMeetingSchedule", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<CrmMeetingScheduleDto> results = new List<CrmMeetingScheduleDto>();

                        foreach (DataRow row in rows)
                        {
                            CrmMeetingScheduleDto obj = new CrmMeetingScheduleDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.LeadID = row["LeadID"].ToDataConvertNullInt64();
                            obj.Subject = row["Subject"].ToString();
                            obj.StartDate = row["StartDate"].ToDataConvertNullDateTime();
                            obj.StartTime = row["StartTime"].ToString();
                            obj.EndTime = row["EndTime"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.JointCaller = row["JointCaller"].ToString();
                            obj.Priority = row["Priority"].ToString();
                            obj.Description = row["Description"].ToString();
                            obj.Status = row["Status"].ToString();
                            obj.RelatedTo = row["RelatedTo"].ToString();
                            obj.Products = row["Products"].ToString();
                            obj.Visibility = row["Visibility"].ToString();
                            obj.AssignedTo = row["AssignedTo"].ToString();
                            obj.TotalKM = row["TotalKM"].ToDataConvertDouble();
                            obj.TotalAmt = row["TotalAmt"].ToDataConvertDouble();
                            obj.IsPrint = row["IsPrint"].ToDataConvertBool();
                            obj.CreateBy = row["CreateBy"].ToDataConvertInt64();
                            obj.RelatedToName = row["RelatedToName"].ToString();
                            obj.PerKm = row["PerKm"].ToDataConvertDouble();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.ContactPersonName = row["ContactPersonName"].ToString();
                            obj.SalesPersonname = row["SALESPERSON"].ToString();
                           
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