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
using ICWR.Data.Utility;
using ICWR.Models;
using ICWR.Data;
using System.Web.UI.DataVisualization.Charting;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{

    public partial class MisBudgetDashboard : System.Web.UI.Page
    {
        DataSet ds = null;
        DataTable dt = new DataTable();
        MisBudgetData _data = null;
        MisBudgetDto request = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    BindDrp();
                    getData();
                }

            }
        }
        void BindDrp()
        {
            ds = new DataSet();
            MISDto mis = new MISDto();
            MISData misdata = new MISData();
            ds = misdata.BindDivision(mis);
            Drpdivisin.DataSource = ds;
            Drpdivisin.DataTextField = "mddesc";
            Drpdivisin.DataValueField = "mdid";
            Drpdivisin.DataBind();
            Drpdivisin.Items.Insert(0, new ListItem("---Select--", string.Empty));


            ds = misdata.BindSubDivision(mis);
            Drpafildivision.DataSource = ds;
            Drpafildivision.DataTextField = "mddesc";
            Drpafildivision.DataValueField = "mdid";
            Drpafildivision.DataBind();
            Drpafildivision.Items.Insert(0, new ListItem("---Select--", string.Empty));
        }
        protected void Drpdivisin_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drpdivisin.SelectedItem.Text == "AFIL")
            {
                Drpafildivision.Visible = true;
                lblsubdivision.Visible = true;
            }
            else
            {
                Drpafildivision.Visible = false;
                lblsubdivision.Visible = false;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            getData();
        }
        void getData()
        {
            DataTable dt = new DataTable();
            _data = new MisBudgetData();
            request = new MisBudgetDto();
            ds = new DataSet();
            request.mbfinancialyear = DrpFYear.SelectedItem.Text.ToString();

            if (Drpdivisin.SelectedItem.Text == "AFIL")
            {
                request.mbdivisionid = Drpafildivision.SelectedValue.ToConvertNullInt();
            }
            else
            {
                request.mbdivisionid = Drpdivisin.SelectedValue.ToConvertNullInt();
            }
            ds = _data.GetAll(request.mbfinancialyear, request.mbdivisionid);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = new DataTable();

                    dt = ds.Tables[0];
                    txtFirstFinYear.Text = dt.Rows[0]["financialYear"].ToString();
                    dt.Columns.Remove("financialYear");
                    dt.AcceptChanges();

                    ChartMisBudget.DataSource = dt;
                    ChartMisBudget.DataBind();
                }
                else
                {
                    dt = new DataTable();
                    ChartMisBudget.DataSource = dt;
                    ChartMisBudget.DataBind();
                }
            }
            else
            {
                dt = new DataTable();
                ChartMisBudget.DataSource = dt;
                ChartMisBudget.DataBind();
            }
        }
    }
}