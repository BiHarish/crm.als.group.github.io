using ICWR.Data;
using InternalCargoWiseReport.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ICWR.Data.Utility;

namespace InternalCargoWiseReport.FrontEnd.CSV
{
    public partial class XMLCargoWise : CustomBasePage
    {
        public CSVOur getmyValue { get { return (CSVOur)ViewState["CSVDATA"]; } set { ViewState["CSVDATA"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        
        {

        }
        class InvoiceClass
        {
            public InvoiceClass()
            {

            }
            public InvoiceClass(
                string a, string b = "", string c = "", string d = ""
                , string e = "", string f = "", string g = "", string h = ""
                , string i = "", string j = "", string k = "", string l = ""
                , string m = "", string n = "", string o = "", string p = ""
                )
            {
                A = a;
                B = b;
                C = c;
                D = d;
                E = e;
                F = f;
                G = g;
                H = h;
                I = i;
                J = j;
                K = k;
                L = l;
                M = m;
                N = n;
                O = o;
                P = p;
            }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
            public string F { get; set; }
            public string G { get; set; }
            public string H { get; set; }
            public string I { get; set; }
            public string J { get; set; }
            public string K { get; set; }
            public string L { get; set; }
            public string M { get; set; }
            public string N { get; set; }
            public string O { get; set; }
            public string P { get; set; }
        }
        public static string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //string[] chars = new string[] { "&", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            string[] chars = new string[] { "&" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        string getCustomerNameCode(string name)
        {
            try
            {
                var myfirstHit = getmyValue.customer.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower());

                if (myfirstHit != null)
                {
                    var secondhit = myfirstHit.SingleOrDefault();
                    if (secondhit != null)
                    {
                        return secondhit.Code;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        string getCustomerCode(string name)
        {
            try
            {
                var myfirstHit = getmyValue.customer.Where(x => x.GSTIN.Trim().ToLower() == name.Trim().ToLower());

                if (myfirstHit != null)
                {
                    var secondhit = myfirstHit.SingleOrDefault();
                    if (secondhit != null)
                    {
                        return secondhit.Code;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        string getTarriffCode(string name)
        {
            try
            {
                var myfirstHit = getmyValue.tarrif.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower());

                if (myfirstHit != null)
                {
                    var secondhit = myfirstHit.FirstOrDefault();
                    if (secondhit != null)
                    {
                        return secondhit.Code;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string getGSTORNOTGST(float value, string myValue)
        {
            if (value == 0)
                return "FREEGST";
            else
                return myValue;
        }
        protected void btnXML2CSV_Click(object sender, EventArgs e)
        {
            if (getmyValue == null)
            {
                return;
            }
            Boolean fileOK = false;
            string flowPath = "/Files/XML/";
           // string SAPath = HttpContext.Current.Request.ApplicationPath;
            string path = Server.MapPath( "/Files/XML/");

            if (fuXMLTOCSV.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fuXMLTOCSV.FileName).ToLower();

                if (fileExtension.Trim().ToLower() == ".xml")
                    fileOK = true;

                if (fileOK)
                {
                    try
                    {
                        string FileNameChanged = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        string Extension = System.IO.Path.GetExtension(fuXMLTOCSV.FileName).ToLower();
                        flowPath = flowPath + FileNameChanged + Extension;
                        string filePath = path + FileNameChanged + Extension;
                        fuXMLTOCSV.PostedFile.SaveAs(filePath);

                        string line = System.IO.File.ReadAllText(filePath);
                        string value = RemoveSpecialChars(line);

                        System.IO.File.WriteAllText(filePath, value);

                        XmlReader xmlFile;
                        xmlFile = XmlReader.Create(filePath, new XmlReaderSettings());
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlFile);

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 9)
                            {
                                if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                                {
                                    if (ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                                    {
                                        if (ds.Tables[9] != null && ds.Tables[9].Rows.Count > 0)
                                        {
                                            DataRowCollection rows = ds.Tables[7].Rows;
                                            List<InvoiceClass> invoice = new List<InvoiceClass>();
                                            List<InvoiceClass> invoiceWithGSTIN = new List<InvoiceClass>();
                                            List<InvoiceClass> invoiceWithoutGSTIN = new List<InvoiceClass>();
                                            
                                            int count = 0;
                                            foreach (DataRow row in rows)
                                            {
                                                InvoiceClass firstObjInvoice = new InvoiceClass("JOBINFO", "NIL");
                                                string InvoiceNumber = ";" + row["VOUCHERNUMBER"].ToString() + ";" + row["PARTYLEDGERNAME"].ToString();
                                                string InvoiceDate = row["DATE"].ToString();
                                                string customerCodem = drpMyExcelType.SelectedValue == "MCS" ? getCustomerNameCode(row["PARTYLEDGERNAME"].ToString()) : getCustomerCode(row["PARTYGSTIN"].ToString());
                                                DataRow[] invoiceAmountDataRow = ds.Tables[9].Select("NAME =" + "'" + row["VOUCHERNUMBER"].ToString() + "'");

                                                string InvoiceAmount = "0";
                                                if (invoiceAmountDataRow != null && invoiceAmountDataRow.Count() > 0)
                                                {
                                                    InvoiceAmount = invoiceAmountDataRow[0]["AMOUNT"].ToString().Replace("-", "");
                                                }

                                                InvoiceClass secondObjInvoice = new InvoiceClass("INVHEAD", "AR", customerCodem, "", "ADJ", "", InvoiceNumber, InvoiceDate
                                                    , "", "INR", InvoiceAmount, "", "", "", "", InvoiceDate);

                                                DataRow[] innerDataRow = ds.Tables[8].Select("VOUCHER_Id =" + "'" + count.ToString() + "'");
                                                count++;
                                                if (innerDataRow != null && innerDataRow.Count() > 0)
                                                {
                                                    invoice.Add(firstObjInvoice);
                                                    invoice.Add(secondObjInvoice);

                                                    int IGSTCount = 0;
                                                    int GSTCount = 0;
                                                    int defaultCount = 0;
                                                    int innerRowCount = 0;
                                                    int ledgerCount = 0;
                                                    float GST = 0;
                                                    string headerName = string.Empty;
                                                    string headerAmount = string.Empty;

                                                    foreach (DataRow innerRow in innerDataRow)
                                                    {
                                                        innerRowCount++;
                                                        if (innerRowCount == 1)
                                                        {
                                                            continue;
                                                        }

                                                        string checkOutput = innerRow["LEDGERNAME"].ToString();

                                                        switch (checkOutput)
                                                        {
                                                            case "SHORT AND EXCESS":
                                                                if (defaultCount == 1)
                                                                {
                                                                    string tarrifCode = getTarriffCode(headerName);
                                                                    InvoiceClass ObjInvoice = new InvoiceClass("INVLINE", tarrifCode, "", headerName, porttype.SelectedValue, drpMyExcelType.SelectedValue, headerAmount, "0"
                                                                      , getGSTORNOTGST(GST, "GST"), GST.ToString(), "N", "", "", "", "", "");

                                                                    defaultCount = 0;

                                                                    if (headerAmount != "0")
                                                                        invoice.Add(ObjInvoice);

                                                                    headerName = string.Empty;
                                                                    headerAmount = string.Empty;

                                                                }
                                                                string whatShowIGSTorGST = string.Empty;
                                                                if (IGSTCount == 1)
                                                                    whatShowIGSTorGST = "IGST";
                                                                else
                                                                    whatShowIGSTorGST = "GST";

                                                                defaultCount = 0;
                                                                ledgerCount = 0;
                                                                GSTCount = 0;
                                                                IGSTCount = 0;
                                                                GST = 0;
                                                                string tarrifCode1 = getTarriffCode(checkOutput);
                                                                InvoiceClass thirdObjInvoice = new InvoiceClass("INVLINE", tarrifCode1, "", checkOutput, porttype.SelectedValue, drpMyExcelType.SelectedValue, innerRow[4].ToString(), "0"
                                                           , getGSTORNOTGST(0, whatShowIGSTorGST), "0", "N", "", "", "", "", "");

                                                                if (innerRow[4].ToString() != "0.000")
                                                                    invoice.Add(thirdObjInvoice);

                                                                break;
                                                            case "CGST":
                                                                defaultCount = 0;
                                                                GSTCount++;
                                                                if (GSTCount == 1)
                                                                {
                                                                    string sGst = innerRow[4].ToString();
                                                                    GST = GST + sGst.ToFloat();
                                                                }
                                                                else if (GSTCount == 2)
                                                                {
                                                                    string cgst = innerRow[4].ToString();
                                                                    ledgerCount++;
                                                                    GST = GST + cgst.ToFloat();
                                                                    defaultCount = 0;
                                                                }
                                                                break;
                                                            case "SGST":
                                                                GSTCount++;
                                                                if (GSTCount == 2)
                                                                {
                                                                    string cgst = innerRow[4].ToString();
                                                                    ledgerCount++;
                                                                    GST = GST + cgst.ToFloat();
                                                                    defaultCount = 0;
                                                                }
                                                                else if (GSTCount == 1)
                                                                {
                                                                    string sGst = innerRow[4].ToString();
                                                                    GST = GST + sGst.ToFloat();
                                                                }
                                                                break;
                                                            case "IGST":
                                                                IGSTCount++;
                                                                if (IGSTCount == 1)
                                                                {
                                                                    string igst = innerRow[4].ToString();
                                                                    ledgerCount++;
                                                                    GST = GST + igst.ToFloat();
                                                                    defaultCount = 0;
                                                                }
                                                                break;
                                                            default:
                                                                ledgerCount = 0;
                                                                GSTCount = 0;
                                                                IGSTCount = 0;
                                                                GST = 0;
                                                                if (defaultCount == 1)
                                                                {
                                                                    string tarrifCode11 = getTarriffCode(headerName);

                                                                    InvoiceClass ObjInvoice = new InvoiceClass("INVLINE", tarrifCode11, "", headerName, porttype.SelectedValue, drpMyExcelType.SelectedValue, headerAmount, "0"
                                                                    , getGSTORNOTGST(GST, "GST"), GST.ToString(), "N", "", "", "", "", "");

                                                                    defaultCount = 0;

                                                                    if (headerAmount != "0")
                                                                        invoice.Add(ObjInvoice);

                                                                    headerName = string.Empty;
                                                                    headerAmount = string.Empty;
                                                                }
                                                                defaultCount++;
                                                                headerName = checkOutput;
                                                                headerAmount = innerRow[4].ToString();
                                                                break;
                                                        }

                                                        if (ledgerCount == 1)
                                                        {
                                                            if (GSTCount == 2)
                                                            {
                                                                string tarrifCode121 = getTarriffCode(headerName);
                                                                InvoiceClass thirdObjInvoice = new InvoiceClass("INVLINE", tarrifCode121, "", headerName, porttype.SelectedValue, drpMyExcelType.SelectedValue, headerAmount, "0"
                                                           , getGSTORNOTGST(GST, "GST"), GST.ToString(), "N", "", "", "", "", "");

                                                                if (headerAmount != "0")
                                                                    invoice.Add(thirdObjInvoice);

                                                                headerName = string.Empty;
                                                                headerAmount = string.Empty;

                                                            }
                                                            if (IGSTCount == 1)
                                                            {
                                                                string tarrifCode1221 = getTarriffCode(headerName);
                                                                InvoiceClass thirdObjInvoice = new InvoiceClass("INVLINE", tarrifCode1221, "", headerName, porttype.SelectedValue, drpMyExcelType.SelectedValue, headerAmount, "0"
                                                           , getGSTORNOTGST(GST, "IGST"), GST.ToString(), "N", "", "", "", "", "");

                                                                if (headerAmount != "0")
                                                                    invoice.Add(thirdObjInvoice);

                                                                headerName = string.Empty;
                                                                headerAmount = string.Empty;
                                                            }
                                                        }
                                                        if (defaultCount == 2)
                                                        {

                                                        }
                                                    }
                                                }

                                                if (invoice != null)
                                                {
                                                    if (string.IsNullOrEmpty(customerCodem) || customerCodem == "NULL" || customerCodem == "." || string.IsNullOrWhiteSpace(customerCodem))
                                                    {
                                                        invoiceWithoutGSTIN.AddRange(invoice);
                                                    }
                                                    else
                                                    {
                                                        invoiceWithGSTIN.AddRange(invoice);
                                                    }
                                                    invoice.Clear();
                                                }
                                            }
                                            DataTable dt;

                                            if (invoiceWithoutGSTIN != null || invoiceWithGSTIN != null)
                                            {
                                                invoiceWithoutGSTIN.AddRange(invoiceWithGSTIN);

                                                dt = invoiceWithoutGSTIN.ToList().ToDataTable1<InvoiceClass>();

                                                //using (XLWorkbook wb = new XLWorkbook())
                                                //{
                                                //    var s = wb.Worksheets.Add(dt);

                                                //    Response.Clear();
                                                //    Response.Buffer = true;
                                                //    Response.Charset = "";
                                                //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                                //    Response.AddHeader("content-disposition", "attachment;filename=" + FileNameChanged + ".csv");
                                                //    using (MemoryStream MyMemoryStream = new MemoryStream())
                                                //    {
                                                //        wb.SaveAs(MyMemoryStream);
                                                //        MyMemoryStream.WriteTo(Response.OutputStream);
                                                //        Response.Flush();
                                                //        Response.End();
                                                //    }
                                                //  }

                                                string csv = string.Empty;

                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    foreach (DataColumn column in dt.Columns)
                                                    {
                                                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                                    }
                                                    csv += "\r\n";
                                                }
                                                //Download the CSV file.
                                                Response.Clear();
                                                Response.Buffer = true;
                                                Response.AddHeader("content-disposition", "attachment;filename=" + FileNameChanged + ".csv");
                                                Response.Charset = "";
                                                Response.ContentType = "application/text";
                                                Response.Output.Write(csv);
                                                Response.Flush();
                                                Response.End();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;   
                    }
                }
            }
        }
        protected void loadCodes_Click(object sender, EventArgs e)
        {
            if (getmyValue == null)
            {
                PanvelCSVData panvelCSV = new PanvelCSVData();
                getmyValue = panvelCSV.GetData();
            }
        }
    }
}