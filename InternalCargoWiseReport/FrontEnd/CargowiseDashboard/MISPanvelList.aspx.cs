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
using System.Drawing;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MISPanvelList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDrp();
            }
        }

        #region Method

        void bindDrp()
        {
            MISMasterData _data = new MISMasterData();

            DataSet result = _data.getFinYear();

            if (result != null)
            {
                drpFinancialYear.DataSource = result.Tables[0];
                drpFinancialYear.DataValueField = "FinYear";
                drpFinancialYear.DataTextField = "FinYear";
                drpFinancialYear.DataBind();

            }

            DataSet divisionResult = _data.getDivision();
            if (divisionResult != null)
            {
                drpDivision.DataSource = divisionResult.Tables[0];
                drpDivision.DataValueField = "mdid";
                drpDivision.DataTextField = "mddesc";
                drpDivision.DataBind();
                drpDivision.Items.Insert(0, new ListItem("--Select--", string.Empty));

            }

            DataSet typeResult = _data.getType();
            if (typeResult != null)
            {
                drpType.DataSource = typeResult.Tables[0];
                drpType.DataValueField = "mtyid";
                drpType.DataTextField = "mtycode";
                drpType.DataBind();

            }
        }

        void bindGrd()
        {
            MisBudgetData _data = new MisBudgetData();
            MisBudgetDto request = new MisBudgetDto();
            request.mbfinancialyear = drpFinancialYear.SelectedValue;
            if (drpSubdivision.Items.Count > 0)
            {
                request.mbdivisionid = drpSubdivision.SelectedValue.ToNullLong();
            }
            else
            {
                request.mbdivisionid = drpDivision.SelectedValue.ToNullLong();
            }
            request.mbmtyid = drpType.SelectedValue.ToNullLong();
            DataSet ds = _data.getDataFromBudgetTableForList(request);
            if (ds != null)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                gvList.DataSource = ds.Tables[0];
                gvList.DataBind();
                this.gvList.Font.Name ="Calibri";
            }
            else
            {
                gvList.DataBind();
                txtRecordFound.Text = "0";
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found!!", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }

        bool Searchvalidation()
        {
            if (drpType.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpFinancialYear.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Financial Year!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpDivision.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpDivision.SelectedValue != string.Empty)
            {
                if (drpSubdivision.Items.Count > 0)
                {
                    if (drpSubdivision.SelectedValue == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select sub Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                }
            }
            return true;
        }


        #endregion

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            if(!Searchvalidation())
            {
                return;
            }
            bindGrd();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MISPanvel.aspx");
        }

        protected void drpDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpDivision.SelectedValue == string.Empty)
            {
                drpSubdivision.Items.Clear();
                // drpSubdivision.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpSubdivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
                return;
            }
            MISMasterData _data = new MISMasterData();
            DataSet result = _data.getSubDivisionResults(drpDivision.SelectedValue);

            if (result != null)
            {
                drpSubdivision.DataSource = result.Tables[0];
                drpSubdivision.DataValueField = "ID";
                drpSubdivision.DataTextField = "NAME";
                drpSubdivision.DataBind();

                drpSubdivision.Visible = true;
                lblSubDivision.Visible = true;
                lblSubdivisionStar.Visible = true;

            }
            else
            {
                drpSubdivision.Items.Clear();
                // drpSubdivision.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpSubdivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 3; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");

                }
            }


            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                Label gvlblDesc = (Label)e.Row.FindControl("gvlblParticular");
                Label gvlblTotal = (Label)e.Row.FindControl("gvlblTotal");
                Label gvlblApr = (Label)e.Row.FindControl("gvlblApr");
                Label gvlblMay = (Label)e.Row.FindControl("gvlblMay");
                Label gvlblJun = (Label)e.Row.FindControl("gvlblJun");
                Label gvlblJul = (Label)e.Row.FindControl("gvlblJul");
                Label gvlblAug = (Label)e.Row.FindControl("gvlblAug");
                Label gvlblSep = (Label)e.Row.FindControl("gvlblSep");
                Label gvlblOct = (Label)e.Row.FindControl("gvlblOct");
                Label gvlblNov = (Label)e.Row.FindControl("gvlblNov");
                Label gvlblDec = (Label)e.Row.FindControl("gvlblDec");
                Label gvlblJan = (Label)e.Row.FindControl("gvlblJan");
                Label gvlblFeb = (Label)e.Row.FindControl("gvlblFeb");
                Label gvlblMar = (Label)e.Row.FindControl("gvlblMar");


                if (gvlblDesc.Text == "Gross Profit%" || gvlblDesc.Text == "EBITDA Before CC%" || gvlblDesc.Text == "EBITDA After CC%" ||
                    gvlblDesc.Text == "% of Salaries to Revenue" || gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    if(gvlblApr.Text!=string.Empty)
                    gvlblApr.Text=    Math.Round(decimal.Parse(gvlblApr.Text),1).ToString()+"%";
                    if(gvlblMay.Text!=string.Empty)
                    gvlblMay.Text=    Math.Round(decimal.Parse(gvlblMay.Text),1).ToString()+"%";
                    if(gvlblJun.Text!=string.Empty)
                    gvlblJun.Text=    Math.Round(decimal.Parse(gvlblJun.Text),1).ToString()+"%";
                    if(gvlblJul.Text!=string.Empty)
                    gvlblJul.Text=    Math.Round(decimal.Parse(gvlblJul.Text),1).ToString()+"%";
                    if(gvlblAug.Text!=string.Empty)
                    gvlblAug.Text=    Math.Round(decimal.Parse(gvlblAug.Text),1).ToString()+"%";
                    if(gvlblSep.Text!=string.Empty)
                    gvlblSep.Text=    Math.Round(decimal.Parse(gvlblSep.Text),1).ToString()+"%";
                    if(gvlblOct.Text!=string.Empty)
                    gvlblOct.Text=    Math.Round(decimal.Parse(gvlblOct.Text),1).ToString()+"%";
                    if(gvlblNov.Text!=string.Empty)
                    gvlblNov.Text=    Math.Round(decimal.Parse(gvlblNov.Text),1).ToString()+"%";
                    if(gvlblDec.Text!=string.Empty)
                    gvlblDec.Text=    Math.Round(decimal.Parse(gvlblDec.Text),1).ToString()+"%";
                    if(gvlblJan.Text!=string.Empty)
                    gvlblJan.Text=    Math.Round(decimal.Parse(gvlblJan.Text),1).ToString()+"%";
                    if(gvlblFeb.Text!=string.Empty)
                    gvlblFeb.Text=    Math.Round(decimal.Parse(gvlblFeb.Text),1).ToString()+"%";
                    if(gvlblMar.Text!=string.Empty)
                    gvlblMar.Text=    Math.Round(decimal.Parse(gvlblMar.Text),1).ToString()+"%";
                    if(gvlblTotal.Text!=string.Empty)
                    gvlblTotal.Text = Math.Round(decimal.Parse(gvlblTotal.Text),1).ToString() + "%";
                }

                


                if (gvlblDesc.Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                }
                if (gvlblDesc.Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {
                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                }

            }
        }
    }
}