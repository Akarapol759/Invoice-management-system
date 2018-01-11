using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using N_Medical.Models;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using N_Medical.Services;

namespace N_Medical.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnLogin.UniqueID;
            if (!this.IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("/Account/Login.aspx");
                }
            }
        }
        protected void SetCookie(int version, string userData)// Version is Role, userData is BuCode
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(version, Username.Text, DateTime.Now, DateTime.Now.AddMinutes(28800), true, userData, FormsAuthentication.FormsCookiePath);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);
        }
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int userId = 0;
                int roles = 0;
                string name = null;
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Validate_Login]"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", Username.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", Cryto.Encrypt(Password.Text.Trim()));
                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        userId = Convert.ToInt32(reader["UserId"]);
                        roles = Convert.ToInt32(reader["Roles"]);
                        name = Convert.ToString(reader["Name"]);
                        con.Close();
                    }

                    switch (userId)
                    {
                        case -1:
                            FailureText.Text = "Username and/or password is incorrect.";
                            ErrorMessage.Visible = true;
                            break;
                        case -2:
                            SetCookie(roles, name);
                            Response.Redirect("/Account/ResetPassword");
                            break;
                        default:
                            SetCookie(roles, name);
                            Response.Redirect(FormsAuthentication.GetRedirectUrl(Username.Text, true));
                            break;
                    }
                }
            }
        }
    }
}