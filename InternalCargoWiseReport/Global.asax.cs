using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ICWR.Data.Utility;

namespace InternalCargoWiseReport
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        void Session_End(object sender, EventArgs e)
        {
            if (Session["whLogID"] != null)
            {
                WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
                WHLeadMasterDto request = new WHLeadMasterDto();
                //request.UserID = LovelySession.Lovely.User.Id;
                request.ID = Session["whLogID"].ToDataConvertInt64();
                _whLeadMasterData.UpdateWhLeadLogs(request);
                Session["whLogID"] = null;
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}