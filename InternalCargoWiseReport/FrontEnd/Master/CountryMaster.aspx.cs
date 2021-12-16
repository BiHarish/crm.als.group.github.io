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
    public partial class CountryMaster : CustomBasePage
    {
        #region Properties
        CountryData _countryData = null;
        private int Id = 0;
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }
        string RqId { get { return Request["requestId"]; } }
        #endregion

        #region Page Load
        private CountryDto MappingObject(CountryDto obj)
        {
            obj.CountryId = Id;
            obj.CountryName = Name;
            obj.CountryIsActive = true;
            return obj;
        }
        private void SetProperties(CountryDto obj)
        {
            Id = Convert.ToInt32(obj.CountryId);
            Name = obj.CountryName;
        }
        private void Clear()
        {
            Name = null;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                if (!CustomPage.isPermission(this.Page, CustomPage.TypePermission.Add))
                {
                    divAdd.Style.Add("display", "none");
                }
                if (!CustomPage.isPermission(this.Page, CustomPage.TypePermission.View))
                {
                   divViewActive.Visible = false;
                   divViewDeactive.Visible = false;
                }
                else
                {
                    BindGridView();
                }

                if (RqId != null)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                    {
                        _countryData = new CountryData();
                        CountryDto result = _countryData.GetById(Convert.ToInt32(RqId));
                        SetProperties(result);
                    }
                }
            }
        }
        private void BindGridView()
        {
            _countryData = new CountryData();

            grdActiveProduct.DataSource = _countryData.GetAll(true);
            grdActiveProduct.DataBind();
            grdDeactiveProduct.DataSource = _countryData.GetAll(false);
            grdDeactiveProduct.DataBind();

            if (!CustomPage.isPermission(this.Page, CustomPage.TypePermission.Update))
            {
                grdActiveProduct.Columns[4].Visible = false;
                grdDeactiveProduct.Columns[3].Visible = false;
            }
            if (!CustomPage.isPermission(this.Page, CustomPage.TypePermission.Delete))
            {
                grdActiveProduct.Columns[3].Visible = false;
            }
        }
        #endregion

        #region Button Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CustomPage.isPermission(this.Page, CustomPage.TypePermission.Add))
            {
                return;
            }

            _countryData = new CountryData();
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
                    CountryDto request = MappingObject(new CountryDto());
                    if (Id == 0)
                    {
                        bool result = _countryData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Country has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Country Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        bool result = _countryData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Country has been Updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Country Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter Country Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Country Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("CountryId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _countryData = new CountryData();
                        bool activeoff = _countryData.IsActiveOnOff(MappingObjectActiveOff(new CountryDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Country has been delete successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("CountryId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("CountryMaster.aspx?lovelyindexing=3&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
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
        private CountryDto MappingObjectActiveOff(CountryDto obj, int CountryId)
        {
            obj.CountryId = CountryId;
            obj.CountryIsActive = false;
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

                    Label txtID = (Label)gvRow.FindControl("CountryId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _countryData = new CountryData();
                        bool activeoff = _countryData.IsActiveOnOff(MappingObjectActiveOn(new CountryDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Country has been Restore successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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
        private CountryDto MappingObjectActiveOn(CountryDto obj, int CountryId)
        {
            obj.CountryId = CountryId;
            obj.CountryIsActive = true;
            return obj;
        }
        #endregion

    }
}