using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.Master
{
    public partial class PricingMaster : CustomBasePage
    {
        #region Properties
        PricingMasterData _PricingMasterData = null;

        private int Id = 0;
        private string Action { get { return drpAction.SelectedValue; } set { drpAction.SelectedValue = value; } }
        private string MasterID { get { return drpSelectMaster.SelectedValue; } set { drpSelectMaster.SelectedValue = value; } }
        private string SearchID { get { return drpSearchName.SelectedValue; } set { drpSearchName.SelectedValue = value; } }
        private string Name { get { return txtValue.Text; } set { txtValue.Text = value; } }
        string RqId { get { return Request["requestId"]; } }
        #endregion

        #region Page Load
        private PricingMasterDto MappingObject(PricingMasterDto obj)
        {

            obj.MasterID = MasterID.ToString();
            obj.ID = string.IsNullOrEmpty(SearchID) ? 1 : Convert.ToInt32(SearchID);
            obj.IsActive = chkActive.Checked;
            obj.Value = txtValue.Text;
            return obj;
        }
        private void SetProperties(PricingMasterDto obj)
        {
            SearchID = obj.ID.ToString();
            //MasterID = obj.CityName;
            //if (obj.CityStateId != null && obj.CityStateId != 0)
            //    StateId = obj.CityStateId.ToString();
        }
        private void Clear()
        {
            drpAction.SelectedItem.Text = "Add";
            drpSelectMaster.SelectedValue = string.Empty;
            drpSearchName.Items.Clear();
            txtValue.Text = string.Empty;
            chkActive.Checked = false;

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
                // BindDropDown();
                // BindGridView();
                // if (RqId != null)
                // {
                //     if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                //     {
                //         _cityData = new CityData();
                //         CityDto result = _cityData.GetById(Convert.ToInt64(RqId));
                //         SetProperties(result);
                //     }
                // }
            }
        }
        private void BindDropDown()
        {
            _PricingMasterData = new PricingMasterData();
            PricingMasterDto obj = new PricingMasterDto();
            obj.MasterID = drpSelectMaster.SelectedValue;
            IList<PricingMasterDto> list = _PricingMasterData.GetAllMaster(obj);
            if (list != null)
            {
                drpSearchName.DataSource = list;
                drpSearchName.DataTextField = "Value";
                drpSearchName.DataValueField = "ID";
                drpSearchName.DataBind();
                drpSearchName.Items.Insert(0, new ListItem("--Select--", string.Empty));

                dlstImages.DataSource = list;
                dlstImages.DataBind();
            }
            else
            {
                drpSearchName.Items.Clear();
            }
        }


        #endregion

        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _PricingMasterData = new PricingMasterData();
            try
            {
                if (!string.IsNullOrEmpty(txtValue.Text))
                {
                    if (RqId != null)
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(RqId, @"^[a-zA-Z]+$"))
                        {
                            Id = Convert.ToInt32(RqId);
                        }
                    }
                    PricingMasterDto request = MappingObject(new PricingMasterDto());
                    if (drpAction.SelectedItem.Text == "Add")
                    {
                        bool result = false;
                        if (drpSelectMaster.SelectedValue == "Region")
                        {
                            result = _PricingMasterData.InsertRegionMaster(request);
                        }
                        else if (drpSelectMaster.SelectedValue == "IncoTerms")
                        {
                            result = _PricingMasterData.InsertIncoTermsMaster(request);
                        }
                        else if (drpSelectMaster.SelectedValue == "Port")
                        {
                            result = _PricingMasterData.InsertPortMaster(request);
                        }
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, drpSelectMaster.SelectedItem.Text + " has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, drpSelectMaster.SelectedItem.Text + " Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                    else if(drpAction.SelectedItem.Text == "Edit")
                    {
                        bool result = false;
                        if (drpSelectMaster.SelectedValue == "Region")
                        {
                            result = _PricingMasterData.UpdateRegionMaster(request);
                        }
                        else if (drpSelectMaster.SelectedValue == "IncoTerms")
                        {
                            result = _PricingMasterData.UpdateIncoTermsMaster(request);
                        }
                        else if (drpSelectMaster.SelectedValue == "Port")
                        {
                            result = _PricingMasterData.UpdatePortMaster(request);
                        }
                        if (result)
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Success, drpSelectMaster.SelectedItem.Text + " has been updated Successfully", "Success!", Toastr.ToastPosition.TopCenter, true);
                            Clear();
                        }
                        else
                        {
                            Toastr.ShowToast(this.Page, Toastr.ToastType.Error, drpSelectMaster.SelectedItem.Text + " Not Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
                        }
                    }
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Warning, "Plaese Select Action Name", "Oops!", Toastr.ToastPosition.TopCenter, true);
                }
            }
            catch (Exception Ex)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "City Already Saved", "Oops!", Toastr.ToastPosition.TopCenter, true);
            }
        }


        #endregion

        protected void drpSelectMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDown();
            Visibility();
        }

        protected void drpSearchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSearchName.SelectedValue != string.Empty)
            {
                txtValue.Text = drpSearchName.SelectedItem.Text;
                chkActive.Checked = true;
            }
            else
            {
                txtValue.Text = string.Empty;
                chkActive.Checked = false;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public void Visibility()
        {
            try
            {
                if (drpAction.SelectedItem.Text == "Add")
                {
                    dvSearchName.Visible = false;
                    dvValue.Visible = true;
                    dvISActive.Visible = true;
                    btnSubmit.Visible = true;
                    btnClear.Visible = true;
                    dlstImages.Visible = false;
                }
                if (drpAction.SelectedItem.Text == "Edit")
                {
                    dvSearchName.Visible = true;
                    dvValue.Visible = true;
                    dvISActive.Visible = true;
                    btnSubmit.Visible = true;
                    btnClear.Visible = true;
                    dlstImages.Visible = false;
                }
                if (drpAction.SelectedItem.Text == "View")
                {
                    dvSearchName.Visible = false;
                    dvValue.Visible = false;
                    dvISActive.Visible = false;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                    dlstImages.Visible = true;
                }
                if (drpSelectMaster.SelectedItem.Text == string.Empty)
                {
                    dvSearchName.Visible = false;
                    dvValue.Visible = false;
                    dvISActive.Visible = false;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                    dlstImages.Visible = true;
                }

            }
            catch(Exception ex)
            {

            }
        
        }
        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Visibility();
        }
    }
}