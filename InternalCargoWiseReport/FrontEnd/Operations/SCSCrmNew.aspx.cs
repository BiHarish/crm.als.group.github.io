using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class SCSCrmNew : System.Web.UI.Page
    {
        long? WHLeadID;
        double totMonthlyBilling = 0;
        string strFileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }

                if (RqId != null)
                {
                    ChkStage3Approver();
                }
                bindDrp();
                drpBU.SelectedValue = "1";
                FirstGridViewRow();
                FirstGridViewRowForContact();

                if (RqId != null)
                {
                    BindLeadTypeTransData();
                    setData();
                    FieldVisibility();

                    bindGridData();
                    bindGridDataForContact();
                    // enableFalse();
                    btnSubmit.Text = "Update";
                    btnCancel.Visible = false;
                    HfID.Value = RqId.ToString();

                }
                if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 23)
                {
                    if (RqId != null)
                    {
                        divCreditLimit.Visible = true;
                    }
                    else
                    {
                        divCreditLimit.Visible = false;
                    }

                    if (LovelySession.Lovely.User.UserTypeId == 23)
                    {
                        EnableFalse();
                    }
                }

                if (RqId != null)//for old record maintain, Hide update button and show approval button
                {
                    if (drpStatusStage.SelectedItem.Text == "Stage 3: OpportunityQualified(Approval Required)"
                        || drpStatusStage.SelectedItem.Text == "Stage 5: Project Approval(Approval Required)")
                    {
                        btnSubmit.Visible = false;
                        btnsendMail.Visible = true;
                    }
                }
                if (drpStatusStage.SelectedValue == "3" || drpStatusStage.SelectedValue == "4" || drpStatusStage.SelectedValue == "5" || drpStatusStage.SelectedValue == "6"
                 || drpStatusStage.SelectedValue == "7" || drpStatusStage.SelectedValue == "8")
                {
                    Label7.Visible = true;
                }
                else
                {
                    Label7.Visible = false;
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
            if (drpStage.SelectedValue == string.Empty)
            {
                obj.StatusStage = drpStatusStage.SelectedValue;
            }
            else
            {
                obj.StatusStage = "7";
            }

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

            obj.PricingType = drpPricingType.SelectedValue;
            obj.ContractType = drpContractType.SelectedValue;
            //obj.LeadType = drpLeadType.SelectedValue;
            //obj.UOM = drpUOM.SelectedValue;
            //obj.Qty_Nos = txtQty.Text.ToDataConvertNullDouble();
            //obj.PerUnitRevenue = txtRevenue.Text.ToDataConvertNullDouble();
            obj.ITSystem = drpItSystem.SelectedValue;
            if (drpItSystem.SelectedValue == "Customer System")
            {
                obj.ITSystemName = txtSystemName.Text;
            }
            else if (drpItSystem.SelectedValue == "ALS System")
            {
                obj.ITSystemName = drpSystemName.SelectedItem.Text;
            }

            obj.PostNegotitationStageID = drpPostNegotitationStage.SelectedValue.ToNullLong();
            obj.Competitor = txtCompetitorName.Text;
            obj.Reason = txtReason.Text;
            obj.Cancelled = drpCancelled.SelectedValue;
            obj.CloseRemarks = txtLeadClose.Text;

            return obj;
        }
        bool validation()
        {
            int count = 0;
            if (txtLeadSource.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Lead Source.", "Error!", Toastr.ToastPosition.TopCenter, true);
                txtLeadSource.Focus();
                return false;
            }
            if (drpCustomerName.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Customer Name.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpCustomerName.Focus();
                return false;
            }
            if (drpNewEncirclement.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type of Account ", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpNewEncirclement.Focus();
                return false;
            }
            if (drpBU.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Business Unit.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpBU.Focus();
                return false;
            }

            if (drpDesignatedBD.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Designated BD Person.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpDesignatedBD.Focus();
                return false;
            }



            if (drpLineOfBusiness.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Business Vertical.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpLineOfBusiness.Focus();
                return false;
            }
            if (drpStatusStage.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Stage of CRM.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpStatusStage.Focus();
                return false;
            }
            if (drpStatusStage.SelectedValue != "1" && drpStatusStage.SelectedValue != "2")
            {
                if (txtProjectEta.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Expected Time Of Onboarding.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    txtProjectEta.Focus();
                    return false;
                }

                if (txtMothlyBilling.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Projected Monthly Billing.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    txtMothlyBilling.Focus();
                    return false;
                }
                if (txtGP.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Projected GP.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    txtGP.Focus();
                    return false;
                }

            }
            if (drpStatusStage.SelectedValue == "6")
            {
                if (txtProjectATA.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Actual Time Of Onboarding.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    drpCustomerName.Focus();
                    return false;
                }
            }

            if (drpStatusStage.SelectedValue != "1" && drpStatusStage.SelectedValue != "2" && drpStatusStage.SelectedValue != "3")
            {
                if (drpDesignatedSolution.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Solution Design Person.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    drpDesignatedSolution.Focus();
                    return false;
                }
                if (drpContractType.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Contract Type.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    drpContractType.Focus();
                    return false;
                }
            }
            if (txtOppBrief.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Special Input.", "Error!", Toastr.ToastPosition.TopCenter, true);
                txtOppBrief.Focus();
                return false;
            }


            if (txtGP.Text != string.Empty)
            {
                if (txtGP.Text.ToDouble() > 100)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Projected GP can't be more then 100.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    txtGP.Focus();
                    return false;
                }
            }
            if (drpRegion.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Region.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpRegion.Focus();
                return false;
            }
            if (txtSegment.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select/Enter Segment.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpSegment.Focus();
                return false;
            }
            //if(drpStatusStage.SelectedValue=="7")
            //{
            //    if (drpStage.SelectedValue == string.Empty)
            //    {
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select stage.", "Error!", Toastr.ToastPosition.TopCenter, true);
            //        drpStage.Focus();
            //        return false;
            //    }
            //}

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
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter at least one Status.", "Error!", Toastr.ToastPosition.TopCenter, true);
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
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Name.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtName.Focus();
                    return false;

                }
                if (gvtxtDesignation.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Desination.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtDesignation.Focus();
                    return false;
                }
                if (gvtxtMailID.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Mail ID.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtMailID.Focus();
                    return false;
                }
                if (gvtxtMailID.Text != string.Empty)
                {
                    Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                    bool isValid = regex.IsMatch(gvtxtMailID.Text.Trim());
                    if (!isValid)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter valid email address.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }

                }
                if (gvtxtPhoneNo.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Phone No.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    gvtxtPhoneNo.Focus();
                    return false;
                }

            }
            if (drpItSystem.SelectedItem.Text == "")
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select IT System.", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpItSystem.SelectedValue != string.Empty)
            {
                if (drpItSystem.SelectedValue == "Customer System")
                {
                    if (txtSystemName.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter System Name.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }

                }
                else if (drpItSystem.SelectedValue == "ALS System")
                {
                    if (drpSystemName.SelectedItem.Text == "")
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select System Name.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                }
            }
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2")
            { }
            else
            {
                foreach (GridViewRow gvRow in gvLeadTypeTransaction.Rows)
                {
                    Label gvlblLOB = (Label)gvRow.FindControl("lblLeadType");
                    DropDownList drpUOM1 = (DropDownList)gvRow.FindControl("drpUOM");
                    TextBox gvtxtQtNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
                    TextBox gvtxtperunitRevenue = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
                    TextBox gvtxtMonthlyBilling = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");

                    if (drpUOM1.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select " + gvlblLOB.Text + " UOM.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                    else if (gvtxtQtNos.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter " + gvlblLOB.Text + " Qty_Nos.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }

                    else if (gvtxtperunitRevenue.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter " + gvlblLOB.Text + " Revenue per unit.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }
                    else if (gvtxtMonthlyBilling.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, gvlblLOB.Text + " Monthly billing not found.", "Error!", Toastr.ToastPosition.TopCenter, true);
                        return false;
                    }

                }
            }
            if (drpStage.SelectedValue != string.Empty)
            {
                if (txtLeadClose.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Lead Close remarks", "Error!", Toastr.ToastPosition.TopCenter, true);
                    txtLeadClose.Focus();
                    return false;
                }
            }


            return true;
        }

        bool approvalValidation()
        {
            if (txtMothlyBilling.Text == string.Empty || txtMothlyBilling.Text == "0")
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Projected Monthly billing Required.", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (txtGP.Text == string.Empty || txtGP.Text == "0")
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Projected GP Required.", "Error!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (txtProjectEta.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Enter Expected Time Of Onboarding.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpContractType.Focus();
                return false;
            }
            else if (drpContractType.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Contract Type.", "Error!", Toastr.ToastPosition.TopCenter, true);
                drpContractType.Focus();
                return false;
            }

            return true;
        }

        bool creditValidation()
        {
            if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 23)
            {
                if (txtCreditRating.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Credit Rating!!.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtCreditDays.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Credit Days!!.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtCreditLimit.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Credit Limit!!.", "Error!", Toastr.ToastPosition.TopCenter, true);
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
            //drpBU.SelectedValue = string.Empty;
            drpNewEncirclement.SelectedValue = string.Empty;
            //txtContactPersonName.Text = string.Empty;
            //txtContactPersonMailID.Text = string.Empty;
            //txtContactPersonDesignation.Text = string.Empty;
            //txtContactPhoneNo.Text = string.Empty;
            drpRegion.SelectedValue = string.Empty;
            drpSegment.SelectedValue = string.Empty;
            txtSegment.Text = string.Empty;
            txtCreditDays.Text = string.Empty;
            txtCreditLimit.Text = string.Empty;
            txtCreditRating.Text = string.Empty;
            txtRevenueRange.Text = string.Empty;
            drpPricingType.SelectedValue = string.Empty;
            drpContractType.SelectedValue = string.Empty;
            drpItSystem.SelectedValue = string.Empty;
            txtSystemName.Text = string.Empty;
            gvLeadTypeTransaction.DataBind();

        }

        void setData()
        {
            long rqID = RqId.ToDataConvertInt64();
            WHLeadMasterData _whLeadData = new WHLeadMasterData();
            WHLeadMasterDto result = _whLeadData.GetById(rqID, LovelySession.Lovely.User.UserTypeId);
            if (result != null)
            {
                hfLeadCreatedDate.Value = result.CreateOn.Value.ToString("dd MMM yyyy");
                drpBU.Enabled = false;
                drpLineOfBusiness.Enabled = false;

                if(result.StatusStage.ToLong()>3)
                {
                    gvLeadTypeTransaction.Enabled = false;
                }

                txtLeadSource.Text = result.LeadSource;
                drpCustomerName.SelectedValue = result.CustomerName;
                drpStage.SelectedValue = result.Stage;


                drpLineOfBusiness.SelectedValue = result.Lineofbusiness;
                txtOppBrief.Text = result.OpportunityBrief;
                drpStatusStage.SelectedValue = result.StatusStage;
                if (result.StatusStage != "1")
                {

                    int Count = result.StatusStage.ToDataConvertInt32();
                    // if(Count==6)
                    // {
                    //     Count = 5;
                    // }
                    if (RqId != null)
                    {
                        for (int i = 1; i < Count; i++)
                        {
                            string ID = i.ToString();
                            //drpStatusStage.Items[i].Attributes["disabled"] = "disabled";
                            drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                        }

                    }
                }
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
                drpPricingType.SelectedValue = result.PricingType;
                drpContractType.SelectedValue = result.ContractType;
                // drpLeadType.SelectedValue = result.LeadType;
                if (drpLineOfBusiness.SelectedValue != null)
                {
                    if (drpLineOfBusiness.SelectedValue == "Warehousing")
                    {
                        drpUOM.Items.Insert(0, new ListItem("--Select--", string.Empty));
                        drpUOM.Items.Insert(1, new ListItem("Unit", "Unit"));
                        drpUOM.Items.Insert(2, new ListItem("Sqft", "Sqft"));
                        drpUOM.Items.Insert(3, new ListItem("Pallet", "Pallet"));
                        drpUOM.Items.Insert(4, new ListItem("Throughput", "Throughput"));
                    }
                    else if (drpLineOfBusiness.SelectedValue == "Transportation")
                    {
                        drpUOM.Items.Insert(0, new ListItem("--Select--", string.Empty));
                        drpUOM.Items.Insert(1, new ListItem("Kg", "Kg"));
                        drpUOM.Items.Insert(2, new ListItem("Unit", "Unit"));
                        drpUOM.Items.Insert(3, new ListItem("Carton", "Carton"));
                        drpUOM.Items.Insert(4, new ListItem("Trip", "Trip"));
                        drpUOM.Items.Insert(5, new ListItem("Vehicle", "Vehicle"));
                    }
                    drpUOM.SelectedValue = result.UOM;
                }
                if (drpUOM.SelectedValue != null)
                {
                    txtQty.Visible = true;
                    txtRevenue.Visible = true;
                    lblQty.Visible = true;
                    lblRevenue.Visible = true;
                    lblQtyStar.Visible = true;
                    lblRevenueStar.Visible = true;
                    txtRevenue.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    txtQty.Text = result.Qty_Nos.ToString();
                    txtRevenue.Text = result.PerUnitRevenue.ToString();

                }
                txtSegment.Enabled = false;
                if (result.ProjectATA != null)
                    txtProjectATA.Text = result.ProjectATA.Value.ToString("dd MMM yyyy");


                if (result.StatusStage == "7")
                {
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    txtProjectATA.Enabled = false;
                    // drpStage.Enabled = false;
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

                lblStarSystemName.Visible = true;
                lblSystemName.Visible = true;
                drpItSystem.SelectedValue = result.ITSystem;
                if (drpItSystem.SelectedValue == "Customer System")
                {
                    txtSystemName.Visible = true;
                    drpSystemName.Visible = false;

                    drpSystemName.SelectedValue = string.Empty;
                    txtSystemName.Text = result.ITSystemName;
                }
                else if (drpItSystem.SelectedValue == "ALS System")
                {
                    txtSystemName.Visible = false;
                    drpSystemName.Visible = true;

                    txtSystemName.Text = string.Empty;

                    drpSystemName.SelectedItem.Text = result.ITSystemName;
                }

                if (result.PostNegotitationStageID != null)
                {
                    drpPostNegotitationStage.SelectedValue = result.PostNegotitationStageID.ToString();
                    txtCompetitorName.Text = result.Competitor;
                    txtReason.Text = result.Reason;
                    drpCancelled.SelectedValue = result.Cancelled;
                    if (result.PostNegotitationStageID == 1)
                    {
                        dvCmpName.Visible = true;
                        dvCmpReason.Visible = true;
                        dvCancelled.Visible = false;
                    }

                    if (result.PostNegotitationStageID == 2)
                    {
                        dvCmpName.Visible = false;
                        dvCmpReason.Visible = false;
                        dvCancelled.Visible = true;
                    }

                    if (result.PostNegotitationStageID == 3)
                    {
                        dvCmpName.Visible = false;
                        dvCmpReason.Visible = false;
                        dvCancelled.Visible = false;
                    }

                }

                WhCreditNoteData _creditNoteData = new WhCreditNoteData();
                WhCreditNoteDto creditResult = _creditNoteData.getbyWhleadIDAndCustomerID(drpCustomerName.SelectedValue, rqID);
                if (creditResult != null)
                {
                    hfCreditNoteID.Value = creditResult.ID.ToString();
                    txtCreditDays.Text = creditResult.CreditDays.ToString();
                    txtCreditLimit.Text = creditResult.CreditLimit.ToString();
                    txtCreditRating.Text = creditResult.CreditRating.ToString();


                }

                if (result.StatusStage != "5" && (result.StatusStage == "3" || result.StatusStage == "4"))
                {
                    drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue("6"));
                    drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue("8"));
                }

                if (result.StatusStage == "8")
                {
                    divNegotitation.Visible = true;
                    if (drpPostNegotitationStage.SelectedValue == string.Empty)
                    {
                        dvCancelled.Visible = false;
                        dvCmpName.Visible = false;
                        dvCmpReason.Visible = false;
                    }
                    if (drpPostNegotitationStage.SelectedValue == "1")
                    {
                        dvCancelled.Visible = false;
                        dvCmpName.Visible = true;
                        dvCmpReason.Visible = true;
                    }
                    if (drpPostNegotitationStage.SelectedValue == "2")
                    {
                        dvCancelled.Visible = true;
                        dvCmpName.Visible = false;
                        dvCmpReason.Visible = false;
                    }

                    if (drpPostNegotitationStage.SelectedValue == "3")
                    {
                        dvCancelled.Visible = false;
                        dvCmpName.Visible = false;
                        dvCmpReason.Visible = false;
                    }
                }

                else
                {
                    //gvLeadTypeTransaction.Enabled = false;
                }

                if (drpStage.SelectedValue != string.Empty)
                {
                    btnSubmit.Visible = false;
                    btnsendMail.Visible = false;
                    btnCancel.Visible = false;
                    txtLeadClose.Text = result.CloseRemarks;
                    divLeadCloseStatus.Visible = true;
                    if (result.StatusStage == "7")
                    {
                        drpStatusStage.Items.Clear();
                        drpStatusStage.Items.Insert(0, new ListItem("Stage 7 :Close", "7"));
                    }
                }


            }
            else
            {
                btnSubmit.Visible = false;
            }

            //if(result.StatusStage=="5" || result.StatusStage=="6" || result.StatusStage=="7" ||result.StatusStage=="8")
            //{
            //    gvLeadTypeTransaction.Enabled = false;
            //}
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
            WHLeadMasterData _whLeadData = new WHLeadMasterData();
            WHLeadMasterDto result = new WHLeadMasterDto();
            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            WhPostNegotitationData _whPostNegotitationData = new WhPostNegotitationData();
            IList<BDSolutionMasterDto> AllBD = _bdSolutionMasterData.GetAllBD().OrderBy(x => x.BD).ToList();
            IList<BDSolutionMasterDto> AllSoultions = _bdSolutionMasterData.GetAllSolution().OrderBy(x => x.Solution).ToList();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("SCS");
            WhItsystemMasterData _whItSystemMasterData = new WhItsystemMasterData();
            IList<WhItsystemMasterDto> systemResults = _whItSystemMasterData.GetAll();
            IList<WhPostNegotitationStageDto> _whpostNegotitation = _whPostNegotitationData.GetAllStage();

            if (RqId != null)
            {

                result = _whLeadData.GetById(RqId.ToLong(), LovelySession.Lovely.User.UserTypeId);
            }
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

            if (_whpostNegotitation != null)
            {
                drpPostNegotitationStage.DataSource = _whpostNegotitation;
                drpPostNegotitationStage.DataValueField = "ID";
                drpPostNegotitationStage.DataTextField = "Stage";
                drpPostNegotitationStage.DataBind();
                drpPostNegotitationStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (systemResults != null)
            {
                drpSystemName.DataSource = systemResults;
                drpSystemName.DataValueField = "ID";
                drpSystemName.DataTextField = "Name";
                drpSystemName.DataBind();
                drpSystemName.Items.Insert(0, new ListItem("--Select--", string.Empty));
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
            if (customerResults != null)
            {

                drpCustomerName.DataSource = customerResults;
                drpCustomerName.DataValueField = "ID";
                drpCustomerName.DataTextField = "Name";
                drpCustomerName.DataBind();
                drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));

            }
            ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            IList<WhBuMasterDto> results = _serviceTypeMasterData.BUGetAll();
            if (results != null)
            {
                drpBU.DataSource = results;
                drpBU.DataValueField = "ID";
                drpBU.DataTextField = "Name";
                drpBU.DataBind();
                drpBU.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

            _leadData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> CrmStageData = _leadData.getSCSCrmStageData();
            if (RqId == null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                for (int i = 3; i <= CrmStageData.Count; i++)
                {
                    string ID = i.ToString();

                    drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                }
                return;
            }
            if (CrmStageData != null)
            {

                if (hfMailApprover.Value == "NULL")
                {
                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    dt.Rows[2][8] = "Stage 3: OpportunityQualified(Approval Required)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();

                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    if (RqId != null)
                    {
                        for (int i = 4; i <= CrmStageData.Count; i++)
                        {
                            string ID = i.ToString();
                            //drpStatusStage.Items[i].Attributes["disabled"] = "disabled";
                            drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                        }

                    }

                }
                else if (hfMailApprover.Value == "Pending")
                {

                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    dt.Rows[2][8] = "Stage 3: OpportunityQualified(Approval Pending)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();
                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    for (int i = 4; i <= CrmStageData.Count; i++)
                    {
                        string ID = i.ToString();
                        //drpStatusStage.Items[i].Attributes["disabled"] = "disabled";
                        drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                    }

                }
                else if (hfMailApprover.Value == "Qualify on 3")
                {

                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    ViewState["VsStatusStage"] = dt;
                    dt.Rows[4][8] = "Stage 5: Project Approval(Approval Required)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();

                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    //drpStatusStage.Items[4].Attributes.Add("style", "color:red");
                    drpStatusStage.Items[0].Attributes.CssStyle.Add("color", "Blue");
                    drpStatusStage.Items[1].Attributes.CssStyle.Add("background-color", "#eae9e9");
                    drpStatusStage.Items[2].Attributes.CssStyle.Add("background-color", "#eae9e9");
                    drpStatusStage.Items[3].Attributes.CssStyle.Add("background-color", "#eae9e9");
                    drpStatusStage.Items[4].Attributes.CssStyle.Add("background-color", "#eae9e9");
                    drpStatusStage.Items[5].Attributes.CssStyle.Add("background-color", "#eae9e9");
                    drpStatusStage.Items[6].Attributes.CssStyle.Add("background-color", "#eae9e9");

                    if (RqId != null)
                    {
                        if (result.StatusStage == "1")
                        {
                            for (int i = 4; i <= CrmStageData.Count; i++)
                            {
                                string ID = i.ToString();

                                drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                            }
                        }
                        else if (result.StatusStage == "2")
                        {
                            for (int i = 4; i <= CrmStageData.Count; i++)
                            {
                                string ID = i.ToString();

                                drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                            }
                        }
                        else
                        {

                            for (int i = 6; i <= CrmStageData.Count; i++)
                            {
                                string ID = i.ToString();

                                drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                            }

                        }

                    }

                }
                else if (hfMailApprover.Value == "Pending on 5")
                {

                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    dt.Rows[4][8] = "Stage 5: Project Approval(Approval Pending)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();
                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    if (RqId != null)
                    {
                        for (int i = 6; i <= CrmStageData.Count; i++)
                        {
                            string ID = i.ToString();
                            //drpStatusStage.Items[i].Attributes["disabled"] = "disabled";
                            drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                        }

                    }

                }
                else if (hfMailApprover.Value == "Qualify on 5")
                {

                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    dt.Rows[6][8] = "Stage 7: Post Negotiation(Approval Required)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();
                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    if (RqId != null)
                    {
                        string ID = "7";
                        drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                        //drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue("6"));
                        //drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue("8"));
                    }

                }
                else if (hfMailApprover.Value == "Pending on 8" || hfMailApprover.Value == "send mail")
                {

                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();
                    dt.Rows[6][8] = "Stage 7: Post Negotiation(Approval Pending)";
                    dt.AcceptChanges();
                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();
                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    if (RqId != null)
                    {
                        string ID = "7";
                        drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue(ID));
                    }

                }

                else
                {


                    DataTable dt = CrmStageData.ToList().ToDataTable<WHLeadStatusUpdateDto>();

                    drpStatusStage.DataSource = dt;
                    drpStatusStage.DataValueField = "ID";
                    drpStatusStage.DataTextField = "Name";
                    drpStatusStage.DataBind();
                    drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));

                    drpStatusStage.Items[0].Selected = true;

                    if (hfMailApprover.Value == "Qualify on 8")
                    {
                        drpStatusStage.Items.Remove(drpStatusStage.Items.FindByValue("7"));
                    }

                }
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
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Connection Error I.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    if (!_whContactPersonData.Update(request))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Connection Error U.", "Error!", Toastr.ToastPosition.TopCenter, true);
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

        void BindLeadTypeTransData()
        {
            try
            {
                int count = 0;
                WhLeadTypeTransactionData _whleadTypeTransData = new WhLeadTypeTransactionData();
                WhLeadTypeTransactionDto obj = new WhLeadTypeTransactionDto();

                IList<WhLeadTypeTransactionDto> result = _whleadTypeTransData.GetAllByID(RqId);
                if (result != null)
                {
                    gvLeadTypeTransaction.Visible = true;

                    gvLeadTypeTransaction.DataSource = result;
                    gvLeadTypeTransaction.DataBind();

                    ViewState["LeadTypeCurrentTable"] = result.ToList().ToDataTable<WhLeadTypeTransactionDto>();

                    if (RqId != null)
                    {
                        //if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2")
                        {
                            gvLeadTypeTransaction.Enabled = true;
                            foreach (GridViewRow gvRow in gvLeadTypeTransaction.Rows)
                            {
                                DropDownList drpUOM1 = (DropDownList)gvRow.FindControl("drpUOM");
                                TextBox gvtxtQtNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
                                TextBox gvtxtperunitRevenue = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
                                DropDownList drpBVType = (DropDownList)gvRow.FindControl("drpBVType");
                                // drpUOM1.Enabled = false;
                                if (drpBVType.SelectedValue == string.Empty)
                                {
                                    drpBVType.Enabled = true;
                                }
                                else
                                {
                                    drpBVType.Enabled = false;
                                }
                                if (drpUOM1.SelectedValue == string.Empty)
                                {
                                    drpUOM1.Enabled = true;
                                }
                                else
                                {
                                    drpUOM1.Enabled = false;
                                }
                                if (gvtxtQtNos.Text == string.Empty)
                                {
                                    gvtxtQtNos.Enabled = true;
                                }
                                else
                                {
                                    gvtxtQtNos.Enabled = false;
                                }

                                if (gvtxtperunitRevenue.Text == string.Empty)
                                {
                                    gvtxtperunitRevenue.Enabled = true;

                                }
                                else
                                {
                                    gvtxtperunitRevenue.Enabled = false;
                                }

                            }
                            //gvLeadTypeTransaction.Enabled = false;
                        }
                    }

                }
                else
                {
                    gvLeadTypeTransaction.Enabled = true;
                    FirstGridBusinessVertical();
                }
            }
            catch (Exception ex)
            {

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
                    txtRevenueRange.Text = "Less than 1 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 100000 && txtMothlyBilling.Text.ToNullDouble() <= 500000)
                {
                    txtRevenueRange.Text = " 1-5 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 500000 && txtMothlyBilling.Text.ToNullDouble() <= 1000000)
                {
                    txtRevenueRange.Text = "5-10 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 1000000 && txtMothlyBilling.Text.ToNullDouble() <= 2000000)
                {
                    txtRevenueRange.Text = "10-20 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 2000000 && txtMothlyBilling.Text.ToNullDouble() <= 3000000)
                {
                    txtRevenueRange.Text = "20-30 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 3000000 && txtMothlyBilling.Text.ToNullDouble() <= 4000000)
                {
                    txtRevenueRange.Text = "30-40 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() >= 4000000 && txtMothlyBilling.Text.ToNullDouble() <= 5000000)
                {
                    txtRevenueRange.Text = "40-50 Lakhs";
                }
                else if (txtMothlyBilling.Text.ToNullDouble() > 5000000)
                {
                    txtRevenueRange.Text = "Greater than 50 Lakhs";
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void ChkStage3Approver()
        {
            try
            {
                DataSet ds1 = new DataSet();
                int tableCount = 0;
                WHLeadMasterData _whleadMasterData = new WHLeadMasterData();
                DataSet ds = _whleadMasterData.chkStageApprover(RqId.ToDataConvertInt32());

                ds1 = _whleadMasterData.findFinalApproverAtStage8(RqId.ToDataConvertInt64());



                if (ds == null)
                {
                    hfMailApprover.Value = "NULL";
                }
                else if (ds != null)
                {

                    tableCount = ds.Tables.Count;


                    DataTable dt = new DataTable();
                    if (tableCount == 0 || ds.Tables[1].Rows.Count <= 0)
                    {

                        dt = ds.Tables[0];

                        if (dt.Rows[0]["IsApproved"].ToString() == "Disapproved" && dt.Rows[1]["IsApproved"].ToString() == "Disapproved")
                        {
                            hfMailApprover.Value = "NULL";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() == "Disapproved" && dt.Rows[1]["IsApproved"].ToString() == "Approved")
                        {
                            hfMailApprover.Value = "NULL";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() == "Approved" && dt.Rows[1]["IsApproved"].ToString() == "Disapproved")
                        {
                            hfMailApprover.Value = "NULL";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() == string.Empty && dt.Rows[1]["IsApproved"].ToString() == string.Empty)
                        {
                            hfMailApprover.Value = "Pending";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() != string.Empty && dt.Rows[1]["IsApproved"].ToString() == string.Empty)
                        {
                            hfMailApprover.Value = "Pending";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() == string.Empty && dt.Rows[1]["IsApproved"].ToString() != string.Empty)
                        {
                            hfMailApprover.Value = "Pending";
                        }
                        if (dt.Rows[0]["IsApproved"].ToString() == "Approved" && dt.Rows[1]["IsApproved"].ToString() == "Approved")
                        {
                            hfMailApprover.Value = "Qualify on 3";
                        }


                    }
                    if (tableCount == 2 && hfMailApprover.Value == string.Empty)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            dt = ds.Tables[1];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["IsApproved"].ToString() == string.Empty)
                                {
                                    hfMailApprover.Value = "Pending on 5";
                                    return;
                                }
                                if (dt.Rows[i]["IsApproved"].ToString() == "Disapproved")
                                {
                                    hfMailApprover.Value = "Qualify on 3";
                                    return;
                                }
                            }
                            if (hfMailApprover.Value == string.Empty && ds1 == null)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["IsApproved"].ToString() == "Approved")
                                    {
                                        hfMailApprover.Value = "Qualify on 5";
                                    }
                                }
                            }

                            if (hfMailApprover.Value == string.Empty && ds1 != null)
                            {
                                dt = ds1.Tables[0];
                                if (dt.Rows.Count > 1)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i]["IsApproved"].ToString() == "Disapproved")
                                        {

                                            hfMailApprover.Value = "send mail";
                                            return;
                                        }

                                    }
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i]["IsApproved"].ToString() == string.Empty)
                                        {
                                            hfMailApprover.Value = "Pending on 8";
                                            return;

                                        }
                                        else
                                        {
                                            hfMailApprover.Value = "Qualify on 8";

                                        }
                                    }
                                }
                                else
                                {
                                    hfMailApprover.Value = "Pending on 8";

                                }
                            }



                        }
                        else
                        {
                            hfMailApprover.Value = "Qualify on 3";
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        void saveCreditData()
        {
            WhCreditNoteDto request = new WhCreditNoteDto();
            WhCreditNoteData _whCreditNoteData = new WhCreditNoteData();

            request.ID = hfCreditNoteID.Value.ToLong();
            request.WhLeadID = RqId.ToLong();
            request.CustomerID = drpCustomerName.SelectedValue.ToNullLong();
            request.CreditDays = txtCreditDays.Text.ToDouble();
            request.CreditLimit = txtCreditLimit.Text.ToDouble();
            request.CreditRating = txtCreditRating.Text.ToDouble();
            request.CreateBy = LovelySession.Lovely.User.Id;
            request.CreditFileName = getCreditFileName();

            if (request.ID == 0)
            {
                long returnID = _whCreditNoteData.Insert(request);
                if (returnID > 0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    //btnCreditUpload.Visible = false;
                    //btnBackToList.Focus();
                }

                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                if (_whCreditNoteData.Update(request))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                    //btnCreditUpload.Visible = false;
                    //btnBackToList.Focus();
                }
                else
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record not updated", "Error!", Toastr.ToastPosition.TopCenter, true);
            }

            WhCreditNoteData _creditNoteData = new WhCreditNoteData();
            WhCreditNoteDto creditResult = _creditNoteData.getbyWhleadIDAndCustomerID(drpCustomerName.SelectedValue, RqId.ToLong());
            if (creditResult != null)
            {
                hfCreditNoteID.Value = creditResult.ID.ToString();
                txtCreditDays.Text = creditResult.CreditDays.ToString();
                txtCreditLimit.Text = creditResult.CreditLimit.ToString();
                txtCreditRating.Text = creditResult.CreditRating.ToString();


            }
        }

        string getCreditFileName()
        {
            string str = null;
            string fileExt = string.Empty;
            string sSavePath;

            sSavePath = "CreditNoteFiles/";
            // Check file size (mustn’t be 0)
            HttpPostedFile myFile = CreditUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                return string.Empty;
            }


            ////					// Check file extension (must be JPG)
            string extName = System.IO.Path.GetExtension(myFile.FileName).ToLower();
            str = myFile.FileName;

            switch (extName) // this switch code validate the files which allow to upload only PDF file   
            {
                case ".pdf":
                    fileExt = "application/pdf";
                    break;
                //case ".xls":
                //    fileExt = "application/xls";
                //    break;
                //case ".xlsx":
                //    fileExt = "application/xlsx";
                //    break;
                //case ".png":
                //    fileExt = "application/png";
                //    break;
                //case ".jpg":
                //    fileExt = "application/png";
                //    break;
                //case ".jpeg":
                //    fileExt = "application/jpeg";
                //    break;
                //case ".doc":
                //    fileExt = "application/doc";
                //    break;
                //case ".docx":
                //    fileExt = "application/docx";
                //    break;
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
                string path = Server.MapPath(appPath + "FrontEnd/Operations/CreditNoteFiles/" + strFileName);
                // Save the stream to disk
                System.IO.FileStream newFile = new FileStream(path, FileMode.Create); // new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();


                return strFileName;
            }
            else
            {
                return string.Empty;
            }


        }

        void EnableFalse()
        {
            txtLeadSource.Enabled = false;
            drpCustomerName.Enabled = false;
            drpLineOfBusiness.Enabled = false;
            drpBU.Enabled = false;
            txtProjectEta.Enabled = false;
            txtProjectATA.Enabled = false;
            drpDesignatedBD.Enabled = false;
            drpDesignatedSolution.Enabled = false;
            drpStage.Enabled = false;
            drpStatusStage.Enabled = false;
            drpNewEncirclement.Enabled = false;
            txtMothlyBilling.Enabled = false;
            txtGP.Enabled = false;
            txtOppBrief.Enabled = false;
            drpRegion.Enabled = false;
            txtRevenue.Enabled = false;
            drpSegment.Enabled = false;
            txtSegment.Enabled = false;
            drpPricingType.Enabled = false;
            drpContractType.Enabled = false;
            drpLeadType.Enabled = false;
            drpItSystem.Enabled = false;
            txtSystemName.Enabled = false;
            drpSystemName.Enabled = false;
            fpDocument.Enabled = false;
            drpPostNegotitationStage.Enabled = false;
            txtCompetitorName.Enabled = false;
            txtReason.Enabled = false;
            drpCancelled.Enabled = false;
            gvLeadTypeTransaction.Enabled = false;
            gvStatusUpdate.Enabled = false;
            gvContactPersonDetails.Enabled = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            btnsendMail.Visible = false;


        }

        void updateLeadDetailsOnRequest()
        {
            WHLeadMasterDto request = new WHLeadMasterDto();
            request.ID = RqId.ToLong();
            request.ProjectETA = txtProjectEta.Text.ToConvertNullDateTime();
            request.ContractType = drpContractType.SelectedValue;
            request.MonthlyBilling = txtMothlyBilling.Text.ToDouble();
            request.GP = txtGP.Text.ToDouble();

            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            if (request.ID != null)
            {
                _whLeadMasterData.UpdateForApprovalRequest(request);
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
                    SaveLeadTypeTransaction();
                    // bindDrp();
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    if (drpStage.SelectedValue != string.Empty)
                    {
                        sendMailForLeadClose();
                    }
                    clear();
                    FirstGridViewRow();
                    FirstGridViewRowForContact();
                    drpStatusStage.Enabled = false;

                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                if (_leadData.Update(request))
                {
                    saveStatusGridData();
                    saveContactPersonGridData();
                    if (drpStatusStage.SelectedValue == "5" && hfExp1.NavigateUrl == null)
                    {
                        Upload();
                    }
                    SaveLeadTypeTransaction();
                    // bindDrp();
                    // setData();
                    btnSubmit.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                    if (txtProjectEta.Text != string.Empty)
                    {
                        txtProjectEta.Enabled = false;
                    }
                    drpStatusStage.Enabled = false;
                    if (drpStage.SelectedValue != string.Empty)
                    {
                        sendMailForLeadClose();
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");

                }
            }
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

        private void FirstGridViewRowLeadType()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("LeadID", typeof(string)));
            dt.Columns.Add(new DataColumn("LeadType", typeof(string)));
            dt.Columns.Add(new DataColumn("UOM", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty_Nos", typeof(string)));
            dt.Columns.Add(new DataColumn("PerUnitRevenue", typeof(string)));

            if (drpLineOfBusiness.SelectedItem.Text != "Transportation+Warehousing")
            {

                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["ID"] = string.Empty;
                if (RqId != null)
                    dr["LeadID"] = RqId.ToString();
                else
                    dr["LeadID"] = string.Empty;
                dr["LeadType"] = drpLineOfBusiness.SelectedItem.Text;
                dr["UOM"] = string.Empty;
                dr["Qty_Nos"] = string.Empty;
                dr["PerUnitRevenue"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["LeadTypeCurrentTable"] = dt;
                gvLeadTypeTransaction.DataSource = dt;
                gvLeadTypeTransaction.DataBind();
            }
            else
            {
                for (int i = 1; i <= 2; i++)
                {

                    dr = dt.NewRow();
                    if (i == 1)
                    {
                        dr["RowNumber"] = 1;
                    }
                    else
                    {
                        dr["RowNumber"] = 2;
                    }

                    dr["ID"] = string.Empty;
                    if (RqId != null)
                        dr["LeadID"] = RqId.ToString();
                    else
                        dr["LeadID"] = string.Empty;
                    if (i == 1)
                    {
                        dr["LeadType"] = "Warehousing";
                    }
                    else
                    {
                        dr["LeadType"] = "Transportation";
                    }


                    dr["UOM"] = string.Empty;
                    dr["Qty_Nos"] = string.Empty;
                    dr["PerUnitRevenue"] = string.Empty;
                    dt.Rows.Add(dr);

                }
                ViewState["LeadTypeCurrentTable"] = dt;
                gvLeadTypeTransaction.DataSource = dt;
                gvLeadTypeTransaction.DataBind();
            }
        }


        #endregion

        #region Business Vertical grid

       
        private void FirstGridBusinessVertical()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("LeadID", typeof(string)));
            dt.Columns.Add(new DataColumn("LeadType", typeof(string)));
            dt.Columns.Add(new DataColumn("UOM", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty_Nos", typeof(string)));
            dt.Columns.Add(new DataColumn("PerUnitRevenue", typeof(string)));
            dr = dt.NewRow();

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["ID"] = string.Empty;
            dr["LeadID"] = string.Empty;
            dr["LeadType"] = string.Empty;
            dr["UOM"] = string.Empty;
            dr["Qty_Nos"] = string.Empty;
            dr["PerUnitRevenue"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["LeadTypeCurrentTable"] = dt;
            gvLeadTypeTransaction.DataSource = dt;
            gvLeadTypeTransaction.DataBind();
        }

        private void AddNewRowBusinessVertical()
        {
            int rowIndex = 0;

            if (ViewState["LeadTypeCurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["LeadTypeCurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvLeadTypeTransaction.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        Label lblLeadID = (Label)gvLeadTypeTransaction.Rows[rowIndex].Cells[1].FindControl("lblLeadID");
                        DropDownList drpBVType = (DropDownList)gvLeadTypeTransaction.Rows[rowIndex].Cells[2].FindControl("drpBVType");
                        DropDownList drpUOM = (DropDownList)gvLeadTypeTransaction.Rows[rowIndex].Cells[3].FindControl("drpUOM");
                        TextBox gvtxtQty_Nos = (TextBox)gvLeadTypeTransaction.Rows[rowIndex].Cells[4].FindControl("gvtxtQty_Nos");
                        TextBox gvtxtPerUnitRevenue = (TextBox)gvLeadTypeTransaction.Rows[rowIndex].Cells[5].FindControl("gvtxtPerUnitRevenue");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ID"] = gvlblID.Text;
                        dtCurrentTable.Rows[i - 1]["LeadID"] = lblLeadID.Text;
                        dtCurrentTable.Rows[i - 1]["LeadType"] = drpBVType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["UOM"] = drpUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Qty_Nos"] = gvtxtQty_Nos.Text;
                        dtCurrentTable.Rows[i - 1]["PerUnitRevenue"] = gvtxtPerUnitRevenue.Text;

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["LeadTypeCurrentTable"] = dtCurrentTable;

                    gvLeadTypeTransaction.DataSource = dtCurrentTable;
                    gvLeadTypeTransaction.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousBusinessVertical();
        }

        private void SetPreviousBusinessVertical()
        {
            int rowIndex = 0;
            if (ViewState["LeadTypeCurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadTypeCurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvLeadTypeTransaction.Rows[rowIndex].Cells[0].FindControl("gvlblID");
                        Label lblLeadID = (Label)gvLeadTypeTransaction.Rows[rowIndex].Cells[1].FindControl("lblLeadID");
                        DropDownList drpBVType = (DropDownList)gvLeadTypeTransaction.Rows[rowIndex].Cells[2].FindControl("drpBVType");
                        DropDownList drpUOM = (DropDownList)gvLeadTypeTransaction.Rows[rowIndex].Cells[3].FindControl("drpUOM");
                        TextBox gvtxtQty_Nos = (TextBox)gvLeadTypeTransaction.Rows[rowIndex].Cells[4].FindControl("gvtxtQty_Nos");
                        TextBox gvtxtPerUnitRevenue = (TextBox)gvLeadTypeTransaction.Rows[rowIndex].Cells[5].FindControl("gvtxtPerUnitRevenue");

                        gvlblID.Text = dt.Rows[i]["ID"].ToString();
                        lblLeadID.Text = dt.Rows[i]["LeadID"].ToString();
                        drpBVType.SelectedValue = dt.Rows[i]["LeadType"].ToString();
                        drpUOM.SelectedValue = dt.Rows[i]["UOM"].ToString();
                        gvtxtQty_Nos.Text = dt.Rows[i]["Qty_Nos"].ToString();
                        gvtxtPerUnitRevenue.Text = dt.Rows[i]["PerUnitRevenue"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRowBusinessVertical();
        }
        protected void drpBVType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)drp.Parent.Parent;
            DropDownList gvdrpUom = (DropDownList)gvRow.FindControl("drpUOM");
            if (drp.SelectedValue == "Warehousing")
            {
                gvdrpUom.Items.Insert(0, new ListItem("--Select--", string.Empty));
                gvdrpUom.Items.Insert(1, new ListItem("Unit", "Unit"));
                gvdrpUom.Items.Insert(2, new ListItem("Sqft", "Sqft"));
                gvdrpUom.Items.Insert(3, new ListItem("Pallet", "Pallet"));
                gvdrpUom.Items.Insert(4, new ListItem("Throughput", "Throughput"));
            }
            else if (drp.SelectedValue == "Transportation")
            {
                gvdrpUom.Items.Insert(0, new ListItem("--Select--", string.Empty));
                gvdrpUom.Items.Insert(1, new ListItem("Kg", "Kg"));
                //gvdrpUom.Items.Insert(2, new ListItem("Unit", "Unit"));
                gvdrpUom.Items.Insert(2, new ListItem("Carton", "Carton"));
                gvdrpUom.Items.Insert(3, new ListItem("Trip", "Trip"));
                gvdrpUom.Items.Insert(4, new ListItem("Vehicle", "Vehicle"));
            }

        }
        protected void gvLeadTypeTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label gvlblID = (e.Row.FindControl("lblLeadType") as Label);
                Label gvdrpUOM = (e.Row.FindControl("gvtxtUOM") as Label);
                DropDownList drp = (e.Row.FindControl("drpUOM") as DropDownList);
                DropDownList drpBVType = (e.Row.FindControl("drpBVType") as DropDownList);

                Label gvlblLeadTypeID = (e.Row.FindControl("gvlblID") as Label);
                LinkButton lnkRemove = (e.Row.FindControl("lnkRemove") as LinkButton);
                if(gvlblLeadTypeID.Text!=string.Empty)
                {
                    lnkRemove.Visible = false;
                    drpBVType.Enabled = false;
                    drp.Enabled = false;
                }


                if (gvlblID.Text == "Warehousing")
                {
                    drp.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drp.Items.Insert(1, new ListItem("Unit", "Unit"));
                    drp.Items.Insert(2, new ListItem("Sqft", "Sqft"));
                    drp.Items.Insert(3, new ListItem("Pallet", "Pallet"));
                    drp.Items.Insert(4, new ListItem("Throughput", "Throughput"));
                }
                else if (gvlblID.Text == "Transportation")
                {
                    drp.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drp.Items.Insert(1, new ListItem("Kg", "Kg"));
                    //drp.Items.Insert(2, new ListItem("Unit", "Unit"));
                    drp.Items.Insert(2, new ListItem("Carton", "Carton"));
                    drp.Items.Insert(3, new ListItem("Trip", "Trip"));
                    drp.Items.Insert(4, new ListItem("Vehicle", "Vehicle"));
                }
                else if (gvlblID.Text == "CFS")
                {
                    drp.Items.Insert(0, new ListItem("--Select--", string.Empty));
                    drp.Items.Insert(1, new ListItem("Unit", "Unit"));
                    drp.Items.Insert(2, new ListItem("Sqft", "Sqft"));
                    drp.Items.Insert(3, new ListItem("Pallet", "Pallet"));
                    drp.Items.Insert(4, new ListItem("Throughput", "Throughput"));
                }
                if (gvdrpUOM.Text != string.Empty)
                {
                    drp.SelectedValue = gvdrpUOM.Text.ToString();
                }
                if (gvlblID.Text != string.Empty)
                {
                    drpBVType.SelectedValue = gvlblID.Text.ToString();
                }
                TextBox txtQtyNos = (TextBox)e.Row.FindControl("gvtxtQty_Nos");
                TextBox txtRevenu = (e.Row.FindControl("gvtxtPerUnitRevenue") as TextBox);
                TextBox lblMonthly = (e.Row.FindControl("gvtxtMonthlyBilling") as TextBox);
                if (txtQtyNos.Text != string.Empty && txtRevenu.Text != string.Empty)
                {
                    double? Qty = txtQtyNos.Text.ToNullDouble();
                    double? Revenu = txtRevenu.Text.ToNullDouble();
                    lblMonthly.Text = (Qty * Revenu).ToString();
                }

            }
        }
        protected void gvLeadTypeTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            double monthlybilling = 0;
            if (e.CommandName == "Remove")
            {
                LinkButton btn = (LinkButton)e.CommandSource;
                if (btn != null)
                {
                    GridViewRow grdrow = (GridViewRow)btn.NamingContainer;
                    int row = grdrow.RowIndex;
                    //  deleteRow();
                    DataTable dtt = (DataTable)ViewState["LeadTypeCurrentTable"];

                    if (dtt.Rows.Count > 1)
                    {
                        dtt.Rows.RemoveAt(row);
                        dtt.AcceptChanges();
                    }

                    gvLeadTypeTransaction.DataSource = dtt;
                    gvLeadTypeTransaction.DataBind();
                    SetPreviousBusinessVertical();
                }
                foreach(GridViewRow gvRow in gvLeadTypeTransaction.Rows)
                {
                    TextBox gvtxtMonthlyBilling = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");

                    monthlybilling = (monthlybilling + gvtxtMonthlyBilling.Text.ToDouble());
                }
                txtMothlyBilling.Text = monthlybilling.ToString();
                RevenueRange();
                
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i <= NumCells - 1; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:center !important");

                }
            }
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
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please fill out all the contact person details.", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            AddNewRowForContact();
        }
        #endregion

        protected void drpStatusStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpStage.SelectedValue = string.Empty;
            FieldVisibility();
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2" || drpStatusStage.SelectedValue == string.Empty)
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
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2" || drpStatusStage.SelectedValue == "3" || drpStatusStage.SelectedValue == string.Empty)
            {
                lblDSStar.Visible = false;
            }
            else
            {
                lblDSStar.Visible = true;
            }
            if (drpStatusStage.SelectedValue == "3")
            {
                Visibility();
            }

            if (drpStatusStage.SelectedValue == "5")
            {
                Visibility();
                Label14.Text = "Upload Draft Contract:";
            }
            if (drpStatusStage.SelectedValue == "8")
            {
                Visibility();
                divNegotitation.Visible = true;
                txtCompetitorName.Visible = true;
                txtReason.Visible = true;

                Label14.Text = "Upload Signed Contract:";
            }
            else
            {
                divNegotitation.Visible = false;
                txtCompetitorName.Visible = false;
                txtReason.Visible = false;
            }
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2" || drpStatusStage.SelectedValue == "4")
            {
                btnsendMail.Visible = false;
                btnSubmit.Visible = true;
            }

            if (drpStatusStage.SelectedValue == "3" || drpStatusStage.SelectedValue == "4" || drpStatusStage.SelectedValue == "5" || drpStatusStage.SelectedValue == "6"
                 || drpStatusStage.SelectedValue == "7" || drpStatusStage.SelectedValue == "8")
            {
                Label7.Visible = true;
            }
            else
            {
                Label7.Visible = false;
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
                txtSegment.Text = "Other";
                // txtSegment.Enabled = true;
                txtSegment.Focus();
            }
            txtSegment.Enabled = false;
        }

        protected void txtMothlyBilling_TextChanged(object sender, EventArgs e)
        {
            RevenueRange();
        }

        protected void drpLeadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpUOM.Items.Clear();
            txtQty.Visible = false;
            txtRevenue.Visible = false;
            lblQty.Visible = false;
            lblRevenue.Visible = false;
            lblQtyStar.Visible = false;
            lblRevenueStar.Visible = false;
            txtRevenue.Text = string.Empty;
            txtQty.Text = string.Empty;
           // FirstGridViewRowLeadType();
            FirstGridBusinessVertical();
            //  drpUOM.Items.Insert(0, new ListItem("--Select--", string.Empty));

            if (drpLineOfBusiness.SelectedValue == "Warehousing")
            {
                drpUOM.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpUOM.Items.Insert(1, new ListItem("Unit", "Unit"));
                drpUOM.Items.Insert(2, new ListItem("Sqft", "Sqft"));
                drpUOM.Items.Insert(3, new ListItem("Pallet", "Pallet"));
                drpUOM.Items.Insert(4, new ListItem("Throughput", "Throughput"));
            }
            else if (drpLineOfBusiness.SelectedValue == "Transportation")
            {
                drpUOM.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpUOM.Items.Insert(1, new ListItem("Kg", "Kg"));
                drpUOM.Items.Insert(2, new ListItem("Unit", "Unit"));
                drpUOM.Items.Insert(3, new ListItem("Carton", "Carton"));
                drpUOM.Items.Insert(4, new ListItem("Trip", "Trip"));
                drpUOM.Items.Insert(5, new ListItem("Vehicle", "Vehicle"));

            }


        }

        protected void drpUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUOM.SelectedValue == string.Empty)
            {
                txtQty.Visible = false;
                txtRevenue.Visible = false;
                lblQty.Visible = false;
                lblRevenue.Visible = false;
                lblQtyStar.Visible = false;
                lblRevenueStar.Visible = false;
                txtRevenue.Text = string.Empty;
                txtQty.Text = string.Empty;
            }
            else
            {
                txtQty.Visible = true;
                txtRevenue.Visible = true;
                lblQty.Visible = true;
                lblRevenue.Visible = true;
                lblQtyStar.Visible = true;
                lblRevenueStar.Visible = true;
                txtRevenue.Text = string.Empty;
                txtQty.Text = string.Empty;
            }
        }

        #region MailFunction
        string mTo = "";
        string mCC = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        string mail_body_format = "", mailsql = "";
        bool sendMailStage3()
        {
            try
            {

                string subject = "Opportunity Qualification Approval-" + drpCustomerName.SelectedItem.Text;
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                DataSet ds = _whleadmaster.getUserID(RqId);
                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "UserID not found", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi,<br/><br/>";
                textBody = textBody + drpDesignatedBD.SelectedItem.Text.ToUpper() + " has requested the movement of the following lead to <b>Opportunity Qualified.</b> Please click the link below to approve or reject. <br/><br/>";
                textBody = textBody + "<br/><table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + drpCustomerName.SelectedItem.Text.ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + drpNewEncirclement.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + drpLineOfBusiness.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding:</b><hr>" + txtProjectEta.Text.ToUpper() + "</td><td> <b>Region:</b> <hr>" + drpRegion.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + drpSegment.SelectedItem.Text.ToUpper() + "</td></tr>";
                foreach (GridViewRow gvRow1 in gvLeadTypeTransaction.Rows)
                {
                    TextBox lblMonthlySum = (TextBox)gvRow1.FindControl("gvtxtMonthlyBilling");
                    Label gvlblleadType = (Label)gvRow1.FindControl("lblLeadType");
                    TextBox txtQtyNos = (TextBox)gvRow1.FindControl("gvtxtQty_Nos");
                    TextBox txtRevenu = (TextBox)gvRow1.FindControl("gvtxtPerUnitRevenue");
                    DropDownList drp = (DropDownList)gvRow1.FindControl("drpUOM");

                    textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + gvlblleadType.Text.ToUpper() + "</td><td> <b>UOM:</b><hr>" + drp.SelectedItem.Text.ToUpper() + "</td><td> <b>Qty:</b> <hr>" + txtQtyNos.Text.ToUpper() + "</td><td> <b>Per Unit Revenue(In Lakhs): </b><hr>" + txtRevenu.Text + "</td></tr>";
                }
                textBody = textBody + "<tr> <td colspan='2'> <b>Projected Monthly Billing(In Lakhs):</b><hr>" + txtMothlyBilling.Text.ToUpper() + "</td><td colspan='2'> <b>Projected GP(%):</b> <hr>" + txtGP.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + drpPricingType.SelectedItem.Text.ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + drpContractType.SelectedItem.Text.ToUpper() + "</td></tr>";
                if (drpItSystem.SelectedItem.Text == "Customer System")
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + txtSystemName.Text.ToUpper() + "</td></tr>";
                }
                else
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + drpSystemName.SelectedItem.Text.ToUpper() + "</td></tr>";
                }
                textBody = textBody + "</table> <br/> <br/>";

                textBody = textBody + "Kindly Approve/Reject by Clicking the link below";

                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + " &nbsp;&nbsp;&nbsp; <a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px;' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";

                // textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://localhost:62150/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                // textBody = textBody + " &nbsp;&nbsp;&nbsp; <a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px;' href =http://localhost:62150/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";

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

        public void InsertintoMailApprover(string LEadID, string ApproverID, string Status)
        {
            try
            {

                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhMailApprovalDto _mailApprovalDto = new WhMailApprovalDto();
                _mailApprovalDto.WhLeadID = LEadID.ToNullLong();
                _mailApprovalDto.ApproverId = ApproverID.ToNullLong();
                _mailApprovalDto.StatusStage = Status.ToNullLong();
                _mailApprovalDto.CreateBy = LovelySession.Lovely.User.Id.ToString();
                _mailApprovalDto.DocName = strFileName;

                WHLeadID = _leadData.InsertMailApprover(_mailApprovalDto);
                if (WHLeadID > 0)
                {
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void InsertintoMailApprover8(string LEadID, string ApproverID, string Status)
        {
            try
            {

                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhMailApprovalDto _mailApprovalDto = new WhMailApprovalDto();
                _mailApprovalDto.WhLeadID = LEadID.ToNullLong();
                _mailApprovalDto.ApproverId = ApproverID.ToNullLong();
                _mailApprovalDto.StatusStage = Status.ToNullLong();
                _mailApprovalDto.DocName = strFileName;
                WHLeadID = _leadData.InsertMailApproverForStage8(_mailApprovalDto);
                if (WHLeadID > 0)
                {
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public bool GetApproverIDStage3()
        {
            try
            {
                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhApproverMasterDTo _mailApprovalMasterDto = new WhApproverMasterDTo();
                IList<WhApproverMasterDTo> result = _leadData.GetMailApproverMailIDData(drpBU.SelectedValue, drpStatusStage.SelectedValue, RqId.ToString());
                if (result != null)
                {
                    DataTable dt = result.ToList().ToDataTable<WhApproverMasterDTo>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hfMailApproverMailID.Value = dt.Rows[i]["EmailID"].ToString();
                        hfMailApproverID.Value = dt.Rows[i]["ID"].ToString();

                        InsertintoMailApprover(RqId.ToString(), hfMailApproverID.Value.ToString(), drpStatusStage.SelectedValue);
                        if (!sendMailStage3())
                        {
                            return false;
                        }
                    }

                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
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
        protected void btnsendMail_Click(object sender, EventArgs e)
        {
            if (!approvalValidation())
            {
                return;
            }
            drpStatusStage.Enabled = false;
           
            WhLeadTypeTransactionData _data = new WhLeadTypeTransactionData();
            IList<WhLeadTypeTransactionDto> result = _data.getbyWhLeadID(RqId.ToString());
            if(result==null)
            {
                SaveLeadTypeTransaction();
            }
            if (drpBU.SelectedValue != string.Empty && drpStatusStage.SelectedValue != string.Empty && drpStatusStage.SelectedValue == "3")
            {
                if (!GetApproverIDStage3())
                {
                    return;
                }
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Approval request  has been Successfully sent", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnsendMail.Visible = false;
                updateLeadDetailsOnRequest();
            }

            if (drpBU.SelectedValue != string.Empty && drpStatusStage.SelectedValue != string.Empty && drpStatusStage.SelectedValue == "5")
            {
                if (!Upload())
                {
                    return;
                }
                if (!GetApproverIDStage5())
                {
                    return;
                }
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Approval request  has been Successfully sent", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnsendMail.Visible = false;
            }
            if (drpBU.SelectedValue != string.Empty && drpStatusStage.SelectedValue != string.Empty && drpStatusStage.SelectedValue == "8")
            {
                if (!Upload())
                {
                    return;
                }
                if (!GetApproverIDStage8())
                {
                    return;
                }
                Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Approval request  has been Successfully sent", "Success!", Toastr.ToastPosition.TopCenter, true);
                btnsendMail.Visible = false;
            }


        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != string.Empty || txtRevenue.Text != string.Empty)
            {
                double? Qty = txtQty.Text.ToNullDouble();
                double? Revenu = txtRevenue.Text.ToNullDouble();
                txtMothlyBilling.Text = (Qty * Revenu).ToString();
            }
            else
            {
                txtMothlyBilling.Text = string.Empty;
            }
        }

        protected void txtRevenue_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != string.Empty || txtRevenue.Text != string.Empty)
            {
                double? Qty = txtQty.Text.ToNullDouble();
                double? Revenu = txtRevenue.Text.ToNullDouble();

                txtMothlyBilling.Text = (Qty * Revenu).ToString();
            }
            else
            {
                txtMothlyBilling.Text = string.Empty;
            }
        }

        public void Visibility()
        {
            try
            {
                if (hfMailApprover.Value == "NULL")
                {
                    btnsendMail.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (hfMailApprover.Value == "Pending")
                {

                    btnsendMail.Visible = true;
                    btnsendMail.Text = "Approval request  already raised  ";
                    btnsendMail.Enabled = false;
                    btnSubmit.Visible = false;
                }
                else if (hfMailApprover.Value == "Qualify on 3" && drpStatusStage.SelectedValue == "5")
                {
                    btnsendMail.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (hfMailApprover.Value == "Qualify on 3")
                {

                    btnsendMail.Visible = false;
                    btnSubmit.Visible = true;
                }
                else if (hfMailApprover.Value == "Qualify on 5" && drpStatusStage.SelectedValue == "5")
                {

                    btnsendMail.Visible = false;
                    btnSubmit.Visible = true;
                    GetFile();
                }
                else if (hfMailApprover.Value == "Qualify on 5" && drpStatusStage.SelectedValue == "8")
                {
                    btnsendMail.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (hfMailApprover.Value == "send mail")
                {

                    btnsendMail.Visible = true;
                    btnSubmit.Visible = false;
                }
                else if (hfMailApprover.Value == "Pending on 8")
                {

                    btnsendMail.Visible = true;
                    btnsendMail.Text = "Approval request  already raised  ";
                    btnsendMail.Enabled = false;
                    btnSubmit.Visible = false;
                }

                else if (hfMailApprover.Value == "Qualify on 8" && drpStatusStage.SelectedValue == "8")
                {

                    btnsendMail.Visible = false;
                    btnSubmit.Visible = true;
                    GetFile();
                }
                if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2")
                {
                    btnsendMail.Visible = false;
                    btnSubmit.Visible = true;
                }


            }
            catch (Exception ex)
            {

            }
        }

        public void FieldVisibility()
        {
            try
            {
                if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2"
                    || drpStatusStage.SelectedValue == "3" || drpStatusStage.SelectedValue == "4")
                {
                    fpDocument.Visible = false;
                    Label14.Visible = false;
                }
                else
                {
                    fpDocument.Visible = true;
                    Label14.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
       

        public bool SaveLeadTypeTransaction()
        {
            try
            {
                foreach (GridViewRow gvRow in gvLeadTypeTransaction.Rows)
                {

                    Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                    Label gvlblleadType = (Label)gvRow.FindControl("lblLeadType");
                    TextBox txtQtyNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
                    TextBox txtRevenu = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
                    TextBox lblMonthlySum = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");
                    DropDownList drp = (DropDownList)gvRow.FindControl("drpUOM");
                    DropDownList drpBVType = (DropDownList)gvRow.FindControl("drpBVType");

                    WhLeadTypeTransactionData _whleadTypeTransData = new WhLeadTypeTransactionData();
                    WhLeadTypeTransactionDto obj = new WhLeadTypeTransactionDto();
                    obj.ID = gvlblID.Text.ToNullLong();
                    if (RqId == null)
                        obj.LeadID = WHLeadID.ToString();
                    else
                        obj.LeadID = RqId.ToString();
                    obj.LeadType = drpBVType.SelectedValue;
                    obj.Qty_Nos = txtQtyNos.Text.ToNullDouble();
                    obj.PerUnitRevenue = txtRevenu.Text.ToNullDouble();
                    obj.UOM = drp.SelectedItem.Text;

                    if (obj != null)
                    {
                        if (obj.LeadType != string.Empty && obj.UOM != string.Empty)
                        {
                            if (obj.ID == null)
                            {

                                if (_whleadTypeTransData.Insert(obj))
                                {

                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {

                                if (_whleadTypeTransData.Update(obj))
                                {

                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        protected void gvtxtQty_Nos_TextChanged(object sender, EventArgs e)
        {
            TextBox lnk = (TextBox)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;


            TextBox txtQtyNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
            TextBox txtRevenu = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
            TextBox lblMonthly = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");

            if (txtQtyNos.Text != string.Empty && txtRevenu.Text != string.Empty)
            {
                double? Qty = txtQtyNos.Text.ToNullDouble();
                double? Revenu = txtRevenu.Text.ToNullDouble();
                lblMonthly.Text = (Qty * Revenu).ToString();

            }
            else
            {
                lblMonthly.Text = string.Empty;
            }
            txtRevenu.Focus();
            foreach (GridViewRow gvRow1 in gvLeadTypeTransaction.Rows)
            {
                TextBox lblMonthlySum = (TextBox)gvRow1.FindControl("gvtxtMonthlyBilling");
                if (lblMonthlySum.Text != string.Empty)
                {
                    totMonthlyBilling = (totMonthlyBilling + (lblMonthlySum.Text.ToDouble()));
                }
            }
            txtMothlyBilling.Text = totMonthlyBilling.ToString();
            RevenueRange();
        }

        protected void gvtxtPerUnitRevenue_TextChanged(object sender, EventArgs e)
        {
            TextBox lnk = (TextBox)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;


            TextBox txtQtyNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
            TextBox txtRevenu = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
            TextBox lblMonthly = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");
            if (txtQtyNos.Text != string.Empty && txtRevenu.Text != string.Empty)
            {
                double? Qty = txtQtyNos.Text.ToNullDouble();
                double? Revenu = txtRevenu.Text.ToNullDouble();
                lblMonthly.Text = (Qty * Revenu).ToString();
            }
            else
            {
                lblMonthly.Text = string.Empty;
            }

            foreach (GridViewRow gvRow1 in gvLeadTypeTransaction.Rows)
            {
                TextBox lblMonthlySum = (TextBox)gvRow1.FindControl("gvtxtMonthlyBilling");
                if (lblMonthlySum.Text != string.Empty)
                {
                    totMonthlyBilling = (totMonthlyBilling + (lblMonthlySum.Text.ToDouble()));
                }
            }
            txtMothlyBilling.Text = totMonthlyBilling.ToString();
            RevenueRange();
        }

        protected void drpUOM_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList lnk = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)lnk.Parent.Parent;

            DropDownList drp = (DropDownList)gvRow.FindControl("drpUOM");

            TextBox txtQtyNos = (TextBox)gvRow.FindControl("gvtxtQty_Nos");
            TextBox txtRevenu = (TextBox)gvRow.FindControl("gvtxtPerUnitRevenue");
            TextBox lblMonthly = (TextBox)gvRow.FindControl("gvtxtMonthlyBilling");
            if (drp.SelectedValue == string.Empty)
            {
                txtQtyNos.Text = string.Empty;
                txtQtyNos.Enabled = false;
                txtRevenu.Text = string.Empty;
                txtRevenu.Enabled = false;
                lblMonthly.Text = string.Empty;
                txtMothlyBilling.Text = string.Empty;
            }
            else
            {
                txtQtyNos.Enabled = true;
                drp.Enabled = true;
                txtRevenu.Enabled = true;
            }
        }

        protected void drpItSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpItSystem.SelectedValue == string.Empty)
            {
                lblStarSystemName.Visible = false;
                lblItSystem.Visible = false;
                txtSystemName.Visible = false;
                drpSystemName.Visible = false;

                txtSystemName.Text = string.Empty;
                drpSystemName.SelectedValue = string.Empty;
            }
            else if (drpItSystem.SelectedValue == "Customer System")
            {
                lblStarSystemName.Visible = true;
                lblItSystem.Visible = true;
                txtSystemName.Visible = true;
                drpSystemName.Visible = false;

                drpSystemName.SelectedValue = string.Empty;
            }
            else if (drpItSystem.SelectedValue == "ALS System")
            {
                lblStarSystemName.Visible = true;
                lblItSystem.Visible = true;
                txtSystemName.Visible = false;
                drpSystemName.Visible = true;

                txtSystemName.Text = string.Empty;
            }
        }




        #region FileUpload
        bool Upload()
        {
            string str = null;
            string fileExt = string.Empty;
            HttpPostedFile myFile = fpDocument.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File has no data and upload the file again!!", "Error!", Toastr.ToastPosition.TopCenter, true);

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
                string _uniqueId = _dd + _hh + _mm + _min + _ss + _yy;
                string name = str.Replace(" ", "_");
                // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an incremental numeric until it is unique
                //sFilename = System.IO.Path.GetFileName(myFile.FileName);
                //  strFileName = RqId+LovelySession.Lovely.User.Name + "_" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();
                if (drpStatusStage.SelectedValue == "5")
                {
                    strFileName = RqId + name + "_P" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower(); //Project Approval
                }
                else if (drpStatusStage.SelectedValue == "8")
                {
                    strFileName = RqId + name + "_DC" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();  //Draft Contract
                }
                else
                {
                    strFileName = RqId + name + "_SC" + _uniqueId + System.IO.Path.GetExtension(myFile.FileName).ToLower();  //Signed Contract
                }

                string appPath = HttpContext.Current.Request.ApplicationPath;
                string path = Server.MapPath(appPath + "FrontEnd/Operations/ApprovalFiles/" + strFileName);
                // Save the stream to disk
                System.IO.FileStream newFile = new FileStream(path, FileMode.Create); // new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();

                WhDocumentUploadTransData _WhDocumentUploadTransData = new WhDocumentUploadTransData();
                WhDocumentUploadTransDto _WhDocumentUploadTransDto = new WhDocumentUploadTransDto();

                _WhDocumentUploadTransDto.WhLeadID = RqId;
                _WhDocumentUploadTransDto.FileName = strFileName;
                if (drpStatusStage.SelectedValue == "5")
                {
                    _WhDocumentUploadTransDto.FileType = "P";  //Project Approval
                }
                else if (drpStatusStage.SelectedValue == "8")
                {
                    _WhDocumentUploadTransDto.FileType = "DC"; //Draft Contract
                }
                else
                {
                    _WhDocumentUploadTransDto.FileType = "SC";   //Signed Contract
                }
                if (_WhDocumentUploadTransData.Insert(_WhDocumentUploadTransDto))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "File has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
                return true;
            }
            else
            {
                strFileName = string.Empty;
            }
            return true;
        }
        bool sendMailStage5()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                DataSet ds = _whleadmaster.getUserID(RqId);
                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "UserID not found", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                string subject = "Project Approval-" + drpCustomerName.SelectedItem.Text + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");

                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi,<br/><br/>";
                textBody = textBody + "With reference to discussions by the Project committee on " + drpCustomerName.SelectedItem.Text.ToUpper() + " , the brief project summary is attached, covering P&L, governance parameter etc.<br/><br/>";
                textBody = textBody + "Please approve or reject the same by clicking on the relevant link below.<br/><br/>";

                textBody = textBody + "<br/><table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                textBody = textBody + "<tr><td colspan='4'> <b>BD Person: </b><hr>" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + drpCustomerName.SelectedItem.Text.ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + drpNewEncirclement.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + drpLineOfBusiness.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding:</b><hr>" + txtProjectEta.Text.ToUpper() + "</td><td> <b>Region:</b> <hr>" + drpRegion.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + drpSegment.SelectedItem.Text.ToUpper() + "</td></tr>";
                foreach (GridViewRow gvRow1 in gvLeadTypeTransaction.Rows)
                {
                    TextBox lblMonthlySum = (TextBox)gvRow1.FindControl("gvtxtMonthlyBilling");
                    Label gvlblleadType = (Label)gvRow1.FindControl("lblLeadType");
                    TextBox txtQtyNos = (TextBox)gvRow1.FindControl("gvtxtQty_Nos");
                    TextBox txtRevenu = (TextBox)gvRow1.FindControl("gvtxtPerUnitRevenue");
                    DropDownList drp = (DropDownList)gvRow1.FindControl("drpUOM");

                    textBody = textBody + "<tr> <td> <b>LOB:</b><hr>" + gvlblleadType.Text.ToUpper() + "</td><td> <b>UOM:</b><hr>" + drp.SelectedItem.Text.ToUpper() + "</td><td> <b>Qty:</b> <hr>" + txtQtyNos.Text.ToUpper() + "</td><td> <b>Per Unit Revenue: </b><hr>" + txtRevenu.Text + "</td></tr>";
                }
                textBody = textBody + "<tr> <td colspan='2'> <b>Projected  Monthly Revenue:</b><hr>" + txtMothlyBilling.Text.ToUpper() + "</td><td colspan='2'> <b>Projected GP(%):</b> <hr>" + txtGP.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + drpPricingType.SelectedItem.Text.ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + drpContractType.SelectedItem.Text.ToUpper() + "</td></tr>";
                if (drpItSystem.SelectedItem.Text == "Customer System")
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + txtSystemName.Text.ToUpper() + "</td></tr>";
                }
                else
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + drpSystemName.SelectedItem.Text.ToUpper() + "</td></tr>";
                }

                textBody = textBody + "</table> <br/> <br/>";

                //textBody = textBody + "Kindly Approve/Reject by Clicking the link below";

                // textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                // textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";
                // textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + strFileName + "> <b><font size=5>View Attachment </font> </b></a> ";

                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + dt.Rows[0]["EmailId"].ToString() + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + strFileName + "> <b><font size=5>View Attachment </font> </b></a> ";

                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>Since this is a system generated mail, please do not reply.";

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
        public bool GetApproverIDStage5()
        {
            try
            {
                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhApproverMasterDTo _mailApprovalMasterDto = new WhApproverMasterDTo();
                IList<WhApproverMasterDTo> result = _leadData.GetMailApproverMailIDData(drpBU.SelectedValue, drpStatusStage.SelectedValue, RqId.ToString());
                if (result != null)
                {
                    DataTable dt = result.ToList().ToDataTable<WhApproverMasterDTo>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hfMailApproverMailID.Value = dt.Rows[i]["EmailID"].ToString();
                        hfMailApproverID.Value = dt.Rows[i]["ID"].ToString();

                        InsertintoMailApprover(RqId.ToString(), hfMailApproverID.Value.ToString(), drpStatusStage.SelectedValue);
                        if (!sendMailStage5())
                        {
                            return false;
                        }
                    }

                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        protected void drpPostNegotitationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPostNegotitationStage.SelectedValue == string.Empty)
            {
                dvCancelled.Visible = false;
                dvCmpName.Visible = false;
                dvCmpReason.Visible = false;
            }
            if (drpPostNegotitationStage.SelectedValue == "1")
            {
                dvCancelled.Visible = false;
                dvCmpName.Visible = true;
                dvCmpReason.Visible = true;
            }
            if (drpPostNegotitationStage.SelectedValue == "2")
            {
                dvCancelled.Visible = true;
                dvCmpName.Visible = false;
                dvCmpReason.Visible = false;
            }

            if (drpPostNegotitationStage.SelectedValue == "3")
            {
                dvCancelled.Visible = false;
                dvCmpName.Visible = false;
                dvCmpReason.Visible = false;
            }
        }

        bool GetFile()
        {
            try
            {
                WhDocumentUploadTransData _WhDocumentUploadTransData = new WhDocumentUploadTransData();
                WhDocumentUploadTransDto _WhDocumentUploadTransDto = new WhDocumentUploadTransDto();

                _WhDocumentUploadTransDto.WhLeadID = RqId;

                WhDocumentUploadTransDto result = _WhDocumentUploadTransData.GetByRqId(RqId.ToInt());
                if (result != null)
                {
                    hfExp1.NavigateUrl = "ApprovalFiles/" + result.FileName;
                    hfExp1.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }


        bool sendMailStage8()
        {
            try
            {
                DataTable dt = new DataTable();
                WHLeadMasterData _whleadmaster = new WHLeadMasterData();
                string subject = string.Empty;
                if (drpStatusStage.SelectedValue == "8")
                {
                    subject = "Post Negotiation Approval-" + drpCustomerName.SelectedItem.Text + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");
                }
                else
                {
                    subject = "Draft Contract Approval-" + drpCustomerName.SelectedItem.Text + "  || Date: " + DateTime.Now.Date.ToString("dd MMM yyyy");
                }

                string ApproverID = hfMailApproverID.Value;
                string textBody = "Hi " + drpDesignatedBD.SelectedItem.Text.ToUpper() + "<br/><br/>";
               // textBody = textBody + "In please find the Details.<br/><br/>";
                textBody = textBody + "Kindly Approve/Reject by Clicking the link below: <br/><br/>";

                textBody = textBody + "<br/><table style='background-color: aliceblue;' borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + " width=" + 70 + "%>";
                textBody = textBody + "<tr><td colspan='4'> <b>BD Person: </b><hr>" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td> <b>Customer Name: </b><hr>" + drpCustomerName.SelectedItem.Text.ToUpper() + "</td><td> <b>Type of Account:</b> <hr>" + drpNewEncirclement.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Business Vertical:</b> <hr>" + drpLineOfBusiness.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Expected Time Of Onboarding:</b><hr>" + txtProjectEta.Text.ToUpper() + "</td><td> <b>Region:</b> <hr>" + drpRegion.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Segment: </b><hr>" + drpSegment.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr><td colspan='2'> <b>Pricing Type: </b><hr>" + drpPricingType.SelectedItem.Text.ToUpper() + "</td> <td colspan='2'> <b>Contract Type:</b><hr>" + drpContractType.SelectedItem.Text.ToUpper() + "</td></tr>";
                if (drpItSystem.SelectedItem.Text == "Customer System")
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + txtSystemName.Text.ToUpper() + "</td></tr>";
                }
                else
                {
                    textBody = textBody + "<tr><td colspan='2'> <b>IT System: </b><hr>" + drpItSystem.Text.ToUpper() + "</td> <td colspan='2'> <b>IT System Name:</b><hr>" + drpSystemName.SelectedItem.Text.ToUpper() + "</td></tr>";
                }

                textBody = textBody + "</table> <br/> <br/>";

                

                //textBody = textBody + "<br/><br/><a href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + ApproverID + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                //textBody = textBody + "<br/><a href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + ApproverID + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";
                //textBody = textBody + "<br/><a href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + strFileName + "> <b><font size=5>View Attachment </font> </b></a> ";


                textBody = textBody + "<br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=A&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + ApproverID + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Approve </font> </b></a> ";
                //textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/SendMailForStage3.aspx?Type=R&LeadNo=" + RqId + "&ApproverID=" + ApproverID + "&StatusStageID=" + drpStatusStage.SelectedValue + "&senderUserID=" + ApproverID + "&senderuserName=" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "><b><font size=5> Reject </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/ApprovalFiles/" + strFileName + "> <b><font size=5>View Attachment </font> </b></a> ";

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
        public bool GetApproverIDStage8()
        {
            try
            {
                WHLeadMasterData _leadData = new WHLeadMasterData();
                WhApproverMasterDTo _mailApprovalMasterDto = new WhApproverMasterDTo();
                IList<WhApproverMasterDTo> result = _leadData.GetMailIDStage8(drpBU.SelectedValue, drpStatusStage.SelectedValue, RqId);
                if (result != null)
                {
                    DataTable dt = result.ToList().ToDataTable<WhApproverMasterDTo>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hfMailApproverMailID.Value = dt.Rows[i]["EmailID"].ToString();
                        hfMailApproverID.Value = dt.Rows[i]["ID"].ToString();

                        InsertintoMailApprover8(RqId.ToString(), hfMailApproverID.Value, drpStatusStage.SelectedValue);
                        if (!sendMailStage8())
                        {
                            return false;
                        }

                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        #endregion

        protected void btnCreditUpload_Click(object sender, EventArgs e)
        {
            if (!creditValidation())
            {
                return;
            }
            saveCreditData();
        }



        protected void drpBU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LovelySession.Lovely != null)
            {
                if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 11 || LovelySession.Lovely.User.UserTypeId == 13
                    || LovelySession.Lovely.User.UserTypeId == 15 || LovelySession.Lovely.User.UserTypeId == 20)
                {
                    return;
                }
                if (drpBU.SelectedValue == "1")
                {
                    Response.Redirect("/FrontEnd/operations/WhLeadList.aspx");
                }

                else if (drpBU.SelectedValue == "2")
                {
                    Response.Redirect("/FrontEnd/operations/FreightForwardingList.aspx");
                }
                else if (drpBU.SelectedValue == "3")
                {
                    Response.Redirect("/FrontEnd/operations/LiquidLogiList.aspx");
                }
                else if (drpBU.SelectedValue == "4")
                {
                    Response.Redirect("/FrontEnd/operations/PrimeList.aspx");
                }
                else if (drpBU.SelectedValue == "5")
                {
                    Response.Redirect("/FrontEnd/operations/CFSInfraList.aspx");
                }
                //else if (drpBU.SelectedValue == "6")
                //{
                //    Response.Redirect("/FrontEnd/operations/CustomBrokerageWeb.aspx");
                //}

            }
        }

        protected void drpCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();

            WhCustomerMasterDto result = _customerMasterData.checkCustomerBlongTo(drpCustomerName.SelectedValue.ToNullLong(), "FF");

            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> ChkCustomerWithBD = _whLeadMasterData.ChkCustomerWithBD(drpCustomerName.SelectedValue);

            IList<WhCustomerAddTransDto> addressResult = _customerMasterData.GetAllAddressByCustomerID(drpCustomerName.SelectedValue.ToNullLong());
            if (addressResult != null)
            {
                mp1.Show();
                gvAddress.DataSource = addressResult;
                gvAddress.DataBind();

                btnClose.Enabled = false;
                btnClose.Style["display"] = "None";
                Button1.Visible = true;

            }
            else
            {
                gvAddress.DataBind();
                lblErrorMsg.Text = "Address incomplete please update the Address!!";
                btnClose.Enabled = true;
                btnClose.Style["display"] = "";
                drpCustomerName.SelectedValue = string.Empty;
                Button1.Visible = false;
            }
            if (result != null)
            {

                if (result.Result == "NO")
                {
                    mp1.Show();
                    lblMessage.Text = "This is Not Freight Forwarding Customer, This is only " + result.BelongTo + " Customer";
                }

                if (ChkCustomerWithBD != null)
                {
                    mp1.Show();
                    gvMsg.DataSource = ChkCustomerWithBD;
                    gvMsg.DataBind();
                }
                else
                {
                    gvMsg.DataBind();
                }

            }
            else
            {
                divAddress.Visible = false;
            }


        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            int count = 0;
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                RadioButton rb = (RadioButton)gvRow.FindControl("rb");

                if (rb.Checked == true)
                {
                    count = 1;
                }
            }
            if (count == 0)
            {
                lblErrorMsg.Text = "Please select address for this customer!!";
                mp1.Show();
            }
            else
            {
                mp1.Hide();
            }
        }

        protected void drpStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLeadClose.Text = string.Empty;
            if (drpStage.SelectedValue == string.Empty)
            {
                divLeadCloseStatus.Visible = false;
            }
            else
            {
                divLeadCloseStatus.Visible = true;
                txtLeadClose.Focus();
            }
        }

        #region Mail for Status won or loss


        public void sendMailForLeadClose()
        {
            try
            {

                string subject = "Lead Closuer intimation of " + drpCustomerName.SelectedItem.Text;

                string textBody = "Hi BD Head,<br/><br/>";

                textBody = textBody + "<b>" + drpDesignatedBD.SelectedItem.Text + "</b> has closed the following lead <b>" + drpCustomerName.SelectedItem.Text +
                    "</b> - Lead Created Date:- <b>" + hfLeadCreatedDate.Value + "</b> for the following  reason:- <b>" + txtLeadClose.Text + "</b> .<br/><br/>";
                textBody += "This is system generated mail, Please don't reply.";

                mFrom = "crm@als.group";
                mTo = "";
                mBCC = "yash.godiyal@biproex.com";
                mSubject = subject;
                mBody = textBody;
                sendMailForFF(mFrom, mTo, mSubject, mBody);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        #endregion

       

       

        


    }
}

