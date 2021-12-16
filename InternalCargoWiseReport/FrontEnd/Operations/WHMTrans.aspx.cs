using ICWR.Data;
using ICWR.Data.Utility;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class WHMTrans : System.Web.UI.Page
    {

        LocationMasterData _LocationMasterData = null;
        WHMTransData _WhMTransData = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GEtAllLocation();
                BindGrid();
            }
        }

        #region BindDrop
        public void GEtAllLocation()
        {
            try
            {
                _LocationMasterData = new LocationMasterData();
                IList<LocationMasterDto> result = _LocationMasterData.GetAll();
                if (result != null)
                {
                    drpLocation.DataSource = result;
                    drpLocation.DataValueField = "ID";
                    drpLocation.DataTextField = "LocationName";
                    drpLocation.DataBind();
                    drpLocation.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
                else
                {

                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
                _WhMTransData = new WHMTransData();
                IList<WHMTransDto> result1 = _WhMTransData.GetAllCustomers();
                if (result1 != null)
                {
                    drpCustomerName.DataSource = result1;
                    drpCustomerName.DataValueField = "CustomerName";
                    drpCustomerName.DataTextField = "CustomerName";
                    drpCustomerName.DataBind();
                    drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drpCustomerName.Items.Insert(result1.Count + 1, new ListItem("Others", "Others"));
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

        protected void drpCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCustomerName.SelectedValue == "Others")
            {
                txtname.Visible = true;
            }
            else
            {
                txtname.Visible = false;
            }
        }
        private WHMTransDto MappingObject(WHMTransDto obj)
        {
            if (hfID.Value != string.Empty)
            {
                obj.ID = hfID.Value.ToLong();
            }
            if (txtname.Text == string.Empty)
            {
                obj.CustomerName = drpCustomerName.SelectedValue;
            }
            else
            {
                obj.CustomerName = txtname.Text;
            }
            obj.WHMID = drpLocation.SelectedValue.ToNullLong();
            obj.TotArea = txtTotalArea.Text.ToNullDouble();
            obj.OccupiedArea = txtOccupied.Text.ToNullDouble();
            obj.Vacant = txtVacant.Text.ToNullDouble();
            return obj;
        }
        public void BindGrid()
        {
            try
            {
                _WhMTransData = new WHMTransData();
                IList<WHMTransDto> result1 = _WhMTransData.GetAll();
                if (result1 != null)
                {
                    gvWhMTrans.DataSource = result1;
                    gvWhMTrans.DataBind();

                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public void GetByID(string ID)
        {
            try
            {
                _WhMTransData = new WHMTransData();
                WHMTransDto result1 = _WhMTransData.GetById(ID.ToInt());
                if (result1 != null)
                {
                    drpCustomerName.SelectedValue = result1.CustomerName.ToString();
                    drpLocation.SelectedValue = result1.WHMID.ToString();
                    txtTotalArea.Text = result1.TotArea.ToString();
                    txtOccupied.Text = result1.OccupiedArea.ToString();
                    txtVacant.Text = result1.Vacant.ToString();
                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected void gvWhMDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWhMTrans.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        public void GetBySearch()
        {
            try
            {
                _WhMTransData = new WHMTransData();
                IList<WHMTransDto> result1 = _WhMTransData.GetBySearch(drpCustomerName.SelectedValue, drpLocation.SelectedValue);
                if (result1 != null)
                {
                    gvWhMTrans.DataSource = result1;
                    gvWhMTrans.DataBind();

                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        private void Clear()
        {
            drpCustomerName.SelectedValue = string.Empty;
            txtname.Text = string.Empty;
            txtname.Visible = false;
            drpLocation.SelectedValue = string.Empty;
            txtTotalArea.Text = string.Empty;
            txtOccupied.Text = string.Empty;
            txtVacant.Text = string.Empty;

        }

        protected void gvWhMTrans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string ID = e.CommandArgument.ToString();
                hfID.Value = ID;
                GetByID(ID);
            }
        }

        protected void gvWhMTrans_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _WhMTransData = new WHMTransData();
            try
            {
                if (drpCustomerName.SelectedValue != string.Empty || txtname.Text != string.Empty)
                {

                    WHMTransDto request = MappingObject(new WHMTransDto());
                    if (hfID.Value == string.Empty)
                    {
                        bool result = _WhMTransData.Insert(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Wharehouse has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Wharehouse Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else
                    {

                        bool result = _WhMTransData.Update(request);
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Wharehouse has been Updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                            BindGrid();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Wharehouse Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Wharehouse Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Location Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

      //  protected void gvWhMTrans_RowDataBound(object sender, GridViewRowEventArgs e)
      //  {
      //      if (e.Row.RowType == DataControlRowType.DataRow)
      //      {
      //          System.Data.DataRowView drv = e.Row.DataItem as System.Data.DataRowView;
      //
      //         // hfID.Value = drv["RequestNo"].ToString();
      //          GetByID(hfID.Value);
      //          e.Row.Attributes.Add("onclick", String.Format("window.location='../Operation/WHMTrans.aspx?Type=T&RequestNo={0}'", drv["RequestNo"]));
      //          e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
      //      }
      //  }


        // protected void lnkButton_Click(object sender, EventArgs e)
        // {
        //     GetBySearch();
        // }

        // protected void lnkRefresh_Click(object sender, EventArgs e)
        // {
        //     GEtAllLocation();
        //     BindGrid();
        //
        // }
    }
}