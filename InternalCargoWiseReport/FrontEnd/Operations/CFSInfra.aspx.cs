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
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CFSInfra : System.Web.UI.Page
    {
        long? WHLeadID;
        string strFileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FirstGridViewRow();
                if (LovelySession.Lovely != null)
                {
                    // if (LovelySession.Lovely.User.UserTypeId == 11 || LovelySession.Lovely.User.UserTypeId == 12)
                    //{
                    //    lblMainHeading.Text = "Freight Forwarding";
                    //    lblSecHeading.Text = "FF";
                    //}
                    bindDrp();


                    drpBU.SelectedValue = "5";
                    if (RqId != null)
                    {
                        setData();
                        bindGridData();
                        // enableFalse();
                        btnSubmit.Text = "Update";
                        btnCancel.Visible = false;
                        HfID.Value = RqId.ToString();

                        if (LovelySession.Lovely != null)
                        {
                            if (LovelySession.Lovely.User.UserTypeId == 1 || LovelySession.Lovely.User.UserTypeId == 11)
                            {
                                btnSubmit.Visible = true;
                            }
                        }
                    }
                }

            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        string RqId { get { return Request["requestId"]; } }

        #region Method
        void RemoveDrpValue(string Value)
        {
            int Index = drpStatusStage.Items.IndexOf(drpStatusStage.Items.FindByValue(Value));

            if (Index > 1)
            {
                for (int i = 1; i < Index; i++)
                {
                    drpStatusStage.Items.RemoveAt(1);
                }
            }
        }

        private WHLeadMasterDto MappingObject(WHLeadMasterDto obj)
        {
            if (HfID.Value != string.Empty)
                obj.ID = HfID.Value.ToLong();
            //obj.LeadNo = string.Empty;
            obj.LeadSource = txtLeadSource.Text.ToUpper();
            obj.CustomerName = drpCustomerName.SelectedValue.ToUpper();
            obj.Lineofbusiness = drpLineOfBusiness.SelectedValue;
            obj.OpportunityBrief = txtOppBrief.Text.ToUpper();
            obj.StatusStage = drpStatusStage.SelectedValue;
            // obj.MonthlyBilling = txtMothlyBilling.Text.ToDouble();
            // obj.GP = txtGP.Text.ToDouble();
            // obj.ProjectETA = txtProjectEta.Text.ToConvertNullDateTime();
            obj.DesignatedBD = drpDesignatedBD.SelectedValue;
            obj.StatusUpdate = txtStatusUpdate.Text.ToUpper();
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.New_Encirclement = drpNewEncirclement.SelectedValue;
            obj.BU = drpBU.SelectedValue;
            obj.ContactPersonName = txtContactPersonName.Text.ToUpper();
            obj.ContactPersonDesignation = txtContactPersonDesignation.Text.ToUpper();
            obj.ContactPersonMailID = txtContactPersonMailID.Text;
            obj.ContactPersonPhoneNo = txtContactPhoneNo.Text;
            //  obj.ProjectATA = txtProjectATA.Text.ToConvertNullDateTime();
            obj.Region = drpRegion.SelectedValue;
            obj.Segment = txtSegment.Text;
            // obj.LocationFrom = txtLocationFrom.Text.ToUpper();
            // obj.LocationTo = txtLocationTo.Text.ToUpper();
            // obj.TradeLane = txtTradeLane.Text.ToUpper();
            obj.ValueAdded = checkedCompanyList();
            obj.Unit = drpUnit.SelectedValue;
            obj.Qty = txtQty.Text.ToNullDouble();
            obj.FileName = strFileName;
            obj.UserTypeID = LovelySession.Lovely.User.UserTypeId;
            obj.BusinessDriver = drpBusinessDriver.SelectedValue;
            obj.NoOfTues = txtTues.Text.ToUpper();// drpTues.SelectedValue;
            obj.AvgNoOfStay = txtAvgNoOfSty.Text.ToDouble();
            obj.ExpectedGroundRent = txtExpGrountRent.Text.ToDouble();
            obj.AvgRelExpected = txtRealizationExpected.Text.ToUpper();//.SelectedValue;
            obj.CategoryOfCompany = drpCategoryOfCompany.SelectedValue;
            obj.Revenue = txtRevenue.Text.ToNullDouble();
            obj.RevenueRange = txtRevenueRange.Text.ToString();
            obj.Stage = drpLeadStatus.SelectedValue;
            obj.PRCGP = txtPrcgp.Text.ToNullDouble();
            obj.POCGP = txtPocgp.Text.ToNullDouble();
            obj.CreditPeriod = drpCreditPeriod.SelectedValue;
            obj.ContractDurationType = drpContractType.SelectedValue;
            obj.BaseRateExpected = txtBaseRateExpected.Text.ToDouble();
            if (txtBaseRateExpected.Text.ToDouble() < 550)
            {
                obj.IsHold = true;
            }

            // obj.Location = txtLocation.Text;
            // obj.ServiceTypeID = drpTypeOfService.SelectedValue.ToNullLong();
            // obj.VehicleTypeID = drpVehicleType.SelectedValue.ToNullLong();
            //obj.EnquiryReceiveDate = txtEnquiryReceiveDate.Text.ToConvertNullDateTime();
            //obj.TargetSubmissionDate = txtTargetSubmissionDate.Text.ToConvertNullDateTime();
            //obj.DataReceiveDate = txtDataReceiveDate.Text.ToConvertNullDateTime();
            //obj.CostingReadinessDate = txtCostingReadinessDate.Text.ToConvertNullDateTime();
            //obj.CostingReviewedDate = txtCostingReviewedDate.Text.ToConvertNullDateTime();
            //obj.ActualSubmissionDate = txtActualSubmissionDate.Text.ToConvertNullDateTime();
            //obj.GoLiveDate = txtGoLiveDate.Text.ToConvertNullDateTime();
            //obj.SOPAfterSubmission = txtStatusOftheProjectAfterSubmission.Text;
            //obj.SizeOfWarehouse = txtSizeOfWarehouse.Text.ToNullDouble();
            //obj.CostingReviewedBy = txtCostingReviewedBy.Text;
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

            //if (txtTradeLane.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Trade Lane.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtTradeLane.Focus();
            //    return false;
            //}
            if (drpLineOfBusiness.SelectedValue != string.Empty)
            {
                if (drpLineOfBusiness.SelectedValue != "Value Added")
                {
                    //if (txtLocationFrom.Text == string.Empty)
                    //{
                    //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location From.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    //    txtLocationFrom.Focus();
                    //    return false;
                    //}
                    //else if (txtLocationTo.Text == string.Empty)
                    //{
                    //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location To.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    //    txtLocationTo.Focus();
                    //    return false;
                    // }
                    if (txtQty.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Qty.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        txtQty.Focus();
                        return false;
                    }
                    else if (drpUnit.SelectedValue == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Unit.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        drpUnit.Focus();
                        return false;
                    }
                }
                else if (drpLineOfBusiness.SelectedValue == "Value Added")
                {
                    if (drpValueAddedList.SelectedValue == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Value Added.", "Oops!", Toastr.ToastPosition.TopCenter, true);

                        return false;
                    }
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
                //if (txtProjectEta.Text == string.Empty)
                //{
                //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Project ETA Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //    txtProjectEta.Focus();
                //    return false;
                //}

                //if (txtMothlyBilling.Text == string.Empty)
                //{
                //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Monthly Billing.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //    txtMothlyBilling.Focus();
                //    return false;
                //}
                //if (txtGP.Text == string.Empty)
                //{
                //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter GP.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                //    txtGP.Focus();
                //    return false;
                //}

            }
            if (drpStatusStage.SelectedValue == "12")
            {
                if (drpLeadStatus.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Lead Status(Won Or Lost).", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    drpLeadStatus.Focus();
                    return false;
                }
            }

            if (txtOppBrief.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Opportunity Brief.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtOppBrief.Focus();
                return false;
            }
            if (txtContactPersonName.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtContactPersonName.Focus();
                return false;
            }
            if (txtContactPersonDesignation.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Desination.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtContactPersonDesignation.Focus();
                return false;
            }
            if (txtContactPersonMailID.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Mail ID.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtContactPersonMailID.Focus();
                return false;
            }
            if (txtContactPersonMailID.Text != string.Empty)
            {
                Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");//, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                Match match = regex.Match(txtContactPersonMailID.Text.Trim());
                if (match.Success)
                { }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter valid email address.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    txtContactPersonMailID.Focus();
                    return false;
                }
            }
            if (txtContactPhoneNo.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Contact Person Phone No.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtContactPhoneNo.Focus();
                return false;
            }

            //if (txtGP.Text != string.Empty)
            //{
            //    if (txtGP.Text.ToDouble() > 100)
            //    {
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "GP can't be more then 100.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //        txtGP.Focus();
            //        return false;
            //    }
            //}
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
            HttpPostedFile myFile = FileUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (5242880 < nFileLen)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File size can't be more then 5 MB.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }

            if (drpBusinessDriver.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Business Driver.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpBusinessDriver.Focus();
                return false;
            }
            if (txtTues.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Tues.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtTues.Focus();
                return false;
            }
            if (txtAvgNoOfSty.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter No Of Stay.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtAvgNoOfSty.Focus();
                return false;
            }
            //if (txtExpGrountRent.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter rent.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    txtExpGrountRent.Focus();
            //    return false;
            //}
            if (txtRealizationExpected.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Realization Expected.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtRealizationExpected.Focus();
                return false;
            }
            if (txtBaseRateExpected.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Base Rate Expected.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtBaseRateExpected.Focus();
                return false;
            }
            if (drpStatusStage.SelectedValue != "9")
            {

                if (txtPrcgp.Text.ToDouble() == 0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "PRCGP can't be zero/Empty.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                else if (txtPocgp.Text.ToDouble() == 0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "POCGP can't be zero/Empty.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

            }
            if (txtPrcgp.Text != string.Empty)
            {
                if (txtPrcgp.Text.ToDouble() > 100)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "PRCGP can't be more then 100.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

            }
            if (txtPocgp.Text != string.Empty)
            {
                if (txtPocgp.Text.ToDouble() > 100)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "POCGP can't be more then 100.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
            }
            if (drpContractType.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Contract Type.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (drpCreditPeriod.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Credit Period.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpCreditPeriod.Focus();
                return false;
            }

            // if (drpTypeOfService.SelectedValue == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Type of service.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            // if (txtLocation.Text == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            // if (txtSizeOfWarehouse.Text == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter size of warehouse.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            // if (txtEnquiryReceiveDate.Text == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter enquiry receive date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            // if (txtTargetSubmissionDate.Text == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Target submission date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            // if (txtDataReceiveDate.Text == string.Empty)
            // {
            //     Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Data Received Date.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //     return false;
            // }
            return true;
        }

        void clear()
        {

            txtLeadSource.Text = string.Empty;
            drpCustomerName.SelectedValue = string.Empty;
            drpLineOfBusiness.SelectedValue = string.Empty;
            txtOppBrief.Text = string.Empty;
            drpStatusStage.SelectedValue = string.Empty;
            // txtMothlyBilling.Text = string.Empty;
            //txtGP.Text = string.Empty;
            // txtProjectEta.Text = string.Empty;
            drpDesignatedBD.SelectedValue = string.Empty;
            txtStatusUpdate.Text = string.Empty;
            //  drpBU.SelectedValue = string.Empty;
            drpNewEncirclement.SelectedValue = string.Empty;
            txtContactPersonName.Text = string.Empty;
            txtContactPersonMailID.Text = string.Empty;
            txtContactPersonDesignation.Text = string.Empty;
            txtContactPhoneNo.Text = string.Empty;
            drpRegion.SelectedValue = string.Empty;
            drpSegment.SelectedValue = string.Empty;
            // txtLocationFrom.Text = string.Empty;
            // txtLocationTo.Text = string.Empty;
            // txtTradeLane.Text = string.Empty;
            drpValueAddedList.ClearSelection();
            drpUnit.SelectedValue = string.Empty;
            txtQty.Text = string.Empty;
            //  txtLocationFrom.Visible = false;
            //  txtLocationTo.Visible = false;
            drpUnit.Visible = false;
            txtQty.Visible = false;
            drpValueAddedList.Visible = false;
            // lblLocationFrom.Visible = false;
            //  lblLocationStar.Visible = false;
            //  lblLocationTo.Visible = false;
            //  lblLocationToStar.Visible = false;
            lblQty.Visible = false;
            lblQtyStar.Visible = false;
            lblUnit.Visible = false;
            lblUnitStar.Visible = false;
            lblValueAddedService.Visible = false;
            lblValueAddedStar.Visible = false;
            // txtProjectATA.Text = string.Empty;
            drpBusinessDriver.SelectedValue = string.Empty;
            txtTues.Text = string.Empty;
            txtAvgNoOfSty.Text = string.Empty;
            txtExpGrountRent.Text = string.Empty;
            txtRevenue.Text = string.Empty;
            txtRealizationExpected.Text = string.Empty;
            txtSegment.Text = string.Empty;
            // txtLocation.Text = string.Empty;
            // drpTypeOfService.SelectedValue = string.Empty;
            // //drpVehicleType.SelectedValue = string.Empty;
            // txtEnquiryReceiveDate.Text = string.Empty;
            // txtTargetSubmissionDate.Text = string.Empty;
            // txtDataReceiveDate.Text = string.Empty;
            // txtCostingReadinessDate.Text = string.Empty;
            // txtCostingReviewedBy.Text = string.Empty;
            // txtCostingReviewedDate.Text = string.Empty;
            // txtActualSubmissionDate.Text = string.Empty;
            // txtGoLiveDate.Text = string.Empty;
            // txtStatusOftheProjectAfterSubmission.Text = string.Empty;
            // txtSizeOfWarehouse.Text = string.Empty;

        }

        void setData()
        {
            long rqID = RqId.ToDataConvertInt64();
            WHLeadMasterData _whLeadData = new WHLeadMasterData();
            WHLeadMasterDto result = _whLeadData.GetById(rqID, LovelySession.Lovely.User.UserTypeId);
            if (result != null)
            {
                drpBU.Enabled = false;
                txtLeadSource.Text = result.LeadSource;
                drpCustomerName.SelectedValue = result.CustomerName;
                drpLineOfBusiness.SelectedValue = result.Lineofbusiness;
                txtOppBrief.Text = result.OpportunityBrief;
                drpStatusStage.SelectedValue = result.StatusStage;
                // txtMothlyBilling.Text = result.MonthlyBilling.ToString();
                //   txtGP.Text = result.GP.ToString();
                //if (result.ProjectETA != null)
                //{
                //    txtProjectEta.Text = result.ProjectETA.Value.ToString("dd MMM yyyy");
                //    txtProjectEta.Enabled = false;
                //}
                //else
                //{
                //    txtProjectEta.Enabled = true;
                //}

                drpDesignatedBD.SelectedValue = result.DesignatedBD;
                txtStatusUpdate.Text = result.StatusUpdate;
                drpNewEncirclement.SelectedValue = result.New_Encirclement;
                drpBU.SelectedValue = result.BU;
                txtContactPersonName.Text = result.ContactPersonName;
                txtContactPersonMailID.Text = result.ContactPersonMailID;
                txtContactPersonDesignation.Text = result.ContactPersonDesignation;
                txtContactPhoneNo.Text = result.ContactPersonPhoneNo;
                drpRegion.SelectedValue = result.Region;
                txtSegment.Text = result.Segment;
                drpSegment.SelectedValue = result.Segment;
                txtSegment.Enabled = false;
                //txtLocationFrom.Text = result.LocationFrom;
                // txtLocationTo.Text = result.LocationTo;
                //  txtTradeLane.Text = result.TradeLane;
                txtQty.Text = result.Qty.ToString();
                drpUnit.SelectedValue = result.Unit;


                if (result.ValueAdded != null)
                {
                    string[] ValueAdded = result.ValueAdded.Split(',');
                    foreach (ListItem item in drpValueAddedList.Items)
                    {
                        for (int i = 0; i < ValueAdded.Length; i++)
                        {
                            if (item.Value == ValueAdded[i])
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                //if (result.ProjectATA != null)
                //    txtProjectATA.Text = result.ProjectATA.Value.ToString("dd MMM yyyy");


                if (result.StatusStage == "7")
                {
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    /// txtProjectATA.Enabled = false;
                    drpStatusStage.Enabled = false;
                    //   txtMothlyBilling.Enabled = false;
                    // txtGP.Enabled = false;
                }
                if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 1)
                {
                    btnSubmit.Visible = true;
                    gvStatusUpdate.Enabled = true;
                }
                drpLineOfBusiness.Enabled = false;
                if (result.Lineofbusiness != "Value Added")
                {
                    //txtLocationFrom.Visible = true;
                    //lblLocationStar.Visible = true;
                    //lblLocationFrom.Visible = true;
                    //txtLocationTo.Visible = true;
                    //lblLocationToStar.Visible = true;
                    //lblLocationTo.Visible = true;
                    lblQty.Visible = true;
                    lblQtyStar.Visible = true;
                    txtQty.Visible = true;
                    lblValueAddedService.Visible = false;
                    lblValueAddedStar.Visible = false;
                    drpValueAddedList.Visible = false;
                    lblUnit.Visible = true;
                    lblUnitStar.Visible = true;
                    drpUnit.Visible = true;
                    divScroll.Visible = false;
                }
                else
                {
                    //txtLocationFrom.Visible = false;
                    //lblLocationStar.Visible = false;
                    //txtLocationTo.Visible = false;
                    //lblLocationToStar.Visible = false;
                    lblValueAddedService.Visible = true;
                    lblValueAddedStar.Visible = true;
                    drpValueAddedList.Visible = true;
                    //lblLocationFrom.Visible = false;
                    //lblLocationTo.Visible = false;
                    lblQty.Visible = false;
                    lblQtyStar.Visible = false;
                    txtQty.Visible = false;
                    lblUnit.Visible = false;
                    lblUnitStar.Visible = false;
                    drpUnit.Visible = false;
                    divScroll.Visible = true;
                }

                if (result.Revenue.ToDataConvertDouble() != 0)
                {
                    txtTues.Enabled = false;
                    txtBaseRateExpected.Enabled = false;
                }
            }
            else
            {
                btnSubmit.Visible = false;
            }

            drpBusinessDriver.SelectedValue = result.BusinessDriver;
            txtTues.Text = result.NoOfTues;
            txtAvgNoOfSty.Text = result.AvgNoOfStay.ToString();
            txtExpGrountRent.Text = result.ExpectedGroundRent.ToString();

            txtRealizationExpected.Text = result.AvgRelExpected;
            txtRevenue.Text = result.Revenue.ToString();
            if (result.RevenueRange != string.Empty)
            {
                txtRevenueRange.Text = result.RevenueRange.ToString();
            }
            else
            {
                RevenueRange();
            }
            drpCategoryOfCompany.SelectedValue = result.CategoryOfCompany;
            txtPrcgp.Text = result.PRCGP.ToString();
            txtPocgp.Text = result.POCGP.ToString();
            drpContractType.SelectedValue = result.ContractDurationType;
            drpCreditPeriod.SelectedValue = result.CreditPeriod;
            if (drpStatusStage.SelectedValue == "9" || drpStatusStage.SelectedValue == "10")
            {
                txtPocgp.Enabled = true;
                txtPrcgp.Enabled = true;
            }
            else
            {
                txtPocgp.Enabled = false;
                txtPrcgp.Enabled = false;

            }
            if (result.StatusStage == "12")
            {
                divStatus.Visible = true;
                drpLeadStatus.SelectedValue = result.Stage;
            }
            else
            {
                divStatus.Visible = false;
            }
            RemoveDrpValue(result.StatusStage);
            txtBaseRateExpected.Text = result.BaseRateExpected.ToString();
            // txtLocation.Text = result.Location;
            //drpTypeOfService.SelectedValue = result.ServiceTypeID.ToString();
            //drpVehicleType.SelectedValue = result.VehicleTypeID.ToString();

            //if(result.EnquiryReceiveDate.ToString()!=string.Empty)
            //txtEnquiryReceiveDate.Text = result.EnquiryReceiveDate.Value.ToString("dd MMM yyyy");
            //if(result.TargetSubmissionDate.ToString()!=string.Empty)
            //txtTargetSubmissionDate.Text = result.TargetSubmissionDate.Value.ToString("dd MMM yyyy");
            //if(result.DataReceiveDate.ToString()!=string.Empty)
            //    txtDataReceiveDate.Text = result.DataReceiveDate.Value.ToString("dd MMM yyyy");
            //if(result.CostingReadinessDate.ToString()!=string.Empty)
            //    txtCostingReadinessDate.Text = result.CostingReadinessDate.Value.ToString("dd MMM yyyy");
            //if(result.CostingReviewedDate.ToString()!=string.Empty)
            //    txtCostingReviewedDate.Text = result.CostingReviewedDate.Value.ToString("dd MMM yyyy");
            //if(result.ActualSubmissionDate.ToString()!=string.Empty)
            //    txtActualSubmissionDate.Text = result.ActualSubmissionDate.Value.ToString("dd MMM yyyy");
            //if(result.GoLiveDate.ToString()!=string.Empty)
            //    txtGoLiveDate.Text = result.GoLiveDate.Value.ToString("dd MMM yyyy");
            //txtStatusOftheProjectAfterSubmission.Text = result.SOPAfterSubmission;
            //txtSizeOfWarehouse.Text = result.SizeOfWarehouse.ToString();
            //txtCostingReviewedBy.Text = result.CostingReviewedBy;
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
            // txtProjectEta.Enabled = false;
            drpDesignatedBD.Enabled = false;
            txtStatusUpdate.Enabled = false;
            drpBU.Enabled = false;
            drpNewEncirclement.Enabled = false;
            txtContactPersonName.Enabled = false;
            txtContactPersonMailID.Enabled = false;
            txtContactPersonDesignation.Enabled = false;
            txtContactPhoneNo.Enabled = false;
            drpRegion.Enabled = false;
            drpSegment.Enabled = false;
            //txtLocationFrom.Enabled = false;
            //txtLocationTo.Enabled = false;
            //txtTradeLane.Enabled = false;
            drpValueAddedList.Enabled = false;
        }

        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            IList<BDSolutionMasterDto> AllFFBD = _bdSolutionMasterData.GetAllCFSBD().OrderBy(x => x.BD).ToList();
            if (AllFFBD != null)
            {
                drpDesignatedBD.DataSource = AllFFBD;
                drpDesignatedBD.DataValueField = "ID";
                drpDesignatedBD.DataTextField = "CFSBD";
                drpDesignatedBD.DataBind();
                drpDesignatedBD.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            WHLeadMasterData _leadData = new WHLeadMasterData();
            IList<WHLeadMasterDto> getSegment = _leadData.getSegment(LovelySession.Lovely.User.UserTypeId, "CFSINFRA");
            if (getSegment != null)
            {

                drpSegment.DataSource = getSegment;
                drpSegment.DataValueField = "Segment";
                drpSegment.DataTextField = "Segment";
                drpSegment.DataBind();
                drpSegment.Items.Insert(0, new ListItem("--Select--", string.Empty));
                drpSegment.Items.Insert((getSegment.Count + 1), new ListItem("Other", "Other"));
            }
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("CFS");
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
            IList<WHLeadStatusUpdateDto> CrmStageData = _leadData.getCrmStageDataWithBu(5);
            if (CrmStageData != null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

            //  ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            //IList<ServiceTypeMasterDto> ServiceResults = _serviceTypeMasterData.GetAllWarehouseServiceType();
            //if (ServiceResults != null)
            //{
            //    drpTypeOfService.DataSource = ServiceResults;
            //    drpTypeOfService.DataValueField = "ID";
            //    drpTypeOfService.DataTextField = "Name";
            //    drpTypeOfService.DataBind();
            //    drpTypeOfService.Items.Insert(0, new ListItem("--Select--", string.Empty));
            //}
            IList<ServiceTypeMasterDto> VehicleTypeResults = _serviceTypeMasterData.GetAllVehicleType();
            //if (VehicleTypeResults != null)
            //{
            //    drpVehicleType.DataSource = VehicleTypeResults;
            //    drpVehicleType.DataValueField = "ID";
            //    drpVehicleType.DataTextField = "Name";
            //    drpVehicleType.DataBind();
            //    drpVehicleType.Items.Insert(0, new ListItem("--Select--", string.Empty));
            //}

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

        string checkedCompanyList()
        {
            List<string> output = new List<string>();
            foreach (ListItem item in drpValueAddedList.Items)
            {
                if (item.Selected)
                    output.Add(item.Value);
            }
            return String.Join(",", output).ToUpper();
        }

        void Upload()
        {
            string str = null;
            string fileExt = string.Empty;
            string sSavePath;

            sSavePath = "FFFiles/";
            // Check file size (mustn’t be 0)
            HttpPostedFile myFile = FileUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
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
                string path = Server.MapPath(appPath + "FrontEnd/Operations/FFFiles/" + strFileName);
                // Save the stream to disk
                System.IO.FileStream newFile = new FileStream(path, FileMode.Create); // new System.IO.FileStream(Server.MapPath(sSavePath + sFilename), System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();
            }
            else
            {
                strFileName = string.Empty;
            }
        }

        public void RevenueRange()
        {
            try
            {
                if (txtRevenue.Text == string.Empty || txtRevenue.Text.ToNullDouble() == 0)
                {
                    txtRevenueRange.Text = "N/A";

                }
                else if (txtRevenue.Text.ToNullDouble() > 0 && txtRevenue.Text.ToNullDouble() <= 100000)
                {
                    txtRevenueRange.Text = "Less than 1 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 100000 && txtRevenue.Text.ToNullDouble() <= 500000)
                {
                    txtRevenueRange.Text = " 1-5 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 500000 && txtRevenue.Text.ToNullDouble() <= 1000000)
                {
                    txtRevenueRange.Text = "5-10 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 1000000 && txtRevenue.Text.ToNullDouble() <= 2000000)
                {
                    txtRevenueRange.Text = "10-20 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 2000000 && txtRevenue.Text.ToNullDouble() <= 3000000)
                {
                    txtRevenueRange.Text = "20-30 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 3000000 && txtRevenue.Text.ToNullDouble() <= 4000000)
                {
                    txtRevenueRange.Text = "30-40 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() >= 4000000 && txtRevenue.Text.ToNullDouble() <= 5000000)
                {
                    txtRevenueRange.Text = "40-50 Lacs";
                }
                else if (txtRevenue.Text.ToNullDouble() > 5000000)
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
            Upload();
            WHLeadMasterDto request = MappingObject(new WHLeadMasterDto());

            WHLeadMasterData _leadData = new WHLeadMasterData();
            if (HfID.Value == string.Empty || HfID.Value == "0")
            {
                WHLeadID = _leadData.Insert(request);
               
                if (WHLeadID > 0)
                {
                    if (txtBaseRateExpected.Text.ToDouble() < 550 && LovelySession.Lovely.User.LocationID==25)
                    {
                        sendMailToBaseRateApprover(WHLeadID);
                    }

                    saveStatusGridData();
                    // bindDrp();
                    // sendMail();

                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    clear();
                    FirstGridViewRow();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Record Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                
                if (_leadData.UpdateForOtherCrm(request))
                {
                    saveStatusGridData();
                    //bindDrp();
                    //sendMail();
                    btnSubmit.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                    //if (txtProjectEta.Text != string.Empty)
                    //{
                    //    txtProjectEta.Enabled = false;
                    //}
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record not updated", "Success!", Toastr.ToastPosition.TopCenter, true);
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Operations/CFSInfraList.aspx");
        }
        #endregion

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


        #endregion

        protected void drpStatusStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpLeadStatus.SelectedValue = string.Empty;
            if (drpStatusStage.SelectedValue == string.Empty)
            {
                lblstarPocgp.Visible = false;
                lblstarPrcgp.Visible = false;
            }

            if (drpStatusStage.SelectedValue == "12")
            {
                divStatus.Visible = true;
            }
            else
            {
                divStatus.Visible = false;
            }
            if (drpStatusStage.SelectedValue == "9")
            {
                lblstarPocgp.Visible = false;
                lblstarPrcgp.Visible = false;
            }
            else
            {
                lblstarPocgp.Visible = true;
                lblstarPrcgp.Visible = true;
            }

        }

        protected void drpLineOfBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpLineOfBusiness.SelectedValue != string.Empty)
            {
                if (drpLineOfBusiness.SelectedValue != "Value Added")
                {
                    //txtLocationFrom.Visible = true;
                    //lblLocationStar.Visible = true;
                    //lblLocationFrom.Visible = true;
                    //txtLocationTo.Visible = true;
                    //lblLocationToStar.Visible = true;
                    //lblLocationTo.Visible = true;
                    lblQty.Visible = true;
                    lblQtyStar.Visible = true;
                    txtQty.Visible = true;
                    lblUnit.Visible = true;
                    lblUnitStar.Visible = true;
                    drpUnit.Visible = true;
                    lblValueAddedService.Visible = false;
                    lblValueAddedStar.Visible = false;
                    drpValueAddedList.Visible = false;
                    divScroll.Visible = false;

                    drpValueAddedList.ClearSelection();
                }
                else
                {
                    // txtLocationFrom.Visible = false;
                    // lblLocationStar.Visible = false;
                    // txtLocationTo.Visible = false;
                    // lblLocationToStar.Visible = false;
                    lblQty.Visible = false;
                    lblQtyStar.Visible = false;
                    txtQty.Visible = false;
                    lblUnit.Visible = false;
                    lblUnitStar.Visible = false;
                    drpUnit.Visible = false;
                    lblValueAddedService.Visible = true;
                    lblValueAddedStar.Visible = true;
                    drpValueAddedList.Visible = true;
                    // lblLocationFrom.Visible = false;
                    // lblLocationTo.Visible = false;
                    divScroll.Visible = true;

                    // txtLocationFrom.Text = string.Empty;
                    // txtLocationTo.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    drpUnit.SelectedValue = string.Empty;
                }

            }
            else
            {
                //txtLocationFrom.Visible = false;
                //lblLocationStar.Visible = false;
                //txtLocationTo.Visible = false;
                //lblLocationToStar.Visible = false;
                lblValueAddedService.Visible = false;
                lblValueAddedStar.Visible = false;
                drpValueAddedList.Visible = false;
                //lblLocationFrom.Visible = false;
                //lblLocationTo.Visible = false;
                lblQty.Visible = false;
                lblQtyStar.Visible = false;
                txtQty.Visible = false;
                lblUnit.Visible = false;
                lblUnitStar.Visible = false;
                drpUnit.Visible = false;
                //txtLocationFrom.Text = string.Empty;
                //txtLocationTo.Text = string.Empty;
                txtQty.Text = string.Empty;
                drpUnit.SelectedValue = string.Empty;
                drpValueAddedList.ClearSelection();
                divScroll.Visible = false;
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
        //public void sendMail()
        //{
        //    try
        //    {

        //        string subject = "Lead Details";

        //        string textBody = "Please find the Lead Details,<br/><br/>";

        //        textBody = textBody + " <table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
        //        textBody = textBody + "<tr> <td> <b>Lead Source:</b><hr>" + txtLeadSource.Text.ToUpper() + "</td><td> <b>Customer Name: </b><hr>" + txtCustomerName.Text.ToUpper() + "</td><td> <b>Type of Business:</b> <hr>" + drpNewEncirclement.SelectedItem.Text.ToUpper() + "</td><td><b> BU:</b> <hr>" + drpBU.SelectedItem.Text.ToUpper() + "</td></tr>";
        //        textBody = textBody + "<tr> <td> <b>Project ETA:</b><hr>" + txtProjectEta.Text.ToUpper() + "</td><td> <b>Project ATA: </b><hr>" + txtProjectATA.Text.ToUpper() + "</td><td><b> Designated BD: </b><hr>" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "</td><td> <b>Line Of Business:</b> <hr>" + drpLineOfBusiness.SelectedItem.Text.ToUpper() + "</td></tr>";
        //        if (drpLineOfBusiness.SelectedItem.Text != "Value Added")
        //        {
        //            textBody = textBody + "<tr> <td> <b>Trade Lane: </b><hr>" + txtTradeLane.Text.ToUpper() + "</td><td> <b>Location From:</b> <hr>" + txtLocationFrom.Text.ToUpper() + "</td><td><b> Location To:</b> <hr>" + txtLocationTo.Text.ToUpper() + "</td><td> <b>Qty:</b> <hr>" + txtQty.Text.ToUpper() + "</td></tr>";
        //            textBody = textBody + "<tr> <td> <b>Unit:</b> <hr>" + drpUnit.SelectedItem.Text.ToUpper() + "</td><td><b> CRM Stage:</b> <hr>" + drpStatusStage.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Monthly Billing:</b> <hr>" + txtMothlyBilling.Text + "</td></tr>";
        //        }
        //        else
        //        {
        //            textBody = textBody + "<tr> <td> <b>Trade Lane:</b> <hr>" + txtTradeLane.Text.ToUpper() + "</td><td> <b>CRM Stage:</b> <hr>" + drpStatusStage.SelectedItem.Text.ToUpper() + "</td><td> <b>Monthly Billing:</b> <hr>" + txtMothlyBilling.Text.ToUpper() + "</td><td> <b>Value Added List:</b> <hr>" + checkedCompanyList() + "</td></tr>";
        //            // textBody = textBody + "<tr> <td colspan='4'> <b>Value Added List:</b> <hr>" + checkedCompanyList() + "</td></tr>";
        //        }
        //        textBody = textBody + "<tr> <td> <b>GP(%):</b> <hr>" + txtGP.Text + "</td><td> <b>Opportunity Brief:</b> <hr>" + txtOppBrief.Text.ToUpper() + "</td><td> <b>Region:</b> <hr>" + drpRegion.SelectedItem.Text.ToUpper() + "</td><td> <b>Segment: </b><hr>" + drpSegment.SelectedItem.Text.ToUpper() + "</td></tr>";
        //        textBody = textBody + "<tr> <td colspan='4'> <h2>Contact Person Details </h2></td></tr>";
        //        textBody = textBody + "<tr> <td><b> Name:</b><hr>" + txtContactPersonName.Text.ToUpper() + "</td><td> <b>Designation:</b><hr>" + txtContactPersonDesignation.Text.ToUpper() + "</td><td><b> Mail ID:</b><hr>" + txtContactPersonMailID.Text.ToUpper() + "</td><td><b> Phone No:</b><hr>" + txtContactPhoneNo.Text + "</td></tr>";
        //        textBody = textBody + "<tr> <td colspan='4'><table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
        //        textBody = textBody + "<tr> <td><b> Status</b> </td> <td><b>Date</b></td></tr>";
        //        foreach (GridViewRow gvRow in gvStatusUpdate.Rows)
        //        {
        //            TextBox gvtxtStatusUpdate = (TextBox)gvRow.FindControl("gvtxtStatusUpdate");
        //            Label lblDate = (Label)gvRow.FindControl("gvlblDate");
        //            textBody += "<tr><td style='word-wrap: break-word'>" + gvtxtStatusUpdate.Text + "</td><td style='word-wrap: break-word'>" + lblDate.Text + "</td></tr>";
        //        }
        //        textBody = textBody + "</table>  </td></tr>";
        //        textBody += "</table> <br/> <br/>";

        //        //textBody += "Regards,<br/><br/>";
        //        textBody += "This is system generated mail, Please don't reply.";

        //        //mFrom = Session["User_ID"].ToString();
        //        mFrom = "crm@als.group";
        //        mTo = "";
        //        //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
        //        mBCC = "harishsangwan23@gmail.com,soma.s@als.group,shivani.r@als.group";
        //        mSubject = subject;
        //        mBody = textBody;
        //        sendMailForFF(mFrom, mTo, mSubject, mBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }


        //}



        protected void txtTues_TextChanged(object sender, EventArgs e)
        {
            if (txtTues.Text.Trim() == string.Empty && txtBaseRateExpected.Text.Trim() != string.Empty)
            {
                txtRevenue.Text = "0";
                RevenueRange();
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Enter No of Tues", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtTues.Focus();
                return;
            }
            if (txtBaseRateExpected.Text.Trim() == string.Empty && txtTues.Text.Trim() == string.Empty)
            {
                txtRevenue.Text = "0";
                RevenueRange();
                return;
            }
            if (txtTues.Text.Trim() != string.Empty && txtBaseRateExpected.Text.Trim() != string.Empty)
            {
                txtRevenue.Text = (txtTues.Text.ToDouble() * txtBaseRateExpected.Text.ToDouble()).ToString();
                RevenueRange();

            }
            txtBaseRateExpected.Focus();
        }

        protected void txtBaseRateExpected_TextChanged(object sender, EventArgs e)
        {
            if (txtBaseRateExpected.Text.Trim() == string.Empty && txtTues.Text.Trim() != string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Enter Base Rate Expected", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtRevenue.Text = "0";
                RevenueRange();
                txtBaseRateExpected.Focus();
                return;
            }
            if (txtBaseRateExpected.Text.Trim() == string.Empty && txtTues.Text.Trim() == string.Empty)
            {
                txtRevenue.Text = "0";
                RevenueRange();
                return;
            }
            if (txtTues.Text.Trim() != string.Empty && txtBaseRateExpected.Text.Trim() != string.Empty)
            {
                txtRevenue.Text = (txtTues.Text.ToDouble() * txtBaseRateExpected.Text.ToDouble()).ToString();
                RevenueRange();
            }
        }

        protected void sendMailForFF(string from, string to, string subject, string body)
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
                //lblMsg.Text = "Your Email has been sent sucessfully -Thank You";
            }
            catch (Exception exc)
            {
                //Response.Write("Send failure: " + exc.ToString());
                //lblMsg.Text = "Your Email Send failure";
            }
        }
        #endregion

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


        #region sendmailto approver
        bool sendMailToBaseRateApprover(long? LeadID)
        {
            try
            {
                WHLeadMasterData _whleadmasterdata = new WHLeadMasterData();
                IList<WhApproverMasterDTo> results = _whleadmasterdata.getBaseRateApproverMailID("100");
                if (results == null)
                {
                    return false;
                }


                foreach (var Multi in results)
                {
                    string subject = "Approved Request for Base Rate below: [ 550 ]";
                    string textBody = "Hi<br/><br/>";
                    textBody = textBody + "Sales Person <b>" + drpDesignatedBD.SelectedItem.Text + "</b> . has created a lead with Base Rate <b>[ " + txtBaseRateExpected.Text + "]</b> </br></br></br>";
                    textBody = textBody + "Please Approve </br></br></br>";

                    textBody = textBody + "<br/><br/><br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/BaseRateApproval.aspx?Type=A&WhLeadID=" + LeadID + "&ApproverID=" + Multi.ID + "><b><font size=5> Approve </font> </b></a> ";
                    textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/Operations/BaseRateApproval.aspx?Type=R&WhLeadID=" + LeadID + "&ApproverID=" + Multi.ID + "><b><font size=5> Reject </font> </b></a> ";


                    //textBody += "Regards,<br/><br/>";
                    textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                    //mFrom = Session["User_ID"].ToString();
                    mFrom = "crm@als.group";


                    mTo = "";//result.EmailID;
                    mBCC = Multi.EmailID;//"harish.kumar@biproex.com";
                    mSubject = subject;
                    mBody = textBody;
                    sendMailForFF(mFrom, mTo, mSubject, mBody);
                }




            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return true;

        }

        #endregion
        //protected void txtExpGrountRent_TextChanged(object sender, EventArgs e)
        //{
        //    if(txtAvgNoOfSty.Text.Trim()==string.Empty)
        //    {
        //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter No Of Stay", "Oops!", Toastr.ToastPosition.TopCenter, true);
        //        txtExpGrountRent.Text = string.Empty;
        //        txtRevenue.Text = string.Empty;
        //        return;
        //    }

        //    txtRevenue.Text=(txtAvgNoOfSty.Text.ToDouble()*txtExpGrountRent.Text.ToDouble()).ToString();
        //}

    }
}
