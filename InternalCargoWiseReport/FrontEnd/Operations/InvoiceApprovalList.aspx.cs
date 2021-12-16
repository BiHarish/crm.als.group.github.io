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
    public partial class InvoiceApprovalList : System.Web.UI.Page
    {
        string strFileName;
        string mTo = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        string NxtApproverID = "";
        string NxtApproverMailID = "";
        string UserName = "";
        string RqMessage = "";
        string FileName = "";
        string Password = "";
        string RqStatus = "";
        string CustomerName = "";
        string InvoiceNo = "";
        string USERID = "";
        string MasID = "";
        DataSet _ds = null;
        WhInvoiceUploadData _WhInvoiceUploadData = new WhInvoiceUploadData();
        WhInvoiceUploadDto _WhInvoiceUploadDto = new WhInvoiceUploadDto();


        WhInvoiceUploadTransDTo _InvUplTrans = new WhInvoiceUploadTransDTo();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGridOrDropDown();
            }
            Page.ClientScript.RegisterOnSubmitStatement(GetType(), "ServerForm", "if(this.submitted) return false; this.submitted = true;");
            btnSearch.Attributes.Add("onclick", string.Format("this.value='Please wait...';this.disabled=true; {0}",
             ClientScript.GetPostBackEventReference(btnSearch, string.Empty)));
        }
        string RqId { get { return Request["requestId"]; } }

        #region BindCustomer
        public void BindGridOrDropDown()
        {
            try
            {
                DataSet _ds = null;
                DataTable dt = new DataTable();
                WhInvoiceUploadTransDTo _WhInvoiceUploadTransDTo = new WhInvoiceUploadTransDTo();
                _WhInvoiceUploadTransDTo.ApproverID = LovelySession.Lovely.User.Id;
                _WhInvoiceUploadTransDTo.Status = drpStatus.SelectedItem.Text;
                _WhInvoiceUploadTransDTo.Inv_Upl_Mas_ID = drpInvoice.SelectedValue.ToNullLong();
                _ds = _WhInvoiceUploadData.GetListUserWise(_WhInvoiceUploadTransDTo);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    dt = _ds.Tables[0];
                    if (drpInvoice.SelectedValue == string.Empty)
                    {
                        drpInvoice.DataSource = dt;
                        drpInvoice.DataTextField = "InvoiceNo";
                        drpInvoice.DataValueField = "MasID";
                        drpInvoice.DataBind();
                        drpInvoice.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    }
                    txtRecordFound.Text = dt.Rows.Count.ToString();
                    gvInvoiceList.DataSource = dt;
                    gvInvoiceList.DataBind();
                    if (drpStatus.SelectedItem.Text != "Pending" && drpStatus.SelectedItem.Text != string.Empty)
                    {
                        gvInvoiceList.Columns[7].Visible = false;
                        gvInvoiceList.Columns[8].Visible = false;
                    }
                    else
                    {
                        gvInvoiceList.Columns[7].Visible = true;
                        gvInvoiceList.Columns[8].Visible = true;
                    }
                }
                else
                {
                    drpInvoice.Items.Clear();
                    gvInvoiceList.DataBind();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            BindGridOrDropDown();
        }

        protected void gvlblFileName_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;
            string ID = ((LinkButton)sender).CommandArgument.ToString();
            LinkButton lnkFileName = (LinkButton)gvRow.FindControl("gvlblFileName");


            string Path = "ApprovalInvoice/" + lnkFileName.Text;
            var filePath = Path;//"ApolloLogisolutions/Health&Wellness/ClaimForm.pdf";
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkFileName.Text);
            Response.TransmitFile(filePath);
            Response.End();
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;
            string ID = ((LinkButton)sender).CommandArgument.ToString();
            Label gvlblMasID = (Label)gvRow.FindControl("gvlblMasID");
            Label lblApproverID = (Label)gvRow.FindControl("gvlblApproverID");
            Label gvlblPassword = (Label)gvRow.FindControl("gvlblPassword");
            RadioButton rdbApproved = (RadioButton)gvRow.FindControl("rdbApproved");
            RadioButton rdbDisapproved = (RadioButton)gvRow.FindControl("rdbDisapproved");

            if (rdbApproved.Checked == true || rdbDisapproved.Checked == true)
            {
                _InvUplTrans.Inv_Upl_Mas_ID = gvlblMasID.Text.ToNullLong();
                _InvUplTrans.ApproverID = lblApproverID.Text.ToNullLong();
                _InvUplTrans.Password = gvlblPassword.Text;
                if (rdbApproved.Checked == true)
                {
                    _InvUplTrans.Status = "Approved";
                    RqStatus = "Approved";
                }
                else
                {
                    _InvUplTrans.Status = "Disapproved";
                    RqStatus = "Disapproved";
                }

                _ds = _WhInvoiceUploadData.UpdateStatus(_InvUplTrans);

                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = _ds.Tables[0];
                    if (dt.Rows[0]["RqStatus"].ToString() == "Final")
                    {
                        MasID = gvlblMasID.Text;
                        InvoiceNo = dt.Rows[0]["InvoiceNo"].ToString();
                        UserName = dt.Rows[0]["UserName"].ToString();
                        USERID = dt.Rows[0]["USERID"].ToString();
                        FileName = dt.Rows[0]["FileName"].ToString();
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        RqMessage = dt.Rows[0]["Message"].ToString();
                        sendMailToUser();
                        string Message = "Request has been Successfully " + RqStatus;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('" + Message + "')</script>");
                    }
                    else
                    {
                        MasID = gvlblMasID.Text;
                        InvoiceNo = dt.Rows[0]["InvoiceNo"].ToString();
                        NxtApproverID = dt.Rows[0]["NxtApproverID"].ToString();
                        NxtApproverMailID = dt.Rows[0]["NxtApproverMailID"].ToString();
                        UserName = dt.Rows[0]["UserName"].ToString();
                        RqMessage = dt.Rows[0]["Message"].ToString();
                        FileName = dt.Rows[0]["FileName"].ToString();
                        CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        Password = dt.Rows[0]["Password"].ToString();
                        sendMailNextApprover();
                    }
                }
            }
            else
            {
                string Message = "Please select Approved or Disapproved";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('" + Message + "')</script>");
            }




        }

        #region MailFunction
        bool sendMailNextApprover()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = "Invoice Approval Request [Invoice No: " + InvoiceNo + "]";
                string ApproverID = NxtApproverID;
                string textBody = "Hi<br/><br/>";
                textBody = textBody + UserName + " has  posted a  invoice.<br/><br/>";
                textBody = textBody + "<b>Message : </b>" + RqMessage + "<br/>";
                textBody = textBody + "<b>Customer Name : </b>" + CustomerName + "<br/>";
                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=A&ID=" + MasID + "&ApproverID=" + ApproverID + "&Password=" + Password + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/InvoiceQuickApproval.aspx?Type=R&ID=" + MasID + "&ApproverID=" + ApproverID + "&Password=" + Password + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalInvoice/" + FileName + "> <b><font size=5>View Invoice </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = NxtApproverMailID;
                mBCC = "";
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
        bool sendMailToUser()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = "[Invoice No: " + InvoiceNo + "] [Status:" + RqStatus + "]";
                string textBody = "Hi<br/>";
                textBody = textBody + UserName + " your Invoice  has been " + RqStatus + ".<br/><br/>";
                textBody = textBody + "<b>Customer Name : </b>" + CustomerName + "<br/>";
                textBody = textBody + "<b>Message : </b>" + RqMessage + "<br/>";
                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalInvoice/" + FileName + "> <b><font size=5>View Invoice </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = USERID;
                mBCC = "";
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
        bool sendMailForFF(string from, string to, string subject, string body)
        {
            System.Net.Mail.MailMessage message;

            message = new System.Net.Mail.MailMessage();
            message.Body = body;
            message.IsBodyHtml = true;
            // message.Bcc.Add(mBCC);
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
        #endregion

        protected void gvInvoiceList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}