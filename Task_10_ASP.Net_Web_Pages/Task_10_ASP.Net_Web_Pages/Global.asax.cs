using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Task_10_ASP.Net_Web_Pages.Models;

namespace Task_10_ASP.Net_Web_Pages
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ProgramModel.InitialLogic();
            AccountModel.InitialLogic();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request["SignOut"] != null)
            {
                AccountModel.SignOut();
            }

            if (Request.HttpMethod == "POST")
            {
                ProgramModel.SelectAction(Context);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            ProgramModel.Save();
        }
    }
}