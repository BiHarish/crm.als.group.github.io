using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class InquiryDto
    {
        public long Id { get; set; }
        public string OrgInquiryId { get; set; }
        public string OrgEnquiryType { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public long? OrgCountry { get; set; }
        public string OrgCity { get; set; }
        public string OrgPostCode { get; set; }
        public string OrgState { get; set; }
        public string OrgWebsite { get; set; }
        public string OrgRegNo { get; set; }
        public string InquiryContact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MobNo { get; set; }
        public string FaxNo { get; set; }
        public string JobDesc { get; set; }
        public string LeadIntrest { get; set; }
        public long? SalesRepName { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
        public DateTime? InquiryDate { get; set; }
        public string CommType { get; set; }
        public string FName { get; set; }
        public byte[] FileData { get; set; }

        //Extra Properties
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ContainerCount { get; set; }
        public string RepName { get; set; }
        public int SrNo { get; set; }
        public string Opportunity { get; set; }
        public long? OppID { get; set; }
    }

    public class OpportunityDto
    {
        public long Id { get; set; }
        public long? InquiryID { get; set; }
        public string OppOrigin { get; set; }
        public string OppDestination { get; set; }
        public string OppMode { get; set; }
        public string OppContainer { get; set; }
        public string OppContType { get; set; }
        public string OppContainerCount { get; set; }
        public string OppRecurring { get; set; }
        public string OppVerticalMarket { get; set; }
        public string OppActivityPeriod { get; set; }
        public string OppCarrier { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public long? CommodityID { get; set; }
        public string Competitor { get; set; }
        public string Terms { get; set; }
        public string CountType { get; set; }
    }
}