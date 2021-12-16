using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class CompanyDto
    {
        public int? CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyEmailId { get; set; }
        public string CompanyMobileNo { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyPhone1 { get; set; }
        public string CompanyPhone2 { get; set; }
        public long? CompanyStateId { get; set; }
        public long? CompanyCityId { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyZipCode { get; set; }
        public int? CompanyBankId { get; set; }
        public string CompanyAccountNo { get; set; }
        public string CompanyIFSCode { get; set; }
        public string CompanyBankBranch { get; set; }
        public string CompanyLogo { get; set; }
        public bool CompanyIsActive { get; set; }
        public string CompanyStateName { get; set; }
        public string CompanyCityName { get; set; }
    }
}