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
    public partial class MISRunRate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();

            }
        }

        #region Method
        void bindDrp()
        {
            MISMasterData _data = new MISMasterData();

            DataSet result = _data.getFinYearForRunRate();

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
        void GetGridData()
        {
            MISRunRateData _data = new MISRunRateData();
            MISRunRateDto _dto = new MISRunRateDto();

            _dto.CYfinancialYear = drpFinancialYear.SelectedValue;
            _dto.month = Drpmonth.SelectedValue.ToConvertNullInt();
            DataSet ds = _data.GetGridData(_dto);
            if (ds != null)
            {
                lblHeading.Visible = true;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GVRunRatewithSEIS.DataSource = ds.Tables[1];
                    GVRunRatewithSEIS.DataBind();
                    GVRunRatewithSEIS.Rows[GVRunRatewithSEIS.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    GVRunRatewithSEIS.DataBind();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    gvRevenuewithSEIS.DataSource = ds.Tables[3];
                    gvRevenuewithSEIS.DataBind();
                    gvRevenuewithSEIS.Rows[gvRevenuewithSEIS.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    gvRevenuewithSEIS.DataBind();
                }


                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVRunRate.DataSource = ds.Tables[0];
                    GVRunRate.DataBind();
                    GVRunRate.Rows[GVRunRate.Rows.Count - 1].Font.Bold = true;//Your Style


                }
                else
                {
                    GVRunRate.DataBind();
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvRevenue.DataSource = ds.Tables[2];
                    gvRevenue.DataBind();
                    gvRevenue.Rows[gvRevenue.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    gvRevenue.DataBind();
                }
            }
            else
            {
                GVRunRate.DataBind();
                lblHeading.Visible = false;
            }

        }
        public void ChangeColor()
        {
            foreach (GridViewRow row in GVRunRate.Rows)
            {

                Label lblLMActualWithoutSEISClaim = (row.FindControl("lblLMActualWithoutSEISClaim") as Label);
                if (lblLMActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLMActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblLMActualWithoutSEISClaim.Text = "(" + lblLMActualWithoutSEISClaim.Text + ")";
                }
                Label lblCMBudget = (row.FindControl("lblCMBudget") as Label);
                if (lblCMBudget.Text.ToNullFloat() < 0)
                {
                    lblCMBudget.ForeColor = Color.Red;
                    //lblCMBudget.Text = "(" + lblCMBudget.Text + ")";
                }
                Label lblCMActualWithoutSEISClaim = (row.FindControl("lblCMActualWithoutSEISClaim") as Label);
                if (lblCMActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCMActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblCMActualWithoutSEISClaim.Text = "(" + lblCMActualWithoutSEISClaim.Text + ")";
                }
                Label lblLYActualWithoutSEISClaim = (row.FindControl("lblLYActualWithoutSEISClaim") as Label);
                if (lblLYActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLYActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblLYActualWithoutSEISClaim.Text = "(" + lblLYActualWithoutSEISClaim.Text + ")";
                }

                Label lblCYBudget = (row.FindControl("lblCYBudget") as Label);
                if (lblCYBudget.Text.ToNullFloat() < 0)
                {
                    lblCYBudget.ForeColor = Color.Red;
                    //lblCYBudget.Text = "(" + lblCYBudget.Text + ")";
                }

                Label lblCYActualWithoutSEISClaim = (row.FindControl("lblCYActualWithoutSEISClaim") as Label);
                if (lblCYActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCYActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblCYActualWithoutSEISClaim.Text = "(" + lblCYActualWithoutSEISClaim.Text + ")";
                }

                Label lblCurrentFullYearBudget = (row.FindControl("lblCurrentFullYearBudget") as Label);
                if (lblCurrentFullYearBudget.Text.ToNullFloat() < 0)
                {
                    lblCurrentFullYearBudget.ForeColor = Color.Red;
                    //    lblCurrentFullYearBudget.Text = "(" + lblCurrentFullYearBudget.Text + ")";
                }

                Label lblBalToAchieveWithoutSEIS = (row.FindControl("lblBalToAchieveWithoutSEIS") as Label);
                if (lblBalToAchieveWithoutSEIS.Text.ToNullFloat() < 0)
                {
                    lblBalToAchieveWithoutSEIS.ForeColor = Color.Red;
                    //lblBalToAchieveWithoutSEIS.Text = "(" + lblBalToAchieveWithoutSEIS.Text + ")";
                }

                Label lblActualRunRateWithoutSEIS = (row.FindControl("lblActualRunRateWithoutSEIS") as Label);
                if (lblActualRunRateWithoutSEIS.Text.ToNullFloat() < 0)
                {
                    lblActualRunRateWithoutSEIS.ForeColor = Color.Red;
                    //lblActualRunRateWithoutSEIS.Text = "(" + lblActualRunRateWithoutSEIS.Text + ")";
                }

                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    Label lblRequiredRunRateWithoutSEIS = (row.FindControl("lblRequiredRunRateWithoutSEIS") as Label);
                    if (lblRequiredRunRateWithoutSEIS.Text.ToNullFloat() < 0)
                    {
                        lblRequiredRunRateWithoutSEIS.ForeColor = Color.Red;
                        //    lblRequiredRunRateWithoutSEIS.Text = "(" + lblRequiredRunRateWithoutSEIS.Text + ")";
                    }
                }

            }

            foreach (GridViewRow row in GVRunRatewithSEIS.Rows)
            {
                Label lblLMActualWithSEISClaim = (row.FindControl("lblLMActualWithSEISClaim") as Label);
                if (lblLMActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLMActualWithSEISClaim.ForeColor = Color.Red;
                    //lblLMActualWithSEISClaim.Text = "(" + lblLMActualWithSEISClaim.Text + ")";
                }
                Label lblCMBudget = (row.FindControl("lblCMBudget") as Label);
                if (lblCMBudget.Text.ToNullFloat() < 0)
                {
                    lblCMBudget.ForeColor = Color.Red;
                    //lblCMBudget.Text = "(" + lblCMBudget.Text + ")";
                }

                Label lblCMActualWithSEISClaim = (row.FindControl("lblCMActualWithSEISClaim") as Label);
                if (lblCMActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCMActualWithSEISClaim.ForeColor = Color.Red;
                    //lblCMActualWithSEISClaim.Text = "(" + lblCMActualWithSEISClaim.Text + ")";
                }
                Label lblLYActualWithSEISClaim = (row.FindControl("lblLYActualWithSEISClaim") as Label);
                if (lblLYActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLYActualWithSEISClaim.ForeColor = Color.Red;
                    //lblLYActualWithSEISClaim.Text = "(" + lblLYActualWithSEISClaim.Text + ")";
                }

                Label lblCYBudget = (row.FindControl("lblCYBudget") as Label);
                if (lblCYBudget.Text.ToNullFloat() < 0)
                {
                    lblCYBudget.ForeColor = Color.Red;
                    //lblCYBudget.Text = "(" + lblCYBudget.Text + ")";
                }

                Label lblCYActualWithSEISClaim = (row.FindControl("lblCYActualWithSEISClaim") as Label);
                if (lblCYActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCYActualWithSEISClaim.ForeColor = Color.Red;
                    //lblCYActualWithSEISClaim.Text = "(" + lblCYActualWithSEISClaim.Text + ")";
                }

                Label lblCurrentFullYearBudget = (row.FindControl("lblCurrentFullYearBudget") as Label);
                if (lblCurrentFullYearBudget.Text.ToNullFloat() < 0)
                {
                    lblCurrentFullYearBudget.ForeColor = Color.Red;
                    //lblCurrentFullYearBudget.Text = "(" + lblCurrentFullYearBudget.Text + ")";
                }

                Label lblBalToAchieveWithSEIS = (row.FindControl("lblBalToAchieveWithSEIS") as Label);
                if (lblBalToAchieveWithSEIS.Text.ToNullFloat() < 0)
                {
                    lblBalToAchieveWithSEIS.ForeColor = Color.Red;
                    //lblBalToAchieveWithSEIS.Text = "(" + lblBalToAchieveWithSEIS.Text + ")";
                }

                Label lblActualRunRateWithSEIS = (row.FindControl("lblActualRunRateWithSEIS") as Label);
                if (lblActualRunRateWithSEIS.Text.ToNullFloat() < 0)
                {
                    lblActualRunRateWithSEIS.ForeColor = Color.Red;
                    //lblActualRunRateWithSEIS.Text = "(" + lblActualRunRateWithSEIS.Text + ")";
                }

                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    Label lblRequiredRunRateWithSEIS = (row.FindControl("lblRequiredRunRateWithSEIS") as Label);
                    if (lblRequiredRunRateWithSEIS.Text.ToNullFloat() < 0)
                    {
                        lblRequiredRunRateWithSEIS.ForeColor = Color.Red;
                        //  lblRequiredRunRateWithSEIS.Text = "(" + lblRequiredRunRateWithSEIS.Text + ")";
                    }
                }

            }

            foreach (GridViewRow row in gvRevenue.Rows)
            {

                Label lblLMActualWithoutSEISClaim = (row.FindControl("lblLMActualWithoutSEISClaim") as Label);
                if (lblLMActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLMActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblLMActualWithoutSEISClaim.Text = "(" + lblLMActualWithoutSEISClaim.Text + ")";
                }
                Label lblCMBudget = (row.FindControl("lblCMBudget") as Label);
                if (lblCMBudget.Text.ToNullFloat() < 0)
                {
                    lblCMBudget.ForeColor = Color.Red;
                    //lblCMBudget.Text = "(" + lblCMBudget.Text + ")";
                }

                Label lblCMActualWithoutSEISClaim = (row.FindControl("lblCMActualWithoutSEISClaim") as Label);
                if (lblCMActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCMActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblCMActualWithoutSEISClaim.Text = "(" + lblCMActualWithoutSEISClaim.Text + ")";
                }
                Label lblLYActualWithoutSEISClaim = (row.FindControl("lblLYActualWithoutSEISClaim") as Label);
                if (lblLYActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLYActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblLYActualWithoutSEISClaim.Text = "(" + lblLYActualWithoutSEISClaim.Text + ")";
                }

                Label lblCYBudget = (row.FindControl("lblCYBudget") as Label);
                if (lblCYBudget.Text.ToNullFloat() < 0)
                {
                    lblCYBudget.ForeColor = Color.Red;
                    //lblCYBudget.Text = "(" + lblCYBudget.Text + ")";
                }

                Label lblCYActualWithoutSEISClaim = (row.FindControl("lblCYActualWithoutSEISClaim") as Label);
                if (lblCYActualWithoutSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCYActualWithoutSEISClaim.ForeColor = Color.Red;
                    //lblCYActualWithoutSEISClaim.Text = "(" + lblCYActualWithoutSEISClaim.Text + ")";
                }

                Label lblCurrentFullYearBudget = (row.FindControl("lblCurrentFullYearBudget") as Label);
                if (lblCurrentFullYearBudget.Text.ToNullFloat() < 0)
                {
                    lblCurrentFullYearBudget.ForeColor = Color.Red;
                    //lblCurrentFullYearBudget.Text = "(" + lblCurrentFullYearBudget.Text + ")";
                }

                Label lblBalToAchieveWithoutSEIS = (row.FindControl("lblBalToAchieveWithoutSEIS") as Label);
                if (lblBalToAchieveWithoutSEIS.Text.ToNullFloat() < 0)
                {
                    lblBalToAchieveWithoutSEIS.ForeColor = Color.Red;
                    //lblBalToAchieveWithoutSEIS.Text = "(" + lblBalToAchieveWithoutSEIS.Text + ")";
                }

                Label lblActualRunRateWithoutSEIS = (row.FindControl("lblActualRunRateWithoutSEIS") as Label);
                if (lblActualRunRateWithoutSEIS.Text.ToNullFloat() < 0)
                {
                    lblActualRunRateWithoutSEIS.ForeColor = Color.Red;
                    //lblActualRunRateWithoutSEIS.Text = "(" + lblActualRunRateWithoutSEIS.Text + ")";
                }
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    Label lblRequiredRunRateWithoutSEIS = (row.FindControl("lblRequiredRunRateWithoutSEIS") as Label);
                    if (lblRequiredRunRateWithoutSEIS.Text.ToNullFloat() < 0)
                    {
                        lblRequiredRunRateWithoutSEIS.ForeColor = Color.Red;
                        //  lblRequiredRunRateWithoutSEIS.Text = "(" + lblRequiredRunRateWithoutSEIS.Text + ")";
                    }
                }

            }

            foreach (GridViewRow row in gvRevenuewithSEIS.Rows)
            {

                Label lblLMActualWithSEISClaim = (row.FindControl("lblLMActualWithSEISClaim") as Label);
                if (lblLMActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLMActualWithSEISClaim.ForeColor = Color.Red;
                    //lblLMActualWithSEISClaim.Text = "(" + lblLMActualWithSEISClaim.Text + ")";
                }
                Label lblCMBudget = (row.FindControl("lblCMBudget") as Label);
                if (lblCMBudget.Text.ToNullFloat() < 0)
                {
                    lblCMBudget.ForeColor = Color.Red;
                    //lblCMBudget.Text = "(" + lblCMBudget.Text + ")";
                }

                Label lblCMActualWithSEISClaim = (row.FindControl("lblCMActualWithSEISClaim") as Label);
                if (lblCMActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCMActualWithSEISClaim.ForeColor = Color.Red;
                    //lblCMActualWithSEISClaim.Text = "(" + lblCMActualWithSEISClaim.Text + ")";
                }
                Label lblLYActualWithSEISClaim = (row.FindControl("lblLYActualWithSEISClaim") as Label);
                if (lblLYActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblLYActualWithSEISClaim.ForeColor = Color.Red;
                    //lblLYActualWithSEISClaim.Text = "(" + lblLYActualWithSEISClaim.Text + ")";
                }

                Label lblCYBudget = (row.FindControl("lblCYBudget") as Label);
                if (lblCYBudget.Text.ToNullFloat() < 0)
                {
                    lblCYBudget.ForeColor = Color.Red;
                    //lblCYBudget.Text = "(" + lblCYBudget.Text + ")";
                }

                Label lblCYActualWithSEISClaim = (row.FindControl("lblCYActualWithSEISClaim") as Label);
                if (lblCYActualWithSEISClaim.Text.ToNullFloat() < 0)
                {
                    lblCYActualWithSEISClaim.ForeColor = Color.Red;
                    //lblCYActualWithSEISClaim.Text = "(" + lblCYActualWithSEISClaim.Text + ")";
                }

                Label lblCurrentFullYearBudget = (row.FindControl("lblCurrentFullYearBudget") as Label);
                if (lblCurrentFullYearBudget.Text.ToNullFloat() < 0)
                {
                    lblCurrentFullYearBudget.ForeColor = Color.Red;
                    //lblCurrentFullYearBudget.Text = "(" + lblCurrentFullYearBudget.Text + ")";
                }

                Label lblBalToAchieveWithSEIS = (row.FindControl("lblBalToAchieveWithSEIS") as Label);
                if (lblBalToAchieveWithSEIS.Text.ToNullFloat() < 0)
                {
                    lblBalToAchieveWithSEIS.ForeColor = Color.Red;
                    //lblBalToAchieveWithSEIS.Text = "(" + lblBalToAchieveWithSEIS.Text + ")";
                }

                Label lblActualRunRateWithSEIS = (row.FindControl("lblActualRunRateWithSEIS") as Label);
                if (lblActualRunRateWithSEIS.Text.ToNullFloat() < 0)
                {
                    lblActualRunRateWithSEIS.ForeColor = Color.Red;
                    //lblActualRunRateWithSEIS.Text = "(" + lblActualRunRateWithSEIS.Text + ")";
                }
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    Label lblRequiredRunRateWithSEIS = (row.FindControl("lblRequiredRunRateWithSEIS") as Label);
                    if (lblRequiredRunRateWithSEIS.Text.ToNullFloat() < 0)
                    {
                        lblRequiredRunRateWithSEIS.ForeColor = Color.Red;
                        //lblRequiredRunRateWithSEIS.Text = "(" + lblRequiredRunRateWithSEIS.Text + ")";
                    }
                }
            }
        }


        #endregion


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
        protected void lnkButton_Click(object sender, EventArgs e)
        {

            string Year;
            lblfyear.Text = drpFinancialYear.SelectedItem.Text;
            GetGridData();
            ChangeColor();
        }
        protected void GVRunRate_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "EBITDA without SEIS Income";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR1.ColumnSpan = 11;
                }
                else
                {
                    HR1.ColumnSpan = 8;
                }

                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                HeaderGridRow1.Cells.Add(HR1);

                GVRunRate.Controls[0].Controls.AddAt(0, HeaderGridRow1);

                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;

                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = Drpmonth.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 3;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text;
                HR21.Font.Bold = true;
                HR21.ColumnSpan = 3;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Text = "";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR21.ColumnSpan = 4;
                }
                else
                {
                    HR21.ColumnSpan = 1;
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                GVRunRate.Controls[0].Controls.AddAt(1, HeaderGridRow2);




            }
        }
        public void HideGridColumn(GridView obj)
        {
            if(Drpmonth.SelectedItem.Text =="Mar")
            {

                obj.Columns[8].Visible = false;
                obj.Columns[9].Visible = false;
                obj.Columns[10].Visible = false;
            }
            else
            {
                obj.Columns[8].Visible = true;
                obj.Columns[9].Visible = true;
                obj.Columns[10].Visible= true;
            }
        }
        protected void GVRunRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[7].Text = "Full Year Budget FY " + drpFinancialYear.SelectedItem.Text.Substring(2, 3) + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (B)";

                string MonthNo = string.Empty;
                if (Drpmonth.SelectedItem.Text == "Apr")
                {
                    MonthNo = "1";
                }
                if (Drpmonth.SelectedItem.Text == "May")
                {
                    MonthNo = "2";
                }
                if (Drpmonth.SelectedItem.Text == "Jun")
                {
                    MonthNo = "3";
                }
                if (Drpmonth.SelectedItem.Text == "Jul")
                {
                    MonthNo = "4";
                }
                if (Drpmonth.SelectedItem.Text == "Aug")
                {
                    MonthNo = "5";
                }
                if (Drpmonth.SelectedItem.Text == "Sep")
                {
                    MonthNo = "6";
                }
                if (Drpmonth.SelectedItem.Text == "Oct")
                {
                    MonthNo = "7";
                }
                if (Drpmonth.SelectedItem.Text == "Nov")
                {
                    MonthNo = "8";
                }
                if (Drpmonth.SelectedItem.Text == "Dec")
                {
                    MonthNo = "9";
                }
                if (Drpmonth.SelectedItem.Text == "Jan")
                {
                    MonthNo = "10";
                }
                if (Drpmonth.SelectedItem.Text == "Feb")
                {
                    MonthNo = "11";
                }
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    MonthNo = "12";
                }
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (A)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (A)/" + MonthNo;
                }

                string Month = string.Empty;
                if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Dec")
                {
                    Month = "Jan";
                }
                else if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Mar")
                {
                    Month = "Apr";
                }
                else
                {
                    Month = Drpmonth.Items[Drpmonth.SelectedIndex + 1].Text.ToString();
                }

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (C)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (C)/" + MonthNo;
                }
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    e.Row.Cells[10].Text = "N/A";
                }
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#FCE4D7");

                e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#DDEBF7");

                e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D9D9D9");
            }
            HideGridColumn(GVRunRate);
        }

        protected void GVRunRatewithSEIS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "EBITDA with SEIS Income";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR1.ColumnSpan = 11;
                }
                else
                {
                    HR1.ColumnSpan = 8;
                }
                
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                HeaderGridRow1.Cells.Add(HR1);

                GVRunRatewithSEIS.Controls[0].Controls.AddAt(0, HeaderGridRow1);

                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;

                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = Drpmonth.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 3;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text;
                HR21.Font.Bold = true;
                HR21.ColumnSpan = 3;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Text = "";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR21.ColumnSpan = 4;
                }
                else
                {
                    HR21.ColumnSpan = 1;
                }
                
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                GVRunRatewithSEIS.Controls[0].Controls.AddAt(1, HeaderGridRow2);
            }

        }

        protected void GVRunRatewithSEIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string MonthNo = string.Empty;
                if (Drpmonth.SelectedItem.Text == "Apr")
                {
                    MonthNo = "1";
                }
                if (Drpmonth.SelectedItem.Text == "May")
                {
                    MonthNo = "2";
                }
                if (Drpmonth.SelectedItem.Text == "Jun")
                {
                    MonthNo = "3";
                }
                if (Drpmonth.SelectedItem.Text == "Jul")
                {
                    MonthNo = "4";
                }
                if (Drpmonth.SelectedItem.Text == "Aug")
                {
                    MonthNo = "5";
                }
                if (Drpmonth.SelectedItem.Text == "Sep")
                {
                    MonthNo = "6";
                }
                if (Drpmonth.SelectedItem.Text == "Oct")
                {
                    MonthNo = "7";
                }
                if (Drpmonth.SelectedItem.Text == "Nov")
                {
                    MonthNo = "8";
                }
                if (Drpmonth.SelectedItem.Text == "Dec")
                {
                    MonthNo = "9";
                }
                if (Drpmonth.SelectedItem.Text == "Jan")
                {
                    MonthNo = "10";
                }
                if (Drpmonth.SelectedItem.Text == "Feb")
                {
                    MonthNo = "11";
                }
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    MonthNo = "12";
                }
                e.Row.Cells[7].Text = "Full Year Budget FY " + drpFinancialYear.SelectedItem.Text.Substring(2, 3) + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (B)";

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (A)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (A)/" + MonthNo;
                }


                string Month = string.Empty;
                if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Dec")
                {
                    Month = "Jan";
                }
                else if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Mar")
                {
                    Month = "Apr";
                }
                else
                {
                    Month = Drpmonth.Items[Drpmonth.SelectedIndex + 1].Text.ToString();
                }

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (C)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (C)/" + MonthNo;
                }
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    e.Row.Cells[10].Text = "N/A";
                }
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#FCE4D7");

                e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#DDEBF7");

                e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D9D9D9");
            }
            HideGridColumn(GVRunRatewithSEIS);
        }

        protected void gvRevenue_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "Revenue without SEIS Income";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR1.ColumnSpan = 11;
                }
                else
                {
                    HR1.ColumnSpan = 8;
                }
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                HeaderGridRow1.Cells.Add(HR1);

                gvRevenue.Controls[0].Controls.AddAt(0, HeaderGridRow1);

                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;

                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = Drpmonth.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 3;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text;
                HR21.Font.Bold = true;
                HR21.ColumnSpan = 3;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Text = "";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR21.ColumnSpan = 4;
                }
                else
                {
                    HR21.ColumnSpan = 1;
                }
                
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                gvRevenue.Controls[0].Controls.AddAt(1, HeaderGridRow2);




            }
        }
        protected void gvRevenue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string MonthNo = string.Empty;
                if (Drpmonth.SelectedItem.Text == "Apr")
                {
                    MonthNo = "1";
                }
                if (Drpmonth.SelectedItem.Text == "May")
                {
                    MonthNo = "2";
                }
                if (Drpmonth.SelectedItem.Text == "Jun")
                {
                    MonthNo = "3";
                }
                if (Drpmonth.SelectedItem.Text == "Jul")
                {
                    MonthNo = "4";
                }
                if (Drpmonth.SelectedItem.Text == "Aug")
                {
                    MonthNo = "5";
                }
                if (Drpmonth.SelectedItem.Text == "Sep")
                {
                    MonthNo = "6";
                }
                if (Drpmonth.SelectedItem.Text == "Oct")
                {
                    MonthNo = "7";
                }
                if (Drpmonth.SelectedItem.Text == "Nov")
                {
                    MonthNo = "8";
                }
                if (Drpmonth.SelectedItem.Text == "Dec")
                {
                    MonthNo = "9";
                }
                if (Drpmonth.SelectedItem.Text == "Jan")
                {
                    MonthNo = "10";
                }
                if (Drpmonth.SelectedItem.Text == "Feb")
                {
                    MonthNo = "11";
                }
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    MonthNo = "12";
                }
                e.Row.Cells[7].Text = "Full Year Budget FY " + drpFinancialYear.SelectedItem.Text.Substring(2, 3) + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (B)";

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (A)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (A)/" + MonthNo;
                }

                string Month = string.Empty;
                if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Dec")
                {
                    Month = "Jan";
                }
                else if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Mar")
                {
                    Month = "Apr";
                }
                else
                {
                    Month = Drpmonth.Items[Drpmonth.SelectedIndex + 1].Text.ToString();
                }

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (C)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (C)/" + MonthNo;
                }
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    e.Row.Cells[10].Text = "N/A";
                }
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#FCE4D7");

                e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#DDEBF7");

                e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D9D9D9");
            }
            HideGridColumn(gvRevenue);
        }

        protected void gvRevenuewithSEIS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "Revenue with SEIS Income";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR1.ColumnSpan = 11;
                }
                else
                {
                    HR1.ColumnSpan = 8;
                }
                
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                HeaderGridRow1.Cells.Add(HR1);

                gvRevenuewithSEIS.Controls[0].Controls.AddAt(0, HeaderGridRow1);

                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;

                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = Drpmonth.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 3;
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text;
                HR21.Font.Bold = true;
                HR21.ColumnSpan = 3;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Text = "";
                if (Drpmonth.SelectedItem.Text != "Mar")
                {
                    HR21.ColumnSpan = 4;
                }
                else
                {
                    HR21.ColumnSpan = 1;
                }
                
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow2.Cells.Add(HR21);

                gvRevenuewithSEIS.Controls[0].Controls.AddAt(1, HeaderGridRow2);
            }

        }

        protected void gvRevenuewithSEIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Header)
            {
                string MonthNo = string.Empty;
                if (Drpmonth.SelectedItem.Text == "Apr")
                {
                    MonthNo = "1";
                }
                if (Drpmonth.SelectedItem.Text == "May")
                {
                    MonthNo = "2";
                }
                if (Drpmonth.SelectedItem.Text == "Jun")
                {
                    MonthNo = "3";
                }
                if (Drpmonth.SelectedItem.Text == "Jul")
                {
                    MonthNo = "4";
                }
                if (Drpmonth.SelectedItem.Text == "Aug")
                {
                    MonthNo = "5";
                }
                if (Drpmonth.SelectedItem.Text == "Sep")
                {
                    MonthNo = "6";
                }
                if (Drpmonth.SelectedItem.Text == "Oct")
                {
                    MonthNo = "7";
                }
                if (Drpmonth.SelectedItem.Text == "Nov")
                {
                    MonthNo = "8";
                }
                if (Drpmonth.SelectedItem.Text == "Dec")
                {
                    MonthNo = "9";
                }
                if (Drpmonth.SelectedItem.Text == "Jan")
                {
                    MonthNo = "10";
                }
                if (Drpmonth.SelectedItem.Text == "Feb")
                {
                    MonthNo = "11";
                }
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    MonthNo = "12";
                }
                e.Row.Cells[7].Text = "Full Year Budget FY " + drpFinancialYear.SelectedItem.Text.Substring(2, 3) + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (B)";

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (A)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[9].Text = "Actual Run Rate till YTD -" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (A)/" + MonthNo;
                }

                string Month = string.Empty;
                if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Dec")
                {
                    Month = "Jan";
                }
                else if (Drpmonth.Items[Drpmonth.SelectedIndex].Text.ToString() == "Mar")
                {
                    Month = "Apr";
                }
                else
                {
                    Month = Drpmonth.Items[Drpmonth.SelectedIndex + 1].Text.ToString();
                }

                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(7, 2) + " (C)/" + MonthNo;
                }
                else
                {
                    e.Row.Cells[10].Text = "Required RR in  -" + Month + "-" + drpFinancialYear.SelectedItem.Text.Substring(2, 2) + " (C)/" + MonthNo;
                }
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    e.Row.Cells[10].Text = "N/A";
                }
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[2].BackColor = ColorTranslator.FromHtml("#FCE4D7");
                e.Row.Cells[3].BackColor = ColorTranslator.FromHtml("#FCE4D7");

                e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#DDEBF7");
                e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#DDEBF7");

                e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D9D9D9");
            }
            HideGridColumn(gvRevenuewithSEIS);
        }

        //protected void rdrwithoutseis_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdrwithsies.Checked = false;
        //}
        //protected void rdrwithsies_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdrwithoutseis.Checked = false;

        //}
    }
}


