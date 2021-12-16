using ICWR.Data;
using ICWR.Models;
using InternalCargoWiseReport.Data.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace InternalCargoWiseReport.FrontEnd
{
    /// <summary>
    /// Summary description for EmailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class EmailService : System.Web.Services.WebService
    {

        [WebMethod]
        public void EmailIdExists(string EmailID )
        {
            
            bool EmailIDInUse = false;

            UserData _userData = new UserData();
            UserDto  request=new UserDto();
            request.EmailId=EmailID;

            EmailIDInUse = _userData.ChkMailID(request);

            Email email = new Email();
            email.Emailid = EmailID;
            email.EmailIDInUse = EmailIDInUse;

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(email));
        }
    }
}
