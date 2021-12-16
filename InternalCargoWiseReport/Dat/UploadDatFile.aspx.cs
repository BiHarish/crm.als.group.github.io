using ICWR.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Renci.SshNet;
using InternalCargoWiseReport.Data.Internal;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace InternalCargoWiseReport.Dat
{
    public partial class UploadDatFile : System.Web.UI.Page
    {
        string Header1Static = "METADATA|TimeRecordGroup|GroupType|ResourceType|PersonNumber|TcStartTime|TcStopTime|AssignmentNumber";
        string Header2Static = "METADATA|TimeRecord|GroupType|ResourceType|PersonNumber|TcStartTime|TcStopTime|OrderEntered|StartTime|StopTime|TmRecType|AssignmentNumber|Measure|UnitOfMeasure";
        string Header3Static = "METADATA|TimeRecordGroupAttribute|GroupType|ResourceType|PersonNumber|TcStartTime|TcStopTime|AttributeName|AttributeDataType|AttributeLongValue|AttributeStringValue|AttributeBigDecimalValue|AttributeDateValue|AttributeTimestampValue";
        string Header4Static = "METADATA|TimeRepositoryAttribute|GroupType|ResourceType|PersonNumber|TcStartTime|TcStopTime|OrderEntered|AttributeName|AttributeDataType|AttributeLongValue|AttributeStringValue|AttributeBigDecimalValue|AttributeDateValue|AttributeTimestampValue";
        string fileName;
        string MainString;
        string TempFolderRoot;
        string sSavePathForDatFile;
        string sSavePathForZipFile;
        string sSavePath;
        OleDbConnection oleDbConn = null;
        OleDbCommand oleCmd = null;
        OleDbDataAdapter oleAdapter = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterOnSubmitStatement(GetType(), "ServerForm", "if(this.submitted) return false; this.submitted = true;");

        }

        #region Method

        void convertExcelToDatFile()
        {
            String path = string.Empty;

            if (FileUpload1.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (fileExtension != ".xlsx")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Please select excel file(.xlsx)!!')", true);
                    return;
                }
            }
            path = Server.MapPath(FileUpload1.FileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            if (FileUpload1.HasFile)
            {
                try
                {
                    sSavePath = "~/Dat/datFile/";
                    sSavePath = HttpContext.Current.Server.MapPath(sSavePath);

                    fileName = FileUpload1.FileName;
                    sSavePath = sSavePath + fileName;
                    FileUpload1.SaveAs(sSavePath);


                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", sSavePath);

                    oleDbConn = new OleDbConnection(connectionString);
                    oleDbConn.Open();

                    oleCmd = new OleDbCommand("SELECT [Sno],[PersonNumber],[TcStartTime],[TcStopTime],[StartTime],[StopTime],[Measure],[AssignmentNumber]" +
                        " FROM [Sheet1$]", oleDbConn);
                    oleAdapter = new OleDbDataAdapter(oleCmd);
                    DataTable Maindt = new DataTable();
                    //  oleAdapter.Fill(ds);
                    oleAdapter.Fill(Maindt);

                    for (int i = Maindt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Maindt.Rows[i][1] == DBNull.Value)
                            Maindt.Rows[i].Delete();
                    }
                    Maindt.AcceptChanges();

                    Maindt.DefaultView.Sort = "PersonNumber,TcStartTime,TcStopTime asc";
                    Maindt = Maindt.DefaultView.ToTable();
                    if (!validation(Maindt))
                    {
                        return;
                    }
                    //Step 1
                    DataTable fdt = new DataTable();
                    fdt.TableName = "CopyTable";
                    fdt = Maindt.Copy();
                    fdt.Columns.Remove("Sno");
                    fdt.Columns.Remove("StartTime");
                    fdt.Columns.Remove("StopTime");

                    DataView view = new DataView(fdt);
                    fdt = view.ToTable(true, "PersonNumber", "TcStartTime", "TcStopTime", "AssignmentNumber");

                    MainString = Header1Static + "\n";
                    for (int i = 0; i < fdt.Rows.Count; i++)
                    {
                        MainString = MainString + "MERGE|TimeRecordGroup|RPTD_TIME|PERSON|" + fdt.Rows[i]["PersonNumber"].ToString()
                            + "|" + fdt.Rows[i]["TcStartTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            + "|" + fdt.Rows[i]["TcStopTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            + "|" + fdt.Rows[i]["AssignmentNumber"].ToString() + "\n";
                    }

                    //Step 2

                    MainString = MainString + Header2Static + "\n";
                    int OrderedCount = 0;
                    string PersonNumber = string.Empty;
                    string StartTime = string.Empty;
                    string StopTime = string.Empty;

                    for (int i = 0; i < Maindt.Rows.Count; i++)
                    {

                        DateTime dt = Maindt.Rows[i]["StopTime"].ToDataConvertDateTime();
                        DateTime dt2 = Maindt.Rows[i]["StartTime"].ToDataConvertDateTime();
                        //TimeSpan ts = (dt2 - dt);
                        //string Measure = ts.ToString(@"hh\:mm\:ss");
                        var Measure = (dt - dt2).Hours;

                        if (PersonNumber == Maindt.Rows[i]["PersonNumber"].ToString() && StartTime == Maindt.Rows[i]["TcStartTime"].ToString() &&
                            StopTime == Maindt.Rows[i]["TcStopTime"].ToString())
                        {
                            OrderedCount = OrderedCount + 1;
                        }
                        else
                        {
                            OrderedCount = 1;
                        }
                        if (OrderedCount > 7)
                        {
                            string script = "alert(\"Duplicate Record found For Person Number: " + Maindt.Rows[i]["PersonNumber"].ToString() + " with this Date " + Maindt.Rows[i]["TcStartTime"].ToString() + "!\");";
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                            return;
                        }


                        MainString = MainString + "MERGE|TimeRecord|RPTD_TIME|PERSON|" + Maindt.Rows[i]["PersonNumber"].ToString() + "|"
                        + Maindt.Rows[i]["TcStartTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) + "|"
                        + Maindt.Rows[i]["TcStopTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) + "|" + OrderedCount.ToString()
                        + "|" + Maindt.Rows[i]["StartTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                        + "|" + Maindt.Rows[i]["StopTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                        + "|RANGE" + "|" + Maindt.Rows[i]["AssignmentNumber"].ToString() + "|" + Maindt.Rows[i]["Measure"].ToString() + "|HR" + "\n";

                        //For Ordered Count
                        PersonNumber = Maindt.Rows[i]["PersonNumber"].ToString();
                        StartTime = Maindt.Rows[i]["TcStartTime"].ToString();
                        StopTime = Maindt.Rows[i]["TcStopTime"].ToString();
                        //End
                    }


                    //step 3
                    MainString = MainString + Header3Static + "\n";
                    for (int i = 0; i < fdt.Rows.Count; i++)
                    {
                        MainString = MainString + "MERGE|TimeRecordGroupAttribute|RPTD_TIME|PERSON|" + fdt.Rows[i]["PersonNumber"].ToString()
                            + "|" + fdt.Rows[i]["TcStartTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            + "|" + fdt.Rows[i]["TcStopTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            + "|Comment|String|||||" + "\n";
                    }

                    //step 4
                    MainString = MainString + Header4Static + "\n";
                    OrderedCount = 0;
                    PersonNumber = string.Empty;
                    StartTime = string.Empty;
                    StopTime = string.Empty;
                    for (int i = 0; i < Maindt.Rows.Count; i++)
                    {
                        if (PersonNumber == Maindt.Rows[i]["PersonNumber"].ToString() && StartTime == Maindt.Rows[i]["TcStartTime"].ToString() &&
                           StopTime == Maindt.Rows[i]["TcStopTime"].ToString())
                        {
                            OrderedCount = OrderedCount + 1;
                        }
                        else
                        {
                            OrderedCount = 1;
                        }

                        MainString = MainString + "MERGE|TimeRepositoryAttribute|RPTD_TIME|PERSON|" + Maindt.Rows[i]["PersonNumber"].ToString() + "|"
                            + Maindt.Rows[i]["TcStartTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            + "|" + Maindt.Rows[i]["TcStopTime"].ToDataConvertDateTime().ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) + "|"
                            + OrderedCount.ToString() + "|PayrollTimeType|STRING||Regular|||" + "\n";


                        //For Ordered Count
                        PersonNumber = Maindt.Rows[i]["PersonNumber"].ToString();
                        StartTime = Maindt.Rows[i]["TcStartTime"].ToString();
                        StopTime = Maindt.Rows[i]["TcStopTime"].ToString();
                        //End
                    }

                    //save file into folder

                    string sSavePath1 = "~/Dat/datFile/";
                    TempFolderRoot = HttpContext.Current.Server.MapPath("~/Dat/datFile/tempFolder/");
                    if (!Directory.Exists(TempFolderRoot))
                    {
                        Directory.CreateDirectory(TempFolderRoot);
                    }
                    sSavePathForDatFile = TempFolderRoot;
                    sSavePathForZipFile = HttpContext.Current.Server.MapPath(sSavePath1);
                    string datfileName = "TimeRecordGroup.dat";
                    string datFileZipName = "TimeRecordGroup.zip";
                    sSavePathForDatFile = sSavePathForDatFile + datfileName;
                    sSavePathForZipFile = sSavePathForZipFile + datFileZipName;


                    System.IO.File.WriteAllText(sSavePathForDatFile, MainString);
                    using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(sSavePathForDatFile))
                    {
                        file1.WriteLine(MainString);

                        file1.Flush();
                        file1.Close();
                        file1.Close();
                    }

                    //Convert txt file into Zip
                    try
                    {
                        ZipFile.CreateFromDirectory(TempFolderRoot, sSavePathForZipFile);
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Conversion zip file issue!!')", true);
                        return;
                    }


                    //ENd

                    //Send File To SFTP Server

                    int Result = SendFileToServer.Send(sSavePathForZipFile);
                    if (Result == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('success','Successfully Updated!!')", true);

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','SFTP Error,Please upload after some time!!')", true);
                    }

                    //End
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','" + ex.Message + "!!')", true);

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
                    //Delete DAT file and zip File From Folder
                    FileInfo datFileInfo = new FileInfo(sSavePathForDatFile);
                    FileInfo zipFileInfo = new FileInfo(sSavePathForZipFile);

                    if (datFileInfo.Exists)
                    {
                        datFileInfo.Delete();
                    }
                    if (zipFileInfo.Exists)
                    {
                        zipFileInfo.Delete();
                    }
                    if (Directory.Exists(TempFolderRoot))
                    {
                        Directory.Delete(TempFolderRoot);
                    }
                    FileInfo finfo = new FileInfo(sSavePath);
                    if (finfo.Exists)
                    {
                        finfo.Delete();
                    }
                    //End
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Please select File!!')", true);
            }
        }

        bool validation(DataTable vdt)
        {
            string FirstDayName = string.Empty;
            string LastDayName = string.Empty;
            DateTime dt;
            DateTime DayStartDate;
            DateTime DayEndDate;
            DateTime WeekStartDate;
            DateTime WeekEndDate;
            int days = 0;

            for (int i = 0; i < vdt.Rows.Count; i++)
            {
                if (vdt.Rows[i]["PersonNumber"].ToString() == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Person Number Can Not be Empty!!')", true);
                    return false;
                }
                else if (vdt.Rows[i]["TcStartTime"].ToString() == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Tc Start Time Can not be Empty!!')", true);
                    return false;
                }
                if (vdt.Rows[i]["TcStartTime"].ToString() != string.Empty)
                {
                    if (!DateTime.TryParse(vdt.Rows[i]["TcStartTime"].ToString(), out dt))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Tc Start time should be in Date Format!!')", true);
                        return false;
                    }

                }
                if (vdt.Rows[i]["TcStopTime"].ToString() == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Tc Stop Time Can not Empty!!')", true);
                    return false;
                }
                if (vdt.Rows[i]["TcStopTime"].ToString() != string.Empty)
                {
                    if (!DateTime.TryParse(vdt.Rows[i]["TcStopTime"].ToString(), out dt))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Tc Stop Time should be in date format!!" + vdt.Rows[i]["TcStopTime"].ToString() + "')", true);
                        return false;
                    }

                }
                if (vdt.Rows[i]["StartTime"].ToString() == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Start time can not empty!!')", true);
                    return false;
                }
                if (vdt.Rows[i]["StartTime"].ToString() != string.Empty)
                {
                    if (!DateTime.TryParse(vdt.Rows[i]["StartTime"].ToString(), out dt))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Start time should be in date format!!')", true);
                        return false;
                    }

                }
                if (vdt.Rows[i]["StopTime"].ToString() == string.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Stop Time can not empty!!')", true);
                    return false;
                }
                if (vdt.Rows[i]["StopTime"].ToString() != string.Empty)
                {
                    if (!DateTime.TryParse(vdt.Rows[i]["StopTime"].ToString(), out dt))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Stop time should be in date format!!')", true);
                        return false;
                    }

                }

                WeekStartDate = vdt.Rows[i]["TcStartTime"].ToDataConvertDateTime();
                WeekEndDate = vdt.Rows[i]["TcStopTime"].ToDataConvertDateTime();
                DayStartDate = vdt.Rows[i]["StartTime"].ToDataConvertDateTime();
                DayEndDate = vdt.Rows[i]["StopTime"].ToDataConvertDateTime();

                if (WeekStartDate > DayStartDate || WeekEndDate < DayStartDate)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','Start Date should come between TcStart Time and TcStop Time!!')", true);

                    return false;
                }
                if (WeekStartDate > DayEndDate || WeekEndDate < DayEndDate)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','End Date should come between TcStart Time and TcStop Time!!')", true);
                    return false;

                }

                FirstDayName = vdt.Rows[i]["TcStartTime"].ToDataConvertDateTime().ToString("dddd");
                LastDayName = vdt.Rows[i]["TcStopTime"].ToDataConvertDateTime().ToString("dddd");

                if (FirstDayName.ToLower() != "monday")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','All TcStart Time should be Monday!!')", true);
                    return false;
                }
                if (LastDayName.ToLower() != "sunday")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','All TcStop time should be Sunday!!')", true);
                    return false;
                }
                days = (vdt.Rows[i]["TcStopTime"].ToDataConvertDateTime() - vdt.Rows[i]["TcStartTime"].ToDataConvertDateTime()).Days + 1;
                if (days != 7)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "text", "alertme('error','TcStart Time and TcStop Time difference should be 7 days!!')", true);
                    return false;
                }

            }

            return true;
        }

        #endregion

        //protected void DownloadFile1()
        //{
        //    string fileName = "userData.xlsx";

        //    //FTP Server URL.
        //    string ftp = "ftp://devsrv.apolloindia.com//";

        //    //FTP Folder name. Leave blank if you want to Download file from root folder.
        //    string ftpFolder = "TELESYSTEMS//";

        //    try
        //    {
        //        //Create FTP Request.
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + fileName);
        //        request.Method = WebRequestMethods.Ftp.DownloadFile;

        //        //Enter FTP Server credentials.
        //        request.Credentials = new NetworkCredential("apolloadmin", "FTP!@#app");
        //        request.UsePassive = true;
        //        request.UseBinary = true;
        //        request.EnableSsl = false;

        //        //Fetch the Response and read it into a MemoryStream object.
        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            //Download the File.
        //            response.GetResponseStream().CopyTo(stream);
        //            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.BinaryWrite(stream.ToArray());
        //            Response.End();
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
        //    }
        //} 

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            convertExcelToDatFile();
        }

        
    }
}