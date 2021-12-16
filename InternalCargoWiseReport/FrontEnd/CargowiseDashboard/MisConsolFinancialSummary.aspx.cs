using ICWR.Data;
using ICWR.Data.Utility;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MisConsolFinancialSummary : System.Web.UI.Page
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

        }

        void GetGridData()
        {
            MisConsolFinancialSummaryData _data = new MisConsolFinancialSummaryData();
            MisConsolFinancialSummaryDto _dto = new MisConsolFinancialSummaryDto();

            _dto.FinancialYear = drpFinancialYear.SelectedValue;
            _dto.Month = Drpmonth.SelectedValue.ToConvertInt();
            DataSet ds = _data.GetGridData(_dto);
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvRevenue.DataSource = ds.Tables[0];
                    gvRevenue.DataBind();
                    //  gvRevenue.Rows[gvRevenue.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    gvRevenue.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvEBITDA.DataSource = ds.Tables[1];
                    gvEBITDA.DataBind();
                    //  gvRevenue.Rows[gvRevenue.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    gvEBITDA.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvPAT.DataSource = ds.Tables[2];
                    gvPAT.DataBind();
                    //  gvRevenue.Rows[gvRevenue.Rows.Count - 1].Font.Bold = true;//Your Style
                }
                else
                {
                    gvPAT.DataBind();
                }
                //if (ds.Tables[3].Rows.Count > 0)
                //{
                //    gvRevenuewithSEIS.DataSource = ds.Tables[3];
                //    gvRevenuewithSEIS.DataBind();
                //    gvRevenuewithSEIS.Rows[gvRevenuewithSEIS.Rows.Count - 1].Font.Bold = true;//Your Style
                //}
                //else
                //{
                //    gvRevenuewithSEIS.DataBind();
                //}


                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    GVRunRate.DataSource = ds.Tables[0];
                //    GVRunRate.DataBind();
                //    GVRunRate.Rows[GVRunRate.Rows.Count - 1].Font.Bold = true;//Your Style


                //}
                //else
                //{
                //    GVRunRate.DataBind();
                //}

                //if (ds.Tables[2].Rows.Count > 0)
                //{
                //    gvRevenue.DataSource = ds.Tables[2];
                //    gvRevenue.DataBind();
                //    gvRevenue.Rows[gvRevenue.Rows.Count - 1].Font.Bold = true;//Your Style
                //}
                //else
                //{
                //    gvRevenue.DataBind();
                //}
            }
            else
            {
                gvRevenue.DataBind();
                gvEBITDA.DataBind();
                gvPAT.DataBind();
            }

        }
        #endregion

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            GetGridData();
        }

        protected void gvRevenue_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "Revenue";
                HR1.ColumnSpan = 1;
                HR1.HorizontalAlign = HorizontalAlign.Left;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow1.Cells.Add(HR1);


                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.BorderWidth = 0;
                HR1.ColumnSpan = 1;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");

                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);
                gvRevenue.Controls[0].Controls.AddAt(0, HeaderGridRow1);


                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;
                HR21.BorderWidth = 0;
                HR21.HorizontalAlign = HorizontalAlign.Center;

                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = drpFinancialYear.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 1;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BorderWidth = 0;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);
                gvRevenue.Controls[0].Controls.AddAt(1, HeaderGridRow2);
            }
        }

        protected void gvRevenue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
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

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                Label lbl = (e.Row.FindControl("lblID") as Label);
                if(lbl.Text== "Infra-Total" || lbl.Text == "3PL Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#FFE699");  //
                    e.Row.Font.Bold = true;
                }
                if (lbl.Text == "Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#BFBFBF");
                    e.Row.Font.Bold = true;
                }
            }
        }



        protected void gvEBITDA_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "EBITDA";
                HR1.ColumnSpan = 1;
                HR1.HorizontalAlign = HorizontalAlign.Left;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow1.Cells.Add(HR1);


                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.BorderWidth = 0;
                HR1.ColumnSpan = 1;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");

                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);
                gvEBITDA.Controls[0].Controls.AddAt(0, HeaderGridRow1);


                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;
                HR21.BorderWidth = 0;
                HR21.HorizontalAlign = HorizontalAlign.Center;

                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = drpFinancialYear.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 1;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BorderWidth = 0;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);
                gvEBITDA.Controls[0].Controls.AddAt(1, HeaderGridRow2);
            }
        }

        protected void gvEBITDA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
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

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                Label lbl = (e.Row.FindControl("lblID") as Label);
                if (lbl.Text == "Infra-Total" || lbl.Text == "3PL Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#FFE699");  //
                    e.Row.Font.Bold = true;
                }
                if (lbl.Text == "Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#BFBFBF");
                    e.Row.Font.Bold = true;
                }
            }
        }


        protected void gvPAT_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR1 = new TableCell();
                HR1.Text = "PAT";
                HR1.ColumnSpan = 1;
                HR1.HorizontalAlign = HorizontalAlign.Left;
                HR1.ForeColor = Color.Black;
                HR1.Font.Bold = true;
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow1.Cells.Add(HR1);


                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.BorderWidth = 0;
                HR1.ColumnSpan = 1;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.BackColor = ColorTranslator.FromHtml("#F2F2F2");

                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);

                HR1 = new TableCell();
                HR1.Font.Bold = true;
                HR1.Text = "Amt in INR Crores";
                HR1.HorizontalAlign = HorizontalAlign.Center;
                HR1.ColumnSpan = 2;
                HeaderGridRow1.Cells.Add(HR1);
                gvPAT.Controls[0].Controls.AddAt(0, HeaderGridRow1);


                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HR21 = new TableCell();
                HR21.Text = "";
                HR21.ColumnSpan = 1;
                HR21.BorderWidth = 0;
                HR21.HorizontalAlign = HorizontalAlign.Center;

                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                HR21.Text = drpFinancialYear.SelectedItem.Text;
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 1;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);


                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);

                HR21 = new TableCell();
                HR21.Font.Bold = true;
                if (Drpmonth.SelectedValue.ToString() == "1" || Drpmonth.SelectedValue.ToString() == "2" || Drpmonth.SelectedValue.ToString() == "3")
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(5, 4);
                }
                else
                {
                    HR21.Text = "YTD-" + Drpmonth.SelectedItem.Text + "-" + drpFinancialYear.SelectedItem.Text.Substring(0, 4);
                }
                HR21.HorizontalAlign = HorizontalAlign.Center;
                HR21.ColumnSpan = 2;
                HR21.BorderWidth = 0;
                HR21.BackColor = ColorTranslator.FromHtml("#F2F2F2");
                HeaderGridRow2.Cells.Add(HR21);
                gvPAT.Controls[0].Controls.AddAt(1, HeaderGridRow2);
            }
        }

        protected void gvPAT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
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

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

                Label lbl = (e.Row.FindControl("lblID") as Label);
                if (lbl.Text == "Infra-Total" || lbl.Text == "3PL Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#FFE699");  //
                    e.Row.Font.Bold = true;
                }
                if (lbl.Text == "Total")
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.BackColor = ColorTranslator.FromHtml("#BFBFBF");
                    e.Row.Font.Bold = true;
                }
            }
        }

    }
}