using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;
using ICWR.Models;
using ICWR.Data;
using System.Drawing;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MisActualList : System.Web.UI.Page
    {
        MISDto mis = null;
        MISData data = null;
        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    BindDrp();
                    Drpafildivision.Visible = false;
                }
            }
        }
        void BindDrp()
        {
            ds = new DataSet();
            mis = new MISDto();
            data = new MISData();
            ds = data.BindDivision(mis);
            Drpdivisin.DataSource = ds;
            Drpdivisin.DataTextField = "mddesc";
            Drpdivisin.DataValueField = "mdid";
            Drpdivisin.DataBind();
            Drpdivisin.Items.Insert(0, new ListItem("---Select--", string.Empty));


            ds = data.BindSubDivision(mis);
            Drpafildivision.DataSource = ds;
            Drpafildivision.DataTextField = "mddesc";
            Drpafildivision.DataValueField = "mdid";
            Drpafildivision.DataBind();
            Drpafildivision.Items.Insert(0, new ListItem("---Select--", string.Empty));

            ds = data.BindMIStype(mis);
            Drpmistype.DataSource = ds;
            Drpmistype.DataTextField = "mtydesc";
            Drpmistype.DataValueField = "mtyid";
            Drpmistype.DataBind();
            Drpmistype.Items.Insert(0, new ListItem("---Select--", string.Empty));

            Drpmistype.Items[2].Selected = true;
            Drpmistype.Items[0].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[1].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[3].Attributes.Add("disabled", "disabled");

            MISMasterData _data = new MISMasterData();
            DataSet result = _data.getFinYear();
            if (result != null)
            {
                DrpFYear.DataSource = result.Tables[0];
                DrpFYear.DataValueField = "FinYear";
                DrpFYear.DataTextField = "FinYear";
                DrpFYear.DataBind();

            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            mis = new MISDto();
            data = new MISData();
            mis = GetProperties();
            ds = data.GetMisdatabyFYearandDivision(mis);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                MisGridList.DataSource = ds;
                MisGridList.DataBind();

                txtcountdata.Text = ds.Tables[0].Rows.Count.ToString();
                GVwithoutSEIS.DataSource = ds;
                GVwithoutSEIS.DataBind();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Not Found!!", "Error!");
                txtRecordFound.Text = "0";
                MisGridList.DataBind();
                GVwithoutSEIS.DataBind();

            }
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MisActual.aspx");
        }
        MISDto GetProperties()
        {
            MISDto obj = new MISDto();
            if (Drpafildivision.Items.Count > 0)
            {
                obj.misdivisionid = Drpafildivision.SelectedValue.ToConvertNullInt();
            }
            else
            {
                obj.misdivisionid = Drpdivisin.SelectedValue.ToConvertNullInt();
            }
            obj.mistyid = Drpmistype.SelectedValue.ToConvertNullInt();
            obj.mafinancialyear = DrpFYear.SelectedItem.Text;
            return obj;
        }
        protected void Drpdivisin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drpdivisin.SelectedValue == string.Empty)
            {
                Drpafildivision.Items.Clear();
                // Drpafildivision.Items.Insert(0, new ListItem("--Select--", string.Empty));
                Drpafildivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
                return;
            }
            MISMasterData _data = new MISMasterData();
            DataSet result = _data.getSubDivisionResults(Drpdivisin.SelectedValue);

            if (result != null)
            {
                Drpafildivision.DataSource = result.Tables[0];
                Drpafildivision.DataValueField = "ID";
                Drpafildivision.DataTextField = "NAME";
                Drpafildivision.DataBind();

                Drpafildivision.Visible = true;
                lblSubDivision.Visible = true;
                lblSubdivisionStar.Visible = true;

            }
            else
            {
                Drpafildivision.Items.Clear();
                Drpafildivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
            }
        }

        protected void MisGridList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
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
                //e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
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

                    if (gvlblApr.Text != string.Empty)
                        gvlblApr.Text = Math.Round(decimal.Parse(gvlblApr.Text), 1).ToString() + "%";
                    if (gvlblMay.Text != string.Empty)
                        gvlblMay.Text = Math.Round(decimal.Parse(gvlblMay.Text), 1).ToString() + "%";
                    if (gvlblJun.Text != string.Empty)
                        gvlblJun.Text = Math.Round(decimal.Parse(gvlblJun.Text), 1).ToString() + "%";
                    if (gvlblJul.Text != string.Empty)
                        gvlblJul.Text = Math.Round(decimal.Parse(gvlblJul.Text), 1).ToString() + "%";
                    if (gvlblAug.Text != string.Empty)
                        gvlblAug.Text = Math.Round(decimal.Parse(gvlblAug.Text), 1).ToString() + "%";
                    if (gvlblSep.Text != string.Empty)
                        gvlblSep.Text = Math.Round(decimal.Parse(gvlblSep.Text), 1).ToString() + "%";
                    if (gvlblOct.Text != string.Empty)
                        gvlblOct.Text = Math.Round(decimal.Parse(gvlblOct.Text), 1).ToString() + "%";
                    if (gvlblNov.Text != string.Empty)
                        gvlblNov.Text = Math.Round(decimal.Parse(gvlblNov.Text), 1).ToString() + "%";
                    if (gvlblDec.Text != string.Empty)
                        gvlblDec.Text = Math.Round(decimal.Parse(gvlblDec.Text), 1).ToString() + "%";
                    if (gvlblJan.Text != string.Empty)
                        gvlblJan.Text = Math.Round(decimal.Parse(gvlblJan.Text), 1).ToString() + "%";
                    if (gvlblFeb.Text != string.Empty)
                        gvlblFeb.Text = Math.Round(decimal.Parse(gvlblFeb.Text), 1).ToString() + "%";
                    if (gvlblMar.Text != string.Empty)
                        gvlblMar.Text = Math.Round(decimal.Parse(gvlblMar.Text), 1).ToString() + "%";
                    if (gvlblTotal.Text != string.Empty)
                        gvlblTotal.Text = Math.Round(decimal.Parse(gvlblTotal.Text), 1).ToString() + "%";
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

                if (Drpdivisin.SelectedValue == "1")
                {
                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }
            }
        }
        protected void GVwithoutSEIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
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

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
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

                    if (gvlblApr.Text != string.Empty)
                        gvlblApr.Text = Math.Round(decimal.Parse(gvlblApr.Text), 1).ToString() + "%";
                    if (gvlblMay.Text != string.Empty)
                        gvlblMay.Text = Math.Round(decimal.Parse(gvlblMay.Text), 1).ToString() + "%";
                    if (gvlblJun.Text != string.Empty)
                        gvlblJun.Text = Math.Round(decimal.Parse(gvlblJun.Text), 1).ToString() + "%";
                    if (gvlblJul.Text != string.Empty)
                        gvlblJul.Text = Math.Round(decimal.Parse(gvlblJul.Text), 1).ToString() + "%";
                    if (gvlblAug.Text != string.Empty)
                        gvlblAug.Text = Math.Round(decimal.Parse(gvlblAug.Text), 1).ToString() + "%";
                    if (gvlblSep.Text != string.Empty)
                        gvlblSep.Text = Math.Round(decimal.Parse(gvlblSep.Text), 1).ToString() + "%";
                    if (gvlblOct.Text != string.Empty)
                        gvlblOct.Text = Math.Round(decimal.Parse(gvlblOct.Text), 1).ToString() + "%";
                    if (gvlblNov.Text != string.Empty)
                        gvlblNov.Text = Math.Round(decimal.Parse(gvlblNov.Text), 1).ToString() + "%";
                    if (gvlblDec.Text != string.Empty)
                        gvlblDec.Text = Math.Round(decimal.Parse(gvlblDec.Text), 1).ToString() + "%";
                    if (gvlblJan.Text != string.Empty)
                        gvlblJan.Text = Math.Round(decimal.Parse(gvlblJan.Text), 1).ToString() + "%";
                    if (gvlblFeb.Text != string.Empty)
                        gvlblFeb.Text = Math.Round(decimal.Parse(gvlblFeb.Text), 1).ToString() + "%";
                    if (gvlblMar.Text != string.Empty)
                        gvlblMar.Text = Math.Round(decimal.Parse(gvlblMar.Text), 1).ToString() + "%";
                    if (gvlblTotal.Text != string.Empty)
                        gvlblTotal.Text = Math.Round(decimal.Parse(gvlblTotal.Text), 1).ToString() + "%";
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

                if (Drpdivisin.SelectedValue == "1")
                {
                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }
            }
        }
    }
}