using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using ClosedXML.Excel;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace ICWR.Data.Utility
{
    public static class ExtensionMethods
    {
        public static TimeSpan? ToNullTimeSpan(this string var)
        {
            if (!string.IsNullOrEmpty(var))
            {
                TimeSpan time;
                if (TimeSpan.TryParse(var, out time))
                {
                    return time;
                }
            }
            return null;
        }
        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static Int32 ToConvertInt(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return 0;
            else
                return Convert.ToInt32(var);
        }

        public static long ToLong(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return 0;
            else
                return Convert.ToInt64(var);
        }

        public static Double ToDouble(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return 0;
            else
                return Convert.ToDouble(var);
        }

        public static float ToFloat(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return 0;
            else
                return float.Parse(var, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ToDataTable1<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// In string without slash convert into date
        /// </summary>
        /// <param name="date">Date must in format of ddMMyyyy</param>
        /// <returns></returns>
        /// 
        public static DateTime? ToWithoutSlashDateToDate(this string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime dt;
                if (DateTime.TryParseExact(date.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out dt))
                {
                    return dt;
                }
            }
            return null;
        }

        public static String ToConvertNullDate(this DateTime? var)
        {
            if (var == null)
                return null;
            else
                return var.Value.ToString("dd MMM yyyy");
        }

        public static DateTime? ToConvertNullDateTime(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return null;
            else
                return Convert.ToDateTime(var);
        }

        public static DateTime? ToConvertDateTime(this string var)
        {
            if (!string.IsNullOrEmpty(var))
                return Convert.ToDateTime(var);
            else
                return Convert.ToDateTime(var);
        }


        public static Int32? ToConvertNullInt(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return (Int32?)null;
            else
                return Convert.ToInt32(var);
        }

        public static Int64? ToNullLong(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return (Int64?)null;
            else
                return Convert.ToInt64(var);
        }

        public static Double? ToNullDouble(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return (double?)null;
            else
                return Convert.ToDouble(var);
        }

        public static float? ToNullFloat(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return (float?)null;
            else
                return Convert.ToSingle(var);
        }



        public static bool? ToNullBool(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return (bool?)null;
            else
                return Convert.ToBoolean(var);
        }

        public static Decimal ToDecimal(this string var)
        {
            if (string.IsNullOrEmpty(var))
                return 0;
            else
                return Convert.ToDecimal(var);
        }

        public static void ExportDataSetToExcel(this DataSet ds, string file)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    wb.Worksheets.Add(ds.Tables[0]);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wb.Style.Font.Bold = true;
                    wb.SaveAs(file, false);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static string AllJobNumber(string name, string value, string place = "Mum")
        {
            string fincalYear;

            if (DateTime.Now.Month < 4)
            {
                fincalYear = Convert.ToString(DateTime.Now.Year - 1) + "-" + Convert.ToString(DateTime.Now.Year).Substring(2, 2);  //.Substring(2,2)
            }
            else
            {
                fincalYear = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Year + 1).Substring(2, 2);  //.Substring(2,2)
            }

            return "Apo/" + place + "/" + name + "/" + fincalYear + "/" + value;
        }
        public static string JobNumberForInvoice(string name, string value)
        {
            string fincalYear;

            if (DateTime.Now.Month < 4)
            {
                fincalYear = Convert.ToString(DateTime.Now.Year - 1).Substring(2, 2) + "-" + Convert.ToString(DateTime.Now.Year).Substring(2, 2);  //.Substring(2,2)
            }
            else
            {
                fincalYear = Convert.ToString(DateTime.Now.Year).Substring(2, 2) + "-" + Convert.ToString(DateTime.Now.Year + 1).Substring(2, 2);  //.Substring(2,2)
            }

            return name + "/" + value + "/" + fincalYear;
        }

        public static string JobNoForInvoice(string name, string value)
        {
            string fincalYear;

            if (DateTime.Now.Month < 4)
            {
                fincalYear = Convert.ToString(DateTime.Now.Year - 1).Substring(2, 2) + "-" + Convert.ToString(DateTime.Now.Year).Substring(2, 2);  //.Substring(2,2)
            }
            else
            {
                fincalYear = Convert.ToString(DateTime.Now.Year).Substring(2, 2) + "-" + Convert.ToString(DateTime.Now.Year + 1).Substring(2, 2);  //.Substring(2,2)
            }

            return name + value + "/" + fincalYear;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "hyddhrii%2moi43Hd5%%";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string DecryptPeoplepointData(string EncryptedText)
        {
            byte[] inputByteArray = new byte[EncryptedText.Length + 1];
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(EncryptedText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string GetFinancialYear(string cdate)
        {
            string finyear = "";
            DateTime dt = Convert.ToDateTime(cdate);
            int m = dt.Month;
            int y = dt.Year;
            if (m > 3)
            {
                finyear = y.ToString().Substring(0, 4) + "-" + Convert.ToString((y + 1)).Substring(2, 2);
                //get last  two digits (eg: 10 from 2010);
            }
            else
            {
                finyear = Convert.ToString((y - 1)).Substring(0, 4) + "-" + y.ToString().Substring(2, 2);
            }
            return finyear;
        }

        public static void ExportDataSetToExcelWithMultipleSheet(this DataSet ds, string file)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    wb.ColumnWidth = 50000.00;
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        if (ds.Tables[i].Rows.Count > 0)
                        {
                            wb.Worksheets.Add(ds.Tables[i], "Sheet" + (i + 1));
                        }
                    }
                   

                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

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