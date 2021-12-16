using InternalCargoWiseReport.Data;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ICWR.Data.Utility;
using ClosedXML.Excel;
using System.IO;


namespace InternalCargoWiseReport.FrontEnd.DailyEffort
{
    public partial class DailyEffortList : System.Web.UI.Page
    {
        DailyEffortData _data = null;
        DailyEffortDto _dto = null;
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDrp();
                BindGrid();
            }

        }
        public void BindDrp()
        {
            try
            {

                _data = new DailyEffortData();
                _dto = new DailyEffortDto();
                DataTable dt = new DataTable();
                dt = _data.GetOrganisation(_dto);
                if (dt != null && dt.Rows.Count > 0)
                {

                    drpoganisationname.DataSource = dt;
                    drpoganisationname.DataValueField = "ID";
                    drpoganisationname.DataTextField = "Name";
                    drpoganisationname.DataBind();
                    drpoganisationname.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    drpoganisationname.Items.Insert(0, new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void drpoganisationname_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindGrid()
        {
            _dto = new DailyEffortDto();
            _data = new DailyEffortData();

            if (drpoganisationname.SelectedValue != string.Empty)
            {
                _dto.sed_om_id = drpoganisationname.SelectedValue.ToConvertNullInt();
                DataTable dt = new DataTable();
                dt = _data.GetAll(_dto.sed_om_id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvDailyEffortList.DataSource = dt;
                    gvDailyEffortList.DataBind();
                    btnexportexcel.Enabled = true;
                }
                else
                {
                    btnexportexcel.Enabled = false;
                    gvDailyEffortList.DataBind();
                }
            }
            else
            {
                DataTable dts = new DataTable();
                dts = _data.GetDetails(_dto);
                if (dts != null && dts.Rows.Count > 0)
                {
                    gvDailyEffortList.DataSource = dts;
                    gvDailyEffortList.DataBind();
                    btnexportexcel.Enabled = true;
                }
                else
                {
                    btnexportexcel.Enabled = false;
                    gvDailyEffortList.DataBind();
                }
            }
        }
        protected void gvDailyEffortList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvDailyEffortList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("lbleffortid");
                    int requestId = txtID.Text.ToInt();

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        Response.Redirect("DailyEffort.aspx?lovelyindexing=27&requestId=" + requestId + "&726782dsjsbj=3877843hdws", false);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        protected void gvDailyEffortList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("DailyEffort.aspx");
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;
            Label gvlblID = (Label)gvRow.FindControl("lbleffortid");
            if (gvlblID.Text != string.Empty)
            {
                _data = new DailyEffortData();
                _dto = new DailyEffortDto();
                _dto.sed_id = gvlblID.Text.ToConvertNullInt();
                DataTable dt = new DataTable();
                dt = _data.GetAllById(_dto.sed_id);
                string str = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    str = dt.Rows[0]["sed_filename"].ToString();
                    if (str != "")
                    {
                        string modified_URL = "window.open('" + str + "', '_blank');";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", modified_URL, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Document Not Found!!", "Error!!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Document Not Found!!", "Error!!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }
        protected void btnexportexcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        public void ExportToExcel()
        {
            DataTable dt = new DataTable("GridView_Data");
            foreach (TableCell cell in gvDailyEffortList.HeaderRow.Cells)
            {
                if (cell.Text.Contains("View") || cell.Text.Contains("Edit"))
                {
                }
                else
                {
                    dt.Columns.Add(cell.Text);
                }

            }
            foreach (GridViewRow row in gvDailyEffortList.Rows)
            {
                dt.Rows.Add();

                Label SrNo = row.FindControl("lbleffortid") as Label;
                Label Orgname = row.FindControl("lblorgname") as Label;
                Label Requestdate = row.FindControl("lblrequestdate") as Label;
                Label Requestby = row.FindControl("lblrequestby") as Label;
                Label Approvedby = row.FindControl("lblapprovedby") as Label;
                Label application = row.FindControl("lblapplication") as Label;
                Label module = row.FindControl("lblmodule") as Label;
                Label justification = row.FindControl("lbljustification") as Label;
                Label effortestimate = row.FindControl("lbleffortestimate") as Label;
                Label effortcrateby = row.FindControl("lbleffortcreateby") as Label;

                row.Cells[0].Text = SrNo.Text;
                row.Cells[1].Text = Orgname.Text;
                row.Cells[2].Text = Requestdate.Text;
                row.Cells[3].Text = Requestby.Text;
                row.Cells[4].Text = Approvedby.Text;
                row.Cells[5].Text = application.Text;
                row.Cells[6].Text = module.Text;
                row.Cells[7].Text = justification.Text;
                row.Cells[8].Text = effortestimate.Text;
                row.Cells[9].Text = effortcrateby.Text;

                for (int i = 0; i < 10; i++)
                {
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        protected void gvDailyEffortList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDailyEffortList.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}