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
    public partial class SEISClaim : System.Web.UI.Page
    {
        SEISClaimDto seisclaim = null;
        SEISClaimData claimdata = null;
        DataSet ds = null;
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDrp();
            }
        }
        bool Validation(string type)
        {
            if (string.IsNullOrEmpty(DrpFYear.SelectedItem.Text))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Year!!", "Error!");
                return true;
            }
            if (string.IsNullOrEmpty(Drpmistype.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select Type!!", "Error!");
                return true;
            }
            if (string.IsNullOrEmpty(DrpSeis.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select SEIS Type!!", "Error!");
                return true;
            }
            return false;
        }
        void BindDrp()
        {
            ds = new DataSet();
            seisclaim = new SEISClaimDto();
            claimdata = new SEISClaimData();
            ds = claimdata.BindDivision(seisclaim);
            Drpdivisin.DataSource = ds;
            Drpdivisin.DataTextField = "mddesc";
            Drpdivisin.DataValueField = "mdid";
            Drpdivisin.DataBind();
            Drpdivisin.Items.Insert(0, new ListItem("---Select--", string.Empty));
            Drpdivisin.Visible = false;

            ds = claimdata.BindMIStype(seisclaim);
            Drpmistype.DataSource = ds;
            Drpmistype.DataTextField = "mtydesc";
            Drpmistype.DataValueField = "mtyid";
            Drpmistype.DataBind();
            Drpmistype.Items.Insert(0, new ListItem("---Select--", string.Empty));

            Drpmistype.Items[2].Selected = true;
            Drpmistype.Items[0].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[1].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[3].Attributes.Add("disabled", "disabled");
            Drpmistype.Enabled = false;


            DrpSeis.Enabled = false;

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
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            SEISClaimData claimdata = new SEISClaimData();
            SEISClaimDto obj = new SEISClaimDto();
            obj = GetProperties();

            foreach (GridViewRow row in MisSEISGrid.Rows)
            {
                string lbldivisionid = (row.FindControl("lbldivisionid") as Label).Text;
                obj.srdivisionid = lbldivisionid.ToConvertNullInt();

                string lblmaid = (row.FindControl("ACtualId") as Label).Text;
                obj.srid = lblmaid.ToConvertNullInt();

                string lblmpmid = (row.FindControl("lblparticularid") as Label).Text;
                obj.srmpmid = lblmpmid.ToConvertNullInt();

                string txtapr = (row.FindControl("gvtxtmptApr") as TextBox).Text;
                obj.srApr = txtapr.ToFloat();

                string txtmay = (row.FindControl("gvtxtmptMay") as TextBox).Text;
                obj.srMay = txtmay.ToFloat();

                string txtjune = (row.FindControl("gvtxtmptJun") as TextBox).Text;
                obj.srJun = txtjune.ToFloat();

                string txtjuly = (row.FindControl("gvtxtJul") as TextBox).Text;
                obj.srJul = txtjuly.ToFloat();

                string txtaug = (row.FindControl("gvtxtAug") as TextBox).Text;
                obj.srAug = txtaug.ToFloat();

                string txtsep = (row.FindControl("gvtxtmptSep") as TextBox).Text;
                obj.srSep = txtsep.ToFloat();

                string txtoct = (row.FindControl("gvtxtmptOct") as TextBox).Text;
                obj.srOct = txtoct.ToFloat();

                string txtnov = (row.FindControl("gvtxtNov") as TextBox).Text;
                obj.srNov = txtnov.ToFloat();

                string txtdec = (row.FindControl("gvtxtmptDec") as TextBox).Text;
                obj.srDec = txtdec.ToFloat();

                string txtjan = (row.FindControl("gvtxtmptJan") as TextBox).Text;
                obj.srJan = txtjan.ToFloat();
                string txtfeb = (row.FindControl("gvtxtFeb") as TextBox).Text;
                obj.srFeb = txtfeb.ToFloat();

                string txtmarch = (row.FindControl("gvtxtMar") as TextBox).Text;
                obj.srMar = txtmarch.ToFloat();
                obj.srTotal = +obj.srMay + obj.srJun + obj.srJul + obj.srAug + obj.srSep + obj.srOct + obj.srNov + obj.srDec + obj.srJan + obj.srFeb + obj.srMar + obj.srApr;
                if (DrpSeis.SelectedItem.Text == "With SEIS")
                {
                    string data = claimdata.Saveseisdata(obj);
                    if (data != null)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data Save Successfully!!", "Success!");
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Something is wrong!!", "Error!");
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Without SEIS Type!!", "Error!");
                }
            }

        }
        SEISClaimDto GetProperties()
        {
            SEISClaimDto obj = new SEISClaimDto();
            obj.msrrmtyid = Drpmistype.SelectedValue.ToString();
            obj.srfinancialyear = DrpFYear.SelectedValue;
            return obj;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (Validation(""))
            {
                return;
            }
            seisclaim = new SEISClaimDto();
            claimdata = new SEISClaimData();
            seisclaim = GetProperties();
            ds = claimdata.GetClaimdata(seisclaim);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                MisSEISGrid.DataSource = ds;
                MisSEISGrid.DataBind();
            }
            else
            {
                DataSet misdata = claimdata.getDataFromParticularMaster(seisclaim);
                if (misdata != null)
                {
                    txtRecordFound.Text = misdata.Tables[0].Rows.Count.ToString();

                    MisSEISGrid.DataSource = misdata.Tables[0];
                    MisSEISGrid.DataBind();
                }
                else
                {
                    MisSEISGrid.DataBind();
                    txtRecordFound.Text = "0";
                }
            }
        }
        protected void MisSEISGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MisSEISGrid.PageIndex = e.NewPageIndex;
        }
        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/SEISClaimList.aspx");
        }

        protected void MisSEISGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 3; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");

                }
            }
        }
    }
}