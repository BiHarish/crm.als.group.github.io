using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.MasterControl
{
    public partial class WhLeadDropDown : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool StageEnableFalse { get { return drpStage.Enabled; } set { drpStage.Enabled = value; } }
        public bool StatusStageEnable { get { return drpStatusStage.Enabled; } set { drpStatusStage.Enabled = value; } }
        public bool LineOfBusinessEnable { get { return drpLineOfBusiness.Enabled; } set { drpLineOfBusiness.Enabled = value; } }
        public bool StageEnableVisible { get { return drpStage.Visible; } set { drpStage.Visible = value; } }
        public bool StatusStageVisible { get { return drpStatusStage.Visible; } set { drpStatusStage.Visible = value; } }
        public bool LineOfBusinessVisible { get { return drpLineOfBusiness.Visible; } set { drpLineOfBusiness.Visible = value; } }
        public bool LineOfBusinessLabelVisible { get { return lblLineOfBusiness.Visible; } set { lblLineOfBusiness.Visible = value; } }
        public string Stage { get { return drpStage.SelectedValue; } set { drpStage.SelectedValue = value; } }
        public string StatusStage { get { return drpStatusStage.SelectedValue; } set { drpStatusStage.SelectedValue = value; } }
        public string LineOfBusiness { get { return drpLineOfBusiness.SelectedValue; } set { drpLineOfBusiness.SelectedValue = value; } }

        
    }
}