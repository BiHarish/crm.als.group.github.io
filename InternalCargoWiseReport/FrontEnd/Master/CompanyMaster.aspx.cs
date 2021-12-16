using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Master
{
    public partial class CompanyMaster : CustomBasePage
    {
        #region Properties
        StateData _stateData = null;
        CityData _cityData = null;
        CompanyData _companyData = null;

        bool NameInvalid = false;
        bool ContactInValid = false;
        bool AddressInValid = false;
        bool BankInvalid = false;

        private string Id { get { return txtId.Value; } set { txtId.Value = value; } }
        private string Name { get { return txtName.Text; } set { txtName.Text = value; } }
        private string Code { get { return txtCode.Text; } set { txtCode.Text = value; } }
        private string GSTNo { get { return txtGST.Text; } set { txtGST.Text = value; } }
        private string Website { get { return txtWebsite.Text; } set { txtWebsite.Text = value; } }
        private string MobileNo { get { return txtMobile.Text; } set { txtMobile.Text = value; } }
        private string EmailId { get { return txtEmail.Text; } set { txtEmail.Text = value; } }
        private string Fax { get { return txtFax.Text; } set { txtFax.Text = value; } }
        private string Phone1 { get { return txtPhone.Text; } set { txtPhone.Text = value; } }
        private string Phone2 { get { return txtPhone2.Text; } set { txtPhone2.Text = value; } }
        private string State { get { return ddlState.SelectedValue; } set { ddlState.SelectedValue = value; } }
        private string City { get { return ddlCity.SelectedValue; } set { ddlCity.SelectedValue = value; } }
        private string Address { get { return txtAddress.InnerText; } set { txtAddress.InnerText = value; } }
        private string ZipCode { get { return txtZipCode.Text; } set { txtZipCode.Text = value; } }
        private string BankId { get { return ddlBank.SelectedValue; } set { ddlBank.SelectedValue = value; } }
        private string AccountNo { get { return txtAccountNo.Text; } set { txtAccountNo.Text = value; } }
        private string IFSCode { get { return txtIFSCode.Text; } set { txtIFSCode.Text = value; } }
        private string Branch { get { return txtBranch.Text; } set { txtBranch.Text = value; } }
        private string Logo { get { return CurrentLogo.Src; } set { CurrentLogo.Src = value; } }
        #endregion

        #region Page Load
        private void BindDropDown()
        {
            _stateData = new StateData();
            _cityData = new CityData();

            ddlState.DataSource = _stateData.GetAll(true);
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Please Select State", ""));

            ddlCity.DataSource = _cityData.GetAll(true);
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityId";
            ddlCity.DataBind();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                BindDropDown();
                BindData();
            }
        }
        private void BindData()
        {
            _companyData = new CompanyData();
            CompanyDto result = _companyData.Get();
            if (result != null)
            {
                Id = result.CompanyId.Value.ToString();
                Name = result.CompanyName;
                Code = result.CompanyCode;
                GSTNo = result.CompanyGSTNo;
                Website = result.CompanyWebsite;
                EmailId = result.CompanyEmailId;
                MobileNo = result.CompanyMobileNo;
                Fax = result.CompanyFax;
                Phone1 = result.CompanyPhone1;
                Phone2 = result.CompanyPhone2;
                Address = result.CompanyAddress;
                ZipCode = result.CompanyZipCode;
                State = result.CompanyStateId.ToString();
                City = result.CompanyCityId.ToString();
                Logo = "/Files/Logo/" + result.CompanyLogo;
                BankId = result.CompanyBankId.ToString();
                AccountNo = result.CompanyAccountNo;
                IFSCode = result.CompanyIFSCode;
                Branch = result.CompanyBankBranch;
            }
        }
        #endregion

        #region Basic Information Save
        protected void btnInformation_Click(object sender, EventArgs e)
        {
            try
            {
                NameValidation();
                if (NameInvalid)
                {
                    _companyData = new CompanyData();
                    bool success = _companyData.UpdateNameInformation(MappingNameInformation(new CompanyDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Name Information Saved Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Information Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Information Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private CompanyDto MappingNameInformation(CompanyDto obj)
        {
            obj.CompanyId = Convert.ToInt32(Id);
            obj.CompanyName = Name;
            obj.CompanyCode = Code;
            obj.CompanyGSTNo = GSTNo;

            return obj;
        }
        private bool NameValidation()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return NameInvalid;
            }
            else if (string.IsNullOrEmpty(Code))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return NameInvalid;
            }
            else
            {
                return NameInvalid = true;
            }
        }
        #endregion

        #region Contact Information Save
        protected void btnContact_Click(object sender, EventArgs e)
        {
            try
            {
                ContactValidation();
                if (ContactInValid)
                {
                    _companyData = new CompanyData();
                    bool success = _companyData.UpdateContactInformation(MappingConatctInformation(new CompanyDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Contact Information Saved Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Contact Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Contact Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private CompanyDto MappingConatctInformation(CompanyDto obj)
        {
            obj.CompanyId = Convert.ToInt32(Id);
            obj.CompanyWebsite = Website;
            obj.CompanyEmailId = EmailId;
            obj.CompanyMobileNo = MobileNo;
            obj.CompanyFax = Fax;
            obj.CompanyPhone1 = Phone1;
            obj.CompanyPhone2 = Phone2;

            return obj;
        }
        private bool ContactValidation()
        {
            if (string.IsNullOrEmpty(EmailId))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Email", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return ContactInValid;
            }
            else if (string.IsNullOrEmpty(MobileNo))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Mobile", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return ContactInValid;
            }
            else
            {
                return ContactInValid = true;
            }
        }
        #endregion

        #region Address Information Save
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCity.Items.Clear();
            if (!string.IsNullOrEmpty(State))
            {
                long StateId = Convert.ToInt64(State);
                _cityData = new CityData();

                ddlCity.DataSource = _cityData.GetAllByStateId(StateId);
                ddlCity.DataTextField = "CityName";
                ddlCity.DataValueField = "CityId";
                ddlCity.DataBind();
            }
            else
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Select State First", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected void btnAddress_Click(object sender, EventArgs e)
        {
            try
            {
                AddressValidation();
                if (AddressInValid)
                {
                    _companyData = new CompanyData();
                    bool success = _companyData.UpdateAddressInformation(MappingAddressInformation(new CompanyDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Address Information Saved Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Address Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Address Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private CompanyDto MappingAddressInformation(CompanyDto obj)
        {
            obj.CompanyId = Convert.ToInt32(Id);
            obj.CompanyAddress = Address;
            obj.CompanyStateId = Convert.ToInt64(State);
            obj.CompanyCityId = Convert.ToInt64(City);
            obj.CompanyZipCode = ZipCode;

            return obj;
        }
        private bool AddressValidation()
        {
            if (string.IsNullOrEmpty(ZipCode))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Zip Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return AddressInValid;
            }
            else if (string.IsNullOrEmpty(Address))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Address", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return AddressInValid;
            }
            else
            {
                return AddressInValid = true;
            }
        }
        #endregion

        #region Bank Information Save
        protected void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                BankValidation();
                if (BankInvalid)
                {
                    _companyData = new CompanyData();
                    bool success = _companyData.UpdateBankInformation(MappingBankInformation(new CompanyDto()));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Bank Information Saved Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Bank Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Bank Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        private CompanyDto MappingBankInformation(CompanyDto obj)
        {
            obj.CompanyId = Convert.ToInt32(Id);
            obj.CompanyBankId = Convert.ToInt32(BankId);
            obj.CompanyAccountNo = AccountNo;
            obj.CompanyIFSCode = IFSCode;
            obj.CompanyBankBranch = Branch;

            return obj;
        }
        private bool BankValidation()
        {
            if (string.IsNullOrEmpty(AccountNo))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Account No", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return BankInvalid;
            }
            else if (string.IsNullOrEmpty(IFSCode))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Ifs Code", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return BankInvalid;
            }
            else if (string.IsNullOrEmpty(Branch))
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Bank Branch", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return BankInvalid;
            }
            else
            {
                return BankInvalid = true;
            }
        }
        #endregion

        #region Logo Information Save
        protected void btnLogo_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgLogo.HasFile)
                {
                    _companyData = new CompanyData();
                    string ext = UploadImage(Path.GetFileName(imgLogo.PostedFile.FileName), "/Files/Logo/", imgLogo);
                    bool success = _companyData.UpdateLogoInformation(MappingLogoInformation(new CompanyDto(), ext));
                    if (success)
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Logo Information Saved Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Logo Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Enter Company Logo", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Logo Not Saved, Please Check Your Network Connection.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }
        protected string UploadImage(string ImageName, string path, FileUpload uploader)
        {
            var exte = "." + ImageName.Split('.')[1];
            uploader.SaveAs(Server.MapPath(path + Id + exte));
            return exte;
        }
        private CompanyDto MappingLogoInformation(CompanyDto obj, string img)
        {
            obj.CompanyId = Convert.ToInt32(Id);
            obj.CompanyLogo = Id + img;

            return obj;
        }
        #endregion
    }
}