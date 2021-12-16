using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.ProfileCore
{
    public partial class ChangePassword : CustomBasePage
    {
        #region Properties
        UserData _userData = null;
        private string Current { get { return txtCurrent.Text; } set { txtCurrent.Text = value; } }
        private string New { get { return txtNew.Text; } set { txtNew.Text = value; } }
        private string Confirm { get { return txtConfirm.Text; } set { txtConfirm.Text = value; } }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely != null)
            {
                if (!IsPostBack)
                {

                }
            }
            else
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
            }
        }
        private void Clear()
        {
            Current = null;
            New = null;
            Confirm = null;
        }
        #endregion

        #region Save Button
        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    _userData = new UserData();
                    bool success = _userData.ChangePassword(MappingObject(new UserDto()));
                    if (success)
                    {
                        Clear();
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Password Change successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Current Password", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private UserDto MappingObject(UserDto obj)
        {
            obj.Id = LovelySession.Lovely.User.Id.Value;
            obj.Password = Current;
            obj.Passport = New;

            return obj;
        }
        private bool Validation()
        {
            if (string.IsNullOrEmpty(Current))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Current Password", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(New))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter New Password", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(Confirm))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Confirm Password", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (New != Confirm)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "confirm password not matching with new passsword", "Warning!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}