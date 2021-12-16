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
    public partial class FreightForwardingCrm : System.Web.UI.Page
    {
        long? WHLeadID;
        string strFileName;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }
                bindDrp();
                drpBU.SelectedValue = "2";
                FirstGridViewRow();
               
                if (RqId != null)
                {
                    setData();
                    bindGridData();
                    // enableFalse();
                    btnSubmit.Text = "Update";
                    btnCancel.Visible = false;
                    HfID.Value = RqId.ToString();

                    if(LovelySession.Lovely!=null)
                    {
                        if(LovelySession.Lovely.User.UserTypeId==1 || LovelySession.Lovely.User.UserTypeId==11)
                        {
                            btnSubmit.Visible = true;
                        }
                    }
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
            obj.LeadSource = txtLeadSource.Text.ToUpper();
            obj.CustomerName = drpCustomerName.SelectedValue.ToUpper();
            obj.Lineofbusiness = drpLineOfBusiness.SelectedValue;
            obj.OpportunityBrief = txtOppBrief.Text.ToUpper();
            obj.StatusStage = drpStatusStage.SelectedValue;
            obj.MonthlyBilling = txtMothlyBilling.Text.ToDouble();
            obj.GP = txtGP.Text.ToDouble();
            obj.ProjectETA = txtProjectEta.Text.ToConvertNullDateTime();
            obj.DesignatedBD = drpDesignatedBD.SelectedValue;
            obj.StatusUpdate = txtStatusUpdate.Text.ToUpper();
            obj.CreateBy = LovelySession.Lovely.User.Id.ToString();
            obj.New_Encirclement = drpNewEncirclement.SelectedValue;
            obj.BU = drpBU.SelectedValue;
            obj.ContactPersonName = txtContactPersonName.Text.ToUpper();
            obj.ContactPersonDesignation = txtContactPersonDesignation.Text.ToUpper();
            obj.ContactPersonMailID = txtContactPersonMailID.Text;
            obj.ContactPersonPhoneNo = txtContactPhoneNo.Text;
            obj.ProjectATA = txtProjectATA.Text.ToConvertNullDateTime();
            obj.Region = drpRegion.SelectedValue;
            obj.Segment = txtSegment.Text;
            obj.LocationFrom = txtLocationFrom.Text.ToUpper();
            obj.LocationTo = txtLocationTo.Text.ToUpper();
            obj.TradeLane = drpTradeLane.SelectedValue.ToUpper();
            obj.ValueAdded = checkedCompanyList();
            obj.Unit = drpUnit.SelectedValue;
            obj.Qty = txtQty.Text.ToNullDouble();
            obj.FileName = strFileName;
            obj.UserTypeID = LovelySession.Lovely.User.UserTypeId;
            obj.RevenueRange = txtRevenueRange.Text.ToString();
            obj.Rate = txtRate.Text.ToNullDouble();
            obj.CFS = DrpCfs.SelectedValue;
            obj.Shipment = txtShipment.Text.ToNullLong();
            obj.IncoTerms = drpIncoterms.SelectedValue;
            obj.Stage = drpStage.SelectedValue;
            obj.LostRemarks = txtLostRemarks.Text.ToString();
            obj.BranchMasterID = drpBranch.SelectedValue.ToNullLong();
            obj.AddrsTransID = getAddressTrasID().ToNullLong();

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

            if (drpTradeLane.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Trade Lane.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                drpTradeLane.Focus();
                return false;
            }
            if (drpLineOfBusiness.SelectedValue != string.Empty)
            {
                if (drpLineOfBusiness.SelectedValue != "Value Added")
                {
                    if (txtLocationFrom.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location From.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        txtLocationFrom.Focus();
                        return false;
                    }
                    else if (txtLocationTo.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Location To.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        txtLocationTo.Focus();
                        return false;
                    }

                     if (txtRate.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Rate.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        drpUnit.Focus();
                        return false;
                    }
                     if (txtQty.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Qty.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        txtQty.Focus();
                        return false;
                    }
                     if (drpUnit.SelectedValue == string.Empty)
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
             HttpPostedFile myFile = FileUpload.PostedFile;
            int nFileLen = myFile.ContentLength;
            if (5242880 < nFileLen)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "File size can't be more then 5 MB.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            //if(txtShipment.Text==string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Shipment.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            //if (DrpCfs.SelectedValue == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select CFS/ICD.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            if(drpStatusStage.SelectedValue=="7")
            {
                if (drpStage.SelectedValue == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Status.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                if (drpStage.SelectedValue == "Cancelled" || drpStage.SelectedValue == "Lost")
                {
                    if (txtLostRemarks.Text == string.Empty)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "In case of stage Lost/Cancelled Remarks is mandatory.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        LostRemarks.Focus();
                        return false;
                    }
                }
            }
           
           
            if (drpIncoterms.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select IncoTerms.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            if (drpBranch.SelectedValue == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Branch.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            return true;
        }
        void clear()
        {
            txtLeadSource.Text = string.Empty;
            drpCustomerName.SelectedValue = string.Empty;
            drpLineOfBusiness.SelectedValue = string.Empty;
            txtOppBrief.Text = string.Empty;
            drpStatusStage.SelectedValue = string.Empty;
            txtMothlyBilling.Text = string.Empty;
            txtGP.Text = string.Empty;
            txtProjectEta.Text = string.Empty;
            drpDesignatedBD.SelectedValue = string.Empty;
            txtStatusUpdate.Text = string.Empty;
           // drpBU.SelectedValue = string.Empty;
            drpNewEncirclement.SelectedValue = string.Empty;
            txtContactPersonName.Text = string.Empty;
            txtContactPersonMailID.Text = string.Empty;
            txtContactPersonDesignation.Text = string.Empty;
            txtContactPhoneNo.Text = string.Empty;
            drpRegion.SelectedValue = string.Empty;
            drpSegment.SelectedValue = string.Empty;
            txtLocationFrom.Text = string.Empty;
            txtLocationTo.Text = string.Empty;
            drpTradeLane.SelectedValue = string.Empty;
            drpValueAddedList.ClearSelection();
            drpUnit.SelectedValue = string.Empty;
            txtQty.Text = string.Empty;
            txtLocationFrom.Visible=false;
            txtLocationTo.Visible=false;
            drpUnit.Visible = false;
            txtQty.Visible = false;
            drpValueAddedList.Visible = false;
            lblLocationFrom.Visible = false;
            lblLocationStar.Visible = false;
            lblLocationTo.Visible = false;
            lblLocationToStar.Visible = false;
            lblQty.Visible = false;
            lblQtyStar.Visible = false;
            lblUnit.Visible = false;
            lblUnitStar.Visible = false;
            lblValueAddedService.Visible = false;
            lblValueAddedStar.Visible = false;
            txtProjectATA.Text = string.Empty;
            txtSegment.Text = string.Empty;
            txtRevenueRange.Text = string.Empty;
            drpBranch.SelectedValue = string.Empty;


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

                txtLocationFrom.Text = result.LocationFrom;
                txtLocationTo.Text = result.LocationTo;
                drpTradeLane.SelectedValue = result.TradeLane;

                //if(txtLocationFrom.Text==string.Empty)
                //{
                //    txtLocFrom.Text = result.LocationFrom;
                //}
                //if(txtLocationTo.Text==string.Empty)
                //{
                //    txtLocTo.Text = result.LocationTo;
                //}
                if(drpTradeLane.SelectedValue==string.Empty)
                {
                    txtTradelane.Text = result.TradeLane;
                }
                if(drpStatusStage.SelectedValue==string.Empty)
                {
                    txtCrmStage.Text = result.StatusStage;
                }

                txtQty.Text = result.Qty.ToString();
                drpUnit.SelectedValue = result.Unit;
                txtRate.Text = result.Rate.ToString();
                DrpCfs.SelectedValue = result.CFS;
                drpIncoterms.SelectedValue = result.IncoTerms;
                txtShipment.Text = result.Shipment.ToString();
                drpStage.SelectedValue = result.Stage;
                if(drpStage.SelectedValue=="Cancelled" || drpStage.SelectedValue == "Lost")
                {
                    LostRemarks.Visible = true;
                }

                if (result.RevenueRange != string.Empty)
                {
                    txtRevenueRange.Text = result.RevenueRange.ToString();
                }
                else
                {
                    RevenueRange();
                }
                if (result.ValueAdded != null)
                {
                    string[] ValueAdded = result.ValueAdded.Split(',');
                    foreach (ListItem item in drpValueAddedList.Items)
                    {
                        for(int i=0;i<ValueAdded.Length;i++)
                        {
                            if(item.Value== ValueAdded[i])
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                if (result.ProjectATA != null)
                    txtProjectATA.Text = result.ProjectATA.Value.ToString("dd MMM yyyy");


                if (result.StatusStage == "7")
                {
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    gvStatusUpdate.Enabled = false;
                    txtProjectATA.Enabled = false;
                    drpStatusStage.Enabled = false;
                    txtMothlyBilling.Enabled = false;
                    txtGP.Enabled = false;
                }
                if (LovelySession.Lovely.User.UserTypeId == 9 || LovelySession.Lovely.User.UserTypeId == 1)
                {
                    btnSubmit.Visible = true;
                    gvStatusUpdate.Enabled = true;
                }
                drpLineOfBusiness.Enabled = false;
                if (result.Lineofbusiness != "Value Added")
                {
                    txtLocationFrom.Visible = true;
                    lblLocationStar.Visible = true;
                    lblLocationFrom.Visible = true;
                    txtLocationTo.Visible = true;
                    lblLocationToStar.Visible = true;
                    lblLocationTo.Visible = true;
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
                    lblRateStar.Visible = true;
                    lblRate.Visible = true;
                    txtRate.Visible = true;
                    txtMothlyBilling.Enabled = false;
                }
                else
                {
                    txtLocationFrom.Visible = false;
                    lblLocationStar.Visible = false;
                    txtLocationTo.Visible = false;
                    lblLocationToStar.Visible = false;
                    lblValueAddedService.Visible = true;
                    lblValueAddedStar.Visible = true;
                    drpValueAddedList.Visible = true;
                    lblLocationFrom.Visible = false;
                    lblLocationTo.Visible = false;
                    lblQty.Visible = false;
                    lblQtyStar.Visible = false;
                    txtQty.Visible = false;
                    lblUnit.Visible = false;
                    lblUnitStar.Visible = false;
                    drpUnit.Visible = false;
                    divScroll.Visible = true;
                    lblRateStar.Visible = false;
                    lblRate.Visible = false;
                    txtRate.Visible = false;
                    txtMothlyBilling.Enabled = true;
                }

                drpBranch.SelectedValue = result.BranchMasterID.ToString();

                bindAddressGrid(drpCustomerName.SelectedValue);

                if(result.MonthlyBilling!=0)
                {
                    txtMothlyBilling.Enabled = false;
                    txtQty.Enabled = false;
                    txtRate.Enabled = false;
                    drpUnit.Enabled = false;
                }

                //if (result.StatusStage == "5" || result.StatusStage == "6" || result.StatusStage == "7" || result.StatusStage == "8")
                //{
                //    txtMothlyBilling.Enabled = false;
                //}
                if (result.StatusStage == "7")
                {
                    divLeadStatus.Visible = true;
                }
                else
                {
                    divLeadStatus.Visible = false;
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
            txtStatusUpdate.Enabled = false;
            drpBU.Enabled = false;
            drpNewEncirclement.Enabled = false;
            txtContactPersonName.Enabled = false;
            txtContactPersonMailID.Enabled = false;
            txtContactPersonDesignation.Enabled = false;
            txtContactPhoneNo.Enabled = false;
            drpRegion.Enabled = false;
            drpSegment.Enabled = false;
           txtLocationFrom.Enabled = false;
           txtLocationTo.Enabled = false;
            drpTradeLane.Enabled = false;
            drpValueAddedList.Enabled = false;
        }
        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMasterData = new BDSolutionMasterData();
            IList<BDSolutionMasterDto> AllFFBD = _bdSolutionMasterData.GetAllFFBD().OrderBy(x => x.BD).ToList();

            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<WhCustomerMasterDto> customerResults = _customerMasterData.GetAll("FF");
            if (customerResults != null)
            {

                drpCustomerName.DataSource = customerResults;
                drpCustomerName.DataValueField = "ID";
                drpCustomerName.DataTextField = "Name";
                drpCustomerName.DataBind();
                drpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));


            }
            if (AllFFBD != null)
            {
                drpDesignatedBD.DataSource = AllFFBD;
                drpDesignatedBD.DataValueField = "ID";
                drpDesignatedBD.DataTextField = "FFBD";
                drpDesignatedBD.DataBind();
                drpDesignatedBD.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

            WHLeadMasterData _leadData = new WHLeadMasterData();
            IList<WHLeadMasterDto> getSegment = _leadData.getSegment(LovelySession.Lovely.User.UserTypeId, "FF");
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
            //IList<WHLeadStatusUpdateDto> CountryData = _leadData.getCountryData();
            //if(CountryData!=null)
            //{
            //    drpLocationFrom.DataSource = CountryData;
            //    drpLocationFrom.DataValueField = "ID";
            //    drpLocationFrom.DataTextField = "Name";
            //    drpLocationFrom.DataBind();
            //    drpLocationFrom.Items.Insert(0, new ListItem("--Select--", string.Empty));

            //    drpLocationTo.DataSource = CountryData;
            //    drpLocationTo.DataValueField = "ID";
            //    drpLocationTo.DataTextField = "Name";
            //    drpLocationTo.DataBind();
            //    drpLocationTo.Items.Insert(0, new ListItem("--Select--", string.Empty));
            //}

            _leadData = new WHLeadMasterData();
            IList<WHLeadStatusUpdateDto> CrmStageData = _leadData.getCrmStageData();
            IList<WHLeadStatusUpdateDto> CrmTradelaneData = _leadData.getCrmTradelaneData();
            if(CrmStageData!=null)
            {
                drpStatusStage.DataSource = CrmStageData;
                drpStatusStage.DataValueField = "ID";
                drpStatusStage.DataTextField = "Name";
                drpStatusStage.DataBind();
                drpStatusStage.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            if (CrmTradelaneData != null)
            {
                drpTradeLane.DataSource = CrmTradelaneData;
                drpTradeLane.DataValueField = "ID";
                drpTradeLane.DataTextField = "Name";
                drpTradeLane.DataBind();
                drpTradeLane.Items.Insert(0, new ListItem("--Select--", string.Empty));

               // drpTradeLane.Items[0].Selected = true;
                //drpTradeLane.Items[3].Attributes["disabled"] = "disabled";


                //for(int i=0;i<CrmTradelaneData.Count;i++)
                //{
                //    drpTradeLane.Items[i].Attributes["disabled"] = "disabled";
                //}
            }

            ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            IList<WhBuMasterDto> results = _serviceTypeMasterData.BUGetAll();
            if(results!=null)
            {
                drpBU.DataSource = results;
                drpBU.DataValueField = "ID";
                drpBU.DataTextField = "Name";
                drpBU.DataBind();
                drpBU.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }

            WhBranchMasterData _whBranchMasterData = new WhBranchMasterData();
            IList<WhBranchMasterDto> GetAllBranch = _whBranchMasterData.GetAll();
            if(GetAllBranch!=null)
            {
                drpBranch.DataSource = GetAllBranch;
                drpBranch.DataValueField = "ID";
                drpBranch.DataTextField = "Name";
                drpBranch.DataBind();
                drpBranch.Items.Insert(0, new ListItem("--Select--", string.Empty));
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

        string getAddressTrasID()
        {
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                RadioButton rb = (RadioButton)gvRow.FindControl("rb");
                Label gvlblID = (Label)gvRow.FindControl("gvlblID");
               

                if (rb.Checked == true)
                {
                    return gvlblID.Text;
                }
            }

            return string.Empty;
        }

        void bindAddressGrid(string AddressID)
        {
            WhCustomerMasterData _customerMasterData = new WhCustomerMasterData();
            IList<WhCustomerAddTransDto> addressResult = _customerMasterData.GetAllAddressByCustomerID(AddressID.ToNullLong());
            if(addressResult!=null)
            {
                gvAddress.DataSource = addressResult;
                gvAddress.DataBind();
            }
            else
            {
                gvAddress.DataBind();
            }
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                Label gvlblID = (Label)gvRow.FindControl("gvlblID");
                RadioButton rb = (RadioButton)gvRow.FindControl("rb");

                if (gvlblID.Text == AddressID)
                {
                    rb.Checked=true;
                }
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
                    saveStatusGridData();
                    sendMail();
                    bindDrp();
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
                if (_leadData.Update(request))
                {
                    saveStatusGridData();
                    sendMail();
                    //bindDrp();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Operations/FreightForwardingList.aspx");
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
            txtLostRemarks.Text = string.Empty;
            drpStage.SelectedValue = string.Empty;
            if (drpStatusStage.SelectedValue == "1" || drpStatusStage.SelectedValue == "2" || drpStatusStage.SelectedValue ==string.Empty)
            {
                lblGpStar.Visible = false;
                lblMonthlyStar.Visible = false;
                lblProjectEtaStar.Visible = false;
                lblQtyStar.Visible = false;
                lblRateStar.Visible = false;
                lblUnitStar.Visible = false;

            }
            else
            {
                lblGpStar.Visible = true;
                lblMonthlyStar.Visible = true;
                lblProjectEtaStar.Visible = true;
                lblQtyStar.Visible = true;
                lblRateStar.Visible = true;
                lblUnitStar.Visible = true;
            }

            if (drpStatusStage.SelectedValue == "6")
            {
                lblProjectATAStar.Visible = true;
            }
            else
            {
                lblProjectATAStar.Visible = false;
            }
            if (drpStatusStage.SelectedValue == "7")
            {
                divLeadStatus.Visible = true;
                txtLostRemarks.Visible = true;
            }
            else
            {
                divLeadStatus.Visible = false;
                txtLostRemarks.Visible = false;
            }

        }

        protected void drpLineOfBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpLineOfBusiness.SelectedValue==string.Empty)
            {
                txtLocationFrom.Visible = false;
                lblLocationStar.Visible = false;
                txtLocationTo.Visible = false;
                lblLocationToStar.Visible = false;
                lblQty.Visible = false;
                lblQtyStar.Visible = false;
                txtQty.Visible = false;
                lblUnit.Visible = false;
                lblUnitStar.Visible = false;
                drpUnit.Visible = false;
                lblValueAddedService.Visible = false;
                lblValueAddedStar.Visible = false;
                drpValueAddedList.Visible = false;
                lblLocationFrom.Visible = false;
                lblLocationTo.Visible = false;
                divScroll.Visible = false;
                txtLocationFrom.Text = string.Empty;
                txtLocationTo.Text = string.Empty;
                txtQty.Text = string.Empty;
                drpUnit.SelectedValue = string.Empty;
                lblRateStar.Visible = false;
                lblRate.Visible = false;
                txtRate.Visible = false;
                txtMothlyBilling.Enabled = true;
                txtRate.Text = string.Empty;
                txtMothlyBilling.Text = string.Empty;
                txtRevenueRange.Text = string.Empty;
            }
            if (drpLineOfBusiness.SelectedValue != string.Empty)
            {
                if (drpLineOfBusiness.SelectedValue != "Value Added")
                {
                    txtLocationFrom.Visible = true;
                    lblLocationStar.Visible = true;
                    lblLocationFrom.Visible = true;
                    txtLocationTo.Visible = true;
                    lblLocationToStar.Visible = true;
                    lblLocationTo.Visible = true;
                    lblQty.Visible = true;
                   // lblQtyStar.Visible = true;
                    txtQty.Visible = true;
                    lblUnit.Visible = true;
                   // lblUnitStar.Visible = true;
                    drpUnit.Visible = true;
                    lblValueAddedService.Visible = false;
                    lblValueAddedStar.Visible = false;
                    drpValueAddedList.Visible = false;
                    divScroll.Visible = false;
                    //lblRateStar.Visible = true;
                    lblRate.Visible = true;
                    txtRate.Visible = true;
                    txtMothlyBilling.Enabled = false;
                    txtRate.Text = string.Empty;
                    txtMothlyBilling.Text = string.Empty;
                    txtRevenueRange.Text = string.Empty;


                    drpValueAddedList.ClearSelection();
                }
                else
                {
                    txtLocationFrom.Visible = false;
                    lblLocationStar.Visible = false;
                   txtLocationTo.Visible = false;
                    lblLocationToStar.Visible = false;
                    lblQty.Visible = false;
                    lblQtyStar.Visible = false;
                    txtQty.Visible = false;
                    lblUnit.Visible = false;
                    lblUnitStar.Visible = false;
                    drpUnit.Visible = false;
                    lblValueAddedService.Visible = true;
                    lblValueAddedStar.Visible = true;
                    drpValueAddedList.Visible = true;
                    lblLocationFrom.Visible = false;
                    lblLocationTo.Visible = false;
                    divScroll.Visible = true;
                    txtLocationFrom.Text = string.Empty;
                    txtLocationTo.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    drpUnit.SelectedValue = string.Empty;
                    lblRateStar.Visible = false;
                    lblRate.Visible = false;
                    txtRate.Visible = false;
                    txtMothlyBilling.Enabled = true;
                    txtRate.Text = string.Empty;
                    txtMothlyBilling.Text = string.Empty;
                    txtRevenueRange.Text = string.Empty;
                }

            }
            else
            {
                txtLocationFrom.Visible = false;
                lblLocationStar.Visible = false;
                txtLocationTo.Visible = false;
                lblLocationToStar.Visible = false;
                lblValueAddedService.Visible = false;
                lblValueAddedStar.Visible = false;
                drpValueAddedList.Visible = false;
                lblLocationFrom.Visible = false;
                lblLocationTo.Visible = false;
                lblQty.Visible = false;
                lblQtyStar.Visible = false;
                txtQty.Visible = false;
                lblUnit.Visible = false;
                lblUnitStar.Visible = false;
                drpUnit.Visible = false;
                txtLocationFrom.Text = string.Empty;
                txtLocationTo.Text = string.Empty;
                txtQty.Text = string.Empty;
                drpUnit.SelectedValue = string.Empty;
                drpValueAddedList.ClearSelection();
                divScroll.Visible = false;
                txtRate.Text = string.Empty;
                txtMothlyBilling.Text = string.Empty;
                txtRevenueRange.Text = string.Empty;
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
        public void sendMail()
        {
            try
            {

                string subject = "Lead Details";

                string textBody = "Please find the Lead Details,<br/><br/>";

                textBody = textBody + " <table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
                textBody = textBody + "<tr> <td> <b>Lead Source:</b><hr>" + txtLeadSource.Text.ToUpper() + "</td><td> <b>Customer Name: </b><hr>" + drpCustomerName.SelectedItem.Text.ToUpper() + "</td><td> <b>Type of Business:</b> <hr>" + drpNewEncirclement.SelectedItem.Text.ToUpper() + "</td><td><b> BU:</b> <hr>" + drpBU.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td> <b>Project ETA:</b><hr>" + txtProjectEta.Text.ToUpper() + "</td><td> <b>Project ATA: </b><hr>" + txtProjectATA.Text.ToUpper() + "</td><td><b> Designated BD: </b><hr>" + drpDesignatedBD.SelectedItem.Text.ToUpper() + "</td><td> <b>Line Of Business:</b> <hr>" + drpLineOfBusiness.SelectedItem.Text.ToUpper() + "</td></tr>";
                if (drpLineOfBusiness.SelectedItem.Text != "Value Added")
                {
                    textBody = textBody + "<tr> <td> <b>Trade Lane: </b><hr>" + drpTradeLane.SelectedItem.Text.ToUpper() + "</td><td> <b>Location From:</b> <hr>" + txtLocationFrom.Text.ToUpper() + "</td><td><b> Location To:</b> <hr>" + txtLocationTo.Text.ToUpper() + "</td><td> <b>Qty:</b> <hr>" + txtQty.Text.ToUpper() + "</td></tr>";
                    textBody = textBody + "<tr> <td> <b>Unit:</b> <hr>" + drpUnit.SelectedItem.Text.ToUpper() + "</td><td><b> CRM Stage:</b> <hr>" + drpStatusStage.SelectedItem.Text.ToUpper() + "</td><td colspan='2'> <b>Monthly Billing:</b> <hr>" + txtMothlyBilling.Text + "</td></tr>";
                }
                else
                {
                    textBody = textBody + "<tr> <td> <b>Trade Lane:</b> <hr>" + drpTradeLane.SelectedItem.Text.ToUpper() + "</td><td> <b>CRM Stage:</b> <hr>" + drpStatusStage.SelectedItem.Text.ToUpper() + "</td><td> <b>Monthly Billing:</b> <hr>" + txtMothlyBilling.Text.ToUpper() + "</td><td> <b>Value Added List:</b> <hr>" + checkedCompanyList() + "</td></tr>";
                   // textBody = textBody + "<tr> <td colspan='4'> <b>Value Added List:</b> <hr>" + checkedCompanyList() + "</td></tr>";
                }
                textBody = textBody + "<tr> <td> <b>GP(%):</b> <hr>" + txtGP.Text + "</td><td> <b>Opportunity Brief:</b> <hr>" + txtOppBrief.Text.ToUpper() + "</td><td> <b>Region:</b> <hr>" + drpRegion.SelectedItem.Text.ToUpper() + "</td><td> <b>Segment: </b><hr>" + drpSegment.SelectedItem.Text.ToUpper() + "</td></tr>";
                textBody = textBody + "<tr> <td colspan='4'> <h2>Contact Person Details </h2></td></tr>";
                textBody = textBody + "<tr> <td><b> Name:</b><hr>" + txtContactPersonName.Text.ToUpper() + "</td><td> <b>Designation:</b><hr>" + txtContactPersonDesignation.Text.ToUpper() + "</td><td><b> Mail ID:</b><hr>" + txtContactPersonMailID.Text.ToUpper() + "</td><td><b> Phone No:</b><hr>" + txtContactPhoneNo.Text + "</td></tr>";
                textBody = textBody + "<tr> <td colspan='4'><table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
                textBody = textBody + "<tr> <td><b> Status</b> </td> <td><b>Date</b></td></tr>";
                foreach (GridViewRow gvRow in gvStatusUpdate.Rows)
                {
                    TextBox gvtxtStatusUpdate = (TextBox)gvRow.FindControl("gvtxtStatusUpdate");
                    Label lblDate = (Label)gvRow.FindControl("gvlblDate");
                    textBody += "<tr><td style='word-wrap: break-word'>" + gvtxtStatusUpdate.Text + "</td><td style='word-wrap: break-word'>" + lblDate.Text + "</td></tr>";
                }
                textBody = textBody + "</table>  </td></tr>";
                textBody += "</table> <br/> <br/>";

                //textBody += "Regards,<br/><br/>";
                textBody += "This is system generated mail, Please don't reply.";

                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = "";
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                mBCC = "harishsangwan23@gmail.com,soma.s@als.group,shivani.r@als.group";
                mSubject = subject;
                mBody = textBody;
                sendMailForFF(mFrom, mTo, mSubject, mBody);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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

        protected void txtMothlyBilling_TextChanged(object sender, EventArgs e)
        {
            RevenueRange();
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
                
                txtSegment.Focus();
            }
            txtSegment.Enabled = false;
        }

        protected void drpStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpStage.SelectedValue == "Cancelled" || drpStage.SelectedValue == "Lost")
            {
                LostRemarks.Visible = true;
            }
            else
            {
                LostRemarks.Visible = false;
            }
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            if(txtQty.Text==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Qty.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtRate.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtMothlyBilling.Text = string.Empty;
                return;
            }
            else if(txtRate.Text==string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Rate.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                txtRate.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtMothlyBilling.Text = string.Empty;
                return;
            }

            txtMothlyBilling.Text = (txtRate.Text.ToDouble() * txtQty.Text.ToDouble()).ToString();
            RevenueRange();
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
            foreach(GridViewRow gvRow in gvAddress.Rows)
            {
                RadioButton rb = (RadioButton)gvRow.FindControl("rb");

                if(rb.Checked==true)
                {
                    count = 1;
                }
            }
            if(count==0)
            {
                lblErrorMsg.Text = "Please select address for this customer!!";
                mp1.Show();
            }
            else
            {
                mp1.Hide();
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text == string.Empty)
            {
                txtMothlyBilling.Text = string.Empty;
                return;
            }
            else if (txtRate.Text == string.Empty)
            {
                txtMothlyBilling.Text = string.Empty;
                return;
            }

            txtMothlyBilling.Text = (txtRate.Text.ToDouble() * txtQty.Text.ToDouble()).ToString();
            RevenueRange();
        }



    }
}
