using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class CityDto
    {
        public int SrNo { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public long? CityStateId { get; set; }
        public bool CityIsActive { get; set; }
        public string StateName { get; set; }
    }
    public class GlbCompanyDto
    {
        public long ID { get; set; }
        public string GC_Code { get; set; }
        public string GC_Name { get; set; }
        public string GC_Address1 { get; set; }
        public string GC_Address2 { get; set; }
        public string GC_City { get; set; }
        public string GC_State { get; set; }
        public double GC_PostCode { get; set; }
        public double GC_NoOfAccountingPeriods { get; set; }
        public string GC_StartDate { get; set; }
        public string GC_RX_NKLocalCurrency { get; set; }
        public string GC_RN_NKCountryCode { get; set; }
        public double GC_IsActive { get; set; }
        public bool IsActive { get; set; }
    }
}