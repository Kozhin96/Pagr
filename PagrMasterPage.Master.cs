using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Pagr.Controllers.pagrEncryption;
using static Pagr.pagrExtensionLibrary.pagrExtension;

namespace Pagr
{

    public partial class PagrMasterPage : System.Web.UI.MasterPage
    {

        public static string SQLConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        SqlConnection connection = new SqlConnection(SQLConnection);

        private string currentFolloingPagrString
        {
            get
            {
                if (ViewState["currentFolloingPagrString"] == null)
                {
                    ViewState["currentFolloingPagrString"] = "";
                }
                return ViewState["currentFolloingPagrString"] as string;
            }
            set
            {
                ViewState["currentFolloingPagrString"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool userLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (userLoggedIn)
            {
                // TO DO NEED TO ADD A COL FROM USER IMAGE, NEED TO CHECK IF IMAGE EXISTS AND IF NOT THEN USE THE DEFAULT NOUSERIMAGEICON ELSE USE THEIR EXSTING IMAGE FROM THE DATABASE
                divUserLoggedInContainer.Visible = true;
                lbSignIn.Visible = false;
                LbSignUp.Visible = false;
                lblUserName.Text = System.Web.HttpContext.Current.User.Identity.Name;
                userImage.ImageUrl = "~/Images/UserNoImageIcon.png";
            }
            else
            {
                divUserLoggedInContainer.Visible = false;
                lbSignIn.Visible = true;
                LbSignUp.Visible = true;
            }
        }
        public void OnClick_LotgOut(object sender, EventArgs args)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
        public void OnClick_SignIn(object sender, EventArgs args)
        {
            signUpAndRegisterDialog.Visible = true;
        }
        public void OnClickClose_SignInDialogBox(object sender, EventArgs args)
        {
            signUpAndRegisterDialog.Visible = false;
        }

        // Checks if signInForm is proerly validated before it can continue to run qeury to database
        protected bool checkSignInForm()
        {
            bool passed = true;
            if (txtUserOrEmail.Text != "")
            {
                SignInUserOrEmailError.Visible = false;
                passed = true;
            }
            else
            {
                SignInUserOrEmailError.Visible = true;
                SignInUserOrEmailError.Text = "You can not leave this field blank";
                passed = false;
            }
            if (txtPassword.Text != "" && passed)
            {
                signInPasswordError.Visible = false;
                passed = true;
            }
            else
            {
                signInPasswordError.Visible = true;
                signInPasswordError.Text = "Please enter your password";
                passed = false;
            }
            return passed;
        }


        protected void checkUserCredentials()
        {
            // compare the user name to user name or email
            string select = @"SELECT Email, Username, Password FROM account";
            SqlCommand cmd = new SqlCommand(select, connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.HasData())
            {
                foreach (DataRow row in dt.Rows)
                {
                    // problem with the decrypted password
                    string DecryptedPassword = CreatePassWord(row["Username"].ToString(), txtPassword.Text);
                    if ((row["Email"].ToString() == txtUserOrEmail.Text || row["Username"].ToString() == txtUserOrEmail.Text) && row["Password"].ToString() == DecryptedPassword)
                    {
                        FormsAuthentication.RedirectFromLoginPage(row["Username"].ToString(), cbRememberME.Checked);
                    }
                }
            }
        }

        protected void OnClick_SignUserIn(object sender, EventArgs e)
        {
            if (checkSignInForm())
            {
                checkUserCredentials();
            }
        }

        protected void OnClick_OpenRegisterDialog(object sender, EventArgs e)
        {
            signUpAndRegisterDialog.Visible = true;
            LogInPanel.Visible = false;
            SignUpPanel.Visible = true;
            LoginRegisterHeader.Text = "Register";
        }

        // Checks if the signUpForm is properly validated before it can continue to the to run querys to the database
        protected bool checkSignUpForm()
        {
            bool Passed = true;
            if (txtUserName.Text != "")
            {
                userNameError.Visible = false;
                Passed = true;
            }
            else
            {
                Passed = false;
                userNameError.Visible = true;
                userNameError.Text = "You can not leave your username blank";
            }

            if (txtEmailAddress.Text != "" && Passed)
            {
                emailAddressError.Visible = false;
                Passed = true;

                if (!txtEmailAddress.Text.Contains("@"))
                {
                    Passed = false;
                    emailAddressError.Visible = true;
                    emailAddressError.Text = "Your email is not valid";
                }
                else
                {
                    emailAddressError.Visible = false;
                    Passed = true;
                }
            }
            else
            {
                Passed = false;
                emailAddressError.Visible = true;
                emailAddressError.Text = "You can not leave your username blank";
            }

            if (txtPasswordRegestration.Text != "" && Passed)
            {
                userRegestrainPasswordError.Visible = false;
                Passed = true;
            }
            else
            {
                Passed = false;
                userRegestrainPasswordError.Visible = true;
                userRegestrainPasswordError.Text = "You can not leave your password blank";
            }

            return Passed;
        }

        protected void OnClick_OpenSignUpSecondStage(object sender, EventArgs e)
        {
            bool continueProcess = true;
            if (checkSignUpForm())
            {
                continueProcess = true;
                if (checkUserAndEmail() && continueProcess)
                {
                    continueProcess = true;
                }
                else
                {
                    continueProcess = false;
                }
            }
            else
            {
                continueProcess = false;
            }
            if (continueProcess)
            {
                CompleteSignUpPanel.Visible = true;
                SignUpPanel.Visible = false;
            }
        }

        // checks if username or email already exsists in the database
        protected bool checkUserAndEmail()
        {
            bool Passed = true;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            // note to self the table returned should be a small size since only one isntance of the username will exist
            string select = @"SELECT Email, Username FROM account WHERE Email = @Email OR Username = @Username";
            SqlCommand cmd = new SqlCommand(select, connection);
            cmd.Parameters.AddWithValue("@Email", txtEmailAddress.Text);
            cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            connection.Close();
            // check if email is already within the database
            if (dt.HasData())
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Email"].ToString() == txtEmailAddress.Text)
                    {
                        Passed = false;
                        emailAddressError.Visible = true;
                        emailAddressError.Text = "This email is already registered";
                        break;
                    }
                    else
                    {
                        emailAddressError.Visible = false;
                        Passed = true;
                    }
                }
            }
            if (dt.HasData())
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Username"].ToString() == txtUserName.Text)
                    {
                        Passed = false;
                        userNameError.Visible = true;
                        userNameError.Text = "This username already exists, please choose a different one";
                        break;
                    }
                    else
                    {
                        Passed = true;
                        userNameError.Visible = false;
                    }
                }
            }

            return Passed;
        }

        protected void OnClick_SkipIntrests(object sender, EventArgs e)
        {
            RegisterUserAccount(true);
        }

        protected void RegisterUserAccount(bool skipIntrest)
        {
            string insert = "";
            if (skipIntrest)
            {
                insert = "INSERT INTO account(Email, Username, Password) VALUES (@Email, @Username, @Password)";

            }
            else
            {
                insert = "INSERT INTO account(Email, Username, Password, FollowingPage) VALUES (@Email, @Username, @Password, @FollowingPage)";
            }


            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            SqlCommand cmd = new SqlCommand(insert, connection);
            cmd.Parameters.AddWithValue("@Email", txtEmailAddress.Text);
            cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
            cmd.Parameters.AddWithValue("@Password", CreatePassWord(txtUserName.Text, txtPasswordRegestration.Text));
            cmd.Parameters.AddWithValue("@FollowingPage", currentFolloingPagrString);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Response.Redirect("~/index.aspx");
        }

        protected void OnClick_CompleteRegestration(object sender, EventArgs e)
        {
            // need to write a code to make sure that atleast three items have been checked
            int numberOfSelectedItem = 0;
            List<string> addedIntrests = new List<string>();
            foreach (ListItem item in cblIntrests.Items)
            {
                if (item.Selected)
                {
                    numberOfSelectedItem += 1;
                    addedIntrests.Add(item.Text);
                }
            }

            if (numberOfSelectedItem >= 3)
            {
                foreach (string intrest in addedIntrests)
                {
                    currentFolloingPagrString += "Ω" + intrest;
                }
                RegisterUserAccount(false);
            }
            else
            {
                checkBoxListError.Visible = true;
                checkBoxListError.Text = "please select the minimum of 3 intrests";
            }

            // now I can add the intrests from the list

        }

      
        protected void GIFSTabButton_Click(object sender, EventArgs e)
        {

        }
        protected void VideosTabButton_Click(object sender, EventArgs e)
        {

        }
        protected void ImagesTabButton_Click(object sender, EventArgs e)
        {

        }

        protected void LinksTabButton_Click(object sender, EventArgs e)
        {

        }
        protected void TextTabButton_Click(object sender, EventArgs e)
        {

        }
        protected void WorkTabButton_Click(object sender, EventArgs e)
        {

        }

    }
}
