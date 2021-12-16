using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class InvoiceApproval : System.Web.UI.Page
    {
        string strFileName;
        string mTo = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        WhInvoiceUploadData _WhInvoiceUploadData = new WhInvoiceUploadData();
        WhInvoiceUploadDto _WhInvoiceUploadDto = new WhInvoiceUploadDto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindBu();
            }
            Page.ClientScript.RegisterOnSubmitStatement(GetType(), "ServerForm", "if(this.submitted) return false; this.submitted = true;");
            btnUpload.Attributes.Add("onclick", string.Format("this.value='Please wait...';this.disabled=true; {0}",
             ClientScript.GetPostBackEventReference(btnUpload, string.Empty)));
        }
        string RqId { get { return Request["requestId"]; } }

        #region BindCustomer

        public void BindBu()
        {
            DataSet ds = null;
            WhInvoiceUploadData _whInvoiceUploadData = new WhInvoiceUploadData();
            WhBindBuDTo _BindBuDto = new WhBindBuDTo();
            ds = _whInvoiceUploadData.BindBU();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                drpBu.DataSource = dt;
                drpBu.DataValueField = "BUID";
                drpBu.DataTextField = "BUName";
                drpBu.DataBind();
                drpBu.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }
        public void BindCustomer(string Bu)
        {
            try
            {
             
                WhCustomerMasterData _customermasterData = new WhCustomerMasterData();


                WhCustomerMasterDto _CustomerMasterDto = new WhCustomerMasterDto();

                IList<WhCustomerMasterDto> list = _customermasterData.GetAllByBU(Bu);
                if (list != null)
                {
                    drpCustomer.DataSource = list;
                    drpCustomer.DataValueField = "ID";
                    drpCustomer.DataTextField = "Name";
                    drpCustomer.DataBind();
                    drpCustomer.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }
                else
                {
                    drpCustomer.Items.Clear();
                    drpCustomer.Items.Insert(0, new ListItem("--Select--", string.Empty));
                }

            }
            catch (Exception ex)
            {

            }
        }

        #endregion
        #region FileUpload
        bool Upload()
        {
            string str = null;
            string fileExt = string.Empty;
            HttpPostedFile myFile = FpUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File have no data", "Success!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            string extName = System.IO.Path.GetExtension(myFile.FileName).ToLower();
            str = myFile.FileName;
            if (extName != string.Empty)
            {

                // Read file into a data stream
                byte[] myData = new Byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);
                DateTime _now = DateTime.Now;
                string _dd = _now.ToString("dd"); //
                string _mm = _now.ToString("MM");
                string _yy = _now.ToString("yyyy");
                string _hh = _now.Hour.ToString();
                string _min = _now.Minute.ToString();
                string _ss = _now.Second.ToString();
                string _uniqueId = _dd + _mm + _hh + _yy + _min + _ss;
                string name = str.Replace(" ", "_");
                // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an incremental numeric until it is unique

                // sFilename = System.IO.Path.GetFileName(myFile.FileName);
                // strFileName = RqId+LovelySession.Lovely.User.Name + "_" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();

                strFileName = drpCustomer.SelectedItem.Text +"_"+Path.GetFileNameWithoutExtension(myFile.FileName).ToLower()+ _uniqueId + Path.GetExtension(myFile.FileName).ToLower(); //Project Approval
                strFileName = strFileName.Replace(" ", "_");
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string path = Server.MapPath(appPath + "FrontEnd/Operations/ApprovalInvoice/" + strFileName);
                // Save the stream to disk
                FileStream newFile = new FileStream(path, FileMode.Create); // new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();


                if (RqId == null)
                {
                    _WhInvoiceUploadDto.ID = null;
                }
                else
                {
                    _WhInvoiceUploadDto.ID = RqId.ToNullLong();
                }
                _WhInvoiceUploadDto.Message = txtMessage.Text;
                _WhInvoiceUploadDto.FileName = strFileName;
                _WhInvoiceUploadDto.Bu = drpBu.SelectedItem.Text;
                _WhInvoiceUploadDto.InvoiceNO = txtInvoiceNo.Text;
                _WhInvoiceUploadDto.CustomerID = drpCustomer.SelectedValue;
                _WhInvoiceUploadDto.Password = Guid.NewGuid().ToString();
                hfPassword.Value = _WhInvoiceUploadDto.Password;
                int ID = _WhInvoiceUploadData.Insert(_WhInvoiceUploadDto);
                if (ID > 0)
                {
                    hfID.Value = ID.ToString();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "File has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
                return true;
            }
            else
            {
                //strFileName = string.Empty;
            }
            return true;
        }
        bool sendMail()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = "Invoice Approval Request [Invoice No: " + txtInvoiceNo.Text + "]";
                GetApproverID();
                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi<br/><br/>";
                textBody = textBody + LovelySession.Lovely.User.Name + " has  posted a  invoice.<br/><br/>";
                textBody = textBody + "<b>Message : </b>" + txtMessage.Text + "<br/>";
                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=A&ID=" + hfID.Value + "&ApproverID=" + ApproverID + "&Password=" + hfPassword.Value + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=R&ID=" + hfID.Value + "&ApproverID=" + ApproverID + "&Password=" + hfPassword.Value + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalInvoice/" + strFileName + "> <b><font size=5>View Invoice </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = hfMailApproverMailID.Value.ToString();
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                // mBCC = "harishsangwan23@gmail.com";
                mBCC = hfMailApproverMailID.Value.ToString();
                mSubject = subject;
                mBody = textBody;
                if (!sendMailForFF(mFrom, mTo, mSubject, mBody))
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }
        public bool GetApproverID()
        {
            try
            {
                WhApproverDTo result = _WhInvoiceUploadData.GetApproverIDByBU(drpBu.SelectedItem.Text, "1");
                if (result != null)
                {
                    hfMailApproverID.Value = result.ID.ToString();
                    hfMailApproverMailID.Value = result.EmailID;
                    SaveTrans();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        bool sendMailForFF(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
            message.Bcc.Add(mBCC);
            message.To.Add(mTo);
            message.From = new MailAddress(from);
            message.Subject = subject;

            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("crm@als.group", "crm@5466");
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = SMTPUserInfo;


            try
            {
                client.Send(message);
                return true;
                //lblMsg.Text = "Your Email has been sent sucessfully -Thank You";
            }
            catch (Exception exc)
            {
                return false;
                //Response.Write("Send failure: " + exc.ToString());
                //lblMsg.Text = "Your Email Send failure";
            }
        }

        bool SaveTrans()
        {
            try
            {
                WhInvoiceUploadTransDTo _InvUplTrans = new WhInvoiceUploadTransDTo();
                _InvUplTrans.Inv_Upl_Mas_ID = hfID.Value.ToNullLong();
                _InvUplTrans.ApproverID = hfMailApproverID.Value.ToNullLong();
                _InvUplTrans.Status = string.Empty;
                int i = _WhInvoiceUploadData.InsertTrans(_InvUplTrans);
                if(i>0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Data has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    return true;
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Not Validate in Transaction", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                    
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return true;
        }
        #endregion

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Upload();
            btnUpload.Attributes.Add("onclick", "this.disabled='true';");
            sendMail();
            Clear();
            btnUpload.Attributes.Add("onload", "this.disabled='false';");
        }
        public void Clear()
        {
            try
            {
                drpBu.Items.Clear();
                drpBu.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpCustomer.Items.Clear();
                txtFileName.Text = string.Empty;
                txtMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }

        protected void drpBu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpBu.SelectedValue != string.Empty)
            {
                BindCustomer(drpBu.SelectedValue);
            }
            else
            {
                drpCustomer.Items.Clear();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Select BU", "Error!", Toastr.ToastPosition.TopCenter, true);
                return;
            }

        }
    }
}