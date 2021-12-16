using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.ProfileCore
{
    public partial class EditProfileImage : CustomBasePage
    {
        #region Properties
        UserData _userData = null;
        private string CurrentImage { get { return imgCurrent.Src; } set { imgCurrent.Src = value; } }
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
                BindImage();
            }
        }
        private void BindImage()
        {
            CurrentImage = "/Files/Profile/" + LovelySession.Lovely.User.Profile;
        }
        #endregion

        #region Change Button
        protected void btnChange_Click(object sender, EventArgs e)
        {
            string rdobtn = null;
            _userData = new UserData();
            RadioButton SelectedRadioButton = (RadioButton)GetRadioButtonsByGroupName(this, "PopUp").FirstOrDefault(x => x.Checked);
            if (SelectedRadioButton != null && !string.IsNullOrEmpty(SelectedRadioButton.Text))
            {
                rdobtn = SelectedRadioButton.Text;
                bool success = _userData.UpdateImage(MappingObject(new UserDto(), rdobtn));
                if (success)
                {
                    BindImage();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Profile Image has been Changed successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Plaese check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select 1 Image to change", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public List<RadioButton> GetRadioButtonsByGroupName(Control parentControl, string groupName)
        {
            List<RadioButton> result = new List<RadioButton>();
            GetRadioButtonsByGroupName(parentControl, groupName, result);
            return result;
        }
        public void GetRadioButtonsByGroupName(Control parentControl, string groupName, List<RadioButton> list)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c is RadioButton && ((RadioButton)c).GroupName.Equals(groupName))
                {
                    list.Add(c as RadioButton);
                }
                else if (c.HasControls())
                {
                    GetRadioButtonsByGroupName(c, groupName, list);
                }
            }
        }
        private UserDto MappingObject(UserDto obj, string Img)
        {
            string Image = Img + ".png";
            obj.Profile = Image;
            obj.Id = Convert.ToInt64(LovelySession.Lovely.User.Id.Value);
            LovelySession.Lovely.User.Profile = Image;

            return obj;
        }
        #endregion

        #region Upload Button
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgNew.HasFile)
                {
                    _userData = new UserData();
                    string BackSide = UploadImage(Path.GetFileName(imgNew.PostedFile.FileName), "/Files/Profile/", imgNew);
                    bool UpdateImageSuccess = _userData.UpdateImage(MappingObjectUpload(new UserDto(), BackSide));
                    if (UpdateImageSuccess)
                    {
                        BindImage();
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Profile Image has been Changed successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Some Internet Problem Occured", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Select Any Image", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Some Internet Problem Occured", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected string UploadImage(string ImageName, string path, FileUpload uploader)
        {
            var exte = "." + ImageName.Split('.')[1];
            var image = LovelySession.Lovely.User.Code + exte;
            uploader.SaveAs(Server.MapPath(path + image));

            return image;
        }
        private UserDto MappingObjectUpload(UserDto obj, string Img)
        {
            obj.Profile = Img;
            obj.Id = Convert.ToInt64(LovelySession.Lovely.User.Id.Value);
            LovelySession.Lovely.User.Profile = Img;

            return obj;
        }
        #endregion
    }
}