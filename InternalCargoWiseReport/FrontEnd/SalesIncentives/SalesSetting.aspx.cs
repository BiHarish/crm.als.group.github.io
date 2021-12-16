using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.SalesIncentives
{
    public partial class SalesSetting : System.Web.UI.Page
    {
        CtcMemberData _ctcMember = null;
        SalesSettingData _salesSettingData = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
                BindCompany();
            }
        }

        #region Method
        public void BindYear()
        {
            try
            {
                _ctcMember = new CtcMemberData();
                IList<CtcMemberDto> list = _ctcMember.GetAllYear();
                if (list != null)
                {
                    drpYear.DataSource = list;
                    drpYear.DataValueField = "AssessmentYearID";
                    drpYear.DataTextField = "AssessmentYear";
                    drpYear.DataBind();
                    drpYear.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public void BindCompany()
        {
            try
            {
                _salesSettingData = new SalesSettingData();
                IList<SalesSettingDto> list = _salesSettingData.GetAllCompany();
                if (list != null)
                {
                    drpCompany.DataSource = list;
                    drpCompany.DataValueField = "CompanyDivisionId";
                    drpCompany.DataTextField = "CompanyDivisionName";
                    drpCompany.DataBind();
                    drpCompany.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        
        public bool GetByYearAndCompanyAndQuarter()
        {
            try
            {
                _salesSettingData = new SalesSettingData();
                if (drpYear.SelectedValue == string.Empty || drpCompany.SelectedValue == string.Empty || drpQuater.SelectedValue==string.Empty)
                {
                    txteligibleonCTC.Enabled = false;
                    txtPercentOnCTC.Enabled = false;
                    txtPerOnOverAmount.Enabled = false;
                    txtPerOnNext.Enabled = false;
                    txtPerAfterSettlement.Enabled = false;
                    txtPerOnNextYear.Enabled = false;

                    txteligibleonCTC.Text = string.Empty;
                    txtPercentOnCTC.Text = string.Empty;
                    txtPerOnOverAmount.Text = string.Empty;
                    txtPerOnNext.Text = string.Empty;
                    txtPerAfterSettlement.Text = string.Empty;
                    txtPerOnNextYear.Text = string.Empty;
                    return false;


                }
                SalesSettingDto result = _salesSettingData.GetByYearAndDivisionIDandQuarter(drpYear.SelectedValue.ToLong(), drpCompany.SelectedValue.ToLong(), drpQuater.SelectedValue.ToLong());
                if (result != null)
                {
                    hfID.Value = result.SalesSettingId.ToString();
                    txteligibleonCTC.Text = result.SalesSettingEligibleOnCTC.ToString();
                    txtPercentOnCTC.Text = result.SalesSettingPercentOnCTC.ToString();
                    txtPerOnOverAmount.Text = result.SalesSettingPercentOnOverAmount.ToString();
                    txtPerOnNext.Text = result.SalesSettingPercentOnNext.ToString();
                    txtPerAfterSettlement.Text = result.SalesSettingPercentOnAfterSettle.ToString();
                    txtPerOnNextYear.Text = result.SalesSettingPercentOnNextYear.ToString();
                    
                    txteligibleonCTC.Enabled = true;
                    txtPercentOnCTC.Enabled = true;
                    txtPerOnOverAmount.Enabled = true;
                    txtPerOnNext.Enabled = true;
                    txtPerAfterSettlement.Enabled = true;
                    txtPerOnNextYear.Enabled = true;
                  
                }
                else
                {
                    hfID.Value = string.Empty;
                    txteligibleonCTC.Enabled = true;
                    txtPercentOnCTC.Enabled = true;
                    txtPerOnOverAmount.Enabled = true;
                    txtPerOnNext.Enabled = true;
                    txtPerAfterSettlement.Enabled = true;
                    txtPerOnNextYear.Enabled = true;
                    txteligibleonCTC.Text = string.Empty;
                    txtPercentOnCTC.Text = string.Empty;
                    txtPerOnOverAmount.Text = string.Empty;
                    txtPerOnNext.Text = string.Empty;
                    txtPerAfterSettlement.Text = string.Empty;
                    txtPerOnNextYear.Text = string.Empty;
                }


            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
            return true;
        }
        #endregion

        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetByYearAndCompanyAndQuarter();
        }
        protected void drpQuater_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetByYearAndCompanyAndQuarter();
        }
        protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetByYearAndCompanyAndQuarter();
        }

        public void Clear()
        {
            try
            {
                drpCompany.SelectedValue = string.Empty;
                drpYear.SelectedValue = string.Empty;
                txteligibleonCTC.Text = string.Empty;
                txtPercentOnCTC.Text = string.Empty;
                txtPerOnOverAmount.Text = string.Empty;
                txtPerOnNext.Text = string.Empty;
                txtPerAfterSettlement.Text = string.Empty;
                txtPerOnNextYear.Text = string.Empty;
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _salesSettingData = new SalesSettingData();
            SalesSettingDto obj = new SalesSettingDto();
            if (hfID.Value != string.Empty)
            {
                obj.SalesSettingId = hfID.Value.ToNullLong();
            }
            obj.SalesSettingPeriodId = drpYear.SelectedValue.ToNullLong();
            obj.SalesSettingCompanyDivisionId = drpCompany.SelectedValue.ToNullLong();
            obj.SalesSettingEligibleOnCTC = txteligibleonCTC.Text.ToNullDouble();
            obj.SalesSettingPercentOnCTC = txtPercentOnCTC.Text.ToNullDouble();
            obj.SalesSettingPercentOnOverAmount = txtPerOnOverAmount.Text.ToNullDouble();
            obj.SalesSettingPercentOnNext = txtPerOnNext.Text.ToNullDouble();
            obj.SalesSettingPercentOnAfterSettle = txtPerAfterSettlement.Text.ToNullDouble();
            obj.SalesSettingPercentOnNextYear = txtPerOnNextYear.Text.ToNullDouble();
            obj.SalesSettingQ = drpQuater.SelectedValue.ToNullLong();
            if (hfID.Value == string.Empty)
            {
                bool result = _salesSettingData.Insert(obj);
                if (result)
                {
                    Clear();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                bool result = _salesSettingData.Update(obj);
                if (result)
                {
                    Clear();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully Updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
    }
}