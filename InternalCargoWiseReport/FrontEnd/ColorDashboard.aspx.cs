using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data;

namespace InternalCargoWiseReport.FrontEnd
{
    public partial class ColorDashboard : System.Web.UI.Page
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
            DispatchData dt = new DispatchData();

            gvDashboard.DataSource = dt.GetColorDashboard();
            gvDashboard.DataBind();
        }

        protected void gvDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {

                Label gvlblEta = e.Row.FindControl("gvlblEta") as Label;
                Label gvlblETD = e.Row.FindControl("gvlblETD") as Label;
                Label gvlblCarrierDoDate = e.Row.FindControl("gvlblCarrierColor") as Label;
                Label gvblNotReleaseDate = e.Row.FindControl("gvlblCarrierBLNotReleseDateColor") as Label;
                if (gvlblEta.Text.ToLower()=="green")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                }
                else if (gvlblEta.Text.ToLower() == "red")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                }
                else if (gvlblEta.Text.ToLower() == "yellow")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                }
                if (gvlblETD.Text.ToLower() == "green")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                }
                else if (gvlblETD.Text.ToLower() == "red")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                }
                else if (gvlblETD.Text.ToLower() == "yellow")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                }

                if (gvlblCarrierDoDate.Text.ToLower()!=string.Empty)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.DarkRed;
                }

                if (gvblNotReleaseDate.Text.ToLower()!=string.Empty)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.DarkRed;
                }
            }
        }

        protected void gvDashboard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindGrid();
            gvDashboard.PageIndex = e.NewPageIndex;
            gvDashboard.DataBind();
        }
    }
}