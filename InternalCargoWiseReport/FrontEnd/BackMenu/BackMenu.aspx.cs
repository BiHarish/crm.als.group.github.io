using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.BackMenu
{
    public partial class BackMenu : System.Web.UI.Page
    {
        BackMenuData _backMenudata = new BackMenuData();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindParentMenu()
        {
            try
            {
                IList<BackMenuDto> parentmenulist = _backMenudata.GetAllParent();
                if (parentmenulist != null)
                {
                    drpParentMenu.DataSource = parentmenulist;
                    drpParentMenu.DataTextField = "BackMenuName";
                    drpParentMenu.DataValueField = "BackMenuId";
                    drpParentMenu.DataBind();
                    drpParentMenu.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
            }
            catch(Exception ex)
            {

            }
        }

   

        protected void drpMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpMenuType.SelectedValue==string.Empty)
            {
                dvParentMenu.Visible = false;
                txtMenuName.Visible = false;
                txtURL.Visible = false;
                Button1.Visible = false;
            }
            else if(drpMenuType.SelectedValue=="Parent")
            {
                dvParentMenu.Visible = false;
                txtMenuName.Visible = true;
                txtURL.Visible = true;
                Button1.Visible = true;
                txtURL.Text = "#";
                txtURL.Enabled = false;


            }
            else if (drpMenuType.SelectedValue == "Child")
            {
                BindParentMenu();
                dvParentMenu.Visible = true;
                txtMenuName.Visible = true;
                txtURL.Text = string.Empty;
                txtURL.Enabled = true;
                txtURL.Visible = true;
                Button1.Visible = true;
            }
        }

        public void Save()
        {
            try
            {
                BackMenuDto _backMenuDto = new BackMenuDto();
                if(drpMenuType.SelectedValue=="Parent")
                {
                    _backMenuDto.BackMenuName = txtMenuName.Text;
                    _backMenuDto.BackMenuParentId = null;
                    _backMenuDto.BackMenuURL = "#";
                }
                else
                {
                    _backMenuDto.BackMenuName = txtMenuName.Text;
                    _backMenuDto.BackMenuParentId = drpParentMenu.SelectedValue.ToDataConvertInt32();
                    _backMenuDto.BackMenuURL = txtURL.Text;
                }
                _backMenudata.Insert(_backMenuDto);
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Successfully Saved.", "Success!", Toastr.ToastPosition.TopCenter, true);

            }
            catch(Exception ex)
            {

            }
        }


        public bool Validation()
        {
            try
            {
                if(drpMenuType.SelectedValue==string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Menu Type", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (drpParentMenu.SelectedValue == string.Empty && drpMenuType.SelectedValue=="Child")
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Select Parent Menu", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtMenuName.Text==string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Enter Menu Name", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtURL.Text == string.Empty && drpMenuType.SelectedValue == "Child")
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Enter URL", "Opps!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }


            }
            catch (Exception ex) { }
            return true;
        }
        public void Clear()
        {
            dvParentMenu.Visible = false;
            txtMenuName.Visible = false;
            txtURL.Visible = false;
            Button1.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(!Validation())
            {
                return;
            }
            Save();
            Clear();
        }
    }
}