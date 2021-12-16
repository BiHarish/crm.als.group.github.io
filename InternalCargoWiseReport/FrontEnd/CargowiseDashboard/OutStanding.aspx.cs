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
using InternalQuery;
using System.Globalization;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class OutStanding : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                getValue();
            }
        }
        void getValue()
        {
            OutResult result = InternalQuery.Query.Dashboard_UnallocatedRec(txtDate.Text);
            if (result.ex == null)
            {
                if (result.ds != null)
                {
                    if (result.ds.Tables[0].Rows.Count > 0)
                    {
                        dt = new DataTable();
                        dt = result.ds.Tables[0];

                        gvGrd.DataSource = dt;
                        gvGrd.DataBind();
                        
                    }
                }
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            getValue();
        }
    }
}