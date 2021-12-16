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
    public class InquiryData
    {
        SqlParameter[] Param(InquiryDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[28];
            Param[0] = new SqlParameter("@OrgInquiryId", SqlDbType.NVarChar);
            Param[0].Value = obj.OrgInquiryId;

            Param[1] = new SqlParameter("@OrgEnquiryType", SqlDbType.NVarChar);
            Param[1].Value = obj.OrgEnquiryType;

            Param[2] = new SqlParameter("@OrgName", SqlDbType.NVarChar);
            Param[2].Value = obj.OrgName;

            Param[3] = new SqlParameter("@OrgAddress", SqlDbType.NVarChar);
            Param[3].Value = obj.OrgAddress;

            Param[4] = new SqlParameter("@OrgCountry", SqlDbType.BigInt);
            Param[4].Value = obj.OrgCountry;

            Param[5] = new SqlParameter("@OrgCity", SqlDbType.NVarChar);
            Param[5].Value = obj.OrgCity;

            Param[6] = new SqlParameter("@OrgPostCode", SqlDbType.NVarChar);
            Param[6].Value = obj.OrgPostCode;

            Param[7] = new SqlParameter("@OrgState", SqlDbType.NVarChar);
            Param[7].Value = obj.OrgState;

            Param[8] = new SqlParameter("@OrgWebsite", SqlDbType.NVarChar);
            Param[8].Value = obj.OrgWebsite;

            Param[9] = new SqlParameter("@OrgRegNo", SqlDbType.NVarChar);
            Param[9].Value = obj.OrgRegNo;

            Param[10] = new SqlParameter("@InquiryContact", SqlDbType.NVarChar);
            Param[10].Value = obj.InquiryContact;

            Param[11] = new SqlParameter("@Phone", SqlDbType.NVarChar);
            Param[11].Value = obj.Phone;

            Param[12] = new SqlParameter("@Email", SqlDbType.NVarChar);
            Param[12].Value = obj.Email;

            Param[13] = new SqlParameter("@MobNo", SqlDbType.NVarChar);
            Param[13].Value = obj.MobNo;

            Param[14] = new SqlParameter("@FaxNo", SqlDbType.NVarChar);
            Param[14].Value = obj.FaxNo;

            Param[15] = new SqlParameter("@JobDesc", SqlDbType.NVarChar);
            Param[15].Value = obj.JobDesc;

            Param[16] = new SqlParameter("@LeadIntrest", SqlDbType.NVarChar);
            Param[16].Value = obj.LeadIntrest;

            Param[17] = new SqlParameter("@SalesRepName", SqlDbType.NVarChar);
            Param[17].Value = obj.SalesRepName;

            Param[18] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[18].Value = obj.CreateBy;

            Param[19] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[19].Value = obj.CreateOn;

            Param[20] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[20].Value = flag;

            Param[21] = new SqlParameter("@Origin", SqlDbType.NVarChar);
            Param[21].Value = obj.Origin;

            Param[22] = new SqlParameter("@Destination", SqlDbType.NVarChar);
            Param[22].Value = obj.Destination;

            Param[23] = new SqlParameter("@ContainerCount", SqlDbType.NVarChar);
            Param[23].Value = obj.ContainerCount;

            Param[24] = new SqlParameter("@InquiryDate", SqlDbType.DateTime);
            Param[24].Value = obj.InquiryDate;

            Param[25] = new SqlParameter("@CommType", SqlDbType.NVarChar);
            Param[25].Value = obj.CommType;

            Param[26] = new SqlParameter("@FName", SqlDbType.NVarChar);
            Param[26].Value = obj.FName;

            Param[27] = new SqlParameter("@FileData", SqlDbType.VarBinary);
            Param[27].Value = obj.FileData;



            return Param;
        }

        SqlParameter[] ParamOpportunity(OpportunityDto obj, int flag)
        {
            SqlParameter[] Param = new SqlParameter[21];
            Param[0] = new SqlParameter("@InquiryID", SqlDbType.BigInt);
            Param[0].Value = obj.InquiryID;

            Param[1] = new SqlParameter("@OppOrigin", SqlDbType.NVarChar);
            Param[1].Value = obj.OppOrigin;

            Param[2] = new SqlParameter("@OppDestination", SqlDbType.NVarChar);
            Param[2].Value = obj.OppDestination;

            Param[3] = new SqlParameter("@OppMode", SqlDbType.NVarChar);
            Param[3].Value = obj.OppMode;

            Param[4] = new SqlParameter("@OppContainer", SqlDbType.NVarChar);
            Param[4].Value = obj.OppContainer;

            Param[5] = new SqlParameter("@OppContType", SqlDbType.NVarChar);
            Param[5].Value = obj.OppContType;

            Param[6] = new SqlParameter("@OppContainerCount", SqlDbType.NVarChar);
            Param[6].Value = obj.OppContainerCount;

            Param[7] = new SqlParameter("@OppRecurring", SqlDbType.NVarChar);
            Param[7].Value = obj.OppRecurring;

            Param[8] = new SqlParameter("@OppVerticalMarket", SqlDbType.NVarChar);
            Param[8].Value = obj.OppVerticalMarket;

            Param[9] = new SqlParameter("@OppActivityPeriod", SqlDbType.NVarChar);
            Param[9].Value = obj.OppActivityPeriod;

            Param[10] = new SqlParameter("@OppCarrier", SqlDbType.NVarChar);
            Param[10].Value = obj.OppCarrier;

            Param[11] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[11].Value = obj.CreateBy;

            Param[12] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[12].Value = obj.CreateOn;

            Param[13] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[13].Value = flag;

            Param[14] = new SqlParameter("@Weight", SqlDbType.Float);
            Param[14].Value = obj.Weight;

            Param[15] = new SqlParameter("@Unit", SqlDbType.NVarChar);
            Param[15].Value = obj.Unit;

            Param[16] = new SqlParameter("@CommodityID", SqlDbType.NVarChar);
            Param[16].Value = obj.CommodityID;

            Param[17] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[17].Value = obj.Id;


            Param[18] = new SqlParameter("@Competitor", SqlDbType.NVarChar);
            Param[18].Value = obj.Competitor;

            Param[19] = new SqlParameter("@Terms", SqlDbType.NVarChar);
            Param[19].Value = obj.Terms;

            Param[20] = new SqlParameter("@CountType", SqlDbType.NVarChar);
            Param[20].Value = obj.CountType;

            return Param;
        }

        public long Insert(InquiryDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Params);

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

        public bool InsertOpportunityData(OpportunityDto obj)
        {
            var Params = ParamOpportunity(obj, 1);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Params);

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

        public InquiryDto getbyInquiryNo(string InquiryNo)
        {
            DataSet _ds = null;

            InquiryDto _inquiryData = new InquiryDto();
            _inquiryData.OrgInquiryId = InquiryNo;
            var Params = Param(_inquiryData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        InquiryDto result = new InquiryDto()
                        {
                            Id = row["Id"].ToDataConvertInt64(),
                            OrgInquiryId = row["OrgInquiryId"].ToString(),
                            OrgEnquiryType = row["OrgEnquiryType"].ToString(),
                            OrgName = row["OrgName"].ToString(),
                            OrgAddress = row["OrgAddress"].ToString(),
                            OrgCountry = row["OrgCountry"].ToDataConvertNullInt64(),
                            OrgCity = row["OrgCity"].ToString(),
                            OrgPostCode = row["OrgPostCode"].ToString(),
                            OrgState = row["OrgState"].ToString(),
                            OrgWebsite = row["OrgWebsite"].ToString(),
                            OrgRegNo = row["OrgRegNo"].ToString(),
                            InquiryContact = row["InquiryContact"].ToString(),
                            Phone = row["Phone"].ToString(),
                            Email = row["Email"].ToString(),
                            MobNo = row["MobNo"].ToString(),
                            FaxNo = row["FaxNo"].ToString(),
                            JobDesc = row["JobDesc"].ToString(),
                            LeadIntrest = row["LeadIntrest"].ToString(),
                            SalesRepName = row["SalesRepName"].ToDataConvertNullInt64(),
                            InquiryDate = row["InquiryDate"].ToDataConvertNullDateTime(),
                            FName = row["FName"].ToString(),
                        };
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

        public InquiryDto chkExistOrNote(InquiryDto obj)
        {
            DataSet _ds = null;
            var Params = Param(obj, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        InquiryDto result = new InquiryDto()
                        {
                            OrgName = row["OrgName"].ToString(),
                            RepName = row["RepName"].ToString(),
                            Origin = row["Origin"].ToString(),
                            Destination = row["Destination"].ToString(),
                            InquiryDate = row["InquiryDate"].ToDataConvertNullDateTime(),
                            ContainerCount = row["ContainerCount"].ToString(),
                        };
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

        public IList<InquiryDto> GetAllForInqList(string OrgName, string UserName)
        {
            DataSet _ds = null;
            InquiryDto _inquiry = new InquiryDto();
            _inquiry.OrgName = OrgName;
            _inquiry.CreateBy = UserName;
            var Params = Param(_inquiry, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<InquiryDto> results = new List<InquiryDto>();

                        foreach (DataRow row in rows)
                        {
                            InquiryDto obj = new InquiryDto();

                            obj.SrNo = row["SrNo"].ToDataConvertInt32();
                            obj.Id = row["ID"].ToDataConvertInt64();
                            obj.OrgName = row["OrgName"].ToString();
                            obj.OrgInquiryId = row["OrgInquiryId"].ToString();
                            obj.RepName = row["RepName"].ToString();
                            obj.Opportunity = row["Opportunity"].ToString();
                            obj.OppID = row["OppID"].ToDataConvertNullInt64();
                            obj.CreateBy = row["CreateBy"].ToString();
                            obj.FName = row["FName"].ToString();
                            obj.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            if (row["FileData"].ToString() != string.Empty)
                            {
                                obj.FileData = (byte[])row["FileData"];
                            }

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

        public OpportunityDto GetById(long id)
        {
            DataSet _ds = null;

            OpportunityDto _oppData = new OpportunityDto();
            _oppData.Id = id;
            var Params = ParamOpportunity(_oppData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        OpportunityDto result = new OpportunityDto();
                        {
                            result.Id = row["Id"].ToDataConvertInt64();
                            result.InquiryID = row["InquiryID"].ToDataConvertNullInt64();
                            result.OppOrigin = row["OppOrigin"].ToString();
                            result.OppDestination = row["OppDestination"].ToString();
                            result.OppMode = row["OppMode"].ToString();
                            result.OppContainer = row["OppContainer"].ToString();
                            result.OppContType = row["OppContType"].ToString();
                            result.OppContainerCount = row["OppContainerCount"].ToString();
                            result.OppRecurring = row["OppRecurring"].ToString();
                            result.OppVerticalMarket = row["OppVerticalMarket"].ToString();
                            result.OppActivityPeriod = row["OppActivityPeriod"].ToString();
                            result.OppCarrier = row["OppCarrier"].ToString();
                            result.Weight = row["Weight"].ToDataConvertDouble();
                            result.Unit = row["Unit"].ToString();
                            result.CommodityID = row["CommodityID"].ToDataConvertNullInt64();
                            result.Competitor = row["Competitor"].ToString();
                            result.Terms = row["Terms"].ToString();
                            result.CountType = row["CountType"].ToString();
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

        public InquiryDto GetByIdFromEnquiry(long id)
        {
            DataSet _ds = null;

            InquiryDto _oppData = new InquiryDto();
            _oppData.Id = id;
            var Params = Param(_oppData, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        InquiryDto result = new InquiryDto();
                        {
                            result.Id = row["Id"].ToDataConvertInt64();
                            result.OrgInquiryId = row["OrgInquiryId"].ToString();
                            result.OrgEnquiryType = row["OrgEnquiryType"].ToString();
                            result.OrgName = row["OrgName"].ToString();
                            result.OrgAddress = row["OrgAddress"].ToString();
                            result.OrgCountry = row["OrgCountry"].ToDataConvertNullInt64();
                            result.OrgCity = row["OrgCity"].ToString();
                            result.OrgPostCode = row["OrgPostCode"].ToString();
                            result.OrgState = row["OrgState"].ToString();
                            result.OrgWebsite = row["OrgWebsite"].ToString();
                            result.OrgRegNo = row["OrgRegNo"].ToString();
                            result.InquiryContact = row["InquiryContact"].ToString();
                            result.Phone = row["Phone"].ToString();
                            result.Email = row["Email"].ToString();
                            result.MobNo = row["MobNo"].ToString();
                            result.FaxNo = row["FaxNo"].ToString();
                            result.JobDesc = row["JobDesc"].ToString();
                            result.LeadIntrest = row["LeadIntrest"].ToString();
                            result.SalesRepName = row["SalesRepName"].ToDataConvertNullInt64();
                            result.CreateBy = row["CreateBy"].ToString();
                            result.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            result.ModifyBy = row["ModifyBy"].ToString();
                            result.InquiryDate = row["InquiryDate"].ToDataConvertNullDateTime();
                            result.CommType = row["CommType"].ToString();
                            result.FName = row["FName"].ToString();

                            if (row["FileData"].ToString() != string.Empty)
                            {
                                result.FileData = (byte[])row["FileData"];
                            }
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

