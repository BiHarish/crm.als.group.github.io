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
using System.Globalization;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class BudgetVsActualReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (requestFinYear != null && requestMonthName != null && requestDivID != null)
                {
                    Label1.Text = Request["DivName"].ToString() + "-" + requestFinYear + " (Rs in Cr.)";
                    if (requestDivID.ToString() == "AFIL Consol" || requestDivID.ToString() == "ALS Consol")
                    {
                        bindConsolGrid();
                    }
                    else
                    {
                        bindGrd1();
                        bindGrd2();
                        bindGrd3();
                    }
                }
            }
        }
        #region Request
        public string requestFinYear { get { return Request["FinYear"]; } }
        public string requestMonthName { get { return Request["MName"]; } }
        public string requestDivID { get { return Request["DivID"]; } }
        #endregion

        #region Method
        void bindGrd1()
        {
            MisBudgetData _data = new MisBudgetData();
            MisBudgetDto request = new MisBudgetDto();
            request.mbfinancialyear = requestFinYear.ToString();
            request.mbdivisionid = requestDivID.ToNullLong();
            request.MonthName = requestMonthName.ToString();

            DataSet ds = _data.getBudgetVsActualReport1(request);

            if (ds != null)
            {
                gvReport1.DataSource = ds.Tables[0];
                gvReport1.DataBind();

                gvReport1.HeaderRow.Cells[2].Text = ds.Tables[0].Rows[0]["LastFinYear"].ToString() + " Actual";
                gvReport1.HeaderRow.Cells[3].Text = ds.Tables[0].Rows[0]["FinYear"].ToString() + " Budget";




            }
            else
            {
                gvReport1.DataBind();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindGrd2()
        {
            MisBudgetData _data = new MisBudgetData();
            MisBudgetDto request = new MisBudgetDto();
            request.mbfinancialyear = requestFinYear.ToString();
            request.mbdivisionid = requestDivID.ToNullLong();
            request.MonthName = requestMonthName.ToString();
            DataSet ds = _data.getBudgetVsActualReport2(request);
            if (ds != null)
            {
                string date = requestMonthName + "/" + requestMonthName + "/2020";
                DateTime dt = Convert.ToDateTime(date);
                txtMName1.Text = "Key Financial-" + dt.ToString("MMM");

                gvBudgetVsActualMonthWise.DataSource = ds.Tables[0];
                gvBudgetVsActualMonthWise.DataBind();
            }
            else
            {
                gvBudgetVsActualMonthWise.DataBind();
                txtMName1.Text = string.Empty;
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindGrd3()
        {
            MisBudgetData _data = new MisBudgetData();
            MisBudgetDto request = new MisBudgetDto();
            request.mbfinancialyear = requestFinYear.ToString();
            request.mbdivisionid = requestDivID.ToNullLong();
            request.MonthName = requestMonthName.ToString();
            DataSet ds = _data.getBudgetVsActualReport3(request);
            if (ds != null)
            {
                string date = requestMonthName + "/" + requestMonthName + "/2020";
                DateTime dt = Convert.ToDateTime(date);
                txtMName2.Text = "Key Financial YTD-" + dt.ToString("MMM");

                gvBudgetVsActualYearWise.DataSource = ds.Tables[0];
                gvBudgetVsActualYearWise.DataBind();
            }
            else
            {
                gvBudgetVsActualYearWise.DataBind();
                txtMName2.Text = string.Empty;
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        void bindConsolGrid()
        {
            MisBudgetData _data = new MisBudgetData();
            MisBudgetVsActualConsolDto req = new MisBudgetVsActualConsolDto();
            req.financialyear = requestFinYear.ToString();
            req.division = requestDivID.ToString();
            req.month = requestMonthName.ToLong();
            DataSet ds = _data.BudgetVsActualConsol(req);
            if(ds!=null)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    gvReport1.DataSource = ds.Tables[0];
                    gvReport1.DataBind();

                    gvReport1.HeaderRow.Cells[2].Text = ds.Tables[0].Rows[0]["LastFinYear"].ToString() + " Actual";
                    gvReport1.HeaderRow.Cells[3].Text = ds.Tables[0].Rows[0]["FinYear"].ToString() + " Budget";

                }
                else
                {
                    gvReport1.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string date = requestMonthName + "/" + requestMonthName + "/2020";
                    DateTime dt = Convert.ToDateTime(date);
                    txtMName1.Text = "Key Financial-" + dt.ToString("MMM");

                    gvBudgetVsActualMonthWise.DataSource = ds.Tables[1];
                    gvBudgetVsActualMonthWise.DataBind();
                }
                else
                {
                    gvBudgetVsActualMonthWise.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string date = requestMonthName + "/" + requestMonthName + "/2020";
                    DateTime dt = Convert.ToDateTime(date);
                    txtMName2.Text = "Key Financial YTD-" + dt.ToString("MMM");
                    gvBudgetVsActualYearWise.DataSource = ds.Tables[2];
                    gvBudgetVsActualYearWise.DataBind();
                }
                else
                {
                    gvBudgetVsActualYearWise.DataBind();
                }
            }
        }
        #endregion

        protected void gvReport1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 2; i <= NumCells - 1; i++)
                {
                    if (i < 2)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }
                    else
                    {


                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                    }

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;



                Label gvlblDesc = (Label)e.Row.FindControl("gvlblParticular");
                Label gvlblProvisional = (Label)e.Row.FindControl("gvlblProvisional");
                Label gvlblExternalBudget = (Label)e.Row.FindControl("gvlblExternalBudget");
                //if (gvlblDesc.Text == "Gross Profit" || gvlblDesc.Text == "Total Indirect Cost" || gvlblDesc.Text == "EBITDA Before CC"
                //    || gvlblDesc.Text == "EBITDA after CC" || gvlblDesc.Text == "PAT" || gvlblDesc.Text == "Cash Profit")
                //{
                //    e.Row.Cells[0].Style.Add("BORDER", "solid");
                //    e.Row.Cells[1].Style.Add("BORDER", "solid");
                //    e.Row.Cells[2].Style.Add("BORDER", "solid");
                //    e.Row.Cells[3].Style.Add("BORDER", "solid");
                //}

                if (gvlblDesc.Text == "Gross Profit%" || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%" ||
                    gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    if (gvlblProvisional.Text != string.Empty)
                    {
                        if (gvlblProvisional.Text != "0.00")
                            gvlblProvisional.Text = Math.Round(decimal.Parse(gvlblProvisional.Text), 1).ToString() + "%";
                    }
                    if (gvlblExternalBudget.Text != string.Empty)
                    {
                        if (gvlblExternalBudget.Text != "0.00")
                            gvlblExternalBudget.Text = Math.Round(decimal.Parse(gvlblExternalBudget.Text), 1) + "%";
                    }
                }

                if (gvlblDesc.Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                }
                if (gvlblDesc.Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }

                if (requestDivID.ToString() == "1")
                {

                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }
                if (gvlblProvisional.Text == "0.00" || gvlblProvisional.Text == string.Empty)
                {
                    gvlblProvisional.Text = "-";
                }
                if (gvlblExternalBudget.Text == "0.00")
                {
                    gvlblExternalBudget.Text = "-";
                }
            }
        }

        protected void gvBudgetVsActualMonthWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Label gvlblDesc = (Label)e.Row.FindControl("gvlblParticular");
                Label gvlblCyBudget = (Label)e.Row.FindControl("gvlblCyBudget");
                Label gvlblCYActual = (Label)e.Row.FindControl("gvlblCYActual");
                Label gvlblLyActual = (Label)e.Row.FindControl("gvlblLyActual");
                Label gvlblActvsBdgt = (Label)e.Row.FindControl("gvlblActvsBdgt");
                //System.Web.UI.WebControls.Image imgUpGreenAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgUpGreenAB");
                //System.Web.UI.WebControls.Image imgDownGreenAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgDownGreenAB");
                //System.Web.UI.WebControls.Image imgUpRedAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgDownRedAB");
                //System.Web.UI.WebControls.Image imgDownRedAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgUpRedAB");
                //if (gvlblDesc.Text == "Gross Profit" || gvlblDesc.Text == "Total Indirect Cost" || gvlblDesc.Text == "EBITDA Before CC"
                //   || gvlblDesc.Text == "EBITDA after CC" || gvlblDesc.Text == "PAT" || gvlblDesc.Text == "Cash Profit")
                //{
                //    e.Row.Cells[0].Style.Add("BORDER", "solid");
                //    e.Row.Cells[1].Style.Add("BORDER", "solid");
                //    e.Row.Cells[2].Style.Add("BORDER", "solid");
                //    e.Row.Cells[3].Style.Add("BORDER", "solid");
                //    e.Row.Cells[4].Style.Add("BORDER", "solid");
                //}
                if (gvlblActvsBdgt.Text.ToDouble() < 0)
                {
                    gvlblActvsBdgt.ForeColor = System.Drawing.Color.Red;
                }
                //if (gvlblDesc.Text == "Revenue" || gvlblDesc.Text == "Gross Profit" || gvlblDesc.Text == "EBITDA Before CC" || gvlblDesc.Text == "EBITDA after CC"
                //    || gvlblDesc.Text == "EI Income" || gvlblDesc.Text == "PAT" || gvlblDesc.Text == "Cash Profit" || gvlblDesc.Text == "Gross Profit%"
                //     || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%") 
                //{
                //    if (gvlblActvsBdgt.Text != "")
                //    {
                //        if (gvlblActvsBdgt.Text.ToDouble() > 0)
                //        {
                //            imgUpGreenAB.Visible = true;
                //            imgDownGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;

                //        }
                //        else if (gvlblActvsBdgt.Text.ToDouble() < 0)
                //        {
                //            imgDownGreenAB.Visible = true;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;
                //        }
                //        else
                //        {
                //            imgDownGreenAB.Visible = true;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;
                //        }
                //    }
                //}
                //if (gvlblDesc.Text == "Direct Cost" || gvlblDesc.Text == "Salaries" || gvlblDesc.Text == "Other Indirect Expenses" || gvlblDesc.Text == "Total Indirect Cost"
                //    || gvlblDesc.Text == "Salaries- Commom" || gvlblDesc.Text == "Other Expenses- Common" || gvlblDesc.Text == "Total Indirect Cost- Common" || gvlblDesc.Text == "Corporate Cost"
                //     || gvlblDesc.Text == "Interest Exp" || gvlblDesc.Text == "Interest Income" || gvlblDesc.Text == "Depreciation" || gvlblDesc.Text == "EI Expenses"
                //     || gvlblDesc.Text == "Taxes" || gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                //{
                //    if (gvlblActvsBdgt.Text != "")
                //    {
                //        if (gvlblActvsBdgt.Text.ToDouble() > 0)
                //        {
                //            imgUpGreenAB.Visible = false;
                //            imgDownGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = true;

                //        }
                //        else if (gvlblActvsBdgt.Text.ToDouble() < 0)
                //        {
                //            imgDownGreenAB.Visible = false;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = true;
                //            imgDownRedAB.Visible = false;
                //        }
                //        else
                //        {
                //            imgDownGreenAB.Visible = false;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;
                //        }
                //    }
                //}


                if (gvlblDesc.Text == "Gross Profit%" || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%" ||
                    gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    if (gvlblCyBudget.Text != string.Empty)
                    {
                        if (gvlblCyBudget.Text != "0.00")
                            gvlblCyBudget.Text = Math.Round(decimal.Parse(gvlblCyBudget.Text), 1).ToString() + "%";
                    }
                    if (gvlblCYActual.Text != string.Empty)
                    {
                        if (gvlblCYActual.Text != "0.00")
                            gvlblCYActual.Text = Math.Round(decimal.Parse(gvlblCYActual.Text), 1).ToString() + "%";
                    }
                    if (gvlblLyActual.Text != string.Empty)
                    {
                        if (gvlblLyActual.Text != "0.00")
                            gvlblLyActual.Text = Math.Round(decimal.Parse(gvlblLyActual.Text), 1).ToString() + "%";
                    }

                }
                if (gvlblActvsBdgt.Text != string.Empty)
                {
                    if (gvlblActvsBdgt.Text != "0.00")
                        gvlblActvsBdgt.Text = Math.Round(decimal.Parse(gvlblActvsBdgt.Text), 1).ToString() + "%";
                    //if (gvlblActvsBdgt.Text.Remove(gvlblActvsBdgt.Text.Length - 1, 1).ToDouble() < 0)
                    //{
                    //    gvlblActvsBdgt.Text = "(" + gvlblActvsBdgt.Text + ")";
                    //}
                }
                if (gvlblDesc.Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                }
                if (gvlblDesc.Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }

                if (requestDivID.ToString() == "1")
                {

                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }

                if (gvlblCyBudget.Text == "0.00")
                {
                    gvlblCyBudget.Text = "-";
                }
                if (gvlblCYActual.Text == "0.00")
                {
                    gvlblCYActual.Text = "-";
                }
                if (gvlblLyActual.Text == "0.00")
                {
                    gvlblLyActual.Text = "-";
                }
                if (gvlblActvsBdgt.Text == "0.00")
                {
                    gvlblActvsBdgt.Text = "-";
                    //imgDownGreenAB.Visible = false;
                    //imgUpGreenAB.Visible = false;
                    //imgUpRedAB.Visible = false;
                    //imgDownRedAB.Visible = false;
                }


            }
        }

        protected void gvBudgetVsActualYearWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Label gvlblDesc = (Label)e.Row.FindControl("gvlblParticular");
                Label gvlblCyBudget = (Label)e.Row.FindControl("gvlblCyBudget");
                Label gvlblCYActual = (Label)e.Row.FindControl("gvlblCYActual");
                Label gvlblLyActual = (Label)e.Row.FindControl("gvlblLyActual");
                Label gvlblActvsBdgt = (Label)e.Row.FindControl("gvlblActvsBdgt");
                //if (gvlblDesc.Text == "Gross Profit" || gvlblDesc.Text == "Total Indirect Cost" || gvlblDesc.Text == "EBITDA Before CC"
                //   || gvlblDesc.Text == "EBITDA after CC" || gvlblDesc.Text == "PAT" || gvlblDesc.Text == "Cash Profit")
                //{
                //    e.Row.Cells[0].Style.Add("BORDER", "solid");
                //    e.Row.Cells[1].Style.Add("BORDER", "solid");
                //    e.Row.Cells[2].Style.Add("BORDER", "solid");
                //    e.Row.Cells[3].Style.Add("BORDER", "solid");
                //    e.Row.Cells[4].Style.Add("BORDER", "solid");
                //}
                //System.Web.UI.WebControls.Image imgUpGreenAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgUpGreenAB");
                //System.Web.UI.WebControls.Image imgDownGreenAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgDownGreenAB");
                //System.Web.UI.WebControls.Image imgUpRedAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgDownRedAB");
                //System.Web.UI.WebControls.Image imgDownRedAB = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgUpRedAB");

                if (gvlblActvsBdgt.Text.ToDouble() < 0)
                {
                    gvlblActvsBdgt.ForeColor = System.Drawing.Color.Red;
                }
                //if (gvlblDesc.Text == "Revenue" || gvlblDesc.Text == "Gross Profit" || gvlblDesc.Text == "EBITDA Before CC" || gvlblDesc.Text == "EBITDA after CC"
                //   || gvlblDesc.Text == "EI Income" || gvlblDesc.Text == "PAT" || gvlblDesc.Text == "Cash Profit" || gvlblDesc.Text == "Gross Profit%"
                //    || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%")
                //{
                //    if (gvlblActvsBdgt.Text != "")
                //    {
                //        if (gvlblActvsBdgt.Text.ToDouble() > 0)
                //        {
                //            imgUpGreenAB.Visible = true;
                //            imgDownGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;

                //        }
                //        else if (gvlblActvsBdgt.Text.ToDouble() < 0)
                //        {
                //            imgDownGreenAB.Visible = true;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;
                //        }
                //        else
                //        {
                //            imgDownGreenAB.Visible = true;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = false;
                //        }
                //    }
                //}
                //if (gvlblDesc.Text == "Direct Cost" || gvlblDesc.Text == "Salaries" || gvlblDesc.Text == "Other Indirect Expenses" || gvlblDesc.Text == "Total Indirect Cost"
                //    || gvlblDesc.Text == "Salaries- Commom" || gvlblDesc.Text == "Other Expenses- Common" || gvlblDesc.Text == "Total Indirect Cost- Common" || gvlblDesc.Text == "Corporate Cost"
                //     || gvlblDesc.Text == "Interest Exp" || gvlblDesc.Text == "Interest Income" || gvlblDesc.Text == "Depreciation" || gvlblDesc.Text == "EI Expenses"
                //     || gvlblDesc.Text == "Taxes" || gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                //{
                //    if (gvlblActvsBdgt.Text != "")
                //    {
                //        if (gvlblActvsBdgt.Text.ToDouble() > 0)
                //        {
                //            imgUpGreenAB.Visible = false;
                //            imgDownGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = true;

                //        }
                //        else if (gvlblActvsBdgt.Text.ToDouble() < 0)
                //        {
                //            imgDownGreenAB.Visible = false;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = true;
                //            imgDownRedAB.Visible = false;
                //        }
                //        else
                //        {
                //            imgDownGreenAB.Visible = false;
                //            imgUpGreenAB.Visible = false;
                //            imgUpRedAB.Visible = false;
                //            imgDownRedAB.Visible = true;
                //        }
                //    }
                //}

                if (gvlblDesc.Text == "Gross Profit%" || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%" ||
                    gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    if (gvlblCyBudget.Text != string.Empty)
                    {
                        if (gvlblCyBudget.Text != "0.00")
                            gvlblCyBudget.Text = Math.Round(decimal.Parse(gvlblCyBudget.Text), 1).ToString() + "%";
                    }
                    if (gvlblCYActual.Text != string.Empty)
                    {
                        if (gvlblCYActual.Text != "0.00")
                            gvlblCYActual.Text = Math.Round(decimal.Parse(gvlblCYActual.Text), 1).ToString() + "%";
                    }
                    if (gvlblLyActual.Text != string.Empty)
                    {
                        if (gvlblLyActual.Text != "0.00")
                            gvlblLyActual.Text = Math.Round(decimal.Parse(gvlblLyActual.Text), 1).ToString() + "%";
                    }

                }
                if (gvlblActvsBdgt.Text != string.Empty)
                {
                    if (gvlblActvsBdgt.Text != "0.00")
                        gvlblActvsBdgt.Text = Math.Round(decimal.Parse(gvlblActvsBdgt.Text), 1).ToString() + "%";
                    //if (gvlblActvsBdgt.Text.Remove(gvlblActvsBdgt.Text.Length - 1, 1).ToDouble() < 0)
                    //{
                    //    gvlblActvsBdgt.Text = "(" + gvlblActvsBdgt.Text + ")";
                    //}
                }
                if (gvlblDesc.Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                }
                if (gvlblDesc.Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }
                if (gvlblDesc.Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ddd9c4");
                }

                if (requestDivID.ToString() == "1")
                {

                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }

                if (gvlblCyBudget.Text == "0.00")
                {
                    gvlblCyBudget.Text = "-";
                }
                if (gvlblCYActual.Text == "0.00")
                {
                    gvlblCYActual.Text = "-";
                }
                if (gvlblLyActual.Text == "0.00")
                {
                    gvlblLyActual.Text = "-";
                }
                if (gvlblActvsBdgt.Text == "0.00")
                {
                    gvlblActvsBdgt.Text = "-";
                    //imgDownGreenAB.Visible = false;
                    //imgUpGreenAB.Visible = false;
                    //imgUpRedAB.Visible = false;
                    //imgDownRedAB.Visible = false;
                }



            }

        }


    }
}