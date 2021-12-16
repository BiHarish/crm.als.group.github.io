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
    public partial class MyProfile : CustomBasePage
    {
        #region Properties
        UserData _userData = null;
        StateData _stateData = null;
        CityData _cityData = null;

        private string Image { get { return lblImg.Src; } set { lblImg.Src = value; } }
        private string LabelName { get { return lblName.InnerText; } set { lblName.InnerText = value; } }
        private string LabelCode { get { return lblCode.InnerText; } set { lblCode.InnerText = value; } }
        private string LabelMobile { get { return lblMobile.InnerText; } set { lblMobile.InnerText = value; } }
        private string LabelEmail { get { return lblEmail.InnerText; } set { lblEmail.InnerText = value; } }
        private string LabelIntroducer { get { return lblIntroducer.InnerText; } set { lblIntroducer.InnerText = value; } }
        private string LabelAddress { get { return lblAddress.InnerText; } set { lblAddress.InnerText = value; } }
        private string LabelCity { get { return lblCity.InnerText; } set { lblCity.InnerText = value; } }
        private string Code { get { return txtCode.Value; } set { txtCode.Value = value; } }
        private string Id { get { return txtId.Value; } set { txtId.Value = value; } }
        private string Name { get { return txtName.Value; } set { txtName.Value = value; } }
        private string Mobile { get { return txtMobile.Value; } set { txtMobile.Value = value; } }
        private string Email { get { return txtEmail.Value; } set { txtEmail.Value = value; } }
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
                BindProperties();
            }
        }

        private void BindProperties()
        {
            _userData = new UserData();
            UserDto data = _userData.GetById(LovelySession.Lovely.User.Id.Value);
            if (data != null)
            {
                Image = "/Files/Profile/" + data.Profile;
                Id = data.Id.ToString();
                LabelAddress = data.Address;
                LabelCode = data.Code;
                LabelEmail = data.EmailId;
                LabelIntroducer = data.IntroducerCode;
                LabelMobile = data.MobileNo;
                LabelName = data.Name;
                LabelCity = data.CityName + " , " + data.StateName;
                Code = data.Code;
                Name = data.Name;
                Mobile = data.MobileNo;
                Email = data.EmailId;
            }
            else
            {
                Response.Redirect("/FrontEnd/Error/500Error.aspx", true);
            }
        }

        #endregion

        #region Save Personnel Inforamtion
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (InfoFieldValidation())
                {
                    _userData = new UserData();
                    bool success = _userData.Update(MappingObject(new UserDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Personnel Info has been update successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                        BindProperties();
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Plaese check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Plaese check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private bool InfoFieldValidation()
        {
            return true;
        }
        #endregion

        #region Save Nominee Inforamtion
        protected void btnNominee_Click(object sender, EventArgs e)
        {
            try
            {
                if (NomineeFieldValidation())
                {
                    _userData = new UserData();
                    bool success = _userData.Update(MappingObject(new UserDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Nominee Info has been update successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                        BindProperties();
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Plaese check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Plaese check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private bool NomineeFieldValidation()
        {
            return true;
        }
        #endregion

        #region MappingObject
        private UserDto MappingObject(UserDto obj)
        {
            obj.Id = Convert.ToInt64(Id);
            obj.Code = Code;
            obj.Name = Name;
            obj.MobileNo = Mobile;
            obj.EmailId = Email;
            obj.UserTypeId = LovelySession.Lovely.User.UserTypeId;
            return obj;
        }
        #endregion
    }
}