using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Office.Interop.Excel.Application; 
//using Microsoft.Office.Interop.Excel.Workbook;
//using Microsoft.Office.Interop.Excel.Worksheet;
//using Microsoft.Office.Interop.Excel.Range;
using ClosedXML.Excel;
using System.Threading;
using System.Globalization;

namespace ICWR.Data.Utility
{
    public class LovelyExport
    {
        public static void ExportDataSetToExcel(DataTable dt, string file)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    var s = wb.Worksheets.Add(dt);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    s.Tables.FirstOrDefault().ShowAutoFilter = false;
                    wb.Style.Font.Bold = true;
                    wb.SaveAs(file, false);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public static void ExporttoExcel(DataTable table, GridView GridView_Result)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = GridView_Result.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(GridView_Result.Columns[j].HeaderText.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public static void ExportToExcel(GridView Name)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + Utility.GetIndianDateTime().ToString("dd/MMM/yyyy") + ".xls");
            HttpContext.Current.Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Name.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        public static void ExportToWord(GridView Name)
        {
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Utility.GetIndianDateTime().ToString("dd/MMM/yyyy") + ".doc");
            HttpContext.Current.Response.Charset = string.Empty;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/doc"; ;
            Name.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            Name.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }
    }
    public static partial class Utility
    {
        //public static DataTable ToDataTable<T>(this List<T> items)
        // {
        //     DataTable dataTable = new DataTable(typeof(T).Name);
        //     PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //     foreach (PropertyInfo prop in Props)
        //     {
        //         dataTable.Columns.Add(prop.Name);
        //     }
        //     foreach (T item in items)
        //     {
        //         var values = new object[Props.Length];
        //         for (int i = 0; i < Props.Length; i++)
        //         {
        //             values[i] = Props[i].GetValue(item, null);
        //         }
        //         dataTable.Rows.Add(values);
        //     }
        //     return dataTable;
        // }
        public static Int32 ToInt(this string var)
        {
            return Convert.ToInt32(var);
        }
        public static DateTime GetIndianDateTime()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        }
        public static void ExportDataSetToExcel1(this DataSet ds, string file) 
        { 
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    var ws = wb.Worksheets.Add(ds.Tables[0], "Report");
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                    //  var ws = wb.Worksheets.Add("SCS1");
                    //  ws.Cell(1, 1).InsertTable(ds.Tables[0]);
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;

                    wb.Style.Font.Bold = true;
                    wb.SaveAs(file, false);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void ExportDataSetToExcel2(this DataSet ds, string file)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    var ws = wb.Worksheets.Add(ds.Tables[0], "Quater");
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count >= 1)
                    {
                        wb.Worksheets.Add(ds.Tables[1], "Cumulative");
                    }
                    //  var ws = wb.Worksheets.Add("SCS1");
                    //  ws.Cell(1, 1).InsertTable(ds.Tables[0]);
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;

                    wb.Style.Font.Bold = true;
                    wb.SaveAs(file, false);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void ExportDataTableToExcel1(this DataTable dt, string file)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    var ws = wb.Worksheets.Add(dt, "Customer");
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                    //  var ws = wb.Worksheets.Add("SCS1");
                    //  ws.Cell(1, 1).InsertTable(ds.Tables[0]);
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;

                    wb.Style.Font.Bold = true;
                    wb.SaveAs(file, false);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}