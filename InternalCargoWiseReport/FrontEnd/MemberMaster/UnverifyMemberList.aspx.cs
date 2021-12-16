using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.MemberMaster
{
    public partial class UnverifyMemberList : CustomBasePage
    {
        #region Properties
        UserData _userData = null;
        RoleData _roleData = null;
        private string Code { get { return txtCode.Text; } set { txtCode.Text = value; } }
        private string DateFrom { get { return txtDateFrom.Text; } set { txtDateFrom.Text = value; } }
        private string DateTo { get { return txtDateTo.Text; } set { txtDateTo.Text = value; } }
        private string UserTypeId { get { return ddlType.SelectedValue; } set { ddlType.SelectedValue = value; } }
        int gvPinId;
        bool gvChkAdd;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                BindDropdown();
            }
        }
        private void BindDropdown()
        {
            DateFrom = Utility.GetIndianDateTime().ToString("yyyy-MM-dd");
            DateTo = Utility.GetIndianDateTime().ToString("yyyy-MM-dd");
            btnVerify.Enabled = false;

            _roleData = new RoleData();
            ddlType.DataSource = _roleData.GetAll(true);
            ddlType.DataTextField = "RoleName";
            ddlType.DataValueField = "RoleId";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("Please Select User Type", ""));
        }
        #endregion

        #region Show Button
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    BindInformation();
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found For this credentials", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private void BindInformation()
        {
            _userData = new UserData();
            IList<UserDto> success = _userData.GetByVerifyCredentials(MappingObject(new UserDto()));
            if (success != null)
            {
                gvUserList.DataSource = success;
                gvUserList.DataBind();
                gvUserList.Visible = true;
                btnVerify.Enabled = true;
            }
            else
            {
                gvUserList.Visible = false;
                btnVerify.Enabled = false;
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found For this credentials", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private UserDto MappingObject(UserDto obj)
        {
            obj.UserTypeId = string.IsNullOrEmpty(UserTypeId) ? (int?)null : UserTypeId.ToInt();
            obj.Code = string.IsNullOrEmpty(Code) ? null : Code;
            obj.IsVerify = false;
            obj.IsActive = true;
            obj.JoiningDate = string.IsNullOrEmpty(DateFrom) ? Utility.GetIndianDateTime() : Convert.ToDateTime(DateFrom);
            obj.VerifyDate = string.IsNullOrEmpty(DateTo) ? Utility.GetIndianDateTime() : Convert.ToDateTime(DateTo);

            return obj;
        }
        private bool FieldValidation()
        {
            if (string.IsNullOrEmpty(DateFrom))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Date From", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(DateTo))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Date To", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (Convert.ToDateTime(DateFrom) > Convert.ToDateTime(DateTo))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Date From Less than Date To", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Work On Grid View Button 
        protected void gvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Restore")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvUserList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("Id");
                    int requestId = txtID.Text.ToInt();

                    _userData = new UserData();
                    bool result = _userData.IsVerifyOnOff(MappingObjectUnVerif(new UserDto(), requestId));
                    if (result)
                    {
                        gvUserList.Visible = false;
                        btnVerify.Enabled = false;
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "User Verify Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        private UserDto MappingObjectUnVerif(UserDto obj, int UserID)
        {
            obj.Id = UserID;
            obj.IsVerify = true;
            obj.VerifyBy = LovelySession.Lovely.User.Id.Value;
            obj.VerifyDate = Utility.GetIndianDateTime();

            return obj;
        }
        protected void gvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserList.PageIndex = e.NewPageIndex;
            BindInformation();
        }
        protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox ckAddHeader = (CheckBox)e.Row.FindControl("AdHeader");
                if (ckAddHeader != null)
                {
                    (ckAddHeader).Attributes.Add("onclick",
                    "javascript:SelectAdd('" + ((CheckBox)e.Row.FindControl("AdHeader")).ClientID + "')");
                }
            }
        }
        #endregion

        #region Verify Button
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            bool success = false;
            foreach (GridViewRow gvRow in gvUserList.Rows)
            {
                if (gvRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblFormName = ((Label)gvRow.FindControl("Id"));
                    gvPinId = lblFormName.Text.ToInt();

                    CheckBox chkAdd = ((CheckBox)gvRow.FindControl("AddHeader"));
                    gvChkAdd = chkAdd.Checked;
                    if (gvChkAdd)
                    {
                        _userData = new UserData();
                        success = _userData.IsVerifyOnOff(MappingObjectUnVerif(new UserDto(), gvPinId));
                    }
                }
            }
            if (success)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "User Verify Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                gvUserList.Visible = false;
                btnVerify.Enabled = false;
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        #endregion
    }
}