using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class WhCustomerMasterDto
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long? PhoneNo { get; set; }
        public string EmailID { get; set; }
        public string GSTNo { get; set; }
        public long? PinCode { get; set; }
        public long? CreateBy { get; set; }
        public string BusinessUnit { get; set; }

        public bool SCS { get; set; }
        public bool FF { get; set; }
        public bool Prime { get; set; }
        public bool Liquid { get; set; }
        public bool CFS { get; set; }

        //Extra field 
        public string BelongTo { get; set; }
        public string Result { get; set; }


    }

    public class WhCustomerAddTransDto
    {
        public long? ID { get; set; }
        public long? WhCustomerID { get; set; }
        public string CustAddress { get; set; }
        public long? CreateBy { get; set; }
        public string RowNumber { get; set; }
      
    }
}