using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class PermissionCompanyDto
    {
        public int SrNo { get; set; }
        public long PermissionCompanyId { get; set; }
        public long PermissionCompanyCompanyId { get; set; }
        public long PermissionCompanyUserRoleId { get; set; }
        public string PermissionCompanyName { get; set; }
        public bool PermissionCompanyview { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyUniqueNumber { get; set; }
        public string Combine_CompanyUnique_USD_Code { get; set; }
    }

    public class PermissionCompanyShowDto
    {
        public List<RoleDto> RoleDisplay { get; set; }
        // public List<MainMenuDto> MainMenuDisplay { get; set; }
    }
}
