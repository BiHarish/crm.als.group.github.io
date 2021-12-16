using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class SalesRepMasterDto
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string MailID { get; set; }
        public string Department { get; set; }
        public string Region { get; set; }
        public bool Status { get; set; }
        public string Designation { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
        public long? SalesCordinatorID { get; set; }
        public string Type { get; set; }
        //Extra Properties
        public string OrgName { get; set; }
        public string InquiryNo { get; set; }
        public string CorMailID { get; set; }
    }
}