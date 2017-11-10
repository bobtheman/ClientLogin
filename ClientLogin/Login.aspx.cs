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
            PassowrdCheck(txtregpassword.Text.Trim());
            ////var RegClientID = txtregClientID.Text.Trim();
            //var RegUsername = txtregusername.Text.Trim();
            //var RegPassword = txtregpassword.Text.Trim();

            //int AddedUser = SQLHelper.SQLHelper.User_List_Register_NewAccount(RegUsername, RegPassword);

            //if (AddedUser == 1)
            //{
            //    Response.Redirect("~/Home.aspx");
            //    return;
            //}
            //if (AddedUser == 2)
            //{
            //    divusernameunavailable.Visible = true;
            //    return;
            //}

            //lblerrormessage.Text = "An error has occured, please try again";
            //ModalPopupError.Show();

        }
        public int PassowrdCheck(string password)
        {
            return (int)PasswordCheck.PasswordAdvisor.CheckStrength(password);
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

        protected void chkregargee_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}