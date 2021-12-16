using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd.PermissionCore
{
    public partial class PermissionCompany : CustomBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LovelySession.Lovely == null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx", true);
                return;
            }
            if (!IsPostBack)
            {
                BindData();
            }
        }
        
        void BindData()
        {
            RoleData _roleData = new RoleData();
            IList<RoleDto> roleGetAll = _roleData.GetAll(true);
            if (roleGetAll != null)
            {
                drpRoleMaster.DataSource = roleGetAll;
                drpRoleMaster.DataTextField = "RoleName";
                drpRoleMaster.DataValueField = "RoleId";
                drpRoleMaster.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(drpRoleMaster.SelectedValue))
            {
                int count = 0;
                foreach (GridViewRow gvRow in grdPermission.Rows)
                {
                    PermissionCompanyDto sendObj = new PermissionCompanyDto();

                    sendObj.PermissionCompanyCompanyId = Convert.ToInt64(((Label)gvRow.FindControl("PermissionCompanyCompanyId")).Text);
                    sendObj.PermissionCompanyUserRoleId = Convert.ToInt64(drpRoleMaster.SelectedValue);
                    sendObj.PermissionCompanyview = ((CheckBox)gvRow.FindControl("PermissionCompanyview")).Checked;

                    PermissionCompanyData _permissionMasterData = new PermissionCompanyData();
                    bool sendOutput = _permissionMasterData.Insert(sendObj);

                    if (!sendOutput)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    //Message Not Saved Successfully
                }
                else
                {
                    //Saved
                }
            }
        }
        protected void grdPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPermission.PageIndex = e.NewPageIndex;
            //  BindGridView();
        }

        protected void drpRoleMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPermission.DataSource = null;

            if (!string.IsNullOrEmpty(drpRoleMaster.SelectedValue))
            {
                PermissionCompanyData _permissionMasterData = new PermissionCompanyData();
                List<PermissionCompanyDto> result = _permissionMasterData.GetByUserRoleMenu(Convert.ToInt64(drpRoleMaster.SelectedValue));

                if (result != null)
                {
                    grdPermission.DataSource = result;
                    grdPermission.DataBind();
                }
            }
        }
    }
}