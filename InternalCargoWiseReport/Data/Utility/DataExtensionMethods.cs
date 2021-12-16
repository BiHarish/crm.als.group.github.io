using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ICWR.Data.Utility
{
    public static class DataExtensionMethods
    {
        public static Int32 ToDataConvertInt32(this object var)
        {
            if (var == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(var);
        }


        public static TimeSpan? ToDataNullTimeSpan(this object var)
        {
            TimeSpan? time = var as TimeSpan?;

            if (time.HasValue)
            {
                //time.Value.ToString("hh:mm:tt");
                return time;
            }
            else
            {
                // time.Value.ToString("00:00:01");
                return null;
            }
        }
        public static Int64 ToDataConvertInt64(this object var)
        {
            if (var == DBNull.Value)
                return 0;
            else
                return Convert.ToInt64(var);
        }

        public static bool ToDataConvertBool(this object var)
        {
            if (var == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(var);
        }

        public static DateTime ToDataConvertDateTime(this object var)
        {
            if (var == DBNull.Value)
                return DateTime.Now;
            else
                return Convert.ToDateTime(var);
        }
        
        public static float ToDataConvertFloat(this object var) {
            if (var == DBNull.Value)
                return 0;
            else
                return float.Parse(var.ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }
        
        public static Decimal ToDataConvertDecimal(this object var)
        {
            if (var == DBNull.Value)
                return 0;
            else
                return Convert.ToDecimal(var);
        }

        public static Int32? ToDataConvertNullInt32(this object var)
        {
            if (var == DBNull.Value)
                return (Int32?)null;
            else
                return Convert.ToInt32(var);
        }

        public static Int64? ToDataConvertNullInt64(this object var)
        {
            if (var == DBNull.Value)
                return (Int64?)null;
            else
                return Convert.ToInt64(var);
        }

        public static bool? ToDataConvertNullBool(this object var)
        {
            if (var == DBNull.Value)
                return (bool?)null;
            else
                return Convert.ToBoolean(var);
        }

        public static DateTime? ToDataConvertNullDateTime(this object var)
        {
            if (var == DBNull.Value)
                return (DateTime?)null;
            else
                return Convert.ToDateTime(var);
        }

        public static DateTime? ToDataConvertNullDate(this object var)
        {
            if (var == DBNull.Value)
                return (DateTime?)null;
            else
                return DateTime.ParseExact((string)var, "yyyy/MM/DD", CultureInfo.InvariantCulture);
        }

        public static float? ToDataConvertNullFloat(this object var)
        {
            if (var == DBNull.Value)
                return (float?)null;
            else
                return float.Parse(var.ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }

        public static double? ToDataConvertNullDouble(this object var)
        {
            if (var == DBNull.Value)
                return (double?)null;
            else
                return double.Parse(var.ToString(), CultureInfo.InvariantCulture.NumberFormat);
        }
        public static double? ToDataConvertEmptyDouble(this object var)
        {
                return (double?)null;
           
        }

        public static Decimal? ToDataConvertNullDecimal(this object var)
        {
            if (var == DBNull.Value)
                return (Decimal?)null;
            else
                return Convert.ToDecimal(var);
        }

        public static double ToDataConvertDouble(this object var)
        {
            if (var == DBNull.Value)
                return 0;
            else
                return Convert.ToDouble(var);
        }
        public static string ToDataConvertString(this object var)
        {
            if (var == DBNull.Value)
                return "";
            else
                return Convert.ToString(var);
        }
    }
}