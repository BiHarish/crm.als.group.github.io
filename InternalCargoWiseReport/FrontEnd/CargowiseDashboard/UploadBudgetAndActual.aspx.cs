using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class UploadBudgetAndActual : System.Web.UI.Page
    {
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;
        DataSet ds = new DataSet();
        string fileName;
        DataTable dtFinal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDrp();
            }
        }

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


            DataSet typeResult = _data.getAllType();
            if (typeResult != null)
            {
                drpType.DataSource = typeResult.Tables[0];
                drpType.DataValueField = "mtyid";
                drpType.DataTextField = "mtycode";
                drpType.DataBind();

            }
        }

        bool Searchvalidation()
        {
            if (drpFinancialYear.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Financial Year!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpType.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type!!", "Error!", Toastr.ToastPosition.TopCenter, true);
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
            else if (!fileUpload.HasFile)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select File!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }

            return true;
        }

        void budgetAndActualUpload()
        {

            String path = string.Empty;

            if (fileUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension != ".xlsx")
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select excel file(.xlsx)!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            path = Server.MapPath(fileUpload.FileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            if (fileUpload.HasFile)
            {
                try
                {
                    string sSavePath = "~/FrontEnd/Operations/WhLeadStatusFile/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    //sSavePath = sSavePath + "\\UploadedFiles\\";
                    fileName = fileUpload.FileName;
                    sSavePath = sSavePath + fileName;
                    fileUpload.SaveAs(sSavePath);

                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();

                    oleCmd = new OleDbCommand("SELECT [Particulars],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec],[Jan],[Feb],[Mar],[Total],'" + LovelySession.Lovely.User.Id + "'as CreateBy,'" + drpFinancialYear.SelectedValue + "' as FinYear,'" + drpType.SelectedValue + "' as Type" +
                        " FROM [Sheet1$]", oleDbConn);
                    oleAdapter = new OleDbDataAdapter(oleCmd);
                    DataTable dt = new DataTable();
                    //  oleAdapter.Fill(ds);

                    

                    oleAdapter.Fill(dt);

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["Particulars"].ToString() == "")
                            dr.Delete();
                    }
                    dt.AcceptChanges();
                    

                    MisBudgetData _data = new MisBudgetData();
                    UploadBudgetAndActualDto request = new UploadBudgetAndActualDto();
                   
                    request.TypeID = drpType.SelectedValue.ToLong();
                    request.FinYear = drpFinancialYear.SelectedValue;
                    if (drpSubdivision.Items.Count > 0)
                    {
                        request.data = ConvertGridViewToDataTableForAFIL(dt); 
                        request.DivID = drpSubdivision.SelectedValue.ToLong();
                    }
                    else
                    {
                        request.data = ConvertGridViewToDataTableForDefault(dt);
                        request.DivID = drpDivision.SelectedValue.ToLong();
                    }
                    long returnValue = _data.uploadBudgetOrActual(request);
                    if (returnValue > 0)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Uploaded!!", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Already Uploaded!!", "Oops!", Toastr.ToastPosition.TopCenter, true);

                    }

                }
                catch (Exception ex)
                {
                    //lblMsg.Text = ex.Message;

                    //   lblMessagesss.Text = ex.Message + "\n" + "Pankaj" + ex.InnerException + "\n" + "Pankaj" + ex.Source;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File format not correct!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
                finally
                {
                    if (oleDbConn != null)
                    {
                        oleDbConn.Close();
                    }
                    if (oleCmd != null)
                    {
                        oleCmd.Dispose();
                    }
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select file!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        DataTable ConvertGridViewToDataTableForAFIL(DataTable dt)
        {
            try
            {
                int i = 1;
               

                foreach (DataRow row in dt.Rows)
                {
                    if(i==1)
                    {
                        dt.Rows[0]["Total"] = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) + (dt.Rows[0]["May"].ToDataConvertDouble()) + (dt.Rows[0]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jul"].ToDataConvertDouble()) + (dt.Rows[0]["Aug"].ToDataConvertDouble()) + (dt.Rows[0]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[0]["Oct"].ToDataConvertDouble()) + (dt.Rows[0]["Nov"].ToDataConvertDouble()) + (dt.Rows[0]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jan"].ToDataConvertDouble()) + (dt.Rows[0]["Feb"].ToDataConvertDouble()) + (dt.Rows[0]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                    if (i == 2)
                    {
                        dt.Rows[1]["Total"] = ((dt.Rows[1]["Apr"].ToDataConvertDouble()) + (dt.Rows[1]["May"].ToDataConvertDouble()) + (dt.Rows[1]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jul"].ToDataConvertDouble()) + (dt.Rows[1]["Aug"].ToDataConvertDouble()) + (dt.Rows[1]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[1]["Oct"].ToDataConvertDouble()) + (dt.Rows[1]["Nov"].ToDataConvertDouble()) + (dt.Rows[1]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jan"].ToDataConvertDouble()) + (dt.Rows[1]["Feb"].ToDataConvertDouble()) + (dt.Rows[1]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                    if (i == 3)//Gross Profit
                    {
                        dt.Rows[2]["Apr"] = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) - (dt.Rows[1]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["May"]   = ((dt.Rows[0]["May"].ToDataConvertDouble()) - (dt.Rows[1]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jun"]   = ((dt.Rows[0]["Jun"].ToDataConvertDouble()) - (dt.Rows[1]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jul"]  = ((dt.Rows[0]["Jul"].ToDataConvertDouble()) - (dt.Rows[1]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Aug"]   = ((dt.Rows[0]["Aug"].ToDataConvertDouble()) - (dt.Rows[1]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Sep"]   = ((dt.Rows[0]["Sep"].ToDataConvertDouble()) - (dt.Rows[1]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Oct"]   = ((dt.Rows[0]["Oct"].ToDataConvertDouble()) - (dt.Rows[1]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Nov"]   = ((dt.Rows[0]["Nov"].ToDataConvertDouble()) - (dt.Rows[1]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Dec"]   = ((dt.Rows[0]["Dec"].ToDataConvertDouble()) - (dt.Rows[1]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jan"]   = ((dt.Rows[0]["Jan"].ToDataConvertDouble()) - (dt.Rows[1]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Feb"]   = ((dt.Rows[0]["Feb"].ToDataConvertDouble()) - (dt.Rows[1]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Mar"]   = ((dt.Rows[0]["Mar"].ToDataConvertDouble()) - (dt.Rows[1]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[2]["Total"] = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) + (dt.Rows[2]["May"].ToDataConvertDouble()) + (dt.Rows[2]["Jun"].ToDataConvertDouble())
                           + (dt.Rows[2]["Jul"].ToDataConvertDouble()) + (dt.Rows[2]["Aug"].ToDataConvertDouble()) + (dt.Rows[2]["Sep"].ToDataConvertDouble())
                           + (dt.Rows[2]["Oct"].ToDataConvertDouble()) + (dt.Rows[2]["Nov"].ToDataConvertDouble()) + (dt.Rows[2]["Dec"].ToDataConvertDouble())
                           + (dt.Rows[2]["Jan"].ToDataConvertDouble()) + (dt.Rows[2]["Feb"].ToDataConvertDouble()) + (dt.Rows[2]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if(i==4)
                    {
                        dt.Rows[3]["Total"] = 
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
                        dt.Rows[4]["Total"] =
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
                        dt.Rows[5]["Apr"] = ((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[4]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["May"] = ((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[4]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jun"] = ((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[4]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jul"] = ((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Aug"] = ((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Sep"] = ((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Oct"] = ((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Nov"] = ((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Dec"] = ((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jan"] = ((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Feb"] = ((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Mar"] = ((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[5]["Total"] =
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
                        dt.Rows[6]["Apr"] = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) - (dt.Rows[5]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["May"] = ((dt.Rows[2]["May"].ToDataConvertDouble()) - (dt.Rows[5]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jun"] = ((dt.Rows[2]["Jun"].ToDataConvertDouble()) - (dt.Rows[5]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jul"] = ((dt.Rows[2]["Jul"].ToDataConvertDouble()) - (dt.Rows[5]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Aug"] = ((dt.Rows[2]["Aug"].ToDataConvertDouble()) - (dt.Rows[5]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Sep"] = ((dt.Rows[2]["Sep"].ToDataConvertDouble()) - (dt.Rows[5]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Oct"] = ((dt.Rows[2]["Oct"].ToDataConvertDouble()) - (dt.Rows[5]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Nov"] = ((dt.Rows[2]["Nov"].ToDataConvertDouble()) - (dt.Rows[5]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Dec"] = ((dt.Rows[2]["Dec"].ToDataConvertDouble()) - (dt.Rows[5]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jan"] = ((dt.Rows[2]["Jan"].ToDataConvertDouble()) - (dt.Rows[5]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Feb"] = ((dt.Rows[2]["Feb"].ToDataConvertDouble()) - (dt.Rows[5]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Mar"] = ((dt.Rows[2]["Mar"].ToDataConvertDouble()) - (dt.Rows[5]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[6]["Total"] =
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
                        dt.Rows[7]["Total"] =
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
                        dt.Rows[8]["Total"] =
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
                        dt.Rows[9]["Apr"] = ((dt.Rows[7]["Apr"].ToDataConvertDouble()) + (dt.Rows[8]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["May"] = ((dt.Rows[7]["May"].ToDataConvertDouble()) + (dt.Rows[8]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Jun"] = ((dt.Rows[7]["Jun"].ToDataConvertDouble()) + (dt.Rows[8]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Jul"] = ((dt.Rows[7]["Jul"].ToDataConvertDouble()) + (dt.Rows[8]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Aug"] = ((dt.Rows[7]["Aug"].ToDataConvertDouble()) + (dt.Rows[8]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Sep"] = ((dt.Rows[7]["Sep"].ToDataConvertDouble()) + (dt.Rows[8]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Oct"] = ((dt.Rows[7]["Oct"].ToDataConvertDouble()) + (dt.Rows[8]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Nov"] = ((dt.Rows[7]["Nov"].ToDataConvertDouble()) + (dt.Rows[8]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Dec"] = ((dt.Rows[7]["Dec"].ToDataConvertDouble()) + (dt.Rows[8]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Jan"] = ((dt.Rows[7]["Jan"].ToDataConvertDouble()) + (dt.Rows[8]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Feb"] = ((dt.Rows[7]["Feb"].ToDataConvertDouble()) + (dt.Rows[8]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[9]["Mar"] = ((dt.Rows[7]["Mar"].ToDataConvertDouble()) + (dt.Rows[8]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[9]["Total"] =
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
                        dt.Rows[10]["Total"] =
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
                        dt.Rows[11]["Apr"] = ((dt.Rows[6]["Apr"].ToDataConvertDouble()) - (dt.Rows[9]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["May"] = ((dt.Rows[6]["May"].ToDataConvertDouble()) - (dt.Rows[9]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Jun"] = ((dt.Rows[6]["Jun"].ToDataConvertDouble()) - (dt.Rows[9]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Jul"] = ((dt.Rows[6]["Jul"].ToDataConvertDouble()) - (dt.Rows[9]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Aug"] = ((dt.Rows[6]["Aug"].ToDataConvertDouble()) - (dt.Rows[9]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Sep"] = ((dt.Rows[6]["Sep"].ToDataConvertDouble()) - (dt.Rows[9]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Oct"] = ((dt.Rows[6]["Oct"].ToDataConvertDouble()) - (dt.Rows[9]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Nov"] = ((dt.Rows[6]["Nov"].ToDataConvertDouble()) - (dt.Rows[9]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Dec"] = ((dt.Rows[6]["Dec"].ToDataConvertDouble()) - (dt.Rows[9]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Jan"] = ((dt.Rows[6]["Jan"].ToDataConvertDouble()) - (dt.Rows[9]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Feb"] = ((dt.Rows[6]["Feb"].ToDataConvertDouble()) - (dt.Rows[9]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[11]["Mar"] = ((dt.Rows[6]["Mar"].ToDataConvertDouble()) - (dt.Rows[9]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[11]["Total"] =
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
                        dt.Rows[12]["Total"] =
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
                        dt.Rows[13]["Total"] =
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
                        dt.Rows[14]["Total"] =
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
                        dt.Rows[15]["Total"] =
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
                        dt.Rows[16]["Total"] =
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
                        dt.Rows[17]["Total"] =
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
                        dt.Rows[18]["Apr"] = ((dt.Rows[11]["Apr"].ToDataConvertDouble()) - (dt.Rows[12]["Apr"].ToDataConvertDouble()) - (dt.Rows[14]["Apr"].ToDataConvertDouble()) - (dt.Rows[15]["Apr"].ToDataConvertDouble()) - (dt.Rows[17]["Apr"].ToDataConvertDouble()) + (dt.Rows[13]["Apr"].ToDataConvertDouble() + dt.Rows[16]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["May"] = ((dt.Rows[11]["May"].ToDataConvertDouble()) - (dt.Rows[12]["May"].ToDataConvertDouble()) - (dt.Rows[14]["May"].ToDataConvertDouble()) - (dt.Rows[15]["May"].ToDataConvertDouble()) - (dt.Rows[17]["May"].ToDataConvertDouble()) + (dt.Rows[13]["May"].ToDataConvertDouble() + dt.Rows[16]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Jun"] = ((dt.Rows[11]["Jun"].ToDataConvertDouble()) - (dt.Rows[12]["Jun"].ToDataConvertDouble()) - (dt.Rows[14]["Jun"].ToDataConvertDouble()) - (dt.Rows[15]["Jun"].ToDataConvertDouble()) - (dt.Rows[17]["Jun"].ToDataConvertDouble()) + (dt.Rows[13]["Jun"].ToDataConvertDouble() + dt.Rows[16]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Jul"] = ((dt.Rows[11]["Jul"].ToDataConvertDouble()) - (dt.Rows[12]["Jul"].ToDataConvertDouble()) - (dt.Rows[14]["Jul"].ToDataConvertDouble()) - (dt.Rows[15]["Jul"].ToDataConvertDouble()) - (dt.Rows[17]["Jul"].ToDataConvertDouble()) + (dt.Rows[13]["Jul"].ToDataConvertDouble() + dt.Rows[16]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Aug"] = ((dt.Rows[11]["Aug"].ToDataConvertDouble()) - (dt.Rows[12]["Aug"].ToDataConvertDouble()) - (dt.Rows[14]["Aug"].ToDataConvertDouble()) - (dt.Rows[15]["Aug"].ToDataConvertDouble()) - (dt.Rows[17]["Aug"].ToDataConvertDouble()) + (dt.Rows[13]["Aug"].ToDataConvertDouble() + dt.Rows[16]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Sep"] = ((dt.Rows[11]["Sep"].ToDataConvertDouble()) - (dt.Rows[12]["Sep"].ToDataConvertDouble()) - (dt.Rows[14]["Sep"].ToDataConvertDouble()) - (dt.Rows[15]["Sep"].ToDataConvertDouble()) - (dt.Rows[17]["Sep"].ToDataConvertDouble()) + (dt.Rows[13]["Sep"].ToDataConvertDouble() + dt.Rows[16]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Oct"] = ((dt.Rows[11]["Oct"].ToDataConvertDouble()) - (dt.Rows[12]["Oct"].ToDataConvertDouble()) - (dt.Rows[14]["Oct"].ToDataConvertDouble()) - (dt.Rows[15]["Oct"].ToDataConvertDouble()) - (dt.Rows[17]["Oct"].ToDataConvertDouble()) + (dt.Rows[13]["Oct"].ToDataConvertDouble() + dt.Rows[16]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Nov"] = ((dt.Rows[11]["Nov"].ToDataConvertDouble()) - (dt.Rows[12]["Nov"].ToDataConvertDouble()) - (dt.Rows[14]["Nov"].ToDataConvertDouble()) - (dt.Rows[15]["Nov"].ToDataConvertDouble()) - (dt.Rows[17]["Nov"].ToDataConvertDouble()) + (dt.Rows[13]["Nov"].ToDataConvertDouble() + dt.Rows[16]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Dec"] = ((dt.Rows[11]["Dec"].ToDataConvertDouble()) - (dt.Rows[12]["Dec"].ToDataConvertDouble()) - (dt.Rows[14]["Dec"].ToDataConvertDouble()) - (dt.Rows[15]["Dec"].ToDataConvertDouble()) - (dt.Rows[17]["Dec"].ToDataConvertDouble()) + (dt.Rows[13]["Dec"].ToDataConvertDouble() + dt.Rows[16]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Jan"] = ((dt.Rows[11]["Jan"].ToDataConvertDouble()) - (dt.Rows[12]["Jan"].ToDataConvertDouble()) - (dt.Rows[14]["Jan"].ToDataConvertDouble()) - (dt.Rows[15]["Jan"].ToDataConvertDouble()) - (dt.Rows[17]["Jan"].ToDataConvertDouble()) + (dt.Rows[13]["Jan"].ToDataConvertDouble() + dt.Rows[16]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Feb"] = ((dt.Rows[11]["Feb"].ToDataConvertDouble()) - (dt.Rows[12]["Feb"].ToDataConvertDouble()) - (dt.Rows[14]["Feb"].ToDataConvertDouble()) - (dt.Rows[15]["Feb"].ToDataConvertDouble()) - (dt.Rows[17]["Feb"].ToDataConvertDouble()) + (dt.Rows[13]["Feb"].ToDataConvertDouble() + dt.Rows[16]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[18]["Mar"] = ((dt.Rows[11]["Mar"].ToDataConvertDouble()) - (dt.Rows[12]["Mar"].ToDataConvertDouble()) - (dt.Rows[14]["Mar"].ToDataConvertDouble()) - (dt.Rows[15]["Mar"].ToDataConvertDouble()) - (dt.Rows[17]["Mar"].ToDataConvertDouble()) + (dt.Rows[13]["Mar"].ToDataConvertDouble() + dt.Rows[16]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[18]["Total"] =
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
                        dt.Rows[19]["Apr"] = ((dt.Rows[18]["Apr"].ToDataConvertDouble()) + (dt.Rows[14]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["May"] = ((dt.Rows[18]["May"].ToDataConvertDouble()) + (dt.Rows[14]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Jun"] = ((dt.Rows[18]["Jun"].ToDataConvertDouble()) + (dt.Rows[14]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Jul"] = ((dt.Rows[18]["Jul"].ToDataConvertDouble()) + (dt.Rows[14]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Aug"] = ((dt.Rows[18]["Aug"].ToDataConvertDouble()) + (dt.Rows[14]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Sep"] = ((dt.Rows[18]["Sep"].ToDataConvertDouble()) + (dt.Rows[14]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Oct"] = ((dt.Rows[18]["Oct"].ToDataConvertDouble()) + (dt.Rows[14]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Nov"] = ((dt.Rows[18]["Nov"].ToDataConvertDouble()) + (dt.Rows[14]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Dec"] = ((dt.Rows[18]["Dec"].ToDataConvertDouble()) + (dt.Rows[14]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Jan"] = ((dt.Rows[18]["Jan"].ToDataConvertDouble()) + (dt.Rows[14]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Feb"] = ((dt.Rows[18]["Feb"].ToDataConvertDouble()) + (dt.Rows[14]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[19]["Mar"] = ((dt.Rows[18]["Mar"].ToDataConvertDouble()) + (dt.Rows[14]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[19]["Total"] =
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
                        dt.Rows[20]["Apr"] = (((dt.Rows[2]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["May"]   = (((dt.Rows[2]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jun"]   = (((dt.Rows[2]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jul"]  = (((dt.Rows[2]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Aug"]   = (((dt.Rows[2]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Sep"]   = (((dt.Rows[2]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Oct"]   = (((dt.Rows[2]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Nov"]   = (((dt.Rows[2]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Dec"]   = (((dt.Rows[2]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jan"]   = (((dt.Rows[2]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Feb"]   = (((dt.Rows[2]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Mar"]   = (((dt.Rows[2]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[20]["Total"] = (((dt.Rows[2]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[20]["Apr"].ToString().ToString() == "NaN" || dt.Rows[20]["Apr"].ToString() == "-Infinity" || dt.Rows[20]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Apr"]="0";
                        }
                        if (dt.Rows[20]["May"].ToString().ToString() == "NaN" || dt.Rows[20]["May"].ToString() == "-Infinity" || dt.Rows[20]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["May"]="0";
                        }
                        if (dt.Rows[20]["Jun"].ToString() == "NaN" || dt.Rows[20]["Jun"].ToString() == "-Infinity" || dt.Rows[20]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jun"]="0";
                        }
                        if (dt.Rows[20]["Jul"].ToString() == "NaN" || dt.Rows[20]["Jul"].ToString() == "-Infinity" || dt.Rows[20]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jul"]="0";
                        }
                        if (dt.Rows[20]["Aug"].ToString() == "NaN" || dt.Rows[20]["Aug"].ToString() == "-Infinity" || dt.Rows[20]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Aug"]="0";
                        }
                        if (dt.Rows[20]["Sep"].ToString() == "NaN" || dt.Rows[20]["Sep"].ToString() == "-Infinity" || dt.Rows[20]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Sep"]="0";
                        }
                        if (dt.Rows[20]["Oct"].ToString() == "NaN" || dt.Rows[20]["Oct"].ToString() == "-Infinity" || dt.Rows[20]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Oct"]="0";
                        }
                        if (dt.Rows[20]["Nov"].ToString() == "NaN" || dt.Rows[20]["Nov"].ToString() == "-Infinity" || dt.Rows[20]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Nov"]="0";
                        }
                        if (dt.Rows[20]["Dec"].ToString() == "NaN" || dt.Rows[20]["Dec"].ToString() == "-Infinity" || dt.Rows[20]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Dec"]="0";
                        }
                        if (dt.Rows[20]["Jan"].ToString() == "NaN" || dt.Rows[20]["Jan"].ToString() == "-Infinity" || dt.Rows[20]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jan"]="0";
                        }
                        if (dt.Rows[20]["Feb"].ToString() == "NaN" || dt.Rows[20]["Feb"].ToString() == "-Infinity" || dt.Rows[20]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Feb"]="0";
                        }
                        if (dt.Rows[20]["Mar"].ToString() == "NaN" || dt.Rows[20]["Mar"].ToString() == "-Infinity" || dt.Rows[20]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Mar"] = "0";
                        }
                    }
                    if (i == 22)//EBITDA Before CC%
                    {
                        dt.Rows[21]["Apr"] = (((dt.Rows[6]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["May"] = (((dt.Rows[6]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jun"] = (((dt.Rows[6]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jul"] = (((dt.Rows[6]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Aug"] = (((dt.Rows[6]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Sep"] = (((dt.Rows[6]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Oct"] = (((dt.Rows[6]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Nov"] = (((dt.Rows[6]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Dec"] = (((dt.Rows[6]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jan"] = (((dt.Rows[6]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Feb"] = (((dt.Rows[6]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Mar"] = (((dt.Rows[6]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[21]["Total"] = (((dt.Rows[6]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[21]["Apr"].ToString().ToString() == "NaN" || dt.Rows[21]["Apr"].ToString() == "-Infinity" || dt.Rows[21]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Apr"] = "0";
                        }
                        if (dt.Rows[21]["May"].ToString().ToString() == "NaN" || dt.Rows[21]["May"].ToString() == "-Infinity" || dt.Rows[21]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["May"] = "0";
                        }
                        if (dt.Rows[21]["Jun"].ToString() == "NaN" || dt.Rows[21]["Jun"].ToString() == "-Infinity" || dt.Rows[21]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jun"] = "0";
                        }
                        if (dt.Rows[21]["Jul"].ToString() == "NaN" || dt.Rows[21]["Jul"].ToString() == "-Infinity" || dt.Rows[21]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jul"] = "0";
                        }
                        if (dt.Rows[21]["Aug"].ToString() == "NaN" || dt.Rows[21]["Aug"].ToString() == "-Infinity" || dt.Rows[21]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Aug"] = "0";
                        }
                        if (dt.Rows[21]["Sep"].ToString() == "NaN" || dt.Rows[21]["Sep"].ToString() == "-Infinity" || dt.Rows[21]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Sep"] = "0";
                        }
                        if (dt.Rows[21]["Oct"].ToString() == "NaN" || dt.Rows[21]["Oct"].ToString() == "-Infinity" || dt.Rows[21]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Oct"] = "0";
                        }
                        if (dt.Rows[21]["Nov"].ToString() == "NaN" || dt.Rows[21]["Nov"].ToString() == "-Infinity" || dt.Rows[21]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Nov"] = "0";
                        }
                        if (dt.Rows[21]["Dec"].ToString() == "NaN" || dt.Rows[21]["Dec"].ToString() == "-Infinity" || dt.Rows[21]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Dec"] = "0";
                        }
                        if (dt.Rows[21]["Jan"].ToString() == "NaN" || dt.Rows[21]["Jan"].ToString() == "-Infinity" || dt.Rows[21]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jan"] = "0";
                        }
                        if (dt.Rows[21]["Feb"].ToString() == "NaN" || dt.Rows[21]["Feb"].ToString() == "-Infinity" || dt.Rows[21]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Feb"] = "0";
                        }
                        if (dt.Rows[21]["Mar"].ToString() == "NaN" || dt.Rows[21]["Mar"].ToString() == "-Infinity" || dt.Rows[21]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Mar"] = "0";
                        }
                    }
                    if (i == 23)//EBITDA After CC%
                    {
                        dt.Rows[22]["Apr"] = (((dt.Rows[11]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["May"] = (((dt.Rows[11]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Jun"] = (((dt.Rows[11]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Jul"] = (((dt.Rows[11]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Aug"] = (((dt.Rows[11]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Sep"] = (((dt.Rows[11]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Oct"] = (((dt.Rows[11]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Nov"] = (((dt.Rows[11]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Dec"] = (((dt.Rows[11]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Jan"] = (((dt.Rows[11]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Feb"] = (((dt.Rows[11]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[22]["Mar"] = (((dt.Rows[11]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[22]["Total"] = (((dt.Rows[11]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[22]["Apr"].ToString().ToString() == "NaN" || dt.Rows[22]["Apr"].ToString() == "-Infinity" || dt.Rows[22]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Apr"] = "0";
                        }
                        if (dt.Rows[22]["May"].ToString().ToString() == "NaN" || dt.Rows[22]["May"].ToString() == "-Infinity" || dt.Rows[22]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["May"] = "0";
                        }
                        if (dt.Rows[22]["Jun"].ToString() == "NaN" || dt.Rows[22]["Jun"].ToString() == "-Infinity" || dt.Rows[22]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Jun"] = "0";
                        }
                        if (dt.Rows[22]["Jul"].ToString() == "NaN" || dt.Rows[22]["Jul"].ToString() == "-Infinity" || dt.Rows[22]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Jul"] = "0";
                        }
                        if (dt.Rows[22]["Aug"].ToString() == "NaN" || dt.Rows[22]["Aug"].ToString() == "-Infinity" || dt.Rows[22]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Aug"] = "0";
                        }
                        if (dt.Rows[22]["Sep"].ToString() == "NaN" || dt.Rows[22]["Sep"].ToString() == "-Infinity" || dt.Rows[22]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Sep"] = "0";
                        }
                        if (dt.Rows[22]["Oct"].ToString() == "NaN" || dt.Rows[22]["Oct"].ToString() == "-Infinity" || dt.Rows[22]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Oct"] = "0";
                        }
                        if (dt.Rows[22]["Nov"].ToString() == "NaN" || dt.Rows[22]["Nov"].ToString() == "-Infinity" || dt.Rows[22]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Nov"] = "0";
                        }
                        if (dt.Rows[22]["Dec"].ToString() == "NaN" || dt.Rows[22]["Dec"].ToString() == "-Infinity" || dt.Rows[22]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Dec"] = "0";
                        }
                        if (dt.Rows[22]["Jan"].ToString() == "NaN" || dt.Rows[22]["Jan"].ToString() == "-Infinity" || dt.Rows[22]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Jan"] = "0";
                        }
                        if (dt.Rows[22]["Feb"].ToString() == "NaN" || dt.Rows[22]["Feb"].ToString() == "-Infinity" || dt.Rows[22]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Feb"] = "0";
                        }
                        if (dt.Rows[22]["Mar"].ToString() == "NaN" || dt.Rows[22]["Mar"].ToString() == "-Infinity" || dt.Rows[22]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[22]["Mar"] = "0";
                        }


                    }
                    if (i == 24)//% of Salaries to Revenue
                    {
                        dt.Rows[23]["Apr"] = ((((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[7]["Apr"].ToDataConvertDouble())) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["May"] = ((((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[7]["May"].ToDataConvertDouble())) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Jun"] = ((((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[7]["Jun"].ToDataConvertDouble())) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Jul"] = ((((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[7]["Jul"].ToDataConvertDouble())) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Aug"] = ((((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[7]["Aug"].ToDataConvertDouble())) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Sep"] = ((((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[7]["Sep"].ToDataConvertDouble())) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Oct"] = ((((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[7]["Oct"].ToDataConvertDouble())) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Nov"] = ((((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[7]["Nov"].ToDataConvertDouble())) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Dec"] = ((((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[7]["Dec"].ToDataConvertDouble())) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Jan"] = ((((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[7]["Jan"].ToDataConvertDouble())) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Feb"] = ((((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[7]["Feb"].ToDataConvertDouble())) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[23]["Mar"] = ((((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[7]["Mar"].ToDataConvertDouble())) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[23]["Total"] = ((((dt.Rows[3]["Total"].ToDataConvertDouble()) + (dt.Rows[7]["Total"].ToDataConvertDouble())) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[23]["Apr"].ToString().ToString() == "NaN" || dt.Rows[23]["Apr"].ToString() == "-Infinity" || dt.Rows[23]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Apr"] = "0";
                        }
                        if (dt.Rows[23]["May"].ToString().ToString() == "NaN" || dt.Rows[23]["May"].ToString() == "-Infinity" || dt.Rows[23]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["May"] = "0";
                        }
                        if (dt.Rows[23]["Jun"].ToString() == "NaN" || dt.Rows[23]["Jun"].ToString() == "-Infinity" || dt.Rows[23]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Jun"] = "0";
                        }
                        if (dt.Rows[23]["Jul"].ToString() == "NaN" || dt.Rows[23]["Jul"].ToString() == "-Infinity" || dt.Rows[23]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Jul"] = "0";
                        }
                        if (dt.Rows[23]["Aug"].ToString() == "NaN" || dt.Rows[23]["Aug"].ToString() == "-Infinity" || dt.Rows[23]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Aug"] = "0";
                        }
                        if (dt.Rows[23]["Sep"].ToString() == "NaN" || dt.Rows[23]["Sep"].ToString() == "-Infinity" || dt.Rows[23]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Sep"] = "0";
                        }
                        if (dt.Rows[23]["Oct"].ToString() == "NaN" || dt.Rows[23]["Oct"].ToString() == "-Infinity" || dt.Rows[23]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Oct"] = "0";
                        }
                        if (dt.Rows[23]["Nov"].ToString() == "NaN" || dt.Rows[23]["Nov"].ToString() == "-Infinity" || dt.Rows[23]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Nov"] = "0";
                        }
                        if (dt.Rows[23]["Dec"].ToString() == "NaN" || dt.Rows[23]["Dec"].ToString() == "-Infinity" || dt.Rows[23]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Dec"] = "0";
                        }
                        if (dt.Rows[23]["Jan"].ToString() == "NaN" || dt.Rows[23]["Jan"].ToString() == "-Infinity" || dt.Rows[23]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Jan"] = "0";
                        }
                        if (dt.Rows[23]["Feb"].ToString() == "NaN" || dt.Rows[23]["Feb"].ToString() == "-Infinity" || dt.Rows[23]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Feb"] = "0";
                        }
                        if (dt.Rows[23]["Mar"].ToString() == "NaN" || dt.Rows[23]["Mar"].ToString() == "-Infinity" || dt.Rows[23]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[23]["Mar"] = "0";
                        }
                    }
                    if (i == 25)//% of Indirect Cost to Revenue
                    {
                        dt.Rows[24]["Apr"] = ((((dt.Rows[4]["Apr"].ToDataConvertDouble()) + (dt.Rows[8]["Apr"].ToDataConvertDouble())) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["May"] = ((((dt.Rows[4]["May"].ToDataConvertDouble()) + (dt.Rows[8]["May"].ToDataConvertDouble())) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Jun"] = ((((dt.Rows[4]["Jun"].ToDataConvertDouble()) + (dt.Rows[8]["Jun"].ToDataConvertDouble())) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Jul"] = ((((dt.Rows[4]["Jul"].ToDataConvertDouble()) + (dt.Rows[8]["Jul"].ToDataConvertDouble())) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Aug"] = ((((dt.Rows[4]["Aug"].ToDataConvertDouble()) + (dt.Rows[8]["Aug"].ToDataConvertDouble())) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Sep"] = ((((dt.Rows[4]["Sep"].ToDataConvertDouble()) + (dt.Rows[8]["Sep"].ToDataConvertDouble())) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Oct"] = ((((dt.Rows[4]["Oct"].ToDataConvertDouble()) + (dt.Rows[8]["Oct"].ToDataConvertDouble())) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Nov"] = ((((dt.Rows[4]["Nov"].ToDataConvertDouble()) + (dt.Rows[8]["Nov"].ToDataConvertDouble())) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Dec"] = ((((dt.Rows[4]["Dec"].ToDataConvertDouble()) + (dt.Rows[8]["Dec"].ToDataConvertDouble())) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Jan"] = ((((dt.Rows[4]["Jan"].ToDataConvertDouble()) + (dt.Rows[8]["Jan"].ToDataConvertDouble())) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Feb"] = ((((dt.Rows[4]["Feb"].ToDataConvertDouble()) + (dt.Rows[8]["Feb"].ToDataConvertDouble())) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[24]["Mar"] = ((((dt.Rows[4]["Mar"].ToDataConvertDouble()) + (dt.Rows[8]["Mar"].ToDataConvertDouble())) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[24]["Total"] = ((((dt.Rows[4]["Total"].ToDataConvertDouble()) + (dt.Rows[8]["Total"].ToDataConvertDouble())) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[24]["Apr"].ToString().ToString() == "NaN" || dt.Rows[24]["Apr"].ToString() == "-Infinity" || dt.Rows[24]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Apr"] = "0";
                        }
                        if (dt.Rows[24]["May"].ToString().ToString() == "NaN" || dt.Rows[24]["May"].ToString() == "-Infinity" || dt.Rows[24]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["May"] = "0";
                        }
                        if (dt.Rows[24]["Jun"].ToString() == "NaN" || dt.Rows[24]["Jun"].ToString() == "-Infinity" || dt.Rows[24]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Jun"] = "0";
                        }
                        if (dt.Rows[24]["Jul"].ToString() == "NaN" || dt.Rows[24]["Jul"].ToString() == "-Infinity" || dt.Rows[24]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Jul"] = "0";
                        }
                        if (dt.Rows[24]["Aug"].ToString() == "NaN" || dt.Rows[24]["Aug"].ToString() == "-Infinity" || dt.Rows[24]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Aug"] = "0";
                        }
                        if (dt.Rows[24]["Sep"].ToString() == "NaN" || dt.Rows[24]["Sep"].ToString() == "-Infinity" || dt.Rows[24]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Sep"] = "0";
                        }
                        if (dt.Rows[24]["Oct"].ToString() == "NaN" || dt.Rows[24]["Oct"].ToString() == "-Infinity" || dt.Rows[24]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Oct"] = "0";
                        }
                        if (dt.Rows[24]["Nov"].ToString() == "NaN" || dt.Rows[24]["Nov"].ToString() == "-Infinity" || dt.Rows[24]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Nov"] = "0";
                        }
                        if (dt.Rows[24]["Dec"].ToString() == "NaN" || dt.Rows[24]["Dec"].ToString() == "-Infinity" || dt.Rows[24]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Dec"] = "0";
                        }
                        if (dt.Rows[24]["Jan"].ToString() == "NaN" || dt.Rows[24]["Jan"].ToString() == "-Infinity" || dt.Rows[24]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Jan"] = "0";
                        }
                        if (dt.Rows[24]["Feb"].ToString() == "NaN" || dt.Rows[24]["Feb"].ToString() == "-Infinity" || dt.Rows[24]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Feb"] = "0";
                        }
                        if (dt.Rows[24]["Mar"].ToString() == "NaN" || dt.Rows[24]["Mar"].ToString() == "-Infinity" || dt.Rows[24]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[24]["Mar"] = "0";
                        }
                    }

                   
                    i++;
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        DataTable ConvertGridViewToDataTableForDefault(DataTable dt)
        {
            try
            {
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 1)
                    {
                        dt.Rows[0]["Total"] = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) + (dt.Rows[0]["May"].ToDataConvertDouble()) + (dt.Rows[0]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jul"].ToDataConvertDouble()) + (dt.Rows[0]["Aug"].ToDataConvertDouble()) + (dt.Rows[0]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[0]["Oct"].ToDataConvertDouble()) + (dt.Rows[0]["Nov"].ToDataConvertDouble()) + (dt.Rows[0]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[0]["Jan"].ToDataConvertDouble()) + (dt.Rows[0]["Feb"].ToDataConvertDouble()) + (dt.Rows[0]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                    if (i == 2)
                    {
                        dt.Rows[1]["Total"] = ((dt.Rows[1]["Apr"].ToDataConvertDouble()) + (dt.Rows[1]["May"].ToDataConvertDouble()) + (dt.Rows[1]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jul"].ToDataConvertDouble()) + (dt.Rows[1]["Aug"].ToDataConvertDouble()) + (dt.Rows[1]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[1]["Oct"].ToDataConvertDouble()) + (dt.Rows[1]["Nov"].ToDataConvertDouble()) + (dt.Rows[1]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[1]["Jan"].ToDataConvertDouble()) + (dt.Rows[1]["Feb"].ToDataConvertDouble()) + (dt.Rows[1]["Mar"].ToDataConvertDouble())
                            ).ToString();
                    }
                   
                    if (i == 3)
                    {
                        dt.Rows[2]["Apr"] = ((dt.Rows[0]["Apr"].ToDataConvertDouble()) - (dt.Rows[1]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["May"] = ((dt.Rows[0]["May"].ToDataConvertDouble()) - (dt.Rows[1]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jun"] = ((dt.Rows[0]["Jun"].ToDataConvertDouble()) - (dt.Rows[1]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jul"] = ((dt.Rows[0]["Jul"].ToDataConvertDouble()) - (dt.Rows[1]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Aug"] = ((dt.Rows[0]["Aug"].ToDataConvertDouble()) - (dt.Rows[1]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Sep"] = ((dt.Rows[0]["Sep"].ToDataConvertDouble()) - (dt.Rows[1]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Oct"] = ((dt.Rows[0]["Oct"].ToDataConvertDouble()) - (dt.Rows[1]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Nov"] = ((dt.Rows[0]["Nov"].ToDataConvertDouble()) - (dt.Rows[1]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Dec"] = ((dt.Rows[0]["Dec"].ToDataConvertDouble()) - (dt.Rows[1]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Jan"] = ((dt.Rows[0]["Jan"].ToDataConvertDouble()) - (dt.Rows[1]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Feb"] = ((dt.Rows[0]["Feb"].ToDataConvertDouble()) - (dt.Rows[1]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[2]["Mar"] = ((dt.Rows[0]["Mar"].ToDataConvertDouble()) - (dt.Rows[1]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[2]["Total"] = ((dt.Rows[0]["Total"].ToDataConvertDouble()) - (dt.Rows[1]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 4)
                    {
                        dt.Rows[3]["Total"] =
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
                        dt.Rows[4]["Total"] =
                             ((dt.Rows[4]["Apr"].ToDataConvertDouble())
                            + (dt.Rows[4]["May"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jun"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())
                            + (dt.Rows[4]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())
                            + (dt.Rows[4]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())
                           ).ToString();
                    }
                    if (i == 6)
                    {
                        dt.Rows[5]["Apr"] = ((dt.Rows[3]["Apr"].ToDataConvertDouble()) + (dt.Rows[4]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["May"] = ((dt.Rows[3]["May"].ToDataConvertDouble()) + (dt.Rows[4]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jun"] = ((dt.Rows[3]["Jun"].ToDataConvertDouble()) + (dt.Rows[4]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jul"] = ((dt.Rows[3]["Jul"].ToDataConvertDouble()) + (dt.Rows[4]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Aug"] = ((dt.Rows[3]["Aug"].ToDataConvertDouble()) + (dt.Rows[4]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Sep"] = ((dt.Rows[3]["Sep"].ToDataConvertDouble()) + (dt.Rows[4]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Oct"] = ((dt.Rows[3]["Oct"].ToDataConvertDouble()) + (dt.Rows[4]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Nov"] = ((dt.Rows[3]["Nov"].ToDataConvertDouble()) + (dt.Rows[4]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Dec"] = ((dt.Rows[3]["Dec"].ToDataConvertDouble()) + (dt.Rows[4]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Jan"] = ((dt.Rows[3]["Jan"].ToDataConvertDouble()) + (dt.Rows[4]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Feb"] = ((dt.Rows[3]["Feb"].ToDataConvertDouble()) + (dt.Rows[4]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[5]["Mar"] = ((dt.Rows[3]["Mar"].ToDataConvertDouble()) + (dt.Rows[4]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[5]["Total"] = ((dt.Rows[3]["Total"].ToDataConvertDouble()) + (dt.Rows[4]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 7)
                    {
                        dt.Rows[6]["Apr"] = ((dt.Rows[2]["Apr"].ToDataConvertDouble()) - (dt.Rows[5]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["May"] = ((dt.Rows[2]["May"].ToDataConvertDouble()) - (dt.Rows[5]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jun"] = ((dt.Rows[2]["Jun"].ToDataConvertDouble()) - (dt.Rows[5]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jul"] = ((dt.Rows[2]["Jul"].ToDataConvertDouble()) - (dt.Rows[5]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Aug"] = ((dt.Rows[2]["Aug"].ToDataConvertDouble()) - (dt.Rows[5]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Sep"] = ((dt.Rows[2]["Sep"].ToDataConvertDouble()) - (dt.Rows[5]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Oct"] = ((dt.Rows[2]["Oct"].ToDataConvertDouble()) - (dt.Rows[5]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Nov"] = ((dt.Rows[2]["Nov"].ToDataConvertDouble()) - (dt.Rows[5]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Dec"] = ((dt.Rows[2]["Dec"].ToDataConvertDouble()) - (dt.Rows[5]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Jan"] = ((dt.Rows[2]["Jan"].ToDataConvertDouble()) - (dt.Rows[5]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Feb"] = ((dt.Rows[2]["Feb"].ToDataConvertDouble()) - (dt.Rows[5]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[6]["Mar"] = ((dt.Rows[2]["Mar"].ToDataConvertDouble()) - (dt.Rows[5]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[6]["Total"] = ((dt.Rows[2]["Total"].ToDataConvertDouble()) - (dt.Rows[5]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 8)
                    {
                        dt.Rows[7]["Total"] =
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
                        dt.Rows[8]["Apr"] = ((dt.Rows[6]["Apr"].ToDataConvertDouble()) - (dt.Rows[7]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["May"] = ((dt.Rows[6]["May"].ToDataConvertDouble()) - (dt.Rows[7]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Jun"] = ((dt.Rows[6]["Jun"].ToDataConvertDouble()) - (dt.Rows[7]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Jul"] = ((dt.Rows[6]["Jul"].ToDataConvertDouble()) - (dt.Rows[7]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Aug"] = ((dt.Rows[6]["Aug"].ToDataConvertDouble()) - (dt.Rows[7]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Sep"] = ((dt.Rows[6]["Sep"].ToDataConvertDouble()) - (dt.Rows[7]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Oct"] = ((dt.Rows[6]["Oct"].ToDataConvertDouble()) - (dt.Rows[7]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Nov"] = ((dt.Rows[6]["Nov"].ToDataConvertDouble()) - (dt.Rows[7]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Dec"] = ((dt.Rows[6]["Dec"].ToDataConvertDouble()) - (dt.Rows[7]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Jan"] = ((dt.Rows[6]["Jan"].ToDataConvertDouble()) - (dt.Rows[7]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Feb"] = ((dt.Rows[6]["Feb"].ToDataConvertDouble()) - (dt.Rows[7]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[8]["Mar"] = ((dt.Rows[6]["Mar"].ToDataConvertDouble()) - (dt.Rows[7]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[8]["Total"] = ((dt.Rows[6]["Total"].ToDataConvertDouble()) - (dt.Rows[7]["Total"].ToDataConvertDouble())).ToString();
                    }
                    if (i == 10)
                    {

                        dt.Rows[9]["Total"] =
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
                        dt.Rows[10]["Total"] =
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
                        dt.Rows[11]["Total"] =
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
                        dt.Rows[12]["Total"] =
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
                        dt.Rows[13]["Total"] =
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
                        dt.Rows[14]["Total"] =
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
                        dt.Rows[15]["Apr"] = ((dt.Rows[8]["Apr"].ToDataConvertDouble()) - (dt.Rows[9]["Apr"].ToDataConvertDouble()) - (dt.Rows[11]["Apr"].ToDataConvertDouble()) - (dt.Rows[12]["Apr"].ToDataConvertDouble()) - (dt.Rows[14]["Apr"].ToDataConvertDouble()) + (dt.Rows[10]["Apr"].ToDataConvertDouble() + dt.Rows[13]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["May"] = ((dt.Rows[8]["May"].ToDataConvertDouble()) - (dt.Rows[9]["May"].ToDataConvertDouble()) - (dt.Rows[11]["May"].ToDataConvertDouble()) - (dt.Rows[12]["May"].ToDataConvertDouble()) - (dt.Rows[14]["May"].ToDataConvertDouble()) + (dt.Rows[10]["May"].ToDataConvertDouble() + dt.Rows[13]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Jun"] = ((dt.Rows[8]["Jun"].ToDataConvertDouble()) - (dt.Rows[9]["Jun"].ToDataConvertDouble()) - (dt.Rows[11]["Jun"].ToDataConvertDouble()) - (dt.Rows[12]["Jun"].ToDataConvertDouble()) - (dt.Rows[14]["Jun"].ToDataConvertDouble()) + (dt.Rows[10]["Jun"].ToDataConvertDouble() + dt.Rows[13]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Jul"] = ((dt.Rows[8]["Jul"].ToDataConvertDouble()) - (dt.Rows[9]["Jul"].ToDataConvertDouble()) - (dt.Rows[11]["Jul"].ToDataConvertDouble()) - (dt.Rows[12]["Jul"].ToDataConvertDouble()) - (dt.Rows[14]["Jul"].ToDataConvertDouble()) + (dt.Rows[10]["Jul"].ToDataConvertDouble() + dt.Rows[13]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Aug"] = ((dt.Rows[8]["Aug"].ToDataConvertDouble()) - (dt.Rows[9]["Aug"].ToDataConvertDouble()) - (dt.Rows[11]["Aug"].ToDataConvertDouble()) - (dt.Rows[12]["Aug"].ToDataConvertDouble()) - (dt.Rows[14]["Aug"].ToDataConvertDouble()) + (dt.Rows[10]["Aug"].ToDataConvertDouble() + dt.Rows[13]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Sep"] = ((dt.Rows[8]["Sep"].ToDataConvertDouble()) - (dt.Rows[9]["Sep"].ToDataConvertDouble()) - (dt.Rows[11]["Sep"].ToDataConvertDouble()) - (dt.Rows[12]["Sep"].ToDataConvertDouble()) - (dt.Rows[14]["Sep"].ToDataConvertDouble()) + (dt.Rows[10]["Sep"].ToDataConvertDouble() + dt.Rows[13]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Oct"] = ((dt.Rows[8]["Oct"].ToDataConvertDouble()) - (dt.Rows[9]["Oct"].ToDataConvertDouble()) - (dt.Rows[11]["Oct"].ToDataConvertDouble()) - (dt.Rows[12]["Oct"].ToDataConvertDouble()) - (dt.Rows[14]["Oct"].ToDataConvertDouble()) + (dt.Rows[10]["Oct"].ToDataConvertDouble() + dt.Rows[13]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Nov"] = ((dt.Rows[8]["Nov"].ToDataConvertDouble()) - (dt.Rows[9]["Nov"].ToDataConvertDouble()) - (dt.Rows[11]["Nov"].ToDataConvertDouble()) - (dt.Rows[12]["Nov"].ToDataConvertDouble()) - (dt.Rows[14]["Nov"].ToDataConvertDouble()) + (dt.Rows[10]["Nov"].ToDataConvertDouble() + dt.Rows[13]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Dec"] = ((dt.Rows[8]["Dec"].ToDataConvertDouble()) - (dt.Rows[9]["Dec"].ToDataConvertDouble()) - (dt.Rows[11]["Dec"].ToDataConvertDouble()) - (dt.Rows[12]["Dec"].ToDataConvertDouble()) - (dt.Rows[14]["Dec"].ToDataConvertDouble()) + (dt.Rows[10]["Dec"].ToDataConvertDouble() + dt.Rows[13]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Jan"] = ((dt.Rows[8]["Jan"].ToDataConvertDouble()) - (dt.Rows[9]["Jan"].ToDataConvertDouble()) - (dt.Rows[11]["Jan"].ToDataConvertDouble()) - (dt.Rows[12]["Jan"].ToDataConvertDouble()) - (dt.Rows[14]["Jan"].ToDataConvertDouble()) + (dt.Rows[10]["Jan"].ToDataConvertDouble() + dt.Rows[13]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Feb"] = ((dt.Rows[8]["Feb"].ToDataConvertDouble()) - (dt.Rows[9]["Feb"].ToDataConvertDouble()) - (dt.Rows[11]["Feb"].ToDataConvertDouble()) - (dt.Rows[12]["Feb"].ToDataConvertDouble()) - (dt.Rows[14]["Feb"].ToDataConvertDouble()) + (dt.Rows[10]["Feb"].ToDataConvertDouble() + dt.Rows[13]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[15]["Mar"] = ((dt.Rows[8]["Mar"].ToDataConvertDouble()) - (dt.Rows[9]["Mar"].ToDataConvertDouble()) - (dt.Rows[11]["Mar"].ToDataConvertDouble()) - (dt.Rows[12]["Mar"].ToDataConvertDouble()) - (dt.Rows[14]["Mar"].ToDataConvertDouble()) + (dt.Rows[10]["Mar"].ToDataConvertDouble() + dt.Rows[13]["Mar"].ToDataConvertDouble())).ToString();


                        dt.Rows[15]["Total"] =
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
                        dt.Rows[16]["Apr"] = ((dt.Rows[15]["Apr"].ToDataConvertDouble()) + (dt.Rows[11]["Apr"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["May"] = ((dt.Rows[15]["May"].ToDataConvertDouble()) + (dt.Rows[11]["May"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Jun"] = ((dt.Rows[15]["Jun"].ToDataConvertDouble()) + (dt.Rows[11]["Jun"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Jul"] = ((dt.Rows[15]["Jul"].ToDataConvertDouble()) + (dt.Rows[11]["Jul"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Aug"] = ((dt.Rows[15]["Aug"].ToDataConvertDouble()) + (dt.Rows[11]["Aug"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Sep"] = ((dt.Rows[15]["Sep"].ToDataConvertDouble()) + (dt.Rows[11]["Sep"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Oct"] = ((dt.Rows[15]["Oct"].ToDataConvertDouble()) + (dt.Rows[11]["Oct"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Nov"] = ((dt.Rows[15]["Nov"].ToDataConvertDouble()) + (dt.Rows[11]["Nov"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Dec"] = ((dt.Rows[15]["Dec"].ToDataConvertDouble()) + (dt.Rows[11]["Dec"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Jan"] = ((dt.Rows[15]["Jan"].ToDataConvertDouble()) + (dt.Rows[11]["Jan"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Feb"] = ((dt.Rows[15]["Feb"].ToDataConvertDouble()) + (dt.Rows[11]["Feb"].ToDataConvertDouble())).ToString();
                        dt.Rows[16]["Mar"] = ((dt.Rows[15]["Mar"].ToDataConvertDouble()) + (dt.Rows[11]["Mar"].ToDataConvertDouble())).ToString();

                        dt.Rows[16]["Total"] =
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

                    if (i == 18)//Gross Profit%
                    {
                        dt.Rows[17]["Apr"] = (((dt.Rows[2]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["May"] = (((dt.Rows[2]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Jun"] = (((dt.Rows[2]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Jul"] = (((dt.Rows[2]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Aug"] = (((dt.Rows[2]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Sep"] = (((dt.Rows[2]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Oct"] = (((dt.Rows[2]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Nov"] = (((dt.Rows[2]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Dec"] = (((dt.Rows[2]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Jan"] = (((dt.Rows[2]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Feb"] = (((dt.Rows[2]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[17]["Mar"] = (((dt.Rows[2]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[17]["Total"] = (((dt.Rows[2]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[17]["Apr"].ToString().ToString() == "NaN" || dt.Rows[17]["Apr"].ToString() == "-Infinity" || dt.Rows[17]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Apr"] = "0";
                        }
                        if (dt.Rows[17]["May"].ToString().ToString() == "NaN" || dt.Rows[17]["May"].ToString() == "-Infinity" || dt.Rows[17]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["May"] = "0";
                        }
                        if (dt.Rows[17]["Jun"].ToString() == "NaN" || dt.Rows[17]["Jun"].ToString() == "-Infinity" || dt.Rows[17]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Jun"] = "0";
                        }
                        if (dt.Rows[17]["Jul"].ToString() == "NaN" || dt.Rows[17]["Jul"].ToString() == "-Infinity" || dt.Rows[17]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Jul"] = "0";
                        }
                        if (dt.Rows[17]["Aug"].ToString() == "NaN" || dt.Rows[17]["Aug"].ToString() == "-Infinity" || dt.Rows[17]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Aug"] = "0";
                        }
                        if (dt.Rows[17]["Sep"].ToString() == "NaN" || dt.Rows[17]["Sep"].ToString() == "-Infinity" || dt.Rows[17]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Sep"] = "0";
                        }
                        if (dt.Rows[17]["Oct"].ToString() == "NaN" || dt.Rows[17]["Oct"].ToString() == "-Infinity" || dt.Rows[17]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Oct"] = "0";
                        }
                        if (dt.Rows[17]["Nov"].ToString() == "NaN" || dt.Rows[17]["Nov"].ToString() == "-Infinity" || dt.Rows[17]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Nov"] = "0";
                        }
                        if (dt.Rows[17]["Dec"].ToString() == "NaN" || dt.Rows[17]["Dec"].ToString() == "-Infinity" || dt.Rows[17]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Dec"] = "0";
                        }
                        if (dt.Rows[17]["Jan"].ToString() == "NaN" || dt.Rows[17]["Jan"].ToString() == "-Infinity" || dt.Rows[17]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Jan"] = "0";
                        }
                        if (dt.Rows[17]["Feb"].ToString() == "NaN" || dt.Rows[17]["Feb"].ToString() == "-Infinity" || dt.Rows[17]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Feb"] = "0";
                        }
                        if (dt.Rows[17]["Mar"].ToString() == "NaN" || dt.Rows[17]["Mar"].ToString() == "-Infinity" || dt.Rows[17]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[17]["Mar"] = "0";
                        }
                    }
                    if (i == 19)//EBITDA Before CC%
                    {
                        dt.Rows[18]["Apr"] = (((dt.Rows[6]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["May"] = (((dt.Rows[6]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Jun"] = (((dt.Rows[6]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Jul"] = (((dt.Rows[6]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Aug"] = (((dt.Rows[6]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Sep"] = (((dt.Rows[6]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Oct"] = (((dt.Rows[6]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Nov"] = (((dt.Rows[6]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Dec"] = (((dt.Rows[6]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Jan"] = (((dt.Rows[6]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Feb"] = (((dt.Rows[6]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[18]["Mar"] = (((dt.Rows[6]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[18]["Total"] = (((dt.Rows[6]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[18]["Apr"].ToString().ToString() == "NaN" || dt.Rows[18]["Apr"].ToString() == "-Infinity" || dt.Rows[18]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Apr"] = "0";
                        }
                        if (dt.Rows[18]["May"].ToString().ToString() == "NaN" || dt.Rows[18]["May"].ToString() == "-Infinity" || dt.Rows[18]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["May"] = "0";
                        }
                        if (dt.Rows[18]["Jun"].ToString() == "NaN" || dt.Rows[18]["Jun"].ToString() == "-Infinity" || dt.Rows[18]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Jun"] = "0";
                        }
                        if (dt.Rows[18]["Jul"].ToString() == "NaN" || dt.Rows[18]["Jul"].ToString() == "-Infinity" || dt.Rows[18]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Jul"] = "0";
                        }
                        if (dt.Rows[18]["Aug"].ToString() == "NaN" || dt.Rows[18]["Aug"].ToString() == "-Infinity" || dt.Rows[18]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Aug"] = "0";
                        }
                        if (dt.Rows[18]["Sep"].ToString() == "NaN" || dt.Rows[18]["Sep"].ToString() == "-Infinity" || dt.Rows[18]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Sep"] = "0";
                        }
                        if (dt.Rows[18]["Oct"].ToString() == "NaN" || dt.Rows[18]["Oct"].ToString() == "-Infinity" || dt.Rows[18]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Oct"] = "0";
                        }
                        if (dt.Rows[18]["Nov"].ToString() == "NaN" || dt.Rows[18]["Nov"].ToString() == "-Infinity" || dt.Rows[18]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Nov"] = "0";
                        }
                        if (dt.Rows[18]["Dec"].ToString() == "NaN" || dt.Rows[18]["Dec"].ToString() == "-Infinity" || dt.Rows[18]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Dec"] = "0";
                        }
                        if (dt.Rows[18]["Jan"].ToString() == "NaN" || dt.Rows[18]["Jan"].ToString() == "-Infinity" || dt.Rows[18]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Jan"] = "0";
                        }
                        if (dt.Rows[18]["Feb"].ToString() == "NaN" || dt.Rows[18]["Feb"].ToString() == "-Infinity" || dt.Rows[18]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Feb"] = "0";
                        }
                        if (dt.Rows[18]["Mar"].ToString() == "NaN" || dt.Rows[18]["Mar"].ToString() == "-Infinity" || dt.Rows[18]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[18]["Mar"] = "0";
                        }
                    }
                    if (i == 20)//EBITDA After CC%
                    {
                        dt.Rows[19]["Apr"] = (((dt.Rows[8]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["May"] = (((dt.Rows[8]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Jun"] = (((dt.Rows[8]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Jul"] = (((dt.Rows[8]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Aug"] = (((dt.Rows[8]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Sep"] = (((dt.Rows[8]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Oct"] = (((dt.Rows[8]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Nov"] = (((dt.Rows[8]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Dec"] = (((dt.Rows[8]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Jan"] = (((dt.Rows[8]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Feb"] = (((dt.Rows[8]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[19]["Mar"] = (((dt.Rows[8]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[19]["Total"] = (((dt.Rows[8]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[19]["Apr"].ToString().ToString() == "NaN" || dt.Rows[19]["Apr"].ToString() == "-Infinity" || dt.Rows[19]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Apr"] = "0";
                        }
                        if (dt.Rows[19]["May"].ToString().ToString() == "NaN" || dt.Rows[19]["May"].ToString() == "-Infinity" || dt.Rows[19]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["May"] = "0";
                        }
                        if (dt.Rows[19]["Jun"].ToString() == "NaN" || dt.Rows[19]["Jun"].ToString() == "-Infinity" || dt.Rows[19]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Jun"] = "0";
                        }
                        if (dt.Rows[19]["Jul"].ToString() == "NaN" || dt.Rows[19]["Jul"].ToString() == "-Infinity" || dt.Rows[19]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Jul"] = "0";
                        }
                        if (dt.Rows[19]["Aug"].ToString() == "NaN" || dt.Rows[19]["Aug"].ToString() == "-Infinity" || dt.Rows[19]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Aug"] = "0";
                        }
                        if (dt.Rows[19]["Sep"].ToString() == "NaN" || dt.Rows[19]["Sep"].ToString() == "-Infinity" || dt.Rows[19]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Sep"] = "0";
                        }
                        if (dt.Rows[19]["Oct"].ToString() == "NaN" || dt.Rows[19]["Oct"].ToString() == "-Infinity" || dt.Rows[19]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Oct"] = "0";
                        }
                        if (dt.Rows[19]["Nov"].ToString() == "NaN" || dt.Rows[19]["Nov"].ToString() == "-Infinity" || dt.Rows[19]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Nov"] = "0";
                        }
                        if (dt.Rows[19]["Dec"].ToString() == "NaN" || dt.Rows[19]["Dec"].ToString() == "-Infinity" || dt.Rows[19]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Dec"] = "0";
                        }
                        if (dt.Rows[19]["Jan"].ToString() == "NaN" || dt.Rows[19]["Jan"].ToString() == "-Infinity" || dt.Rows[19]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Jan"] = "0";
                        }
                        if (dt.Rows[19]["Feb"].ToString() == "NaN" || dt.Rows[19]["Feb"].ToString() == "-Infinity" || dt.Rows[19]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Feb"] = "0";
                        }
                        if (dt.Rows[19]["Mar"].ToString() == "NaN" || dt.Rows[19]["Mar"].ToString() == "-Infinity" || dt.Rows[19]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[19]["Mar"] = "0";
                        }
                    }
                    if (i == 21)//% of Salaries to Revenue
                    {
                        dt.Rows[20]["Apr"] = (((dt.Rows[3]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["May"] = (((dt.Rows[3]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jun"] = (((dt.Rows[3]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jul"] = (((dt.Rows[3]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Aug"] = (((dt.Rows[3]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Sep"] = (((dt.Rows[3]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Oct"] = (((dt.Rows[3]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Nov"] = (((dt.Rows[3]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Dec"] = (((dt.Rows[3]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Jan"] = (((dt.Rows[3]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Feb"] = (((dt.Rows[3]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[20]["Mar"] = (((dt.Rows[3]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[20]["Total"] = (((dt.Rows[3]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[20]["Apr"].ToString().ToString() == "NaN" || dt.Rows[20]["Apr"].ToString() == "-Infinity" || dt.Rows[20]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Apr"] = "0";
                        }
                        if (dt.Rows[20]["May"].ToString().ToString() == "NaN" || dt.Rows[20]["May"].ToString() == "-Infinity" || dt.Rows[20]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["May"] = "0";
                        }
                        if (dt.Rows[20]["Jun"].ToString() == "NaN" || dt.Rows[20]["Jun"].ToString() == "-Infinity" || dt.Rows[20]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jun"] = "0";
                        }
                        if (dt.Rows[20]["Jul"].ToString() == "NaN" || dt.Rows[20]["Jul"].ToString() == "-Infinity" || dt.Rows[20]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jul"] = "0";
                        }
                        if (dt.Rows[20]["Aug"].ToString() == "NaN" || dt.Rows[20]["Aug"].ToString() == "-Infinity" || dt.Rows[20]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Aug"] = "0";
                        }
                        if (dt.Rows[20]["Sep"].ToString() == "NaN" || dt.Rows[20]["Sep"].ToString() == "-Infinity" || dt.Rows[20]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Sep"] = "0";
                        }
                        if (dt.Rows[20]["Oct"].ToString() == "NaN" || dt.Rows[20]["Oct"].ToString() == "-Infinity" || dt.Rows[20]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Oct"] = "0";
                        }
                        if (dt.Rows[20]["Nov"].ToString() == "NaN" || dt.Rows[20]["Nov"].ToString() == "-Infinity" || dt.Rows[20]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Nov"] = "0";
                        }
                        if (dt.Rows[20]["Dec"].ToString() == "NaN" || dt.Rows[20]["Dec"].ToString() == "-Infinity" || dt.Rows[20]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Dec"] = "0";
                        }
                        if (dt.Rows[20]["Jan"].ToString() == "NaN" || dt.Rows[20]["Jan"].ToString() == "-Infinity" || dt.Rows[20]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Jan"] = "0";
                        }
                        if (dt.Rows[20]["Feb"].ToString() == "NaN" || dt.Rows[20]["Feb"].ToString() == "-Infinity" || dt.Rows[20]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Feb"] = "0";
                        }
                        if (dt.Rows[20]["Mar"].ToString() == "NaN" || dt.Rows[20]["Mar"].ToString() == "-Infinity" || dt.Rows[20]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[20]["Mar"] = "0";
                        }
                    }
                    if (i == 22)//% of Indirect Cost to Revenue
                    {
                        dt.Rows[21]["Apr"] = (((dt.Rows[4]["Apr"].ToDataConvertDouble()) / (dt.Rows[0]["Apr"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["May"] = (((dt.Rows[4]["May"].ToDataConvertDouble()) / (dt.Rows[0]["May"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jun"] = (((dt.Rows[4]["Jun"].ToDataConvertDouble()) / (dt.Rows[0]["Jun"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jul"] = (((dt.Rows[4]["Jul"].ToDataConvertDouble()) / (dt.Rows[0]["Jul"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Aug"] = (((dt.Rows[4]["Aug"].ToDataConvertDouble()) / (dt.Rows[0]["Aug"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Sep"] = (((dt.Rows[4]["Sep"].ToDataConvertDouble()) / (dt.Rows[0]["Sep"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Oct"] = (((dt.Rows[4]["Oct"].ToDataConvertDouble()) / (dt.Rows[0]["Oct"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Nov"] = (((dt.Rows[4]["Nov"].ToDataConvertDouble()) / (dt.Rows[0]["Nov"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Dec"] = (((dt.Rows[4]["Dec"].ToDataConvertDouble()) / (dt.Rows[0]["Dec"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Jan"] = (((dt.Rows[4]["Jan"].ToDataConvertDouble()) / (dt.Rows[0]["Jan"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Feb"] = (((dt.Rows[4]["Feb"].ToDataConvertDouble()) / (dt.Rows[0]["Feb"].ToDataConvertDouble())) * 100).ToString();
                        dt.Rows[21]["Mar"] = (((dt.Rows[4]["Mar"].ToDataConvertDouble()) / (dt.Rows[0]["Mar"].ToDataConvertDouble())) * 100).ToString();

                        dt.Rows[21]["Total"] = (((dt.Rows[4]["Total"].ToDataConvertDouble()) / (dt.Rows[0]["Total"].ToDataConvertDouble())) * 100).ToString();

                        if (dt.Rows[21]["Apr"].ToString().ToString() == "NaN" || dt.Rows[21]["Apr"].ToString() == "-Infinity" || dt.Rows[21]["Apr"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Apr"] = "0";
                        }
                        if (dt.Rows[21]["May"].ToString().ToString() == "NaN" || dt.Rows[21]["May"].ToString() == "-Infinity" || dt.Rows[21]["May"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["May"] = "0";
                        }
                        if (dt.Rows[21]["Jun"].ToString() == "NaN" || dt.Rows[21]["Jun"].ToString() == "-Infinity" || dt.Rows[21]["Jun"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jun"] = "0";
                        }
                        if (dt.Rows[21]["Jul"].ToString() == "NaN" || dt.Rows[21]["Jul"].ToString() == "-Infinity" || dt.Rows[21]["Jul"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jul"] = "0";
                        }
                        if (dt.Rows[21]["Aug"].ToString() == "NaN" || dt.Rows[21]["Aug"].ToString() == "-Infinity" || dt.Rows[21]["Aug"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Aug"] = "0";
                        }
                        if (dt.Rows[21]["Sep"].ToString() == "NaN" || dt.Rows[21]["Sep"].ToString() == "-Infinity" || dt.Rows[21]["Sep"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Sep"] = "0";
                        }
                        if (dt.Rows[21]["Oct"].ToString() == "NaN" || dt.Rows[21]["Oct"].ToString() == "-Infinity" || dt.Rows[21]["Oct"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Oct"] = "0";
                        }
                        if (dt.Rows[21]["Nov"].ToString() == "NaN" || dt.Rows[21]["Nov"].ToString() == "-Infinity" || dt.Rows[21]["Nov"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Nov"] = "0";
                        }
                        if (dt.Rows[21]["Dec"].ToString() == "NaN" || dt.Rows[21]["Dec"].ToString() == "-Infinity" || dt.Rows[21]["Dec"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Dec"] = "0";
                        }
                        if (dt.Rows[21]["Jan"].ToString() == "NaN" || dt.Rows[21]["Jan"].ToString() == "-Infinity" || dt.Rows[21]["Jan"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Jan"] = "0";
                        }
                        if (dt.Rows[21]["Feb"].ToString() == "NaN" || dt.Rows[21]["Feb"].ToString() == "-Infinity" || dt.Rows[21]["Feb"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Feb"] = "0";
                        }
                        if (dt.Rows[21]["Mar"].ToString() == "NaN" || dt.Rows[21]["Mar"].ToString() == "-Infinity" || dt.Rows[21]["Mar"].ToString() == "Infinity")
                        {
                            dt.Rows[21]["Mar"] = "0";
                        }
                    }

                    
                    i++;
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            if (!Searchvalidation())
            {
                return;
            }
            budgetAndActualUpload();
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

 
    }
}