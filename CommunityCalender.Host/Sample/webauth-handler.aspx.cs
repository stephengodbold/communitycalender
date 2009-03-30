using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WindowsLive;
using System.Web.Security;

namespace CommunityCalender.Host.Sample
{
    public partial class webauth_handler : System.Web.UI.Page
    {
        // Initialize the WindowsLiveLogin module.
        static WindowsLiveLogin wll = new WindowsLiveLogin(true);

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest req = HttpContext.Current.Request;
            HttpResponse res = HttpContext.Current.Response;

            // Extract the 'action' parameter from the request, if any.
            string action = req["action"];

            /*
              If action is 'logout', clear the login cookie and redirect
              to the logout page.

              If action is 'clearcookie', clear the login cookie and
              return a GIF as response to signify success.

              By default, try to process a login. If login was
              successful, cache the user token in a cookie and redirect
              to the site's main page.  If login failed, clear the cookie
              and redirect to the main page.
            */

            if (action == "logout")
            {
                HttpCookie loginCookie = new HttpCookie(WindowsLiveLogin.LoginCookie);
                loginCookie.Expires = WindowsLiveLogin.ExpireCookie;
                res.Cookies.Add(loginCookie);
                res.Redirect(WindowsLiveLogin.LogoutPage);
                res.End();
            }
            else if (action == "clearcookie")
            {
                HttpCookie loginCookie = new HttpCookie(WindowsLiveLogin.LoginCookie);
                loginCookie.Expires = WindowsLiveLogin.ExpireCookie;
                res.Cookies.Add(loginCookie);

                string type;
                byte[] content;
                wll.GetClearCookieResponse(out type, out content);
                res.ContentType = type;
                res.OutputStream.Write(content, 0, content.Length);

                res.End();
            }
            else
            {
                WindowsLiveLogin.User user = wll.ProcessLogin(req.Form);

                HttpCookie loginCookie = new HttpCookie(WindowsLiveLogin.LoginCookie);
                if (user != null)
                {
                    loginCookie.Value = user.Token;

                    if (user.UsePersistentCookie)
                    {
                        loginCookie.Expires = WindowsLiveLogin.PersistCookie;
                    }

                    System.Web.Security.Membership.ValidateUser(user.Id, "Windows Live ID");
                    CreateTicket(user.Id, user.UsePersistentCookie, "", WindowsLiveLogin.PersistCookie);
                }
                else
                {
                    loginCookie.Expires = WindowsLiveLogin.ExpireCookie;
                }

                res.Cookies.Add(loginCookie);
                res.Redirect(WindowsLiveLogin.LoginPage);
                res.End();
            }
        }


        private static bool CreateTicket(string userName, bool stayLoggedIn, string type, DateTime cookieTime)
        {
            FormsAuthentication.Initialize();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, cookieTime, stayLoggedIn, type, FormsAuthentication.FormsCookiePath);

            string hash = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }
    }
}
