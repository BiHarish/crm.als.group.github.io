using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.SalesIncentives
{
    public partial class UpdateYearIntrest : System.Web.UI.Page
    {
        CtcMemberData _ctcMember = null;
        UpdateYearIntrestData _UpdateYearIntrestData = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();

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

        public void BindGrid()
        {
            try
            {
                _UpdateYearIntrestData = new UpdateYearIntrestData();
                IList<UpdateYearIntrestDto> list = _UpdateYearIntrestData.GetByYear(drpYear.SelectedItem.Text);
                if (list != null)
                {
                    gvUpdateYearMonthList.DataSource = list;
                    gvUpdateYearMonthList.DataBind();
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    gvUpdateYearMonthList.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch(Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);

            }
        }

       

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData();
            BindGrid();
        }



        #endregion


        public void SaveData()
        {
            try
            {
                foreach (GridViewRow gvRow in gvUpdateYearMonthList.Rows)
                {
                    Label gvID = (Label)gvRow.FindControl("gvIAM_Id");
                    TextBox gvtxtAmount = (TextBox)gvRow.FindControl("gvtxtIAM_PercentOfInterest");
                    UpdateYearIntrestDto obj = new UpdateYearIntrestDto();
                    obj.IAM_Id = gvID.Text.ToLong();
                    obj.IAM_PercentOfInterest = gvtxtAmount.Text.ToNullDouble();

                    _UpdateYearIntrestData = new UpdateYearIntrestData();


                    if (gvID.Text != string.Empty || gvID.Text != "0")
                    {
                        bool result = _UpdateYearIntrestData.Update(obj);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                           
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {

                    }


                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Something went wrong", "Oops!", Toastr.ToastPosition.TopCenter, true);

            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{

        //    _salesSettingData = new SalesSettingData();
        //    SalesSettingDto obj = new SalesSettingDto();
        //    if (hfID.Value != string.Empty)
        //    {
        //        obj.SalesSettingId = hfID.Value.ToNullLong();
        //    }
        //    obj.SalesSettingPeriodId = drpYear.SelectedValue.ToNullLong();
        //    obj.SalesSettingCompanyDivisionId = drpCompany.SelectedValue.ToNullLong();
        //    obj.SalesSettingEligibleOnCTC = txteligibleonCTC.Text.ToNullDouble();
        //    obj.SalesSettingPercentOnCTC = txtPercentOnCTC.Text.ToNullDouble();
        //    obj.SalesSettingPercentOnOverAmount = txtPerOnOverAmount.Text.ToNullDouble();
        //    obj.SalesSettingPercentOnNext = txtPerOnNext.Text.ToNullDouble();
        //    obj.SalesSettingPercentOnAfterSettle = txtPerAfterSettlement.Text.ToNullDouble();
        //    obj.SalesSettingPercentOnNextYear = txtPerOnNextYear.Text.ToNullDouble();

        //    if (hfID.Value == string.Empty)
        //    {
        //        bool result = _salesSettingData.Insert(obj);
        //        if (result)
        //        {
        //            Clear();
        //            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
        //        }
        //        else
        //        {
        //            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
        //        }
        //    }
        //    else
        //    {
        //        bool result = _salesSettingData.Update(obj);
        //        if (result)
        //        {
        //            Clear();
        //            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully Updated", "Success!", Toastr.ToastPosition.TopCenter, true);
        //        }
        //        else
        //        {
        //            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
        //        }
        //    }
        //}
    }
}