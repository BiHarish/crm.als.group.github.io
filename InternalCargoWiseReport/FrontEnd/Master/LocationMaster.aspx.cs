using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Master
{
    public partial class LocationMaster : CustomBasePage
    {
        #region Properties

        LocationMasterData _LocationMasterData = null;
        private int Id = 0;
        private string Name { get { return txtname.Text; } set { txtname.Text = value; } }
        private string Region { get { return drpRegion.SelectedValue; } set { drpRegion.SelectedValue = value; } }
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
                GEtAllLocation();
                
            }
        }


        private LocationMasterDto MappingObject(LocationMasterDto obj)
        {
            obj.Id = LiteralID.Text.ToNullLong();
            Id = LiteralID.Text.ToInt();
            obj.LocationName = Name;
            obj.Region = Region;
            return obj;
        }
        private void SetProperties(LocationMasterDto obj)
        {
            Id = Convert.ToInt32(obj.Id);
            Name = obj.LocationName;
            Region = obj.Region;
        }
        private void Clear()
        {
            Name = null;
            Region = null;
        }
        #endregion

        #region Button Save
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _LocationMasterData = new LocationMasterData();
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                   
                    LocationMasterDto request = MappingObject(new LocationMasterDto());
                    if (Id == 0)
                    {
                        bool result = _LocationMasterData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Location has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Location Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {
                       
                        bool result = _LocationMasterData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Location has been Updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Location Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Location Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Location Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        #endregion

        #region Method
        public void BindGrid()
        {
            try
            {
                _LocationMasterData = new LocationMasterData();
                IList<LocationMasterDto> result = _LocationMasterData.GetBySearch(drpLocationName.SelectedValue, drpSearchRegion.SelectedValue);
                if (result != null)
                {   
                     txtRecordFound.Text = result.Count.ToString();
                     gvLocationList.DataSource = result;
                     gvLocationList.DataBind();
                 }
                 else
                 {
                     txtRecordFound.Text = "0";
                     gvLocationList.DataBind();
                     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                 }
            }
            catch(Exception ex)
            {
                
            }
        }


        public void GEtAllLocation()
        {
            try
            {
                _LocationMasterData = new LocationMasterData();
                IList<LocationMasterDto> result = _LocationMasterData.GetAll();
                if (result != null)
                {
                    drpLocationName.DataSource = result;
                    drpLocationName.DataValueField = "ID";
                    drpLocationName.DataTextField = "LocationName";
                    drpLocationName.DataBind();
                    drpLocationName.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
                else
                {
                   
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRecordFound.Text = "0";
            gvLocationList.DataBind();
            drpLocationName.SelectedValue = string.Empty;
            drpSearchRegion.SelectedValue=string.Empty;
        }

        protected void gvLocationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvLocationList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("gvlblID");
                    int requestId = txtID.Text.ToInt();
                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        _LocationMasterData = new LocationMasterData();
                        LocationMasterDto result = _LocationMasterData.GetById(requestId);
                        if(result != null)
                        {
                            LiteralID.Text = result.Id.ToString();
                            SetProperties(result);
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }

        protected void gvLocationList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvLocationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocationList.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}