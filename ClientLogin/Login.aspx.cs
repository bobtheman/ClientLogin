using System;
using System.Web.UI;
using ClientLogin.PassowrdChecker;

namespace ClientLogin
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "validateAndHighlight();");
            divloginerror.Visible = false;
            divusernameunavailable.Visible = false;
            divpasswordstrength.Visible = false;
            if (IsPostBack)
            {
                string Password = txtpassword.Text;
                txtpassword.Attributes.Add("value", Password);
            }

        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            var Username = txtusername.Text.Trim();
            var Password = txtpassword.Text.Trim();

            if (!string.IsNullOrWhiteSpace(SQLHelper.SQLHelper.GetUserDetailsByUserName(Username, Password)))
            {
                Session.Add("VaildUser", SQLHelper.SQLHelper.GetUserDetailsByUserName(Username, Password));
                Response.Redirect("~/Home.aspx");
            }

            divloginerror.Visible = true;
        }

        protected void btnregister_Click(object sender, EventArgs e)
        {
            ModalPopupRegister.Show();
        }

        protected void btnRegisterAccount_Click(object sender, EventArgs e)
        {
            
            int AddedUser = PassowrdCheck(txtregpassword.Text.Trim());

            if (AddedUser < 6)
            {
                PasswordStrength(AddedUser);
            }

            if (AddedUser >= 6)
            {
                CreateAccount();
            }
        }

        public void CreateAccount()
        {
            int AddedUser = SQLHelper.SQLHelper.User_List_Register_NewAccount(txtregusername.Text.Trim(), txtregpassword.Text.Trim());

            if (AddedUser == 1)
            {
                string Username = txtregusername.Text.Trim();
                Session.Add("VaildUser", SQLHelper.SQLHelper.GetUserDetailsByUserName(Username, txtregpassword.Text.Trim()));
                var message = "Your account has been created";
                SendEmailService.SendEmailService.SendEmail(Username, txtregusername.Text.Trim(), message, Username);
                Response.Redirect("~/Home.aspx");
                //return;
            }
            divusernameunavailable.Visible = true;
        }

        public int PassowrdCheck(string password)
        {
            //return (int)PasswordCheck.PasswordAdvisor.CheckStrength(password);

            return PasswordCheck.PasswordValidate.ValidatePassword(password);
        }

        protected void btnCancelRegister_Click(object sender, EventArgs e)
        {
            ModalPopupRegister.Hide();
        }

        protected void btncloseerror_Click(object sender, EventArgs e)
        {
            txtregusername.Text = "";
            txtregpassword.Text = "";
            ModalPopupRegister.Hide();
        }

        protected void PasswordStrength(int strenghtindicator)
        {
            if (strenghtindicator == 1)
            {
                lblpasswordstrength.Text = "Password should contain At least one lower case letter";
                divpasswordstrength.Visible = true;
            }

            if (strenghtindicator == 2)
            {
                lblpasswordstrength.Text = "Password should contain At least one upper case letter";
                divpasswordstrength.Visible = true;
            }

            if (strenghtindicator == 3)
            {
                lblpasswordstrength.Text = "Password should not be less than or greater than 10 characters";
                divpasswordstrength.Visible = true;
            }

            if (strenghtindicator == 4)
            {
                lblpasswordstrength.Text = "Password should contain At least one numeric value";
                divpasswordstrength.Visible = true;
            }

            if (strenghtindicator == 5)
            {
                lblpasswordstrength.Text = "Password should contain At least one special case characters";
                divpasswordstrength.Visible = true;
            }
        }

        protected void chkargee_CheckedChanged(object sender, EventArgs e)
        {
            if (chkargee.Checked)
            {
                btnlogin.Enabled = true;
                return;
            }
            btnlogin.Enabled = false;
        }
    }
}