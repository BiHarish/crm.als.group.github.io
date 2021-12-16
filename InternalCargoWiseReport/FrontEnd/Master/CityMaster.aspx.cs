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
    public partial class CityMaster : CustomBasePage
    {
        #region Properties
        CityData _cityData = null;
        StateData _stateData = null;
        private long Id = 0;
        private string StateId { get { return ddlState.SelectedValue; } set { ddlState.SelectedValue = value; } }
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }

        string RqId { get { return Request["requestId"]; } }
        #endregion

        #region Page Load
        private CityDto MappingObject(CityDto obj)
        {
            obj.CityId = Id;
            obj.CityName = Name;
            obj.CityStateId = string.IsNullOrEmpty(StateId) ? 1 : Convert.ToInt64(StateId);
            obj.CityIsActive = true;
            return obj;
        }
        private void SetProperties(CityDto obj)
        {
            Id = Convert.ToInt64(obj.CityId);
            Name = obj.CityName;
            if (obj.CityStateId != null && obj.CityStateId != 0)
                StateId = obj.CityStateId.ToString();
        }
        private void Clear()
        {
            Name = null;
        }
        private void BindGridView()
        {
            _cityData = new CityData();

            grdActiveProduct.DataSource = _cityData.GetAll(true);
            grdActiveProduct.DataBind();
            grdDeactiveProduct.DataSource = _cityData.GetAll(false);
            grdDeactiveProduct.DataBind();
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
                        _cityData = new CityData();
                        CityDto result = _cityData.GetById(Convert.ToInt64(RqId));
                        SetProperties(result);
                    }
                }
            }
        }
        private void BindDropDown()
        {
            _stateData = new StateData();
            ddlState.DataSource = _stateData.GetAll(true);
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _cityData = new CityData();
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
                    CityDto request = MappingObject(new CityDto());
                    if (Id == 0)
                    {
                        bool result = _cityData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "City has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "City Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                        bool result = _cityData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "City has been updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGridView();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "City Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Enter City Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "City Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("CityId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _cityData = new CityData();
                        bool activeoff = _cityData.IsActiveOnOff(MappingObjectActiveOff(new CityDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "City has been delete successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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

                    Label txtID = (Label)gvRow.FindControl("CityId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("CityMaster.aspx?lovelyindexing=5&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
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
        private CityDto MappingObjectActiveOff(CityDto obj, int StateId)
        {
            obj.CityId = StateId;
            obj.CityIsActive = false;
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

                    Label txtID = (Label)gvRow.FindControl("CityId");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _cityData = new CityData();
                        bool activeoff = _cityData.IsActiveOnOff(MappingObjectActiveOn(new CityDto(), txtID.Text.ToInt()));
                        if (activeoff)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "City has been Restore successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
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
        private CityDto MappingObjectActiveOn(CityDto obj, int CityId)
        {
            obj.CityId = CityId;
            obj.CityIsActive = true;
            return obj;
        }
        #endregion
    }
}