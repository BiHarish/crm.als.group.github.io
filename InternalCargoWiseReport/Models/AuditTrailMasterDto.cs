using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class AuditTrailMasterDto
    {
        public long ID { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionHeader { get; set; }
        public string ActionName { get; set; }
        public long UserID { get; set; }
        public string Remarks { get; set; }
        public DateTime? ActionFromDate { get; set; }
        public DateTime? ActionToDate { get; set; } 

        //Extra properties
        public string UserName { get; set; }
    }
}