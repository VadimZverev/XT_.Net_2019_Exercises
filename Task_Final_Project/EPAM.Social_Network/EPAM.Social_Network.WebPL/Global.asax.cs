using EPAM.Social_Network.Loggers;
using EPAM.Social_Network.WebPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace EPAM.Social_Network.WebPL
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST"
                && (Request.UrlReferrer.AbsolutePath == "/Pages/Search"
                    || Request.UrlReferrer.AbsolutePath == "/Pages/Accounts/UserProfile")
                && Request["Mode"] != null
                && Request["Mode"].Contains("Delete"))
            {
                AccountModel.RemoveAccount();
            }
            else if (Request.HttpMethod == "POST"
                     && Request.UrlReferrer.AbsolutePath == "/Pages/Accounts/UserProfile"
                     && Request["Mode"] != null
                     && Request["Mode"].Contains("DelFriend"))
            {
                AccountModel.RemoveFromFriends();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                Logger.InitLogger();
                Logger.SendFatalError(exc, string.Empty);

                Server.ClearError();

                Response.Redirect($"/Pages/FatalError?errorMsg={exc.Message}", true);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}