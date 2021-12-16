using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Operations
{
    public partial class UserInquiry : CustomBasePage
    {

        string mTo;
        string mCC;
        string mBCC;
        string mBody;
        string mSubject;
        string strFileName;
        byte[] FileData;

        #region Properties
        CityData _cityData = null;
        StateData _stateData = null;
        private long Id = 0;

        string RqId { get { return Request["requestId"]; } }
        string RqInqNo { get { return Request["InqNo"]; } }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                bindDrp();
                lblDateOfInquiry.Text = DateTime.Now.ToString();
                if (RqInqNo != null)
                {
                    txtInquiryID.Text = RqInqNo;
                    getbyInqNo();
                    enableFalse();

                }

            }
        }


        #endregion

        #region Method
        void enableFalse()
        {
            txtInquiryID.Enabled = false;
            drpType.Enabled = false;
            txtOrgName.Enabled = false;
            txtOrgAddress.Enabled = false;
            ddlCountry.Enabled = false;
            txtCityName.Enabled = false;
            txtPostalCode.Enabled = false;
            txtState.Enabled = false;
            txtWebsite.Enabled = false;
            txtRegNo.Enabled = false;
            drpSalesRepName.Enabled = false;
            drpLeadInterest.Enabled = false;
            txtInquiryContact.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtMobile.Enabled = false;
            txtFax.Enabled = false;
            drpJobDesc.Enabled = false;
            btnCancel.Visible = false;
        }
        string getInquiryNo()
        {
            MaxValData _maxvalData = new MaxValData();
            MaxValDto result = _maxvalData.GetByDescription("UserInquiry");
            if (result != null)
            {
                return "Enq/" + txtOrgName.Text.Substring(0, 3) + "/" + DateTime.Now.ToString("yyyymmddhhmm") + "/" + result.Value;
            }

            return string.Empty;
        }

        void updateMaxVal()
        {
            MaxValData _maxvalData = new MaxValData();
            MaxValDto obj = new MaxValDto();
            obj.Description = "UserInquiry";
            _maxvalData.Update(obj);
        }

        private InquiryDto MappingObject(InquiryDto obj)
        {
            obj.Id = 0;
            obj.OrgInquiryId = getInquiryNo();
            obj.OrgEnquiryType = drpType.SelectedValue;
            obj.OrgName = txtOrgName.Text;
            obj.OrgAddress = txtOrgAddress.Text;
            if (ddlCountry.SelectedValue != "All") obj.OrgCountry = Convert.ToInt64(ddlCountry.SelectedValue);
            obj.OrgCity = txtCityName.Text;
            obj.OrgPostCode = txtPostalCode.Text;
            obj.OrgState = txtState.Text;
            obj.OrgWebsite = txtWebsite.Text;
            obj.OrgRegNo = txtRegNo.Text;
            obj.InquiryContact = txtInquiryContact.Text;
            obj.Phone = txtPhone.Text;
            obj.Email = txtEmail.Text;
            obj.MobNo = txtMobile.Text;
            obj.FaxNo = txtFax.Text;
            obj.JobDesc = drpJobDesc.SelectedValue;
            obj.LeadIntrest = drpLeadInterest.SelectedValue;
            if (drpSalesRepName.SelectedValue != string.Empty) obj.SalesRepName = Convert.ToInt64(drpSalesRepName.SelectedValue);
            if (LovelySession.Lovely.User.Name != null)
                obj.CreateBy = LovelySession.Lovely.User.Name;
            obj.CreateOn = DateTime.Now;
            obj.InquiryDate = lblDateOfInquiry.Text.ToConvertNullDateTime();
            obj.CommType = "Portal";
            if (strFileName != string.Empty)
                obj.FName = strFileName;
            if (FileData != null)
                obj.FileData = FileData;
            return obj;
        }

        private OpportunityDto getProperties(OpportunityDto obj)
        {
            obj.Id = 0;
            if (hfID.Value != "-1") obj.InquiryID = Convert.ToInt64(hfID.Value);
            obj.OppOrigin = txtOrgin.Text;
            obj.OppDestination = txtDestination.Text;
            obj.OppMode = drpMode.SelectedValue;
            obj.OppContainer = drpContainer.SelectedValue;
            obj.OppContType = drpOppType.SelectedValue;
            obj.OppContainerCount = txtContainerCount.Text;
            obj.OppRecurring = drpRecurring.SelectedValue;
            obj.OppVerticalMarket = txtVerticalMarket.Text;
            obj.OppActivityPeriod = drpPeriodOfActivity.SelectedValue;
            obj.OppCarrier = txtCarrier.Text;
            obj.CreateBy = LovelySession.Lovely.User.Name;
            obj.CreateOn = DateTime.Now;
            obj.Weight = txtWeight.Text.ToDouble();
            obj.Unit = drpUnit.SelectedValue;
            obj.CommodityID = drpCommodity.SelectedValue.ToNullLong();
            obj.ModifyBy = LovelySession.Lovely.User.Name;
            obj.Competitor = txtCompetitor.Text;
            obj.Terms = txtTerms.Text;
            obj.CountType = drpCountType.SelectedValue;


            return obj;
        }

        private void Clear()
        {
            txtInquiryID.Text = string.Empty;
            drpType.SelectedValue = string.Empty;
            txtOrgName.Text = string.Empty;
            txtOrgAddress.Text = string.Empty;
            ddlCountry.SelectedValue = "";
            txtCityName.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtState.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtRegNo.Text = string.Empty;
            drpSalesRepName.SelectedValue = string.Empty;
            drpLeadInterest.SelectedValue = string.Empty;
            txtInquiryContact.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtFax.Text = string.Empty;
            drpJobDesc.SelectedValue = string.Empty;
            hfID.Value = string.Empty;

        }

        void clearOppData()
        {
            txtOrgin.Text = string.Empty;
            drpMode.SelectedValue = string.Empty;
            drpOppType.SelectedValue = string.Empty;
            drpRecurring.SelectedValue = string.Empty;
            drpPeriodOfActivity.SelectedValue = string.Empty;
            txtWeight.Text = string.Empty;
            txtDestination.Text = string.Empty;
            drpContainer.SelectedValue = string.Empty;
            txtContainerCount.Text = string.Empty;
            txtVerticalMarket.Text = string.Empty;
            txtCarrier.Text = string.Empty;
            drpUnit.SelectedValue = string.Empty;
            txtCompetitor.Text = string.Empty;
            txtTerms.Text = string.Empty;
        }

        void bindDrp()
        {
            SalesRepMasterData _salesRepMasterData = new SalesRepMasterData();
            IList<SalesRepMasterDto> salesResults = _salesRepMasterData.GetAll();
            if (salesResults != null)
            {
                drpSalesRepName.DataSource = salesResults;
                drpSalesRepName.DataTextField = "Name";
                drpSalesRepName.DataValueField = "ID";
                drpSalesRepName.DataBind();
                drpSalesRepName.Items.Insert(0, new ListItem("All", string.Empty));
            }


            ContainerMasterData _containerMasterData = new ContainerMasterData();
            IList<ContainerMasterDto> results = _containerMasterData.GetAll(true);
            if (results != null)
            {
                drpContainer.DataSource = results;
                drpContainer.DataTextField = "ContCode";
                drpContainer.DataValueField = "ID";
                drpContainer.DataBind();
                drpContainer.Items.Insert(0, new ListItem("All", string.Empty));
            }


            CountryData _countryData = new CountryData();
            ddlCountry.DataSource = _countryData.GetAll(true);
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("All", ""));


            CommodityMasterData _cmdMasterData = new CommodityMasterData();
            IList<CommodityMasterDto> cmdResults = _cmdMasterData.GetAll();
            if (cmdResults != null)
            {
                drpCommodity.DataSource = cmdResults;
                drpCommodity.DataTextField = "Name";
                drpCommodity.DataValueField = "ID";
                drpCommodity.DataBind();
                drpCommodity.Items.Insert(0, new ListItem("All", string.Empty));
            }
        }

        bool validation()
        {
            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtMobile.Text, @"^[0-9]{10}$"))
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Valid Phone Number", "Oops!", Toastr.ToastPosition.TopCenter, true);

            //    return false;
            //}
            if (drpType.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpType.Focus();
                return false;
            }
            if (txtOrgName.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Organization name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtOrgName.Focus();
                return false;
            }
            if (txtOrgName.Text!=string.Empty)
            {
                if(txtOrgName.Text.Trim().Length<3)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Org name can't be less then 3 characters", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtOrgName.Focus();
                    return false;

                }
            }
            if (txtOrgAddress.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Org Address", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtOrgAddress.Focus();
                return false;
            }
            if (ddlCountry.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Country", "Oops!", Toastr.ToastPosition.TopCenter, true);
                ddlCountry.Focus();
                return false;
            }
            if (txtCityName.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter city name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtCityName.Focus();
                return false;
            }
            if (txtPostalCode.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Postal code", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtPostalCode.Focus();
                return false;
            }
            if (txtState.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter State name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtState.Focus();
                return false;
            }
            //if (txtWebsite.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Website name", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtWebsite.Focus();
            //    return false;
            //}
            //if (txtRegNo.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Reg No", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtRegNo.Focus();
            //    return false;
            //}

            if (getInquiryNo() == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Inquiry Number not Created", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpSalesRepName.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Sales Rep Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpLeadInterest.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select lead interest", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpLeadInterest.Focus();
                return false;
            }
            if (txtInquiryContact.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Enquiry Contact", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtInquiryContact.Focus();
                return false;
            }
            //if (txtPhone.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter phone no", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtPhone.Focus();
            //    return false;
            //}
            //if (txtEmail.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Email.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtEmail.Focus();
            //    return false;
            //}
            if (txtMobile.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Mobile No.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtMobile.Focus();
                return false;
            }
            //if (txtFax.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Fax No.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtFax.Focus();
            //    return false;
            //}
            if (drpJobDesc.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Job Description.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpJobDesc.Focus();
                return false;
            }

            return true;
        }

        bool OppValidation()
        {
            if (drpMode.SelectedValue == "Air" && drpOppType.SelectedValue == "LSE")
            {

                if (drpCountType.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Count Type.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            else
            {
                if (drpContainer.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Container No.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }
           // Upload();
            uploadFileInDatabase();
            InquiryDto request = MappingObject(new InquiryDto());
            InquiryData _inquiryData = new InquiryData();
            long result = _inquiryData.Insert(request);
            if (result > 0)
            {
                txtInquiryID.Text = getInquiryNo();
                hfID.Value = result.ToString();

                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnPopUp.Visible = true;
                updateMaxVal();
                btnSubmit.Visible = false;
               // sendMailForEnquiry();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }

        }

        protected void BtnSave_Click1(object sender, EventArgs e)
        {
            double d;
            if (!OppValidation())
            {
                mp1.Show();
                return;
            }
            if (drpOppType.SelectedValue == "LCL")
            {
                if (txtContainerCount.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Container Count.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            if (txtWeight.Text != string.Empty)
            {
                if (!double.TryParse(txtWeight.Text, out d))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Weight Should be Numeric/Decimal Value", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    mp1.Show();
                    return;
                }
            }

            if (!chkExistOrNote())
            {
                MPE.Show();
                return;
            }
            OpportunityDto request = getProperties(new OpportunityDto());
            InquiryData _inquiryData = new InquiryData();
            bool result = _inquiryData.InsertOpportunityData(request);
            if (result)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Opportunity Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnPopUp.Visible = false;
                btnSubmit.Visible = true;
               // sendMailForopportunity();
                Clear();
                clearOppData();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Opportunity Record Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        #endregion

        #region Active Grid Work




        #endregion

        protected void txtInquiryID_TextChanged(object sender, EventArgs e)
        {
            getbyInqNo();
        }

        void getbyInqNo()
        {
            InquiryData _inquiryData = new InquiryData();
            if (txtInquiryID.Text != string.Empty)
            {
                InquiryDto result = _inquiryData.getbyInquiryNo(txtInquiryID.Text);
                if (result != null)
                {
                    drpType.SelectedValue = result.OrgEnquiryType;
                    txtOrgName.Text = result.OrgName;
                    txtOrgAddress.Text = result.OrgAddress;
                    if (result.OrgCountry != null)
                        ddlCountry.SelectedValue = result.OrgCountry.ToString();
                    txtCityName.Text = result.OrgCity;
                    txtPostalCode.Text = result.OrgPostCode;
                    txtState.Text = result.OrgState;
                    txtWebsite.Text = result.OrgWebsite;
                    txtRegNo.Text = result.OrgRegNo;
                    drpSalesRepName.SelectedValue = result.SalesRepName.ToString();
                    drpLeadInterest.SelectedValue = result.LeadIntrest;
                    txtInquiryContact.Text = result.InquiryContact;
                    txtPhone.Text = result.Phone;
                    txtEmail.Text = result.Email;
                    txtMobile.Text = result.MobNo;
                    txtFax.Text = result.FaxNo;
                    drpJobDesc.SelectedValue = result.JobDesc;
                    hfID.Value = result.Id.ToString();
                    if (result.InquiryDate != null)
                        lblDateOfInquiry.Text = result.InquiryDate.Value.ToString("dd MMM yyyy");

                    if (hfID.Value != string.Empty)
                    {
                        btnSubmit.Visible = false;
                        btnPopUp.Visible = true;
                    }


                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "No Record found with this Enquiry No", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtInquiryID.Text = string.Empty;
                    btnPopUp.Visible = false;
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Enquiry No", "Oops!", Toastr.ToastPosition.TopCenter, true);
                Clear();
                btnPopUp.Visible = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            clearOppData();
            btnSubmit.Visible = true;
            btnPopUp.Visible = false;
        }

        bool chkExistOrNote()
        {
            InquiryData _inquriyData = new InquiryData();
            InquiryDto obj = new InquiryDto();
            obj.SalesRepName = drpSalesRepName.SelectedValue.ToNullLong();
            obj.InquiryDate = lblDateOfInquiry.Text.ToConvertNullDateTime();
            obj.OrgName = txtOrgName.Text;
            obj.Origin = txtOrgin.Text;
            obj.Destination = txtDestination.Text;
            obj.ContainerCount = txtContainerCount.Text;

            InquiryDto result = _inquriyData.chkExistOrNote(obj);
            if (result != null)
            {
                txtMSalesRepName.Text = result.RepName;
                if (result.InquiryDate != null)
                    txtMInquiryDate.Text = result.InquiryDate.Value.ToString("dd MMM yyyy");
                txtMOrgName.Text = result.OrgName;
                txtMOrigin.Text = result.Origin;
                txtMDestination.Text = result.Destination;
                txtMContainerCount.Text = result.ContainerCount;
                MPE.Show();
                return false;
            }
            return true;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (!OppValidation())
            {
                return;
            }
            OpportunityDto request = getProperties(new OpportunityDto());
            InquiryData _inquiryData = new InquiryData();
            bool result = _inquiryData.InsertOpportunityData(request);
            if (result)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Opportunity Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnPopUp.Visible = false;
                btnSubmit.Visible = true;
                sendMailForopportunity();
                Clear();
                clearOppData();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Opportunity Record Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mp1.Show();
        }

        #region Mail

        public bool sendMailForEnquiry()
        {
            if (drpSalesRepName.SelectedValue != null)
            {
                long salesRepID = Convert.ToInt64(drpSalesRepName.SelectedValue);
                SalesRepMasterData _salesRepMasterData = new SalesRepMasterData();
                SalesRepMasterDto result = _salesRepMasterData.GetById(salesRepID);
                if (result != null)
                {
                    string mFrom = "crm@als.group";
                    string mPassword = "crm@5466";
                    mBCC = "harishsangwan23@gmail.com,debu@als.group,anuj.p@als.group ";

                    mSubject = "OrgName:" + result.OrgName + ",EnquiryNo:" + result.InquiryNo;
                    mTo = result.CorMailID;

                    mBody = mBody + "Hi" + " " + result.Name + ",<br><br>";
                    mBody = mBody + "Enquiry Details are:<br><br>";
                    mBody = mBody + "<table cellspacing='10' cellpadding='1' border='1'><tr><td><b>EnquiryNo:</b></td><td>" + result.InquiryNo + "</td><td><b>Type:</b></td><td>" + drpType.SelectedValue + "</td></tr>";
                    mBody = mBody + "<tr><td><b> OrgName:</b></td><td> " + txtOrgName.Text + " </td><td> <b>OrgAddress:</b></td><td> " + txtOrgAddress.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Country:</b></td><td> " + ddlCountry.SelectedItem.Text + " </td><td><b> City:</b></td><td> " + txtCityName.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> PostalCode:</b></td><td> " + txtPostalCode.Text + " </td><td><b> State:</b></td><td> " + txtState.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Website:</b></td ><td> " + txtWebsite.Text + " </td><td> <b>RegNo:</b></td><td> " + txtRegNo.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> SalesRepName:</b></td><td> " + drpSalesRepName.SelectedItem.Text + " </td><td> <b>LeadInterest:</b></td><td> " + drpLeadInterest.SelectedItem.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Enquiry Contact:</b></td><td> " + txtInquiryContact.Text + " </td><td><b> Phone:</b></td><td> " + txtPhone.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Email:</b></td><td> " + txtEmail.Text + " </td><td><b> Mobile:</b></td><td> " + txtMobile.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Fax:</b></td><td> " + txtFax.Text + " </td><td><b> JobDesc:</b></td><td> " + drpJobDesc.SelectedValue + " </td></tr> ";
                    mBody = mBody + "</table><br><br>";
                    mBody += "Regards,<br>IT Team";



                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mFrom, mTo, mSubject, mBody);
                    message.IsBodyHtml = true;
                    message.Bcc.Add(new MailAddress(mBCC));
                    //Smtp client                
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    //Smtp Port 25
                    //client.Port = 465;
                    client.Port = 587;
                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(mFrom, mPassword);
                    client.UseDefaultCredentials = false;
                    //true if SSL Exits else false
                    client.EnableSsl = true;
                    client.Credentials = SMTPUserInfo;


                    try
                    {
                        client.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool sendMailForopportunity()
        {
            mBody = string.Empty;
            if (drpSalesRepName.SelectedValue != null)
            {
                long salesRepID = Convert.ToInt64(drpSalesRepName.SelectedValue);
                SalesRepMasterData _salesRepMasterData = new SalesRepMasterData();
                SalesRepMasterDto result = _salesRepMasterData.GetById(salesRepID);
                if (result != null)
                {
                    string mFrom = "crm@als.group";
                    string mPassword = "crm@5466";
                    mBCC = "harishsangwan23@gmail.com,debu@als.group,anuj.p@als.group ";

                    mSubject = "OrgName:" + result.OrgName + ",EnquiryNo:" + result.InquiryNo;
                    mTo = result.CorMailID;

                    mBody = mBody + "Hi" + " " + result.Name + ",<br><br>";
                    mBody = mBody + "Opportunity Details are:<br><br>";
                    mBody = mBody + "<table cellspacing='10' cellpadding='1' border='1'><tr><td><b>Origin:</b></td><td>" + txtOrgin.Text + "</td><td><b>Destination:</b></td><td>" + txtDestination.Text + "</td></tr>";
                    mBody = mBody + "<tr><td><b> Mode:</b></td><td> " + drpMode.SelectedValue + " </td><td> <b>Container:</b></td><td> " + drpContainer.SelectedItem.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Type:</b></td><td> " + drpOppType.SelectedValue + " </td><td><b> Container Count:</b></td><td> " + txtContainerCount.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Recurring:</b></td><td> " + drpRecurring.SelectedItem.Text + " </td><td><b> Vertical Market:</b></td><td> " + txtVerticalMarket.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Period Of Activity:</b></td ><td> " + drpPeriodOfActivity.SelectedItem.Text + " </td><td> <b>Carrier:</b></td><td> " + txtCarrier.Text + " </td></tr> ";
                    mBody = mBody + "<tr><td><b> Weight:</b></td><td> " + txtWeight.Text + " </td><td> <b>Unit:</b></td><td> " + drpUnit.SelectedValue + " </td></tr> ";
                    mBody = mBody + "</table><br><br>";
                    mBody += "Regards,<br>IT Team";



                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mFrom, mTo, mSubject, mBody);
                    message.IsBodyHtml = true;
                    message.Bcc.Add(new MailAddress(mBCC));
                    //Smtp client                
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    //Smtp Port 25
                    //client.Port = 465;
                    client.Port = 587;
                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(mFrom, mPassword);
                    client.UseDefaultCredentials = false;
                    //true if SSL Exits else false
                    client.EnableSsl = true;
                    client.Credentials = SMTPUserInfo;

                    try
                    {
                        client.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInquiryList.aspx?lovelyindexing=105");
        }

        protected void drpMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            mp1.Show();
            if (drpMode.SelectedValue != string.Empty)
            {
                if (drpMode.SelectedValue == "Air")
                {
                    drpOppType.Items.Clear();
                    drpOppType.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drpOppType.Items.Insert(1, new ListItem("LSE", "LSE"));
                    drpOppType.Items.Insert(2, new ListItem("ULD", "ULD"));
                }
                else if (drpMode.SelectedValue == "Sea")
                {
                    drpOppType.Items.Clear();
                    drpOppType.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drpOppType.Items.Insert(1, new ListItem("FCL", "FCL"));
                    drpOppType.Items.Insert(2, new ListItem("LCL", "LCL"));
                    drpOppType.Items.Insert(3, new ListItem("Bulk", "BLK"));
                    drpOppType.Items.Insert(4, new ListItem("Liquid", "LQD"));
                    drpOppType.Items.Insert(5, new ListItem("Break Bulk", "BBK"));
                    drpOppType.Items.Insert(6, new ListItem("Roll On-Roll Off", "ROR"));

                }
            }
            else
            {
                drpOppType.Items.Clear();
                drpOppType.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        protected void drpOppType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mp1.Show();
            if (drpMode.SelectedValue == "Air" && drpOppType.SelectedValue == "LSE")
            {
                drpContainer.SelectedValue = string.Empty;
                lblContainer.Visible = false;
                drpContainer.Visible = false;
                lblContainerStar.Visible = false;
                lblCountType.Visible = true;
                drpCountType.Visible = true;
                lblCountTypeStar.Visible = true;

            }
            else
            {
                drpCountType.SelectedValue = string.Empty;
                lblContainer.Visible = true;
                drpContainer.Visible = true;
                lblContainerStar.Visible = true;
                lblCountType.Visible = false;
                drpCountType.Visible = false;
                lblCountTypeStar.Visible = false;
            }

        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
          //  Upload();
            uploadFileInDatabase();
        }
        void Upload()
        {
            string str = null;
            string fileExt = string.Empty;
            string sSavePath;
            string sFilename = "";

            // Set constant values
            sSavePath = "CrmUserFiles/";
            // Check file size (mustn’t be 0)
            HttpPostedFile myFile = FileUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                //lblMsg.Text = "Error: There wasn't any file uploaded.";
               // Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select file", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return;
            }

            ////					// Check file extension (must be JPG)
            string extName = System.IO.Path.GetExtension(myFile.FileName).ToLower();
            str = myFile.FileName;

            switch (extName) // this switch code validate the files which allow to upload only PDF file   
            {
                case ".pdf":
                    fileExt = "application/pdf";
                    break;
                case ".xls":
                    fileExt = "application/xls";
                    break;
                case ".xlsx":
                    fileExt = "application/xlsx";
                    break;
                case ".png":
                    fileExt = "application/png";
                    break;
                case ".jpg":
                    fileExt = "application/png";
                    break;
                case ".jpeg":
                    fileExt = "application/jpeg";
                    break;
                case ".doc":
                    fileExt = "application/doc";
                    break;
                case ".docx":
                    fileExt = "application/docx";
                    break;
            }
            if (fileExt != string.Empty)
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
                string _uniqueId = _dd + _hh + _mm + _min + _ss + _yy;
                // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an incremental numeric until it is unique
                //sFilename = System.IO.Path.GetFileName(myFile.FileName);
                strFileName = LovelySession.Lovely.User.Name + "_" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string path = Server.MapPath(appPath + "FrontEnd/Operations/CrmUserFiles/" + strFileName);
                // Save the stream to disk
                System.IO.FileStream newFile = new FileStream(path, FileMode.Create); // new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();
                //lblMsg.Text="File " + sFilename + " Uploaded Successfully";
                //RefreshGrid(Server.MapPath(sSavePath + sFilename));

               // strFileName = sFilename;
            }
            else
            {
                strFileName = string.Empty;
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File Format not supported", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }

        void uploadFileInDatabase()
        {
            if (!FileUpload.HasFile)
            {
                Response.Write("No file Selected"); return;
            }
            else
            {
                string filename = Path.GetFileName(FileUpload.PostedFile.FileName);
                string extension = Path.GetExtension(filename).ToLower();
                string contentType = FileUpload.PostedFile.ContentType;
                HttpPostedFile file = FileUpload.PostedFile;
                byte[] size = new byte[file.ContentLength];
                
                file.InputStream.Read(size, 0, file.ContentLength);

                

                //Validations  
                if ((extension == ".pdf") || (extension == ".doc") || (extension == ".docx") ||
                    (extension == ".xls") || (extension == ".xlsx") || (extension == ".jpg") || (extension == ".png") || (extension == ".jpeg"))//extension  
                {
                    if (file.ContentLength <= 31457280)//size  
                    {
                        //Insert the Data in the Table  
                        //InquiryData _inqData = new InquiryData();
                        //InquiryDto request = new InquiryDto();
                        //request.FName = filename;
                        //request.FileData = document;
                        //_inqData.Insert(request);
                        //Response.Write("Save"); return;

                        strFileName = filename;
                        FileData = size;
                    }
                    else
                    { Response.Write("Inavlid File size"); return; }
                }
                else
                {
                    Response.Write("Inavlid File"); return;
                }
            }  
        }
    }
}
