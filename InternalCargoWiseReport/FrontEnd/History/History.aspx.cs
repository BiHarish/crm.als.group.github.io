using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.History
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Audit_Trail_Log.InsertAuditTrailLog("CRM HISTORY", "Visit", "Mr/Mrs: " + LovelySession.Lovely.User.Name + " successfully visit on CRM History Page On " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
                bindDrp();
            }
        }

        void bindDrp()
        {
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("SCS");
            if (customerResults != null)
            {
                drpCustomerName.DataSource = customerResults;
                drpCustomerName.DataValueField = "ID";
                drpCustomerName.DataTextField = "Name";
                drpCustomerName.DataBind();
                drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        void bindgrd()
        {
            WHLeadMasterData _data = new WHLeadMasterData();
            DataSet results = _data.getResultForHistory(drpCustomerName.SelectedValue);
            if (results != null)
            {
                try
                {
                    gvLeadList.DataSource = results.Tables[0];
                    gvLeadList.DataBind();

                    if(results.Tables[1]!=null && results.Tables[1].Rows.Count>0)
                    {
                        gvContact.DataSource = results.Tables[1];
                        gvContact.DataBind();
                    }
                    else
                    {
                        gvContact.DataBind();
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Server Issue !!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }

            }
            else
            {
                gvLeadList.DataBind();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "No Record found with this Criteria !!", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Audit_Trail_Log.InsertAuditTrailLog("CRM HISTORY", "Search", "Mr/Mrs: " + LovelySession.Lovely.User.Name + "Search History of "+drpCustomerName.SelectedItem.Text +"On:  " + DateTime.Now.Date.ToString("dd/MMM/yyyy"));
            if(drpCustomerName.SelectedValue==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please select Customer !!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                gvLeadList.DataBind();
                return;
            }
            bindgrd();

        }

        protected void gvLeadList_DataBound(object sender, EventArgs e)
        {
            for (int i = gvLeadList.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvLeadList.Rows[i];
                GridViewRow previousRow = gvLeadList.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }

        protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrd();
            gvLeadList.PageIndex = e.NewPageIndex;

        }

        protected void gvContact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}