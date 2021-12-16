using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhLeadContactTransDto
    {
        public long ID { get; set; }
        public long? WhLeadID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string MailID { get; set; }
        public string PhoneNo { get; set; }
        public string RowNumber { get; set; }
    }
}