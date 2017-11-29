using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Data;
namespace Pagr
{

    public partial class post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id =root; database=pagr;password=root");

            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT PostTitle, PostBody, Submitted FROM Posts", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            cmd.Dispose();
            con.Close();
        }

    }
} 