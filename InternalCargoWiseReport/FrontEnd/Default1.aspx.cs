using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;

namespace ICWR.FrontEnd
{
    public partial class Default1 : System.Web.UI.Page
    {
        DataTable gd = new DataTable();
        DataTable gt = new DataTable();

        #region Properties
        string HdfUserLoginCountryCode { get { return hdfUserLogin.Value; } set { hdfUserLogin.Value = value; } }
        string HdfCompanyCode { get { return hdfCompanyCode.Value; } set { hdfCompanyCode.Value = value; } }
        string HdfBranchCode { get { return hdfBranchCode.Value; } set { hdfBranchCode.Value = value; } }
        string HdfCommanArgument { get { return HdfCommandAtr.Value; } set { HdfCommandAtr.Value = value; } }

        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely != null)
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                Response.Redirect("/FrontEnd/SignIn.aspx?RequestUrl=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }
        
        private void BindData()
        {
            DashboardData _dashboardData = new DashboardData();

            DataTable dt2 = _dashboardData.GetAllDashBoard();
            if (dt2 != null)
            {
                gvDashboard.DataSource = dt2;
                gvDashboard.DataBind();
            }


            DataTable dt = _dashboardData.GetCountUserLogined(true);
            if (dt != null)
            {
                rptUserLogin.DataSource = dt;
                rptUserLogin.DataBind();
            }



            DataTable dt1 = _dashboardData.GetCountUserLogined(false);
            if (dt != null)
            {
                rptSalesUsers.DataSource = dt1;
                rptSalesUsers.DataBind();
            }
        }
        #endregion
        void BindUserLogin(bool IsLogin)
        {
            if (!string.IsNullOrEmpty(HdfUserLoginCountryCode))
            {
                DashboardData _dashboardData = new DashboardData();
                DataTable dt = _dashboardData.GetUserLoginedDetail(HdfUserLoginCountryCode, IsLogin);
                if (dt != null)
                {
                    gvLoginUsers.DataSource = dt;
                    gvLoginUsers.DataBind();

                    mp1.Show();
                }
                else
                {
                    gvLoginUsers.DataSource = null;
                    gvLoginUsers.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "We Are Checking. Contact Maintance Team", "Warning!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        void BindDashboard(string CompanyCode, string branchCode, int flag)
        {
            if (!string.IsNullOrEmpty(CompanyCode) && !string.IsNullOrEmpty(branchCode))
            {
                DashboardData _dashboardData = new DashboardData();
                DataSet ds = _dashboardData.GetDashBoardByFlag(CompanyCode, branchCode, flag);
                gd = ds.Tables[0];

                if (ds.Tables[0] != null)
                {

                    gvLoginUsers.DataSource = ds.Tables[0];
                    gvLoginUsers.DataBind();

                    if (ds.Tables[1].Rows[0]["AirGreen"].ToString() != string.Empty)
                    {
                        lblAirGreen.Text = ds.Tables[1].Rows[0]["AirGreen"].ToString();
                        txtAirGreen.Visible = true;
                    }
                    else
                    {
                        lblAirGreen.Text = string.Empty;
                        txtAirGreen.Visible = false;
                    }
                    
                    if (ds.Tables[1].Rows[0]["AirYellow"].ToString() != string.Empty)
                    {
                        lblAirYellow.Text = ds.Tables[1].Rows[0]["AirYellow"].ToString();
                        txtAirYellow.Visible = true;
                    }
                    else
                    {
                        lblAirYellow.Text = string.Empty;
                        txtAirYellow.Visible = false;
                    }
                    if (ds.Tables[1].Rows[0]["AirRed"].ToString() != string.Empty)
                    {
                        lblAirRed.Text = ds.Tables[1].Rows[0]["AirRed"].ToString();
                        txtAirRed.Visible = true;
                    }
                    else
                    {
                        txtAirRed.Visible = false;
                        lblAirRed.Text = string.Empty;
                    }
                    if (ds.Tables[1].Rows[0]["AirDarkRed"].ToString() != string.Empty)
                    {
                        lblAirDarkRed.Text = ds.Tables[1].Rows[0]["AirDarkRed"].ToString();
                        txtAirDarkRed.Visible = true;
                    }
                    else
                    {
                        txtAirDarkRed.Visible = false;
                        lblAirDarkRed.Text = string.Empty;
                    }

                    if (ds.Tables[1].Rows[0]["SeaGreen"].ToString() != string.Empty)
                    {
                        lblSeaGreen.Text = ds.Tables[1].Rows[0]["SeaGreen"].ToString();
                        txtSeaGreen.Visible = true;
                    }
                    else
                    {
                        txtSeaGreen.Visible = false;
                        lblSeaGreen.Text = string.Empty;
                    }
                    if (ds.Tables[1].Rows[0]["SeaYellow"].ToString() != string.Empty)
                    {
                        lblSeaYellow.Text = ds.Tables[1].Rows[0]["SeaYellow"].ToString();
                        txtSeaYellow.Visible = true;
                    }
                    else
                    {
                        txtSeaYellow.Visible = false;
                        lblSeaYellow.Text = string.Empty;
                    }
                       
                    if (ds.Tables[1].Rows[0]["SeaRed"].ToString() != string.Empty)
                    {
                        lblSeaRed.Text = ds.Tables[1].Rows[0]["SeaRed"].ToString();
                        txtSeaRed.Visible = true;
                    }
                    else
                    {
                        txtSeaRed.Visible = false;
                        lblSeaRed.Text = string.Empty;
                    }
                    if (ds.Tables[1].Rows[0]["SeaDarkRed"].ToString() != string.Empty)
                    {
                        lblSeaDarkRed.Text = ds.Tables[1].Rows[0]["SeaDarkRed"].ToString();
                        txtSeaDarkRed.Visible = true;
                    }
                    else
                    {
                        txtSeaDarkRed.Visible = false;
                        lblSeaDarkRed.Text = string.Empty;
                    }

                    mp1.Show();


                }
                else
                {
                    gvLoginUsers.DataSource = null;
                    gvLoginUsers.DataBind();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "We Are Checking. Contact Maintance Team", "Warning!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        protected void lnkUserLoginMoreInfo_Command(object sender, CommandEventArgs e)
        {
            tbl.Visible = false;
            dv.Visible = false;
            gvLoginUsers.PageIndex = 0;
            string data = e.CommandArgument.ToString();
            HdfUserLoginCountryCode = data;
            BindUserLogin(true);
            HdfCommanArgument = "LoginMore";
        }
        protected void lnkSalesLoginMoreInfo_Command(object sender, CommandEventArgs e)
        {
            gvLoginUsers.PageIndex = 0;
            string data = e.CommandArgument.ToString();
            HdfUserLoginCountryCode = data;
            BindUserLogin(false);
            HdfCommanArgument = "SalesLoginMore";
        }
        protected void gvLoginUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (HdfCommanArgument.Equals("JobNotOpen"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.JobPendingForOpening);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("LoginMore"))
            {
                BindUserLogin(true);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("SalesLoginMore"))
            {
                BindUserLogin(true);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("ATDETDNotUpdated"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ATDATANotUpdated);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("BLNOTReleased"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotReleased);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("BLNOTCreated"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotCreated);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTReleased"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotReleased);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTCreated"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotCreated);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("COSTNOTBOOKED"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.CostNotBooked);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("REVENUENOTBOOKED"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.RevenueNotBooked);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("UNBILLED"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.Unbilled);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("ShipPEndingForOpening"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ShipmentPendingForClosure);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("DLNOTToDate"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotCreated);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("DLNOTReleased"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotReleased);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("SIPENDINGS"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.SIPending);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("IGMNO"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMNo);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("IGMDT"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMFillingDT);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("InvoiceDetails"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.InvoiceDispatchDetails);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
            else if (HdfCommanArgument.Equals("VGNPending"))
            {
                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.VGMPending);
                gvLoginUsers.PageIndex = e.NewPageIndex;
                gvLoginUsers.DataBind();
            }
        }
        protected void lnkJobNotOpen_Command(object sender, CommandEventArgs e)
        {
            lblHeader.Text = e.CommandName.ToString();
            tbl.Visible = true;
            dv.Visible = true;
            if (e.CommandName.Equals("JobNotOpen"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.JobPendingForOpening);
                HdfCommanArgument = "JobNotOpen";
            }
            else if (e.CommandName.Equals("ATDETDNotUpdated"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ATDATANotUpdated);
                HdfCommanArgument = "ATDETDNotUpdated";
            }
            else if (e.CommandName.Equals("BLNOTReleased"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotReleased);
                HdfCommanArgument = "BLNOTReleased";
            }
            else if (e.CommandName.Equals("BLNOTCreated"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotCreated);
                HdfCommanArgument = "BLNOTCreated";
            }
            else if (e.CommandName.Equals("BLHOUSEMASTERNOTReleased"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotReleased);
                HdfCommanArgument = "BLHOUSEMASTERNOTReleased";
            }
            else if (e.CommandName.Equals("BLHOUSEMASTERNOTCreated"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotCreated);
                HdfCommanArgument = "BLHOUSEMASTERNOTCreated";
            }
            else if (e.CommandName.Equals("COSTNOTBOOKED"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.CostNotBooked);
                HdfCommanArgument = "COSTNOTBOOKED";
            }
            else if (e.CommandName.Equals("REVENUENOTBOOKED"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.RevenueNotBooked);
                HdfCommanArgument = "REVENUENOTBOOKED";
            }
            else if (e.CommandName.Equals("UNBILLED"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.Unbilled);
                HdfCommanArgument = "UNBILLED";
            }
            else if (e.CommandName.Equals("ShipPEndingForOpening"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ShipmentPendingForClosure);
                HdfCommanArgument = "ShipPEndingForOpening";
            }
            else if (e.CommandName.Equals("DLNOTToDate"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotCreated);
                HdfCommanArgument = "DLNOTToDate";
            }
            else if (e.CommandName.Equals("DLNOTReleased"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotReleased);
                HdfCommanArgument = "DLNOTReleased";
            }

            else if (e.CommandName.Equals("IGMNO"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMNo);
                HdfCommanArgument = "IGMNO";
            }
            else if (e.CommandName.Equals("SIPENDINGS"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.SIPending);
                HdfCommanArgument = "SIPENDINGS";
            }
            else if (e.CommandName.Equals("InvoiceDetails"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.InvoiceDispatchDetails);
                HdfCommanArgument = "InvoiceDetails";
            }
            else if (e.CommandName.Equals("IGMDT"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMFillingDT);
                HdfCommanArgument = "IGMDT";
            }
            else if (e.CommandName.Equals("VGNPending"))
            {
                gvLoginUsers.PageIndex = 0;
                string data = e.CommandArgument.ToString();
                HdfUserLoginCountryCode = data;
                string[] dd = data.Split(new Char[] { ':' });

                HdfBranchCode = dd[1];
                HdfCompanyCode = dd[0];

                BindDashboard(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.VGMPending);
                HdfCommanArgument = "VGNPending";
            }
        }
        protected void btnImportExcel_Click(object sender, EventArgs e)
        {

            DashboardData _dashboardData = new DashboardData();
            DataSet ds = null;

            if (HdfCommanArgument.Equals("JobNotOpen"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.JobPendingForOpening);
            }
            else if (HdfCommanArgument.Equals("ATDETDNotUpdated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ATDATANotUpdated);
            }
            else if (HdfCommanArgument.Equals("BLNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotReleased);
            }
            else if (HdfCommanArgument.Equals("BLNOTCreated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotCreated);
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotReleased);
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTCreated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotCreated);
            }
            else if (HdfCommanArgument.Equals("COSTNOTBOOKED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.CostNotBooked);
            }
            else if (HdfCommanArgument.Equals("REVENUENOTBOOKED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.RevenueNotBooked);
            }
            else if (HdfCommanArgument.Equals("UNBILLED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.Unbilled);
            }
            else if (HdfCommanArgument.Equals("ShipPEndingForOpening"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ShipmentPendingForClosure);
            }
            else if (HdfCommanArgument.Equals("DLNOTToDate"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotCreated);
            }
            else if (HdfCommanArgument.Equals("DLNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotReleased);
            }
            else if (HdfCommanArgument.Equals("SIPENDINGS"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.SIPending);
            }
            else if (HdfCommanArgument.Equals("InvoiceDetails"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.InvoiceDispatchDetails);
            }
            else if (HdfCommanArgument.Equals("IGMNO"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMNo);
            }
            else if (HdfCommanArgument.Equals("IGMDT"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMFillingDT);
            }
            else if (HdfCommanArgument.Equals("VGNPending"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.VGMPending);
            }
            if (ds != null)
            {
                string filePath = HdfCommanArgument.ToString() + ".xlsx";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\ExpContGateOutReport.xlsx");
                try
                {
                    LovelyExport.ExportDataSetToExcel(ds.Tables[0], appDataPath);

                    HttpContext.Current.Response.ContentType = ContentType;//"APPLICATION/OCTET-STREAM";
                    String Header = "Attachment; Filename=" + filePath;
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
                    System.IO.FileInfo Dfile = new System.IO.FileInfo(appDataPath);
                    try
                    {
                        HttpContext.Current.Response.WriteFile(appDataPath);
                    }
                    catch (Exception Ex)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                    finally
                    {
                        HttpContext.Current.Response.End();
                    }
                }
                catch (Exception Ex)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Data Found", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void gvLoginUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int count = gd.Columns.Count;
                for (int i = 0; i < count; i++)
                {

                    if (e.Row.Cells[i].Text.ToLower() == "red" || e.Row.Cells[i].Text.ToLower() == "green" || e.Row.Cells[i].Text.ToLower() == "yellow" || e.Row.Cells[i].Text.ToLower() == "darkred")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.FromName(e.Row.Cells[i].Text.ToLower());
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.FromName(e.Row.Cells[i].Text.ToLower());
                    }
                }
            }
        }

        void ExportGridToExcel()
        {
            string filePath = string.Empty;
            DashboardData _dashboardData = new DashboardData();
            DataSet ds = null;
            if (HdfCommanArgument.Equals("JobNotOpen"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.JobPendingForOpening);
            }
            else if (HdfCommanArgument.Equals("ATDETDNotUpdated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ATDATANotUpdated);
            }
            else if (HdfCommanArgument.Equals("BLNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotReleased);
            }
            else if (HdfCommanArgument.Equals("BLNOTCreated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlNotCreated);
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotReleased);
            }
            else if (HdfCommanArgument.Equals("BLHOUSEMASTERNOTCreated"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.BlHouseMasterNotCreated);
            }
            else if (HdfCommanArgument.Equals("COSTNOTBOOKED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.CostNotBooked);
            }
            else if (HdfCommanArgument.Equals("REVENUENOTBOOKED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.RevenueNotBooked);
            }
            else if (HdfCommanArgument.Equals("UNBILLED"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.Unbilled);
            }
            else if (HdfCommanArgument.Equals("ShipPEndingForOpening"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.ShipmentPendingForClosure);
            }
            else if (HdfCommanArgument.Equals("DLNOTToDate"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotCreated);
            }
            else if (HdfCommanArgument.Equals("DLNOTReleased"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.DlNotReleased);
            }
            else if (HdfCommanArgument.Equals("SIPENDINGS"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.SIPending);
            }
            else if (HdfCommanArgument.Equals("InvoiceDetails"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.InvoiceDispatchDetails);
            }
            else if (HdfCommanArgument.Equals("IGMNO"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMNo);
            }
            else if (HdfCommanArgument.Equals("IGMDT"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.IGMFillingDT);
            }
            else if (HdfCommanArgument.Equals("VGNPending"))
            {
                ds = _dashboardData.GetDashBoardByFlag(HdfCompanyCode, HdfBranchCode, (int)DashboardFetchType.VGMPending);
            }
            if (ds != null)
            {
                gt = ds.Tables[0];
                filePath = HdfCommanArgument.ToString() + ".xls";
                var appDataPath = Server.MapPath("~" + "\\ExcelFiles\\ExpContGateOutReport.xlsx");

                gvTempGrid.DataSource = ds.Tables[0];
                gvTempGrid.DataBind();



            }
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filePath);
            gvTempGrid.HeaderRow.Style.Add("background-color", "#fff");
            gvTempGrid.GridLines = GridLines.Both;
            gvTempGrid.HeaderStyle.Font.Bold = true;
            gvTempGrid.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
            gvTempGrid.Visible = false;




        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btnImportExcel_Click1(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        protected void gvTempGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int count = gt.Columns.Count;
                for (int i = 0; i < count; i++)
                {

                    if (e.Row.Cells[i].Text.ToLower() == "red" || e.Row.Cells[i].Text.ToLower() == "green" || e.Row.Cells[i].Text.ToLower() == "yellow" || e.Row.Cells[i].Text.ToLower() == "darkred")
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.FromName(e.Row.Cells[i].Text.ToLower());
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.FromName(e.Row.Cells[i].Text.ToLower());
                    }
                }
            }
        }
    }
}