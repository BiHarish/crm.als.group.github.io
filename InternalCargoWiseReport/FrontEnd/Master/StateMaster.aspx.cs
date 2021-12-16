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
    public partial class StateMaster : CustomBasePage
    {
        #region Properties
        CountryData _countryData = null;
        StateData _stateData = null;
        private long Id = 0;
        private string CountryId { get { return ddlCountry.SelectedValue; } set { ddlCountry.SelectedValue = value; } }
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }
        string RqId { get { return Request["requestId"]; } }
        #endregion

        #region Page Load
        private StateDto MappingObject(StateDto obj)
        {
            obj.StateId = Id;
            obj.StateName = Name;
            obj.StateCountryId = string.IsNullOrEmpty(CountryId) ? 1 : Convert.ToInt32(CountryId);
            obj.StateIsActive = true;
            return obj;
        }
        private void SetProperties(StateDto obj)
        {
            Id = Convert.ToInt64(obj.StateId);
            Name = obj.StateName;
            if (obj.StateCountryId != null && obj.StateCountryId != 0)
                CountryId = obj.StateCountryId.ToString();
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
                BindDropDown();
                BindGridView();
                if (RqId != null)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                    {
                        _stateData = new StateData();
                        StateDto result = _stateData.GetById(Convert.ToInt64(RqId));
                        SetProperties(result);
                    }
                }
            }
        }
        private void BindDropDown()
        {
            _countryData = new CountryData();
            ddlCountry.DataSource = _countryData.GetAll(true);
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
        }
        private void BindGridView()
        {
            _stateData = new StateData();

            grdActiveProduct.DataSource = _stateData.GetAll(true);
            grdActiveProduct.DataBind();
            grdDeactiveProduct.DataSource = _stateData.GetAll(false);
            grdDeactiveProduct.DataBind();
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _stateData = new StateData();
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    if (RqId != null)
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                        {
                            Id = Convert.ToInt64(RqId);
                        }
                    }
                    StateDto request = MappingObject(new StateDto());
                    if (Id == 0)
                    {
                        bool result = _stateData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "State has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "State Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        bool result = _stateData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "State has been updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "State Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter State Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "State Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("StateId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _stateData = new StateData();
                        bool activeoff = _stateData.IsActiveOnOff(MappingObjectActiveOff(new StateDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "State has been delete successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("StateId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("StateMaster.aspx?lovelyindexing=3&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
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
        private StateDto MappingObjectActiveOff(StateDto obj, int StateId)
        {
            obj.StateId = StateId;
            obj.StateIsActive = false;
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

                    Label txtID = (Label)gvRow.FindControl("StateId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _stateData = new StateData();
                        bool activeoff = _stateData.IsActiveOnOff(MappingObjectActiveOn(new StateDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "State has been Restore successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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
        private StateDto MappingObjectActiveOn(StateDto obj, int StateId)
        {
            obj.StateId = StateId;
            obj.StateIsActive = true;
            return obj;
        }
        #endregion
    }
}