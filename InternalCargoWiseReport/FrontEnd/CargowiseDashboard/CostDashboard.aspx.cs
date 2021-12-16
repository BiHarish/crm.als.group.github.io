﻿using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using InternalQuery;
using System.Globalization;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class CostDashboard : System.Web.UI.Page
    {
        string MonthWise;
        string YearWise;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("MMM yyyy", CultureInfo.InvariantCulture);
                getCost();

                lblYearWiseChart.Text = "YTD " + ExtensionMethods.GetFinancialYear(txtDate.Text);
                lblYearWiseTable.Text = "YTD " + ExtensionMethods.GetFinancialYear(txtDate.Text);

                lblMonthWiseChart.Text = txtDate.Text.ToConvertDateTime().Value.ToString("MMMM") + " (" + Convert.ToDateTime(txtDate.Text).Year + ")";
                lblMonthWiseTable.Text = txtDate.Text.ToConvertDateTime().Value.ToString("MMMM") + " (" + Convert.ToDateTime(txtDate.Text).Year + ")";

            }
        }

        void getCost()
        {
            lblYearWiseChart.Text = string.Empty;
            lblYearWiseTable.Text = string.Empty;
            lblMonthWiseChart.Text = string.Empty;
            lblMonthWiseTable.Text = string.Empty;

            string Date = Convert.ToDateTime(txtDate.Text).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);

            OutResult result = InternalQuery.Query.Dashboard_BillCost(Date, "AP", "INV");
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                        dt = new DataTable();
                        dt = result.ds.Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            MonthWise = MonthWise + "{ Name: '" + dt.Rows[i]["Name"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                        }
                        ltrMonthWise.Text = MonthWise;
                        gvListMonthWise.DataSource = dt;
                        gvListMonthWise.DataBind();

                        lblMonthWiseChart.Text = txtDate.Text.ToConvertDateTime().Value.ToString("MMMM") + " (" + Convert.ToDateTime(txtDate.Text).Year + ")";
                        lblMonthWiseTable.Text = txtDate.Text.ToConvertDateTime().Value.ToString("MMMM") + " (" + Convert.ToDateTime(txtDate.Text).Year + ")";


                        dt = new DataTable();
                        dt = result.ds.Tables[1];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            YearWise = YearWise + "{ Name: '" + dt.Rows[i]["Name"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                        }
                        ltrYearWise.Text = YearWise;
                        gvListYearWise.DataSource = dt;
                        gvListYearWise.DataBind();

                        lblYearWiseChart.Text = "YTD " + ExtensionMethods.GetFinancialYear(txtDate.Text);
                        lblYearWiseTable.Text = "YTD " + ExtensionMethods.GetFinancialYear(txtDate.Text);
                    }
                    else
                    {
                        ltrMonthWise.Text = string.Empty;
                        ltrYearWise.Text = string.Empty;
                        lblYearWiseChart.Text = string.Empty;
                        lblYearWiseTable.Text = string.Empty;
                        lblMonthWiseChart.Text = string.Empty;
                        lblMonthWiseTable.Text = string.Empty;

                        gvListYearWise.DataBind();
                        gvListMonthWise.DataBind();
                    }
                }
                else
                {
                    ltrMonthWise.Text = string.Empty;
                    ltrYearWise.Text = string.Empty;
                    lblYearWiseChart.Text = string.Empty;
                    lblYearWiseTable.Text = string.Empty;
                    lblMonthWiseChart.Text = string.Empty;
                    lblMonthWiseTable.Text = string.Empty;

                    gvListYearWise.DataBind();
                    gvListMonthWise.DataBind();
                }
            }
            else
            {
                ltrMonthWise.Text = string.Empty;
                ltrYearWise.Text = string.Empty;
                lblYearWiseChart.Text = string.Empty;
                lblYearWiseTable.Text = string.Empty;
                lblMonthWiseChart.Text = string.Empty;
                lblMonthWiseTable.Text = string.Empty;

                gvListYearWise.DataBind();
                gvListMonthWise.DataBind();
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            getCost();
        }
    }
}