using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class SEISClaimDto
    {
        public int? srid { get; set; }
        public int? srdivisionid { get; set; }
        public string srfinancialyear { get; set; }
        public int? srmpmid { get; set; }
        public float srApr { get; set; }
        public float srMay { get; set; }
        public float srJun { get; set; }
        public float srJul { get; set; }
        public float srAug { get; set; }
        public float srSep { get; set; }
        public float srOct { get; set; }
        public float srNov { get; set; }
        public float srDec { get; set; }
        public float srJan { get; set; }
        public float srFeb { get; set; }
        public float srMar { get; set; }
        public float srTotal { get; set; }
        public int? srcreateby { get; set; }
        public DateTime? srcreateon { get; set; }
        public int? srmodifyby { get; set; }
        public DateTime? srmodifyon { get; set; }
        public int? msrrid { get; set; }
        public string msrrmtyid { get; set; }

        public string currentmonth { get; set; }
        public int msrrMonth { get; set; }
        public string msrrMonthName { get; set; }
        public string msrrRemarks { get; set; }
        public float msrrvTotal { get; set; }
        public float msrrRemarkYtd { get; set; }

    }
}