using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalCargoWiseReport.FrontEnd.CargowiseDashboard
{
    public partial class CfsSouthClusterDashboard : System.Web.UI.Page
    {
        string MonthWise;
        string YearWise20ftImport;
        string YearWise40ftImport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                PostEvent_Data();
            }
          
        }

        public class EventTB
        {
            public string searchdate { get; set; }
        }

        void PostEvent_Data()
        {
            YearWise20ftImport = string.Empty;
            YearWise40ftImport=string.Empty;
            using (var client = new WebClient())
            {
                EventTB objtb = new EventTB(); //Setting parameter to post  
                objtb.searchdate = Convert.ToDateTime(txtDate.Text).ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString("http://103.238.229.187/WebService/VolumeDetails.php?searchdate=" + objtb.searchdate, JsonConvert.SerializeObject(objtb));
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                if(dt!=null && dt.Rows.Count>0)
                {
                    DataTable dtData = new DataTable();

                    IEnumerable<DataRow> varImportYearly = from row in dt.AsEnumerable()
                                                   where 
                                                          row.Field<string>("Dept") == "Import"
                                                         && row.Field<string>("Type") == "Yearly"
                                                   select row;


                    dtData = varImportYearly.CopyToDataTable<DataRow>();
                    gvImportYearly.DataSource = dtData;
                    gvImportYearly.DataBind();

                    dtData = new DataTable();

                    IEnumerable<DataRow> varImportMonthly = from row in dt.AsEnumerable()
                                                           where
                                                                  row.Field<string>("Dept") == "Import"
                                                                 && row.Field<string>("Type") == "Monthly"
                                                           select row;


                    dtData = varImportMonthly.CopyToDataTable<DataRow>();
                    gvImportMonthly.DataSource = dtData;
                    gvImportMonthly.DataBind();

                    dtData = new DataTable();

                    IEnumerable<DataRow> varImportDaily = from row in dt.AsEnumerable()
                                                            where
                                                                   row.Field<string>("Dept") == "Import"
                                                                  && row.Field<string>("Type") == "Daily"
                                                            select row;


                    dtData = varImportDaily.CopyToDataTable<DataRow>();
                    gvImportDaily.DataSource = dtData;
                    gvImportDaily.DataBind();


                    //Export Start
                    dtData = new DataTable();

                    IEnumerable<DataRow> varExportYearly = from row in dt.AsEnumerable()
                                                           where
                                                                  row.Field<string>("Dept") == "Export"
                                                                 && row.Field<string>("Type") == "Yearly"
                                                           select row;


                    dtData = varExportYearly.CopyToDataTable<DataRow>();
                    gvExportYearly.DataSource = dtData;
                    gvExportYearly.DataBind();

                    dtData = new DataTable();

                    IEnumerable<DataRow> varExportMonthly = from row in dt.AsEnumerable()
                                                            where
                                                                   row.Field<string>("Dept") == "Export"
                                                                  && row.Field<string>("Type") == "Monthly"
                                                            select row;


                    dtData = varExportMonthly.CopyToDataTable<DataRow>();
                    gvExportMonthly.DataSource = dtData;
                    gvExportMonthly.DataBind();

                    dtData = new DataTable();

                    IEnumerable<DataRow> varExportDaily = from row in dt.AsEnumerable()
                                                          where
                                                                 row.Field<string>("Dept") == "Export"
                                                                && row.Field<string>("Type") == "Daily"
                                                          select row;


                    dtData = varExportDaily.CopyToDataTable<DataRow>();
                    gvExportDaily.DataSource = dtData;
                    gvExportDaily.DataBind();
                   
                    #region Cmnt
                    //if (dtdata != null && dtdata.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtdata.Rows.Count; i++)
                    //    {

                    //        YearWise20ftImport = YearWise20ftImport + "{ Name: '" + dtdata.Rows[i]["Company"].ToString() + " " + dtdata.Rows[i]["Name"].ToString() + "', Value: '" + dtdata.Rows[i]["Value"].ToString() + "'},";
                    //    }
                    //    ltrYearWise20ft.Text = YearWise20ftImport;
                    //}

                    //dtdata = new DataTable();

                    //IEnumerable<DataRow> dyi40ft = from row in dt.AsEnumerable()
                    //                              where row.Field<string>("Name") == "40"
                    //                                    && row.Field<string>("Dept") == "Import"
                    //                                    && row.Field<string>("Type") == "Yearly"
                    //                              select row;

                    //dtdata = dyi40ft.CopyToDataTable<DataRow>();

                    //if (dtdata != null && dtdata.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtdata.Rows.Count; i++)
                    //    {

                    //        YearWise40ftImport = YearWise40ftImport + "{ Name: '" + dtdata.Rows[i]["Company"].ToString() + " " + dtdata.Rows[i]["Name"].ToString() + "', Value: '" + dtdata.Rows[i]["Value"].ToString() + "'},";
                    //    }
                    //    ltrYearWise40ft.Text = YearWise40ftImport;
                    //}
                    #endregion
                }
            }
        }


        protected void lnkButton_Click(object sender, EventArgs e)
        {
            PostEvent_Data();
        }
    }
}