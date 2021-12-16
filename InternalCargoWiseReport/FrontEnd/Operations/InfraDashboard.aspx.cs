﻿using ICWR.Data;
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
    public partial class InfraDashboard : System.Web.UI.Page
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
            IList<WHLeadMasterDto> results = _whLeadMasterData.getInfraPipelineSplitRecord();
            if (results != null)
            {
                dt = results.ToList().ToDataTable<WHLeadMasterDto>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    MyData = MyData + "{ Segment: '" + dt.Rows[i]["Segment"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                }
                Literal1.Text = MyData;
            }


           
        }

        private void bindGeographyChart()
        {
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            IList<WHLeadMasterDto> results = _whLeadMasterData.getInfraRegionRecord();
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
            IList<WHLeadMasterDto> results = _whLeadMasterData.getInfraLineOfBusinessRecord();
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
            IList<WHLeadMasterDto> results = _whLeadMasterData.getInfraStatusStageAndBillingRecord();
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