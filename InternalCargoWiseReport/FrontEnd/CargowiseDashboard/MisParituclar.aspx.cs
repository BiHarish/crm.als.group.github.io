using ClosedXML.Excel;
using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class MisParituclar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();
                // drpFinancialYear.SelectedValue = "2019-2020";
                // bindGrd();
            }
        }

        #region Method

        void bindDrp()
        {
            MISMasterData _data = new MISMasterData();

            DataSet result = _data.getFinYearForRunRate();

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
                drpDivision.Items.Insert(1, new ListItem("ALS Consol", "ALS Consol"));
                drpDivision.Items.Insert(2, new ListItem("AFIL Consol", "AFIL Consol"));

            }
        }
        void bindGrd()
        {
            DataSet ds = null;

            DataTable dt = null;
            MisParticularsTransactionData _data = new MisParticularsTransactionData();
            MisParticularsTransactionDto request = new MisParticularsTransactionDto();
            if (drpDivision.SelectedValue == "ALS Consol" || drpDivision.SelectedValue == "AFIL Consol")
            {
                bindGrdWithConsole();
                //  lnkActualVsBudget.Visible = false;
                return;
            }
            request.mptfinancialyear = drpFinancialYear.SelectedValue;
            if (drpSubdivision.Items.Count > 0)
            {
                request.mptdivisionid = drpSubdivision.SelectedValue.ToNullLong();
            }
            else
            {
                request.mptdivisionid = drpDivision.SelectedValue.ToNullLong();
            }
            request.MonthNo = Drpmonth.SelectedValue.ToConvertInt();

            ds = _data.getData(request);


            if (ds != null)
            {
                lnkActualVsBudget.Visible = true;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBudget.Text = ds.Tables[0].Rows[0]["FinYear"].ToString() + "-(Rs. Cr)";
                    lblDivisionName1.Text = ds.Tables[0].Rows[0]["Division"].ToString();
                    lblBudgetHeading.Visible = true;
                    dt = new DataTable();
                    dt = ds.Tables[0];
                    dt.Columns.Remove("Division");
                    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    dt.AcceptChanges();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvBudgetList.DataSource = dt;
                        gvBudgetList.DataBind();

                    }
                    else
                    {
                        gvBudgetList.DataBind();
                        lblBudget.Text = string.Empty;
                        lblDivisionName1.Text = string.Empty;
                        lblBudgetHeading.Visible = false;

                    }
                }
                else
                {
                    gvBudgetList.DataBind();
                    lblBudget.Text = string.Empty;
                    lblDivisionName1.Text = string.Empty;
                    lblBudgetHeading.Visible = false;

                }


                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblActual1.Text = ds.Tables[1].Rows[0]["FinYear"].ToString();
                    lblDivisionName2.Text = ds.Tables[1].Rows[0]["Division"].ToString();
                    lblCurrentActualHeading.Visible = true;
                    dt = new DataTable();
                    dt = ds.Tables[1];
                    dt.Columns.Remove("Division");
                    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    dt.AcceptChanges();
                    gvActual1.DataSource = dt;
                    gvActual1.DataBind();
                }
                else
                {
                    gvActual1.DataBind();
                    lblActual1.Text = string.Empty;
                    lblDivisionName2.Text = string.Empty;
                    lblCurrentActualHeading.Visible = false;
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (drpDivision.SelectedValue == "7" || drpDivision.SelectedValue == "3" || drpDivision.SelectedValue == "5"
                         || drpDivision.SelectedValue == "8")
                    {
                        lblActual2.Text = ds.Tables[2].Rows[0]["FinYear"].ToString() + " With SEIS";
                    }
                    else
                    {
                        lblActual2.Text = ds.Tables[2].Rows[0]["FinYear"].ToString();
                    }

                    lblDivisionName3.Text = ds.Tables[2].Rows[0]["Division"].ToString();
                    lblLastYearActualHeading.Visible = true;
                    dt = new DataTable();
                    dt = ds.Tables[2];
                    dt.Columns.Remove("Division");
                    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    dt.AcceptChanges();
                    gvActual2.DataSource = dt;
                    gvActual2.DataBind();
                }
                else
                {
                    lblActual2.Text = string.Empty;
                    gvActual2.DataBind();
                    lblDivisionName3.Text = string.Empty;
                    lblLastYearActualHeading.Visible = false;
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    if (drpDivision.SelectedValue == "7" || drpDivision.SelectedValue == "3" || drpDivision.SelectedValue == "5"
                        || drpDivision.SelectedValue == "8")
                    {
                        lblActual3.Text = ds.Tables[3].Rows[0]["FinYear"].ToString() + " With SEIS";
                    }
                    else
                    {
                        lblActual3.Text = ds.Tables[3].Rows[0]["FinYear"].ToString();
                    }
                    lblSLastActualHeading.Visible = true;
                    lblDivisionName4.Text = ds.Tables[3].Rows[0]["Division"].ToString();
                    dt = new DataTable();
                    dt = ds.Tables[3];
                    dt.Columns.Remove("Division");
                    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    dt.AcceptChanges();
                    gvActual3.DataSource = dt;
                    gvActual3.DataBind();
                }
                else
                {
                    gvActual3.DataBind();
                    lblActual3.Text = string.Empty;
                    lblDivisionName4.Text = string.Empty;
                    lblSLastActualHeading.Visible = false;
                }
            }
            else
            {
                gvBudgetList.DataBind();
                gvActual1.DataBind();
                gvActual2.DataBind();
                gvActual3.DataBind();
                lblBudget.Text = string.Empty;
                lblActual1.Text = string.Empty;
                lblActual2.Text = string.Empty;
                lblActual3.Text = string.Empty;
                lblDivisionName1.Text = string.Empty;
                lblDivisionName2.Text = string.Empty;
                lblDivisionName3.Text = string.Empty;
                lblDivisionName4.Text = string.Empty;
                lnkActualVsBudget.Visible = false;
                lblBudgetHeading.Visible = false;
                lblCurrentActualHeading.Visible = false;
                lblLastYearActualHeading.Visible = false;
                lblSLastActualHeading.Visible = false;
                // txtRecordFound.Text = "0";
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
            }
            if (drpDivision.SelectedValue == "7" || drpDivision.SelectedValue == "3" || drpDivision.SelectedValue == "5" ||
                drpDivision.SelectedValue == "8")
            {
                bindWithoutSEISGrd();
            }
            else
            {
                gvActualSLastYearWithoutSEIS.DataBind();
                gvActualWithoutSEISCurrent.DataBind();
                lblDivisionNameCurrentSEIS.Text = string.Empty;
                lblDivisionNameSLastSEIS.Text = string.Empty;
                lblFinYear.Text = string.Empty;
                lblFinYearSLastSEIS.Text = string.Empty;
            }

        }
        void bindWithoutSEISGrd()
        {
            DataSet ds = null;

            DataTable dt = null;
            MisParticularsTransactionData _data = new MisParticularsTransactionData();
            MisParticularsTransactionDto request = new MisParticularsTransactionDto();
            request.mptfinancialyear = drpFinancialYear.SelectedValue;
            if (drpSubdivision.Items.Count > 0)
            {
                request.mptdivisionid = drpSubdivision.SelectedValue.ToNullLong();
            }
            else
            {
                request.mptdivisionid = drpDivision.SelectedValue.ToNullLong();
            }
            request.MonthNo = Drpmonth.SelectedValue.ToConvertInt();

            ds = _data.getDataWithoutSeis(request);

            if (ds != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (drpDivision.SelectedValue == "7" || drpDivision.SelectedValue == "3" || drpDivision.SelectedValue == "5"
                        || drpDivision.SelectedValue == "8")
                    {
                        lblFinYear.Text = ds.Tables[2].Rows[0]["FinYear"].ToString() + " Without SEIS";
                    }
                    else
                    {
                        lblFinYear.Text = ds.Tables[2].Rows[0]["FinYear"].ToString();
                    }
                    lblDivisionNameCurrentSEIS.Text = ds.Tables[2].Rows[0]["Division"].ToString();
                    lblCurrentSEISActualHeading.Visible = true;
                    dt = new DataTable();
                    dt = ds.Tables[2];
                    dt.Columns.Remove("Division");
                    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    dt.AcceptChanges();
                    gvActualWithoutSEISCurrent.DataSource = dt;
                    gvActualWithoutSEISCurrent.DataBind();
                }
                else
                {
                    gvActualWithoutSEISCurrent.DataBind();
                    lblDivisionNameCurrentSEIS.Text = string.Empty;
                    lblFinYear.Text = string.Empty;
                    lblCurrentSEISActualHeading.Visible = false;
                }
                if (drpDivision.SelectedValue == "7")
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        if (drpDivision.SelectedValue == "7" || drpDivision.SelectedValue == "3" || drpDivision.SelectedValue == "5"
                        || drpDivision.SelectedValue == "8")
                        {
                            lblFinYearSLastSEIS.Text = ds.Tables[3].Rows[0]["FinYear"].ToString() + " Without SEIS";
                        }
                        else
                        {
                            lblFinYearSLastSEIS.Text = ds.Tables[3].Rows[0]["FinYear"].ToString();
                        }
                        lblDivisionNameSLastSEIS.Text = ds.Tables[3].Rows[0]["Division"].ToString();
                        lblSLastSEISActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[3];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActualSLastYearWithoutSEIS.DataSource = dt;
                        gvActualSLastYearWithoutSEIS.DataBind();
                    }
                    else
                    {
                        lblFinYearSLastSEIS.Text = string.Empty;
                        gvActualSLastYearWithoutSEIS.DataBind();
                        lblDivisionNameSLastSEIS.Text = string.Empty;
                        lblSLastSEISActualHeading.Visible = false;
                    }
                }
                else
                {
                    lblFinYearSLastSEIS.Text = string.Empty;
                    gvActualSLastYearWithoutSEIS.DataBind();
                    lblDivisionNameSLastSEIS.Text = string.Empty;
                    lblSLastSEISActualHeading.Visible = false;
                }
            }
            else
            {
                gvActualSLastYearWithoutSEIS.DataBind();
                gvActualWithoutSEISCurrent.DataBind();
                lblDivisionNameCurrentSEIS.Text = string.Empty;
                lblDivisionNameSLastSEIS.Text = string.Empty;
                lblFinYear.Text = string.Empty;
                lblFinYearSLastSEIS.Text = string.Empty;
                lblSLastSEISActualHeading.Visible = false;
                lblCurrentSEISActualHeading.Visible = false;
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found!!", "Error!", Toastr.ToastPosition.TopCenter, true);

            }
        }
        void bindGrdWithConsole()
        {
            DataSet ds = null;

            DataTable dt = null;
            MisParticularsTransactionData _data = new MisParticularsTransactionData();
            ConsoleDto request = new ConsoleDto();
            request.CYfinancialYear = drpFinancialYear.SelectedValue;
            request.month = Drpmonth.SelectedValue.ToConvertInt();

            ds = _data.getDataConsol(request);

            if (drpDivision.SelectedValue == "ALS Consol")
            {
                if (ds != null)
                {
                    lnkActualVsBudget.Visible = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblBudget.Text = ds.Tables[0].Rows[0]["FinYear"].ToString() + "-(Rs. Cr)";
                        lblDivisionName1.Text = drpDivision.SelectedValue;
                        lblBudgetHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[0];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvBudgetList.DataSource = dt;
                            gvBudgetList.DataBind();

                        }
                        else
                        {
                            gvBudgetList.DataBind();
                            lblBudget.Text = string.Empty;
                            lblDivisionName1.Text = string.Empty;
                            lblBudgetHeading.Visible = false;
                        }
                    }
                    else
                    {
                        gvBudgetList.DataBind();
                        lblBudget.Text = string.Empty;
                        lblDivisionName1.Text = string.Empty;
                        lblBudgetHeading.Visible = false;
                    }


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblActual1.Text = ds.Tables[1].Rows[0]["FinYear"].ToString();
                        lblDivisionName2.Text = drpDivision.SelectedValue;
                        lblCurrentActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[1];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual1.DataSource = dt;
                        gvActual1.DataBind();
                    }
                    else
                    {
                        gvActual1.DataBind();
                        lblActual1.Text = string.Empty;
                        lblDivisionName2.Text = string.Empty;
                        lblBudgetHeading.Visible = false;
                    }

                    if (ds.Tables[2].Rows.Count > 0)///Without SEIS
                    {
                        lblFinYear.Text = ds.Tables[2].Rows[0]["FinYear"].ToString() + " Without SEIS";
                        lblDivisionNameCurrentSEIS.Text = drpDivision.SelectedValue;
                        lblCurrentSEISActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[2];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActualWithoutSEISCurrent.DataSource = dt;
                        gvActualWithoutSEISCurrent.DataBind();
                    }
                    else
                    {
                        gvActualWithoutSEISCurrent.DataBind();
                        lblDivisionNameCurrentSEIS.Text = string.Empty;
                        lblFinYear.Text = string.Empty;
                        lblCurrentSEISActualHeading.Visible = false;
                    }

                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lblActual2.Text = ds.Tables[3].Rows[0]["FinYear"].ToString() + " With SEIS";
                        lblDivisionName3.Text = drpDivision.SelectedValue;
                        lblSLastActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[3];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual2.DataSource = dt;
                        gvActual2.DataBind();
                    }
                    else
                    {
                        lblActual2.Text = string.Empty;
                        gvActual2.DataBind();
                        lblDivisionName3.Text = string.Empty;
                        lblSLastActualHeading.Visible = false;
                    }


                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        lblFinYearSLastSEIS.Text = ds.Tables[4].Rows[0]["FinYear"].ToString() + " Without SEIS";
                        lblDivisionNameSLastSEIS.Text = drpDivision.SelectedValue;
                        lblSLastSEISActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[4];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActualSLastYearWithoutSEIS.DataSource = dt;
                        gvActualSLastYearWithoutSEIS.DataBind();
                    }
                    else
                    {
                        lblFinYearSLastSEIS.Text = string.Empty;
                        gvActualSLastYearWithoutSEIS.DataBind();
                        lblDivisionNameSLastSEIS.Text = string.Empty;
                        lblSLastSEISActualHeading.Visible = false;
                    }

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        lblActual3.Text = ds.Tables[5].Rows[0]["FinYear"].ToString() + " With SEIS";
                        lblDivisionName4.Text = drpDivision.SelectedValue;
                        lblSLastActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[5];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual3.DataSource = dt;
                        gvActual3.DataBind();
                    }
                    else
                    {
                        gvActual3.DataBind();
                        lblActual3.Text = string.Empty;
                        lblDivisionName4.Text = string.Empty;
                        lblSLastActualHeading.Visible = false;
                    }
                }
                else
                {
                    gvBudgetList.DataBind();
                    gvActual1.DataBind();
                    gvActual2.DataBind();
                    gvActual3.DataBind();
                    lblBudget.Text = string.Empty;
                    lblActual1.Text = string.Empty;
                    lblActual2.Text = string.Empty;
                    lblActual3.Text = string.Empty;
                    lblDivisionName1.Text = string.Empty;
                    lblDivisionName2.Text = string.Empty;
                    lblDivisionName3.Text = string.Empty;
                    lblDivisionName4.Text = string.Empty;
                    lnkActualVsBudget.Visible = false;
                    // txtRecordFound.Text = "0";
                    gvActualSLastYearWithoutSEIS.DataBind();
                    gvActualWithoutSEISCurrent.DataBind();
                    lblDivisionNameCurrentSEIS.Text = string.Empty;
                    lblDivisionNameSLastSEIS.Text = string.Empty;
                    lblFinYear.Text = string.Empty;
                    lblFinYearSLastSEIS.Text = string.Empty;

                    lblBudgetHeading.Visible = false;
                    lblCurrentActualHeading.Visible = false;
                    lblLastYearActualHeading.Visible = false;
                    lblSLastActualHeading.Visible = false;
                    lblCurrentSEISActualHeading.Visible = false;
                    lblSLastSEISActualHeading.Visible = false;

                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }

            if (drpDivision.SelectedValue == "AFIL Consol")
            {
                if (ds != null)
                {
                    lnkActualVsBudget.Visible = true;
                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        lblBudget.Text = ds.Tables[6].Rows[0]["FinYear"].ToString() + "-(Rs. Cr)";
                        lblDivisionName1.Text = drpDivision.SelectedValue;
                        lblBudgetHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[6];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            gvBudgetList.DataSource = dt;
                            gvBudgetList.DataBind();

                        }
                        else
                        {
                            gvBudgetList.DataBind();
                            lblBudget.Text = string.Empty;
                            lblDivisionName1.Text = string.Empty;
                            lblBudgetHeading.Visible = true;
                        }
                    }
                    else
                    {
                        gvBudgetList.DataBind();
                        lblBudget.Text = string.Empty;
                        lblDivisionName1.Text = string.Empty;
                        lblBudgetHeading.Visible = false;
                    }


                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        lblActual1.Text = ds.Tables[7].Rows[0]["FinYear"].ToString();
                        lblDivisionName2.Text = drpDivision.SelectedValue;
                        lblCurrentActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[7];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual1.DataSource = dt;
                        gvActual1.DataBind();
                    }
                    else
                    {
                        gvActual1.DataBind();
                        lblActual1.Text = string.Empty;
                        lblDivisionName2.Text = string.Empty;
                        lblCurrentActualHeading.Visible = false;
                    }

                    //if (ds.Tables[8].Rows.Count > 0)///Without SEIS
                    //{
                    //    lblFinYear.Text = ds.Tables[8].Rows[0]["FinYear"].ToString() + " Without SEIS";
                    //    lblDivisionNameCurrentSEIS.Text = drpDivision.SelectedValue;
                    //    dt = new DataTable();
                    //    dt = ds.Tables[8];
                    //    dt.Columns.Remove("Division");
                    //    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    //    dt.AcceptChanges();
                    //    gvActualWithoutSEISCurrent.DataSource = dt;
                    //    gvActualWithoutSEISCurrent.DataBind();
                    //}
                    //else
                    //{
                    //    gvActualWithoutSEISCurrent.DataBind();
                    //    lblDivisionNameCurrentSEIS.Text = string.Empty;
                    //    lblFinYear.Text = string.Empty;
                    //}

                    if (ds.Tables[9].Rows.Count > 0)
                    {
                        lblActual2.Text = ds.Tables[9].Rows[0]["FinYear"].ToString();
                        lblDivisionName3.Text = drpDivision.SelectedValue;
                        lblLastYearActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[9];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual2.DataSource = dt;
                        gvActual2.DataBind();
                    }
                    else
                    {
                        lblActual2.Text = string.Empty;
                        gvActual2.DataBind();
                        lblDivisionName3.Text = string.Empty;
                        lblLastYearActualHeading.Visible = false;
                    }


                    //if (ds.Tables[10].Rows.Count > 0)
                    //{
                    //    lblFinYearSLastSEIS.Text = ds.Tables[10].Rows[0]["FinYear"].ToString() + " Without SEIS";
                    //    lblDivisionNameSLastSEIS.Text = drpDivision.SelectedValue;
                    //    dt = new DataTable();
                    //    dt = ds.Tables[10];
                    //    dt.Columns.Remove("Division");
                    //    dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                    //    dt.AcceptChanges();
                    //    gvActualSLastYearWithoutSEIS.DataSource = dt;
                    //    gvActualSLastYearWithoutSEIS.DataBind();
                    //}
                    //else
                    //{
                    //    lblFinYearSLastSEIS.Text = string.Empty;
                    //    gvActualSLastYearWithoutSEIS.DataBind();
                    //    lblDivisionNameSLastSEIS.Text = string.Empty;
                    //}

                    if (ds.Tables[11].Rows.Count > 0)
                    {
                        lblActual3.Text = ds.Tables[11].Rows[0]["FinYear"].ToString();
                        lblDivisionName4.Text = drpDivision.SelectedValue;
                        lblSLastActualHeading.Visible = true;
                        dt = new DataTable();
                        dt = ds.Tables[11];
                        dt.Columns.Remove("Division");
                        dt.Columns["YTD"].ColumnName = "YTD" + "-" + Drpmonth.SelectedItem.Text;
                        dt.AcceptChanges();
                        gvActual3.DataSource = dt;
                        gvActual3.DataBind();
                    }
                    else
                    {
                        gvActual3.DataBind();
                        lblActual3.Text = string.Empty;
                        lblDivisionName4.Text = string.Empty;
                        lblSLastActualHeading.Visible = false;
                    }
                }
                else
                {
                    gvBudgetList.DataBind();
                    gvActual1.DataBind();
                    gvActual2.DataBind();
                    gvActual3.DataBind();
                    lblBudget.Text = string.Empty;
                    lblActual1.Text = string.Empty;
                    lblActual2.Text = string.Empty;
                    lblActual3.Text = string.Empty;
                    lblDivisionName1.Text = string.Empty;
                    lblDivisionName2.Text = string.Empty;
                    lblDivisionName3.Text = string.Empty;
                    lblDivisionName4.Text = string.Empty;
                    lnkActualVsBudget.Visible = false;
                    // txtRecordFound.Text = "0";
                    gvActualSLastYearWithoutSEIS.DataBind();
                    gvActualWithoutSEISCurrent.DataBind();
                    lblDivisionNameCurrentSEIS.Text = string.Empty;
                    lblDivisionNameSLastSEIS.Text = string.Empty;
                    lblFinYear.Text = string.Empty;
                    lblFinYearSLastSEIS.Text = string.Empty;

                    lblBudgetHeading.Visible = false;
                    lblCurrentActualHeading.Visible = false;
                    lblLastYearActualHeading.Visible = false;
                    lblSLastActualHeading.Visible = false;
                    lblCurrentSEISActualHeading.Visible = false;
                    lblSLastSEISActualHeading.Visible = false;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not found!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }




        }
        protected void ExcelDownloadFile(object sender, EventArgs e)
        {
            if(gvBudgetList.Rows.Count==0&&gvActual1.Rows.Count==0&&gvActual2.Rows.Count==0 && gvActual3.Rows.Count==0&&gvActualWithoutSEISCurrent.Rows.Count==0&&
                gvActualWithoutSEISCurrent.Rows.Count==0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please submit first!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }
            ExportToExcel();

            //MisParticularsTransactionData _data = new MisParticularsTransactionData();
            //MisParticularsTransactionDto request = new MisParticularsTransactionDto();
            //request.mptfinancialyear = drpFinancialYear.SelectedValue;
            //if (drpSubdivision.Items.Count > 0)
            //{
            //    request.mptdivisionid = drpSubdivision.SelectedValue.ToNullLong();
            //}
            //else
            //{
            //    request.mptdivisionid = drpDivision.SelectedValue.ToNullLong();
            //}
            //DataSet result = _data.getData(request);
            //
            //if (result != null)
            //{
            //    string filePath = "MisData.xlsx";
            //    var appDataPath = Server.MapPath("~" + "\\SCSExcelFiles\\MisData.xlsx");
            //    try
            //    {
            //        ICWR.Data.Utility.ExtensionMethods.ExportDataSetToExcelWithMultipleSheet(result, appDataPath);
            //
            //
            //
            //        HttpContext.Current.Response.ContentType = ContentType;//"APPLICATION/OCTET-STREAM";
            //        String Header = "Attachment; Filename=" + filePath;
            //        HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            //        System.IO.FileInfo Dfile = new System.IO.FileInfo(appDataPath);
            //        try
            //        {
            //            HttpContext.Current.Response.WriteFile(appDataPath);
            //        }
            //        catch (Exception)
            //        {
            //            //MessageLabel = "Try After Some Time";
            //            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Try After Some Time.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //        }
            //        finally
            //        {
            //            HttpContext.Current.Response.End();
            //        }
            //    }
            //    catch (Exception Ex)
            //    {
            //
            //    }
            //}
            //else
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //}

        }
        bool Searchvalidation()
        {
            if (drpDivision.SelectedValue == string.Empty)
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
        public void ExportToExcel()
        {

            gvBudgetList.AllowPaging = false;
            gvActual1.AllowPaging = false;
            gvActualWithoutSEISCurrent.AllowPaging = false;
            gvActual2.AllowPaging = false;
            gvActualSLastYearWithoutSEIS.AllowPaging = false;
            gvActual3.AllowPaging = false;
            bool? IsColumnExists = false;
            int count = 0;
            DataTable dt = new DataTable("Page_1");
            if (gvBudgetList.Rows.Count > 0)
            {
                #region GridView1
                DataRow dr = null;

                foreach (System.Web.UI.WebControls.TableCell cell in gvBudgetList.HeaderRow.Cells)
                {
                    dt.Columns.Add(cell.Text);
                    if (count == 0)
                    {
                        dt.Rows.Add();
                        IsColumnExists = true;
                    }
                    count = count + 1;
                    dt.Rows[dt.Rows.Count - 1][cell.Text] = cell.Text;
                }

                foreach (GridViewRow row in gvBudgetList.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }
                dr = dt.NewRow();
                dr[0] = "ALS-" + drpDivision.SelectedItem.Text + " Budget-" + drpFinancialYear.SelectedItem.Text;
                dt.Rows.InsertAt(dr, 0);
                //   dt.Columns.Remove("a");
                //   dt.AcceptChanges();

                #endregion
            }
            if (gvActual1.Rows.Count > 0)
            {

                #region GridView2
                if (IsColumnExists == true)
                {

                    DataRow dr1 = null;
                    dr1 = dt.NewRow();
                    dr1[0] = "ALS-" + drpDivision.SelectedItem.Text + " Actual-" + drpFinancialYear.SelectedItem.Text;
                    dt.Rows.InsertAt(dr1, dt.Rows.Count + 2);
                    count = 0;
                }



                foreach (System.Web.UI.WebControls.TableCell cell in gvActual1.HeaderRow.Cells)
                {
                    string temp2 = string.Empty;
                    if (dt.Columns.Count > count)
                    {
                        temp2 = dt.Columns[count].ColumnName.ToString();
                    }

                    if (dt.Columns.Count <= count)
                    {
                        temp2 = cell.Text;
                        dt.Columns.Add(temp2);
                    }
                    if (count == 0)
                    {
                        dt.Rows.Add();
                    }

                    dt.Rows[dt.Rows.Count - 1][temp2] = cell.Text;
                    count = count + 1;
                }

                foreach (GridViewRow row in gvActual1.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }

                if (IsColumnExists == false)
                {

                    DataRow dr1 = null;
                    dr1 = dt.NewRow();
                    dr1[0] = "ALS-" + drpDivision.SelectedItem.Text + " Actual-" + drpFinancialYear.SelectedItem.Text;
                    dt.Rows.InsertAt(dr1, 0);
                    count = 0;
                    IsColumnExists = true;
                }

                #endregion
            }
            if (gvActualWithoutSEISCurrent.Rows.Count > 0)
            {

                #region GridView3

                if (IsColumnExists == true)
                {
                    DataRow dr2 = null;
                    dr2 = dt.NewRow();
                    dr2[0] = lblDivisionNameCurrentSEIS.Text + lblCurrentSEISActualHeading.Text + lblFinYear.Text;
                    dt.Rows.InsertAt(dr2, dt.Rows.Count + 2);
                    count = 0;
                }


                foreach (System.Web.UI.WebControls.TableCell cell in gvActualWithoutSEISCurrent.HeaderRow.Cells)
                {
                    string temp2 = string.Empty;
                    if (dt.Columns.Count > count)
                    {
                        temp2 = dt.Columns[count].ColumnName.ToString();
                    }

                    if (dt.Columns.Count <= count)
                    {
                        temp2 = cell.Text;
                        dt.Columns.Add(temp2);
                    }
                    if (count == 0)
                    {
                        dt.Rows.Add();
                    }
                    dt.Rows[dt.Rows.Count - 1][temp2] = cell.Text;
                    count = count + 1;
                }

                foreach (GridViewRow row in gvActualWithoutSEISCurrent.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }
                if (IsColumnExists == false)
                {
                    DataRow dr2 = null;
                    dr2 = dt.NewRow();
                    dr2[0] = lblDivisionNameCurrentSEIS.Text + lblCurrentSEISActualHeading.Text + lblFinYear.Text;
                    dt.Rows.InsertAt(dr2, 0);
                    IsColumnExists = true;
                    count = 0;
                }
                #endregion
            }
            if (gvActual2.Rows.Count > 0)
            {

                #region GridView4
                if (IsColumnExists == true)
                {
                    DataRow dr3 = null;
                    dr3 = dt.NewRow();
                    dr3[0] = lblDivisionName3.Text + lblLastYearActualHeading.Text + lblActual2.Text;
                    dt.Rows.InsertAt(dr3, dt.Rows.Count + 2);
                    count = 0;
                }



                foreach (System.Web.UI.WebControls.TableCell cell in gvActual2.HeaderRow.Cells)
                {
                    string temp2 = string.Empty;
                    if (dt.Columns.Count > count)
                    {
                        temp2 = dt.Columns[count].ColumnName.ToString();
                    }

                    if (dt.Columns.Count <= count)
                    {
                        temp2 = cell.Text;
                        dt.Columns.Add(temp2);
                    }
                    if (count == 0)
                    {
                        dt.Rows.Add();
                    }

                    dt.Rows[dt.Rows.Count - 1][temp2] = cell.Text;
                    count = count + 1;
                }

                foreach (GridViewRow row in gvActual2.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }
                if (IsColumnExists == false)
                {
                    DataRow dr3 = null;
                    dr3 = dt.NewRow();
                    dr3[0] = lblDivisionName3.Text + lblLastYearActualHeading.Text + lblActual2.Text;
                    dt.Rows.InsertAt(dr3, 0);
                    count = 0;
                    IsColumnExists = true;
                }
                #endregion
            }
            if (gvActualSLastYearWithoutSEIS.Rows.Count > 0)
            {
                #region GridView5
                if (IsColumnExists == true)
                {
                    DataRow dr4 = null;
                    dr4 = dt.NewRow();
                    dr4[0] = lblDivisionNameSLastSEIS.Text + lblSLastSEISActualHeading.Text + lblFinYearSLastSEIS.Text;
                    dt.Rows.InsertAt(dr4, dt.Rows.Count + 2);
                    count = 0;
                }

                foreach (System.Web.UI.WebControls.TableCell cell in gvActualSLastYearWithoutSEIS.HeaderRow.Cells)
                {
                    string temp2 = string.Empty;
                    if (dt.Columns.Count > count)
                    {
                        temp2 = dt.Columns[count].ColumnName.ToString();
                    }

                    if (dt.Columns.Count <= count)
                    {
                        temp2 = cell.Text;
                        dt.Columns.Add(temp2);
                    }
                    if (count == 0)
                    {
                        dt.Rows.Add();
                    }

                    dt.Rows[dt.Rows.Count - 1][temp2] = cell.Text;
                    count = count + 1;
                }

                foreach (GridViewRow row in gvActualSLastYearWithoutSEIS.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }
                if (IsColumnExists == false)
                {
                    DataRow dr4 = null;
                    dr4 = dt.NewRow();
                    dr4[0] = lblDivisionNameSLastSEIS.Text + lblSLastSEISActualHeading.Text + lblFinYearSLastSEIS.Text;
                    dt.Rows.InsertAt(dr4, 0);
                    IsColumnExists = true;
                    count = 0;
                }
                #endregion
            }
            if (gvActual3.Rows.Count > 0)
            {
                #region GridView6
                if (IsColumnExists == true)
                {
                    DataRow dr5 = null;
                    dr5 = dt.NewRow();
                    dr5[0] = lblDivisionName4.Text + lblSLastActualHeading.Text + lblActual3.Text;
                    dt.Rows.InsertAt(dr5, dt.Rows.Count + 2);
                    count = 0;
                }


                foreach (System.Web.UI.WebControls.TableCell cell in gvActual3.HeaderRow.Cells)
                {
                    string temp2 = string.Empty;
                    if (dt.Columns.Count > count)
                    {
                        temp2 = dt.Columns[count].ColumnName.ToString();
                    }

                    if (dt.Columns.Count <= count)
                    {
                        temp2 = cell.Text;
                        dt.Columns.Add(temp2);
                    }
                    if (count == 0)
                    {
                        dt.Rows.Add();
                    }

                    dt.Rows[dt.Rows.Count - 1][temp2] = cell.Text;
                    count = count + 1;
                }

                foreach (GridViewRow row in gvActual3.Rows)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        dt.Rows[dt.Rows.Count - 1][j] = row.Cells[j].Text;
                    }
                }

                if (IsColumnExists == false)
                {
                    DataRow dr5 = null;
                    dr5 = dt.NewRow();
                    dr5[0] = lblDivisionName4.Text + lblSLastActualHeading.Text + lblActual3.Text;
                    dt.Rows.InsertAt(dr5, 0);
                    IsColumnExists = true;
                    count = 0;
                }
                #endregion
            }

            DataColumnCollection columns = dt.Columns;



            if (columns.Contains("a"))
            {
                dt.Columns.Remove("a");
            }
            if (columns.Contains("-"))
            {
                dt.Columns.Remove("-");
            }
            if (columns.Contains("1"))
            {
                dt.Columns.Remove("1");
            }
            dt.AcceptChanges();

            if(drpDivision.SelectedItem.Text=="AFIL")
            {
                ChangeColorForAfil(dt);
            }
            else 
            {
                ChangeColorForNonAfil(dt);
            }


        }
        public void ChangeColorForNonAfil(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString()== "&nbsp;")
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = "-";
                    }
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                bool IsActualProcess = false;
                var s = wb.Worksheets.Add(dt);
                if (dt.Rows.Count > 1)
                {
                    if (gvActual1.Rows.Count > 0 && gvBudgetList.Rows.Count<=0)
                    {
                        IsActualProcess = true;
                        s.Worksheet.Range("A3:s3").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                        s.Worksheet.Range("A6:s6").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A9:s9").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                        s.Worksheet.Range("A10:s10").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A12:s12").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A19:s19").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A20:s20").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    }
                    else
                    {

                        s.Worksheet.Range("A3:P3").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                        s.Worksheet.Range("A6:P6").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A9:P9").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                        s.Worksheet.Range("A10:P10").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A12:P12").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A19:P19").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A20:P20").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    }
                }

                if (dt.Rows.Count > 27)
                {

                    if (gvActual1.Rows.Count > 0 & IsActualProcess == false)
                    {
                        s.Worksheet.Range("A27:S27").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                        s.Worksheet.Range("A30:S30").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A33:S33").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                        s.Worksheet.Range("A34:S34").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A36:S36").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A43:S43").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A44:S44").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        
                    }
                    else
                    {
                        s.Worksheet.Range("A27:p27").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                        s.Worksheet.Range("A30:p30").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A33:p33").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                        s.Worksheet.Range("A34:p34").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A36:p36").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A43:p43").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        s.Worksheet.Range("A44:p44").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                        
                    }
                }
                if (dt.Rows.Count > 51)
                {

                    s.Worksheet.Range("A51:P51").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                    s.Worksheet.Range("A54:P54").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A57:P57").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                    s.Worksheet.Range("A58:P58").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A60:P60").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A67:P67").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A68:P68").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    
                }
                if (dt.Rows.Count > 75)
                {

                    s.Worksheet.Range("A75:P75").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                    s.Worksheet.Range("A78:P78").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A81:P81").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                    s.Worksheet.Range("A82:P82").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A84:P84").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A91:P91").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A92:P92").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    
                }


                if (dt.Rows.Count > 99)
                {

                    s.Worksheet.Range("A99:P99").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                    s.Worksheet.Range("A102:P102").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A105:P105").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                    s.Worksheet.Range("A106:P106").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A108:P108").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A115:P115").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A116:P116").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    
                }
                if (dt.Rows.Count > 123)
                {

                    s.Worksheet.Range("A123:P123").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");
                    s.Worksheet.Range("A125:P125").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A128:P128").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");
                    s.Worksheet.Range("A129:P129").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A131:P131").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A138:P138").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    s.Worksheet.Range("A139:P139").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                    
                }

                s.Worksheet.Style.Font.FontSize = 10;
                s.Row(1).Hide();
                s.Tables.FirstOrDefault().ShowAutoFilter = false;
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
        public void ChangeColorForAfil(DataTable dt)
        {


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString() == "&nbsp;")
                    {
                        // Write your Custom Code
                        dt.Rows[i][j] = "-";
                    }
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                bool IsActualProcess = false;
                var s = wb.Worksheets.Add(dt);
                if (dt.Rows.Count > 1)
                {


                    if (gvActual1.Rows.Count > 0 && gvBudgetList.Rows.Count <= 0)
                    {
                        IsActualProcess = true;
                        s.Worksheet.Range("A3:S3").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");   //+7
                        s.Worksheet.Range("A6:S6").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");   //+3
                        s.Worksheet.Range("A9:S9").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");   //+3
                        s.Worksheet.Range("A10:S10").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+1
                        s.Worksheet.Range("A13:S13").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda"); //+3
                        s.Worksheet.Range("A15:S15").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+2
                        s.Worksheet.Range("A22:S22").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+7
                        s.Worksheet.Range("A23:S23").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+1
                    }

                    else
                    {

                        s.Worksheet.Range("A3:P3").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");   //+7
                        s.Worksheet.Range("A6:P6").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");   //+3
                        s.Worksheet.Range("A9:P9").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");   //+3
                        s.Worksheet.Range("A10:P10").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+1
                        s.Worksheet.Range("A13:P13").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda"); //+3
                        s.Worksheet.Range("A15:P15").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+2
                        s.Worksheet.Range("A22:P22").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+7
                        s.Worksheet.Range("A23:P23").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+1
                    }
                }

                if (dt.Rows.Count > 30)
                {

                    if (gvActual1.Rows.Count > 0 && IsActualProcess == false)
                    {
                        s.Worksheet.Range("A30:S30").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");//+7
                        s.Worksheet.Range("A33:S33").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+3
                        s.Worksheet.Range("A36:S36").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                        s.Worksheet.Range("A37:S37").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                        s.Worksheet.Range("A40:S40").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                        s.Worksheet.Range("A42:S42").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+2
                        s.Worksheet.Range("A49:S49").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+7
                        s.Worksheet.Range("A50:S50").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                    }
                    else
                    {

                        s.Worksheet.Range("A30:p30").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");//+7
                        s.Worksheet.Range("A33:p33").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+3
                        s.Worksheet.Range("A36:p36").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                        s.Worksheet.Range("A37:p37").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                        s.Worksheet.Range("A40:p40").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                        s.Worksheet.Range("A42:p42").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+2
                        s.Worksheet.Range("A49:p49").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+7
                        s.Worksheet.Range("A50:p50").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1                                                                                      //+1
                    }
                }
                if (dt.Rows.Count > 57)
                {

                    s.Worksheet.Range("A57:P57").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");//+7
                    s.Worksheet.Range("A60:P60").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+3
                    s.Worksheet.Range("A63:P63").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A64:P64").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                    s.Worksheet.Range("A67:P67").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A69:P69").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+2
                    s.Worksheet.Range("A78:P78").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+7
                    s.Worksheet.Range("A79:P79").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                }
                if (dt.Rows.Count > 86)
                {

                    s.Worksheet.Range("A86:P86").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");//+7
                    s.Worksheet.Range("A89:P89").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+3
                    s.Worksheet.Range("A92:P92").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A93:P93").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                    s.Worksheet.Range("A96:P96").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A98:P98").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+2
                    s.Worksheet.Range("A105:P105").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+7
                    s.Worksheet.Range("A106:P106").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                }


                if (dt.Rows.Count > 113)
                {

                    s.Worksheet.Range("A113:P113").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");   //+7
                    s.Worksheet.Range("A116:P116").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+3
                    s.Worksheet.Range("A119:P119").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda"); //+3
                    s.Worksheet.Range("A120:P120").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+1
                    s.Worksheet.Range("A123:P123").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda"); //+3
                    s.Worksheet.Range("A125:P125").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+2
                    s.Worksheet.Range("A132:P132").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9"); //+7
                    s.Worksheet.Range("A133:P133").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                }
                if (dt.Rows.Count > 140)
                {

                    s.Worksheet.Range("A140:P140").Style.Fill.BackgroundColor = XLColor.FromHtml("#f8cbad");//+7
                    s.Worksheet.Range("A143:P143").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+3
                    s.Worksheet.Range("A146:P146").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A147:P147").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                    s.Worksheet.Range("A150:P150").Style.Fill.BackgroundColor = XLColor.FromHtml("#e2efda");//+3
                    s.Worksheet.Range("A152:P152").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+2
                    s.Worksheet.Range("A159:P159").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+7
                    s.Worksheet.Range("A160:P160").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");//+1
                }

                s.Worksheet.Style.Font.FontSize = 10;
                s.Row(1).Hide();
                s.Tables.FirstOrDefault().ShowAutoFilter = false;
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
        bool BudgetVsActualValidation()
        {
            if (drpFinancialYear.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Financial Year!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpDivision.SelectedValue == string.Empty)
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
            if (Drpmonth.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Month!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            return true;
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

        protected void gvBudgetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBudgetList.PageIndex = e.NewPageIndex;
            bindGrd();
        }

        protected void drpDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvBudgetList.DataBind();
            gvActual1.DataBind();
            gvActual2.DataBind();
            gvActual3.DataBind();
            lblBudget.Text = string.Empty;
            lblActual1.Text = string.Empty;
            lblActual2.Text = string.Empty;
            lblActual3.Text = string.Empty;
            lblDivisionName1.Text = string.Empty;
            lblDivisionName2.Text = string.Empty;
            lblDivisionName3.Text = string.Empty;
            lblDivisionName4.Text = string.Empty;
            lnkActualVsBudget.Visible = false;
            // txtRecordFound.Text = "0";
            gvActualSLastYearWithoutSEIS.DataBind();
            gvActualWithoutSEISCurrent.DataBind();
            lblDivisionNameCurrentSEIS.Text = string.Empty;
            lblDivisionNameSLastSEIS.Text = string.Empty;
            lblFinYear.Text = string.Empty;
            lblFinYearSLastSEIS.Text = string.Empty;

            lblBudgetHeading.Visible = false;
            lblCurrentActualHeading.Visible = false;
            lblLastYearActualHeading.Visible = false;
            lblSLastActualHeading.Visible = false;
            lblCurrentSEISActualHeading.Visible = false;
            lblSLastSEISActualHeading.Visible = false;

            if (drpDivision.SelectedValue == string.Empty || drpDivision.SelectedValue == "ALS Consol" || drpDivision.SelectedValue == "AFIL Consol")
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

        protected void gvBudgetList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //gvBudgetList.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("Color", "white");

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                





                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }

                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text != string.Empty)
                        e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%";
                    if (e.Row.Cells[3].Text != string.Empty)
                        e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%";
                    if (e.Row.Cells[4].Text != string.Empty)
                        e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%";
                    if (e.Row.Cells[5].Text != string.Empty)
                        e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%";
                    if (e.Row.Cells[6].Text != string.Empty)
                        e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%";
                    if (e.Row.Cells[7].Text != string.Empty)
                        e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%";
                    if (e.Row.Cells[8].Text != string.Empty)
                        e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%";
                    if (e.Row.Cells[9].Text != string.Empty)
                        e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%";
                    if (e.Row.Cells[10].Text != string.Empty)
                        e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%";
                    if (e.Row.Cells[11].Text != string.Empty)
                        e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%";
                    if (e.Row.Cells[12].Text != string.Empty)
                        e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%";
                    if (e.Row.Cells[13].Text != string.Empty)
                        e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%";
                    if (e.Row.Cells[14].Text != string.Empty)
                        e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%";
                    if (e.Row.Cells[16].Text != string.Empty)
                        e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%";


                }
                if (e.Row.Cells[2].Text == "0.00") { e.Row.Cells[2].Text = "-"; }
                if (e.Row.Cells[3].Text == "0.00") { e.Row.Cells[3].Text = "-"; }
                if (e.Row.Cells[4].Text == "0.00") { e.Row.Cells[4].Text = "-"; }
                if (e.Row.Cells[5].Text == "0.00") { e.Row.Cells[5].Text = "-"; }
                if (e.Row.Cells[6].Text == "0.00") { e.Row.Cells[6].Text = "-"; }
                if (e.Row.Cells[7].Text == "0.00") { e.Row.Cells[7].Text = "-"; }
                if (e.Row.Cells[8].Text == "0.00") { e.Row.Cells[8].Text = "-"; }
                if (e.Row.Cells[9].Text == "0.00") { e.Row.Cells[9].Text = "-"; }
                if (e.Row.Cells[10].Text == "0.00") { e.Row.Cells[10].Text = "-"; }
                if (e.Row.Cells[11].Text == "0.00") { e.Row.Cells[11].Text = "-"; }
                if (e.Row.Cells[12].Text == "0.00") { e.Row.Cells[12].Text = "-"; }
                if (e.Row.Cells[13].Text == "0.00") { e.Row.Cells[13].Text = "-"; }
                if (e.Row.Cells[14].Text == "0.00") { e.Row.Cells[14].Text = "-"; }
                if (e.Row.Cells[16].Text == "0.00") { e.Row.Cells[16].Text = "-"; }
                e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void gvActual1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("Color", "white");


                    e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("Color", "white");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "0.00") { e.Row.Cells[2].Text = "-"; }
                if (e.Row.Cells[3].Text == "0.00") { e.Row.Cells[3].Text = "-"; }
                if (e.Row.Cells[4].Text == "0.00") { e.Row.Cells[4].Text = "-"; }
                if (e.Row.Cells[5].Text == "0.00") { e.Row.Cells[5].Text = "-"; }
                if (e.Row.Cells[6].Text == "0.00") { e.Row.Cells[6].Text = "-"; }
                if (e.Row.Cells[7].Text == "0.00") { e.Row.Cells[7].Text = "-"; }
                if (e.Row.Cells[8].Text == "0.00") { e.Row.Cells[8].Text = "-"; }
                if (e.Row.Cells[9].Text == "0.00") { e.Row.Cells[9].Text = "-"; }
                if (e.Row.Cells[10].Text == "0.00") { e.Row.Cells[10].Text = "-"; }
                if (e.Row.Cells[11].Text == "0.00") { e.Row.Cells[11].Text = "-"; }
                if (e.Row.Cells[12].Text == "0.00") { e.Row.Cells[12].Text = "-"; }
                if (e.Row.Cells[13].Text == "0.00") { e.Row.Cells[13].Text = "-"; }
                if (e.Row.Cells[14].Text == "0.00") { e.Row.Cells[14].Text = "-"; }
                if (e.Row.Cells[15].Text == "0.00") { e.Row.Cells[15].Text = "-"; }
                if (e.Row.Cells[16].Text == "0.00") { e.Row.Cells[16].Text = "-"; }
                if (e.Row.Cells[17].Text == "0.00") { e.Row.Cells[17].Text = "-"; }
                if (e.Row.Cells[18].Text == "0.00") { e.Row.Cells[18].Text = "-"; }
                if (e.Row.Cells[19].Text == "0.00") { e.Row.Cells[19].Text = "-"; }
                if (e.Row.Cells[20].Text == "0.00") { e.Row.Cells[20].Text = "-"; }
                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }
                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text == "-" || e.Row.Cells[2].Text == "&nbsp;") { } else { e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[3].Text == "-" || e.Row.Cells[3].Text == "&nbsp;") { } else { e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[4].Text == "-" || e.Row.Cells[4].Text == "&nbsp;") { } else { e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[5].Text == "-" || e.Row.Cells[5].Text == "&nbsp;") { } else { e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[6].Text == "-" || e.Row.Cells[6].Text == "&nbsp;") { } else { e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[7].Text == "-" || e.Row.Cells[7].Text == "&nbsp;") { } else { e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[8].Text == "-" || e.Row.Cells[8].Text == "&nbsp;") { } else { e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[9].Text == "-" || e.Row.Cells[9].Text == "&nbsp;") { } else { e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[10].Text == "-" || e.Row.Cells[10].Text == "&nbsp;") { } else { e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[11].Text == "-" || e.Row.Cells[11].Text == "&nbsp;") { } else { e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[12].Text == "-" || e.Row.Cells[12].Text == "&nbsp;") { } else { e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[13].Text == "-" || e.Row.Cells[13].Text == "&nbsp;") { } else { e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[14].Text == "-" || e.Row.Cells[14].Text == "&nbsp;") { } else { e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[16].Text == "-" || e.Row.Cells[16].Text == "&nbsp;") { } else { e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[18].Text == "-" || e.Row.Cells[18].Text == "&nbsp;") { } else { e.Row.Cells[18].Text = Math.Round(decimal.Parse(e.Row.Cells[18].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[19].Text == "-" || e.Row.Cells[19].Text == "&nbsp;") { } else { e.Row.Cells[19].Text = Math.Round(decimal.Parse(e.Row.Cells[19].Text), 1).ToString() + "%"; }
                    if (e.Row.Cells[20].Text == "-" || e.Row.Cells[20].Text == "&nbsp;") { } else { e.Row.Cells[20].Text = Math.Round(decimal.Parse(e.Row.Cells[20].Text), 1).ToString() + "%"; }
                }
                e.Row.Cells[e.Row.Cells.Count - 4].Style.Add("background-color", "white");
                e.Row.Cells[e.Row.Cells.Count - 6].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void gvActual2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("Color", "white");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }
                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text != string.Empty && e.Row.Cells[2].Text != "&nbsp;")
                        e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%";
                    if (e.Row.Cells[3].Text != string.Empty && e.Row.Cells[3].Text != "&nbsp;")
                        e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%";
                    if (e.Row.Cells[4].Text != string.Empty && e.Row.Cells[4].Text != "&nbsp;")
                        e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%";
                    if (e.Row.Cells[5].Text != string.Empty && e.Row.Cells[5].Text != "&nbsp;")
                        e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%";
                    if (e.Row.Cells[6].Text != string.Empty && e.Row.Cells[6].Text != "&nbsp;")
                        e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%";
                    if (e.Row.Cells[7].Text != string.Empty && e.Row.Cells[7].Text != "&nbsp;")
                        e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%";
                    if (e.Row.Cells[8].Text != string.Empty && e.Row.Cells[8].Text != "&nbsp;")
                        e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%";
                    if (e.Row.Cells[9].Text != string.Empty && e.Row.Cells[9].Text != "&nbsp;")
                        e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%";
                    if (e.Row.Cells[10].Text != string.Empty && e.Row.Cells[10].Text != "&nbsp;")
                        e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%";
                    if (e.Row.Cells[11].Text != string.Empty && e.Row.Cells[11].Text != "&nbsp;")
                        e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%";
                    if (e.Row.Cells[12].Text != string.Empty && e.Row.Cells[12].Text != "&nbsp;")
                        e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%";
                    if (e.Row.Cells[13].Text != string.Empty && e.Row.Cells[13].Text != "&nbsp;")
                        e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%";
                    if (e.Row.Cells[14].Text != string.Empty && e.Row.Cells[14].Text != "&nbsp;")
                        e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%";
                    if (e.Row.Cells[15].Text != string.Empty && e.Row.Cells[15].Text != "&nbsp;")
                        e.Row.Cells[15].Text = Math.Round(decimal.Parse(e.Row.Cells[15].Text), 1).ToString() + "%";
                    if (e.Row.Cells[16].Text != string.Empty && e.Row.Cells[16].Text != "&nbsp;")
                        e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%";
                    //e.Row.Cells[16].Text = e.Row.Cells[16].Text + "%";
                }
                e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void gvActual3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("Color", "white");

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }
                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text != string.Empty && e.Row.Cells[2].Text != "&nbsp;")
                        e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%";
                    if (e.Row.Cells[3].Text != string.Empty && e.Row.Cells[3].Text != "&nbsp;")
                        e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%";
                    if (e.Row.Cells[4].Text != string.Empty && e.Row.Cells[4].Text != "&nbsp;")
                        e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%";
                    if (e.Row.Cells[5].Text != string.Empty && e.Row.Cells[5].Text != "&nbsp;")
                        e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%";
                    if (e.Row.Cells[6].Text != string.Empty && e.Row.Cells[6].Text != "&nbsp;")
                        e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%";
                    if (e.Row.Cells[7].Text != string.Empty && e.Row.Cells[7].Text != "&nbsp;")
                        e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%";
                    if (e.Row.Cells[8].Text != string.Empty && e.Row.Cells[8].Text != "&nbsp;")
                        e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%";
                    if (e.Row.Cells[9].Text != string.Empty && e.Row.Cells[9].Text != "&nbsp;")
                        e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%";
                    if (e.Row.Cells[10].Text != string.Empty && e.Row.Cells[10].Text != "&nbsp;")
                        e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%";
                    if (e.Row.Cells[11].Text != string.Empty && e.Row.Cells[11].Text != "&nbsp;")
                        e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%";
                    if (e.Row.Cells[12].Text != string.Empty && e.Row.Cells[12].Text != "&nbsp;")
                        e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%";
                    if (e.Row.Cells[13].Text != string.Empty && e.Row.Cells[13].Text != "&nbsp;")
                        e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%";
                    if (e.Row.Cells[14].Text != string.Empty && e.Row.Cells[14].Text != "&nbsp;")
                        e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%";
                    if (e.Row.Cells[15].Text != string.Empty && e.Row.Cells[15].Text != "&nbsp;")
                        e.Row.Cells[15].Text = Math.Round(decimal.Parse(e.Row.Cells[15].Text), 1).ToString() + "%";

                    if (e.Row.Cells[16].Text != string.Empty && e.Row.Cells[16].Text != "&nbsp;")
                        e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%";
                    //e.Row.Cells[16].Text = e.Row.Cells[16].Text + "%";
                }
                e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void lnkActualVsBudget_Click(object sender, EventArgs e)
        {
            if (!BudgetVsActualValidation())
            {
                return;
            }
            string DivID = string.Empty;
            string DivisionName = string.Empty;
            if (drpSubdivision.Items.Count > 0)
            {
                DivID = drpSubdivision.SelectedValue;
                DivisionName = drpSubdivision.SelectedItem.Text;
            }
            else
            {
                DivID = drpDivision.SelectedValue;
                DivisionName = drpDivision.SelectedItem.Text;
            }

            string url = "/FrontEnd/CargowiseDashboard/BudgetVsActualReport.aspx?FinYear=" + drpFinancialYear.SelectedValue + "&MName=" + Drpmonth.SelectedValue + "&DivID=" + DivID + "&DivName=" + DivisionName + "&726782dsjsbj=3877843hdws";
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.open('");
            //sb.Append(url);
            //sb.Append("');");
            //sb.Append("</script>");
            //ClientScript.RegisterStartupScript(this.GetType(),
            //        "script", sb.ToString());

            //string url = "Popup.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=1024,height=600,left=100,top=50,resizable=no');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }

        protected void gvActualWithoutSEISCurrent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("Color", "white");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = 202;
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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }
                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text != string.Empty && e.Row.Cells[2].Text != "&nbsp;")
                        e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%";
                    if (e.Row.Cells[3].Text != string.Empty && e.Row.Cells[3].Text != "&nbsp;")
                        e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%";
                    if (e.Row.Cells[4].Text != string.Empty && e.Row.Cells[4].Text != "&nbsp;")
                        e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%";
                    if (e.Row.Cells[5].Text != string.Empty && e.Row.Cells[5].Text != "&nbsp;")
                        e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%";
                    if (e.Row.Cells[6].Text != string.Empty && e.Row.Cells[6].Text != "&nbsp;")
                        e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%";
                    if (e.Row.Cells[7].Text != string.Empty && e.Row.Cells[7].Text != "&nbsp;")
                        e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%";
                    if (e.Row.Cells[8].Text != string.Empty && e.Row.Cells[8].Text != "&nbsp;")
                        e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%";
                    if (e.Row.Cells[9].Text != string.Empty && e.Row.Cells[9].Text != "&nbsp;")
                        e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%";
                    if (e.Row.Cells[10].Text != string.Empty && e.Row.Cells[10].Text != "&nbsp;")
                        e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%";
                    if (e.Row.Cells[11].Text != string.Empty && e.Row.Cells[11].Text != "&nbsp;")
                        e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%";
                    if (e.Row.Cells[12].Text != string.Empty && e.Row.Cells[12].Text != "&nbsp;")
                        e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%";
                    if (e.Row.Cells[13].Text != string.Empty && e.Row.Cells[13].Text != "&nbsp;")
                        e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%";
                    if (e.Row.Cells[14].Text != string.Empty && e.Row.Cells[14].Text != "&nbsp;")
                        e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%";
                    if (e.Row.Cells[15].Text != string.Empty && e.Row.Cells[15].Text != "&nbsp;")
                        e.Row.Cells[15].Text = Math.Round(decimal.Parse(e.Row.Cells[15].Text), 1).ToString() + "%";
                    if (e.Row.Cells[16].Text != string.Empty && e.Row.Cells[16].Text != "&nbsp;")
                        e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%";
                    //e.Row.Cells[16].Text = e.Row.Cells[16].Text + "%";
                }
                e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void gvActualSLastYearWithoutSEIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    if (i == 0)
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:left !important");
                    }
                    else
                    {
                        e.Row.Cells[i].Attributes.Add("style", "text-align:right !important");
                    }
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("BORDER", "1px");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
                    e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("Color", "white");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Visible = false;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "Gross Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Total Indirect Cost")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                }
                if (e.Row.Cells[0].Text == "EBITDA Before CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "EBITDA after CC")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "PAT")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }
                if (e.Row.Cells[0].Text == "Cash Profit")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                }

                if (drpDivision.SelectedValue == "1")
                {

                    if (e.Row.Cells[0].Text == "Total Indirect Cost- Common")
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#e2efda");
                    }
                }
                if (e.Row.Cells[0].Text == "Gross Profit%" || e.Row.Cells[0].Text == "EBITDA Before CC%" || e.Row.Cells[0].Text == "EBITDA After CC%"
                    || e.Row.Cells[0].Text == "% of Salaries to Revenue" || e.Row.Cells[0].Text == "% of Indirect Cost to Revenue")
                {
                    if (e.Row.Cells[2].Text != string.Empty && e.Row.Cells[2].Text != "&nbsp;")
                        e.Row.Cells[2].Text = Math.Round(decimal.Parse(e.Row.Cells[2].Text), 1).ToString() + "%";
                    if (e.Row.Cells[3].Text != string.Empty && e.Row.Cells[3].Text != "&nbsp;")
                        e.Row.Cells[3].Text = Math.Round(decimal.Parse(e.Row.Cells[3].Text), 1).ToString() + "%";
                    if (e.Row.Cells[4].Text != string.Empty && e.Row.Cells[4].Text != "&nbsp;")
                        e.Row.Cells[4].Text = Math.Round(decimal.Parse(e.Row.Cells[4].Text), 1).ToString() + "%";
                    if (e.Row.Cells[5].Text != string.Empty && e.Row.Cells[5].Text != "&nbsp;")
                        e.Row.Cells[5].Text = Math.Round(decimal.Parse(e.Row.Cells[5].Text), 1).ToString() + "%";
                    if (e.Row.Cells[6].Text != string.Empty && e.Row.Cells[6].Text != "&nbsp;")
                        e.Row.Cells[6].Text = Math.Round(decimal.Parse(e.Row.Cells[6].Text), 1).ToString() + "%";
                    if (e.Row.Cells[7].Text != string.Empty && e.Row.Cells[7].Text != "&nbsp;")
                        e.Row.Cells[7].Text = Math.Round(decimal.Parse(e.Row.Cells[7].Text), 1).ToString() + "%";
                    if (e.Row.Cells[8].Text != string.Empty && e.Row.Cells[8].Text != "&nbsp;")
                        e.Row.Cells[8].Text = Math.Round(decimal.Parse(e.Row.Cells[8].Text), 1).ToString() + "%";
                    if (e.Row.Cells[9].Text != string.Empty && e.Row.Cells[9].Text != "&nbsp;")
                        e.Row.Cells[9].Text = Math.Round(decimal.Parse(e.Row.Cells[9].Text), 1).ToString() + "%";
                    if (e.Row.Cells[10].Text != string.Empty && e.Row.Cells[10].Text != "&nbsp;")
                        e.Row.Cells[10].Text = Math.Round(decimal.Parse(e.Row.Cells[10].Text), 1).ToString() + "%";
                    if (e.Row.Cells[11].Text != string.Empty && e.Row.Cells[11].Text != "&nbsp;")
                        e.Row.Cells[11].Text = Math.Round(decimal.Parse(e.Row.Cells[11].Text), 1).ToString() + "%";
                    if (e.Row.Cells[12].Text != string.Empty && e.Row.Cells[12].Text != "&nbsp;")
                        e.Row.Cells[12].Text = Math.Round(decimal.Parse(e.Row.Cells[12].Text), 1).ToString() + "%";
                    if (e.Row.Cells[13].Text != string.Empty && e.Row.Cells[13].Text != "&nbsp;")
                        e.Row.Cells[13].Text = Math.Round(decimal.Parse(e.Row.Cells[13].Text), 1).ToString() + "%";
                    if (e.Row.Cells[14].Text != string.Empty && e.Row.Cells[14].Text != "&nbsp;")
                        e.Row.Cells[14].Text = Math.Round(decimal.Parse(e.Row.Cells[14].Text), 1).ToString() + "%";
                    if (e.Row.Cells[15].Text != string.Empty && e.Row.Cells[15].Text != "&nbsp;")
                        e.Row.Cells[15].Text = Math.Round(decimal.Parse(e.Row.Cells[15].Text), 1).ToString() + "%";
                    if (e.Row.Cells[16].Text != string.Empty && e.Row.Cells[16].Text != "&nbsp;")
                        e.Row.Cells[16].Text = Math.Round(decimal.Parse(e.Row.Cells[16].Text), 1).ToString() + "%";
                    //e.Row.Cells[16].Text = e.Row.Cells[16].Text + "%";
                }
                e.Row.Cells[e.Row.Cells.Count - 2].Style.Add("background-color", "white");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}