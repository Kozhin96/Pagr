using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Pagr.pagrExtensionLibrary
{
    public static class pagrExtension
    {
        public static string SQLConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool HasData(this DataTable tbl)
        {
            if (tbl == null)
            {
                return false;
            }
            else
            {
                return tbl.Rows.Count > 0;
            }
        }

        public static int userID()
        {
            MySqlConnection connection = new MySqlConnection(SQLConnection);
            string getUserID = @"SELECT AccountID FROM accounts WHERE Username = @Username";
            string Username = HttpContext.Current.User.Identity.Name;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            MySqlCommand cmd = new MySqlCommand(getUserID, connection);
            cmd.Parameters.AddWithValue("@Username", Username);
            connection.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            int userID = (int)cmd.ExecuteScalar();
            connection.Close();
            return userID;
        }
    }
}