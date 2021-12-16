using InternalQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using ICWR.Data;
using ICWR.Models;
using System.Web.UI.DataVisualization.Charting;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class GPCompanyWiseTrendReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDrp();
                
                getData();
            }
        }
        void bindDrp()
        {
            OutResult resultGP = InternalQuery.Query.getFinYear();
            if (resultGP.ex == null)
            {
                if (resultGP.ds != null)
                {
                    if (resultGP.ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        dt = resultGP.ds.Tables[0];

                        drpFinYear.DataSource = dt;
                        drpFinYear.DataValueField = "FinYear";
                        drpFinYear.DataTextField = "FinYear";
                        drpFinYear.DataBind();

                        drpFinYear.SelectedValue = "2019-2020";

                    }
                }
            }
        }
        void getData()
        {
            DataTable dt = new DataTable();
            OutResult result = InternalQuery.Query.CompanyWiseTrend(drpFinYear.SelectedValue);
            if (result.ex == null)
            {
                
                if (result.ds != null)
                {
                    if (result.ds.Tables.Count > 0)
                    {
                        if (result.ds.Tables[0].Rows.Count > 0)
                        {
                            dt = new DataTable();

                            dt = result.ds.Tables[0];
                            txtFirstCompany.Text = dt.Rows[0]["GC_Code"].ToString();
                            dt.Columns.Remove("FinYear");
                            dt.Columns.Remove("GC_Code");
                            dt.Columns.Remove("Months");
                            dt.Columns.Remove("Sno");
                            dt.AcceptChanges();

                            Chart1CompanyWiseTrend.DataSource = dt;
                            Chart1CompanyWiseTrend.DataBind();

                            div1.Visible = true;
                        }
                        else
                        {
                            dt = new DataTable();
                            Chart1CompanyWiseTrend.DataSource = dt;
                            Chart1CompanyWiseTrend.DataBind();
                        }
                    }
                    else
                    {
                        div1.Visible = false;
                    }
                    if (result.ds.Tables.Count > 1)
                    {
                        if (result.ds.Tables[1].Rows.Count > 0)
                        {
                            dt = new DataTable();

                            dt = result.ds.Tables[1];
                            txtSecCompany.Text = dt.Rows[0]["GC_Code"].ToString();
                            dt.Columns.Remove("FinYear");
                            dt.Columns.Remove("GC_Code");
                            dt.Columns.Remove("Months");
                            dt.Columns.Remove("Sno");
                            dt.AcceptChanges();

                            Chart2CompanyWiseTrend.DataSource = dt;
                            Chart2CompanyWiseTrend.DataBind();

                            div2.Visible = true;
                        }
                        else
                        {
                            dt = new DataTable();
                            Chart2CompanyWiseTrend.DataSource = dt;
                            Chart2CompanyWiseTrend.DataBind();
                        }
                    }
                    else
                    {
                        div2.Visible = false;
                    }

                    if (result.ds.Tables.Count > 2)
                    {
                        if (result.ds.Tables[2].Rows.Count > 0)
                        {
                            dt = new DataTable();

                            dt = result.ds.Tables[2];
                            txtThirdCompany.Text = dt.Rows[0]["GC_Code"].ToString();
                            dt.Columns.Remove("FinYear");
                            dt.Columns.Remove("GC_Code");
                            dt.Columns.Remove("Months");
                            dt.Columns.Remove("Sno");
                            dt.AcceptChanges();

                            Chart3CompanyWiseTrend.DataSource = dt;
                            Chart3CompanyWiseTrend.DataBind();

                            div3.Visible = true;
                        }
                        else
                        {
                            dt = new DataTable();
                            Chart3CompanyWiseTrend.DataSource = dt;
                            Chart3CompanyWiseTrend.DataBind();
                        }
                    }
                    else
                    {
                        div3.Visible = false;
                    }
                    if (result.ds.Tables.Count > 3)
                    {
                        if (result.ds.Tables[3].Rows.Count > 0)
                        {
                            dt = new DataTable();

                            dt = result.ds.Tables[3];
                            txtFourthCompany.Text = dt.Rows[0]["GC_Code"].ToString();
                            dt.Columns.Remove("FinYear");
                            dt.Columns.Remove("GC_Code");
                            dt.Columns.Remove("Months");
                            dt.Columns.Remove("Sno");
                            dt.AcceptChanges();

                            Chart4CompanyWiseTrend.DataSource = dt;
                            Chart4CompanyWiseTrend.DataBind();

                            div4.Visible = true;
                        }
                        else
                        {
                            dt = new DataTable();
                            Chart4CompanyWiseTrend.DataSource = dt;
                            Chart4CompanyWiseTrend.DataBind();
                        }
                    }
                    else
                    {
                        div4.Visible = false;
                    }
                    if (result.ds.Tables.Count > 4)
                    {
                        if (result.ds.Tables[4].Rows.Count > 0)
                        {
                            dt = new DataTable();

                            dt = result.ds.Tables[4];
                            txtFifthCompany.Text = dt.Rows[0]["GC_Code"].ToString();
                            dt.Columns.Remove("FinYear");
                            dt.Columns.Remove("GC_Code");
                            dt.Columns.Remove("Months");
                            dt.Columns.Remove("Sno");
                            dt.AcceptChanges();

                            Chart5CompanyWiseTrend.DataSource = dt;
                            Chart5CompanyWiseTrend.DataBind();

                            div5.Visible = true;
                        }
                        else
                        {
                            dt = new DataTable();
                            Chart5CompanyWiseTrend.DataSource = dt;
                            Chart5CompanyWiseTrend.DataBind();
                        }
                    }
                    else
                    {
                        div5.Visible = false;
                    }

                }
                else
                {
                    dt = new DataTable();
                    Chart1CompanyWiseTrend.DataSource = dt;
                    Chart1CompanyWiseTrend.DataBind();
                    Chart2CompanyWiseTrend.DataSource = dt;
                    Chart2CompanyWiseTrend.DataBind();
                    Chart3CompanyWiseTrend.DataSource = dt;
                    Chart3CompanyWiseTrend.DataBind();
                    Chart4CompanyWiseTrend.DataSource = dt;
                    Chart4CompanyWiseTrend.DataBind();
                    Chart5CompanyWiseTrend.DataSource = dt;
                    Chart5CompanyWiseTrend.DataBind();
                }
            }
            else
            {
                dt = new DataTable();
                Chart1CompanyWiseTrend.DataSource = dt;
                Chart1CompanyWiseTrend.DataBind();
                Chart2CompanyWiseTrend.DataSource = dt;
                Chart2CompanyWiseTrend.DataBind();
                Chart3CompanyWiseTrend.DataSource = dt;
                Chart3CompanyWiseTrend.DataBind();
                Chart4CompanyWiseTrend.DataSource = dt;
                Chart4CompanyWiseTrend.DataBind();
                Chart5CompanyWiseTrend.DataSource = dt;
                Chart5CompanyWiseTrend.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            getData();
        }
    }
}