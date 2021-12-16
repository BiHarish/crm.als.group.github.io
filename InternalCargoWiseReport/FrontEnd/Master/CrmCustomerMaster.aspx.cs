using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Master
{
    public partial class CrmCustomerMaster : System.Web.UI.Page
    {
        WhCustomerMasterData _whCustomerMasterData = null;
        long WhCustomerID;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    return;
                }
               

                bindGrid();
                bindDrp();
                FirstGridViewRow();

                if (LovelySession.Lovely.User.Id == 10 || LovelySession.Lovely.User.Id == 16 || LovelySession.Lovely.User.Id == 1)
                {

                }
                else
                {

                    string path = Request.UrlReferrer.AbsoluteUri;
                    Response.Redirect(path);
                }

            }
            Page.MaintainScrollPositionOnPostBack = true;
            lnkButton.Focus(); 
        }

        #region Method

        bool validation()
        {
            if (txtName.Text == string.Empty)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Customer Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }


            //else if (txtPinCode.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter PinCode.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}

            //else if (txtAddress.Text == string.Empty)
            //{
            //    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Customer Address.", "Oops!", Toastr.ToastPosition.TopCenter, true);
            //    return false;
            //}
            else if (chkSCS.Checked == false && chkFF.Checked == false && chkPrime.Checked == false && chkLiquid.Checked == false && chkCFS.Checked == false)
            {
                Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select atleast one checkbox!!.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                return false;
            }
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                
                TextBox gvtxtAddress = (TextBox)gvRow.FindControl("txtAddress");

                if (gvtxtAddress.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Address!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return false;
                }

            }

            return true;
        }

        private WhCustomerMasterDto MappingObject(WhCustomerMasterDto obj)
        {
            obj.ID = HfID.Value.ToLong();
            obj.Name = txtName.Text;
            obj.Address = txtAddress.Text;
            obj.CreateBy = LovelySession.Lovely.User.Id;
            string phoneNo = txtPhoneNo.Text.Replace(" ", "");
            obj.PhoneNo = phoneNo.ToNullLong();//txtPhoneNo.Text.ToNullLong();
            obj.GSTNo = txtGstNo.Text;
            string Pincode = txtPinCode.Text.Replace(" ", "");
            obj.PinCode = Pincode.ToNullLong();//txtPinCode.Text.ToNullLong();
            obj.EmailID = txtEmailID.Text;
            obj.SCS = chkSCS.Checked;
            obj.FF = chkFF.Checked;
            obj.Prime = chkPrime.Checked;
            obj.Liquid = chkLiquid.Checked;
            obj.CFS = chkCFS.Checked;

            return obj;
        }

        void clear()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtGstNo.Text = string.Empty;
            txtPinCode.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            HfID.Value = string.Empty;
            btnSubmit.Text = "Submit";
            chkSCS.Checked = false;
            chkFF.Checked = false;
            chkPrime.Checked = false;
            chkLiquid.Checked = false;
            chkCFS.Checked = false;
            FirstGridViewRow();
          
        }

        void bindGrid()
        {
            _whCustomerMasterData = new WhCustomerMasterData();

            IList<WhCustomerMasterDto> results = _whCustomerMasterData.GetAll(drpBU.SelectedValue);

            if (results != null)
            {
                txtTotRecord.Text = results.Count.ToString();
                gvCustomerList.DataSource = results;
                gvCustomerList.DataBind();
            }

        }
        void bindAddressGrid(long? WhCustomerID)
        {
            _whCustomerMasterData = new WhCustomerMasterData();
            IList<WhCustomerAddTransDto> Addressresults = _whCustomerMasterData.GetAllAddressByCustomerID(WhCustomerID);
            if (Addressresults != null)
            {
                gvAddress.DataSource = Addressresults;
                gvAddress.DataBind();

                ViewState["CurrentTableForAddress"] = Addressresults.ToList().ToDataTable<WhCustomerAddTransDto>();
            }
            else
            {
                FirstGridViewRow();
                ViewState["CurrentTableForAddress"] = null;
            }
        }

        void setData()
        {
            _whCustomerMasterData = new WhCustomerMasterData();
            long ID = HfID.Value.ToLong();
            if (ID > 0)
            {
                WhCustomerMasterDto result = _whCustomerMasterData.GetById(ID);
                if (result != null)
                {
                    txtName.Text = result.Name;
                    txtAddress.Text = result.Address;
                    txtPhoneNo.Text = result.PhoneNo.ToString();
                    txtGstNo.Text = result.GSTNo;
                    txtEmailID.Text = result.EmailID;
                    txtPinCode.Text = result.PinCode.ToString();
                    chkSCS.Checked = result.SCS;
                    chkFF.Checked = result.FF;
                    chkPrime.Checked = result.Prime;
                    chkLiquid.Checked = result.Liquid;
                    chkCFS.Checked = result.CFS;

                    //bind Address Grid
                    bindAddressGrid(ID);
                }
            }
        }

        void bindDrp()
        {
            _whCustomerMasterData = new WhCustomerMasterData();

            IList<WhCustomerMasterDto> results = _whCustomerMasterData.GetAll("");

            if (results != null)
            {
                DrpCustomerName.DataSource = results;
                DrpCustomerName.DataValueField = "ID";
                DrpCustomerName.DataTextField = "Name";
                DrpCustomerName.DataBind();
                DrpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            else
            {
                DrpCustomerName.Items.Clear();
                DrpCustomerName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        void saveAddressGrid()
        {
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                Label gvlblID = (Label)gvRow.FindControl("lblID");
                Label gvlblWhCustID = (Label)gvRow.FindControl("lblWhCustomerID");
                TextBox gvlblAddress = (TextBox)gvRow.FindControl("txtAddress");

                WhCustomerAddTransDto request = new WhCustomerAddTransDto();
                request.ID = gvlblID.Text.ToLong();
                if (WhCustomerID != 0)
                    request.WhCustomerID = WhCustomerID;
                else
                    request.WhCustomerID = HfID.Value.ToLong();
                request.CustAddress = gvlblAddress.Text;
                request.CreateBy = LovelySession.Lovely.User.Id;

                WhCustomerMasterData _whCustomerMaster = new WhCustomerMasterData();

                if (gvlblID.Text == string.Empty)
                {
                   

                    if (_whCustomerMaster.InsertAddress(request))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Address not saved.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                else
                {
                    if(_whCustomerMaster.UpdateAddress(request))
                    {

                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Address not Updated.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
            }
        }

        #endregion



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validation())
            {
                return;
            }
            _whCustomerMasterData = new WhCustomerMasterData();
            WhCustomerMasterDto request = MappingObject(new WhCustomerMasterDto());

            if (request.ID == 0)
            {
                WhCustomerID = _whCustomerMasterData.Insert(request);
                if (WhCustomerID > 0)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully saved", "Success!", Toastr.ToastPosition.TopCenter, true);
                    saveAddressGrid();
                    bindGrid();

                    clear();
                }
                else
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Alreday Saved.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }
            }
            else
            {
                if (_whCustomerMasterData.Update(request))
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Record has been Successfully Updated.", "Success!", Toastr.ToastPosition.TopCenter, true);
                    saveAddressGrid();
                    bindGrid();
                    btnSubmit.Text = "Submit";
                    clear();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void gvCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                try
                {
                    GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int RowIndex = oItem.RowIndex;
                    GridViewRow gvRow = gvCustomerList.Rows[RowIndex];

                    Label txtID = (Label)gvRow.FindControl("lblID");


                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        HfID.Value = txtID.Text;
                        setData();
                        btnSubmit.Text = "Update";
                    }
                    else
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                    }
                }
                catch
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please Check Your Connection", "Error!", Toastr.ToastPosition.TopCenter, true);
                }
            }
        }

        protected void gvCustomerList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerList.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            if (DrpCustomerName.SelectedValue == string.Empty)
            {
                //Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please select Customer Name.", "Oops!", Toastr.ToastPosition.TopCenter, true);
                bindGrid();
                return;
            }
            else
            {
                _whCustomerMasterData = new WhCustomerMasterData();

                WhCustomerMasterDto result = _whCustomerMasterData.GetById(DrpCustomerName.SelectedValue.ToLong());

                if (result != null)
                {
                    IList<WhCustomerMasterDto> list = new List<WhCustomerMasterDto>();
                    list.Add(result);
                    txtTotRecord.Text = list.Count.ToString();
                    gvCustomerList.DataSource = list;
                    gvCustomerList.DataBind();
                }

            }

        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            bindGrid();
            bindDrp();
            clear();
        }


        #region AddressGrid
        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("WhCustomerID", typeof(string)));
            dt.Columns.Add(new DataColumn("CustAddress", typeof(string)));
            dr = dt.NewRow();

            dr["RowNumber"] = 1;
            dr["ID"] = string.Empty;
            dr["WhCustomerID"] = "0";
            dr["CustAddress"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTableForAddress"] = dt;

            gvAddress.DataSource = dt;
            gvAddress.DataBind();
        }

        private void AddNewRowForAddress()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTableForAddress"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableForAddress"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvAddress.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label gvlblCustomerID = (Label)gvAddress.Rows[rowIndex].Cells[1].FindControl("lblWhCustomerID");
                        TextBox gvtxtAddress = (TextBox)gvAddress.Rows[rowIndex].Cells[2].FindControl("txtAddress");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["ID"] = gvlblID.Text;
                        dtCurrentTable.Rows[i - 1]["WhCustomerID"] = gvlblCustomerID.Text;
                        dtCurrentTable.Rows[i - 1]["CustAddress"] = gvtxtAddress.Text;

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTableForAddress"] = dtCurrentTable;

                    gvAddress.DataSource = dtCurrentTable;
                    gvAddress.DataBind();
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
            if (ViewState["CurrentTableForAddress"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTableForAddress"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label gvlblID = (Label)gvAddress.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label gvlblCustomerID = (Label)gvAddress.Rows[rowIndex].Cells[1].FindControl("lblWhCustomerID");
                        TextBox gvtxtAddress = (TextBox)gvAddress.Rows[rowIndex].Cells[2].FindControl("txtAddress");

                        gvlblID.Text = dt.Rows[i]["ID"].ToString();
                        gvlblCustomerID.Text = dt.Rows[i]["WhCustomerID"].ToString();
                        gvtxtAddress.Text = dt.Rows[i]["CustAddress"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void gvAddress_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Remove")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                GridViewRow gvRow = gvAddress.Rows[RowIndex];



                Label lblID = (Label)gvRow.FindControl("lblID");
                Label lblWhCustomerID = (Label)gvRow.FindControl("lblWhCustomerID");

                if (lblID.Text != string.Empty)
                {
                    _whCustomerMasterData = new WhCustomerMasterData();

                    if (_whCustomerMasterData.deleteAddress(lblID.Text.ToLong()))
                    {
                        Toastr.ShowToast(this.Page, Toastr.ToastType.Success, "Address Successfully deleted!!", "Success!", Toastr.ToastPosition.TopCenter, true);


                        bindAddressGrid(lblWhCustomerID.Text.ToLong());
                    }


                }
                else if (oItem != null)
                {

                    DataTable dtt = (DataTable)ViewState["CurrentTableForAddress"];

                    if (dtt.Rows.Count > 1)
                    {
                        dtt.Rows.RemoveAt(RowIndex);
                        dtt.AcceptChanges();
                    }

                    gvAddress.DataSource = dtt;
                    gvAddress.DataBind();
                    SetPreviousContactData();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in gvAddress.Rows)
            {
                TextBox gvtxtAddress = (TextBox)gvRow.FindControl("txtAddress");

                if (gvtxtAddress.Text == string.Empty)
                {
                    Toastr.ShowToast(this.Page, Toastr.ToastType.Error, "Please enter Address!!", "Error!", Toastr.ToastPosition.TopCenter, true);
                    return;
                }

            }
            AddNewRowForAddress();
        }
        #endregion

      

    }
}