using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace Pagr
{

    public partial class submitpost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void OnClick_SubmitPost(object sender, EventArgs e)
        {
            string title = spTitleBox.Text;
            string body = spDescriptionBox.Text;
            int owner = 1;
            string type = "text";
            bool disabled = false;



            MySqlConnection con = new MySqlConnection("server=localhost;user id =root; database=pagr;password=root");
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO Posts (PostTitle, PostBody, PostOwnerID, PostType, Submitted, Disabled) VALUES (@title,@body, @owner,@type,@datetime,@disabled)";
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@body", body);
            command.Parameters.AddWithValue("@owner", owner);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@datetime", DateTime.Now);
            command.Parameters.AddWithValue("@disabled", disabled);

            con.Open();
            command.ExecuteNonQuery();

            Response.Redirect(Request.RawUrl);
        }
    }
}   