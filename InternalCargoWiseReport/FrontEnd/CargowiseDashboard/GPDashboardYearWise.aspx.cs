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
    public partial class GPDashboardYearWise : System.Web.UI.Page
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
            CityData _data = new CityData();
            IList<GlbCompanyDto> results = _data.GetAllGlbCompany();
            if (results != null)
            {
                drpCompanyCode.DataSource = results;
                drpCompanyCode.DataValueField = "GC_Code";
                drpCompanyCode.DataTextField = "GC_Code";
                drpCompanyCode.DataBind();
                // drpCompanyCode.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

          void getData()
        {
            OutResult result = InternalQuery.Query.FinancialWiseDashboard(drpCompanyCode.SelectedValue,drpYear.SelectedValue);
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        dt = result.ds.Tables[0];
                       // dt.Columns.Remove("Name");
                       // dt.Columns.Remove("Months");
                       // dt.Columns.Remove("Sno");
                       // dt.AcceptChanges();

                        ChartFinYearWise.DataSource = dt;
                        ChartFinYearWise.DataBind();
                        //ChartFinYearWise.Legends["Legend1"].Position.Auto = false;
                        //ChartFinYearWise.Legends["Legend1"].Docking = Docking.Bottom;
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        ChartFinYearWise.DataSource = dt;
                        ChartFinYearWise.DataBind();
                    }
                }
                else
                {
                    DataTable dt = new DataTable();
                    ChartFinYearWise.DataSource = dt;
                    ChartFinYearWise.DataBind();
                }
            }
            else
            {
                DataTable dt = new DataTable();
                ChartFinYearWise.DataSource = dt;
                ChartFinYearWise.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            getData();
        }
    }
}