using ICWR.Data;
using ICWR.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternalQuery;
using ICWR.Models;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MisFinancialDashboard : System.Web.UI.Page
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

            DataSet result = _data.getFinYearForRunRate();

            if (result != null)
            {
                drpFinancialYear.DataSource = result.Tables[0];
                drpFinancialYear.DataValueField = "FinYear";
                drpFinancialYear.DataTextField = "FinYear";
                drpFinancialYear.DataBind();

            }

            DataSet divisionResult = _data.getDivisionWithUniqueID();
            if (divisionResult != null)
            {
                drpDivision.DataSource = divisionResult.Tables[0];
                drpDivision.DataValueField = "ID";
                drpDivision.DataTextField = "mddesc";
                drpDivision.DataBind();
                drpDivision.Items.Insert(0, new ListItem("--Select--", string.Empty));

            }


        }

        bool Searchvalidation()
        {

            if (drpFinancialYear.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Financial Year!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpDivision.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }

            return true;
        }

        void getdataMisOutstanding()
        {
            string DivisionID = string.Empty;
            lblChartFinReview.Text = drpDivision.SelectedItem.Text;
            lblPMetrics.Text = drpDivision.SelectedItem.Text;
            lblLinePM.Text = drpDivision.SelectedItem.Text;
            DivisionID = drpDivision.SelectedValue;
            OutResult result = InternalQuery.Query.MisFinDashboard(drpFinancialYear.SelectedValue, DivisionID);
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        dt = result.ds.Tables[0];


                        MISData _data = new MISData();
                        MISDto request = new MISDto();
                        request.uniqueID = DivisionID;
                        request.mafinancialyear = drpFinancialYear.SelectedValue;
                        DataSet _dsActual = _data.MisFinDashboard(request);
                        if (_dsActual != null && _dsActual.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < _dsActual.Tables[0].Rows.Count; i++)
                            {
                                dt.ImportRow(_dsActual.Tables[0].Rows[i]);
                            }
                        }

                        dt.Columns.Remove("AH_GC");
                        dt.Columns.Remove("GC_PK");
                        dt.Columns.Remove("GC_Code");
                        dt.AcceptChanges();

                        gvResult.DataSource = dt;
                        gvResult.DataBind();

                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt.Rows[i];
                            if (dr["Name"].ToString() == "OverDue")
                                dr.Delete();
                        }
                        dt.AcceptChanges();
                        DataTable dtnew = new DataTable();
                        dtnew.Columns.Add("Mname");
                        dtnew.Columns.Add("OutStanding");
                        dtnew.Columns.Add("Collection");
                        dtnew.Columns.Add("Revenue");
                        dtnew.Columns.Add("GP");

                        DataRow dr1 = null;
                        foreach (DataColumn column in dt.Columns)
                        {
                            dr1 = dtnew.NewRow();
                            string ColumnName = column.ColumnName.ToString();
                            dr1["Mname"] = ColumnName;
                            dtnew.Rows.Add(dr1);

                            if (dt.Rows.Count == 1)
                            {
                                dr1["OutStanding"] = dt.Rows[0][ColumnName].ToString();
                            }
                            if (dt.Rows.Count == 2)
                            {
                                dr1["OutStanding"] = dt.Rows[0][ColumnName].ToString();
                                dr1["Collection"] = dt.Rows[1][ColumnName].ToString();
                            }
                            if (dt.Rows.Count == 3)
                            {
                                dr1["OutStanding"] = dt.Rows[0][ColumnName].ToString();
                                dr1["Collection"] = dt.Rows[1][ColumnName].ToString();
                                dr1["Revenue"] = dt.Rows[2][ColumnName].ToString();

                            }
                            if (dt.Rows.Count == 4)
                            {
                                dr1["GP"] = dt.Rows[3][ColumnName].ToString();
                                dr1["OutStanding"] = dt.Rows[0][ColumnName].ToString();
                                dr1["Collection"] = dt.Rows[1][ColumnName].ToString();
                                dr1["Revenue"] = dt.Rows[2][ColumnName].ToString();
                            }
                            if (column.ColumnName == "Mar")
                            {
                                break;
                            }
                        }

                        divFinYear.Visible = true;
                        divtblFinYear.Visible = true;
                        divLinePM.Visible = true;
                        ChartFinYearWise.DataSource = dtnew;
                        ChartFinYearWise.DataBind();

                        LineChartFinYearWise.DataSource = dtnew;
                        LineChartFinYearWise.DataBind();

                        if (dtnew.Rows.Count == 0)
                        {
                            divFinYear.Visible = false;
                            divtblFinYear.Visible = false;
                            divLinePM.Visible = false;
                            ChartFinYearWise.DataSource = null;
                            ChartFinYearWise.DataBind();
                        }

                    }
                    else
                    {
                        divFinYear.Visible = false;
                        divtblFinYear.Visible = false;
                        divLinePM.Visible = false;
                        ChartFinYearWise.DataSource = null;
                        ChartFinYearWise.DataBind();
                        gvResult.DataBind();

                    }
                }
                else
                {
                    divFinYear.Visible = false;
                    divtblFinYear.Visible = false;
                    divLinePM.Visible = false;
                    ChartFinYearWise.DataSource = null;
                    ChartFinYearWise.DataBind();
                    gvResult.DataBind();

                }
            }
        }

        void getForcastRevenue()
        {
            MISData _data = new MISData();
            MISDto request = new MISDto();
            string DivisionID = string.Empty;
            lblChartRevenueVsForcastRevenue.Text = drpDivision.SelectedItem.Text;
            lbltblForCastRevenue.Text = drpDivision.SelectedItem.Text;
            lblLineForcastRevenue.Text = drpDivision.SelectedItem.Text;
            DivisionID = drpDivision.SelectedValue;
            request.uniqueID = DivisionID;
            request.mafinancialyear = drpFinancialYear.SelectedValue;
            DataSet ds = _data.getForcastRevenue(request);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DivRevenueVsForcastRevenue.Visible = true;
                divtblForcastRevenue.Visible = true;
                DivLineForcastRevenue.Visible = true;
                ChartForcastRevenue.DataSource = ds.Tables[0];
                ChartForcastRevenue.DataBind();
                LineChartForcastRevenue.DataSource = ds.Tables[0];
                LineChartForcastRevenue.DataBind();

                gvRevenueVsForcast.DataSource = ds.Tables[0];
                gvRevenueVsForcast.DataBind();
            }
            else
            {
                ChartForcastRevenue.DataSource = null;
                ChartForcastRevenue.DataBind();
                gvRevenueVsForcast.DataBind();
                DivRevenueVsForcastRevenue.Visible = false;
                divtblForcastRevenue.Visible = false;
                DivLineForcastRevenue.Visible = false;
            }
        }

        void getRevenueVsCost()
        {
            MISData _data = new MISData();
            MISDto request = new MISDto();

            lblchartRevenueVsCost.Text = drpDivision.SelectedItem.Text;
            lbltblRevenueVsCost.Text = drpDivision.SelectedItem.Text;
            lblLineRevenueVsCost.Text = drpDivision.SelectedItem.Text;

            request.uniqueID = drpDivision.SelectedValue;
            request.mafinancialyear = drpFinancialYear.SelectedValue;
            DataSet ds = _data.getRevenueVsCost(request);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];


                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Mname");
                dtnew.Columns.Add("Revenue");
                dtnew.Columns.Add("Cost");

                DataRow dr1 = null;
                foreach (DataColumn column in dt.Columns)
                {
                    dr1 = dtnew.NewRow();
                    string ColumnName = column.ColumnName.ToString();
                    dr1["Mname"] = ColumnName;
                    dtnew.Rows.Add(dr1);

                    dr1["Revenue"] = dt.Rows[0][ColumnName].ToString();
                    dr1["Cost"] = dt.Rows[1][ColumnName].ToString();

                    if (column.ColumnName == "Mar")
                    {
                        break;
                    }
                }
                divRevenueVsCost.Visible = true;
                divtblRevenueVsCost.Visible = true;
                divLineRevenueVsCost.Visible = true;
                ChartRevenueVsCost.DataSource = dtnew;
                ChartRevenueVsCost.DataBind();
                LineChartRevenueVsCost.DataSource = dtnew;
                LineChartRevenueVsCost.DataBind();

                gvRevenueVsCost.DataSource = dt;
                gvRevenueVsCost.DataBind();
            }
            else
            {
                ChartRevenueVsCost.DataSource = null;
                ChartRevenueVsCost.DataBind();
                divRevenueVsCost.Visible = false;
                divtblRevenueVsCost.Visible = false;
                divLineRevenueVsCost.Visible = false;
                gvRevenueVsCost.DataBind();
            }
        }

        void getTop10Client()
        {
            OutResult result = InternalQuery.Query.getTop10Client(drpFinancialYear.SelectedValue, drpDivision.SelectedValue);
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        lblChartRevenueFromTopClient.Text = drpDivision.SelectedItem.Text;
                        lbltblRevenueFromTopClient.Text = drpDivision.SelectedItem.Text;
                        lblLineRevenueTopClient.Text = drpDivision.SelectedItem.Text;

                        dt = result.ds.Tables[0];

                        gvTopClient.DataSource = dt;
                        gvTopClient.DataBind();

                        divRevenueFromTopClient.Visible = true;
                        divtblRevenueToClient.Visible = true;
                        divLineTopClient.Visible = true;
                        ChartTopClient.DataSource = dt;
                        ChartTopClient.DataBind();
                        LineChartTopClient.DataSource = dt;
                        LineChartTopClient.DataBind();


                    }
                    else
                    {
                        gvTopClient.DataBind();
                        divRevenueFromTopClient.Visible = false;
                        divtblRevenueToClient.Visible = false;
                        divLineTopClient.Visible = false;
                        ChartTopClient.DataSource = null;
                        ChartTopClient.DataBind();
                    }
                }
                else
                {
                    gvTopClient.DataBind();
                    divRevenueFromTopClient.Visible = false;
                    divtblRevenueToClient.Visible = false;
                    ChartTopClient.DataSource = null;
                    ChartTopClient.DataBind();
                    divLineTopClient.Visible = false;
                }
            }
            else
            {
                gvTopClient.DataBind();
                divRevenueFromTopClient.Visible = false;
                divtblRevenueToClient.Visible = false;
                ChartTopClient.DataSource = null;
                ChartTopClient.DataBind();
                divLineTopClient.Visible = false;
            }

        }

        void getMomChart()
        {
            MISData _data = new MISData();
            MISDto request = new MISDto();

            lblChartMom.Text = drpDivision.SelectedItem.Text;
            lbltblRevenueMom.Text = drpDivision.SelectedItem.Text;
            lblLineMom.Text = drpDivision.SelectedItem.Text;

            request.uniqueID = drpDivision.SelectedValue;
            request.mafinancialyear = drpFinancialYear.SelectedValue;
            DataSet ds = _data.getRevenueMOM(request);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];


                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("Mname");
                dtnew.Columns.Add("MOM");

                DataRow dr1 = null;
                foreach (DataColumn column in dt.Columns)
                {
                    dr1 = dtnew.NewRow();
                    string ColumnName = column.ColumnName.ToString();
                    dr1["Mname"] = ColumnName;
                    dtnew.Rows.Add(dr1);

                    dr1["MOM"] = dt.Rows[0][ColumnName].ToString();

                    if (column.ColumnName == "Mar")
                    {
                        break;
                    }
                }
                divChartMom.Visible = true;
                divtblMom.Visible = true;
                divLineMom.Visible = true;

                ChartMom.DataSource = dtnew;
                ChartMom.DataBind();

                LineChartMom.DataSource = dtnew;
                LineChartMom.DataBind();

                gvMom.DataSource = dt;
                gvMom.DataBind();
            }
            else
            {

                divChartMom.Visible = false;
                divtblMom.Visible = false;
                divLineMom.Visible = false;
            }
        }

        protected void lnkSearchButton_Click(object sender, EventArgs e)
        {
            if (!Searchvalidation())
            {
                return;
            }
            getdataMisOutstanding();
            getForcastRevenue();
            getRevenueVsCost();
            getTop10Client();
            getMomChart();
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
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

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
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
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    if (e.Row.Cells[1].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    if (e.Row.Cells[2].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    if (e.Row.Cells[3].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    if (e.Row.Cells[4].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    if (e.Row.Cells[5].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[6].Text != "&nbsp;")
                {
                    if (e.Row.Cells[6].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[7].Text != "&nbsp;")
                {
                    if (e.Row.Cells[7].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[8].Text != "&nbsp;")
                {
                    if (e.Row.Cells[8].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[9].Text != "&nbsp;")
                {
                    if (e.Row.Cells[9].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[10].Text != "&nbsp;")
                {
                    if (e.Row.Cells[10].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    if (e.Row.Cells[11].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[12].Text != "&nbsp;")
                {
                    if (e.Row.Cells[12].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                    }
                }



                Label gvlblDesc = (Label)e.Row.FindControl("gvlblDesc");
                if (gvlblDesc.Text == "OverDue")
                {
                    e.Row.Visible = false;
                }

            }
        }

        protected void gvRevenueVsForcast_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    if (e.Row.Cells[1].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    if (e.Row.Cells[2].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void gvRevenueVsCost_RowDataBound(object sender, GridViewRowEventArgs e)
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

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
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
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    if (e.Row.Cells[1].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    if (e.Row.Cells[2].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    if (e.Row.Cells[3].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    if (e.Row.Cells[4].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    if (e.Row.Cells[5].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[6].Text != "&nbsp;")
                {
                    if (e.Row.Cells[6].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[7].Text != "&nbsp;")
                {
                    if (e.Row.Cells[7].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[8].Text != "&nbsp;")
                {
                    if (e.Row.Cells[8].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[9].Text != "&nbsp;")
                {
                    if (e.Row.Cells[9].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[10].Text != "&nbsp;")
                {
                    if (e.Row.Cells[10].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    if (e.Row.Cells[11].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[12].Text != "&nbsp;")
                {
                    if (e.Row.Cells[12].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void gvTopClient_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                if (e.Row.Cells[1].Text.ToDouble() < 0)
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvMom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    //if (i == 0)
                    //{
                    //    e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    //}
                    //else
                    //{
                        e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                   // }

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.Cells[0].Text != "&nbsp;")
                {
                    if (e.Row.Cells[0].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    if (e.Row.Cells[1].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    if (e.Row.Cells[2].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    if (e.Row.Cells[3].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    if (e.Row.Cells[4].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    if (e.Row.Cells[5].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[6].Text != "&nbsp;")
                {
                    if (e.Row.Cells[6].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[7].Text != "&nbsp;")
                {
                    if (e.Row.Cells[7].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[8].Text != "&nbsp;")
                {
                    if (e.Row.Cells[8].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[9].Text != "&nbsp;")
                {
                    if (e.Row.Cells[9].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[10].Text != "&nbsp;")
                {
                    if (e.Row.Cells[10].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    if (e.Row.Cells[11].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (e.Row.Cells[12].Text != "&nbsp;")
                {
                    if (e.Row.Cells[12].Text.ToDouble() < 0)
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                    }
                }





            }
        }
    }
}