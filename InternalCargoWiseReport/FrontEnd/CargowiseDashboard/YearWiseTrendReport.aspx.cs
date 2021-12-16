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
    public partial class YearWiseTrendReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getData();
            }
        }
        void getData()
        {
            DataTable dt = new DataTable();
            OutResult result = InternalQuery.Query.YearWiseTrendReport();
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                         dt = new DataTable();

                        dt = result.ds.Tables[0];
                        txtFirstFinYear.Text = dt.Rows[0]["FinYear"].ToString();
                        dt.Columns.Remove("FinYear");
                        dt.Columns.Remove("Months");
                        dt.Columns.Remove("Sno");
                        dt.AcceptChanges();

                        Chart1YearWiseTrend.DataSource = dt;
                        Chart1YearWiseTrend.DataBind();
                    }
                    else
                    {
                         dt = new DataTable();
                        Chart1YearWiseTrend.DataSource = dt;
                        Chart1YearWiseTrend.DataBind();
                    }

                    if (result.ds.Tables[1].Rows.Count > 0)
                    {
                        dt = new DataTable();

                        dt = result.ds.Tables[1];
                        txtSecFinYear.Text = dt.Rows[0]["FinYear"].ToString();
                        dt.Columns.Remove("FinYear");
                        dt.Columns.Remove("Months");
                        dt.Columns.Remove("Sno");
                        dt.AcceptChanges();

                        Chart2YearWiseTrend.DataSource = dt;
                        Chart2YearWiseTrend.DataBind();
                    }
                    else
                    {
                        dt = new DataTable();
                        Chart2YearWiseTrend.DataSource = dt;
                        Chart2YearWiseTrend.DataBind();
                    }

                    if (result.ds.Tables[2].Rows.Count > 0)
                    {
                        dt = new DataTable();

                        dt = result.ds.Tables[2];
                        txtThirdFinYear.Text = dt.Rows[0]["FinYear"].ToString();
                        dt.Columns.Remove("FinYear");
                        dt.Columns.Remove("Months");
                        dt.Columns.Remove("Sno");
                        dt.AcceptChanges();

                        Chart3YearWiseTrend.DataSource = dt;
                        Chart3YearWiseTrend.DataBind();
                    }
                    else
                    {
                        dt = new DataTable();
                        Chart3YearWiseTrend.DataSource = dt;
                        Chart3YearWiseTrend.DataBind();
                    }
                    if (result.ds.Tables[3].Rows.Count > 0)
                    {
                        dt = new DataTable();

                        dt = result.ds.Tables[3];
                        txtFourthFinYear.Text = dt.Rows[0]["FinYear"].ToString();
                        dt.Columns.Remove("FinYear");
                        dt.Columns.Remove("Months");
                        dt.Columns.Remove("Sno");
                        dt.AcceptChanges();

                        Chart4YearWiseTrend.DataSource = dt;
                        Chart4YearWiseTrend.DataBind();
                    }
                    else
                    {
                        dt = new DataTable();
                        Chart4YearWiseTrend.DataSource = dt;
                        Chart4YearWiseTrend.DataBind();
                    }
                    //if (result.ds.Tables[4].Rows.Count > 0)
                    //{
                    //    dt = new DataTable();

                    //    dt = result.ds.Tables[4];
                    //    txtFifthFinYear.Text = dt.Rows[0]["FinYear"].ToString();
                    //    dt.Columns.Remove("FinYear");
                    //    dt.Columns.Remove("Months");
                    //    dt.Columns.Remove("Sno");
                    //    dt.AcceptChanges();

                    //    Chart5YearWiseTrend.DataSource = dt;
                    //    Chart5YearWiseTrend.DataBind();
                    //}
                    //else
                    //{
                    //    dt = new DataTable();
                    //    Chart5YearWiseTrend.DataSource = dt;
                    //    Chart5YearWiseTrend.DataBind();
                    //}

                }
                else
                {
                     dt = new DataTable();
                    Chart1YearWiseTrend.DataSource = dt;
                    Chart1YearWiseTrend.DataBind();
                    Chart2YearWiseTrend.DataSource = dt;
                    Chart2YearWiseTrend.DataBind();
                    Chart3YearWiseTrend.DataSource = dt;
                    Chart3YearWiseTrend.DataBind();
                    Chart4YearWiseTrend.DataSource = dt;
                    Chart4YearWiseTrend.DataBind();
                    Chart5YearWiseTrend.DataSource = dt;
                    Chart5YearWiseTrend.DataBind();
                }
            }
            else
            {
                 dt = new DataTable();
                Chart1YearWiseTrend.DataSource = dt;
                Chart1YearWiseTrend.DataBind();
                Chart2YearWiseTrend.DataSource = dt;
                Chart2YearWiseTrend.DataBind();
                Chart3YearWiseTrend.DataSource = dt;
                Chart3YearWiseTrend.DataBind();
                Chart4YearWiseTrend.DataSource = dt;
                Chart4YearWiseTrend.DataBind();
                Chart5YearWiseTrend.DataSource = dt;
                Chart5YearWiseTrend.DataBind();
            }
        }
    }
}