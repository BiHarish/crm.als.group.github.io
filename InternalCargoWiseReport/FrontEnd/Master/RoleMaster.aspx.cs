using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Master
{
    public partial class RoleMaster : CustomBasePage
    {
        #region Properties
        RoleData _roleData = null;
        private int Id = 0;
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }
        private string Amount { get { return txtAmount.Text; } set { txtAmount.Text = value; } }
        string RqId { get { return Request["requestId"]; } }
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
                BindGridView();
                if (RqId != null)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                    {
                        _roleData = new RoleData();
                        RoleDto result = _roleData.GetById(Convert.ToInt32(RqId));
                        SetProperties(result);
                    }
                }
            }
        }
        private RoleDto MappingObject(RoleDto obj)
        {
            obj.RoleId = Id;
            obj.RoleName = Name;
            obj.RoleIsActive = true;
            obj.RoleAmount = Amount;
            return obj;
        }
        private void SetProperties(RoleDto obj)
        {
            Id = Convert.ToInt32(obj.RoleId);
            Name = obj.RoleName;
            Amount = obj.RoleAmount;
        }
        private void Clear()
        {
            Name = null;
        }
        private void BindGridView()
        {
            _roleData = new RoleData();

            grdActiveProduct.DataSource = _roleData.GetAll(true);
            grdActiveProduct.DataBind();
            grdDeactiveProduct.DataSource = _roleData.GetAll(false);
            grdDeactiveProduct.DataBind();
        }
        #endregion

        #region Button Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _roleData = new RoleData();
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    if (RqId != null)
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                        {
                            Id = Convert.ToInt32(RqId);
                        }
                    }
                    RoleDto request = MappingObject(new RoleDto());
                    if (Id == 0)
                    {
                        bool result = _roleData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Role has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Role Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        bool result = _roleData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Role has been Update Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Role Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter Role Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Role Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        #endregion

        #region Active Grid Work
        protected void grdActiveProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = grdActiveProduct.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("RoleId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _roleData = new RoleData();
                        bool activeoff = _roleData.IsActiveOnOff(MappingObjectActiveOff(new RoleDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Role has been delete successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                            BindGridView();
                        }
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
            else if (e.CommandName == "Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = grdActiveProduct.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("RoleId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("RoleMaster.aspx?lovelyindexing=14&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
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
        private RoleDto MappingObjectActiveOff(RoleDto obj, int RoleId)
        {
            obj.RoleId = RoleId;
            obj.RoleIsActive = false;
            return obj;
        }
        protected void grdActiveProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdActiveProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void grdActiveProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdActiveProduct.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        #endregion

        #region Deactive Grid Work
        protected void grdDeactiveProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reactive")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = grdDeactiveProduct.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("RoleId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _roleData = new RoleData();
                        bool activeoff = _roleData.IsActiveOnOff(MappingObjectActiveOn(new RoleDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Role has been Restore successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                            BindGridView();
                        }
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
        protected void grdDeactiveProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDeactiveProduct.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        private RoleDto MappingObjectActiveOn(RoleDto obj, int RoleId)
        {
            obj.RoleId = RoleId;
            obj.RoleIsActive = true;
            return obj;
        }
        #endregion
    }
}