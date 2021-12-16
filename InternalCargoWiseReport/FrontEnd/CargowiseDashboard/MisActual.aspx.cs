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
    public partial class MisActual : System.Web.UI.Page
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
                Drpafildivision.Visible = false;
            }
        }
        bool Validation(string type)
        {
            if (string.IsNullOrEmpty(Drpdivisin.SelectedValue))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select Division!!", "Error!");
                return true;
            }
            else if (Drpdivisin.SelectedValue != string.Empty)
            {
                if (Drpafildivision.Items.Count > 0)
                {
                    if (Drpafildivision.SelectedValue == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select sub Division!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                }
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

            Drpmonth.Items.Insert(0, new ListItem("---Select--", string.Empty));
            Drpmonth.Items[0].Selected = true;


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
            if(MisGrid.Rows.Count==0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Search First!!", "Error!");
                return;
            }
            if (Drpdivisin.SelectedValue == "1")
            {
                ConvertGridViewToDataTableAFIL();
            }
            else
            {
                ConvertGridViewToDataTableDefault();
            }
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
                if (Drpmonth.SelectedItem.Text == "Apr")
                {
                    string txtapr = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maApr = txtapr.ToFloat();
                    string txtaprwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maAprWS = txtaprwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "May")
                {
                    string txtmay = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maMay = txtmay.ToFloat();
                    string txtmaywoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maMayWS = txtmaywoseis.ToFloat();
                }

                if (Drpmonth.SelectedItem.Text == "Jun")
                {
                    string txtjune = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maJun = txtjune.ToFloat();
                    string txtjunwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maJunWS = txtjunwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Jul")
                {
                    string txtjuly = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maJul = txtjuly.ToFloat();
                    string txtjulwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maJulWS = txtjulwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Aug")
                {
                    string txtaug = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maAug = txtaug.ToFloat();
                    string txtaugwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maAugWS = txtaugwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Sep")
                {
                    string txtsep = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maSep = txtsep.ToFloat();
                    string txtsepwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maSepWS = txtsepwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Oct")
                {
                    string txtoct = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maOct = txtoct.ToFloat();
                    string txtoctwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maOctWS = txtoctwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Nov")
                {
                    string txtnov = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maNov = txtnov.ToFloat();
                    string txtnovwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maNovWS = txtnovwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Dec")
                {
                    string txtdec = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maDec = txtdec.ToFloat();
                    string txtdecwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maDecWS = txtdecwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Jan")
                {
                    MisGrid.Columns[4].HeaderText = "Jan";
                    string txtjan = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maJan = txtjan.ToFloat();
                    string txtjanwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maJanWS = txtjanwoseis.ToFloat();

                }
                if (Drpmonth.SelectedItem.Text == "Feb")
                {
                    string txtfeb = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maFeb = txtfeb.ToFloat();
                    string txtfebwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maFebWS = txtfebwoseis.ToFloat();
                }
                if (Drpmonth.SelectedItem.Text == "Mar")
                {
                    string txtmarch = (row.FindControl("mismonth") as TextBox).Text;
                    obj.maMar = txtmarch.ToFloat();
                    string txtmarwoseis = (row.FindControl("Mwithseis") as TextBox).Text;
                    obj.maMarWS = txtmarwoseis.ToFloat();
                }
                string txtremarks = (row.FindControl("marremarks") as TextBox).Text;
                obj.marRemarks = txtremarks.ToString();
                string data = misdata.Savemisdata(obj);
                if (data != null)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data Save Successfully!!", "Success!");
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
            if (Drpafildivision.Items.Count > 0)
            {
                obj.misdivisionid = Drpafildivision.SelectedValue.ToConvertNullInt();
            }
            else
            {
                obj.misdivisionid = Drpdivisin.SelectedValue.ToConvertNullInt();
            }
            obj.mistyid = Drpmistype.SelectedValue.ToConvertNullInt();
            obj.mafinancialyear = DrpFYear.SelectedValue;
            obj.marMonthName = Drpmonth.SelectedItem.Text;

            if (obj.marMonthName == "Jan")
            {
                MisGrid.Columns[4].HeaderText = "Jan(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Jan(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 1;
            }
            if (obj.marMonthName == "Feb")
            {
                MisGrid.Columns[4].HeaderText = "Feb(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Feb(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 2;
            }
            else if (obj.marMonthName == "Mar")
            {
                MisGrid.Columns[4].HeaderText = "Mar(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Mar(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 3;
            }
            else if (obj.marMonthName == "Apr")
            {
                MisGrid.Columns[4].HeaderText = "Apr(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Apr(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 4;
            }
            else if (obj.marMonthName == "May")
            {
                MisGrid.Columns[4].HeaderText = "May(W SEIS)";
                MisGrid.Columns[5].HeaderText = "May(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 5;
            }
            else if (obj.marMonthName == "Jun")
            {
                MisGrid.Columns[4].HeaderText = "Jun(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Jun(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 6;
            }
            else if (obj.marMonthName == "Jul")
            {
                MisGrid.Columns[4].HeaderText = "Jul(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Jul(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 7;
            }
            else if (obj.marMonthName == "Aug")
            {
                MisGrid.Columns[4].HeaderText = "Aug(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Aug(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 8;
            }
            else if (obj.marMonthName == "Sep")
            {
                MisGrid.Columns[4].HeaderText = "Sep(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Sep(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 9;
            }
            else if (obj.marMonthName == "Oct")
            {
                MisGrid.Columns[4].HeaderText = "Oct(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Oct(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 10;
            }
            else if (obj.marMonthName == "Nov")
            {
                MisGrid.Columns[4].HeaderText = "Nov(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Nov(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 11;
            }
            else if (obj.marMonthName == "Dec")
            {
                MisGrid.Columns[4].HeaderText = "Dec(W SEIS)";
                MisGrid.Columns[5].HeaderText = "Dec(WO SEIS)";
                obj.currentmonth = Drpmonth.SelectedItem.Text;
                obj.monthwithoutseis = Drpmonth.SelectedItem.Text;
                obj.marMonth = 12;
            }
            return obj;
        }

        void ConvertGridViewToDataTableDefault()
        {
            try
            {
                int i = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("currentmonth");
                dt.Columns.Add("monthwithoutseis");

                foreach (GridViewRow gvRow in MisGrid.Rows)
                {
                    TextBox gvtxtmismonth = (TextBox)gvRow.FindControl("mismonth");
                    TextBox gvtxtwoseis = (TextBox)gvRow.FindControl("Mwithseis");

                    DataRow dr = dt.NewRow();
                    if (i == 3)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[1]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[1]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 6)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[3]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[4]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[3]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[4]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 7)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[2]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[5]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[2]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[5]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 9)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[6]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[7]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[6]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[7]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 16)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[8]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[9]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[11]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[12]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[14]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[10]["CurrentMonth"].ToDataConvertNullDouble() + dt.Rows[13]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[8]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[9]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[11]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[12]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[14]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[10]["CurrentMonth"].ToDataConvertNullDouble() + dt.Rows[13]["CurrentMonth"].ToDataConvertNullDouble())).ToString();

                    }

                    if (i == 17)
                    {
                        gvtxtmismonth.Text = ((dt.Rows[15]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[11]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtwoseis.Text = ((dt.Rows[15]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[11]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }

                    if (i == 18)
                    {

                        gvtxtmismonth.Text = (((dt.Rows[2]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtwoseis.Text = (((dt.Rows[2]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 19)
                    {
                        
                        gvtxtmismonth.Text = (((dt.Rows[6]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtwoseis.Text = (((dt.Rows[6]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 20)
                    {
                        
                        gvtxtmismonth.Text = (((dt.Rows[8]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtwoseis.Text = (((dt.Rows[8]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 21)
                    {
                        gvtxtmismonth.Text = (((dt.Rows[3]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtwoseis.Text = (((dt.Rows[3]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 22)
                    {
                        gvtxtmismonth.Text = (((dt.Rows[4]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtwoseis.Text = (((dt.Rows[4]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }

                    if (gvtxtmismonth.Text == "NaN")
                        gvtxtmismonth.Text = "0";
                    if (gvtxtwoseis.Text == "NaN")
                        gvtxtwoseis.Text = "0";



                    dr["CurrentMonth"] = gvtxtmismonth.Text;
                    dr["monthwithoutseis"] = gvtxtwoseis.Text;

                    dt.Rows.Add(dr);
                    i++;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void ConvertGridViewToDataTableAFIL()
        {
            try
            {
                int i = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("currentmonth");
                dt.Columns.Add("monthwithoutseis");


                foreach (GridViewRow gvRow in MisGrid.Rows)
                {
                    TextBox gvtxtmismonth = (TextBox)gvRow.FindControl("mismonth");
                    TextBox gvtxtmonthwithoutseis = (TextBox)gvRow.FindControl("Mwithseis");

                    DataRow dr = dt.NewRow();
                    if (i == 3)//Gross Profit
                    {
                        gvtxtmismonth.Text = ((dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[1]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[1]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 6)//Total Indirect Cost
                    {
                        gvtxtmismonth.Text = ((dt.Rows[3]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[4]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[3]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[4]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 7)//EBITDA Before CC
                    {
                        gvtxtmismonth.Text = ((dt.Rows[2]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[5]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[2]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[5]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 10)//Total Indirect Cost- Common
                    {
                        gvtxtmismonth.Text = ((dt.Rows[7]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[8]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[7]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[8]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 12)//EBITDA after CC
                    {
                        gvtxtmismonth.Text = ((dt.Rows[6]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[9]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[6]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[9]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }
                    if (i == 19)//PAT
                    {
                        gvtxtmismonth.Text = ((dt.Rows[11]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[12]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[14]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[15]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[17]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[13]["CurrentMonth"].ToDataConvertNullDouble() + dt.Rows[16]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[11]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[12]["monthwithoutseis"].ToDataConvertNullDouble()) - (dt.Rows[14]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[15]["CurrentMonth"].ToDataConvertNullDouble()) - (dt.Rows[17]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[13]["CurrentMonth"].ToDataConvertNullDouble() + dt.Rows[16]["CurrentMonth"].ToDataConvertNullDouble())).ToString();

                    }

                    if (i == 20)//Cash Profit
                    {
                        gvtxtmismonth.Text = ((dt.Rows[18]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[14]["CurrentMonth"].ToDataConvertNullDouble())).ToString();
                        gvtxtmonthwithoutseis.Text = ((dt.Rows[18]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[14]["monthwithoutseis"].ToDataConvertNullDouble())).ToString();
                    }

                    if (i == 21)//Gross Profit%
                    {
                        gvtxtmismonth.Text = (((dt.Rows[2]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtmonthwithoutseis.Text = (((dt.Rows[2]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 22)//EBITDA Before CC%
                    {
                        gvtxtmismonth.Text = (((dt.Rows[6]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtmonthwithoutseis.Text = (((dt.Rows[6]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 23)//EBITDA After CC%
                    {
                        gvtxtmismonth.Text = (((dt.Rows[11]["CurrentMonth"].ToDataConvertNullDouble()) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtmonthwithoutseis.Text = (((dt.Rows[11]["monthwithoutseis"].ToDataConvertNullDouble()) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 24)//% of Salaries to Revenue
                    {
                        gvtxtmismonth.Text = ((((dt.Rows[3]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[7]["CurrentMonth"].ToDataConvertNullDouble())) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtmonthwithoutseis.Text = ((((dt.Rows[3]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[7]["monthwithoutseis"].ToDataConvertNullDouble())) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }
                    if (i == 25)//% of Indirect Cost to Revenue
                    {
                        gvtxtmismonth.Text = ((((dt.Rows[4]["CurrentMonth"].ToDataConvertNullDouble()) + (dt.Rows[8]["CurrentMonth"].ToDataConvertNullDouble())) / (dt.Rows[0]["CurrentMonth"].ToDataConvertNullDouble())) * 100).ToString();
                        gvtxtmonthwithoutseis.Text = ((((dt.Rows[4]["monthwithoutseis"].ToDataConvertNullDouble()) + (dt.Rows[8]["monthwithoutseis"].ToDataConvertNullDouble())) / (dt.Rows[0]["monthwithoutseis"].ToDataConvertNullDouble())) * 100).ToString();
                    }

                    if (gvtxtmismonth.Text == "NaN")
                        gvtxtmismonth.Text = "0";



                    dr["CurrentMonth"] = gvtxtmismonth.Text;
                    dr["monthwithoutseis"] = gvtxtmonthwithoutseis.Text;

                    dt.Rows.Add(dr);
                    i++;
                }
            }
            catch (Exception ex)
            {
            }
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
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                MisGrid.DataSource = ds;
                MisGrid.DataBind();
            }
            else
            {
                DataSet misdata = data.getDataFromParticularMaster(mis);
                if (misdata != null)
                {
                    txtRecordFound.Text = misdata.Tables[0].Rows.Count.ToString();

                    MisGrid.DataSource = misdata.Tables[0];
                    MisGrid.DataBind();
                }
                else
                {
                    MisGrid.DataBind();
                    txtRecordFound.Text = "0";
                }
            }
        }

        protected void Drpdivisin_SelectedIndexChanged(object sender, EventArgs e)
        {
            MisGrid.DataBind();
            if (Drpdivisin.SelectedValue == string.Empty)
            {
                Drpafildivision.Items.Clear();
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
                //Drpafildivision.Items.Insert(0, new ListItem("--Select--", string.Empty));

                Drpafildivision.Visible = true;
                lblSubDivision.Visible = true;
                lblSubdivisionStar.Visible = true;

            }
            else
            {
                Drpafildivision.Items.Clear();
                // Drpafildivision.Items.Insert(0, new ListItem("--Select--", string.Empty));
                Drpafildivision.Visible = false;
                lblSubDivision.Visible = false;
                lblSubdivisionStar.Visible = false;
            }
        }
        protected void MisGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MisGrid.PageIndex = e.NewPageIndex;
        }

        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MisActualList.aspx");
        }

        protected void MisGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label gvlblDesc = (Label)e.Row.FindControl("mpmdescription");
                if (gvlblDesc.Text == "Gross Profit")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "Total Indirect Cost")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                }
                if (gvlblDesc.Text == "EBITDA Before CC")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "EBITDA after CC")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "PAT")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "Cash Profit")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "Gross Profit%")
                {
                    e.Row.Enabled = false;
                }

                if (gvlblDesc.Text == "EBITDA Before CC%")
                {
                    e.Row.Enabled = false;
                }
                if (gvlblDesc.Text == "EBITDA After CC%")
                {
                    e.Row.Enabled = false;
                }
                if (gvlblDesc.Text == "% of Salaries to Revenue")
                {
                    e.Row.Enabled = false;
                }
                if (gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    e.Row.Enabled = false;
                }
                if (Drpdivisin.SelectedValue == "1")
                {
                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                    if (gvlblDesc.Text == "Corporate Cost")
                    {
                        e.Row.Enabled = false;
                    }
                }
            }
        }

        protected void Drpmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            MisGrid.DataBind();
        }

    }
}