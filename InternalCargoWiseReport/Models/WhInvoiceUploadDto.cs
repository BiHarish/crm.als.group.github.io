using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhInvoiceUploadDto
    {
        public long? ID { get; set; }
        public string Bu { get; set; }
        public string CustomerID { get; set; }

        public string Message { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public long CreateBy { get; set; }
        public string Path { get; set; }
        public string Password { get; set; }
        public string InvoiceNO { get; set; }
    }

    public class WhApproverDTo
    {
        public long? ID { get; set; }
        public string EmailID { get; set; }
        public string BU { get; set; }
        public string SeqNo { get; set; }

    }
    public class WhInvoiceUploadTransDTo
    {
        public long? ID { get; set; }
        public long? Inv_Upl_Mas_ID { get; set; }
        public long? ApproverID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        //eXTA Field
        public string InvoiceNo { get; set; }
    }
    public class WhBindBuDTo
    {
        public long? MemberID { get; set;}
        public string BUID { get; set; }
        public string BUName { get; set; }
    }


}