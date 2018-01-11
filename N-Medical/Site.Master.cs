using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace N_Medical
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                if (ticket.Version == 1)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Sale').innerHTML = '';");
                    sb.Append("var x = document.getElementById('BB').innerHTML = '';");
                    sb.Append("var y = document.getElementById('JNJ').innerHTML = '';");
                    sb.Append("var z = document.getElementById('Logis').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Finance').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else if (ticket.Version == 4)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Admin').innerHTML = '';");
                    sb.Append("var x = document.getElementById('Sale').innerHTML = '';");
                    sb.Append("var y = document.getElementById('JNJ').innerHTML = '';");
                    sb.Append("var z = document.getElementById('Logis').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Finance').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else if (ticket.Version == 5)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Admin').innerHTML = '';");
                    sb.Append("var x = document.getElementById('Sale').innerHTML = '';");
                    sb.Append("var y = document.getElementById('BB').innerHTML = '';");
                    sb.Append("var z = document.getElementById('Logis').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Finance').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else if (ticket.Version == 6)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Admin').innerHTML = '';");
                    sb.Append("var x = document.getElementById('BB').innerHTML = '';");
                    sb.Append("var y = document.getElementById('JNJ').innerHTML = '';");
                    sb.Append("var z = document.getElementById('Logis').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Finance').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else if (ticket.Version == 7)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Admin').innerHTML = '';");
                    sb.Append("var x = document.getElementById('Sale').innerHTML = '';");
                    sb.Append("var y = document.getElementById('BB').innerHTML = '';");
                    sb.Append("var z = document.getElementById('JNJ').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Finance').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
                else if (ticket.Version == 9)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("function hideMenu() {");
                    sb.Append("var w = document.getElementById('Admin').innerHTML = '';");
                    sb.Append("var x = document.getElementById('Sale').innerHTML = '';");
                    sb.Append("var y = document.getElementById('BB').innerHTML = '';");
                    sb.Append("var z = document.getElementById('JNJ').innerHTML = '';");
                    sb.Append("var a = document.getElementById('Logis').innerHTML = '';");
                    sb.Append("} window.onload = hideMenu;");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}