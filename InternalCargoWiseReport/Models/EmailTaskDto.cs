using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace InternalCargoWiseReport.Models
{
 

    public class EmailTaskDto
    {
        public int id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string TMID { get; set; }
        public string Timing{ get; set; }
        public bool IsMonday    { get; set; }
        public bool IsTuesday   { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday  { get; set; }
        public bool IsFriday    { get; set; }
        public bool IsSaturday  { get; set; }
        public bool IsSunday    { get; set; }
        public string MailTo { get; set; }
        public string MailCC { get; set; }
        public string MailBCC { get; set; }
        public string MobileNo { get; set; }
        public string GroupBy { get; set; }

        public bool IsEmail { get; set; }
        public bool IsMsg { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsActiveT { get; set; }
        public long CreateBy { get; set; }

        public string TaskID { get; set; }
        public string TaskName { get; set; }

        public string CompanyCode { get; set; }
        public DataTable EmailTaskMasterDt { get; set; }
        public DataTable EmailTaskTransDt { get; set; }
    }
}