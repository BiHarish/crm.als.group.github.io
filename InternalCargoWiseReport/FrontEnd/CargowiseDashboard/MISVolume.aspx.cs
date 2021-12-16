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
    public partial class MISVolume : System.Web.UI.Page
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

                request.mvtdivisionid = drpSubdivision.SelectedValue.ToDataConvertInt32();
            }
            else
            {
                request.mvtdivisionid = drpDivision.SelectedValue.ToDataConvertInt32();
            }
            DataSet ds = _data.getDataFromVolumeTable(request);
            if (ds != null)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                GVVolume.DataSource = ds.Tables[0];
                GVVolume.DataBind();
            }
            else
            {
                //DataSet budgetMasterData = _data.getDataFromVolumeTable(request);
                //if (budgetMasterData != null)
                //{
                //    txtRecordFound.Text = budgetMasterData.Tables[0].Rows.Count.ToString();
                //    GVVolume.DataSource = budgetMasterData.Tables[0];
                //    GVVolume.DataBind();
                //}
                //else
                //{
                //    GVVolume.DataBind();
                //    txtRecordFound.Text = "0";
                //}
                GVVolume.DataBind();
                txtRecordFound.Text = "0";
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record Found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void saveVolumeData()
        {
            MisVolumeActualAndBudgetData _data = new MisVolumeActualAndBudgetData();
            foreach (GridViewRow gvRow in GVVolume.Rows)
            {
                //Label gvlblmvtid = (Label)gvRow.FindControl("gvlblmvtid");
                Label gvlblvtyid = (Label)gvRow.FindControl("gvlblvtyid");
                Label gvlblvtydivisionid = (Label)gvRow.FindControl("gvlblvtydivisionid");
                Label gvlblmvtptid = (Label)gvRow.FindControl("gvlblmvtptid");
                Label gvlblmvtptvtyid = (Label)gvRow.FindControl("gvlblmvtptvtyid");
                Label gvlblmvtptmptid = (Label)gvRow.FindControl("gvlblmvtptmptid");
                Label gvlblmptid = (Label)gvRow.FindControl("gvlblmptid");
                Label gvlblmvbid = (Label)gvRow.FindControl("gvlblmvbid");
                Label gvlblmvbdivisionid = (Label)gvRow.FindControl("gvlblmvbdivisionid");
                Label gvlblmvbvtyid = (Label)gvRow.FindControl("gvlblmvbvtyid");
                Label gvlblmvbmptid = (Label)gvRow.FindControl("gvlblmvbmptid");
                Label gvlblmvbmpmid = (Label)gvRow.FindControl("gvlblmvbmpmid");
                Label gvlblvtydesc = (Label)gvRow.FindControl("gvlblvtydesc");//
                Label gvlblvtycode = (Label)gvRow.FindControl("gvlblvtycode");
                TextBox gvtxtFinYear = (TextBox)gvRow.FindControl("gvtxtmptfinancialyear");

                TextBox gvtxtmptApr = (TextBox)gvRow.FindControl("gvtxtmptApr");
                TextBox gvtxtmptMay = (TextBox)gvRow.FindControl("gvtxtmptMay");
                TextBox gvtxtmptJun = (TextBox)gvRow.FindControl("gvtxtmptJun");
                TextBox gvtxtJul = (TextBox)gvRow.FindControl("gvtxtJul");
                TextBox gvtxtAug = (TextBox)gvRow.FindControl("gvtxtAug");
                TextBox gvtxtmptSep = (TextBox)gvRow.FindControl("gvtxtmptSep");
                TextBox gvtxtmptOct = (TextBox)gvRow.FindControl("gvtxtmptOct");
                TextBox gvtxtNov = (TextBox)gvRow.FindControl("gvtxtNov");
                TextBox gvtxtmptDec = (TextBox)gvRow.FindControl("gvtxtmptDec");
                TextBox gvtxtmptJan = (TextBox)gvRow.FindControl("gvtxtmptJan");
                TextBox gvtxtFeb = (TextBox)gvRow.FindControl("gvtxtFeb");
                TextBox gvtxtMar = (TextBox)gvRow.FindControl("gvtxtMar");

                MisVolumeActualAndBudgetDto request = new MisVolumeActualAndBudgetDto();
                //request.mvtid = gvlblmvtid.Text.ToConvertNullInt();
                if (drpSubdivision.Items.Count > 0)
                {
                    request.mvadivisionid = drpSubdivision.SelectedValue.ToConvertNullInt();
                }
                else
                {
                    request.mvadivisionid = drpDivision.SelectedValue.ToConvertNullInt();
                }

                request.mvaid = gvlblvtyid.Text.ToLong();
                request.TypeDesc = gvlblvtydesc.Text;
                request.mvamptid = gvlblmptid.Text.ToNullLong();
                request.mvavtyid = gvlblmvtptvtyid.Text.ToNullLong();
                request.mvafinancialyear = gvtxtFinYear.Text;
                request.mvaApr = gvtxtmptApr.Text.ToNullDouble();
                request.mvaMay = gvtxtmptMay.Text.ToNullDouble();
                request.mvaJun = gvtxtmptJun.Text.ToNullDouble();
                request.mvaJul = gvtxtJul.Text.ToNullDouble();
                request.mvaAug = gvtxtAug.Text.ToNullDouble();
                request.mvaSep = gvtxtmptSep.Text.ToNullDouble();
                request.mvaOct = gvtxtmptOct.Text.ToNullDouble();
                request.mvaNov = gvtxtNov.Text.ToNullDouble();
                request.mvaDec = gvtxtmptDec.Text.ToNullDouble();
                request.mvaJan = gvtxtmptJan.Text.ToNullDouble();
                request.mvaFeb = gvtxtFeb.Text.ToNullDouble();
                request.mvaMar = gvtxtMar.Text.ToNullDouble();
                //request.mvtTotal = gvtxtmptTotal.Text.ToNullDouble();
                //request.mvtYTD = gvtxtmptYtd.Text.ToNullDouble();
                request.mvacreateby = LovelySession.Lovely.User.Id;
                request.PeriodTypeID = gvlblmptid.Text.ToNullLong();


                // if (gvlblvtyid.Text.ToLong() == 0)
                {
                    long returnID = _data.Insert(request);
                    if (returnID > 0)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Server Error", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                //else
                //{
                //    if (_data.Update(request))
                //    {
                //        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully Updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                //    }
                //    else
                //    {
                //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Server Error", "Error!", Toastr.ToastPosition.TopCenter, true);
                //    }
                //}



            }
        }
        bool validation()
        {
            if (drpDivision.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpDivision.SelectedValue != string.Empty)
            {
                if (drpSubdivision.Items.Count > 0)
                {
                    if (drpSubdivision.SelectedValue == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select sub Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                }
            }
            return true;
        }
        protected void lnkButton_Click(object sender, EventArgs e)
        {
            bindGrd();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!validation())
            {
                return;
            }
            saveVolumeData();
            bindGrd();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/FrontEnd/CargowiseDashboard/MISVolume.aspx");
        }
        protected void drpDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVVolume.DataBind();
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
        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MISVolumeList.aspx");
        }

        protected void GVVolume_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 5; i < NumCells; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");

                    //e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("BORDER", "1px");
                    //e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("background-color", "white");


                    //e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("BORDER", "1px");
                    //e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("background-color", "white");
                }
            }
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                Label gvlblvtydesc = (Label)e.Row.FindControl("gvlblvtydesc");
                if(gvlblvtydesc.Text.ToLower()=="total")
                {
                    e.Row.Enabled = false;
                }
            }
        }

        protected void GVVolume_DataBound(object sender, EventArgs e)
        {
            for (int i = GVVolume.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = GVVolume.Rows[i];
                GridViewRow previousRow = GVVolume.Rows[i - 1];
                for (int j = 3; j <= 3; j++)
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