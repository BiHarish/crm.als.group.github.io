using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class MISDto
    {
        public int? maid { get; set; }
        public int? misdivisionid { get; set; }
        public string division { get; set; }
        public int? mistyid { get; set; }
        public string mistype { get; set; }
        public string mafinancialyear { get; set; }
        public int? mampmid { get; set; }
        public float maApr { get; set; }
        public float maMay { get; set; }
        public float maJun { get; set; }
        public float maJul { get; set; }
        public float maAug { get; set; }
        public float maSep { get; set; }
        public float maOct { get; set; }
        public float maNov { get; set; }
        public float maDec { get; set; }
        public float maJan { get; set; }
        public float maFeb { get; set; }
        public float maMar { get; set; }
        public float maTotal { get; set; }
        public float maYtd { get; set; }
        public float maActualRR { get; set; }
        public float maBaltoAchieve { get; set; }
        public float maRequiredRR { get; set; }
        public string SEISType { get; set; }
        public float maAprWS { get; set; }
        public float maMayWS { get; set; }
        public float maJunWS { get; set; }
        public float maJulWS { get; set; }
        public float maAugWS { get; set; }
        public float maSepWS { get; set; }
        public float maOctWS { get; set; }
        public float maNovWS { get; set; }
        public float maDecWS { get; set; }
        public float maJanWS { get; set; }
        public float maFebWS { get; set; }
        public float maMarWS { get; set; }
        public float maTotalWS { get; set; }
        public int? createby { get; set; }
        public DateTime? createon { get; set; }
        public int? modifyby { get; set; }
        public DateTime? modifyon { get; set; }
        public int? marid { get; set; }
        public float marMonth { get; set; }
        public string marMonthName { get; set; }
        public string marRemarks { get; set; }
        public float marvTotal { get; set; }
        public int? marRemarkYtd { get; set; }
        public string currentmonth { get; set; }
        public string monthwithoutseis { get; set; }

        public string uniqueID { get; set; }
    }
}