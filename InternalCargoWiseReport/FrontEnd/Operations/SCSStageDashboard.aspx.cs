using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class SCSStageDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindGrid();
            }
        }

        void bindGrid()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getSCSStageDashboardData();
            txtRecordFound.Text = "0";
            if(results!=null)
            {
                txtRecordFound.Text = results.Count.ToString();
                gvScsStageDashboard.DataSource = results;
                gvScsStageDashboard.DataBind();
            }
        }

        protected void gvScsStageDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.Header)
            {
                Label gvlblAsOn1 = (e.Row.FindControl("gvlblStageAsOn1") as Label);
                Label gvlblAsOn2 = (e.Row.FindControl("gvlblStageAsOn2") as Label);
                Label gvlblAsOn3 = (e.Row.FindControl("gvlblStageAsOn3") as Label);


                gvlblAsOn1.Text = DateTime.Now.ToString("dd MMM yyyy");
                gvlblAsOn2.Text = DateTime.Now.AddDays(-7).ToString("dd MMM yyyy");
                gvlblAsOn3.Text = DateTime.Now.AddDays(-14).ToString("dd MMM yyyy");

            }
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                Label gvlblStatus = (e.Row.FindControl("gvlblStatus") as Label);
                Label gvlblIsGreen = (e.Row.FindControl("gvlblIsGreen") as Label);

                int NoOfDays = gvlblStatus.Text.ToConvertInt();

                //if(NoOfDays<=7)
                //{
                //    e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("Green");
                //}
                if (NoOfDays>7 && NoOfDays<14)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("#ff91c0");
                }
                else if (NoOfDays>=14)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("Red");
                }
                if (gvlblIsGreen.Text == "True" || gvlblIsGreen.Text == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("Green");
                }
            }
        }
    }
}