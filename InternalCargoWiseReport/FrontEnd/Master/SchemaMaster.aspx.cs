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
    public partial class SchemaMaster : CustomBasePage
    {
        #region Properties
        SchemaMasterData _SchemaMasterData = null;
        private int Id = 0;
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }
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
                        _SchemaMasterData = new SchemaMasterData();
                        SchemaMasterDto result = _SchemaMasterData.GetById(Convert.ToInt32(RqId));
                        SetProperties(result);
                    }
                }
            }
        }
        private SchemaMasterDto MappingObject(SchemaMasterDto obj)
        {
            obj.SchemaMasterId = Id;
            obj.SchemaMasterName = Name;
            obj.SchemaMasterIsActive = true;
            return obj;
        }
        private void SetProperties(SchemaMasterDto obj)
        {
            Id = Convert.ToInt32(obj.SchemaMasterId);
            Name = obj.SchemaMasterName;
        }
        private void Clear()
        {
            Name = null;
        }
        private void BindGridView()
        {
            _SchemaMasterData = new SchemaMasterData();

            grdActiveProduct.DataSource = _SchemaMasterData.GetAll(true);
            grdActiveProduct.DataBind();
            grdDeactiveProduct.DataSource = _SchemaMasterData.GetAll(false);
            grdDeactiveProduct.DataBind();
        }
        #endregion

        #region Button Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _SchemaMasterData = new SchemaMasterData();
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
                    SchemaMasterDto request = MappingObject(new SchemaMasterDto());
                    if (Id == 0)
                    {
                        bool result = _SchemaMasterData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "SchemaMaster has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "SchemaMaster Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        bool result = _SchemaMasterData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "SchemaMaster has been Update Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "SchemaMaster Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter SchemaMaster Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "SchemaMaster Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("SchemaMasterId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _SchemaMasterData = new SchemaMasterData();
                        bool activeoff = _SchemaMasterData.IsActiveOnOff(MappingObjectActiveOff(new SchemaMasterDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "SchemaMaster has been delete successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("SchemaMasterId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("SchemaMaster.aspx?lovelyindexing=94&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
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
        private SchemaMasterDto MappingObjectActiveOff(SchemaMasterDto obj, int SchemaMasterId)
        {
            obj.SchemaMasterId = SchemaMasterId;
            obj.SchemaMasterIsActive = false;
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

                    Label txtID = (Label)gvRow.FindControl("SchemaMasterId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _SchemaMasterData = new SchemaMasterData();
                        bool activeoff = _SchemaMasterData.IsActiveOnOff(MappingObjectActiveOn(new SchemaMasterDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "SchemaMaster has been Restore successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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
        private SchemaMasterDto MappingObjectActiveOn(SchemaMasterDto obj, int SchemaMasterId)
        {
            obj.SchemaMasterId = SchemaMasterId;
            obj.SchemaMasterIsActive = true;
            return obj;
        }
        #endregion
    }
}