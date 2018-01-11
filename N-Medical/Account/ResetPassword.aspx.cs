using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using N_Medical.Models;
using System.Web.Security;
using N_Medical.Services;

namespace N_Medical.Account
{
    public partial class ResetPassword : Page
    {
        LOGIN_Service oLoginService = new LOGIN_Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnReset.UniqueID;
            if (!this.IsPostBack)
            {
            }
        }
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Text.Trim()) && !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(Password.Text.Trim()) != !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
                {
                    FailureText.Text = "Password not match.";
                    ErrorMessage.Visible = true;
                }
                else
                {
                    //updateCustomer
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    oLoginService.changePassword(ticket.Name.ToString(), Cryto.Encrypt(ConfirmPassword.Text.Trim()));

                    ticket = new FormsAuthenticationTicket(ticket.Version, ticket.Name.ToString(), DateTime.Now, DateTime.Now.AddMinutes(28800), true, ticket.UserData.ToString(), FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(ticket.Name.ToString(), true));
                }
            }
            else
            {
                FailureText.Text = "Please enter required field.";
                ErrorMessage.Visible = true;
            }
        }

        protected void ConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Text.Trim()) != !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
            {
                FailureText.Text = "Password not match.";
                ErrorMessage.Visible = true;
            }
        }
    }
}