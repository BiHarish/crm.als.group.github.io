using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Net;
namespace Sql
{
    /// <span class="code-SummaryComment"><summary></span>
    /// This file Handles the Exception and logging them into the database.
    /// <span class="code-SummaryComment"></summary></span>
    internal class sqlLogging
    {
        internal static void LogError(Exception oEx)
        {
            try
            {
                bool blLogCheck = true;
                if (blLogCheck)
                {
                    HandleException(oEx);
                }
            }
            catch
            {

            }
        }
        static void HandleException(Exception ex)
        {
            string strLogConnString = Connection.Csvconnection;
            DateTime logDateTime = DateTime.Now;

            string strMessage = string.Empty, strSource = string.Empty, strTargetSite = string.Empty, strStackTrace = string.Empty;
            while (ex != null)
            {
                strMessage = ex.Message;
                strSource = ex.Source;
                strTargetSite = ex.HelpLink;
                strStackTrace = ex.StackTrace;
                ex = ex.InnerException;
            }
            if (strLogConnString.Length > 0)
            {
                SqlCommand strSqlCmd = new SqlCommand();
                strSqlCmd.CommandType = CommandType.StoredProcedure;
                strSqlCmd.CommandText = "uspStmELog";
                SqlConnection sqlConn = new SqlConnection(strLogConnString);
                strSqlCmd.Connection = sqlConn;
                sqlConn.Open();
                try
                {
                    strSqlCmd.Parameters.Add(new SqlParameter("@SE_Source", strSource));
                    strSqlCmd.Parameters.Add(new SqlParameter("@SE_LogDateTime", logDateTime));
                    strSqlCmd.Parameters.Add(new SqlParameter("@SE_Message", strMessage));
                    strSqlCmd.Parameters.Add(new SqlParameter("@SE_TargetSite", strTargetSite));
                    strSqlCmd.Parameters.Add(new SqlParameter("@SE_StackTrace", strStackTrace));
                    SqlParameter outParm = new SqlParameter("@SE_PK", SqlDbType.Int);
                    outParm.Direction = ParameterDirection.Output;
                    strSqlCmd.Parameters.Add(outParm);
                    strSqlCmd.ExecuteNonQuery();
                    strSqlCmd.Dispose();
                    sqlConn.Close();
                }
                catch (Exception exc)
                {
                    EventLog.WriteEntry(exc.Source, "Database Error From Exception Log!", EventLogEntryType.Error, 65535);
                }
                finally
                {
                    strSqlCmd.Dispose();
                    sqlConn.Close();
                }
            }
        }
    }
}