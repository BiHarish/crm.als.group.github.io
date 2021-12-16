using ICWR.Data;
using ICWR.Data.Utility;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICWR.FrontEnd
{
    public partial class SignOut : System.Web.UI.Page
    {
        WHLeadMasterData _whLeadMasterData = null;
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
           // BindData();
            if(LovelySession.Lovely==null)
            {
                Response.Redirect("/FrontEnd/SignIn.aspx");
            }
            updateAuditReport();
            Session.Abandon();
                 Response.Redirect("/FrontEnd/SignIn.aspx");
        }
        private void BindData()
        {
            LovelySession.fnDestroy();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Redirect("/Default.aspx", false);
        }

        void updateAuditReport()
        {
            _whLeadMasterData = new WHLeadMasterData();
            WHLeadMasterDto request = new WHLeadMasterDto();
            request.UserID = LovelySession.Lovely.User.Id;
            request.ID = Session["whLogID"].ToDataConvertInt64();
            _whLeadMasterData.UpdateWhLeadLogs(request);
           // Session["whLogID"] = null;
        }
        #endregion
    }
}