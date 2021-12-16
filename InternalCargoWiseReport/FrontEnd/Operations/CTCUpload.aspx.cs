using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CTCUpload : System.Web.UI.Page
    {
        CtcMemberData _ctcMember = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                if(list != null)
                {
                    drpYear.DataSource = list;
                    drpYear.DataValueField = "AssessmentYearID";
                    drpYear.DataTextField = "AssessmentYear";
                    drpYear.DataBind();
                    drpYear.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
            }
            catch(Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public void BindGrid()
        {
            try
            {
                _ctcMember = new CtcMemberData();
                IList<CtcMemberDto> list = _ctcMember.GetByYear(drpYear.SelectedValue);
                if (list != null)
                {
                    gvWhMTrans.DataSource = list;
                    gvWhMTrans.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public void SaveData()
        {
            try
            {
                foreach(GridViewRow gvRow in gvWhMTrans.Rows)
                {
                    Label gvID = (Label)gvRow.FindControl("gvlblID");
                    Label gvlblMemberID = (Label)gvRow.FindControl("gvlblMemberID");
                    TextBox gvtxtAmount = (TextBox)gvRow.FindControl("gvtxtAmount");
                    CtcMemberDto obj = new CtcMemberDto();
                    obj.CTCMemberAmount = gvtxtAmount.Text.ToDouble();
                    obj.CTCMemberId = gvlblMemberID.Text.ToLong();
                    obj.ID = gvID.Text.ToLong();
                    obj.CTCMemberPeriodId = drpYear.SelectedValue.ToString().ToLong();

                    _ctcMember = new CtcMemberData();

                    if (gvID.Text == string.Empty || gvID.Text=="0")
                    {
                        bool result = _ctcMember.Insert(obj);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                           // Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {

                        bool result = _ctcMember.Update(obj);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            //Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }


                }
            }
            catch(Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Something went wrong", "Oops!", Toastr.ToastPosition.TopCenter, true);

            }
        }
        #endregion

        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpYear.SelectedValue ==string.Empty)
            {
                return;
            }
            BindGrid();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}