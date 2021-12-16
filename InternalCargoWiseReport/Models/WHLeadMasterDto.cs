using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WHLeadMasterDto
    {
        public long ID { get; set; }
        public string LeadNo { get; set; }
        public string LeadSource { get; set; }
        public string CustomerName { get; set; }
        public string Stage { get; set; }
        public string Lineofbusiness { get; set; }
        public string OpportunityBrief { get; set; }
        public string StatusStage { get; set; }
        public double MonthlyBilling { get; set; }
        public double GP { get; set; }
        public DateTime? ProjectETA { get; set; }
        public string DesignatedBD { get; set; }
        public string DesignatedSolution { get; set; }
        public string StatusUpdate { get; set; }
        public string CreateBy { get; set; }
        public string CreateByName { get; set; }
        public DateTime? CreateOn { get; set; }
        public int SrNo { get; set; }
        public string New_Encirclement { get; set; }
        public string BU { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonMailID { get; set; }
        public string ContactPersonPhoneNo { get; set; }
        public string Region { get; set; }
        public string Segment { get; set; }
        public long? UserTypeID { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string TradeLane { get; set; }
        public string ValueAdded { get; set; }
        public double? Qty { get; set; }
        public string Unit { get; set; }
        public string FileName { get; set; }


        public string BusinessDriver { get; set; }
        public string NoOfTues { get; set; }
        public double? AvgNoOfStay { get; set; }
        public double? ExpectedGroundRent { get; set; }
        public string AvgRelExpected { get; set; }
        public string CategoryOfCompany { get; set; }

        public double? Revenue { get; set; }

        public string RevenueRange { get; set; }

        public long? Route { get; set; }
        public DateTime? ProjectATA { get; set; }
        public long? Shipment { get; set; }
        public double? Rate { get; set; }
        public string CFS { get; set; }
        public string IncoTerms { get; set; }
        public string ITSystem { get; set; }
        public string ITSystemName { get; set; }
        public long? PostNegotitationStageID { get; set; }
        public string Competitor { get; set; }
        public string Reason { get; set; }
        public string Cancelled { get; set; }
        public long? BranchMasterID { get; set; }
        public long? AddrsTransID { get; set; }

        //extra field
        public long? UserID { get; set; }
        public string QtyAndUnit { get; set; }
        public string LeadType { get; set; }
        public double Value { get; set; }
        public string SystemIpAddr { get; set; }
        public string On1 { get; set; }
        public string Days7 { get; set; }
        public string Days14 { get; set; }
        public string NoOfDays { get; set; }
        public string Duration { get; set; }
        public string IsGreen { get; set; }
        public string PrevStageDays { get; set; }

        public string RouteName { get; set; }

        public string PricingType { get; set; }
        public string ContractType { get; set; }
        public string UOM { get; set; }
        public double? Qty_Nos { get; set; }
        public double? PerUnitRevenue { get; set; }

        public string LostRemarks { get; set; }

        public string Location { get; set; }
        public long? ServiceTypeID { get; set; }
        public long? VehicleTypeID { get; set; }
        public string NoOfTrip { get; set; }
        public DateTime? EnquiryReceiveDate { get; set; }
        public DateTime? TargetSubmissionDate { get; set; }
        public DateTime? DataReceiveDate { get; set; }
        public DateTime? CostingReadinessDate { get; set; }
        public DateTime? CostingReviewedDate { get; set; }
        public DateTime? ActualSubmissionDate { get; set; }
        public DateTime? GoLiveDate { get; set; }
        public string SOPAfterSubmission { get; set; }
        public double? SizeOfWarehouse { get; set; }
        public string CostingReviewedBy { get; set; }
        public string LeadCurrentStatus { get; set; }
        public DateTime? CreateFromDate { get; set; }
        public DateTime? CreateToDate { get; set; }
        public DateTime? ModifyFromDate { get; set; }
        public DateTime? ModifyToDate { get; set; }

        public double? PRCGP { get; set; }
        public double? POCGP { get; set; }
        public string ContractDurationType { get; set; }
        public string CreditPeriod { get; set; }
        public string CloseRemarks { get; set; }
        public double BaseRateExpected { get; set; }
        public bool IsHold { get; set; }

    }
    public class UploadWHLeadStatusDto
    {
        public DataTable LeadStatusdt { get; set; }
        public string UserTypeID { get; set; }

    }


    public class WHLeadStatusUpdateDto
    {
        public long ID { get; set; }
        public long? WHLeadID { get; set; }
        public string Status { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
        public long RowNumber { get; set; }
        public long UserTypeID { get; set; }
        public string LeadType { get; set; }
        public long? BU { get; set; }




        //Extra
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class WhMailApprovalDto
    {
        public long? WhLeadID { get; set; }
        public long? ApproverId { get; set; }

        public string IsApproved { get; set; }
        public string Remarks { get; set; }

        public long? StatusStage { get; set; }
        public string CreateBy { get; set; }

        public string ModifyBy { get; set; }
        public string DocName { get; set; }

    }


    public class WhApproverMasterDTo
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public bool? IsActive { get; set; }
        public string Vertical { get; set; }
        public string StageApprover { get; set; }
        public long? CreateBy { get; set; }
        public long? ModifyBy { get; set; }

        //Extra Filed//
        public string LeadNo { get; set; }

    }
}