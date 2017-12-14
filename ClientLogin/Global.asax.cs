using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ClientLogin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            HttpException lastErrorWrapper = ((HttpException)(Server.GetLastError()));
            Server.ClearError();

            switch (lastErrorWrapper.GetHttpCode())
            {
                case 404:
                    Response.Redirect("~/ErrorPages/404.aspx");
                    break;
                case 500:
                    string lasterrortype = lastErrorWrapper.GetType().ToString();
                    string lasterrormessage = lastErrorWrapper.Message;
                    string lastErrorstacktrace = lastErrorWrapper.StackTrace;
                    Response.Redirect("~/ErrorPages/500.aspx");
                    break;
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