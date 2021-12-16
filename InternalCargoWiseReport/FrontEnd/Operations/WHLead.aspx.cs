using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class WHLead : System.Web.UI.Page
    {
        long? WHLeadID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }
                bindDrp();
                FirstGridViewRow();
                FirstGridViewRowForContact();

                if (RqId != null)
                {
                    setData();
                    bindGridData();
                    bindGridDataForContact();
                    // enableFalse();
                    btnSubmit.Text = "Update";
                    btnCancel.Visible = false;
                    HfID.Value = RqId.ToString();
                }

            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        string RqId { get { return Request["requestId"]; } }
        #region Method
        private WHLeadMasterDto MappingObject(WHLeadMasterDto obj)
        {
            if (HfID.Value != string.Empty)
                obj.ID = HfID.Value.ToLong();
            //obj.LeadNo = string.Empty;
            obj.LeadSource = txtLeadSource.Text;
            obj.CustomerName = drpCustomerName.SelectedValue;
            obj.Stage = drpStage.SelectedValue;
            obj.Lineofbusiness = drpLineOfBusiness.SelectedValue;
            obj.OpportunityBrief = txtOppBrief.Text;
            obj.StatusStage = drpStatusStage.SelectedValue;
            obj.MonthlyBilling = txtMothlyBilling.Text.ToDouble();
            obj.GP = txtGP.Text.ToDouble();
            obj.ProjectETA = txtProjectEta.Text.ToConvertNullDateTime();
            obj.DesignatedBD = drpDesignatedBD.SelectedValue;
            obj.DesignatedSolution = drpDesignatedSolution.SelectedValue;
            obj.StatusUpdate = txtStatusUpdate.Text;
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.New_Encirclement = drpNewEncirclement.SelectedValue;
            obj.BU = drpBU.SelectedValue;
            //obj.ContactPersonName = txtContactPersonName.Text;
            //obj.ContactPersonDesignation = txtContactPersonDesignation.Text;
            //obj.ContactPersonMailID = txtContactPersonMailID.Text;
            //obj.ContactPersonPhoneNo = txtContactPhoneNo.Text;
            obj.ProjectATA = txtProjectATA.Text.ToConvertNullDateTime();
            obj.Region = drpRegion.SelectedValue;
            obj.Segment = txtSegment.Text;
            obj.UserTypeID = LovelySession.Lovely.User.UserTypeId;
            obj.RevenueRange = txtRevenueRange.Text.ToString();
            obj.LostRemarks = txtLostRemarks.Text.ToString();
            return obj;
        }
        bool validation()
        {
            int count = 0;
            if (drpCustomerName.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Customer Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpCustomerName.Focus();
                return false;
            }
            if (drpNewEncirclement.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type of Business ", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpNewEncirclement.Focus();
                return false;
            }
            if (drpBU.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select BU.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpBU.Focus();
                return false;
            }

            if (drpDesignatedBD.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Designated BD.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpDesignatedBD.Focus();
                return false;
            }

            if (drpStage.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select stage.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpStage.Focus();
                return false;
            }
            if (drpStage.SelectedValue == "Cancelled" || drpStage.SelectedValue == "Lost")
            {
                if(txtLostRemarks.Text==string.Empty)
                {

                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "In case of stage Lost/Cancelled Remarks is mandatory.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    LostRemarks.Focus();
                    return false;
                }
            }
            if (drpLineOfBusiness.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Line Of Business.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpLineOfBusiness.Focus();
                return false;
            }
            if (drpStatusStage.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Crm Stage.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpStatusStage.Focus();
                return false;
            }
            if (drpStatusStage.SelectedValue != "1" && drpStatusStage.SelectedValue != "2")
            {
                if (txtProjectEta.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Project ETA Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtProjectEta.Focus();
                    return false;
                }

                if (txtMothlyBilling.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Monthly Billing.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtMothlyBilling.Focus();
                    return false;
                }
                if (txtGP.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter GP.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtGP.Focus();
                    return false;
                }

            }
            if (drpStatusStage.SelectedValue == "6")
            {
                if (txtProjectATA.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Project ATA Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    drpCustomerName.Focus();
                    return false;
                }
            }
            if (drpStatusStage.SelectedValue != "1" && drpStatusStage.SelectedValue != "2" && drpStatusStage.SelectedValue != "3")
            {
                if (drpDesignatedSolution.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Designated Solution.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    drpDesignatedSolution.Focus();
                    return false;
                }
            }
            if (txtOppBrief.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Opportunity Briff.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtOppBrief.Focus();
                return false;
            }


            if (txtGP.Text != string.Empty)
            {
                if (txtGP.Text.ToDouble() > 100)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "GP can't be more then 100.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtGP.Focus();
                    return false;
                }
            }
            if (drpRegion.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Region.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpRegion.Focus();
                return false;
            }
            if (txtSegment.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select/Enter Segment.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpSegment.Focus();
                return false;
            }

            foreach (GridViewRow gvRow in gvStatusUpdate.Rows)
            {
                TextBox txtstatus = (TextBox)gvRow.FindControl("gvtxtStatusUpdate");
                if (txtstatus.Text != string.Empty)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter at least one Status.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }


            foreach (GridViewRow gvRow in gvContactPersonDetails.Rows)
            {
                TextBox gvtxtName = (TextBox)gvRow.FindControl("gvtxtName");
                TextBox gvtxtDesignation = (TextBox)gvRow.FindControl("gvtxtDesignation");
                TextBox gvtxtMailID = (TextBox)gvRow.FindControl("gvtxtMailID");
                TextBox gvtxtPhoneNo = (TextBox)gvRow.FindControl("gvtxtPhoneNo");

                if (gvtxtName.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtName.Focus();
                    return false;

                }
                if (gvtxtDesignation.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Desination.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtDesignation.Focus();
                    return false;
                }
                if (gvtxtMailID.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Mail ID.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtMailID.Focus();
                    return false;
                }
                if (gvtxtMailID.Text != string.Empty)
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(gvtxtMailID.Text.Trim());
                    if (match.Success)
                    { }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter valid email address.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                }
                if (gvtxtPhoneNo.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Phone No.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtPhoneNo.Focus();
                    return false;
                }

            }
            return true;
        }
        void clear()
        {
            txtLeadSource.Text = string.Empty;
            drpCustomerName.SelectedValue = string.Empty;
            drpStage.SelectedValue = string.Empty;
            drpLineOfBusiness.SelectedValue = string.Empty;
            txtOppBrief.Text = string.Empty;
            drpStatusStage.SelectedValue = string.Empty;
            txtMothlyBilling.Text = string.Empty;
            txtGP.Text = string.Empty;
            txtProjectEta.Text = string.Empty;
            drpDesignatedBD.SelectedValue = string.Empty;
            drpDesignatedSolution.SelectedValue = string.Empty;
            txtStatusUpdate.Text = string.Empty;
            drpBU.SelectedValue = string.Empty;
            drpNewEncirclement.SelectedValue = string.Empty;
            //txtContactPersonName.Text = string.Empty;
            //txtContactPersonMailID.Text = string.Empty;
            //txtContactPersonDesignation.Text = string.Empty;
            //txtContactPhoneNo.Text = string.Empty;
            drpRegion.SelectedValue = string.Empty;
            drpSegment.SelectedValue = string.Empty;
            txtSegment.Text = string.Empty;
        }

        void setData()
        {
            long rqID = RqId.ToDataConvertInt64();
            WHLeadMasterData _whLeadData = new WHLeadMasterData();
            WHLeadMasterDto result = _whLeadData.GetById(rqID, LovelySession.Lovely.User.UserTypeId);
            if (result != null)
            {
                txtLeadSource.Text = result.LeadSource;
                drpCustomerName.SelectedValue = result.CustomerName;
                drpStage.SelectedValue = result.Stage;
                if(drpStage.SelectedValue=="Cancelled")
                {
                    LostRemarks.Visible = true;
                }
                drpLineOfBusiness.SelectedValue = result.Lineofbusiness;
                txtOppBrief.Text = result.OpportunityBrief;
                drpStatusStage.SelectedValue = result.StatusStage;
                txtMothlyBilling.Text = result.MonthlyBilling.ToString();
                txtGP.Text = result.GP.ToString();
                if (result.ProjectETA != null)
                {
                    txtProjectEta.Text = result.ProjectETA.Value.ToString("dd MMM yyyy");
                    txtProjectEta.Enabled = false;
                }
                else
                {
                    txtProjectEta.Enabled = true;
                }

                drpDesignatedBD.SelectedValue = result.DesignatedBD;
                drpDesignatedSolution.SelectedValue = result.DesignatedSolution;
                txtStatusUpdate.Text = result.StatusUpdate;
                drpNewEncirclement.SelectedValue = result.New_Encirclement;
                drpBU.SelectedValue = result.BU;
                //txtContactPersonName.Text = result.ContactPersonName;
                //txtContactPersonMailID.Text = result.ContactPersonMailID;
                //txtContactPersonDesignation.Text = result.ContactPersonDesignation;
                //txtContactPhoneNo.Text = result.ContactPersonPhoneNo;
                drpRegion.SelectedValue = result.Region;
                txtSegment.Text = result.Segment;
                drpSegment.SelectedValue = result.Segment;
                txtSegment.Enabled = false;
                if (result.ProjectATA != null)
                    txtProjectATA.Text = result.ProjectATA.Value.ToString("dd MMM yyyy");


                if (result.StatusStage == "7")
                {
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    txtProjectATA.Enabled = false;
                    drpStage.Enabled = false;
                    drpStatusStage.Enabled = false;
                    txtMothlyBilling.Enabled = false;
                    txtGP.Enabled = false;
                }
                if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 1)
                {
                    btnSubmit.Visible = true;
                    gvStatusUpdate.Enabled = true;
                }
                if (result.RevenueRange != string.Empty)
                {
                    txtRevenueRange.Text = result.RevenueRange.ToString();
                }
                else
                {
                    RevenueRange();
                }

            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        void enableFalse()
        {
            txtLeadSource.Enabled = false;
            drpCustomerName.Enabled = false;
            //drpStage.Enabled = false;
            drpLineOfBusiness.Enabled = false;
            txtOppBrief.Enabled = false;
            //drpStatusStage.Enabled = false;
            //txtMothlyBilling.Enabled = false;
            //txtGP.Enabled = false;
            txtProjectEta.Enabled = false;
            drpDesignatedBD.Enabled = false;
            drpDesignatedSolution.Enabled = false;
            txtStatusUpdate.Enabled = false;
            drpBU.Enabled = false;
            drpNewEncirclement.Enabled = false;
            //txtContactPersonName.Enabled = false;
            //txtContactPersonMailID.Enabled = false;
            //txtContactPersonDesignation.Enabled = false;
            //txtContactPhoneNo.Enabled = false;
            drpRegion.Enabled = false;
            drpSegment.Enabled = false;
        }
        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<BDSolutionMasterDto> AllBD = _bdSolutionMasterData.GetAllBD().OrderBy(x => x.BD).ToList();
            IList<BDSolutionMasterDto> AllSoultions = _bdSolutionMasterData.GetAllSolution().OrderBy(x => x.Solution).ToList();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("SCS");
            if (AllBD != null)
            {
                drpDesignatedBD.DataSource = AllBD;
                drpDesignatedBD.DataValueField = "ID";
                drpDesignatedBD.DataTextField = "BD";
                drpDesignatedBD.DataBind();
                drpDesignatedBD.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (AllSoultions != null)
            {
                drpDesignatedSolution.DataSource = AllSoultions;
                drpDesignatedSolution.DataValueField = "ID";
                drpDesignatedSolution.DataTextField = "Solution";
                drpDesignatedSolution.DataBind();
                drpDesignatedSolution.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

            WHLeadMasterData _leadData = new WHLeadMasterData();
            IList<WHLeadMasterDto> getSegment = _leadData.getSegment(LovelySession.Lovely.User.UserTypeId, "SCS");
            if (getSegment != null)
            {

                drpSegment.DataSource = getSegment;
                drpSegment.DataValueField = "Segment";
                drpSegment.DataTextField = "Segment";
                drpSegment.DataBind();
                drpSegment.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpSegment.Items.Insert((getSegment.Count + 1), new ListItem("Other", "Other"));
            }
            _leadData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> CrmStageData = _leadData.getCrmStageData();
            if (CrmStageData != null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (customerResults != null)
            {

                drpCustomerName.DataSource = customerResults;
                drpCustomerName.DataValueField = "ID";
                drpCustomerName.DataTextField = "Name";
                drpCustomerName.DataBind();
                drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));


            }
        }

        void saveStatusGridData()
        {
            foreach (GridViewRow gvRow in gvStatusUpdate.Rows)
            {
                TextBox gvtxtStatusUpdate = (TextBox)gvRow.FindControl("gvtxtStatusUpdate");
                Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                long StatusID;
                if (gvlblID.Text == string.Empty)
                {
                    StatusID = 0;
                }
                else
                {
                    StatusID = gvlblID.Text.ToLong();
                }
                WHLeadStatusUpdateDto request = new WHLeadStatusUpdateDto();
                request.ID = StatusID;
                request.Status = gvtxtStatusUpdate.Text;
                if (RqId == null)
                {
                    request.WHLeadID = WHLeadID;
                }
                else
                {
                    request.WHLeadID = RqId.ToNullLong();
                }
                request.ModifyBy = LovelySession.Lovely.User.Id;
                if (gvtxtStatusUpdate.Text != string.Empty)
                {
                    WHLeadMasterData _whleadMasterData = new WHLeadMasterData();
                    long id = _whleadMasterData.InsertStatus(request);
                }
            }
        }

        void saveContactPersonGridData()
        {
            foreach (GridViewRow gvRow in gvContactPersonDetails.Rows)
            {
                TextBox gvtxtName = (TextBox)gvRow.FindControl("gvtxtName");
                TextBox gvtxtDesignation = (TextBox)gvRow.FindControl("gvtxtDesignation");
                TextBox gvtxtMailID = (TextBox)gvRow.FindControl("gvtxtMailID");
                TextBox gvtxtPhoneNo = (TextBox)gvRow.FindControl("gvtxtPhoneNo");
                Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                long ContactPersonID;
                if (gvlblID.Text == string.Empty)
                {
                    ContactPersonID = 0;
                }
                else
                {
                    ContactPersonID = gvlblID.Text.ToLong();
                }

                WhLeadContactTransDto request = new WhLeadContactTransDto();
                request.ID = ContactPersonID;
                request.Name = gvtxtName.Text;
                request.Designation = gvtxtDesignation.Text;
                request.MailID = gvtxtMailID.Text;
                request.PhoneNo = gvtxtPhoneNo.Text;
                if (RqId == null)
                {
                    request.WhLeadID = WHLeadID;
                }
                else
                {
                    request.WhLeadID = RqId.ToNullLong();
                }


                WhLeadContactTransData _whContactPersonData = new WhLeadContactTransData();

                if (ContactPersonID == 0)
                {
                    if (!_whContactPersonData.Insert(request))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Connection Error I.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    if (!_whContactPersonData.Update(request))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Connection Error U.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
        }

        void bindGridData()
        {
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> results = _whLeadMasterData.GetByIdForStatus(RqId.ToLong());
            if (results != null)
            {
                gvStatusUpdate.DataSource = results;
                gvStatusUpdate.DataBind();

                ViewState["CurrentTable"] = results.ToList().ToDataTable<WHLeadStatusUpdateDto>();
            }

        }

        void bindGridDataForContact()
        {
            WhLeadContactTransData _whContactData = new WhLeadContactTransData();
            IList<WhLeadContactTransDto> results = _whContactData.getByWhLeadID(RqId.ToLong());
            if (results != null)
            {
                gvContactPersonDetails.DataSource = results;
                gvContactPersonDetails.DataBind();

                ViewState["CurrentTableForContact"] = results.ToList().ToDataTable<WhLeadContactTransDto>();
            }

        }

        public void RevenueRange()
        {
            try
            {
                if (txtMothlyBilling.Text == string.Empty || txtMothlyBilling.Text.ToNullDouble() == 0)
                {
                    txtRevenueRange.Text = "N/A";

                }
                else if (txtMothlyBilling.Text.ToNullDouble() > 0 && txtMothlyBilling.Text.ToNullDouble() <= 100000)
                {
                    txtRevenueRange.Text = "Less than 1 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 100000 && txtMothlyBilling.Text.ToNullDouble() <= 500000)
                {
                    txtRevenueRange.Text = " 1-5 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 500000 && txtMothlyBilling.Text.ToNullDouble() <= 1000000)
                {
                    txtRevenueRange.Text = "5-10 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 1000000 && txtMothlyBilling.Text.ToNullDouble() <= 2000000)
                {
                    txtRevenueRange.Text = "10-20 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 2000000 && txtMothlyBilling.Text.ToNullDouble() <= 3000000)
                {
                    txtRevenueRange.Text = "20-30 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 3000000 && txtMothlyBilling.Text.ToNullDouble() <= 4000000)
                {
                    txtRevenueRange.Text = "30-40 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 4000000 && txtMothlyBilling.Text.ToNullDouble() <= 5000000)
                {
                    txtRevenueRange.Text = "40-50 Lacs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() > 5000000)
                {
                    txtRevenueRange.Text = "Greater than 50 Lacs";
                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        #region Save Button

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }

            WHLeadMasterDto request = MappingObject(new WHLeadMasterDto());
            WHLeadMasterData _leadData = new WHLeadMasterData();
            if (HfID.Value == string.Empty || HfID.Value == "0")
            {
                WHLeadID = _leadData.Insert(request);
                if (WHLeadID > 0)
                {
                    saveStatusGridData();
                    saveContactPersonGridData();
                    bindDrp();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    clear();
                    FirstGridViewRow();
                    FirstGridViewRowForContact();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                if (_leadData.Update(request))
                {
                    saveStatusGridData();
                    saveContactPersonGridData();
                    bindDrp();
                    btnSubmit.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                    if (txtProjectEta.Text != string.Empty)
                    {
                        txtProjectEta.Enabled = false;
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record not updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
            }

        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Operations/WhLeadList.aspx");
        }

        #region Gridview Function
        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("ModifyOn", typeof(string)));
            dr = dt.NewRow();

            dr["RowNumber"] = 1;
            dr["ID"] = string.Empty;
            dr["Status"] = string.Empty;
            dr["ModifyOn"] = DateTime.Now.ToString("dd MMM yyyy");
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvStatusUpdate.DataSource = dt;
            gvStatusUpdate.DataBind();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvStatusUpdate.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        TextBox gvtxtStatusUpdate = (TextBox)gvStatusUpdate.Rows[rowIndex].Cells[1].FindControl("gvtxtStatusUpdate");
                        Label gvlblDate = (Label)gvStatusUpdate.Rows[rowIndex].Cells[2].FindControl("gvlblDate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ID"] = gvlblID.Text;
                        dtCurrentTable.Rows[i - 1]["Status"] = gvtxtStatusUpdate.Text;

                        dtCurrentTable.Rows[i - 1]["ModifyOn"] = gvlblDate.Text;
                        rowIndex++;


                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    string aaa;
                    foreach (DataRow dr in dtCurrentTable.Rows)
                    {
                        aaa = dr["ModifyOn"].ToString();
                        if (aaa == string.Empty)
                        {
                            dr["ModifyOn"] = DateTime.Now.ToString("dd MMM yyyy");
                        }
                    }
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvStatusUpdate.DataSource = dtCurrentTable;
                    gvStatusUpdate.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvStatusUpdate.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        TextBox gvtxtStatusUpdate = (TextBox)gvStatusUpdate.Rows[rowIndex].Cells[1].FindControl("gvtxtStatusUpdate");
                        Label gvlblDate = (Label)gvStatusUpdate.Rows[rowIndex].Cells[2].FindControl("gvlblDate");

                        gvlblID.Text = dt.Rows[i]["ID"].ToString();
                        gvtxtStatusUpdate.Text = dt.Rows[i]["Status"].ToString();
                        gvlblDate.Text = dt.Rows[i]["ModifyOn"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvStatusUpdate.Rows)
            {
                TextBox gvtxtStatusUpdate = (TextBox)gvRow.FindControl("gvtxtStatusUpdate");
                if (gvtxtStatusUpdate.Text == string.Empty)
                {

                    return;
                }
            }
            AddNewRow();

        }

        protected void gvStatusUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                LinkButton btn = (LinkButton)e.CommandSource;
                if (btn != null)
                {
                    GridViewRow grdrow = (GridViewRow)btn.NamingContainer;
                    int row = grdrow.RowIndex;
                    //  deleteRow();
                    DataTable dtt = (DataTable)ViewState["CurrentTable"];

                    if (dtt.Rows.Count > 1)
                    {
                        dtt.Rows.RemoveAt(row);
                        dtt.AcceptChanges();
                    }

                    gvStatusUpdate.DataSource = dtt;
                    gvStatusUpdate.DataBind();
                    SetPreviousData();
                }
            }
        }

        protected void gvStatusUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox gvtxtUpdate = (e.Row.FindControl("gvtxtStatusUpdate") as TextBox);
                Label gvlblID = (e.Row.FindControl("gvlblID") as Label);
                LinkButton lnk = (e.Row.FindControl("lnkRemove") as LinkButton);
                if (gvtxtUpdate.Text != string.Empty && RqId != null && gvlblID.Text != string.Empty)
                {
                    gvtxtUpdate.Enabled = false;
                    lnk.Visible = false;
                }
            }
        }


        #endregion

        #region ContactGrid
        private void FirstGridViewRowForContact()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("MailID", typeof(string)));
            dt.Columns.Add(new DataColumn("PhoneNo", typeof(string)));
            dr = dt.NewRow();

            dr["RowNumber"] = 1;
            dr["ID"] = string.Empty;
            dr["Name"] = string.Empty;
            dr["Designation"] = string.Empty;
            dr["MailID"] = string.Empty;
            dr["PhoneNo"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTableForContact"] = dt;

            gvContactPersonDetails.DataSource = dt;
            gvContactPersonDetails.DataBind();
        }

        private void AddNewRowForContact()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTableForContact"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableForContact"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvContactPersonDetails.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        TextBox gvtxtName = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[1].FindControl("gvtxtName");
                        TextBox gvtxtDesignation = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[2].FindControl("gvtxtDesignation");
                        TextBox gvtxtMailID = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[3].FindControl("gvtxtMailID");
                        TextBox gvtxtPhoneNo = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[4].FindControl("gvtxtPhoneNo");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ID"] = gvlblID.Text;
                        dtCurrentTable.Rows[i - 1]["Name"] = gvtxtName.Text;
                        dtCurrentTable.Rows[i - 1]["Designation"] = gvtxtDesignation.Text;
                        dtCurrentTable.Rows[i - 1]["MailID"] = gvtxtMailID.Text;
                        dtCurrentTable.Rows[i - 1]["PhoneNo"] = gvtxtPhoneNo.Text;

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTableForContact"] = dtCurrentTable;

                    gvContactPersonDetails.DataSource = dtCurrentTable;
                    gvContactPersonDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousContactData();
        }

        private void SetPreviousContactData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTableForContact"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableForContact"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvContactPersonDetails.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        TextBox gvtxtName = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[1].FindControl("gvtxtName");
                        TextBox gvtxtDesignation = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[2].FindControl("gvtxtDesignation");
                        TextBox gvtxtMailID = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[3].FindControl("gvtxtMailID");
                        TextBox gvtxtPhoneNo = (TextBox)gvContactPersonDetails.Rows[rowIndex].Cells[4].FindControl("gvtxtPhoneNo");

                        gvlblID.Text = dt.Rows[i]["ID"].ToString();
                        gvtxtName.Text = dt.Rows[i]["Name"].ToString();
                        gvtxtDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        gvtxtMailID.Text = dt.Rows[i]["MailID"].ToString();
                        gvtxtPhoneNo.Text = dt.Rows[i]["PhoneNo"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void gvContactPersonDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                LinkButton btn = (LinkButton)e.CommandSource;
                if (btn != null)
                {
                    GridViewRow grdrow = (GridViewRow)btn.NamingContainer;
                    int row = grdrow.RowIndex;
                    //  deleteRow();
                    DataTable dtt = (DataTable)ViewState["CurrentTableForContact"];

                    if (dtt.Rows.Count > 1)
                    {
                        dtt.Rows.RemoveAt(row);
                        dtt.AcceptChanges();
                    }

                    gvContactPersonDetails.DataSource = dtt;
                    gvContactPersonDetails.DataBind();
                    SetPreviousContactData();
                }
            }
        }

        protected void gvContactPersonDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox gvtxtName = (e.Row.FindControl("gvtxtName") as TextBox);
                TextBox gvtxtDesignation = (e.Row.FindControl("gvtxtDesignation") as TextBox);
                TextBox gvtxtMailID = (e.Row.FindControl("gvtxtMailID") as TextBox);
                TextBox gvtxtPhoneNo = (e.Row.FindControl("gvtxtPhoneNo") as TextBox);
                Label gvlblID = (e.Row.FindControl("gvlblID") as Label);
                LinkButton lnk = (e.Row.FindControl("lnkRemove") as LinkButton);

                if (gvtxtName.Text != string.Empty && gvtxtDesignation.Text != string.Empty && gvtxtMailID.Text != string.Empty &&
                    gvtxtPhoneNo.Text != string.Empty && RqId != null && gvlblID.Text != string.Empty)
                {
                    //gvtxtName.Enabled = false;
                    //gvtxtDesignation.Enabled = false;
                    //gvtxtMailID.Enabled = false;
                    //gvtxtPhoneNo.Enabled = false;
                    lnk.Visible = false;
                }
            }
        }

        protected void BtnAddContactPerson_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvContactPersonDetails.Rows)
            {
                TextBox gvtxtName = (TextBox)gvRow.FindControl("gvtxtName");
                TextBox gvtxtDesignation = (TextBox)gvRow.FindControl("gvtxtDesignation");
                TextBox gvtxtMailID = (TextBox)gvRow.FindControl("gvtxtMailID");
                TextBox gvtxtPhoneNo = (TextBox)gvRow.FindControl("gvtxtPhoneNo");

                if (gvtxtName.Text == string.Empty || gvtxtDesignation.Text == string.Empty || gvtxtMailID.Text == string.Empty
                    || gvtxtPhoneNo.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please fill out all the contact person details.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            AddNewRowForContact();
        }
        #endregion

        protected void drpStatusStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2")
            {
                lblGpStar.Visible = false;
                lblMonthlyStar.Visible = false;
                lblProjectEtaStar.Visible = false;

            }
            else
            {
                lblGpStar.Visible = true;
                lblMonthlyStar.Visible = true;
                lblProjectEtaStar.Visible = true;
            }

            if (drpStatusStage.SelectedValue == "6")
            {
                lblProjectATAStar.Visible = true;
            }
            else
            {
                lblProjectATAStar.Visible = false;
            }
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2" || drpStatusStage.SelectedValue == "3")
            {
                lblDSStar.Visible = false;
            }
            else
            {
                lblDSStar.Visible = true;
            }
        }

        protected void drpSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSegment.SelectedValue == string.Empty)
            {
                txtSegment.Text = string.Empty;
                txtSegment.Enabled = true;
            }
            else if (drpSegment.SelectedValue != string.Empty)
            {
                txtSegment.Text = drpSegment.SelectedValue;
                txtSegment.Enabled = false;
            }

            if (drpSegment.SelectedValue.ToLower() == "other")
            {
                txtSegment.Text = string.Empty;
                txtSegment.Enabled = true;
                txtSegment.Focus();
            }
        }

        protected void txtMothlyBilling_TextChanged(object sender, EventArgs e)
        {
            RevenueRange();
        }

        
        protected void drpStage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(drpStage.SelectedValue=="Cancelled" || drpStage.SelectedValue == "Lost")
            {
                LostRemarks.Visible = true;
            }
            else
            {
                LostRemarks.Visible = false;
            }
        }
    }
}
