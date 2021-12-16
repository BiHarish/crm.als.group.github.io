using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICWR.Data.Utility;
using ICWR.Models;

namespace ICWR.Data
{
    public class CustomBasePage : System.Web.UI.Page
    {
        override protected void OnPreInit(EventArgs e)
        {
            //Always call the base method when override
            // so what it originally did can still happen
            base.OnPreInit(e);

            //Example that just reads a querystring that
            // should be supported for all pages
            string myindexValue = Request.QueryString["lovelyindexing"];
            if (null != myindexValue)
            {
                int myindexintValue = 0;
                if (int.TryParse(myindexValue, out myindexintValue))
                {
                    if (LovelySession.Lovely != null)
                    {
                        if (LovelySession.Lovely.Permissions != null)
                        {

                            IEnumerable<PermissionMasterDto> PermissionFill = LovelySession.Lovely.Permissions.Where(x => x.PermissionMasterMenuId == myindexintValue && x.PermissionMasterMenuShow == true);
                            if (PermissionFill != null && PermissionFill.Any())
                            {
                                base.OnPreInit(e);
                            }
                            else
                            {
                                Response.Redirect("/FrontEnd/error/500Error.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("/FrontEnd/SignIn.aspx?RequestUrl=" + Request.Url.AbsoluteUri, false);
                            //Server.TransferRequest("/FrontEnd/SignIn.aspx", false);
                        }
                    }
                    else
                    {
                        //Response.Redirect("/FrontEnd/SignIn.aspx", false);
                        Response.Redirect("/FrontEnd/SignIn.aspx?RequestUrl=" +Request.Url.AbsoluteUri, false);
                    }
                }
            }
            else
            {
                //Server.TransferRequest("/FrontEnd/error/500Error.aspx", false);
                Response.Redirect("/FrontEnd/SignIn.aspx?RequestUrl=" + Request.Url.AbsoluteUri, false);
            }
        }
    }
}