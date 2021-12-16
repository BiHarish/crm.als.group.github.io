using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class MisParticularsTransactionDto
    {
        public long? mptid { get; set; }
        public long? mptdivisionid { get; set; }
        public long? mptmtyid { get; set; }
        public string mptfinancialyear { get; set; }
        public long? mpmid { get; set; }
        public double mptApr { get; set; }
        public double mptMay { get; set; }
        public double mptJun { get; set; }
        public double mptJul { get; set; }
        public double mptAug { get; set; }
        public double mptSep { get; set; }
        public double mptOct { get; set; }
        public double mptNov { get; set; }
        public double mptDec { get; set; }
        public double mptJan { get; set; }
        public double mptFeb { get; set; }
        public double mptMar { get; set; }
        public double mptTotal { get; set; }
        public double mptYtd { get; set; }
        public double mptActualRR { get; set; }
        public double mptBaltoAchieve { get; set; }
        public double mptRequiredRR { get; set; }
        public int MonthNo { get; set; }

    }
    public class ConsoleDto
    {
        public string CYfinancialYear { get; set; }
        public int month { get; set; }
        public int flag { get; set; } 
    }
}