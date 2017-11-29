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
using static Pagr.Controllers.pagrEncryption;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace Pagr
{
    public partial class Login : System.Web.UI.Page
    {
        // the current password is used as the panel is invisible which means that the text returns as empty
        // need to make it store in the view state

        private string currentPassword
        {
            get
            {
                if (ViewState["currentPassword"] == null)
                {
                    ViewState["currentPassword"] = "";
                }
                return ViewState["currentPassword"] as string;
            }
            set
            {
                ViewState["currentPassword"] = value;
            }
        }

        private string currentEmailAddress
        {
            get
            {
                if (ViewState["currentEmailAddress"] == null)
                {
                    ViewState["currentEmailAddress"] = "";
                }
                return ViewState["currentEmailAddress"] as string;
            }
            set
            {
                ViewState["currentEmailAddress"] = value;
            }
        }

        private string currentUserName
        {
            get
            {
                if (ViewState["currentUserName"] == null)
                {
                    ViewState["currentUserName"] = "";
                }
                return ViewState["currentUserName"] as string;
            }
            set
            {
                ViewState["currentUserName"] = value;
            }
        }

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

        public static string SQLConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        MySqlConnection connection = new MySqlConnection(SQLConnection);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnClick_OpenRegisterDialog(object sender, EventArgs e)
        {
            LogInPanel.Visible = false;
            SignUpPanel.Visible = true;
            LoginRegisterHeader.Text = "Register";
        }

        protected void OnClick_OpenSignInPanel(object sender, EventArgs e)
        {
            LogInPanel.Visible = true;
            SignUpPanel.Visible = false;
            LoginRegisterHeader.Text = "Sign In";
            CompleteSignUpPanel.Visible = false;
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

        protected void OnClick_SignUserIn(object sender, EventArgs e)
        {
            if (checkSignInForm())
            {
                currentPassword = txtPassword.Text;
                checkUserCredentials();
            }
        }

        protected void checkUserCredentials()
        {
            // compare the user name to user name or email
            string select = @"SELECT EmailAddress, Username, Password FROM accounts";
            MySqlCommand cmd = new MySqlCommand(select, connection);
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.HasData())
            {
                foreach (DataRow row in dt.Rows)
                {
                    // problem with the decrypted password
                    string DecryptedPassword = CreatePassWord(row["Username"].ToString(), txtPassword.Text);
                    if ((row["EmailAddress"].ToString() == txtUserOrEmail.Text || row["Username"].ToString() == txtUserOrEmail.Text) && row["Password"].ToString() == DecryptedPassword)
                    {
                        FormsAuthentication.RedirectFromLoginPage(row["Username"].ToString(), cbRememberME.Checked);
                    }
                }
            }
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

        // Checks if the signUpForm is properly validated before it can continue to the to run querys to the database
        protected bool checkSignUpForm()
        {
            bool Passed = true;
            if (txtUserName.Text != "")
            {
                bool validUserName = Regex.IsMatch(txtUserName.Text, @"^\S+$", RegexOptions.IgnoreCase);
                if (!validUserName)
                {

                    Passed = false;
                    userNameError.Visible = true;
                    userNameError.Text = "Your username is not allowed any blank spaces";
                }
                else
                {
                    userNameError.Visible = false;
                    currentUserName = txtUserName.Text;
                    Passed = true;
                }
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

                bool isEmail = Regex.IsMatch(txtEmailAddress.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                {

                    Passed = false;
                    emailAddressError.Visible = true;
                    emailAddressError.Text = "Your email is not valid";
                }
                else
                {
                    currentEmailAddress = txtEmailAddress.Text;
                    emailAddressError.Visible = false;
                    Passed = true;
                }
            }
            else
            {
                Passed = false;
                emailAddressError.Visible = true;
                emailAddressError.Text = "You can not leave your email address blank";
            }

            if (txtPasswordRegestration.Text != "" && Passed)
            {
                bool validPassword = Regex.IsMatch(txtPasswordRegestration.Text, @"^\S+$", RegexOptions.IgnoreCase);
                currentPassword = txtPasswordRegestration.Text;
                if (!validPassword)
                {

                    Passed = false;
                    userRegestrainPasswordError.Visible = true;
                    userRegestrainPasswordError.Text = "Your password is not allowed any blank spaces";
                }
                else
                {
                    userRegestrainPasswordError.Visible = false;
                    Passed = true;
                }
            }
            else
            {
                Passed = false;
                userRegestrainPasswordError.Visible = true;
                userRegestrainPasswordError.Text = "You can not leave your password blank";
            }

            // using regEX to make sure that the details is all one string and no spaces are added in

            return Passed;
        }

        // checks if username or email already exsists in the database
        protected bool checkUserAndEmail()
        {
            bool Passed = true;
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            // note to self the table returned should be a small size since only one isntance of the username will exist
            string select = @"SELECT EmailAddress, Username FROM accounts WHERE EmailAddress = @EmailAddress OR Username = @Username";
            MySqlCommand cmd = new MySqlCommand(select, connection);
            cmd.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
            cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
            DataTable dt = new DataTable();
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            connection.Close();
            // check if email is already within the database
            if (dt.HasData())
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["EmailAddress"].ToString() == txtEmailAddress.Text)
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

        protected void RegisterUserAccount(bool skipIntrest)
        {
            string insert = "";
            if (skipIntrest)
            {
                insert = "INSERT INTO accounts(EmailAddress, Username, Password, Age) VALUES (@EmailAddress, @Username, @Password, @Age)";

            }
            else
            {
                insert = "INSERT INTO accounts(EmailAddress, Username, Password, FollowingPage, Age) VALUES (@EmailAddress, @Username, @Password, @FollowingPage, @Age)";
            }


            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            MySqlCommand cmd = new MySqlCommand(insert, connection);
            cmd.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
            cmd.Parameters.AddWithValue("@Username", txtUserName.Text);
            cmd.Parameters.AddWithValue("@Password", CreatePassWord(txtUserName.Text, currentPassword));
            cmd.Parameters.AddWithValue("@FollowingPage", currentFolloingPagrString);
            // Need to add the option for age, right now will not allow me not to do some stuff cause not null execpetion
            cmd.Parameters.AddWithValue("@Age", 18);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            LogInPanel.Visible = true;
            SignUpPanel.Visible = false;
            LoginRegisterHeader.Text = "Sign In";
            CompleteSignUpPanel.Visible = false;
            txtUserOrEmail.Text = "";
        }

    }
}