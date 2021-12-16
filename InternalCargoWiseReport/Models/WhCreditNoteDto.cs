using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhCreditNoteDto
    {
        public long ID { get; set; }
        public long? WhLeadID { get; set; }
        public long? CustomerID { get; set; }
        public double CreditRating { get; set; }
        public double CreditLimit { get; set; }
        public double CreditDays { get; set; }
        public string CreditFileName { get; set; }
        public long? CreateBy { get; set; }

    }

}