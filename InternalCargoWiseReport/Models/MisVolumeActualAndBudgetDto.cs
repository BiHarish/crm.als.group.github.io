using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class MisVolumeActualAndBudgetDto
    {
        public long? mvaid { get; set; }
        public long? mvadivisionid { get; set; }
        public long? mvavtyid { get; set; }
        public long? mvamptid { get; set; }//
        public string mvafinancialyear { get; set; }
        public long? mvampmid { get; set; }
        public double? mvaApr { get; set; }
        public double? mvaMay { get; set; }
        public double? mvaJun { get; set; }
        public double? mvaJul { get; set; }
        public double? mvaAug { get; set; }
        public double? mvaSep { get; set; }
        public double? mvaOct { get; set; }
        public double? mvaNov { get; set; }
        public double? mvaDec { get; set; }
        public double? mvaJan { get; set; }
        public double? mvaFeb { get; set; }
        public double? mvaMar { get; set; }
        public double? mvaTotal { get; set; }
        public double? mvaYtd { get; set; }
        public double? mvaActualRR { get; set; }
        public double? mvaBaltoAchieve { get; set; }
        public double? mvaRequiredRR { get; set; }
        public long? mvacreateby { get; set; }
        public long? PeriodTypeID { get; set; }
        public string TypeDesc { get; set; }
    }
}