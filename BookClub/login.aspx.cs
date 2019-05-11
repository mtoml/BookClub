using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookClub.classes;


namespace BookClub
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void LoginButton_Click(object sender, EventArgs e)
        {
            SQLFunctions conn = new SQLFunctions();
            if (conn.authenticateUser(txtUserName.Text, txtPassword.Text) == true)
            {
                //Response.Redirect("index.aspx");
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(30), true, "Extra data");
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                
                ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);
                Response.Redirect("voting.aspx");
            } else
            {
                InvalidCredentialsMessage.Visible = true;
            }
        }
    }
}