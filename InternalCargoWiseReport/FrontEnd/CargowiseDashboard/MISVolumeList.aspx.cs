using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using System.Drawing;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MISVolumeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();
            }
        }
        void bindDrp()
        {
            MISMasterData _data = new MISMasterData();

            DataSet result = _data.getFinYear();

            if (result != null)
            {
                drpFinancialYear.DataSource = result.Tables[0];
                drpFinancialYear.DataValueField = "FinYear";
                drpFinancialYear.DataTextField = "FinYear";
                drpFinancialYear.DataBind();

            }
            DataSet divisionResult = _data.getDivision();
            if (divisionResult != null)
            {
                drpDivision.DataSource = divisionResult.Tables[0];
                drpDivision.DataValueField = "mdid";
                drpDivision.DataTextField = "mddesc";
                drpDivision.DataBind();
                drpDivision.Items.Insert(0, new ListItem("--Select--", string.Empty));

            }
        }
        void bindGrd()
        {
            MISVolumeData _data = new MISVolumeData();
            MISVolumeDto request = new MISVolumeDto();
            request.mvtfinancialyear = drpFinancialYear.SelectedValue;
            if (drpSubdivision.Items.Count > 0)
            {
                request.mvtdivisionid = drpSubdivision.SelectedValue.ToConvertNullInt();
            }
            else
            {
                request.mvtdivisionid = drpDivision.SelectedValue.ToConvertNullInt();
            }
            DataSet ds = _data.getVolumeList(request);
            if (ds != null)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                VolumeList.DataSource = ds.Tables[0];
                VolumeList.DataBind();
                this.VolumeList.Font.Name = "Calibri";
            }
            else
            {
                VolumeList.DataBind();
                txtRecordFound.Text = "0";
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }
        protected void lnkButton_Click(object sender, EventArgs e)
        {
            bindGrd();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MISVolume.aspx");
        }
        protected void drpDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpDivision.SelectedValue == string.Empty)
            {
                drpSubdivision.Items.Clear();
                drpSubdivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
                return;
            }
            MISMasterData _data = new MISMasterData();
            DataSet result = _data.getSubDivisionResults(drpDivision.SelectedValue);

            if (result != null)
            {
                drpSubdivision.DataSource = result.Tables[0];
                drpSubdivision.DataValueField = "ID";
                drpSubdivision.DataTextField = "NAME";
                drpSubdivision.DataBind();

                drpSubdivision.Visible = true;
                lblSubDivision.Visible = true;
                lblSubdivisionStar.Visible = true;

            }
            else
            {
                drpSubdivision.Items.Clear();
                drpSubdivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
            }
        }

        protected void VolumeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 5; i < NumCells; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");

                }
            }
        }
    }
}