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

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class GpDashboard : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.AddMonths(-1).ToString("MMM yyyy", CultureInfo.InvariantCulture);
                getRevenue();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }
        void getRevenue()
        {
            string Date = Convert.ToDateTime(txtDate.Text).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
            DateTime Month = (Convert.ToDateTime(txtDate.Text));
            string CurrentMonth = String.Format("{0:MMMM}", (Convert.ToDateTime(txtDate.Text)));
            string Year = (Convert.ToDateTime(txtDate.Text).Year).ToString();
            //lblYearlyHeading.Text  = CurrentMonth;
            lblMonthlyHeading.Text = CurrentMonth+"-" + Year;

            OutResult resultGP = InternalQuery.Query.Dashboard_GP(Date);
            if (resultGP.ex == null)
            {
                if (resultGP.ds != null)
                {
                    if (resultGP.ds.Tables[0].Rows.Count > 0)
                    {
                        dt = new DataTable();

                        dt = resultGP.ds.Tables[0];
                        double? TotReve = 0;
                        double? TotCost = 0;
                        double? TotGp = 0;
                        dt.Columns.Add("GP", typeof(string));
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["GP"] = dt.Rows[i]["ARValue"].ToDataConvertDouble() - dt.Rows[i]["APValue"].ToDataConvertDouble();
                            TotReve = TotReve + dt.Rows[i]["ARValue"].ToDataConvertDouble();
                            TotCost = TotCost + dt.Rows[i]["APValue"].ToDataConvertDouble();
                            TotGp = TotGp + dt.Rows[i]["GP"].ToDataConvertDouble();
                        }
                        dr["Name"] = "Total";
                        dr["ARValue"] = TotReve;
                        dr["APValue"] = TotCost;
                        dr["GP"] = TotGp;
                        
                        dt.Rows.InsertAt(dr, dt.Rows.Count + 1);
                        gvGpMonth.DataSource = dt;
                        gvGpMonth.DataBind();
                        gvGpMonth.Rows[gvGpMonth.Rows.Count - 1].Font.Bold = true;
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                        dlMonthlyList.DataSource = dt;
                        dlMonthlyList.DataBind();
                        Chart2.DataSource = dt;
                        Chart2.DataBind();


                        dt = new DataTable();
                       
                        double? yearlyTotReve = 0;
                        double? yearlyTotCost = 0;
                        double? yearlyTotGp = 0;
                        dt = resultGP.ds.Tables[1];
                        dt.Columns.Add("GP", typeof(string));
                        DataRow dr1 = dt.NewRow();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["GP"] = dt.Rows[i]["ARValue"].ToDataConvertDouble() - dt.Rows[i]["APValue"].ToDataConvertDouble();
                            yearlyTotReve = yearlyTotReve + dt.Rows[i]["ARValue"].ToDataConvertDouble();
                            yearlyTotCost = yearlyTotCost + dt.Rows[i]["APValue"].ToDataConvertDouble();
                            yearlyTotGp = yearlyTotGp + dt.Rows[i]["GP"].ToDataConvertDouble();
                        }
                        dr1["Name"] = "Total";
                        dr1["ARValue"] = yearlyTotReve;
                        dr1["APValue"] = yearlyTotCost;
                        dr1["GP"] = yearlyTotGp;
                        dt.Rows.InsertAt(dr1, dt.Rows.Count + 1);
                        gvGpYearly.DataSource = dt;
                        gvGpYearly.DataBind();
                        gvGpYearly.Rows[gvGpYearly.Rows.Count - 1].Font.Bold = true;
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                        dlRdbList.DataSource = dt;
                        dlRdbList.DataBind();
                        Chart1.DataSource = dt;
                        Chart1.DataBind();

                    }
                }
            }
        }

        protected void rdbTable_CheckedChanged(object sender, EventArgs e)
        {
            lblHeader.Text = "YTD (Figures in cr.)";
            mp1.Show();
            gvGpYearly.Visible = true;
            gvCompanyrevenue.Visible = false;
            dvTableMonth.Visible = false;
        }
        protected void rpCompanyName_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Check")
            {
                int value = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            rdbTable.Checked = false;
            rdbMothlyTableFormat.Checked = false;
            gvGpYearly.Visible = false;
            gvCompanyrevenue.Visible = false;
            dvTableMonth.Visible = false;
            foreach (DataListItem item in dlRdbList.Items)
            {
                RadioButton chkbox = (RadioButton)item.FindControl("rdbCompanyName");
                chkbox.Checked = false;
            }
            foreach (DataListItem item in dlMonthlyList.Items)
            {
                RadioButton chkbox = (RadioButton)item.FindControl("rdbMonthlyCompanyName");
                chkbox.Checked = false;
            }


            mp1.Hide();

        }

        protected void rdbCompanyName_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = (RadioButton)sender;
            string Date = Convert.ToDateTime(txtDate.Text).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
            string rdbtext = rdb.Text;
            if(rdb.Text=="ASC")
            {
                rdbtext = "AFL";
            }
            OutResult resultGP = InternalQuery.Query.Dashboard_AllInOneGP(Date, rdbtext);

            if (resultGP.ex == null)
            {
                if (resultGP.ds != null)
                {
                    if (resultGP.ds.Tables[1].Rows.Count > 0)
                    {
                        dt = new DataTable();
                        double? TotReve = 0;
                        double? TotCost = 0;
                        double? TotGp = 0;
                        dt = resultGP.ds.Tables[1];

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //dt.Rows[i]["GP"] = dt.Rows[i]["ARValue"].ToDataConvertDouble() - dt.Rows[i]["APValue"].ToDataConvertDouble();
                            TotReve = TotReve + dt.Rows[i]["Revenue"].ToDataConvertDouble();
                            TotCost = TotCost + dt.Rows[i]["Cost"].ToDataConvertDouble();
                            TotGp = TotGp + dt.Rows[i]["GP"].ToDataConvertDouble();
                        }
                        dr["Department"] = "Total";
                        dr["Revenue"] = TotReve;
                        dr["Cost"] = TotCost;
                        dr["GP"] = TotGp;
                        dt.Rows.InsertAt(dr, dt.Rows.Count+1); 
                        gvCompanyrevenue.DataSource = dt;
                        gvCompanyrevenue.DataBind();
                        gvCompanyrevenue.Rows[gvCompanyrevenue.Rows.Count - 1].Font.Bold = true;
        


                        lblHeader.Text = rdb.Text + ": YTD (Figures in cr.)";
                        mp1.Show();
                        gvCompanyrevenue.Visible = true;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            getRevenue();
        }

        protected void rdbMonthlyCompanyName_CheckedChanged1(object sender, EventArgs e)
        {
            RadioButton rdb = (RadioButton)sender;
            string Date = Convert.ToDateTime(txtDate.Text).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
            string rdbtext = rdb.Text;
            if (rdb.Text == "ASC")
            {
                rdbtext = "AFL";
            }
            OutResult resultGP = InternalQuery.Query.Dashboard_AllInOneGP(Date, rdbtext);

            if (resultGP.ex == null)
            {
                if (resultGP.ds != null)
                {
                    if (resultGP.ds.Tables[0].Rows.Count > 0)
                    {
                        dt = new DataTable();
                        double? TotReve = 0;
                        double? TotCost = 0;
                        double? TotGp = 0;
                        dt = resultGP.ds.Tables[0];
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //dt.Rows[i]["GP"] = dt.Rows[i]["ARValue"].ToDataConvertDouble() - dt.Rows[i]["APValue"].ToDataConvertDouble();
                            TotReve = TotReve + dt.Rows[i]["Revenue"].ToDataConvertDouble();
                            TotCost = TotCost + dt.Rows[i]["Cost"].ToDataConvertDouble();
                            TotGp = TotGp + dt.Rows[i]["GP"].ToDataConvertDouble();
                        }
                        dr["Department"] = "Total";
                        dr["Revenue"] = TotReve;
                        dr["Cost"] = TotCost;
                        dr["GP"] = TotGp;
                        dt.Rows.InsertAt(dr, dt.Rows.Count + 1);
                        gvCompanyrevenue.DataSource = dt;
                        gvCompanyrevenue.DataBind();
                        gvCompanyrevenue.Rows[gvCompanyrevenue.Rows.Count - 1].Font.Bold = true;

                        lblHeader.Text = rdb.Text + ": Monthly (Figures in cr.)";
                        mp1.Show();
                        gvCompanyrevenue.Visible = true;
                    }
                }
            }
        }

        protected void rdbMothlyTableFormat_CheckedChanged(object sender, EventArgs e)
        {
            mp1.Show();
            lblHeader.Text = "Current Month (Figures in cr.)";
            gvGpYearly.Visible = false;
            gvCompanyrevenue.Visible = false;
            dvTableMonth.Visible = true;
        }
    }
}