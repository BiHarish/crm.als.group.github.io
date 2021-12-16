using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class PermissionMasterDto
    {
        public int SrNo { get; set; }
        public long PermissionMasterId {get;set;}
        public long PermissionMasterMenuId { get; set; }
        public long PermissionMasterUserRoleId { get; set; }
        public string PermissionMasterMenuName { get; set; }
        public bool PermissionMasterMenuShow { get; set; }
        public bool PermissionMasterView { get; set; }
        public bool PermissionMasterAdd { get; set; }
        public bool PermissionMasterUpdate { get; set; }
        public bool PermissionMasterDelete { get; set; }
        public bool PermissionMasterPrint { get; set; }
        public bool PermissionMasterSelf { get; set; }

    }

    public class PermissionMasterShowDto
    {
        public List<RoleDto> RoleDisplay { get; set; }
       // public List<MainMenuDto> MainMenuDisplay { get; set; }
    }
}