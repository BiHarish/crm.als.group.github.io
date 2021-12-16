using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhLeadTypeTransactionDto
    {
        public long? ID { get; set; }
        public string LeadID { get; set; }
        public string LeadType { get; set; }
        public string UOM { get; set; }
        public double? Qty_Nos { get; set; }
        public double? PerUnitRevenue { get; set; }
        public string RowNumber { get; set; }
        

    }
}