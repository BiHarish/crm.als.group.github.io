using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class MISVolumeDto
    {
        public int? mvtid { get; set; }
        public int? mvtvtyid { get; set; }
        public int? mvtmptid { get; set; }
        public string mvtperiod { get; set; }
        public int? mvtdivisionid { get; set; }
        public string mvtfinancialyear { get; set; }
        public double? mvtApr { get; set; }
        public double? mvtMay { get; set; }
        public double? mvtJun { get; set; }
        public double? mvtJul { get; set; }
        public double? mvtAug { get; set; }
        public double? mvtSep { get; set; }
        public double? mvtOct { get; set; }
        public double? mvtNov { get; set; }
        public double? mvtDec { get; set; }
        public double? mvtJan { get; set; }
        public double? mvtFeb { get; set; }
        public double? mvtMar { get; set; }
        public double? mvtTotal { get; set; }
        public double? mvtYTD { get; set; }
        public int? mvtcreateby { get; set; }
        public DateTime? mvtcreateon { get; set; }
        public int? mvtmodifyby { get; set; }
        public DateTime? mvtmodifyon { get; set; }
        public string MonthName { get; set; }
    }
}