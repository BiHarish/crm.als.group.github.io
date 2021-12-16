using System.Configuration;
using System.Data.OleDb;

namespace Sql
{
    internal sealed class Connection
    {
        public static string Csvconnection { get { return ConfigurationManager.ConnectionStrings["Csvconnection"].ConnectionString; } }
    }
}
