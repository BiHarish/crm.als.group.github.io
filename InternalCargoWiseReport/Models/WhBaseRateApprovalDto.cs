using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhBaseRateApprovalDto
    {
        public long? Id { get; set; }
        public long? WhLeadID { get; set; }
        public long? ApproverID { get; set; }
        public long? ApprovedDate { get; set; }
        public string ApproverStatus { get; set; }
        public long? CreateBy { get; set; }

        //Extra Properties

        public string BdMailID { get; set; }
    }
}