using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Sql
{
    public sealed class Easy<T, W> where T : class
    {
        static SqlDbType CheckDbType(Type property)
        {
            var sqlDbType = SqlDbType.VarChar;
            if (property == typeof(int) || property == typeof(int?))
            {
                sqlDbType = SqlDbType.Int;
            }
            else if (property == typeof(long) || property == typeof(long?))
            {
                sqlDbType = SqlDbType.BigInt;
            }
            else if (property == typeof(bool) || property == typeof(bool?))
            {
                sqlDbType = SqlDbType.Bit;
            }
            else if (property == typeof(char) || property == typeof(char?))
            {
                sqlDbType = SqlDbType.Char;
            }
            else if (property == typeof(DateTime) || property == typeof(DateTime?))
            {
                sqlDbType = SqlDbType.DateTime;
            }
            else if (property == typeof(double) || property == typeof(double?))
            {
                sqlDbType = SqlDbType.Float;
            }
            else if (property == typeof(DataTable))
            {
                sqlDbType = SqlDbType.Structured;
            }
            return sqlDbType;
        }
        public static SqlParameter[] CommonParam(T obj, int flag)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            int ArrayCount = properties.Count() + 1;
            if (properties.Count() > 0)
            {
                SqlParameter[] Param = new SqlParameter[ArrayCount];
                for (int i = 0; i < ArrayCount; i++)
                {
                    if (i == ArrayCount - 1)
                    {
                        Param[i] = new SqlParameter("@Flag", SqlDbType.Int);
                        Param[i].Value = flag;
                    }
                    else
                    {
                        Param[i] = new SqlParameter("@" + properties[i].Name, CheckDbType(properties[i].PropertyType));
                        Param[i].Value = properties[i].GetValue(obj, null);
                    }
                }
                return Param;
            }
            return new SqlParameter[0];
        }
        public static LongReturn Insert(T obj, string proc, int flag)
        {
            LongReturn result = new LongReturn();

            var Params = CommonParam(obj, flag);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(Connection.Csvconnection, CommandType.StoredProcedure, proc, Params);

                    if (i != null)
                    {
                        result.value = Convert.ToInt64(i);
                        result.ex = null;
                    }
                }
                catch (Exception ex)
                {
                    result.ex = ex;
                    sqlLogging.LogError(ex);
                }
                finally
                {

                }
            }
            return result;
        }
        public static LongReturn Update(T obj, string proc, int flag)
        {
            LongReturn result = new LongReturn();

            var Params = CommonParam(obj, flag);
            if (Params != null)
            {
                try
                {
                    object i = SqlHelper.ExecuteScalar(Connection.Csvconnection, CommandType.StoredProcedure, proc, Params);

                    if (i != null)
                    {
                        result.value = Convert.ToInt64(i);
                        result.ex = null;
                    }
                }
                catch (Exception ex)
                {
                    result.ex = ex;
                    sqlLogging.LogError(ex);
                }
                finally
                {

                }
            }
            return result;
        }
        public static BoolReturn update(T obj, string proc, int flag)
        {
            BoolReturn result = new BoolReturn();

            var Params = CommonParam(obj, flag);
            if (Params != null)
            {
                try
                {
                    int i = SqlHelper.ExecuteNonQuery(Connection.Csvconnection, CommandType.StoredProcedure, proc, Params);

                    if (i >0)
                    {
                        result.value=true;
                        result.ex = null;
                    }
                    else
                    {
                        result.value = false;
                        result.ex = null;
                    }
                }
                catch (Exception ex)
                {
                    result.ex = ex;
                    sqlLogging.LogError(ex);
                }
                finally
                {

                }
            }
            return result;
        }
        public static DataSetReturn GetDataSet(T obj, string proc, int flag)
        {
            DataSetReturn result = new DataSetReturn();
            var Params = CommonParam(obj, flag);
            if (Params != null)
            {
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Connection.Csvconnection, CommandType.StoredProcedure, proc, Params);

                    result.ds = ds;
                    result.ex = null;
                }
                catch (Exception ex)
                {
                    result.ex = ex;
                    sqlLogging.LogError(ex);
                }
                finally
                {

                }
            }
            return result;
        }
        public static DataSetReturn InsertBulk(T obj, string proc, int flag)
        {
            DataSetReturn result = new DataSetReturn();
            var Params = CommonParam(obj, flag);
            if (Params != null)
            {
                try
                {
                    DataSet ds = SqlHelper.ExecuteDataset(Connection.Csvconnection, CommandType.StoredProcedure, proc, Params);

                    if (ds != null)
                    {
                        result.ds = ds;
                        result.ex = null;
                    }
                }
                catch (Exception ex)
                {
                    result.ex = ex;
                    sqlLogging.LogError(ex);
                }
                finally
                {

                }
            }
            return result;
        }
    }
}