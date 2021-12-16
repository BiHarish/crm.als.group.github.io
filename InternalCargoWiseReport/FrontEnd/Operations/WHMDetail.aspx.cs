using ICWR.Data;
using ICWR.Data.Utility;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class WHMDetail : System.Web.UI.Page
    {

        LocationMasterData _LocationMasterData = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GEtAllLocation();
                BindGrid();
                BindSumGrid();
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
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        #region Bind Grid
        public void BindGrid()
        {
            try
            {
                WHMDetailData _whmdetaildata = new WHMDetailData();
                IList<WHMDetailDto> list = _whmdetaildata.GetBySearch(drpLocation.SelectedValue);
                if (list != null)
                {
                    gvWhMDetail.DataSource = list;
                    gvWhMDetail.DataBind();
                }
                else
                {
                    gvWhMDetail.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }

            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public void BindSumGrid()
        {
            try
            {
                WHMDetailData _whmdetaildata = new WHMDetailData();
                IList<WHMDetailDto> list = _whmdetaildata.GetAllSum();
                if (list != null)
                {
                  
                    gvTotal.DataSource = list;
                    gvTotal.DataBind();
                }
                else
                {
                    gvTotal.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion
        public void BindPopGrid()
        {
            try
            {
                string ABC=string.Empty;

                WHMTransData _WhMTransData = new WHMTransData();
                IList<WHMTransDto> result1 = _WhMTransData.GetBySearch(ABC,hfID.Value);
                if (result1 != null)
                {
                    DataTable dt = new DataTable();
                    dt = result1.ToList().ToDataTable<WHMTransDto>();
                    gvWhMTrans.DataSource = dt;
                   // GridViewGroup gvCustomerName = new GridViewGroup(gvWhMTrans, null, "CustomerName");
                   // GridViewGroup gvLocation = new GridViewGroup(gvWhMTrans, gvCustomerName, "LocationName");
                   // GridViewGroup gvTotalArea = new GridViewGroup(gvWhMTrans, gvLocation, "TotArea");
                    gvWhMTrans.DataBind();

                }

            }
            catch (Exception ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected void gvWhMDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWhMDetail.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void gvWhMTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWhMTrans.PageIndex = e.NewPageIndex;
            BindPopGrid();
        }
        protected void lnkButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            drpLocation.SelectedValue = string.Empty;
        }

        protected void gvWhMDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="View")
            {
                hfID.Value = e.CommandArgument.ToString();
                BindPopGrid();
                mp1.Show();
            }
        }

        protected void gvWhMDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvWhMTrans_DataBound(object sender, EventArgs e)
        {
            for (int i = gvWhMTrans.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvWhMTrans.Rows[i];
                GridViewRow previousRow = gvWhMTrans.Rows[i - 1];
                for (int j = 1; j < row.Cells.Count-1; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }
    }
}