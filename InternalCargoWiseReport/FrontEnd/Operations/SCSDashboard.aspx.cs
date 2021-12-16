using ICWR.Data;
using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICWR.Data.Utility;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class SCSDashboard : System.Web.UI.Page
    {
        string MyData;
        string RegionData;
        string LineOfBusiness;
        string oppData;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindPipelineChart();
                bindGeographyChart();
                bindLineOfBusinessChart();
                getStatusStageAndBillingRecord();
                //Literal12.Text = MyData;
            }
        }

        private void bindPipelineChart()
        {
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getPipelineSplitRecord();
            if(results!=null)
            {
                dt = results.ToList().ToDataTable<WHLeadMasterDto>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    MyData = MyData + "{ Segment: '" + dt.Rows[i]["Segment"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                }
                Literal1.Text = MyData;
            }

           
            //DataRow dr = null;
            //dt.Columns.Add(new DataColumn("Country", typeof(string)));
            //dt.Columns.Add(new DataColumn("Value", typeof(string)));

            //dr = dt.NewRow();
            //dr["Country"] = "India";
            //dr["Value"] = "30";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Country"] = "Pakistan";
            //dr["Value"] = "10";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Country"] = "America";
            //dr["Value"] = "30";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Country"] = "Austrlia";
            //dr["Value"] = "30";
            //dt.Rows.Add(dr);

            ////Store the DataTable in ViewState for future reference   
            //ViewState["CurrentTable"] = dt;


            


            //Bind the Gridview   

        }

        private void bindGeographyChart()
        {
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getRegionRecord();
            if (results != null)
            {
                dt = results.ToList().ToDataTable<WHLeadMasterDto>();
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    RegionData = RegionData + "{ Region: '" + dt.Rows[i]["Region"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                }
                LitRegion.Text = RegionData;
            }
        }

        private void bindLineOfBusinessChart()
        {
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getLineOfBusinessRecord();
            if (results != null)
            {
                dt = results.ToList().ToDataTable<WHLeadMasterDto>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    LineOfBusiness = LineOfBusiness + "{ LineOfBusiness: '" + dt.Rows[i]["LineOfBusiness"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                }
                ltrLineOfBusiness.Text = LineOfBusiness;
            }
        }

        private void getStatusStageAndBillingRecord()
        {
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getStatusStageAndBillingRecord();
            if (results != null)
            {
                dt = results.ToList().ToDataTable<WHLeadMasterDto>();
                WHLeadMasterDto total = results.FirstOrDefault();
                txtTotal.Text = total.MonthlyBilling.ToString("0.00");

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    oppData = oppData + "{ StatusStage: '" + dt.Rows[i]["StatusStage"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                }
                ltrOpportunity.Text = oppData;
            }
        }
    }
}