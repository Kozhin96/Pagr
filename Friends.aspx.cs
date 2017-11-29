using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Pagr.pagrExtensionLibrary.pagrExtension;

namespace Pagr
{
    public partial class Friends : System.Web.UI.Page
    {
        public static string SQLConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(SQLConnection);

        protected void Page_Load(object sender, EventArgs e)
        {
            loadFriends();
        }

        protected void loadFriends()
        {
            //ccompare the user name to user name or email
            string select = @"SELECT Accounts.username,  Accounts.AccountID
                                FROM Accounts JOIN Friends 
                                ON Accounts.AccountID = Friends.user1 
                                WHERE Friends.disabled = 1 
                                AND Friends.user1 != Accounts.AccountID 
                                UNION SELECT Accounts.username,  Accounts.AccountID 
                                FROM Accounts 
                                JOIN Friends 
                                ON Accounts.AccountID = Friends.user2 
                                WHERE Friends.disabled = 1";
            // user 1 is prime user
            // SELECT Friends.user1, account.username, user2 FROM FRIENDS
            // JOIN Acount on user1
            // WHERE disabled = 1 AND user 1 = current user
            // 
            MySqlCommand cmd = new MySqlCommand(select, connection);
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.HasData())
            {
                repFriendsList.DataSource = dt;
                repFriendsList.DataBind();
                cmd.Dispose();
            }
        }
    }
} 