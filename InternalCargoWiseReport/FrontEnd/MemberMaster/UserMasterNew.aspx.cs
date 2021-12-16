﻿using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.MemberMaster
{
    public partial class UserMasterNew : System.Web.UI.Page
    {
        #region Properties
        UserData _userData = null;
        RoleData _roleData = null;
        StateData _stateData = null;
        CityData _cityData = null;

        private long Id = 0;
        private string UserTypeId { get { return ddlMemberType.SelectedValue; } set { ddlMemberType.SelectedValue = value; } }
        private string BranchId { get { return ddlBranch.SelectedValue; } set { ddlBranch.SelectedValue = value; } }
        private string RankId { get { return ddlRank.SelectedValue; } set { ddlRank.SelectedValue = value; } }
        private string ApplicantName { get { return txtName.Value; } set { txtName.Value = value; } }
        private string DOB { get { return datepicker.Text; } set { datepicker.Text = value; } }
        private string IntroducerCode { get { return "CWR"; } set { txtIntroducer.Text = value; } }
        private string IntroducerName { get { return "CWR"; } set { txtIName.Value = value; } }
        private string DirectCode { get { return txtDirect.Text; } set { txtDirect.Text = value; } }
        private string DirectName { get { return txtDirectName.Value; } set { txtDirectName.Value = value; } }
        private string EpinName { get { return txtPin.Text; } set { txtPin.Text = value; } }
        private string Gender { get { return ddlGender.SelectedValue; } set { ddlGender.SelectedValue = value; } }
        private string MaritalStatus { get { return ddlMarital.SelectedValue; } set { ddlMarital.SelectedValue = value; } }
        private string Mobile { get { return txtMobile.Text; } set { txtMobile.Text = value; } }
        private string Email { get { return txtEmail.Text; } set { txtEmail.Text = value; } }
        private string Pan { get { return txtPan.Value; } set { txtPan.Value = value; } }
        private string FatherName { get { return txtGuardian.Value; } set { txtGuardian.Value = value; } }
        private string Address { get { return txtAddress.Value; } set { txtAddress.Value = value; } }
        private string StateId { get { return ddlState.SelectedValue; } set { ddlState.SelectedValue = value; } }
        private string CityId { get { return ddlCity.SelectedValue; } set { ddlCity.SelectedValue = value; } }
        private string PinCode { get { return txtZipCode.Value; } set { txtZipCode.Value = value; } }
        private string BankId { get { return ddlBank.SelectedValue; } set { ddlBank.SelectedValue = value; } }
        private string BankBranch { get { return txtBranch.Value; } set { txtBranch.Value = value; } }
        private string AccountNo { get { return txtAccount.Value; } set { txtAccount.Value = value; } }
        private string IFSCode { get { return txtIFSC.Value; } set { txtIFSC.Value = value; } }
        private string NomineeName { get { return txtNName.Value; } set { txtNName.Value = value; } }
        private string NomineeAddress { get { return txtNAddress.Value; } set { txtNAddress.Value = value; } }
        private string NomineeCity { get { return ddlNCity.SelectedValue; } set { ddlNCity.SelectedValue = value; } }
        private string NomineeAge { get { return txtNAge.Value; } set { txtNAge.Value = value; } }
        private string NomineePinCode { get { return txtNZip.Value; } set { txtNZip.Value = value; } }
        private string RelationId { get { return ddlRelation.SelectedValue; } set { ddlRelation.SelectedValue = value; } }
        private string DocumentType { get { return ddlDocument.SelectedValue; } set { ddlDocument.SelectedValue = value; } }
        private string DocumentNo { get { return txtDocumentNo.Value; } set { txtDocumentNo.Value = value; } }
        private string PlanId { get { return ddlPlan.SelectedValue; } set { ddlPlan.SelectedValue = value; } }
        private string PlanAmount { get { return txtPlanAmount.Value; } set { txtPlanAmount.Value = "0"; } }
        private string PlanDescription { get { return txtPlanDescription.Value; } set { txtPlanDescription.Value = "PP"; } }
        string RqId { get { return Request["requestId"]; } }
        #endregion

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindPageLoad();
                if (RqId != null)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                    {
                        _userData = new UserData();
                        UserDto result = _userData.GetById(Convert.ToInt32(RqId));
                        SetProperties(result);
                    }
                }
            }

        }
        private void BindDropDown()
        {
            _roleData = new RoleData();
            _stateData = new StateData();
            _cityData = new CityData();
            _userData = new UserData();



            IList<RoleDto> results = _roleData.GetAll(true).Where(x => x.RoleId == 10 || x.RoleId == 12 || x.RoleId == 14 || x.RoleId == 16 || x.RoleId == 21 || x.RoleId == 26).ToList();
            if (results != null)
            {
                ddlMemberType.DataSource = results;
                ddlMemberType.DataValueField = "RoleId";
                ddlMemberType.DataTextField = "RoleName";
                ddlMemberType.DataBind();
                ddlMemberType.Items.Insert(0, new ListItem("Please Select User Type", ""));
            }


            ddlState.DataSource = _stateData.GetAll(true);
            ddlState.DataValueField = "StateId";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Please Select State", ""));

            ddlCity.DataSource = _cityData.GetAll(true);
            ddlCity.DataValueField = "CityId";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("Please Select City", ""));

            ddlNCity.DataSource = _cityData.GetAll(true);
            ddlNCity.DataValueField = "CityId";
            ddlNCity.DataTextField = "CityName";
            ddlNCity.DataBind();
            ddlNCity.Items.Insert(0, new ListItem("Please Select City", ""));


            ddlRank.DataSource = _userData.GetCompanyDivision();
            ddlRank.DataValueField = "CompanyDivisionId";
            ddlRank.DataTextField = "CompanyDivisionName";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("Please Select Division", ""));

            txtRDate.Value = Utility.GetIndianDateTime().ToString("dd/MM/yyyy");
            //BranchId = LovelySession.Lovely.User.BranchId.ToString();

            IList<UserDto> AllRptMgr = _userData.getAllRptMgr();

            if (AllRptMgr != null)
            {
                drpRptMgr.DataSource = AllRptMgr;
                drpRptMgr.DataValueField = "ID";
                drpRptMgr.DataTextField = "Name";
                drpRptMgr.DataBind();
                drpRptMgr.Items.Insert(0, new ListItem("Please Select Reporting Manager", ""));
            }


        }
        private void BindPageLoad()
        {
            BindDropDown();
            lblRank.Visible = false;
            lblDirectCode.Visible = false;
            lblDirectName.Visible = false;
            lblEpin.Visible = false;
            lblDocument.Visible = false;
            lblDocumentNo.Visible = false;
            lblPlanAmount.Visible = false;
            lblPlanDescription.Visible = false;
            lblPlan.Visible = false;
            lblPlanDescription.Visible = false;

            if (LovelyGlobal.UserMaster_RankDropdown == 1)
            { lblRank.Visible = true; }
            if (LovelyGlobal.UserMaster_DirectCode == 1)
            { lblDirectCode.Visible = true; lblDirectName.Visible = true; }
            if (LovelyGlobal.UserMaster_Epin == 1)
            { lblEpin.Visible = true; }
            if (LovelyGlobal.UserMaster_Document == 1)
            { lblDocument.Visible = false; lblDocumentNo.Visible = false; }
        }
        private void SetProperties(UserDto obj)
        {
            Id = Convert.ToInt64(obj.BankId);
            ApplicantName = obj.Name;
            IntroducerCode = obj.UplineCode;
            if (obj.UserTypeId != 0)
                UserTypeId = obj.UserTypeId.ToString();
            if (obj.BranchId != 0)
                BranchId = obj.BranchId.ToString();
            if (obj.RankId != 0)
                RankId = obj.RankId.ToString();
            DOB = Convert.ToDateTime(obj.Birthday).ToString("yyyy-MM-dd");
            PlanAmount = obj.IntroducerCode;
            txtRDate.Value = Convert.ToDateTime(obj.JoiningDate).ToString("yyyy-MM-dd");
            DirectCode = obj.UplineCode;
            EpinName = obj.PinNumber;
            if (obj.Gender != null)
                Gender = obj.Gender.ToString();
            if (obj.MaritalStatus != null)
                MaritalStatus = obj.MaritalStatus.ToString();
            Mobile = obj.MobileNo;
            Email = obj.EmailId;
            Pan = obj.PanCardNo;
            FatherName = obj.GuardianName;
            Address = obj.Address;
            if (obj.StateId.HasValue && obj.StateId > 0)
                StateId = obj.StateId.ToString();
            if (obj.CityId.HasValue && obj.CityId > 0)
                CityId = obj.CityId.ToString();
            PinCode = obj.ZipCode;
            if (obj.BankId.HasValue && obj.BankId > 0)
                BankId = obj.BankId.ToString();
            BankBranch = obj.BankAddress;
            AccountNo = obj.AccountNo;
            IFSCode = obj.Ifscode;
            NomineeName = obj.NomineeName;
            NomineeAge = obj.NomineeAge.ToString();
            NomineeAddress = obj.NomineeAddress;
            if (obj.NomineeCityId.HasValue && obj.NomineeCityId > 0)
                NomineeCity = obj.NomineeCityId.ToString();
            if (obj.NomineeRelationId.HasValue && obj.NomineeRelationId > 0)
                RelationId = obj.NomineeRelationId.ToString();
            NomineePinCode = obj.NomineeZipCode.ToString();
            if (obj.PlanId.HasValue && obj.PlanId > 0)
                PlanId = obj.PlanId.ToString();
            lblEpin.Visible = false;
            lblDirectName.Visible = false;
            lblDirectCode.Visible = false;
            lblIntroName.Visible = false;
            if (!string.IsNullOrEmpty(obj.UplineCode))
            {
                txtIntroducer.Enabled = false;
            }
            else
            {
                txtIntroducer.Enabled = true;
            }
            txtPlanDescription.InnerText = obj.PinNumber;
            drpRptMgr.SelectedValue = obj.RptMgrID.ToString();
            chkIsCrm.Checked = obj.IsCRM;
        }
        #endregion

        #region Clear
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            UserTypeId = null;
            BranchId = null;
            RankId = null;
            ApplicantName = null;
            DOB = null;
            IntroducerCode = null;
            IntroducerName = null;
            DirectCode = null;
            DirectName = null;
            EpinName = null;
            Mobile = null;
            Email = null;
            Pan = null;
            FatherName = null;
            Address = null;
            StateId = null;
            CityId = null;
            PinCode = null;
            BankId = null;
            BankBranch = null;
            IFSCode = null;
            NomineeName = null;
            NomineeCity = null;
            NomineeAge = null;
            NomineeAddress = null;
            NomineePinCode = null;
            RelationId = null;
            DocumentNo = null;
            PlanDescription = null;
            PlanId = null;
            PlanAmount = null;
            chkIsCrm.Checked = false;
            drpRptMgr.SelectedValue = string.Empty;
            chkIsBD.Checked = false;
            txtApproverMailID.Text = string.Empty;
        }
        #endregion

        #region Post Back Change
        protected void txtDirect_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(DirectCode))
            //{
            //    string DirectorName = ValidIntroducer(DirectCode);
            //    if (!string.IsNullOrEmpty(DirectorName))
            //    {
            //        DirectName = DirectorName;
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //    }
            //    else
            //    {
            //        DirectName = null;
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Direct Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    }
            //}
            //else
            //{
            //    if (LovelyGlobal.UserMaster_PositionThree == 0)
            //    { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Direct Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //}
        }
        protected void txtPin_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(EpinName))
            //{
            //    _pinData = new PinData();
            //    if (PinValid())
            //    {
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "EPin Is Valid", "Success!", Toastr.ToastPosition.TopCenter, true);
            //    }
            //    else
            //    {
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Check Your EPin", "Pin Not Valid!", Toastr.ToastPosition.TopCenter, true);
            //    }
            //}
            //else
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Epin", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //}
        }
        private bool PinValid()
        {
            //_pinData = new PinData();
            //bool Valid = _pinData.IsPinValid(EpinName);
            //if (Valid)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(PlanId))
            //{
            //    _planData = new PlanData();
            //    int Plan = Convert.ToInt32(PlanId);
            //    PlanDto result = _planData.GetById(Plan);
            //    if (result != null)
            //    {
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        PlanAmount = result.PlanAmount.ToString();
            //        PlanDescription = result.PlanDescription;
            //    }
            //    else
            //    {
            //        if (LovelyGlobal.UserMaster_PositionThree == 0)
            //        { rdoPosition.Items[2].Attributes.CssStyle.Add("visibility", "hidden"); }
            //        if (LovelyGlobal.UserMaster_LevelPlan == 1)
            //        { lblPosition.Visible = false; }
            //        PlanAmount = null;
            //        PlanDescription = null;
            //    }
            //}
            //else
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select Plan", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //}
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StateId))
            {
                _cityData = new CityData();
                ddlCity.Items.Clear();
                long State = Convert.ToInt64(StateId);

                ddlCity.DataSource = _cityData.GetAllByStateId(State);
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityId";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Please Select City", ""));
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please select State", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private string ValidIntroducer(string Code)
        {
            try
            {
                _userData = new UserData();
                UserDto Data = _userData.GetByCode(Code);
                if (Data != null)
                {
                    return Data.Name;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        protected void txtIntroducer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IntroducerCode))
            {
                string IntroName = ValidIntroducer(IntroducerCode);
                if (!string.IsNullOrEmpty(IntroName))
                {
                    IntroducerName = IntroName;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    IntroducerName = null;
                    btnSubmit.Enabled = false;
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Introducer Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Introducer Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        public bool BindApproverMailID()
        {
            try
            {
                UserData _userData = new UserData();
                UserDto result = _userData.GetApproverID(ddlMemberType.SelectedValue);
                if (result != null)
                {
                    txtApproverMailID.Text = result.ApproverMailID;
                    return true;
                }
                else
                {
                    txtApproverMailID.Text = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberType.SelectedValue != string.Empty)
            {
                BindApproverMailID();
            }
            else
            {
                txtApproverMailID.Text = string.Empty;
            }
        }

        void saveBdDetails()
        {
            BDSolutionMasterData _bdSolutionData = new BDSolutionMasterData();

            BDSolutionMasterDto request = new BDSolutionMasterDto();
            request.BD = ApplicantName;
            request.FFBD = UserTypeId;

            _bdSolutionData.Insert(request);


        }

        bool checkEmailIdExistInPP()
        {
            bool EmailIDInUse = false;

            UserData _userData = new UserData();
            UserDto request = new UserDto();
            request.EmailId = txtEmail.Text;
            EmailIDInUse = _userData.checkEmailIdExistInPP(request);
            if (EmailIDInUse == true)
            {
                return true;
            }
            Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Create your account in Peoplepoint first.", "Warning!!", Toastr.ToastPosition.TopCenter, true);
            return false;
        }

        #endregion

        #region Save Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _userData = new UserData();
            try
            {
                if (FieldValidation())
                {
                    if (!checkEmailIdExistInPP())
                    {
                        return;
                    }
                    if (RqId != null)
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                        {
                            Id = Convert.ToInt64(RqId);
                        }
                    }
                    UserDto request = MappingObject(new UserDto());
                    if (Id == 0)
                    {
                        UserDto success = _userData.Insert(request);
                        if (success != null)
                        {
                            hfID.Value = success.Id.ToString();
                            // sendMailToApprover(); Stop Mail approver for sometime
                            approved(success.Id.ToDataConvertInt64());
                            if (chkIsBD.Checked)
                            {
                                saveBdDetails();
                            }

                            //Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Your request of user creation has been successfully sent to approval.", "Success!!", Toastr.ToastPosition.TopCenter, true);
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Your account has been create successfully and Credentials sent to <b>" + txtEmail.Text + "</b>.", "Success!!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please check Your internet connection", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    //else
                    //{
                    //    bool result = _userData.Update(request);
                    //    if (result)
                    //    {
                    //        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "User has been update successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    //    }
                    //    else
                    //    {
                    //        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "User Details not update.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    //    }
                    //}
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please check Your internet connection or Contact Admin", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private bool FieldValidation()
        {
            if (string.IsNullOrEmpty(ApplicantName))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Applicant Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(Mobile))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Enter Mobile No", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (string.IsNullOrEmpty(UserTypeId))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Select User Type", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (Mobile.Length != 10)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please Correct Mobile", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (Email == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Please enter email id", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            else if (txtApproverMailID.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Approver Email id not found for this User Type.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }

            else if (txtEmail.Text != string.Empty)
            {
                bool EmailIDInUse = false;

                UserData _userData = new UserData();
                UserDto request = new UserDto();
                request.EmailId = txtEmail.Text;

                EmailIDInUse = _userData.ChkMailID(request);
                if (EmailIDInUse == true)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Email ID Already in use!!", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
        private UserDto MappingObject(UserDto obj)
        {
            obj.Id = Id;
            obj.Name = ApplicantName;
            obj.GuardianName = FatherName;
            obj.Birthday = string.IsNullOrEmpty(DOB) ? Utility.GetIndianDateTime() : Convert.ToDateTime(DOB);
            obj.Gender = Gender;
            obj.MaritalStatus = MaritalStatus;
            obj.BranchId = string.IsNullOrEmpty(BranchId) ? 1 : Convert.ToInt32(BranchId);
            obj.Address = Address;
            obj.CityId = string.IsNullOrEmpty(CityId) ? 1 : Convert.ToInt64(CityId);
            obj.StateId = string.IsNullOrEmpty(StateId) ? 1 : Convert.ToInt64(StateId);
            obj.ZipCode = PinCode;
            obj.MobileNo = Mobile;
            obj.EmailId = Email;
            obj.AccountNo = AccountNo;
            obj.BankId = string.IsNullOrEmpty(BankId) ? 1 : Convert.ToInt32(BankId);
            obj.BankAddress = BankBranch;
            obj.Ifscode = IFSCode;
            obj.NomineeName = NomineeName;
            obj.NomineeZipCode = NomineePinCode;
            obj.JoiningDate = Utility.GetIndianDateTime();
            obj.ProductReceive = "N";
            obj.NomineeAddress = NomineeAddress;
            obj.NomineeAge = string.IsNullOrEmpty(NomineeAge) ? 0 : Convert.ToInt32(NomineeAge);
            obj.NomineeCityId = string.IsNullOrEmpty(NomineeCity) ? 1 : Convert.ToInt32(NomineeCity);
            obj.NomineeRelationId = string.IsNullOrEmpty(RelationId) ? 1 : Convert.ToInt32(RelationId);
            obj.IntroducerCode = PlanAmount;
            obj.UplineCode = IntroducerCode;
            obj.PlanId = string.IsNullOrEmpty(PlanId) ? (int?)null : Convert.ToInt32(PlanId);
            obj.ApprovalType = "Mail";
            if (LovelyGlobal.UserMaster_Document == 1)
            {
                if (DocumentType == "U")
                {
                    obj.Aadhar = DocumentNo;
                }
                else if (DocumentType == "V")
                {
                    obj.VoterId = DocumentNo;
                }
                else if (DocumentType == "P")
                {
                    obj.Passport = DocumentNo;
                }
                else if (DocumentType == "D")
                {
                    obj.DlNo = DocumentNo;
                }
            }
            obj.IsVerify = false;
            obj.UserTypeId = string.IsNullOrEmpty(UserTypeId) ? 0 : Convert.ToInt32(UserTypeId);
            obj.PanCardNo = Pan;
            obj.IsActive = false;
            obj.PinNumber = txtPlanDescription.InnerText;
            obj.RankId = RankId == "" ? 1 : RankId.ToInt();
            obj.RptMgrID = drpRptMgr.SelectedValue.ToNullLong();
            obj.IsCRM = chkIsCrm.Checked;

            return obj;
        }
        #endregion

        #region Mail
        string mTo = "";
        string mBCC = "";
        string mFrom = "";
        string mSubject = "";
        string mBody = "";
        bool sendMailToApprover()
        {
            try
            {
                string subject = "Login Creation Approval of : [" + ApplicantName + "]";
                string textBody = "Hi<br/><br/>";
                textBody = textBody + "Please approved CRM ID creation for <b>" + ApplicantName + "</b> . New Joinee in " + ddlMemberType.SelectedItem.Text + "<br/>";
                textBody = textBody + "<b>User Name : </b>" + ApplicantName + "<br/>";
                textBody = textBody + "<b>Email-ID  : </b>" + txtEmail.Text + "<br/>";
                textBody = textBody + "<b>Reporting Manager  : </b>" + drpRptMgr.SelectedItem.Text + "<br/>";
                textBody = textBody + "<b>Mobile-No  : </b>" + txtMobile.Text + "<br/>";
                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/MemberMaster/UserApproval.aspx?Type=A&ID=" + hfID.Value + "><b><font size=5> Approve </font> </b></a> ";
                textBody = textBody + "&nbsp;&nbsp;&nbsp;<a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/MemberMaster/UserApproval.aspx?Type=R&ID=" + hfID.Value + "><b><font size=5> Reject </font> </b></a> ";


                //textBody += "Regards,<br/><br/>";
                textBody += "<br/><br/>This is system generated mail, Please don't reply.";
                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = txtApproverMailID.Text;
                // mBCC = "debu@als.group,Ram.pk@als.group";
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
            //message.Bcc.Add(mBCC);
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

        #region credentials mail to user
        void approved(long MemberID)
        {
            UserData _userData = new UserData();

            UserDto request = new UserDto();
            request.Id = MemberID;
            request.GuardianName = "Approved";
            request.IsActive = true;


            UserDto success = _userData.UserApproved(request);
            if (success != null)
            {
                if (success.Id != 0)
                {
                    sendMailToUser(MemberID);
                }

            }
        }

        void sendMailToUser(long MemberID)
        {
            UserData _userData = new UserData();
            UserDto result = _userData.GetByIdforUserApproval(MemberID);

            if (result != null)
            {
                string textBody = string.Empty;
                string subject = string.Empty;

                subject = "CRM login Details";

                textBody = "Please find below your CRM login Details,<br/><br/>";

                textBody = textBody + " <table borderColor='Black' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "width=" + 80 + "%>";
                textBody = textBody + "<tr> <td> <b>UserID:</b><hr>" + result.Code + "</td><td> <b>Password: </b><hr>" + result.Password + "</td></tr>";


                textBody = textBody + "</table>  </td></tr>";
                textBody += "</table> <br/> <br/>";

                textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://crm.als.group/FrontEnd/SignIn.aspx><b><font size=5> Crm Login </font> </b></a> ";
                // textBody = textBody + "<br/><a style='background-color: Skyblue;  color: White;  padding: 0px 18px; text-align: center; text-decoration: none; display: inline-block; border-radius: 11px; ' href =http://1.22.156.70:4000/FrontEnd/SignIn.aspx><b><font size=5> OutSide Corporate Office </font> </b></a><br/><br/><br/> ";


                textBody += "This is system generated mail, Please don't reply.";

                //mFrom = Session["User_ID"].ToString();
                mFrom = "crm@als.group";
                mTo = result.EmailId;
                //mBCC = "ggnho@apolloindia.com,wifin.gurgaon@wifintech.com,apollointranet@gmail.com";
                //mBCC = "debu@als.group,Ram.pk@als.group";
                mSubject = subject;
                mBody = textBody;
                sendMailForFF(mFrom, mTo, mSubject, mBody);
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/FrontEnd/SignIn.aspx");
        }

    }
}