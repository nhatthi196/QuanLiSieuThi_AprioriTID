using System.Data.SqlClient;
using System.Windows;
using AprioriTID.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
namespace AprioriTID.DAO
{
    public static class Connection
    {
        public static string serverName = @"data source=DESKTOP-DBFS4MJ\SERVER;";
        public static string databaseName = "QLST";
        public static SqlConnection conn;
       

        public static ConnectionMessage ConnectToDatabase(string serverName, string databaseName, string username, string password)
        {
            ConnectionMessage error = new ConnectionMessage();
          
            string connectionString = serverName + "initial catalog=" + databaseName + ";User Id=" + username + ";Password=" + password + ";";
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                error.State = true; 
            }
            catch (SqlException err)
            {
                if (err.Class == 20)
                {
                    error.State = false;
                    error.Message = Constant.sqlUnvailible;

                }
                else
                {
                    error.State = false;
                    error.Message = err.Message;
                }
                conn.Close();
            }
            return error;
        }
        public static void CloseConnection()
        {
            conn.Close();
        }
    }


}

