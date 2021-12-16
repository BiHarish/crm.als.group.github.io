using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class MisBudgetDto
    {
        public long? mbid { get; set; }
        public long? mbdivisionid { get; set; }
        public long? mbmtyid { get; set; }
        public string mbfinancialyear { get; set; }
        public long? mbmpmid { get; set; }
        public double? mbApr { get; set; }
        public double? mbMay { get; set; }
        public double? mbJun { get; set; }
        public double? mbJul { get; set; }
        public double? mbAug { get; set; }
        public double? mbSep { get; set; }
        public double? mbOct { get; set; }
        public double? mbNov { get; set; }
        public double? mbDec { get; set; }
        public double? mbJan { get; set; }
        public double? mbFeb { get; set; }
        public double? mbMar { get; set; }
        public double? mbTotal { get; set; }
        public double? mbYtd { get; set; }
        public long? mbcreateby { get; set; }
        public string MonthName { get; set; }
    }
    public class UploadBudgetAndActualDto
    {
        public DataTable data { get; set; }
        public string FinYear { get; set; }
        public long TypeID { get; set; }
        public long DivID { get; set; }

    }

    public class MisBudgetVsActualConsolDto
    {
        public string financialyear { get; set; }
        public string division { get; set; }
        public long month { get; set; }
        

    }
}