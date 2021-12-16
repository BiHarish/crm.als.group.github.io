using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Data.Utility
{
    public class LovelyUserPermission
    {
        public UserDto User { get; set; } 
        public List<PermissionMasterDto> Permissions { get; set; }
        public List<PermissionSchemaMasterDto> PermissionsSchema { get; set; }
        public List<PermissionCompanyDto> PermissionsCompany { get; set; }
        public List<CurrencyMasterDto> PermissionsCurrencyMaster { get; set; }
    }
    public class LovelySession
    { 
        public static LovelyUserPermission Lovely
        {
            get
            {
                return (LovelyUserPermission)HttpContext.Current.Session["UserInfo"];
            }
            set
            {
                HttpContext.Current.Session["UserInfo"] = value;
            }
        }
        public static void fnDestroy()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }
    }
}