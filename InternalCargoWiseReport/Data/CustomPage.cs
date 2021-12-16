using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Data
{
    public static class CustomPage
    {
        public enum TypePermission
        {
            Add = 1,
            Update = 2,
            Self = 3,
            Delete = 4,
            Print = 5,
            View = 6
        }

        public static bool isPermission(System.Web.UI.Page page, TypePermission type)
        {
            string myindexValue = page.Request.QueryString["lovelyindexing"]; 
            if (null != myindexValue)
            {
                int myindexintValue = 0;
                if (int.TryParse(myindexValue, out myindexintValue))
                {
                    if (LovelySession.Lovely != null)
                    {
                        if (LovelySession.Lovely.Permissions != null)
                        {
                            IEnumerable<PermissionMasterDto> PermissionFill = LovelySession.Lovely.Permissions.Where(x => x.PermissionMasterMenuId == myindexintValue);
                            if (PermissionFill != null && PermissionFill.Any())
                            {
                                bool result = false;
                                switch (type)
                                {
                                    case TypePermission.Add:
                                         result = PermissionFill.SingleOrDefault().PermissionMasterAdd;
                                        break;
                                    case TypePermission.Delete:
                                        result = PermissionFill.SingleOrDefault().PermissionMasterDelete;
                                        break;
                                    case TypePermission.Update:
                                        result = PermissionFill.SingleOrDefault().PermissionMasterUpdate;
                                        break;
                                    case TypePermission.Print:
                                        result = PermissionFill.SingleOrDefault().PermissionMasterPrint;
                                        break;
                                    case TypePermission.Self:
                                        result = PermissionFill.SingleOrDefault().PermissionMasterSelf;
                                        break;
                                    case TypePermission.View:
                                        result = PermissionFill.SingleOrDefault().PermissionMasterView;
                                        break;
                                    default:
                                        break;
                                }
                                return result;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}