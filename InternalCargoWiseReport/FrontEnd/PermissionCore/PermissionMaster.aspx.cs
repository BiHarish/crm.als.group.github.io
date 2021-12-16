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
    public partial class PermissionMaster : Page
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
        protected void drpParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPermission.DataSource = null;
            
            if (!string.IsNullOrEmpty(drpParentMenu.SelectedValue) && !string.IsNullOrEmpty(drpRoleMaster.SelectedValue))
            {
                PermissionMasterData _permissionMasterData = new PermissionMasterData();
                List<PermissionMasterDto> result = _permissionMasterData.GetByUserRoleMenu(Convert.ToInt64(drpParentMenu.SelectedValue), Convert.ToInt64(drpRoleMaster.SelectedValue));
                
                if (result != null)
                {
                    grdPermission.DataSource = result;
                    grdPermission.DataBind();
                }
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

            BackMenuData _backMenuData = new BackMenuData();
            IList<BackMenuDto> backMenuGetAll = _backMenuData.GetAllParent();
            if (backMenuGetAll != null)
            {
                drpParentMenu.DataSource = backMenuGetAll;
                drpParentMenu.DataTextField = "BackMenuName";
                drpParentMenu.DataValueField = "BackMenuId";
                drpParentMenu.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(drpParentMenu.SelectedValue) && !string.IsNullOrEmpty(drpRoleMaster.SelectedValue))
            {
                int count = 0;
                foreach (GridViewRow gvRow in grdPermission.Rows)
                {
                    PermissionMasterDto sendObj = new PermissionMasterDto()
                    {
                        PermissionMasterMenuId = Convert.ToInt64(((Label)gvRow.FindControl("PermissionMasterMenuId")).Text),
                        PermissionMasterUserRoleId = Convert.ToInt64(drpRoleMaster.SelectedValue),
                        PermissionMasterMenuShow = ((CheckBox)gvRow.FindControl("PermissionMasterMenuShow")).Checked,
                        PermissionMasterView = ((CheckBox)gvRow.FindControl("PermissionMasterView")).Checked,
                        PermissionMasterAdd = ((CheckBox)gvRow.FindControl("PermissionMasterAdd")).Checked,
                        PermissionMasterUpdate = ((CheckBox)gvRow.FindControl("PermissionMasterUpdate")).Checked,
                        PermissionMasterDelete = ((CheckBox)gvRow.FindControl("PermissionMasterDelete")).Checked,
                        PermissionMasterPrint = ((CheckBox)gvRow.FindControl("PermissionMasterPrint")).Checked,
                        PermissionMasterSelf = ((CheckBox)gvRow.FindControl("PermissionMasterSelf")).Checked
                    };

                    PermissionMasterData _permissionMasterData = new PermissionMasterData();
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

            if (!string.IsNullOrEmpty(drpParentMenu.SelectedValue) && !string.IsNullOrEmpty(drpRoleMaster.SelectedValue))
            {
                PermissionMasterData _permissionMasterData = new PermissionMasterData();
                List<PermissionMasterDto> result = _permissionMasterData.GetByUserRoleMenu(Convert.ToInt64(drpParentMenu.SelectedValue), Convert.ToInt64(drpRoleMaster.SelectedValue));

                if (result != null)
                {
                    grdPermission.DataSource = result;
                    grdPermission.DataBind();
                }
            }
        }
    }
}