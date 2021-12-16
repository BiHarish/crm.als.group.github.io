using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class CrmMeetingScheduleDto
    {
        public long   ID { get; set; }
        public long?  LeadID { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string JointCaller { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string RelatedTo { get; set; }
        public string Location { get; set; }
        public string Products { get; set; }
        public string Visibility { get; set; }
        public string AssignedTo { get; set; }
        public double? TotalKM { get; set; }
        public double? TotalAmt { get; set; }
          public bool IsPrint { get; set; }
          public long CreateBy { get; set; }
        public string RelatedToName { get; set; }

        //Extra properties
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
        public string EmpCode { get; set; }
        public string CreatedUserName { get; set; }
        public string SalesPersonname { get; set; }
        public long SrNo { get; set; }
        public DateTime? CreateOn { get; set; }
        public double? PerKm { get; set; }
        public string CustomerName { get; set; }
        public string ContactPersonName { get; set; }
        public string ClaimType { get; set; }
        public string Remarks { get; set; }

    }
}