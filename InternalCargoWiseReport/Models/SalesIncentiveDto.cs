using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class AccManageDto
    {
        public int IAM_Id { get; set; }
        public int IAM_CWPeriod { get; set; }
        public int IAM_Year { get; set; }
        public string IAM_SelfPeriod { get; set; }
        public DateTime IAM_StartDate { get; set; }
        public DateTime IAM_EndDate { get; set; }
    }

    public class CompanyDivisionDto
    {
        public int CompanyDivisionId { get; set; }
        public string CompanyDivisionName { get; set; }
        public bool CompanyDivisionIsActive { get; set; }
    }

    public class CtcMemberDto
    {
        public long? ID { get; set; }
        public long? CTCMemberId { get; set; }
        public long? CTCMemberPeriodId { get; set; }
        public double? CTCMemberAmount { get; set; }


        //Extra Field For Year DropDown
        public long AssessmentYearID { get; set; }
        public string AssessmentYear { get; set; }
        public string CTCMemberName { get; set; }
    }


    public class GeneratePayDto
    {
        public string AssmentYear { get; set; }
        public int Q { get; set; }
        public DataTable grossDt { get;set;}
        public DataTable interestDt { get; set; }
        public int Close { get; set; }
        public bool isFinal { get;set;}
    }

    public class QuaterDateDto
    {
        public DateTime firstdate { get; set; }
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
    }

    public class SalesSettingDto
    {
        public long? SalesSettingId { get; set; }
        public long? SalesSettingPeriodId { get; set; }
        public long? SalesSettingCompanyDivisionId { get; set; }
        public double? SalesSettingEligibleOnCTC { get; set; }
        public double? SalesSettingPercentOnCTC { get; set; }
        public double? SalesSettingPercentOnOverAmount { get; set; }
        public double? SalesSettingPercentOnNext { get; set; }
        public double? SalesSettingPercentOnAfterSettle { get; set; }
        public double? SalesSettingPercentOnNextYear { get; set; }

        public long? SalesSettingQ  { get; set; }

        // Extra fields For Bind Company
        public long CompanyDivisionId { get; set; }
        public string CompanyDivisionName { get; set; }
    }
    public class UpdateYearIntrestDto
    {
        public  long? IAM_Id { get; set; }
        public  string IAM_CWPeriod { get; set; }
        public  string IAM_SelfPeriod { get; set; }
        public  DateTime? IAM_StartDate { get; set; }
        public DateTime? IAM_EndDate { get; set; }
        public  double? IAM_PercentOfInterest { get; set; }
    }
}