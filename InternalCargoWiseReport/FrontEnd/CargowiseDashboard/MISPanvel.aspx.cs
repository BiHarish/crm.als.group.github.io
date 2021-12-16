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
    public partial class MISPanvel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

        //void bindGrd()
        //{
        //    MisParticularsTransactionData _data = new MisParticularsTransactionData();
        //    MisParticularsTransactionDto request = new MisParticularsTransactionDto();
        //    request.mptfinancialyear = drpFinancialYear.SelectedValue;
        //    request.mptdivisionid = drpDivision.SelectedValue.ToNullLong();
        //    request.mptmtyid = drpType.SelectedValue.ToNullLong();
        //    DataSet ds = _data.getData(request);
        //    if(ds!=null)
        //    {
        //        txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
        //        gvList.DataSource = ds.Tables[0];
        //        gvList.DataBind();
        //    }
        //    else
        //    {
        //        gvList.DataBind();
        //        txtRecordFound.Text = "0";
        //    }
        //}

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
            DataSet ds = _data.getDataFromBudgetTable(request);
            if (ds != null)
            {
                txtRecordFound.Text = ds.Tables[0].Rows.Count.ToString();
                gvList.DataSource = ds.Tables[0];
                gvList.DataBind();
            }
            else
            {
                DataSet budgetMasterData = _data.getDataFromParticularMaster(request);
                if (budgetMasterData != null)
                {
                    txtRecordFound.Text = budgetMasterData.Tables[0].Rows.Count.ToString();
                    gvList.DataSource = budgetMasterData.Tables[0];
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataBind();
                    txtRecordFound.Text = "0";
                }

            }
        }

        void saveBudgetData()
        {
            MisBudgetData _data = new MisBudgetData();
            foreach (GridViewRow gvRow in gvList.Rows)
            {
                Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                Label gvlblparitcularID = (Label)gvRow.FindControl("gvlblmpmid");
                Label gvlblDivisionID = (Label)gvRow.FindControl("mpmDivisionID");
                Label gvlblTypeID = (Label)gvRow.FindControl("TypeID");
                TextBox gvtxtFinYear = (TextBox)gvRow.FindControl("gvtxtmptfinancialyear");
                TextBox gvtxtmptApr = (TextBox)gvRow.FindControl("gvtxtmptApr");
                TextBox gvtxtmptMay = (TextBox)gvRow.FindControl("gvtxtmptMay");
                TextBox gvtxtmptJun = (TextBox)gvRow.FindControl("gvtxtmptJun");
                TextBox gvtxtJul = (TextBox)gvRow.FindControl("gvtxtJul");
                TextBox gvtxtAug = (TextBox)gvRow.FindControl("gvtxtAug");
                TextBox gvtxtmptSep = (TextBox)gvRow.FindControl("gvtxtmptSep");
                TextBox gvtxtmptOct = (TextBox)gvRow.FindControl("gvtxtmptOct");
                TextBox gvtxtNov = (TextBox)gvRow.FindControl("gvtxtNov");
                TextBox gvtxtmptDec = (TextBox)gvRow.FindControl("gvtxtmptDec");
                TextBox gvtxtmptJan = (TextBox)gvRow.FindControl("gvtxtmptJan");
                TextBox gvtxtFeb = (TextBox)gvRow.FindControl("gvtxtFeb");
                TextBox gvtxtMar = (TextBox)gvRow.FindControl("gvtxtMar");
                TextBox gvtxtmptTotal = (TextBox)gvRow.FindControl("gvtxtmptTotal");
                TextBox gvtxtmptYtd = (TextBox)gvRow.FindControl("gvtxtmptYtd");

                MisBudgetDto request = new MisBudgetDto();
                request.mbid = gvlblID.Text.ToNullLong();
                if (drpSubdivision.Items.Count > 0)
                {
                    request.mbdivisionid = drpSubdivision.SelectedValue.ToNullLong();
                }
                else
                {
                    request.mbdivisionid = drpDivision.SelectedValue.ToNullLong();
                }

                request.mbmtyid = drpType.SelectedValue.ToNullLong();
                request.mbmpmid = gvlblparitcularID.Text.ToNullLong();
                request.mbfinancialyear = gvtxtFinYear.Text;
                request.mbApr = gvtxtmptApr.Text.ToNullDouble();
                request.mbMay = gvtxtmptMay.Text.ToNullDouble();
                request.mbJun = gvtxtmptJun.Text.ToNullDouble();
                request.mbJul = gvtxtJul.Text.ToNullDouble();
                request.mbAug = gvtxtAug.Text.ToNullDouble();
                request.mbSep = gvtxtmptSep.Text.ToNullDouble();
                request.mbOct = gvtxtmptOct.Text.ToNullDouble();
                request.mbNov = gvtxtNov.Text.ToNullDouble();
                request.mbDec = gvtxtmptDec.Text.ToNullDouble();
                request.mbJan = gvtxtmptJan.Text.ToNullDouble();
                request.mbFeb = gvtxtFeb.Text.ToNullDouble();
                request.mbMar = gvtxtMar.Text.ToNullDouble();
                request.mbTotal = gvtxtmptTotal.Text.ToNullDouble();
                request.mbYtd = gvtxtmptYtd.Text.ToNullDouble();
                request.mbcreateby = LovelySession.Lovely.User.Id;

                if (gvlblID.Text.ToLong() == 0)
                {
                    long returnID = _data.Insert(request);
                    if (returnID > 0)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Server Error", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    if (_data.Update(request))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully Updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Server Error", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }



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

        bool submitValidation()
        {
            if (gvList.Rows.Count == 0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please search first!!", "Error!", Toastr.ToastPosition.TopCenter, true);
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

        void ConvertGridViewToDataTableForDefault()
        {
            try
            {
                int i = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("FinancialYear");
                dt.Columns.Add("Apr");
                dt.Columns.Add("May");
                dt.Columns.Add("Jun");
                dt.Columns.Add("Jul");
                dt.Columns.Add("Aug");
                dt.Columns.Add("Sep");
                dt.Columns.Add("Oct");
                dt.Columns.Add("Nov");
                dt.Columns.Add("Dec");
                dt.Columns.Add("Jan");
                dt.Columns.Add("Feb");
                dt.Columns.Add("Mar");
                dt.Columns.Add("Total");
                dt.Columns.Add("YTD");

                foreach (GridViewRow gvRow in gvList.Rows)
                {
                    Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                    Label gvlblparitcularID = (Label)gvRow.FindControl("gvlblmpmid");
                    Label gvlblDivisionID = (Label)gvRow.FindControl("mpmDivisionID");
                    Label gvlblTypeID = (Label)gvRow.FindControl("TypeID");
                    TextBox gvtxtFinYear = (TextBox)gvRow.FindControl("gvtxtmptfinancialyear");
                    TextBox gvtxtmptApr = (TextBox)gvRow.FindControl("gvtxtmptApr");
                    TextBox gvtxtmptMay = (TextBox)gvRow.FindControl("gvtxtmptMay");
                    TextBox gvtxtmptJun = (TextBox)gvRow.FindControl("gvtxtmptJun");
                    TextBox gvtxtJul = (TextBox)gvRow.FindControl("gvtxtJul");
                    TextBox gvtxtAug = (TextBox)gvRow.FindControl("gvtxtAug");
                    TextBox gvtxtmptSep = (TextBox)gvRow.FindControl("gvtxtmptSep");
                    TextBox gvtxtmptOct = (TextBox)gvRow.FindControl("gvtxtmptOct");
                    TextBox gvtxtNov = (TextBox)gvRow.FindControl("gvtxtNov");
                    TextBox gvtxtmptDec = (TextBox)gvRow.FindControl("gvtxtmptDec");
                    TextBox gvtxtmptJan = (TextBox)gvRow.FindControl("gvtxtmptJan");
                    TextBox gvtxtFeb = (TextBox)gvRow.FindControl("gvtxtFeb");
                    TextBox gvtxtMar = (TextBox)gvRow.FindControl("gvtxtMar");
                    TextBox gvtxtmptTotal = (TextBox)gvRow.FindControl("gvtxtmptTotal");
                    TextBox gvtxtmptYtd = (TextBox)gvRow.FindControl("gvtxtmptYtd");
                    DataRow dr = dt.NewRow();

                    dr["Description"] = gvlblTypeID.Text;
                    dr["FinancialYear"] = gvtxtFinYear.Text;
                    dr["Apr"] = gvtxtmptApr.Text;
                    dr["May"] = gvtxtmptMay.Text;
                    dr["Jun"] = gvtxtmptJun.Text;
                    dr["Jul"] = gvtxtJul.Text;
                    dr["Aug"] = gvtxtAug.Text;
                    dr["Sep"] = gvtxtmptSep.Text;
                    dr["Oct"] = gvtxtmptOct.Text;
                    dr["Nov"] = gvtxtNov.Text;
                    dr["Dec"] = gvtxtmptDec.Text;
                    dr["Jan"] = gvtxtmptJan.Text;
                    dr["Feb"] = gvtxtFeb.Text;
                    dr["Mar"] = gvtxtMar.Text;
                    dr["Total"] = gvtxtmptTotal.Text;
                    dr["YTD"] = gvtxtmptYtd.Text;
                    dt.Rows.Add(dr);

                    if (i == 1)//Revenue
                    {
                        gvtxtmptTotal.Text = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) + (dt.Rows[0]["May"].ToDataConvertDouble()) + (dt.Rows[0]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jul"].ToDataConvertDouble()) + (dt.Rows[0]["Aug"].ToDataConvertDouble()) + (dt.Rows[0]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[0]["Oct"].ToDataConvertDouble()) + (dt.Rows[0]["Nov"].ToDataConvertDouble()) + (dt.Rows[0]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jan"].ToDataConvertDouble()) + (dt.Rows[0]["Feb"].ToDataConvertDouble()) + (dt.Rows[0]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                    if (i == 2)//Direct Cost
                    {
                        gvtxtmptTotal.Text = ((dt.Rows[1]["Apr"].ToDataConvertDouble()) + (dt.Rows[1]["May"].ToDataConvertDouble()) + (dt.Rows[1]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jul"].ToDataConvertDouble()) + (dt.Rows[1]["Aug"].ToDataConvertDouble()) + (dt.Rows[1]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[1]["Oct"].ToDataConvertDouble()) + (dt.Rows[1]["Nov"].ToDataConvertDouble()) + (dt.Rows[1]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jan"].ToDataConvertDouble()) + (dt.Rows[1]["Feb"].ToDataConvertDouble()) + (dt.Rows[1]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }

                    if (i == 3)//Gross Profit
                    {
                        gvtxtmptApr.Text = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) - (dt.Rows[1]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[0]["May"].ToDataConvertDouble()) - (dt.Rows[1]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[0]["Jun"].ToDataConvertDouble()) - (dt.Rows[1]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[0]["Jul"].ToDataConvertDouble()) - (dt.Rows[1]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[0]["Aug"].ToDataConvertDouble()) - (dt.Rows[1]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[0]["Sep"].ToDataConvertDouble()) - (dt.Rows[1]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[0]["Oct"].ToDataConvertDouble()) - (dt.Rows[1]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[0]["Nov"].ToDataConvertDouble()) - (dt.Rows[1]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[0]["Dec"].ToDataConvertDouble()) - (dt.Rows[1]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[0]["Jan"].ToDataConvertDouble()) - (dt.Rows[1]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[0]["Feb"].ToDataConvertDouble()) - (dt.Rows[1]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[0]["Mar"].ToDataConvertDouble()) - (dt.Rows[1]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text = ((dt.Rows[0]["Total"].ToDataConvertDouble()) - (dt.Rows[1]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 4)//Salaries
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[3]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[3]["May"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[3]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[3]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[3]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 5)//Other Indirect exp
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[4]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[4]["May"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[4]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 6)//Total iNdirect Cost
                    {
                        gvtxtmptApr.Text = ((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[4]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[4]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[4]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text = ((dt.Rows[3]["Total"].ToDataConvertDouble()) + (dt.Rows[4]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 7)
                    {
                        gvtxtmptApr.Text = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) - (dt.Rows[5]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[2]["May"].ToDataConvertDouble()) - (dt.Rows[5]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[2]["Jun"].ToDataConvertDouble()) - (dt.Rows[5]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[2]["Jul"].ToDataConvertDouble()) - (dt.Rows[5]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[2]["Aug"].ToDataConvertDouble()) - (dt.Rows[5]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[2]["Sep"].ToDataConvertDouble()) - (dt.Rows[5]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[2]["Oct"].ToDataConvertDouble()) - (dt.Rows[5]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[2]["Nov"].ToDataConvertDouble()) - (dt.Rows[5]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[2]["Dec"].ToDataConvertDouble()) - (dt.Rows[5]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[2]["Jan"].ToDataConvertDouble()) - (dt.Rows[5]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[2]["Feb"].ToDataConvertDouble()) - (dt.Rows[5]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[2]["Mar"].ToDataConvertDouble()) - (dt.Rows[5]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text = ((dt.Rows[2]["Total"].ToDataConvertDouble()) - (dt.Rows[5]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 8)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[7]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[7]["May"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jul"].ToDataConvertDouble()) + (dt.Rows[7]["Aug"].ToDataConvertDouble()) + (dt.Rows[7]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[7]["Oct"].ToDataConvertDouble()) + (dt.Rows[7]["Nov"].ToDataConvertDouble()) + (dt.Rows[7]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jan"].ToDataConvertDouble()) + (dt.Rows[7]["Feb"].ToDataConvertDouble()) + (dt.Rows[7]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 9)
                    {
                        gvtxtmptApr.Text = ((dt.Rows[6]["Apr"].ToDataConvertDouble()) - (dt.Rows[7]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[6]["May"].ToDataConvertDouble()) - (dt.Rows[7]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[6]["Jun"].ToDataConvertDouble()) - (dt.Rows[7]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[6]["Jul"].ToDataConvertDouble()) - (dt.Rows[7]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[6]["Aug"].ToDataConvertDouble()) - (dt.Rows[7]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[6]["Sep"].ToDataConvertDouble()) - (dt.Rows[7]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[6]["Oct"].ToDataConvertDouble()) - (dt.Rows[7]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[6]["Nov"].ToDataConvertDouble()) - (dt.Rows[7]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[6]["Dec"].ToDataConvertDouble()) - (dt.Rows[7]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[6]["Jan"].ToDataConvertDouble()) - (dt.Rows[7]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[6]["Feb"].ToDataConvertDouble()) - (dt.Rows[7]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[6]["Mar"].ToDataConvertDouble()) - (dt.Rows[7]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text = ((dt.Rows[6]["Total"].ToDataConvertDouble()) - (dt.Rows[7]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 10)
                    {

                        gvtxtmptTotal.Text =
                             ((dt.Rows[9]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[9]["May"].ToDataConvertDouble())
                            + (dt.Rows[9]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[9]["Jul"].ToDataConvertDouble()) + (dt.Rows[9]["Aug"].ToDataConvertDouble()) + (dt.Rows[9]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[9]["Oct"].ToDataConvertDouble()) + (dt.Rows[9]["Nov"].ToDataConvertDouble()) + (dt.Rows[9]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[9]["Jan"].ToDataConvertDouble()) + (dt.Rows[9]["Feb"].ToDataConvertDouble()) + (dt.Rows[9]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 11)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[10]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[10]["May"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[10]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[10]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[10]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[10]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[10]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[10]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[10]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }

                    if (i == 12)
                    {
                        gvtxtmptTotal.Text =
                            ((dt.Rows[11]["Apr"].ToDataConvertDouble())
                           + (dt.Rows[11]["May"].ToDataConvertDouble())
                           + (dt.Rows[11]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[11]["Jul"].ToDataConvertDouble())
                           + (dt.Rows[11]["Aug"].ToDataConvertDouble())
                           + (dt.Rows[11]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[11]["Oct"].ToDataConvertDouble())
                           + (dt.Rows[11]["Nov"].ToDataConvertDouble())
                           + (dt.Rows[11]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[11]["Jan"].ToDataConvertDouble())
                           + (dt.Rows[11]["Feb"].ToDataConvertDouble())
                           + (dt.Rows[11]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }
                    if (i == 13)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[12]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[12]["May"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[12]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[12]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[12]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[12]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[12]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[12]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[12]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 14)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[13]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[13]["May"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[13]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[13]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[13]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[13]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[13]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[13]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[13]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 15)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[14]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[14]["May"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[14]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[14]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[14]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[14]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[14]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[14]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[14]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 16)
                    {
                        gvtxtmptApr.Text = ((dt.Rows[8]["Apr"].ToDataConvertDouble()) - (dt.Rows[9]["Apr"].ToDataConvertDouble()) - (dt.Rows[11]["Apr"].ToDataConvertDouble()) - (dt.Rows[12]["Apr"].ToDataConvertDouble()) - (dt.Rows[14]["Apr"].ToDataConvertDouble()) + (dt.Rows[10]["Apr"].ToDataConvertDouble() + dt.Rows[13]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[8]["May"].ToDataConvertDouble()) - (dt.Rows[9]["May"].ToDataConvertDouble()) - (dt.Rows[11]["May"].ToDataConvertDouble()) - (dt.Rows[12]["May"].ToDataConvertDouble()) - (dt.Rows[14]["May"].ToDataConvertDouble()) + (dt.Rows[9]["May"].ToDataConvertDouble() + dt.Rows[13]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[8]["Jun"].ToDataConvertDouble()) - (dt.Rows[9]["Jun"].ToDataConvertDouble()) - (dt.Rows[11]["Jun"].ToDataConvertDouble()) - (dt.Rows[12]["Jun"].ToDataConvertDouble()) - (dt.Rows[14]["Jun"].ToDataConvertDouble()) + (dt.Rows[9]["Jun"].ToDataConvertDouble() + dt.Rows[13]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[8]["Jul"].ToDataConvertDouble()) - (dt.Rows[9]["Jul"].ToDataConvertDouble()) - (dt.Rows[11]["Jul"].ToDataConvertDouble()) - (dt.Rows[12]["Jul"].ToDataConvertDouble()) - (dt.Rows[14]["Jul"].ToDataConvertDouble()) + (dt.Rows[9]["Jul"].ToDataConvertDouble() + dt.Rows[13]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[8]["Aug"].ToDataConvertDouble()) - (dt.Rows[9]["Aug"].ToDataConvertDouble()) - (dt.Rows[11]["Aug"].ToDataConvertDouble()) - (dt.Rows[12]["Aug"].ToDataConvertDouble()) - (dt.Rows[14]["Aug"].ToDataConvertDouble()) + (dt.Rows[9]["Aug"].ToDataConvertDouble() + dt.Rows[13]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[8]["Sep"].ToDataConvertDouble()) - (dt.Rows[9]["Sep"].ToDataConvertDouble()) - (dt.Rows[11]["Sep"].ToDataConvertDouble()) - (dt.Rows[12]["Sep"].ToDataConvertDouble()) - (dt.Rows[14]["Sep"].ToDataConvertDouble()) + (dt.Rows[9]["Sep"].ToDataConvertDouble() + dt.Rows[13]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[8]["Oct"].ToDataConvertDouble()) - (dt.Rows[9]["Oct"].ToDataConvertDouble()) - (dt.Rows[11]["Oct"].ToDataConvertDouble()) - (dt.Rows[12]["Oct"].ToDataConvertDouble()) - (dt.Rows[14]["Oct"].ToDataConvertDouble()) + (dt.Rows[9]["Oct"].ToDataConvertDouble() + dt.Rows[13]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[8]["Nov"].ToDataConvertDouble()) - (dt.Rows[9]["Nov"].ToDataConvertDouble()) - (dt.Rows[11]["Nov"].ToDataConvertDouble()) - (dt.Rows[12]["Nov"].ToDataConvertDouble()) - (dt.Rows[14]["Nov"].ToDataConvertDouble()) + (dt.Rows[9]["Nov"].ToDataConvertDouble() + dt.Rows[13]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[8]["Dec"].ToDataConvertDouble()) - (dt.Rows[9]["Dec"].ToDataConvertDouble()) - (dt.Rows[11]["Dec"].ToDataConvertDouble()) - (dt.Rows[12]["Dec"].ToDataConvertDouble()) - (dt.Rows[14]["Dec"].ToDataConvertDouble()) + (dt.Rows[9]["Dec"].ToDataConvertDouble() + dt.Rows[13]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[8]["Jan"].ToDataConvertDouble()) - (dt.Rows[9]["Jan"].ToDataConvertDouble()) - (dt.Rows[11]["Jan"].ToDataConvertDouble()) - (dt.Rows[12]["Jan"].ToDataConvertDouble()) - (dt.Rows[14]["Jan"].ToDataConvertDouble()) + (dt.Rows[9]["Jan"].ToDataConvertDouble() + dt.Rows[13]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[8]["Feb"].ToDataConvertDouble()) - (dt.Rows[9]["Feb"].ToDataConvertDouble()) - (dt.Rows[11]["Feb"].ToDataConvertDouble()) - (dt.Rows[12]["Feb"].ToDataConvertDouble()) - (dt.Rows[14]["Feb"].ToDataConvertDouble()) + (dt.Rows[9]["Feb"].ToDataConvertDouble() + dt.Rows[13]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[8]["Mar"].ToDataConvertDouble()) - (dt.Rows[9]["Mar"].ToDataConvertDouble()) - (dt.Rows[11]["Mar"].ToDataConvertDouble()) - (dt.Rows[12]["Mar"].ToDataConvertDouble()) - (dt.Rows[14]["Mar"].ToDataConvertDouble()) + (dt.Rows[9]["Mar"].ToDataConvertDouble() + dt.Rows[13]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                           ((dt.Rows[15]["Apr"].ToDataConvertDouble())
                          + (dt.Rows[15]["May"].ToDataConvertDouble())
                          + (dt.Rows[15]["Jun"].ToDataConvertDouble())
                          + (dt.Rows[15]["Jul"].ToDataConvertDouble())
                          + (dt.Rows[15]["Aug"].ToDataConvertDouble())
                          + (dt.Rows[15]["Sep"].ToDataConvertDouble())
                          + (dt.Rows[15]["Oct"].ToDataConvertDouble())
                          + (dt.Rows[15]["Nov"].ToDataConvertDouble())
                          + (dt.Rows[15]["Dec"].ToDataConvertDouble())
                          + (dt.Rows[15]["Jan"].ToDataConvertDouble())
                          + (dt.Rows[15]["Feb"].ToDataConvertDouble())
                          + (dt.Rows[15]["Mar"].ToDataConvertDouble())
                         ).ToString();

                    }

                    if (i == 17)
                    {
                        gvtxtmptApr.Text = ((dt.Rows[15]["Apr"].ToDataConvertDouble()) + (dt.Rows[11]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[15]["May"].ToDataConvertDouble()) + (dt.Rows[11]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[15]["Jun"].ToDataConvertDouble()) + (dt.Rows[11]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[15]["Jul"].ToDataConvertDouble()) + (dt.Rows[11]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[15]["Aug"].ToDataConvertDouble()) + (dt.Rows[11]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[15]["Sep"].ToDataConvertDouble()) + (dt.Rows[11]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[15]["Oct"].ToDataConvertDouble()) + (dt.Rows[11]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[15]["Nov"].ToDataConvertDouble()) + (dt.Rows[11]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[15]["Dec"].ToDataConvertDouble()) + (dt.Rows[11]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[15]["Jan"].ToDataConvertDouble()) + (dt.Rows[11]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[15]["Feb"].ToDataConvertDouble()) + (dt.Rows[11]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[15]["Mar"].ToDataConvertDouble()) + (dt.Rows[11]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                            ((dt.Rows[16]["Apr"].ToDataConvertDouble())
                           + (dt.Rows[16]["May"].ToDataConvertDouble())
                           + (dt.Rows[16]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[16]["Jul"].ToDataConvertDouble())
                           + (dt.Rows[16]["Aug"].ToDataConvertDouble())
                           + (dt.Rows[16]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[16]["Oct"].ToDataConvertDouble())
                           + (dt.Rows[16]["Nov"].ToDataConvertDouble())
                           + (dt.Rows[16]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[16]["Jan"].ToDataConvertDouble())
                           + (dt.Rows[16]["Feb"].ToDataConvertDouble())
                           + (dt.Rows[16]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }

                    if (i == 18)
                    {
                        gvtxtmptApr.Text = (((dt.Rows[2]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[2]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[2]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[2]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[2]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[2]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[2]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[2]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[2]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[2]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[2]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[2]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();


                        gvtxtmptTotal.Text = (((dt.Rows[2]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();


                    }
                    if (i == 19)
                    {
                        gvtxtmptApr.Text = (((dt.Rows[6]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[6]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[6]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[6]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[6]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[6]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[6]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[6]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[6]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[6]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[6]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[6]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();


                        gvtxtmptTotal.Text = (((dt.Rows[6]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();


                    }
                    if (i == 20)
                    {
                        gvtxtmptApr.Text = (((dt.Rows[8]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[8]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[8]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[8]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[8]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[8]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[8]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[8]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[8]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[8]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[8]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[8]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[8]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();


                    }
                    if (i == 21)
                    {
                        gvtxtmptApr.Text = (((dt.Rows[3]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[3]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[3]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[3]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[3]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[3]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[3]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[3]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[3]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[3]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[3]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[3]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[3]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();


                    }
                    if (i == 22)
                    {
                        gvtxtmptApr.Text = (((dt.Rows[4]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[4]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[4]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[4]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[4]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[4]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[4]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[4]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[4]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[4]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[4]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[4]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[4]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();


                        if (dt.Rows[21]["Mar"].ToString() == "NaN" || dt.Rows[21]["Mar"].ToString() == "-Infinity")
                        {
                            dt.Rows[21]["Mar"] = "0";
                        }
                    }

                    if (gvtxtmptApr.Text == "NaN" || gvtxtmptApr.Text == "-Infinity" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptApr.Text = "0";

                    if (gvtxtmptMay.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptMay.Text = "0";

                    if (gvtxtmptJun.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptJun.Text = "0";

                    if (gvtxtJul.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtJul.Text = "0";
                    if (gvtxtAug.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtAug.Text = "0";
                    if (gvtxtmptSep.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptSep.Text = "0";
                    if (gvtxtmptOct.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptOct.Text = "0";


                    if (gvtxtNov.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtNov.Text = "0";

                    if (gvtxtmptDec.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptDec.Text = "0";

                    if (gvtxtmptJan.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptJan.Text = "0";

                    if (gvtxtFeb.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtFeb.Text = "0";

                    if (gvtxtMar.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtMar.Text = "0";
                    if (gvtxtmptTotal.Text == "NaN" || gvtxtmptTotal.Text.ToLower() == "infinity")
                        gvtxtmptTotal.Text = "0";




                    i++;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void ConvertGridViewToDataTableForAFIL()
        {
            try
            {
                int i = 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("FinancialYear");
                dt.Columns.Add("Apr");
                dt.Columns.Add("May");
                dt.Columns.Add("Jun");
                dt.Columns.Add("Jul");
                dt.Columns.Add("Aug");
                dt.Columns.Add("Sep");
                dt.Columns.Add("Oct");
                dt.Columns.Add("Nov");
                dt.Columns.Add("Dec");
                dt.Columns.Add("Jan");
                dt.Columns.Add("Feb");
                dt.Columns.Add("Mar");
                dt.Columns.Add("Total");
                dt.Columns.Add("YTD");

                foreach (GridViewRow gvRow in gvList.Rows)
                {
                    Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                    Label gvlblparitcularID = (Label)gvRow.FindControl("gvlblmpmid");
                    Label gvlblDivisionID = (Label)gvRow.FindControl("mpmDivisionID");
                    Label gvlblTypeID = (Label)gvRow.FindControl("TypeID");
                    TextBox gvtxtFinYear = (TextBox)gvRow.FindControl("gvtxtmptfinancialyear");
                    TextBox gvtxtmptApr = (TextBox)gvRow.FindControl("gvtxtmptApr");
                    TextBox gvtxtmptMay = (TextBox)gvRow.FindControl("gvtxtmptMay");
                    TextBox gvtxtmptJun = (TextBox)gvRow.FindControl("gvtxtmptJun");
                    TextBox gvtxtJul = (TextBox)gvRow.FindControl("gvtxtJul");
                    TextBox gvtxtAug = (TextBox)gvRow.FindControl("gvtxtAug");
                    TextBox gvtxtmptSep = (TextBox)gvRow.FindControl("gvtxtmptSep");
                    TextBox gvtxtmptOct = (TextBox)gvRow.FindControl("gvtxtmptOct");
                    TextBox gvtxtNov = (TextBox)gvRow.FindControl("gvtxtNov");
                    TextBox gvtxtmptDec = (TextBox)gvRow.FindControl("gvtxtmptDec");
                    TextBox gvtxtmptJan = (TextBox)gvRow.FindControl("gvtxtmptJan");
                    TextBox gvtxtFeb = (TextBox)gvRow.FindControl("gvtxtFeb");
                    TextBox gvtxtMar = (TextBox)gvRow.FindControl("gvtxtMar");
                    TextBox gvtxtmptTotal = (TextBox)gvRow.FindControl("gvtxtmptTotal");
                    TextBox gvtxtmptYtd = (TextBox)gvRow.FindControl("gvtxtmptYtd");
                    DataRow dr = dt.NewRow();

                    dr["Description"] = gvlblTypeID.Text;
                    dr["FinancialYear"] = gvtxtFinYear.Text;
                    if (gvtxtmptApr.Text == string.Empty)
                    {
                        dr["Apr"] = "0";
                    }
                    else
                    {
                        dr["Apr"] = gvtxtmptApr.Text;
                    }

                    if (gvtxtmptMay.Text == string.Empty)
                    {
                        dr["May"] = "0";
                    }
                    else
                    {
                        dr["May"] = gvtxtmptMay.Text;
                    }
                    if (gvtxtmptJun.Text == string.Empty)
                    {
                        dr["Jun"] = "0";
                    }
                    else
                    {
                        dr["Jun"] = gvtxtmptJun.Text;
                    }
                    if (gvtxtJul.Text == string.Empty)
                    {
                        dr["Jul"] = "0";
                    }
                    else
                    {
                        dr["Jul"] = gvtxtJul.Text;
                    }
                    if (gvtxtAug.Text == string.Empty)
                    {
                        dr["Aug"] = "0";
                    }
                    else
                    {
                        dr["Aug"] = gvtxtAug.Text;
                    }
                    if (gvtxtmptSep.Text == string.Empty)
                    {
                        dr["Sep"] = "0";
                    }
                    else
                    {
                        dr["Sep"] = gvtxtmptSep.Text;
                    }
                    if (gvtxtmptOct.Text == string.Empty)
                    {
                        dr["Oct"] = "0";
                    }
                    else
                    {
                        dr["Oct"] = gvtxtmptOct.Text;
                    }

                    if (gvtxtNov.Text == string.Empty)
                    {
                        dr["Nov"] = "0";
                    }
                    else
                    {
                        dr["Nov"] = gvtxtNov.Text;
                    }
                    if (gvtxtmptDec.Text == string.Empty)
                    {
                        dr["Dec"] = "0";
                    }
                    else
                    {
                        dr["Dec"] = gvtxtmptDec.Text;
                    }
                    if (gvtxtmptJan.Text == string.Empty)
                    {
                        dr["Jan"] = "0";
                    }
                    else
                    {
                        dr["Jan"] = gvtxtmptJan.Text;
                    }
                    if (gvtxtFeb.Text == string.Empty)
                    {
                        dr["Feb"] = "0";
                    }
                    else
                    {
                        dr["Feb"] = gvtxtFeb.Text;
                    }
                    if (gvtxtMar.Text == string.Empty)
                    {
                        dr["Mar"] = "0";
                    }
                    else
                    {
                        dr["Mar"] = gvtxtMar.Text;
                    }

                    dr["Total"] = gvtxtmptTotal.Text;
                    dr["YTD"] = gvtxtmptYtd.Text;
                    dt.Rows.Add(dr);

                    if (i == 1)
                    {
                        gvtxtmptTotal.Text = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) + (dt.Rows[0]["May"].ToDataConvertDouble()) + (dt.Rows[0]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jul"].ToDataConvertDouble()) + (dt.Rows[0]["Aug"].ToDataConvertDouble()) + (dt.Rows[0]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[0]["Oct"].ToDataConvertDouble()) + (dt.Rows[0]["Nov"].ToDataConvertDouble()) + (dt.Rows[0]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jan"].ToDataConvertDouble()) + (dt.Rows[0]["Feb"].ToDataConvertDouble()) + (dt.Rows[0]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                    if (i == 2)
                    {
                        gvtxtmptTotal.Text = ((dt.Rows[1]["Apr"].ToDataConvertDouble()) + (dt.Rows[1]["May"].ToDataConvertDouble()) + (dt.Rows[1]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jul"].ToDataConvertDouble()) + (dt.Rows[1]["Aug"].ToDataConvertDouble()) + (dt.Rows[1]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[1]["Oct"].ToDataConvertDouble()) + (dt.Rows[1]["Nov"].ToDataConvertDouble()) + (dt.Rows[1]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jan"].ToDataConvertDouble()) + (dt.Rows[1]["Feb"].ToDataConvertDouble()) + (dt.Rows[1]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }

                    if (i == 3)//Gross Profit
                    {
                        gvtxtmptApr.Text = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) - (dt.Rows[1]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[0]["May"].ToDataConvertDouble()) - (dt.Rows[1]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[0]["Jun"].ToDataConvertDouble()) - (dt.Rows[1]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[0]["Jul"].ToDataConvertDouble()) - (dt.Rows[1]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[0]["Aug"].ToDataConvertDouble()) - (dt.Rows[1]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[0]["Sep"].ToDataConvertDouble()) - (dt.Rows[1]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[0]["Oct"].ToDataConvertDouble()) - (dt.Rows[1]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[0]["Nov"].ToDataConvertDouble()) - (dt.Rows[1]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[0]["Dec"].ToDataConvertDouble()) - (dt.Rows[1]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[0]["Jan"].ToDataConvertDouble()) - (dt.Rows[1]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[0]["Feb"].ToDataConvertDouble()) - (dt.Rows[1]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[0]["Mar"].ToDataConvertDouble()) - (dt.Rows[1]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) + (dt.Rows[2]["May"].ToDataConvertDouble()) + (dt.Rows[2]["Jun"].ToDataConvertDouble())
                          + (dt.Rows[2]["Jul"].ToDataConvertDouble()) + (dt.Rows[2]["Aug"].ToDataConvertDouble()) + (dt.Rows[2]["Sep"].ToDataConvertDouble())
                          + (dt.Rows[2]["Oct"].ToDataConvertDouble()) + (dt.Rows[2]["Nov"].ToDataConvertDouble()) + (dt.Rows[2]["Dec"].ToDataConvertDouble())
                          + (dt.Rows[2]["Jan"].ToDataConvertDouble()) + (dt.Rows[2]["Feb"].ToDataConvertDouble()) + (dt.Rows[2]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }
                    if (i == 4)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[3]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[3]["May"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[3]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[3]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[3]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 5)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[4]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[4]["May"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[4]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 6)//Total Indirect Cost
                    {
                        gvtxtmptApr.Text = ((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[4]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[4]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[4]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                            ((dt.Rows[5]["Apr"].ToDataConvertDouble())
                           + (dt.Rows[5]["May"].ToDataConvertDouble())
                           + (dt.Rows[5]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[5]["Jul"].ToDataConvertDouble()) + (dt.Rows[5]["Aug"].ToDataConvertDouble()) + (dt.Rows[5]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[5]["Oct"].ToDataConvertDouble()) + (dt.Rows[5]["Nov"].ToDataConvertDouble()) + (dt.Rows[5]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[5]["Jan"].ToDataConvertDouble()) + (dt.Rows[5]["Feb"].ToDataConvertDouble()) + (dt.Rows[5]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }
                    if (i == 7)//EBITDA Before CC
                    {
                        gvtxtmptApr.Text = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) - (dt.Rows[5]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[2]["May"].ToDataConvertDouble()) - (dt.Rows[5]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[2]["Jun"].ToDataConvertDouble()) - (dt.Rows[5]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[2]["Jul"].ToDataConvertDouble()) - (dt.Rows[5]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[2]["Aug"].ToDataConvertDouble()) - (dt.Rows[5]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[2]["Sep"].ToDataConvertDouble()) - (dt.Rows[5]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[2]["Oct"].ToDataConvertDouble()) - (dt.Rows[5]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[2]["Nov"].ToDataConvertDouble()) - (dt.Rows[5]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[2]["Dec"].ToDataConvertDouble()) - (dt.Rows[5]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[2]["Jan"].ToDataConvertDouble()) - (dt.Rows[5]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[2]["Feb"].ToDataConvertDouble()) - (dt.Rows[5]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[2]["Mar"].ToDataConvertDouble()) - (dt.Rows[5]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                            ((dt.Rows[6]["Apr"].ToDataConvertDouble())
                           + (dt.Rows[6]["May"].ToDataConvertDouble())
                           + (dt.Rows[6]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[6]["Jul"].ToDataConvertDouble()) + (dt.Rows[6]["Aug"].ToDataConvertDouble()) + (dt.Rows[6]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[6]["Oct"].ToDataConvertDouble()) + (dt.Rows[6]["Nov"].ToDataConvertDouble()) + (dt.Rows[6]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[6]["Jan"].ToDataConvertDouble()) + (dt.Rows[6]["Feb"].ToDataConvertDouble()) + (dt.Rows[6]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }
                    if (i == 8)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[7]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[7]["May"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jul"].ToDataConvertDouble()) + (dt.Rows[7]["Aug"].ToDataConvertDouble()) + (dt.Rows[7]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[7]["Oct"].ToDataConvertDouble()) + (dt.Rows[7]["Nov"].ToDataConvertDouble()) + (dt.Rows[7]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[7]["Jan"].ToDataConvertDouble()) + (dt.Rows[7]["Feb"].ToDataConvertDouble()) + (dt.Rows[7]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 9)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[8]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[8]["May"].ToDataConvertDouble())
                            + (dt.Rows[8]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[8]["Jul"].ToDataConvertDouble()) + (dt.Rows[8]["Aug"].ToDataConvertDouble()) + (dt.Rows[8]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[8]["Oct"].ToDataConvertDouble()) + (dt.Rows[8]["Nov"].ToDataConvertDouble()) + (dt.Rows[8]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[8]["Jan"].ToDataConvertDouble()) + (dt.Rows[8]["Feb"].ToDataConvertDouble()) + (dt.Rows[8]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 10)//Total Indirect Cost- Common
                    {
                        gvtxtmptApr.Text = ((dt.Rows[7]["Apr"].ToDataConvertDouble()) + (dt.Rows[8]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[7]["May"].ToDataConvertDouble()) + (dt.Rows[8]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[7]["Jun"].ToDataConvertDouble()) + (dt.Rows[8]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[7]["Jul"].ToDataConvertDouble()) + (dt.Rows[8]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[7]["Aug"].ToDataConvertDouble()) + (dt.Rows[8]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[7]["Sep"].ToDataConvertDouble()) + (dt.Rows[8]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[7]["Oct"].ToDataConvertDouble()) + (dt.Rows[8]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[7]["Nov"].ToDataConvertDouble()) + (dt.Rows[8]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[7]["Dec"].ToDataConvertDouble()) + (dt.Rows[8]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[7]["Jan"].ToDataConvertDouble()) + (dt.Rows[8]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[7]["Feb"].ToDataConvertDouble()) + (dt.Rows[8]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[7]["Mar"].ToDataConvertDouble()) + (dt.Rows[8]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                           ((dt.Rows[9]["Apr"].ToDataConvertDouble())
                          + (dt.Rows[9]["May"].ToDataConvertDouble())
                          + (dt.Rows[9]["Jun"].ToDataConvertDouble())
                          + (dt.Rows[9]["Jul"].ToDataConvertDouble()) + (dt.Rows[9]["Aug"].ToDataConvertDouble()) + (dt.Rows[9]["Sep"].ToDataConvertDouble())
                          + (dt.Rows[9]["Oct"].ToDataConvertDouble()) + (dt.Rows[9]["Nov"].ToDataConvertDouble()) + (dt.Rows[9]["Dec"].ToDataConvertDouble())
                          + (dt.Rows[9]["Jan"].ToDataConvertDouble()) + (dt.Rows[9]["Feb"].ToDataConvertDouble()) + (dt.Rows[9]["Mar"].ToDataConvertDouble())
                         ).ToString();
                    }
                    if (i == 11)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[10]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[10]["May"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[10]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[10]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[10]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[10]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[10]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[10]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[10]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[10]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 12)//EBITDA after CC
                    {
                        gvtxtmptApr.Text = ((dt.Rows[6]["Apr"].ToDataConvertDouble()) - (dt.Rows[9]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[6]["May"].ToDataConvertDouble()) - (dt.Rows[9]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[6]["Jun"].ToDataConvertDouble()) - (dt.Rows[9]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[6]["Jul"].ToDataConvertDouble()) - (dt.Rows[9]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[6]["Aug"].ToDataConvertDouble()) - (dt.Rows[9]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[6]["Sep"].ToDataConvertDouble()) - (dt.Rows[9]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[6]["Oct"].ToDataConvertDouble()) - (dt.Rows[9]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[6]["Nov"].ToDataConvertDouble()) - (dt.Rows[9]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[6]["Dec"].ToDataConvertDouble()) - (dt.Rows[9]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[6]["Jan"].ToDataConvertDouble()) - (dt.Rows[9]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[6]["Feb"].ToDataConvertDouble()) - (dt.Rows[9]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[6]["Mar"].ToDataConvertDouble()) - (dt.Rows[9]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                           ((dt.Rows[11]["Apr"].ToDataConvertDouble())
                          + (dt.Rows[11]["May"].ToDataConvertDouble())
                          + (dt.Rows[11]["Jun"].ToDataConvertDouble())
                          + (dt.Rows[11]["Jul"].ToDataConvertDouble())
                          + (dt.Rows[11]["Aug"].ToDataConvertDouble())
                          + (dt.Rows[11]["Sep"].ToDataConvertDouble())
                          + (dt.Rows[11]["Oct"].ToDataConvertDouble())
                          + (dt.Rows[11]["Nov"].ToDataConvertDouble())
                          + (dt.Rows[11]["Dec"].ToDataConvertDouble())
                          + (dt.Rows[11]["Jan"].ToDataConvertDouble())
                          + (dt.Rows[11]["Feb"].ToDataConvertDouble())
                          + (dt.Rows[11]["Mar"].ToDataConvertDouble())
                         ).ToString();
                    }
                    if (i == 13)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[12]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[12]["May"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[12]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[12]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[12]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[12]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[12]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[12]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[12]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[12]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 14)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[13]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[13]["May"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[13]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[13]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[13]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[13]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[13]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[13]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[13]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[13]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 15)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[14]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[14]["May"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[14]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[14]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[14]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[14]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[14]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[14]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[14]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[14]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 16)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[15]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[15]["May"].ToDataConvertDouble())
                            + (dt.Rows[15]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[15]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[15]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[15]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[15]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[15]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[15]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[15]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[15]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[15]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 17)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[16]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[16]["May"].ToDataConvertDouble())
                            + (dt.Rows[16]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[16]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[16]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[16]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[16]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[16]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[16]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[16]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[16]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[16]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 18)
                    {
                        gvtxtmptTotal.Text =
                             ((dt.Rows[17]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[17]["May"].ToDataConvertDouble())
                            + (dt.Rows[17]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[17]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[17]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[17]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[17]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[17]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[17]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[17]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[17]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[17]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }

                    if (i == 19)//PAT
                    {
                        gvtxtmptApr.Text = ((dt.Rows[11]["Apr"].ToDataConvertDouble()) - (dt.Rows[12]["Apr"].ToDataConvertDouble()) - (dt.Rows[14]["Apr"].ToDataConvertDouble()) - (dt.Rows[15]["Apr"].ToDataConvertDouble()) - (dt.Rows[17]["Apr"].ToDataConvertDouble()) + (dt.Rows[13]["Apr"].ToDataConvertDouble() + dt.Rows[16]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[11]["May"].ToDataConvertDouble()) - (dt.Rows[12]["May"].ToDataConvertDouble()) - (dt.Rows[14]["May"].ToDataConvertDouble()) - (dt.Rows[15]["May"].ToDataConvertDouble()) - (dt.Rows[17]["May"].ToDataConvertDouble()) + (dt.Rows[13]["May"].ToDataConvertDouble() + dt.Rows[16]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[11]["Jun"].ToDataConvertDouble()) - (dt.Rows[12]["Jun"].ToDataConvertDouble()) - (dt.Rows[14]["Jun"].ToDataConvertDouble()) - (dt.Rows[15]["Jun"].ToDataConvertDouble()) - (dt.Rows[17]["Jun"].ToDataConvertDouble()) + (dt.Rows[13]["Jun"].ToDataConvertDouble() + dt.Rows[16]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[11]["Jul"].ToDataConvertDouble()) - (dt.Rows[12]["Jul"].ToDataConvertDouble()) - (dt.Rows[14]["Jul"].ToDataConvertDouble()) - (dt.Rows[15]["Jul"].ToDataConvertDouble()) - (dt.Rows[17]["Jul"].ToDataConvertDouble()) + (dt.Rows[13]["Jul"].ToDataConvertDouble() + dt.Rows[16]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[11]["Aug"].ToDataConvertDouble()) - (dt.Rows[12]["Aug"].ToDataConvertDouble()) - (dt.Rows[14]["Aug"].ToDataConvertDouble()) - (dt.Rows[15]["Aug"].ToDataConvertDouble()) - (dt.Rows[17]["Aug"].ToDataConvertDouble()) + (dt.Rows[13]["Aug"].ToDataConvertDouble() + dt.Rows[16]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[11]["Sep"].ToDataConvertDouble()) - (dt.Rows[12]["Sep"].ToDataConvertDouble()) - (dt.Rows[14]["Sep"].ToDataConvertDouble()) - (dt.Rows[15]["Sep"].ToDataConvertDouble()) - (dt.Rows[17]["Sep"].ToDataConvertDouble()) + (dt.Rows[13]["Sep"].ToDataConvertDouble() + dt.Rows[16]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[11]["Oct"].ToDataConvertDouble()) - (dt.Rows[12]["Oct"].ToDataConvertDouble()) - (dt.Rows[14]["Oct"].ToDataConvertDouble()) - (dt.Rows[15]["Oct"].ToDataConvertDouble()) - (dt.Rows[17]["Oct"].ToDataConvertDouble()) + (dt.Rows[13]["Oct"].ToDataConvertDouble() + dt.Rows[16]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[11]["Nov"].ToDataConvertDouble()) - (dt.Rows[12]["Nov"].ToDataConvertDouble()) - (dt.Rows[14]["Nov"].ToDataConvertDouble()) - (dt.Rows[15]["Nov"].ToDataConvertDouble()) - (dt.Rows[17]["Nov"].ToDataConvertDouble()) + (dt.Rows[13]["Nov"].ToDataConvertDouble() + dt.Rows[16]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[11]["Dec"].ToDataConvertDouble()) - (dt.Rows[12]["Dec"].ToDataConvertDouble()) - (dt.Rows[14]["Dec"].ToDataConvertDouble()) - (dt.Rows[15]["Dec"].ToDataConvertDouble()) - (dt.Rows[17]["Dec"].ToDataConvertDouble()) + (dt.Rows[13]["Dec"].ToDataConvertDouble() + dt.Rows[16]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[11]["Jan"].ToDataConvertDouble()) - (dt.Rows[12]["Jan"].ToDataConvertDouble()) - (dt.Rows[14]["Jan"].ToDataConvertDouble()) - (dt.Rows[15]["Jan"].ToDataConvertDouble()) - (dt.Rows[17]["Jan"].ToDataConvertDouble()) + (dt.Rows[13]["Jan"].ToDataConvertDouble() + dt.Rows[16]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[11]["Feb"].ToDataConvertDouble()) - (dt.Rows[12]["Feb"].ToDataConvertDouble()) - (dt.Rows[14]["Feb"].ToDataConvertDouble()) - (dt.Rows[15]["Feb"].ToDataConvertDouble()) - (dt.Rows[17]["Feb"].ToDataConvertDouble()) + (dt.Rows[13]["Feb"].ToDataConvertDouble() + dt.Rows[16]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[11]["Mar"].ToDataConvertDouble()) - (dt.Rows[12]["Mar"].ToDataConvertDouble()) - (dt.Rows[14]["Mar"].ToDataConvertDouble()) - (dt.Rows[15]["Mar"].ToDataConvertDouble()) - (dt.Rows[17]["Mar"].ToDataConvertDouble()) + (dt.Rows[13]["Mar"].ToDataConvertDouble() + dt.Rows[16]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                             ((dt.Rows[18]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[18]["May"].ToDataConvertDouble())
                            + (dt.Rows[18]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[18]["Jul"].ToDataConvertDouble())
                            + (dt.Rows[18]["Aug"].ToDataConvertDouble())
                            + (dt.Rows[18]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[18]["Oct"].ToDataConvertDouble())
                            + (dt.Rows[18]["Nov"].ToDataConvertDouble())
                            + (dt.Rows[18]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[18]["Jan"].ToDataConvertDouble())
                            + (dt.Rows[18]["Feb"].ToDataConvertDouble())
                            + (dt.Rows[18]["Mar"].ToDataConvertDouble())
                           ).ToString();

                    }

                    if (i == 20)//Cash Profit
                    {
                        gvtxtmptApr.Text = ((dt.Rows[18]["Apr"].ToDataConvertDouble()) + (dt.Rows[14]["Apr"].ToDataConvertDouble())).ToString();
                        gvtxtmptMay.Text = ((dt.Rows[18]["May"].ToDataConvertDouble()) + (dt.Rows[14]["May"].ToDataConvertDouble())).ToString();
                        gvtxtmptJun.Text = ((dt.Rows[18]["Jun"].ToDataConvertDouble()) + (dt.Rows[14]["Jun"].ToDataConvertDouble())).ToString();
                        gvtxtJul.Text = ((dt.Rows[18]["Jul"].ToDataConvertDouble()) + (dt.Rows[14]["Jul"].ToDataConvertDouble())).ToString();
                        gvtxtAug.Text = ((dt.Rows[18]["Aug"].ToDataConvertDouble()) + (dt.Rows[14]["Aug"].ToDataConvertDouble())).ToString();
                        gvtxtmptSep.Text = ((dt.Rows[18]["Sep"].ToDataConvertDouble()) + (dt.Rows[14]["Sep"].ToDataConvertDouble())).ToString();
                        gvtxtmptOct.Text = ((dt.Rows[18]["Oct"].ToDataConvertDouble()) + (dt.Rows[14]["Oct"].ToDataConvertDouble())).ToString();
                        gvtxtNov.Text = ((dt.Rows[18]["Nov"].ToDataConvertDouble()) + (dt.Rows[14]["Nov"].ToDataConvertDouble())).ToString();
                        gvtxtmptDec.Text = ((dt.Rows[18]["Dec"].ToDataConvertDouble()) + (dt.Rows[14]["Dec"].ToDataConvertDouble())).ToString();
                        gvtxtmptJan.Text = ((dt.Rows[18]["Jan"].ToDataConvertDouble()) + (dt.Rows[14]["Jan"].ToDataConvertDouble())).ToString();
                        gvtxtFeb.Text = ((dt.Rows[18]["Feb"].ToDataConvertDouble()) + (dt.Rows[14]["Feb"].ToDataConvertDouble())).ToString();
                        gvtxtMar.Text = ((dt.Rows[18]["Mar"].ToDataConvertDouble()) + (dt.Rows[14]["Mar"].ToDataConvertDouble())).ToString();

                        gvtxtmptTotal.Text =
                            ((dt.Rows[19]["Apr"].ToDataConvertDouble())
                           + (dt.Rows[19]["May"].ToDataConvertDouble())
                           + (dt.Rows[19]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[19]["Jul"].ToDataConvertDouble())
                           + (dt.Rows[19]["Aug"].ToDataConvertDouble())
                           + (dt.Rows[19]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[19]["Oct"].ToDataConvertDouble())
                           + (dt.Rows[19]["Nov"].ToDataConvertDouble())
                           + (dt.Rows[19]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[19]["Jan"].ToDataConvertDouble())
                           + (dt.Rows[19]["Feb"].ToDataConvertDouble())
                           + (dt.Rows[19]["Mar"].ToDataConvertDouble())
                          ).ToString();
                    }

                    if (i == 21)//Gross Profit%
                    {
                        gvtxtmptApr.Text = (((dt.Rows[2]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[2]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[2]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[2]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[2]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[2]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[2]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[2]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[2]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[2]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[2]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[2]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[2]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();
                    }
                    if (i == 22)//EBITDA Before CC%
                    {
                        gvtxtmptApr.Text = (((dt.Rows[6]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[6]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[6]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[6]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[6]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[6]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[6]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[6]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[6]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[6]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[6]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[6]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[6]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();
                    }
                    if (i == 23)//EBITDA After CC%
                    {
                        gvtxtmptApr.Text = (((dt.Rows[11]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = (((dt.Rows[11]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = (((dt.Rows[11]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = (((dt.Rows[11]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = (((dt.Rows[11]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = (((dt.Rows[11]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = (((dt.Rows[11]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = (((dt.Rows[11]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = (((dt.Rows[11]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = (((dt.Rows[11]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = (((dt.Rows[11]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = (((dt.Rows[11]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = (((dt.Rows[11]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();
                    }
                    if (i == 24)//% of Salaries to Revenue
                    {
                        gvtxtmptApr.Text = ((((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[7]["Apr"].ToDataConvertDouble())) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = ((((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[7]["May"].ToDataConvertDouble())) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = ((((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[7]["Jun"].ToDataConvertDouble())) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = ((((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[7]["Jul"].ToDataConvertDouble())) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = ((((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[7]["Aug"].ToDataConvertDouble())) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = ((((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[7]["Sep"].ToDataConvertDouble())) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = ((((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[7]["Oct"].ToDataConvertDouble())) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = ((((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[7]["Nov"].ToDataConvertDouble())) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = ((((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[7]["Dec"].ToDataConvertDouble())) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = ((((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[7]["Jan"].ToDataConvertDouble())) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = ((((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[7]["Feb"].ToDataConvertDouble())) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = ((((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[7]["Mar"].ToDataConvertDouble())) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = ((((dt.Rows[3]["Total"].ToDataConvertDouble()) + (dt.Rows[7]["Total"].ToDataConvertDouble())) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();
                    }
                    if (i == 25)//% of Indirect Cost to Revenue
                    {
                        gvtxtmptApr.Text = ((((dt.Rows[4]["Apr"].ToDataConvertDouble()) + (dt.Rows[8]["Apr"].ToDataConvertDouble())) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptMay.Text = ((((dt.Rows[4]["May"].ToDataConvertDouble()) + (dt.Rows[8]["May"].ToDataConvertDouble())) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJun.Text = ((((dt.Rows[4]["Jun"].ToDataConvertDouble()) + (dt.Rows[8]["Jun"].ToDataConvertDouble())) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtJul.Text = ((((dt.Rows[4]["Jul"].ToDataConvertDouble()) + (dt.Rows[8]["Jul"].ToDataConvertDouble())) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtAug.Text = ((((dt.Rows[4]["Aug"].ToDataConvertDouble()) + (dt.Rows[8]["Aug"].ToDataConvertDouble())) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptSep.Text = ((((dt.Rows[4]["Sep"].ToDataConvertDouble()) + (dt.Rows[8]["Sep"].ToDataConvertDouble())) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptOct.Text = ((((dt.Rows[4]["Oct"].ToDataConvertDouble()) + (dt.Rows[8]["Oct"].ToDataConvertDouble())) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtNov.Text = ((((dt.Rows[4]["Nov"].ToDataConvertDouble()) + (dt.Rows[8]["Nov"].ToDataConvertDouble())) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptDec.Text = ((((dt.Rows[4]["Dec"].ToDataConvertDouble()) + (dt.Rows[8]["Dec"].ToDataConvertDouble())) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtmptJan.Text = ((((dt.Rows[4]["Jan"].ToDataConvertDouble()) + (dt.Rows[8]["Jan"].ToDataConvertDouble())) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtFeb.Text = ((((dt.Rows[4]["Feb"].ToDataConvertDouble()) + (dt.Rows[8]["Feb"].ToDataConvertDouble())) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        gvtxtMar.Text = ((((dt.Rows[4]["Mar"].ToDataConvertDouble()) + (dt.Rows[8]["Mar"].ToDataConvertDouble())) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        gvtxtmptTotal.Text = ((((dt.Rows[4]["Total"].ToDataConvertDouble()) + (dt.Rows[8]["Total"].ToDataConvertDouble())) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();
                    }

                    if (gvtxtmptApr.Text == "NaN" || gvtxtmptApr.Text == "-Infinity" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptApr.Text = "0";

                    if (gvtxtmptMay.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptMay.Text = "0";

                    if (gvtxtmptJun.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptJun.Text = "0";

                    if (gvtxtJul.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtJul.Text = "0";
                    if (gvtxtAug.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtAug.Text = "0";
                    if (gvtxtmptSep.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptSep.Text = "0";
                    if (gvtxtmptOct.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptOct.Text = "0";


                    if (gvtxtNov.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtNov.Text = "0";

                    if (gvtxtmptDec.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptDec.Text = "0";

                    if (gvtxtmptJan.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtmptJan.Text = "0";

                    if (gvtxtFeb.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtFeb.Text = "0";

                    if (gvtxtMar.Text == "NaN" || gvtxtmptApr.Text.ToLower() == "infinity")
                        gvtxtMar.Text = "0";
                    if (gvtxtmptTotal.Text == "NaN" || gvtxtmptTotal.Text.ToLower() == "infinity")
                        gvtxtmptTotal.Text = "0";



                    i++;
                }
            }
            catch (Exception ex)
            {
            }
        }


        #endregion

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            if (!Searchvalidation())
            {
                return;
            }
            bindGrd();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            bindGrd();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (drpDivision.SelectedValue == "1")
            {
                ConvertGridViewToDataTableForAFIL();
            }
            else
            {
                ConvertGridViewToDataTableForDefault();
            }

            if (!submitValidation())
            {
                return;
            }
            if (drpType.SelectedValue == "1")
            {
                saveBudgetData();
                bindGrd();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            gvList.DataBind();
        }

        protected void drpDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBind();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label gvlblDesc = (Label)e.Row.FindControl("gvlblmpmparticularDesc");
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
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (gvlblDesc.Text == "EBITDA Before CC%")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "EBITDA After CC%")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "% of Salaries to Revenue")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (gvlblDesc.Text == "% of Indirect Cost to Revenue")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {
                    if (gvlblDesc.Text == "Corporate Cost")
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                    }
                    if (gvlblDesc.Text == "Total Indirect Cost- Common")
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = ColorTranslator.FromHtml("#FDE9D9");
                    }
                }
            }
        }

        protected void BtnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/CargowiseDashboard/MISPanvelList.aspx");
        }
    }
}