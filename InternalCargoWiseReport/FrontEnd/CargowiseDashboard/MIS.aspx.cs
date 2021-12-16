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


namespace InsertMultipleExcelFile
{
    public partial class UspMIS : System.Web.UI.Page
    {
        MISDto mis = null;
        MISData data = null;
        DataSet ds = null;
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDrp();
                //BindGrid();
            }
        }
        bool Validation(string type)
        {
            if (string.IsNullOrEmpty(Drpdivisin.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select Division!!", "Error!");
                return true;
            }
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
            if (Drpmonth.SelectedItem.Text == "---Select--")
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select Current Month!!", "Error!");
                return true;
            }
            return false;
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

            ds = data.BindMIStype(mis);
            Drpmistype.DataSource = ds;
            Drpmistype.DataTextField = "mtydesc";
            Drpmistype.DataValueField = "mtyid";
            Drpmistype.DataBind();
            Drpmistype.Items.Insert(0, new ListItem("---Select--", string.Empty));

            Drpmistype.Items[0].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[1].Attributes.Add("disabled", "disabled");
            Drpmistype.Items[3].Attributes.Add("disabled", "disabled");

            Drpmonth.Items.Insert(0, new ListItem("---Select--", string.Empty));

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            MISData misdata = new MISData();
            MISDto obj = new MISDto();
            obj = GetProperties();

            foreach (GridViewRow row in MisGrid.Rows)
            {
                string lblmarid = (row.FindControl("lblremarksid") as Label).Text;
                obj.marid = lblmarid.ToConvertNullInt();

                string lblmaid = (row.FindControl("ACtualId") as Label).Text;
                obj.maid = lblmaid.ToConvertNullInt();

                string lblmpmid = (row.FindControl("lblparticularid") as Label).Text;
                obj.mampmid = lblmpmid.ToConvertNullInt();

                //string txtapr = (row.FindControl("misapr") as TextBox).Text;
                //obj.maApr = txtapr.ToFloat();

                string txtmay = (row.FindControl("mismay") as TextBox).Text;
                obj.maMay = txtmay.ToFloat();

                //string txtjune = (row.FindControl("misjune") as TextBox).Text;
                //obj.maJun = txtjune.ToFloat();

                //string txtjuly = (row.FindControl("misjuly") as TextBox).Text;
                //obj.maJul = txtjuly.ToFloat();

                //string txtaug = (row.FindControl("misaug") as TextBox).Text;
                //obj.maAug = txtaug.ToFloat();

                //string txtsep = (row.FindControl("missep") as TextBox).Text;
                //obj.maSep = txtsep.ToFloat();

                //string txtoct = (row.FindControl("misoct") as TextBox).Text;
                //obj.maOct = txtoct.ToFloat();

                //string txtnov = (row.FindControl("misnov") as TextBox).Text;
                //obj.maNov = txtnov.ToFloat();

                //string txtdec = (row.FindControl("misdec") as TextBox).Text;
                //obj.maDec = txtdec.ToFloat();

                //string txtjan = (row.FindControl("misjan") as TextBox).Text;
                //obj.maJan = txtjan.ToFloat();

                //string txtfeb = (row.FindControl("misfeb") as TextBox).Text;
                //obj.maFeb = txtfeb.ToFloat();

                //string txtmarch = (row.FindControl("mismarch") as TextBox).Text;
                //obj.maMar = txtmarch.ToFloat();
                //if (obj.maMay == 0.0 && obj.maJun == 0.0 && obj.maJul == 0.0 && obj.maAug == 0.0 && obj.maSep == 0.0 && obj.maOct == 0.0 && obj.maNov == 0.0 && obj.maDec == 0.0 && obj.maJan == 0.0 && obj.maFeb == 0.0 && obj.maMar == 0.0 && obj.maApr == 0.0)
                //{
                //    return;
                //}
                //string txttotal = (row.FindControl("mistotal") as TextBox).Text;
                //string txtytd = (row.FindControl("misytd") as TextBox).Text;
                string txtremarks = (row.FindControl("marremarks") as TextBox).Text;
                obj.marRemarks = txtremarks.ToString();
                //obj.maTotal = +obj.maMay + obj.maJun + obj.maJul + obj.maAug + obj.maSep + obj.maOct + obj.maNov + obj.maDec + obj.maJan + obj.maFeb + obj.maMar + obj.maApr;


                string data = misdata.Savemisdata(obj);
                if (data != null)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data Save Successfully!!", "Error!");
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Something is wrong!!", "Error!");
                }
            }

        }
        MISDto GetProperties()
        {
            MISDto obj = new MISDto();
            obj.misdivisionid = Drpdivisin.SelectedValue.ToConvertNullInt();
            obj.mistyid = Drpmistype.SelectedValue.ToConvertNullInt();
            obj.mafinancialyear = DrpFYear.SelectedItem.Text;
            obj.marMonthName = Drpmonth.SelectedItem.Text;
            if (obj.marMonthName == "Jan")
            {

                obj.marMonth = 1;
            }
            else if (obj.marMonthName == "Feb")
            {
                obj.marMonth = 2;
            }
            else if (obj.marMonthName == "March")
            {
                obj.marMonth = 3;
            }
            else if (obj.marMonthName == "Apr")
            {
                obj.marMonth = 4;
            }
            else if (obj.marMonthName == "May")
            {
                obj.marMonth = 5;
            }
            else if (obj.marMonthName == "June")
            {
                obj.marMonth = 6;
            }
            else if (obj.marMonthName == "July")
            {
                obj.marMonth = 7;
            }
            else if (obj.marMonthName == "Aug")
            {
                obj.marMonth = 8;
            }
            else if (obj.marMonthName == "Sep")
            {
                obj.marMonth = 9;
            }
            else if (obj.marMonthName == "Oct")
            {
                obj.marMonth = 10;
            }
            else if (obj.marMonthName == "Nov")
            {
                obj.marMonth = 11;
            }
            else if (obj.marMonthName == "Dec")
            {
                obj.marMonth = 12;
            }
            return obj;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (Validation(""))
            {
                return;
            }
            mis = new MISDto();
            data = new MISData();
            mis = GetProperties();
            ds = data.GetMisdata(mis);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                MisGrid.DataSource = ds;
                MisGrid.DataBind();
            }
            else
            {
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Data Not Found!!", "Error!");
            }
        }
        //void BindGrid()
        //{
        //    mis = new MISDto();
        //    data = new MISData();
        //    mis = GetProperties();
        //    ds = data.GetAll(mis);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        MisGrid.DataSource = ds;
        //        MisGrid.DataBind();
        //    }
        //    else
        //    {

        //    }
        //}
    }
}