using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ICWR.Data 
{
    public class WHLeadMasterData
    {
        SqlParameter[] Param(WHLeadMasterDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[90];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@LeadNo", SqlDbType.NVarChar);
            Param[1].Value = obj.LeadNo;

            Param[2] = new SqlParameter("@LeadSource", SqlDbType.NVarChar);
            Param[2].Value = obj.LeadSource;

            Param[3] = new SqlParameter("@CustomerName", SqlDbType.NVarChar);
            Param[3].Value = obj.CustomerName;

            Param[4] = new SqlParameter("@Stage", SqlDbType.NVarChar);
            Param[4].Value = obj.Stage;

            Param[5] = new SqlParameter("@LineOfBusiness", SqlDbType.NVarChar);
            Param[5].Value = obj.Lineofbusiness;

            Param[6] = new SqlParameter("@OpportunityBrief", SqlDbType.NVarChar);
            Param[6].Value = obj.OpportunityBrief;

            Param[7] = new SqlParameter("@StatusStage", SqlDbType.NVarChar);
            Param[7].Value = obj.StatusStage;

            Param[8] = new SqlParameter("@MonthlyBilling", SqlDbType.Float);
            Param[8].Value = obj.MonthlyBilling;

            Param[9] = new SqlParameter("@GP", SqlDbType.Float);
            Param[9].Value = obj.GP;

            Param[10] = new SqlParameter("@ProjectETA", SqlDbType.DateTime);
            Param[10].Value = obj.ProjectETA;

            Param[11] = new SqlParameter("@DesignatedBD", SqlDbType.NVarChar);
            Param[11].Value = obj.DesignatedBD;

            Param[12] = new SqlParameter("@DesignatedSolution", SqlDbType.NVarChar);
            Param[12].Value = obj.DesignatedSolution;

            Param[13] = new SqlParameter("@StatusUpdate", SqlDbType.NVarChar);
            Param[13].Value = obj.StatusUpdate;

            Param[14] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[14].Value = obj.CreateBy;

            Param[15] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[15].Value = DateTime.Now;

            Param[16] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[16].Value = flag;

            Param[17] = new SqlParameter("@New_Encirclement", SqlDbType.NVarChar);
            Param[17].Value = obj.New_Encirclement;

            Param[18] = new SqlParameter("@BU", SqlDbType.NVarChar);
            Param[18].Value = obj.BU;

            Param[19] = new SqlParameter("@ContactPersonName", SqlDbType.NVarChar);
            Param[19].Value = obj.ContactPersonName;

            Param[20] = new SqlParameter("@ContactPersonDesignation", SqlDbType.NVarChar);
            Param[20].Value = obj.ContactPersonDesignation;

            Param[21] = new SqlParameter("@ContactPersonMailID", SqlDbType.NVarChar);
            Param[21].Value = obj.ContactPersonMailID;

            Param[22] = new SqlParameter("@ContactPersonPhoneNo", SqlDbType.NVarChar);
            Param[22].Value = obj.ContactPersonPhoneNo;

            Param[23] = new SqlParameter("@ProjectATA", SqlDbType.DateTime);
            Param[23].Value = obj.ProjectATA;

            Param[24] = new SqlParameter("@Region", SqlDbType.NVarChar);
            Param[24].Value = obj.Region;
            Param[25] = new SqlParameter("@Segment", SqlDbType.NVarChar);
            Param[25].Value = obj.Segment;
            Param[26] = new SqlParameter("@UserTypeID", SqlDbType.BigInt);
            Param[26].Value = obj.UserTypeID;
            Param[27] = new SqlParameter("@UserID", SqlDbType.BigInt);
            Param[27].Value = obj.UserID;
            Param[28] = new SqlParameter("@LocationFrom", SqlDbType.NVarChar);
            Param[28].Value = obj.LocationFrom;
            Param[29] = new SqlParameter("@LocationTo", SqlDbType.NVarChar);
            Param[29].Value = obj.LocationTo;
            Param[30] = new SqlParameter("@TradeLane", SqlDbType.NVarChar);
            Param[30].Value = obj.TradeLane;
            Param[31] = new SqlParameter("@ValueAdded", SqlDbType.NVarChar);
            Param[31].Value = obj.ValueAdded;
            Param[32] = new SqlParameter("@Qty", SqlDbType.Float);
            Param[32].Value = obj.Qty;
            Param[33] = new SqlParameter("@Unit", SqlDbType.NVarChar);
            Param[33].Value = obj.Unit;
            Param[34] = new SqlParameter("@FileName", SqlDbType.NVarChar);
            Param[34].Value = obj.FileName;
            Param[35] = new SqlParameter("@LeadType", SqlDbType.NVarChar);
            Param[35].Value = obj.LeadType;
            Param[36] = new SqlParameter("@SystemIpAddr", SqlDbType.NVarChar);
            Param[36].Value = obj.SystemIpAddr;
            Param[37] = new SqlParameter("@BusinessDriver", SqlDbType.NVarChar);
            Param[37].Value = obj.BusinessDriver;
            Param[38] = new SqlParameter("@NoOfTues", SqlDbType.NVarChar);
            Param[38].Value = obj.NoOfTues;
            Param[39] = new SqlParameter("@AvgNoOfStay", SqlDbType.Float);
            Param[39].Value = obj.AvgNoOfStay;
            Param[40] = new SqlParameter("@ExpectedGroundRent", SqlDbType.Float);
            Param[40].Value = obj.ExpectedGroundRent;
            Param[41] = new SqlParameter("@AvgRelExpected", SqlDbType.NVarChar);
            Param[41].Value = obj.AvgRelExpected;
            Param[42] = new SqlParameter("@CategoryOfCompany", SqlDbType.NVarChar);
            Param[42].Value = obj.CategoryOfCompany;
            Param[43] = new SqlParameter("@Revenue", SqlDbType.Float);
            Param[43].Value = obj.Revenue;
            Param[44] = new SqlParameter("@Route", SqlDbType.BigInt);
            Param[44].Value = obj.Route;
            Param[45] = new SqlParameter("@RevenueRange", SqlDbType.NVarChar);
            Param[45].Value = obj.RevenueRange;
            Param[46] = new SqlParameter("@Rate", SqlDbType.Float);
            Param[46].Value = obj.Rate;
            Param[47] = new SqlParameter("@CFS", SqlDbType.NVarChar);
            Param[47].Value = obj.CFS;
            Param[48] = new SqlParameter("@Shipment", SqlDbType.BigInt);
            Param[48].Value = obj.Shipment;
            Param[49] = new SqlParameter("@IncoTerms", SqlDbType.NVarChar);
            Param[49].Value = obj.IncoTerms;

            Param[50] = new SqlParameter("@PricingType", SqlDbType.NVarChar);
            Param[50].Value = obj.PricingType;
            Param[51] = new SqlParameter("@ContractType", SqlDbType.NVarChar);
            Param[51].Value = obj.ContractType;
            Param[52] = new SqlParameter("@UOM", SqlDbType.NVarChar);
            Param[52].Value = obj.UOM;
            Param[53] = new SqlParameter("@Qty_Nos", SqlDbType.Float);
            Param[53].Value = obj.Qty_Nos;
            Param[54] = new SqlParameter("@PerUnitRevenue", SqlDbType.Float);
            Param[54].Value = obj.PerUnitRevenue;
            Param[55] = new SqlParameter("@ITSystem", SqlDbType.NVarChar);
            Param[55].Value = obj.ITSystem;
            Param[56] = new SqlParameter("@ITSystemName", SqlDbType.NVarChar);
            Param[56].Value = obj.ITSystemName;

            Param[57] = new SqlParameter("@PostNegotitationStageID", SqlDbType.BigInt);
            Param[57].Value = obj.PostNegotitationStageID;
            Param[58] = new SqlParameter("@Competitor", SqlDbType.NVarChar);
            Param[58].Value = obj.Competitor;
            Param[59] = new SqlParameter("@Reason", SqlDbType.NVarChar);
            Param[59].Value = obj.Reason;
            Param[60] = new SqlParameter("@Cancelled", SqlDbType.NVarChar);
            Param[60].Value = obj.Cancelled;
            Param[61] = new SqlParameter("@LostRemarks", SqlDbType.NVarChar);
            Param[61].Value = obj.LostRemarks;
            Param[62] = new SqlParameter("@Location", SqlDbType.NVarChar);
            Param[62].Value = obj.Location;
            Param[63] = new SqlParameter("@ServiceTypeID", SqlDbType.BigInt);
            Param[63].Value = obj.ServiceTypeID;
            Param[64] = new SqlParameter("@VehicleTypeID", SqlDbType.BigInt);
            Param[64].Value = obj.VehicleTypeID;
            Param[65] = new SqlParameter("@NoOfTrip", SqlDbType.NVarChar);
            Param[65].Value = obj.NoOfTrip;
            Param[66] = new SqlParameter("@EnquiryReceiveDate", SqlDbType.DateTime);
            Param[66].Value = obj.EnquiryReceiveDate;
            Param[67] = new SqlParameter("@TargetSubmissionDate", SqlDbType.DateTime);
            Param[67].Value = obj.TargetSubmissionDate;
            Param[68] = new SqlParameter("@DataReceiveDate", SqlDbType.DateTime);
            Param[68].Value = obj.DataReceiveDate;
            Param[69] = new SqlParameter("@CostingReadinessDate", SqlDbType.DateTime);
            Param[69].Value = obj.CostingReadinessDate;
            Param[70] = new SqlParameter("@CostingReviewedDate", SqlDbType.DateTime);
            Param[70].Value = obj.CostingReviewedDate;
            Param[71] = new SqlParameter("@ActualSubmissionDate", SqlDbType.DateTime);
            Param[71].Value = obj.ActualSubmissionDate;
            Param[72] = new SqlParameter("@GoLiveDate", SqlDbType.DateTime);
            Param[72].Value = obj.GoLiveDate;
            Param[73] = new SqlParameter("@SOPAfterSubmission", SqlDbType.NVarChar);
            Param[73].Value = obj.SOPAfterSubmission;
            Param[74] = new SqlParameter("@SizeOfWarehouse", SqlDbType.Float);
            Param[74].Value = obj.SizeOfWarehouse;
            Param[75] = new SqlParameter("@CostingReviewedBy", SqlDbType.NVarChar);
            Param[75].Value = obj.CostingReviewedBy;
            Param[76] = new SqlParameter("@BranchMasterID", SqlDbType.BigInt);
            Param[76].Value = obj.BranchMasterID;
            Param[77] = new SqlParameter("@AddrsTransID", SqlDbType.BigInt);
            Param[77].Value = obj.AddrsTransID;

            Param[78] = new SqlParameter("@CreateFromDate", SqlDbType.Date);
            Param[78].Value = obj.CreateFromDate;
            Param[79] = new SqlParameter("@CreateToDate", SqlDbType.Date);
            Param[79].Value = obj.CreateToDate;
            Param[80] = new SqlParameter("@ModifyFromDate", SqlDbType.Date);
            Param[80].Value = obj.ModifyFromDate;
            Param[81] = new SqlParameter("@ModifyToDate", SqlDbType.Date);
            Param[81].Value = obj.ModifyToDate;
            Param[82] = new SqlParameter("@LeadCurrentStatus", SqlDbType.NVarChar);
            Param[82].Value = obj.LeadCurrentStatus;

            Param[83] = new SqlParameter("@PRCGP", SqlDbType.Decimal);
            Param[83].Value = obj.PRCGP;
            Param[84] = new SqlParameter("@POCGP", SqlDbType.Decimal);
            Param[84].Value = obj.POCGP;
            Param[85] = new SqlParameter("@ContractDurationType", SqlDbType.NVarChar);
            Param[85].Value = obj.ContractDurationType;
            Param[86] = new SqlParameter("@CreditPeriod", SqlDbType.NVarChar);
            Param[86].Value = obj.CreditPeriod;
            Param[87] = new SqlParameter("@CloseRemarks", SqlDbType.NVarChar);
            Param[87].Value = obj.CloseRemarks;
            Param[88] = new SqlParameter("@BaseRateExpected", SqlDbType.Float);
            Param[88].Value = obj.BaseRateExpected;
            Param[89] = new SqlParameter("@IsHold", SqlDbType.Bit);
            Param[89].Value = obj.IsHold;




            return Param;
        }

        SqlParameter[] ParamStatus(WHLeadStatusUpdateDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Status", SqlDbType.NVarChar);
            Param[1].Value = obj.Status;

            Param[2] = new SqlParameter("@WHLeadID", SqlDbType.BigInt);
            Param[2].Value = obj.WHLeadID;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@ModifyBy", SqlDbType.BigInt);
            Param[4].Value = obj.ModifyBy;

            Param[5] = new SqlParameter("@UserTypeID", SqlDbType.BigInt);
            Param[5].Value = obj.UserTypeID;

            Param[6] = new SqlParameter("@LeadType", SqlDbType.NVarChar);
            Param[6].Value = obj.LeadType;
            Param[7] = new SqlParameter("@BU", SqlDbType.BigInt);
            Param[7].Value = obj.BU;


            return Param;
        }

        SqlParameter[] ParamMailApprover(WhMailApprovalDto obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[9];
            Param[0] = new SqlParameter("@WhLeadID", SqlDbType.BigInt);
            Param[0].Value = obj.WhLeadID;

            Param[1] = new SqlParameter("@ApproverId", SqlDbType.BigInt);
            Param[1].Value = obj.ApproverId;

            Param[2] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
            Param[2].Value = obj.Remarks;

            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = flag;

            Param[4] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[4].Value = obj.CreateBy;

            Param[5] = new SqlParameter("@ModifyBy", SqlDbType.BigInt);
            Param[5].Value = obj.ModifyBy;

            Param[6] = new SqlParameter("@StatusStage", SqlDbType.BigInt);
            Param[6].Value = obj.StatusStage;

            Param[7] = new SqlParameter("@IsApproved", SqlDbType.NVarChar);
            Param[7].Value = obj.IsApproved;

            Param[8] = new SqlParameter("@DocName", SqlDbType.NVarChar);
            Param[8].Value = obj.DocName;


            return Param;
        }

        SqlParameter[] ParamMailApproverMaster(WhApproverMasterDTo obj, int flag)
        {

            SqlParameter[] Param = new SqlParameter[10];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = obj.ID;

            Param[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
            Param[1].Value = obj.Name;

            Param[2] = new SqlParameter("@EmailID", SqlDbType.NVarChar);
            Param[2].Value = obj.EmailID;

            Param[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Param[3].Value = obj.IsActive;

            Param[4] = new SqlParameter("@Vertical", SqlDbType.NVarChar);
            Param[4].Value = obj.Vertical;

            Param[5] = new SqlParameter("@StageApprover", SqlDbType.NVarChar);
            Param[5].Value = obj.StageApprover;

            Param[6] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[6].Value = flag;

            Param[7] = new SqlParameter("@CreateBy", SqlDbType.BigInt);
            Param[7].Value = obj.CreateBy;

            Param[8] = new SqlParameter("@ModifyBy", SqlDbType.BigInt);
            Param[8].Value = obj.ModifyBy;

            Param[9] = new SqlParameter("@LeadNo", SqlDbType.NVarChar);
            Param[9].Value = obj.LeadNo;

            return Param;
        }


        public long Insert(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadMaster", Params);

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

        public long InsertStatus(WHLeadStatusUpdateDto obj)
        {
            var Params = ParamStatus(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

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

        public long InsertWhLogs(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 6);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadMaster", Params);

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


        public long InsertMailApprover(WhMailApprovalDto obj)
        {
            var Params = ParamMailApprover(obj, 1);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

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
        public long InsertMailApproverForStage5(WhMailApprovalDto obj)
        {
            var Params = ParamMailApprover(obj, 6);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

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


        public long InsertMailApproverForStage8(WhMailApprovalDto obj)
        {
            var Params = ParamMailApprover(obj, 7);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

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
        public bool updateMailApprover(WhMailApprovalDto obj)
        {
            var Params = ParamMailApprover(obj, 3); 
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

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
        public bool Update(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 2);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

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

        public bool UpdateForApprovalRequest(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 60);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

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

        public bool UpdateForOtherCrm(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 20);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

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

        public bool UpdateWhLeadLogs(WHLeadMasterDto obj)
        {
            var Params = Param(obj, 7);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

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

        public WHLeadMasterDto GetById(long id, int? UserTypeID)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto(); 
            LeadData.ID = id;
            LeadData.UserTypeID = UserTypeID;
            var Params = Param(LeadData, 55);//old 4
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHLeadMasterDto result = new WHLeadMasterDto();
                        {
                            result.ID = row["ID"].ToDataConvertInt64();
                            result.LeadNo = row["LeadNo"].ToString();
                            result.LeadSource = row["LeadSource"].ToString();
                            result.CustomerName = row["CustomerName"].ToString();
                            result.Stage = row["Stage"].ToString();
                            result.Lineofbusiness = row["Lineofbusiness"].ToString();
                            result.OpportunityBrief = row["OpportunityBrief"].ToString();
                            result.StatusStage = row["StatusStage"].ToString();
                            result.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            result.GP = row["GP"].ToDataConvertDouble();
                            result.ProjectETA = row["ProjectETA"].ToDataConvertNullDateTime();
                            result.DesignatedBD = row["DesignatedBD"].ToString();
                            result.DesignatedSolution = row["DesignatedSolution"].ToString();
                            result.StatusUpdate = row["StatusUpdate"].ToString();
                            result.CreateBy = row["CreateBy"].ToString();
                            result.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            result.New_Encirclement = row["New_Encirclement"].ToString();
                            result.BU = row["BU"].ToString();
                            result.ContactPersonName = row["ContactPersonName"].ToString();
                            result.ContactPersonDesignation = row["ContactPersonDesignation"].ToString();
                            result.ContactPersonMailID = row["ContactPersonMailID"].ToString();
                            result.ContactPersonPhoneNo = row["ContactPersonPhoneNo"].ToString();
                            result.ProjectATA = row["ProjectATA"].ToDataConvertNullDateTime();
                            result.Region = row["Region"].ToString();
                            result.Segment = row["Segment"].ToString();
                            result.UserTypeID = row["UserTypeID"].ToDataConvertNullInt64();
                            result.LocationFrom = row["LocationFrom"].ToString();
                            result.LocationTo = row["LocationTo"].ToString();
                            result.TradeLane = row["TradeLane"].ToString();
                            result.ValueAdded = row["ValueAdded"].ToString();
                            result.Qty = row["Qty"].ToDataConvertNullDouble();
                            result.Unit = row["Unit"].ToString();
                            result.FileName = row["FileName"].ToString();
                            result.BusinessDriver = row["BusinessDriver"].ToString();
                            result.NoOfTues = row["NoOfTues"].ToString();
                            result.AvgNoOfStay = row["AvgNoOfStay"].ToDataConvertNullDouble();
                            result.ExpectedGroundRent = row["ExpectedGroundRent"].ToDataConvertNullDouble();
                            result.AvgRelExpected = row["AvgRelExpected"].ToString();
                            result.CategoryOfCompany = row["CategoryOfCompany"].ToString();
                            result.Revenue = row["Revenue"].ToDataConvertNullDouble();
                            result.Route = row["LocationRoute"].ToDataConvertNullInt64();
                            result.RevenueRange = row["RevenueRange"].ToString();
                            result.Rate = row["Rate"].ToDataConvertDouble();
                            result.CFS = row["CFS"].ToString();
                            result.Shipment = row["Shipment"].ToDataConvertNullInt64();
                            result.IncoTerms = row["IncoTerms"].ToString();
                            result.PricingType = row["PricingType"].ToString();
                            result.ContractType = row["ContractType"].ToString();
                            result.LeadType = row["LeadType"].ToString();
                            result.UOM = row["UOM"].ToString();
                            result.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            result.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
                            result.ITSystem = row["ITSystem"].ToString();
                            result.ITSystemName = row["ITSystemName"].ToString();

                            result.PostNegotitationStageID = row["PostNegotitationStageID"].ToDataConvertNullInt64();
                            result.Competitor = row["Competitor"].ToString();
                            result.Reason = row["Reason"].ToString();
                            result.Cancelled = row["Cancelled"].ToString();
                            result.LostRemarks = row["LostRemarks"].ToString();
                            result.Location = row["Location"].ToString();
                            result.ServiceTypeID = row["ServiceTypeID"].ToDataConvertNullInt64();
                            result.VehicleTypeID = row["VehicleTypeID"].ToDataConvertNullInt64();
                            result.NoOfTrip = row["NoOfTrip"].ToString();
                            result.EnquiryReceiveDate = row["EnquiryReceiveDate"].ToDataConvertNullDateTime();
                            result.TargetSubmissionDate = row["TargetSubmissionDate"].ToDataConvertNullDateTime();
                            result.DataReceiveDate = row["DataReceiveDate"].ToDataConvertNullDateTime();
                            result.CostingReadinessDate = row["CostingReadinessDate"].ToDataConvertNullDateTime();
                            result.CostingReviewedDate = row["CostingReviewedDate"].ToDataConvertNullDateTime();
                            result.ActualSubmissionDate = row["ActualSubmissionDate"].ToDataConvertNullDateTime();
                            result.GoLiveDate = row["GoLiveDate"].ToDataConvertNullDateTime();
                            result.SOPAfterSubmission = row["SOPAfterSubmission"].ToString();
                            result.SizeOfWarehouse = row["SizeOfWarehouse"].ToDataConvertNullDouble();
                            result.CostingReviewedBy = row["CostingReviewedBy"].ToString();
                            result.BranchMasterID = row["BranchMasterID"].ToDataConvertNullInt64();
                            result.AddrsTransID = row["AddrsTransID"].ToDataConvertNullInt64();
                            result.PRCGP = row["PRCGP"].ToDataConvertDouble();
                            result.POCGP = row["POCGP"].ToDataConvertDouble();
                            result.ContractDurationType = row["ContractDurationType"].ToString();
                            result.CreditPeriod = row["CreditPeriod"].ToString();
                            result.CloseRemarks = row["CloseRemarks"].ToString();
                            result.BaseRateExpected = row["BaseRateExpected"].ToDataConvertDouble();
                            result.IsHold = row["IsHold"].ToDataConvertBool();

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

        public WHLeadMasterDto GetByIdForApprovelDetails(long id)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto();
            LeadData.ID = id;
            var Params = Param(LeadData, 61);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHLeadMasterDto result = new WHLeadMasterDto();
                        {
                            result.CustomerName = row["CustomerName"].ToString();
                            result.Lineofbusiness = row["Lineofbusiness"].ToString();
                            result.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            result.GP = row["GP"].ToDataConvertDouble();
                            result.ProjectETA = row["ProjectETA"].ToDataConvertNullDateTime();
                            result.New_Encirclement = row["New_Encirclement"].ToString();
                            result.Region = row["Region"].ToString();
                            result.Segment = row["Segment"].ToString();
                            result.Qty = row["Qty"].ToDataConvertNullDouble();
                            result.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            result.PricingType = row["PricingType"].ToString();
                            result.ContractType = row["ContractType"].ToString();
                            result.UOM = row["UOM"].ToString();
                            result.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
                            result.ITSystem = row["ITSystem"].ToString();
                            result.ITSystemName = row["ITSystemName"].ToString();
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

        public IList<WHLeadStatusUpdateDto> GetByIdForStatus(long WhLeadID)
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            LeadData.WHLeadID = WhLeadID;
            var Params = ParamStatus(LeadData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Status = row["Status"].ToString();
                            obj.WHLeadID = row["WHLeadID"].ToDataConvertNullInt64();
                            obj.ModifyBy = row["ModifyBy"].ToDataConvertNullInt64();
                            obj.ModifyOn = row["ModifyOn"].ToDataConvertNullDateTime();
                            obj.RowNumber = row["RowNumber"].ToDataConvertInt64();

                            results.Add(obj);
                        }
                        return results;
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


        public IList<WHLeadMasterDto> GetByCustomerName(string Name, string CreateBy, string Stage, string StatusStage, string Encirclement, string Region,
            string Segment, string lineOfBusiness, int? UserTypeID, string LeadType, long? ID, string BDID,string CreateFromDate,string CreateToDate,string ModifyFromDate,
            string ModifyToDate,string LeadCurrentStatus)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.ID = ID.ToDataConvertInt64();
            _whLeadMaster.CustomerName = Name;
            _whLeadMaster.CreateBy = CreateBy;
            _whLeadMaster.StatusStage = StatusStage;
            _whLeadMaster.New_Encirclement = Encirclement;
            _whLeadMaster.Region = Region;
            _whLeadMaster.Segment = Segment;
            _whLeadMaster.Lineofbusiness = lineOfBusiness;
            _whLeadMaster.UserTypeID = UserTypeID;
            _whLeadMaster.LeadType = LeadType;
            _whLeadMaster.Stage = Stage;
            _whLeadMaster.DesignatedBD = BDID;
            _whLeadMaster.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            _whLeadMaster.CreateToDate = CreateToDate.ToConvertNullDateTime();
            _whLeadMaster.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            _whLeadMaster.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            _whLeadMaster.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(_whLeadMaster, 54);//OLD FLAG 3
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            // obj.LeadNo = row["LeadNo"].ToString();
                            obj.LeadSource = row["LeadSource"].ToString();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.Stage = row["Stage"].ToString();
                            obj.Lineofbusiness = row["Lineofbusiness"].ToString();
                            obj.OpportunityBrief = row["OpportunityBrief"].ToString();
                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            obj.GP = row["GP"].ToDataConvertDouble();
                            obj.ProjectETA = row["ProjectETA"].ToDataConvertNullDateTime();
                            obj.DesignatedBD = row["DesignatedBD"].ToString();
                            obj.DesignatedSolution = row["DesignatedSolution"].ToString();
                            obj.StatusUpdate = row["StatusUpdate"].ToString();
                            obj.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            obj.SrNo = row["SrNo"].ToDataConvertInt32();
                           // obj.CreateByName = row["CreateByName"].ToString();
                            obj.CreateBy = row["CreateBy"].ToString();
                            obj.New_Encirclement = row["New_Encirclement"].ToString();
                            obj.BU = row["BU"].ToString();
                            obj.ContactPersonName = row["ContactPersonName"].ToString();
                            obj.ContactPersonDesignation = row["ContactPersonDesignation"].ToString();
                            obj.ContactPersonMailID = row["ContactPersonMailID"].ToString();
                            obj.ContactPersonPhoneNo = row["ContactPersonPhoneNo"].ToString();
                            obj.ProjectATA = row["ProjectATA"].ToDataConvertNullDateTime();
                            obj.QtyAndUnit = row["Qty"].ToString();
                            obj.FileName = row["FileName"].ToString();
                            obj.Revenue = row["Revenue"].ToDataConvertNullDouble();
                            obj.IsHold = row["IsHold"].ToDataConvertBool();
                            // obj.LeadType = row["LeadType"].ToString();
                            // obj.UOM = row["UOM"].ToString();
                            // obj.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            // obj.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
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

        public DataSet getForWhLeadMasterExel(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region,
            string Segment, string LineOfBusiness, string LeadType, string BD, string CreateFromDate, string CreateToDate, string ModifyFromDate,
            string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.LeadType = LeadType;
            obj.DesignatedBD = BD;
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 64);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public DataSet getForWhLeadMasterExelForSCS(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region,
            string Segment, string LineOfBusiness, string BDID, string CreateFromDate, string CreateToDate, string ModifyFromDate,
            string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.DesignatedBD = BDID;
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 9);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public long uploadStatus(UploadWHLeadStatusDto obj)
        {
            SqlParameter[] Param = new SqlParameter[2];

            Param[0] = new SqlParameter("@LeadStatusdt", obj.LeadStatusdt);
            Param[1] = new SqlParameter("@UserTypeID", obj.UserTypeID);

            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpload", Param);

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

        public WHLeadMasterDto getLastLoginDate(long? userID)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto();
            LeadData.UserID = userID;
            var Params = Param(LeadData, 8);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHLeadMasterDto result = new WHLeadMasterDto();
                        {
                            result.CreateOn = row["LoginDate"].ToDataConvertNullDateTime();

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

        public IList<WHLeadMasterDto> getPipelineSplitRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 10);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getRegionRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 11);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getLineOfBusinessRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 12);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getStatusStageAndBillingRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 13);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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

        public long uploadCurrency(UploadWHLeadStatusDto obj)
        {
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@LeadStatusdt", obj.LeadStatusdt);

            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspCurrencyUpload", Param);

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

        public long uploadCurrencyParent(UploadWHLeadStatusDto obj)
        {
            SqlParameter[] Param = new SqlParameter[2];

            Param[0] = new SqlParameter("@LeadStatusdt", obj.LeadStatusdt);
            Param[1] = new SqlParameter("@UserTypeID", obj.UserTypeID);

            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspParentCurrencyUpload", Param);

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

        public IList<WHLeadMasterDto> getAllLeadByUserID(long? UserID, string Location)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.ID = UserID.ToDataConvertInt64();
            _whLeadMaster.CustomerName = Location;
            var Params = Param(_whLeadMaster, 14);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.CustomerName = row["CustomerName"].ToString();

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

        public IList<WHLeadMasterDto> getAllCustomerList(int? UserTypeID, string LeadType, long? ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.UserTypeID = UserTypeID;
            _whLeadMaster.LeadType = LeadType;
            _whLeadMaster.ID = ID.ToDataConvertInt64();

            var Params = Param(_whLeadMaster, 15);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.CustomerName = row["CustomerName"].ToString();

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

        public IList<WHLeadMasterDto> getFFPipelineSplitRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 16);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getFFRegionRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 17);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getFFLineOfBusinessRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 18);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getFFStatusStageAndBillingRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 19);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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


        public IList<WHLeadMasterDto> GetByCustomerNameForCFSInfra(string Name, string CreateBy, string Stage, string StatusStage, string Encirclement, string Region, string Segment, string lineOfBusiness, int? UserTypeID, string LeadType, long? ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.ID = ID.ToDataConvertInt64();
            _whLeadMaster.CustomerName = Name;
            _whLeadMaster.CreateBy = CreateBy;
            _whLeadMaster.StatusStage = StatusStage;
            _whLeadMaster.New_Encirclement = Encirclement;
            _whLeadMaster.Region = Region;
            _whLeadMaster.Segment = Segment;
            _whLeadMaster.Lineofbusiness = lineOfBusiness;
            _whLeadMaster.UserTypeID = UserTypeID;
            _whLeadMaster.LeadType = LeadType;
            _whLeadMaster.Stage = Stage;
            var Params = Param(_whLeadMaster, 54);//old code 21
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            // obj.LeadNo = row["LeadNo"].ToString();
                            obj.LeadSource = row["LeadSource"].ToString();
                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.Stage = row["Stage"].ToString();
                            obj.Lineofbusiness = row["Lineofbusiness"].ToString();
                            obj.OpportunityBrief = row["OpportunityBrief"].ToString();
                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            obj.GP = row["GP"].ToDataConvertDouble();
                            obj.ProjectETA = row["ProjectETA"].ToDataConvertNullDateTime();
                            obj.DesignatedBD = row["DesignatedBD"].ToString();
                            obj.DesignatedSolution = row["DesignatedSolution"].ToString();
                            obj.StatusUpdate = row["StatusUpdate"].ToString();
                            obj.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                            obj.SrNo = row["SrNo"].ToDataConvertInt32();
                           // obj.CreateByName = row["CreateByName"].ToString();
                            obj.CreateBy = row["CreateBy"].ToString();
                            obj.New_Encirclement = row["New_Encirclement"].ToString();
                            obj.BU = row["BU"].ToString();
                            obj.ContactPersonName = row["ContactPersonName"].ToString();
                            obj.ContactPersonDesignation = row["ContactPersonDesignation"].ToString();
                            obj.ContactPersonMailID = row["ContactPersonMailID"].ToString();
                            obj.ContactPersonPhoneNo = row["ContactPersonPhoneNo"].ToString();
                            obj.ProjectATA = row["ProjectATA"].ToDataConvertNullDateTime();
                            obj.QtyAndUnit = row["Qty"].ToString();
                            obj.FileName = row["FileName"].ToString();
                            obj.PricingType = row["PricingType"].ToString();
                            obj.ContractType = row["ContractType"].ToString();
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

        public DataSet getForWhLeadMasterExelForCfsInfra(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region, string Segment, string LineOfBusiness, string CreateFromDate, string CreateToDate, string ModifyFromDate,
            string ModifyToDate, string LeadCurrentStatus)
        { 
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
           // obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.LeadType = "CFSINFRA";
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 22);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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


        public DataSet getForWhLeadMasterExelForLiquid(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region, string Segment, string LineOfBusiness, string BD, string CreateFromDate, string CreateToDate, string ModifyFromDate,
            string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.DesignatedBD = BD;
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 23);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public DataSet getForWhLeadMasterExelForPrime(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region,
            string Segment, string LineOfBusiness, string LeadType, string BD, string CreateFromDate, string CreateToDate, string ModifyFromDate,
            string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.LeadType = LeadType;
            obj.DesignatedBD = BD;
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 24);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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


        #region  PrimeDashBoard
        public IList<WHLeadMasterDto> getPrimePipelineSplitRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 25);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getPrimeRegionRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 26);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getPrimeLineOfBusinessRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 27);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getPrimeStatusStageAndBillingRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 28);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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
        #endregion

        #region LiquidDashBoard
        public IList<WHLeadMasterDto> getLiquidPipelineSplitRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 29);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getLiquidRegionRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 30);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getLiquidLineOfBusinessRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 31);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getLiquidStatusStageAndBillingRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 32);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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
        #endregion

        #region InfraDashBoard
        public IList<WHLeadMasterDto> getInfraPipelineSplitRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 33);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getInfraRegionRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 34);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getInfraLineOfBusinessRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 35);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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
        public IList<WHLeadMasterDto> getInfraStatusStageAndBillingRecord()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 36);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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
        #endregion


        public IList<WHLeadMasterDto> getSegment(long? UserTypeID, string LeadType)
        {
            DataSet _ds = null;
            WHLeadStatusUpdateDto _whStatus = new WHLeadStatusUpdateDto();
            _whStatus.LeadType = LeadType;
            _whStatus.UserTypeID = UserTypeID.ToDataConvertInt64();

            var Params = ParamStatus(_whStatus, 8);//OLD 3
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();

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


        public DataSet GetCRMReport()
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            var Params = Param(obj1, 37);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
                        //DataRowCollection rows = _ds.Tables[0].Rows;
                        //IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        //foreach (DataRow row in rows)
                        //{
                        //    WHLeadMasterDto obj = new WHLeadMasterDto();

                        //    //obj.ID = row["ID"].ToDataConvertInt64();
                        //    obj.LeadSource = row["LeadSource"].ToString();
                        //    obj.CustomerName = row["CustomerName"].ToString();
                        //    obj.Stage = row["Stage"].ToString();
                        //    obj.Lineofbusiness = row["Lineofbusiness"].ToString();
                        //    obj.StatusStage = row["StatusStage"].ToString();
                        //    obj.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                        //    obj.GP = row["GP"].ToDataConvertDouble();
                        //    obj.CreateOn = row["CreateOn"].ToDataConvertNullDateTime();
                        //   // obj.SrNo = row["SrNo"].ToDataConvertInt32();
                        //    obj.QtyAndUnit = row["Qty"].ToString();
                        //    obj.Segment = row["Segment"].ToString();
                        //    obj.Region = row["Region"].ToString();
                        //    obj.LocationFrom = row["LocationFrom"].ToString();
                        //    obj.LocationTo = row["LocationTo"].ToString();
                        //    obj.RouteName = row["LocationRoute"].ToString();
                        //    obj.NoOfTues = row["NoOfTues"].ToString();
                        //    results.Add(obj);
                        //}
                        //return results;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public IList<WHLeadMasterDto> getSCSStageDashboardData()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 38);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.CustomerName = row["CustomerName"].ToString();
                            obj.MonthlyBilling = row["MONTHLYBILLING"].ToDataConvertDouble();
                            obj.DesignatedBD = row["BD"].ToString();
                            obj.On1 = row["On1"].ToString();
                            obj.Days7 = row["Day7"].ToString();
                            obj.Days14 = row["Day14"].ToString();
                            obj.NoOfDays = row["NoOfDays"].ToString();
                            obj.Duration = row["Duration"].ToString();
                            obj.IsGreen = row["isGreen"].ToString();
                            obj.PrevStageDays = row["PrevStageDays"].ToString();
                            obj.Unit = row["MBilling"].ToString();

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

        public IList<WHLeadStatusUpdateDto> getCountryData()
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            var Params = ParamStatus(LeadData, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();
                            obj.Code = row["Code"].ToString();

                            results.Add(obj);
                        }
                        return results;
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

        public IList<WHLeadStatusUpdateDto> getCrmStageData()
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            var Params = ParamStatus(LeadData, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();

                            results.Add(obj);
                        }
                        return results;
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

        public IList<WHLeadStatusUpdateDto> getCrmStageDataWithBu(long? Bu)
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            LeadData.BU = Bu;
            var Params = ParamStatus(LeadData, 9);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();

                            results.Add(obj);
                        }
                        return results;
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

        public IList<WHLeadStatusUpdateDto> getCrmTradelaneData()
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            var Params = ParamStatus(LeadData, 6);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();

                            results.Add(obj);
                        }
                        return results;
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


        public DataSet chkStageApprover(int LeadID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = LeadID;
            var Params = Param(obj1, 39);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }


        public IList<WhApproverMasterDTo> GetMailApproverMailIDData(string Vertical, string StageID, string LeadNo)
        {
            DataSet _ds = null;

            WhApproverMasterDTo LeadData = new WhApproverMasterDTo();
            LeadData.Vertical = Vertical;
            LeadData.StageApprover = StageID;
            LeadData.LeadNo = LeadNo;
            var Params = ParamMailApproverMaster(LeadData, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhApproverMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhApproverMasterDTo> results = new List<WhApproverMasterDTo>();

                        foreach (DataRow row in rows)
                        {
                            WhApproverMasterDTo obj = new WhApproverMasterDTo();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.EmailID = row["EmailID"].ToString();
                            obj.IsActive = row["IsActive"].ToDataConvertNullBool();
                            obj.Vertical = row["Vertical"].ToString();
                            obj.StageApprover = row["StageApprover"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            obj.ModifyBy = row["ModifyBy"].ToDataConvertNullInt64();
                            results.Add(obj);
                        }
                        return results;
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

        #region Dummay Data
        public IList<WHLeadMasterDto> getPipelineSplitRecordDemi()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 40);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Segment = row["Segment"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getRegionRecordDemi()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 41);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Region = row["Region"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getLineOfBusinessRecordDemi()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 42);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.Lineofbusiness = row["LineOfBusiness"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();

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

        public IList<WHLeadMasterDto> getStatusStageAndBillingRecordDemi()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();

            var Params = Param(_whLeadMaster, 43);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.StatusStage = row["StatusStage"].ToString();
                            obj.Value = row["Value"].ToDataConvertDouble();
                            obj.MonthlyBilling = row["TotalBilling"].ToDataConvertDouble();

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

        #endregion


        public WHLeadMasterDto getWhledMasterDetailByID(long id)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto();
            LeadData.ID = id;
            var Params = Param(LeadData, 44);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WHLeadMasterDto result = new WHLeadMasterDto();
                        {
                            result.CustomerName = row["CustomerName"].ToString();
                            result.Lineofbusiness = row["Lineofbusiness"].ToString();
                            result.MonthlyBilling = row["MonthlyBilling"].ToDataConvertDouble();
                            result.GP = row["GP"].ToDataConvertDouble();
                            result.ProjectETA = row["ProjectETA"].ToDataConvertNullDateTime();
                            result.New_Encirclement = row["New_Encirclement"].ToString();
                            result.ProjectATA = row["ProjectATA"].ToDataConvertNullDateTime();
                            result.Region = row["Region"].ToString();
                            result.Segment = row["Segment"].ToString();
                            result.PricingType = row["PricingType"].ToString();
                            result.ContractType = row["ContractType"].ToString();
                            // result.LeadType = row["LeadType"].ToString();
                            result.UOM = row["UOM"].ToString();
                            result.Qty_Nos = row["Qty_Nos"].ToDataConvertNullDouble();
                            result.PerUnitRevenue = row["PerUnitRevenue"].ToDataConvertNullDouble();
                            result.ITSystem = row["ITSystem"].ToString();
                            result.ITSystemName = row["ITSystemName"].ToString();
                            result.ContactPersonMailID = row["EmailID"].ToString();




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

        public IList<WhApproverMasterDTo> getApproverName(string WhLeadID)
        {
            DataSet _ds = null;

            WhMailApprovalDto LeadData = new WhMailApprovalDto();
            LeadData.WhLeadID = WhLeadID.ToLong();
            var Params = ParamMailApprover(LeadData, 4);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhApproverMasterDTo> results = new List<WhApproverMasterDTo>();

                        foreach (DataRow row in rows)
                        {
                            WhApproverMasterDTo obj = new WhApproverMasterDTo();

                            obj.Name = row["Name"].ToString();
                            obj.StageApprover = row["IsApproved"].ToString();
                            obj.EmailID = row["EmailID"].ToString();

                            results.Add(obj);
                        }
                        return results;
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

        public WhMailApprovalDto getApproverCount(string WhLeadID)
        {
            DataSet _ds = null;

            WhMailApprovalDto LeadData = new WhMailApprovalDto();
            LeadData.WhLeadID = WhLeadID.ToLong();
            var Params = ParamMailApprover(LeadData, 5);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStageApprover", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = _ds.Tables[0].Rows[0];
                        WhMailApprovalDto result = new WhMailApprovalDto();
                        {
                            result.ApproverId = row["Count"].ToDataConvertInt64();


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

        public DataSet getDetailsForStage5ForSecondApproverMail(long ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;
            var Params = Param(obj1, 45);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet IsThisFinalApprover(long WhLeadID, string StatusStage)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto();
            LeadData.ID = WhLeadID;
            LeadData.StatusStage = StatusStage;

            var Params = Param(LeadData, 46);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
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


        public DataSet getDetailsForStage5ForFinalApproverMail(long ID, string ApproverID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;
            obj1.UserTypeID = ApproverID.ToNullLong();
            var Params = Param(obj1, 47);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet getUserID(string WhLeadID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = WhLeadID.ToLong();
            var Params = Param(obj1, 48);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }


        public IList<WHLeadStatusUpdateDto> getSCSCrmStageData()
        {
            DataSet _ds = null;

            WHLeadStatusUpdateDto LeadData = new WHLeadStatusUpdateDto();
            var Params = ParamStatus(LeadData, 7);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadStatusUpdate", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadStatusUpdateDto> results = new List<WHLeadStatusUpdateDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadStatusUpdateDto obj = new WHLeadStatusUpdateDto();

                            obj.ID = row["ID"].ToDataConvertInt64();
                            obj.Name = row["Name"].ToString();

                            results.Add(obj);
                        }
                        return results;
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

        public IList<WhApproverMasterDTo> GetMailIDStage8(string Vertical, string StageID, string RqID)
        {
            DataSet _ds = null;

            WhApproverMasterDTo LeadData = new WhApproverMasterDTo();
            LeadData.Vertical = Vertical;
            LeadData.StageApprover = StageID;
            LeadData.ID = RqID.ToDataConvertNullInt64();
            var Params = ParamMailApproverMaster(LeadData, 2);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhApproverMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhApproverMasterDTo> results = new List<WhApproverMasterDTo>();

                        foreach (DataRow row in rows)
                        {
                            WhApproverMasterDTo obj = new WhApproverMasterDTo();

                            obj.ID = row["ID"].ToDataConvertNullInt64();
                            obj.Name = row["Name"].ToString();
                            obj.EmailID = row["EmailID"].ToString();
                            obj.IsActive = row["IsActive"].ToDataConvertNullBool();
                            obj.Vertical = row["Vertical"].ToString();
                            obj.StageApprover = row["StageApprover"].ToString();
                            obj.CreateBy = row["CreateBy"].ToDataConvertNullInt64();
                            obj.ModifyBy = row["ModifyBy"].ToDataConvertNullInt64();
                            results.Add(obj);
                        }
                        return results;
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

        public DataSet GetDetailsForMailFormatAtStage8(long ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;

            var Params = Param(obj1, 49);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet findFinalApproverAtStage8(long ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;

            var Params = Param(obj1, 50);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet GetFirstSenderID(long ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;

            var Params = Param(obj1, 51);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet GetNameAtStage8(long ID)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.ID = ID;

            var Params = Param(obj1, 52);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet getDataForReport(string FromDate,string ToDate,string CrmStage,string LeadType)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.LocationFrom = FromDate;
            obj1.LocationTo = ToDate;
            obj1.StatusStage = CrmStage;
            obj1.LeadType = LeadType;

            var Params = Param(obj1, 53);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }



        public DataSet getRptMgrWiseReport(string ReportFor, string Bu, long? CreateBy)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            obj1.LeadType = ReportFor;
            obj1.BU = Bu;
            obj1.CreateBy = CreateBy.ToString();

            var Params = Param(obj1, 56);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }


        public IList<WHLeadMasterDto> ChkCustomerWithBD(string CustomerID)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.CustomerName = CustomerID;

            var Params = Param(_whLeadMaster, 57);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.DesignatedBD = row["BD"].ToString();
                            obj.CustomerName = row["CustomerName"].ToString();

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

        #region CrmDashboard
        public DataSet getPipelineSplitRecordForCrm(string Employee,string BU)
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
            _whLeadMaster.DesignatedBD = Employee;
            _whLeadMaster.BU = BU;

            var Params = Param(_whLeadMaster, 59);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        
                        return _ds;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
        #endregion

        public DataSet ExcelDownloadFileForFFUser(string Name, string Encirclement, string StatusStage, long? CreateBy, string Region, string Segment, string LineOfBusiness,
           string CreateFromDate, string CreateToDate, string ModifyFromDate, string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            obj.CreateBy = CreateBy.ToString();
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.Region = Region;
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 62);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public IList<WHLeadMasterDto> getAllLeadStatus()
        {
            DataSet _ds = null;
            WHLeadMasterDto _whLeadMaster = new WHLeadMasterDto();
           
            var Params = Param(_whLeadMaster,63 );
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WHLeadMasterDto> results = new List<WHLeadMasterDto>();

                        foreach (DataRow row in rows)
                        {
                            WHLeadMasterDto obj = new WHLeadMasterDto();

                            obj.LeadCurrentStatus = row["LeadCurrentStatus"].ToString();
                           
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

        public DataSet getForWhLeadMasterExelForCustomBrokerage(string Name, string Encirclement, string StatusStage, string Stage, int? UserTypeID, string Region, string Segment, string LineOfBusiness, string CreateFromDate, string CreateToDate, string ModifyFromDate,
          string ModifyToDate, string LeadCurrentStatus)
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
            obj.CustomerName = Name;
            obj.New_Encirclement = Encirclement;
            obj.StatusStage = StatusStage;
            // obj.Stage = Stage;
            obj.UserTypeID = UserTypeID;
            obj.Region = Region;
            obj.Lineofbusiness = LineOfBusiness;
            obj.Segment = Segment;
            obj.LeadType = "CustomBrokerage";
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.CreateFromDate = CreateFromDate.ToConvertNullDateTime();
            obj.CreateToDate = CreateToDate.ToConvertNullDateTime();
            obj.ModifyFromDate = ModifyFromDate.ToConvertNullDateTime();
            obj.ModifyToDate = ModifyToDate.ToConvertNullDateTime();
            obj.LeadCurrentStatus = LeadCurrentStatus;
            var Params = Param(obj, 65);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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


        public IList<WhApproverMasterDTo> getBaseRateApproverMailID(string Vertical)
        {
            DataSet _ds = null;

            WhApproverMasterDTo approverdata = new WhApproverMasterDTo();
            approverdata.Vertical = Vertical;
            var Params = ParamMailApproverMaster(approverdata, 3);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhApproverMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        DataRowCollection rows = _ds.Tables[0].Rows;
                        IList<WhApproverMasterDTo> results = new List<WhApproverMasterDTo>();

                        foreach (DataRow row in rows)
                        {
                            WhApproverMasterDTo result = new WhApproverMasterDTo();

                            result.EmailID = row["EmailID"].ToString();
                            result.ID = row["ID"].ToDataConvertInt64();

                            results.Add(result);
                        }
                        return results;
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

        public DataSet GetConsoledateReport(long? id)
        {
            DataSet _ds = null;
            WHLeadMasterDto obj1 = new WHLeadMasterDto();
            var Params = Param(obj1, 1);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "usp_whlreport", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public DataSet getAllVerticalReport()
        {
            WHLeadMasterDto obj = new WHLeadMasterDto();
           
            var Params = Param(obj, 67);

            if (Params != null && Params.Count() > 0)
            {
                try
                {
                    DataSet _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWHLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
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

        public DataSet getResultForHistory(string CustomerID)
        {
            DataSet _ds = null;

            WHLeadMasterDto LeadData = new WHLeadMasterDto();
            LeadData.CustomerName = CustomerID;
            var Params = Param(LeadData, 68);
            if (Params != null)
            {
                try
                {
                    _ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspWhLeadMaster", Params);

                    if (_ds != null && _ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        return _ds;
                    }
                    else
                    {
                        return null;
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