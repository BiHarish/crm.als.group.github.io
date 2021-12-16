using ICWR.Data;
using ICWR.Models;
using InternalCargoWiseReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.Reports
{
    public partial class SalesPersonWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDrp();
            }
        }
        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMaster = new BDSolutionMasterData();
            IList<BDSolutionMasterDto> results = _bdSolutionMaster.GetAllBD();

            if (results != null)
            {
                chkSalesPersonList.DataSource = results;
                chkSalesPersonList.DataValueField = "ID";
                chkSalesPersonList.DataTextField = "BD";
                chkSalesPersonList.DataBind();
            }
            ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            IList<WhBuMasterDto> buresults = _serviceTypeMasterData.BUGetAll();
            if (buresults != null)
            {
                chkBUList.DataSource = buresults;
                chkBUList.DataValueField = "ID";
                chkBUList.DataTextField = "Name";
                chkBUList.DataBind();
            }
        }
    }
}