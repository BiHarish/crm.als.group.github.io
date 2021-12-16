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
using InternalCargoWiseReport.Models;
using System.Threading;
using System.Globalization;

namespace InternalCargoWiseReport.FrontEnd.Operations
{
    public partial class CrmDashboard : System.Web.UI.Page
    {
        string MyData;
        string RegionData;
        string LineOfBusiness;
        string oppData;
        string convertedData;
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LovelySession.Lovely == null)
                {
                    Response.Redirect("/FrontEnd/SignIn.aspx");
                }
                bindDrp();
                bindPipelineChart();
                bindGeographyChart();
                bindLineOfBusinessChart();
                getStatusStageAndBillingRecord();
                getConvertedPipeline();
            }
        }

        private void bindPipelineChart()
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            ds = _whLeadMasterData.getPipelineSplitRecordForCrm(drpEmployee.SelectedValue, drpBU.SelectedValue);
            
            if (ds != null)
            {
               if(ds.Tables[0].Rows.Count>0)
               {
                   dt = ds.Tables[0];

                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       MyData = MyData + "{ Segment: '" + dt.Rows[i]["Segment"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                   }
                   Literal1.Text = MyData;
               }
               else
               {
                   Literal1.Text = null;
               }
            }
            else
            {
                Literal1.Text = null;
            }
        }

        private void bindGeographyChart()
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            ds = _whLeadMasterData.getPipelineSplitRecordForCrm(drpEmployee.SelectedValue, drpBU.SelectedValue);
           
            if (ds != null)
            {
               if(ds.Tables[1].Rows.Count>0)
               {
                   dt = ds.Tables[1];

                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       RegionData = RegionData + "{ Region: '" + dt.Rows[i]["Region"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                   }
                   LitRegion.Text = RegionData;
               }
               else
               {
                   LitRegion.Text = null;
               }
            }
            else
            {
                LitRegion.Text = null;
            }
        }

        private void bindLineOfBusinessChart()
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            ds = _whLeadMasterData.getPipelineSplitRecordForCrm(drpEmployee.SelectedValue, drpBU.SelectedValue);
            if (ds != null)
            {
               if(ds.Tables[2].Rows.Count>0)
               {
                   dt = ds.Tables[2];

                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       LineOfBusiness = LineOfBusiness + "{ LineOfBusiness: '" + dt.Rows[i]["LineOfBusiness"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                   }
                   ltrLineOfBusiness.Text = LineOfBusiness;
               }
               else
               {
                   ltrLineOfBusiness.Text = null;
               }
            }
            else
            {
                ltrLineOfBusiness.Text = null;
            }
        }

        private void getStatusStageAndBillingRecord()
        {
            txtTotal.Text = "0.00";
            ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            ds = _whLeadMasterData.getPipelineSplitRecordForCrm(drpEmployee.SelectedValue, drpBU.SelectedValue);
            if (ds != null)
            {
               if(ds.Tables[3].Rows.Count>0)
               {
                   dt = ds.Tables[3];

                   //WHLeadMasterDto total = results.FirstOrDefault();
                   //DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                   //txtTotal.Text = lastRow[1].ToString();
                   //if (txtTotal.Text != string.Empty)
                   //    Ruppe();
                   //lastRow.Delete();
                   //dt.AcceptChanges();
                   object Total;
                   Total = dt.Compute("Sum(Value)", string.Empty);
                   txtTotal.Text = Total.ToString();
                   Ruppe();
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       oppData = oppData + "{ StatusStage: '" + dt.Rows[i]["StatusStage"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                   }
                   ltrOpportunity.Text = oppData;
                  
               }
               else
               {
                   ltrOpportunity.Text = null;
               }
            }
            else
            {
                ltrOpportunity.Text = null;
            }
        }

        private void getConvertedPipeline()
        {
            txtTotalWon.Text = "0.00";
            txtTotalLost.Text = "0.00";
            ds = new DataSet();
            DataTable dt = new DataTable();
            WHLeadMasterData _whLeadMasterData = new WHLeadMasterData();
            ds = _whLeadMasterData.getPipelineSplitRecordForCrm(drpEmployee.SelectedValue, drpBU.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    dt = ds.Tables[4];

                    object totalWon;
                    object totalLoss;
                   // totalWon = dt.Compute("Sum(Value)", "stage=Won");
                   // totalLoss = dt.Compute("Sum(Value)", "stage=Lost");
                    double Won = dt.AsEnumerable().Where(row => row.Field<string>("Stage") == "Won").Sum(row => row.Field<double>("Value"));
                    double Lost = dt.AsEnumerable().Where(row => row.Field<string>("Stage") == "Lost").Sum(row => row.Field<double>("Value"));
                    txtTotalWon.Text = Won.ToString();  //totalWon.ToString();
                    txtTotalLost.Text = Lost.ToString();//totalLoss.ToString() ;
                    RuppeForConvertedPipeline();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        convertedData = convertedData + "{ StatusStage: '" + dt.Rows[i]["StatusStage"].ToString() + "', Value: '" + dt.Rows[i]["Value"].ToString() + "'},";
                    }
                    ltrLost.Text = convertedData;

                }
                else
                {
                    ltrLost.Text = null;
                }
            }
            else
            {
                ltrLost.Text = null;
            }
        }

        void Ruppe()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            
            string str = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

            string result = str +" " + txtTotal.Text;


             txtTotal.Text = string.Format(result, CultureInfo.InvariantCulture);

        }

        void RuppeForConvertedPipeline()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");

            string str = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

            string resultWon = str + " " + txtTotalWon.Text;
            string resultLoss = str + " " + txtTotalLost.Text;


            txtTotalWon.Text = string.Format(resultWon, CultureInfo.InvariantCulture);
            txtTotalLost.Text = string.Format(resultLoss, CultureInfo.InvariantCulture);

        }

        void bindDrp()
        {
            BDSolutionMasterData _bdSolutionMaster = new BDSolutionMasterData();
            IList<BDSolutionMasterDto> results = _bdSolutionMaster.GetAllBD();

            if (results != null)
            {
                drpEmployee.DataSource = results;
                drpEmployee.DataValueField = "ID";
                drpEmployee.DataTextField = "BD";
                drpEmployee.DataBind();
                drpEmployee.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
            ServiceTypeMasterData _serviceTypeMasterData = new ServiceTypeMasterData();
            IList<WhBuMasterDto> buresults = _serviceTypeMasterData.BUGetAll();
            if (buresults != null)
            {
                drpBU.DataSource = buresults;
                drpBU.DataValueField = "ID";
                drpBU.DataTextField = "Name";
                drpBU.DataBind();
                //drpBU.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }

        protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPipelineChart();
            bindGeographyChart();
            bindLineOfBusinessChart();
            getStatusStageAndBillingRecord();
            getConvertedPipeline();
        }

        protected void drpBU_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPipelineChart();
            bindGeographyChart();
            bindLineOfBusinessChart();
            getStatusStageAndBillingRecord();
            getConvertedPipeline();
            if(drpBU.SelectedValue=="5")
            {
                tblCurrCfsPipe.Visible = true;
                tblCurrScsPipe.Visible = false;

                tblExeCfsPipe.Visible = true;
                tblExeScsPipe.Visible = false;
            }
            else
            {
                tblCurrCfsPipe.Visible = false;
                tblCurrScsPipe.Visible = true;

                tblExeCfsPipe.Visible = false;
                tblExeScsPipe.Visible = true;
            }
        }
    }
}