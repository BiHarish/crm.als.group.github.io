using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CrmMeetingSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely != null)
                {
                    bindDrp();
                    if (RqId != null)
                    {
                        setData();
                        btnSubmit.Text = "Update";
                        btnCancel.Visible = false;
                        lblTotalKm.Visible = true;
                        lblRatePerKm.Visible = true;
                        txtTotalKm.Visible = true;
                        txtRatePerKm.Visible = true;
                        lblTotalAmt.Visible = true;
                        txtTotalAmt.Visible = true;
                        drpLead.Enabled = false;

                        if(LovelySession.Lovely.User.UserTypeId==1 || LovelySession.Lovely.User.UserTypeId==11)
                        {
                            btnSubmit.Visible = true;
                        }
                    }
                }
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }


        string RqId { get { return Request["requestId"]; } }
        #region

        void bindDrp()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            UserData _userData = new UserData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getAllLeadByUserID(LovelySession.Lovely.User.Id, LovelySession.Lovely.User.Location);
            IList<UserDto> users = _userData.getbyUserTypeID(LovelySession.Lovely.User.UserTypeId,LovelySession.Lovely.User.Id);
            if (results != null)
            {
                drpLead.DataSource = results;
                drpLead.DataValueField = "ID";
                drpLead.DataTextField = "CustomerName";
                drpLead.DataBind();
                drpLead.Items.Insert(0, new ListItem("--Select--", string.Empty));

                drpRelatedToName.DataSource = results;
                drpRelatedToName.DataValueField = "ID";
                drpRelatedToName.DataTextField = "CustomerName";
                drpRelatedToName.DataBind();
                drpRelatedToName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (users != null)
            {
                drpAssignedTo.DataSource = users;
                drpAssignedTo.DataValueField = "ID";
                drpAssignedTo.DataTextField = "Name";
                drpAssignedTo.DataBind();
                drpAssignedTo.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpAssignedTo.SelectedValue = LovelySession.Lovely.User.Id.ToString();
            }

        }

        private CrmMeetingScheduleDto MappingObject(CrmMeetingScheduleDto obj)
        {
            if (HfID.Value != string.Empty)
                obj.ID = HfID.Value.ToLong();
            obj.LeadID = drpLead.SelectedValue.ToNullLong();
            obj.Subject = txtSubject.Text;
            obj.StartDate = txtMettingDate.Text.ToConvertNullDateTime();
            obj.StartTime = txtStartTime.Text;
            obj.EndTime = txtEndTime.Text;
            obj.Duration = txtDuration.Text;
            obj.JointCaller = txtJointCaller.Text;
            obj.Priority = drpPriority.SelectedValue;
            obj.Description = txtDescription.Text;
            obj.Status = drpStatus.SelectedValue;
            obj.RelatedTo = drpRelatedTo.SelectedValue;
            obj.Location = txtLocation.Text;
            obj.Products = drpProduct.SelectedValue;
            obj.Visibility = drpVisibility.SelectedValue;
            obj.AssignedTo = drpAssignedTo.SelectedValue;
            obj.TotalKM = txtTotalKm.Text.ToNullDouble();
            obj.PerKm = txtRatePerKm.Text.ToNullDouble();
            obj.TotalAmt = txtTotalAmt.Text.ToNullDouble();
            if (RqId != null)
            {
                if (txtTotalKm.Text != string.Empty && txtRatePerKm.Text != string.Empty)
                {
                    txtTotalAmt.Text = string.Empty;
                    obj.TotalKM = txtTotalKm.Text.ToNullDouble();
                    obj.TotalAmt = (txtTotalKm.Text.ToDouble() * txtRatePerKm.Text.ToDouble());
                }
            }
            obj.ClaimType = drpClaimType.SelectedValue;
            obj.Remarks=txtRemarks.Text;

            obj.CreateBy = LovelySession.Lovely.User.Id.ToDataConvertInt64();
            if (drpRelatedToName.SelectedValue != string.Empty)
            {
                obj.RelatedToName = drpRelatedToName.SelectedItem.Text;

            }

            return obj;
        }

        void setData()
        {
            CrmMeetingScheduleData _meetingData = new CrmMeetingScheduleData();
            CrmMeetingScheduleDto result = _meetingData.GetById(RqId.ToLong());
            if (result != null)
            {
                HfID.Value = RqId.ToString();
                drpLead.SelectedValue = result.LeadID.ToString();
                txtSubject.Text = result.Subject;
                if (result.StartDate != null)
                    txtMettingDate.Text = result.StartDate.Value.ToString("dd MMM yyyy");
                txtStartTime.Text = result.StartTime;
                txtEndTime.Text = result.EndTime;
                txtDuration.Text = result.Duration;
                txtJointCaller.Text = result.JointCaller;
                drpPriority.SelectedValue = result.Priority;
                drpAssignedTo.SelectedValue = result.AssignedTo;
                drpStatus.SelectedValue = result.Status;
                drpRelatedTo.SelectedValue = result.RelatedTo;
                if(result.TotalAmt!=null)
                txtTotalAmt.Text = result.TotalAmt.Value.ToString("0.00");
                if (result.RelatedToName != string.Empty)
                {
                    drpRelatedToName.SelectedItem.Text = result.RelatedToName;

                }
                txtLocation.Text = result.Location;
                drpProduct.SelectedValue = result.Products;
                drpVisibility.SelectedValue = result.Visibility;
                txtDescription.Text = result.Description;
                if (result.TotalKM != null && result.TotalAmt != null)
                {
                    txtTotalKm.Text = result.TotalKM.ToString();
                    txtRatePerKm.Text = (result.TotalAmt / result.TotalKM).ToString();
                }
                else
                {
                    txtTotalKm.Text = string.Empty;
                    txtRatePerKm.Text = string.Empty;
                }
                if (result.IsPrint == false)
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }

                drpClaimType.SelectedValue = result.ClaimType;
                txtRemarks.Text = result.Remarks;
                 if(result.ClaimType=="Road")
            {
                
                divTotalAmt.Visible = true;
                divTotalKm.Visible = true;
                divRatePerKm.Visible = true;
                divRemarks.Visible = false;
            }
            else
            {
               
                divTotalAmt.Visible = true;
                divTotalKm.Visible = false;
                divRatePerKm.Visible = false;
                divRemarks.Visible = true;
                txtTotalAmt.Enabled = true;
            }
            }
        }

        bool validation()
        {
            if (drpLead.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Lead.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtSubject.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Subject.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtMettingDate.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter meeting Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtStartTime.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter start Time.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (txtEndTime.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter End Time.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            //if(txtDuration.Text==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Duration.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(txtJointCaller.Text==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Joint Caller.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(drpPriority.SelectedValue==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Priority.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            if (drpAssignedTo.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select AssignedTo.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpStatus.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Status.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            //if(drpRelatedTo.SelectedValue==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Related To.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(drpRelatedToName.SelectedValue==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Related To Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(drpProduct.SelectedValue==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Product Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(txtLocation.Text==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(drpVisibility.SelectedValue==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Visibility.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if(txtDescription.Text==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Description .", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}

            return true;
        }

        void clear()
        {
            HfID.Value = string.Empty;
            drpLead.SelectedValue = string.Empty;
            txtSubject.Text = string.Empty;
            txtMettingDate.Text = string.Empty;
            txtStartTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            txtDuration.Text = string.Empty;
            txtJointCaller.Text = string.Empty;
            drpPriority.SelectedValue = string.Empty;
            drpAssignedTo.SelectedValue = string.Empty;
            drpRelatedTo.SelectedValue = string.Empty;
            drpRelatedToName.SelectedValue = string.Empty;
            txtLocation.Text = string.Empty;
            drpProduct.SelectedValue = string.Empty;
            drpVisibility.SelectedValue = string.Empty;
            txtDescription.Text = string.Empty;
            drpClaimType.SelectedValue = string.Empty;
            txtTotalKm.Text = string.Empty;
            txtRatePerKm.Text = string.Empty;
            txtTotalAmt.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }


        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }
            CrmMeetingScheduleDto request = MappingObject(new CrmMeetingScheduleDto());
            CrmMeetingScheduleData _crmMeetingScheduleData = new CrmMeetingScheduleData();
            if (request.ID == 0)
            {
                if (_crmMeetingScheduleData.Insert(request))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Successfully Saved.", "Success!", Toastr.ToastPosition.TopCenter, true);
                    Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "ADD", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " successfully saved new record " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                    clear();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Internet problem", "Opps!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                if (_crmMeetingScheduleData.Update(request))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Successfully Updated.", "Success!", Toastr.ToastPosition.TopCenter, true);
                    Audit_Trail_Log.InsertAuditTrailLog("Meeting Schedule", "ADD", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " successfully Update existing record " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Internet problem", "Opps!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Operations/CrmMeetingScheduleList.aspx");
        }

        protected void txtEndTime_TextChanged(object sender, EventArgs e)
        {
            TimeSpan ts;
            if (txtStartTime.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Start Time.", "Opps!", Toastr.ToastPosition.TopCenter, true);
                txtEndTime.Text = string.Empty;
                txtDuration.Text = string.Empty;
                return;
            }
            else if (txtStartTime.Text != string.Empty)
            {
                if (!TimeSpan.TryParse(txtStartTime.Text, out ts))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter valid Start Time.", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    txtEndTime.Text = string.Empty;
                    txtDuration.Text = string.Empty;
                    return;
                }
            }
            if (txtEndTime.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter End Time.", "Opps!", Toastr.ToastPosition.TopCenter, true);
                txtEndTime.Text = string.Empty;
                txtDuration.Text = string.Empty;
                return;
            }
            else if (txtEndTime.Text != string.Empty)
            {
                if (!TimeSpan.TryParse(txtEndTime.Text, out ts))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter valid End Time.", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    txtEndTime.Text = string.Empty;
                    txtDuration.Text = string.Empty;
                    return;
                }
            }

            if ((TimeSpan.Parse(txtEndTime.Text) < TimeSpan.Parse(txtStartTime.Text)))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Start time should be less then to End Time.", "Opps!", Toastr.ToastPosition.TopCenter, true);
                txtEndTime.Text = string.Empty;
                txtDuration.Text = string.Empty;
                return;
            }

            TimeSpan t = (TimeSpan.Parse(txtEndTime.Text) - TimeSpan.Parse(txtStartTime.Text));
            txtDuration.Text = t.ToString();
        }

        protected void txtRatePerKm_TextChanged(object sender, EventArgs e)
        {
            txtTotalAmt.Text = (txtTotalKm.Text.ToDouble() * txtRatePerKm.Text.ToDouble()).ToString("0.00");
        }

        protected void drpClaimType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotalKm.Text = string.Empty;
            txtRatePerKm.Text = string.Empty;
            txtTotalAmt.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTotalAmt.Enabled = false;

            if(drpClaimType.SelectedValue==string.Empty)
            {
               
                divTotalAmt.Visible = false;
                divTotalKm.Visible = false;
                divRatePerKm.Visible = false;
                divRemarks.Visible = false;
                
            }
            else if(drpClaimType.SelectedValue=="Road")
            {
                
                divTotalAmt.Visible = true;
                divTotalKm.Visible = true;
                divRatePerKm.Visible = true;
                divRemarks.Visible = false;
            }
            else
            {
               
                divTotalAmt.Visible = true;
                divTotalKm.Visible = false;
                divRatePerKm.Visible = false;
                divRemarks.Visible = true;
                txtTotalAmt.Enabled = true;
            }
        }

    }
}