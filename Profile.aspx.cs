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
    public partial class Profile : System.Web.UI.Page
    {
        public static string SQLConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(SQLConnection);

        protected void Page_Load(object sender, EventArgs e)
        {
            bool userLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (userLoggedIn)
            {
                loadProfileDetails();
                //lblUserName.Text = System.Web.HttpContext.Current.User.Identity.Name;
            }
        }

        protected void loadProfileDetails()
        {
            string select = @"SELECT accounts.Username, accounts.Bio, accounts.AccountID, COUNT(FriendID) AS FriendCount, COUNT(PostID) AS PostCount, COUNT(FollowingPages.UserFollowing) AS FollowingCount
                              FROM accounts
                              JOIN friends
                              ON friends.FriendID = accounts.AccountID
                              JOIN Posts
                              ON Posts.PostOwnerID = accounts.AccountID
                              JOIN FollowingPages
                              ON FollowingPages.UserFollowing = accounts.AccountID
                              WHERE
                              accounts.AccountID = @userID
                              GROUP BY
                              accounts.AccountID, accounts.AccountID, accounts.Bio";
            // might need to change the friend id to friend 1 and friend 2
            // I am sure that it works where if they are friends they are added in the database in which it is enabled or disabled

            MySqlCommand cmd = new MySqlCommand(select, connection);
            cmd.Parameters.AddWithValue("@userID", userID());
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.HasData())
            {
                lblUserName.Text = dt.Rows[0]["Username"].ToString();
                lblUserPost.Text = "Post: " + dt.Rows[0]["PostCount"].ToString();
                lblUserFollowing.Text = "Following: " + dt.Rows[0]["FollowingCount"].ToString();
                lblUserFriends.Text = "Friends: " + dt.Rows[0]["FollowingCount"].ToString();
            }
            else
            {
                // have a page where it redirects to page not found or have a secton in which you fill it with the text
            }
        }

        protected void changeProfileImage_OnClick(object sender, EventArgs e)
        {
            signUpAndRegisterDialog.Visible = true;
        }

        protected void OnClickClose_SignInDialogBox(object sender, EventArgs e)
        {
            signUpAndRegisterDialog.Visible = false;
        }
    }
}