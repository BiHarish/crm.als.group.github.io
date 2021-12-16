using ICWR.Data;
using ICWR.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace ICWR.FrontEnd
{
    public class BackMenuHandler : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            BackMenuData _mainMenuData = new BackMenuData();
            List<BackMenuDto> menu = _mainMenuData.GetAllMenuAccordingPermission().ToList();
            List<BackMenuDto> myMenu = GetMenuTree(menu, null);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            context.Response.Write(javaScriptSerializer.Serialize(myMenu));
        }

        public List<BackMenuDto> GetMenuTree(List<BackMenuDto> list, int? parent)
        {
            return list.Where(x => x.BackMenuParentId == parent).Select(

             x => new BackMenuDto
             {
                 BackMenuId = x.BackMenuId,
                 BackMenuName = x.BackMenuName,
                 BackMenuURL = x.BackMenuURL,
                 BackMenuParentId = x.BackMenuParentId,
                 BackMenuChild = GetMenuTree(list, x.BackMenuId)
             }).ToList();
        }
        public bool IsReusable
        {
            get { return false; }
        }
    }
}