using ICWR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.ApplicationBlocks.Data;
using ICWR.Data.Utility;
using System.Web.Script.Serialization;
using ICWR.Data;
using System.IO;
using System.Text;


namespace InternalCargoWiseReport
{
    /// <summary>
    /// Summary description for CrmEnquiry
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CrmEnquiry : System.Web.Services.WebService
    {
        string getInquiryNo(string OrgName)
        {
            MaxValData _maxvalData = new MaxValData();
            MaxValDto result = _maxvalData.GetByDescription("UserInquiry");
            if (result != null)
            {
                return "Enq/" + OrgName.Substring(0, 3) + "/" + DateTime.Now.ToString("yyyymmddhhmm") + "/" + result.Value;
            }

            return string.Empty;
        }

        void updateMaxVal()
        {
            MaxValData _maxvalData = new MaxValData();
            MaxValDto obj = new MaxValDto();
            obj.Description = "UserInquiry";
            _maxvalData.Update(obj);
        }

     //  [WebService(Namespace = "http://tempuri.org/")]
     //  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
        

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void saveCrmEnquiry(string Type, string EnquiryDate, string OrgName, string OrgAddress, string CountryID, string CityName, string PostalCode, string State, string Website, string RegNo, string SalesRepID, string LeadInterest, string EnquiryContact, string Phone, string Email, string Mobile, string Fax, string JobDescription, string CreateOn, string CreateBy,string FName,string FileData)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[25];

            Param[0] = new SqlParameter("@OrgEnquiryType", SqlDbType.NVarChar);
            Param[0].Value = Type;

            Param[1] = new SqlParameter("@OrgName", SqlDbType.NVarChar);
            Param[1].Value = OrgName;

            Param[2] = new SqlParameter("@OrgAddress", SqlDbType.NVarChar);
            Param[2].Value = OrgAddress;

            if (CountryID != string.Empty)
            {
                Param[3] = new SqlParameter("@OrgCountry", SqlDbType.BigInt);
                Param[3].Value = CountryID;
            }
            else
            {
                Param[3] = new SqlParameter("@OrgCountry", SqlDbType.BigInt);
                Param[3].Value = DBNull.Value;
            }


            Param[4] = new SqlParameter("@OrgCity", SqlDbType.NVarChar);
            Param[4].Value = CityName;

            Param[5] = new SqlParameter("@OrgPostCode", SqlDbType.NVarChar);
            Param[5].Value = PostalCode;

            Param[6] = new SqlParameter("@OrgState", SqlDbType.NVarChar);
            Param[6].Value = State;

            Param[7] = new SqlParameter("@OrgWebsite", SqlDbType.NVarChar);
            Param[7].Value = Website;

            Param[8] = new SqlParameter("@OrgRegNo", SqlDbType.NVarChar);
            Param[8].Value = RegNo;

            Param[9] = new SqlParameter("@InquiryContact", SqlDbType.NVarChar);
            Param[9].Value = EnquiryContact;

            Param[10] = new SqlParameter("@Phone", SqlDbType.NVarChar);
            Param[10].Value = Phone;

            Param[11] = new SqlParameter("@Email", SqlDbType.NVarChar);
            Param[11].Value = Email;

            Param[12] = new SqlParameter("@MobNo", SqlDbType.NVarChar);
            Param[12].Value = Mobile;

            Param[13] = new SqlParameter("@FaxNo", SqlDbType.NVarChar);
            Param[13].Value = Fax;

            Param[14] = new SqlParameter("@JobDesc", SqlDbType.NVarChar);
            Param[14].Value = JobDescription;

            Param[15] = new SqlParameter("@LeadIntrest", SqlDbType.NVarChar);
            Param[15].Value = LeadInterest;

            if (SalesRepID != string.Empty)
            {
                Param[16] = new SqlParameter("@SalesRepName", SqlDbType.BigInt);
                Param[16].Value = SalesRepID;
            }
            else
            {
                Param[16] = new SqlParameter("@SalesRepName", SqlDbType.BigInt);
                Param[16].Value = DBNull.Value;
            }
       

            Param[17] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[17].Value = CreateBy;

            if(CreateOn!=string.Empty)
            {
                Param[18] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
                Param[18].Value = CreateOn;
            }
            else
            {
                Param[18] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
                Param[18].Value = DBNull.Value;
            }
            

            Param[19] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[19].Value = 1;

            if(EnquiryDate!=string.Empty)
            {
                Param[20] = new SqlParameter("@InquiryDate", SqlDbType.DateTime);
                Param[20].Value = EnquiryDate;
            }
            else
            {
                Param[20] = new SqlParameter("@InquiryDate", SqlDbType.DateTime);
                Param[20].Value = DBNull.Value;
            }
          

            Param[21] = new SqlParameter("@OrgInquiryID", SqlDbType.NVarChar);
            Param[21].Value = getInquiryNo(OrgName);

            Param[22] = new SqlParameter("@CommType", SqlDbType.NVarChar);
            Param[22].Value = "Mobile";

            Param[23] = new SqlParameter("@FName", SqlDbType.NVarChar);
            Param[23].Value = FName;

            Param[24] = new SqlParameter("@FileData", SqlDbType.VarBinary);
            byte[] newBytes = Convert.FromBase64String(FileData);
            Param[24].Value = newBytes;

            //Param[25] = new SqlParameter("@TestingField", SqlDbType.NVarChar);
            //Param[25].Value = TestingField;
            
            
            //Stream str = toStream(FileData);
            //BinaryReader br = new BinaryReader(str);
            //byte[] size = br.ReadBytes((int)str.Length);
            //Param[24].Value = size;
            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Param);

                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    if (i != null)
                    {
                        dr1["Result"] = i;
                        dt.Rows.Add(dr1);
                        updateMaxVal();

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                            Context.Response.Write(resultJSON);
                        }
                    }
                    else
                    {
                        dr1["Result"] = "Data Not Saved";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                            Context.Response.Write(resultJSON);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Saved";
                    dt.Rows.Add(dr1);


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                        Context.Response.Write(resultJSON);
                    }
                }

                finally
                {

                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void saveCrmopportunity(string InquiryID, string OppOrigin, string OppDestination, string OppMode, string OppContainer, string OppContType, string OppContainerCount, string OppRecurring, string OppVerticalMarket, string OppActivityPeriod, string OppCarrier, string CreateOn, string Weight, string Unit, string CreateBy, string CommodityID, string Competitor, string Terms, string CountType)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[20];
            Param[0] = new SqlParameter("@InquiryID", SqlDbType.BigInt);
            Param[0].Value = InquiryID;

            Param[1] = new SqlParameter("@OppOrigin", SqlDbType.NVarChar);
            Param[1].Value = OppOrigin;

            Param[2] = new SqlParameter("@OppDestination", SqlDbType.NVarChar);
            Param[2].Value = OppDestination;

            Param[3] = new SqlParameter("@OppMode", SqlDbType.NVarChar);
            Param[3].Value = OppMode;

            Param[4] = new SqlParameter("@OppContainer", SqlDbType.NVarChar);
            Param[4].Value = OppContainer;

            Param[5] = new SqlParameter("@OppContType", SqlDbType.NVarChar);
            Param[5].Value = OppContType;

            Param[6] = new SqlParameter("@OppContainerCount", SqlDbType.NVarChar);
            Param[6].Value = OppContainerCount;

            Param[7] = new SqlParameter("@OppRecurring", SqlDbType.NVarChar);
            Param[7].Value = OppRecurring;

            Param[8] = new SqlParameter("@OppVerticalMarket", SqlDbType.NVarChar);
            Param[8].Value = OppVerticalMarket;

            Param[9] = new SqlParameter("@OppActivityPeriod", SqlDbType.NVarChar);
            Param[9].Value = OppActivityPeriod;

            Param[10] = new SqlParameter("@OppCarrier", SqlDbType.NVarChar);
            Param[10].Value = OppCarrier;

            Param[11] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[11].Value = CreateBy;

            Param[12] = new SqlParameter("@CreateOn", SqlDbType.DateTime);
            Param[12].Value = DateTime.Now;

            Param[13] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[13].Value = 1;

            if(Weight==string.Empty)
            {
                Param[14] = new SqlParameter("@Weight", SqlDbType.Float);
                Param[14].Value = DBNull.Value;
            }
            else
            {
                Param[14] = new SqlParameter("@Weight", SqlDbType.Float);
                Param[14].Value = Weight;
            }
            

            Param[15] = new SqlParameter("@Unit", SqlDbType.NVarChar);
            Param[15].Value = Unit;

            Param[16] = new SqlParameter("@CommodityID", SqlDbType.BigInt);
            Param[16].Value = CommodityID;

            Param[17] = new SqlParameter("@Competitor", SqlDbType.NVarChar);
            Param[17].Value = Competitor;

            Param[18] = new SqlParameter("@Terms", SqlDbType.NVarChar);
            Param[18].Value = Terms;

            Param[19] = new SqlParameter("@CountType", SqlDbType.NVarChar);
            Param[19].Value = CountType;
            if (Param != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Param);

                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    if (i >= 1)
                    {
                        dr1["Result"] = "Record successfully saved";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                    else
                    {
                        dr1["Result"] = "Data Not Saved ";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Saved ";
                    dt.Rows.Add(dr1);


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();


                    }
                }

                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getByOrgName(string OrgName,string CreateBy)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@OrgName", SqlDbType.NVarChar);
            Param[0].Value = OrgName;
            Param[1] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[1].Value = 4;
            Param[2] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
            Param[2].Value = CreateBy;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Param);

                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data Not Found ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Found ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getByInquiryNo(string InquiryNo)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@OrgInquiryId", SqlDbType.NVarChar);
            Param[0].Value = InquiryNo;
            Param[1] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[1].Value = 2;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Param);

                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data Not Found ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Found ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CheckopportunityDataExistOrNot(string SalesRepName, string InquiryDate, string OrgName, string Origin, string Destination, string ContainerCount)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@SalesRepName", SqlDbType.NVarChar);
            Param[0].Value = SalesRepName;
            Param[1] = new SqlParameter("@InquiryDate", SqlDbType.NVarChar);
            Param[1].Value = InquiryDate;
            Param[2] = new SqlParameter("@OrgName", SqlDbType.NVarChar);
            Param[2].Value = OrgName;
            Param[3] = new SqlParameter("@Origin", SqlDbType.NVarChar);
            Param[3].Value = Origin;
            Param[4] = new SqlParameter("@Destination", SqlDbType.NVarChar);
            Param[4].Value = Destination;
            Param[5] = new SqlParameter("@ContainerCount", SqlDbType.NVarChar);
            Param[5].Value = ContainerCount;
            Param[6] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[6].Value = 3;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Param);

                    dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data Already Exist,Still you want to continue.. ";
                        dt.Rows.Add(dr1);

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data not saved already you can save it. ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Error ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Country()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[2];

            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = 4;
            Param[1] = new SqlParameter("@CountryIsActive", SqlDbType.Bit);
            Param[1].Value = 1;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCountry", Param);

                    dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data not found. ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Error ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CountainerData()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = 1;

            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspContainerMaster", Param);

                    dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data not found. ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Error ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CommodityData()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = 1;

            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspCommodityMaster", Param);

                    dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data not found. ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Error ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SalesRepresentative()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[0].Value = 1;

            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspSalesRepMaster", Param);

                    dt = ds.Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data not found. ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Error ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void OpportunityUpdate(string OpportunityID, string Status, string UpdateBy)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[4];
            Param[0] = new SqlParameter("@Id", SqlDbType.BigInt);
            Param[0].Value = OpportunityID;
            Param[1] = new SqlParameter("@Status", SqlDbType.NVarChar);
            Param[1].Value = Status;
            Param[2] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
            Param[2].Value = UpdateBy;
            Param[3] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[3].Value = 3;

            if (Param != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Param);

                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    if (i >= 1)
                    {
                        dr1["Result"] = "Record successfully updated";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                    else
                    {
                        dr1["Result"] = "Data Not Saved ";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Saved ";
                    dt.Rows.Add(dr1);


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();


                    }
                }

                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getByOpportunityID(string ID)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = ID.ToNullLong();
            Param[1] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[1].Value = 2;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateOpportunity", Param);

                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data Not Found ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Found ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void geyByEnquiryIDForImgDownload(string ID)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";


            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@ID", SqlDbType.BigInt);
            Param[0].Value = ID.ToNullLong();
            Param[1] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[1].Value = 6;


            if (Param != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    ds = SqlHelper.ExecuteDataset(LovelyPair.connection, CommandType.StoredProcedure, "uspInsertUpdateEnquiry", Param);

                    dt = ds.Tables[0];
                    byte[] newBytes = (byte[])dt.Rows[0]["FileData"];
                    dt.Columns.Add("FData", typeof(string));
                    dt.Rows[0]["FData"] = Convert.ToBase64String(newBytes); 
                    dt.AcceptChanges();
                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                    else
                    {
                        DataRow dr1 = null;
                        dt = null;
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        dr1 = dt.NewRow();
                        dr1["Result"] = "Data Not Found ";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Found ";
                    dt.Rows.Add(dr1);

                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                    }
                }
                finally
                {
                    Context.Response.Write(resultJSON);
                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PostIncidentReportingData(string Incident, string ReportedBy, string Department, string Email, string MobileNo, string OccuranceDate, string OccuranceTime, string ImpactedPersonName, string ImpactedMobileNo, string IncidentLocation, string chkAccident, string chkIncident, string chkNearMiss, string chkViolence, string chkIllHealth, string chkSafety, string IncidentReport, string IncidentDescribe, string IncidentDescribeMeasures, string CreateBy)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            SqlParameter[] Param = new SqlParameter[21];

            Param[0] = new SqlParameter("@Incident", SqlDbType.NVarChar);
            Param[0].Value = Incident;

            Param[1] = new SqlParameter("@ReportedBy", SqlDbType.NVarChar);
            Param[1].Value = ReportedBy;

            Param[2] = new SqlParameter("@Department", SqlDbType.NVarChar);
            Param[2].Value = Department;


            Param[3] = new SqlParameter("@Email", SqlDbType.NVarChar);
            Param[3].Value = Email;



            Param[4] = new SqlParameter("@MobileNo", SqlDbType.NVarChar);
            Param[4].Value = MobileNo;

            Param[5] = new SqlParameter("@OccuranceDate", SqlDbType.NVarChar);
            Param[5].Value = OccuranceDate;

            Param[6] = new SqlParameter("@OccuranceTime", SqlDbType.NVarChar);
            Param[6].Value = OccuranceTime;

            Param[7] = new SqlParameter("@ImpactedPersonName", SqlDbType.NVarChar);
            Param[7].Value = ImpactedPersonName;

            Param[8] = new SqlParameter("@ImpactedMobileNo", SqlDbType.NVarChar);
            Param[8].Value = ImpactedMobileNo;

            Param[9] = new SqlParameter("@IncidentLocation", SqlDbType.NVarChar);
            Param[9].Value = IncidentLocation;

            Param[10] = new SqlParameter("@chkAccident", SqlDbType.Bit);
            Param[10].Value = chkAccident;

            Param[11] = new SqlParameter("@chkIncident", SqlDbType.Bit);
            Param[11].Value = chkIncident;

            Param[12] = new SqlParameter("@chkNearMiss", SqlDbType.Bit);
            Param[12].Value = chkNearMiss;

            Param[13] = new SqlParameter("@chkViolence", SqlDbType.Bit);
            Param[13].Value = chkViolence;

            Param[14] = new SqlParameter("@chkIllHealth", SqlDbType.Bit);
            Param[14].Value = chkIllHealth;

            Param[15] = new SqlParameter("@chkSafety", SqlDbType.Bit);
            Param[15].Value = chkSafety;


            Param[16] = new SqlParameter("@IncidentReport", SqlDbType.NVarChar);
            Param[16].Value = IncidentReport;


            Param[17] = new SqlParameter("@IncidentDescribe", SqlDbType.NVarChar);
            Param[17].Value = IncidentDescribe;

            Param[18] = new SqlParameter("@IncidentDescribeMeasures", SqlDbType.NVarChar);
            Param[18].Value = IncidentDescribeMeasures;

            Param[19] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
            Param[19].Value = CreateBy;

            Param[20] = new SqlParameter("@Flag", SqlDbType.Int);
            Param[20].Value = 1;

           
            if (Param != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(LovelyPair.connection, CommandType.StoredProcedure, "uspwhIncidientReporting", Param);

                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    if (i.ToDataConvertInt64()>0)
                    {
                        dr1["Result"] = "Incident Report has successfully saved";
                        dt.Rows.Add(dr1);

                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                            Context.Response.Write(resultJSON);
                        }
                    }
                    else
                    {
                        dr1["Result"] = "Data already saved with same Incident";
                        dt.Rows.Add(dr1);


                        if (dt.Rows.Count > 0)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                            Dictionary<String, Object> row;
                            foreach (DataRow dr in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {

                                    row.Add(col.ColumnName, dr[col].ToString());
                                }
                                tableRows.Add(row);
                            }
                            resultJSON = serializer.Serialize(tableRows).ToString();
                            Context.Response.Write(resultJSON);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DataTable dt = new DataTable();
                    DataRow dr1 = null;
                    dt.Columns.Add(new DataColumn("Result", typeof(string)));
                    dr1 = dt.NewRow();
                    dr1["Result"] = "Data Not Saved";
                    dt.Rows.Add(dr1);


                    if (dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                        Dictionary<String, Object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {

                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            tableRows.Add(row);
                        }
                        resultJSON = serializer.Serialize(tableRows).ToString();
                        Context.Response.Write(resultJSON);
                    }
                }

                finally
                {

                }
            }
        }
    }
}
