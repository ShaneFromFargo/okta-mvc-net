using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Okta.AspNet;
using Owin;

[assembly: OwinStartup(typeof(okta_aspnet_mvc_example.Startup))]

namespace okta_aspnet_mvc_example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            //Pirin here is where you  can set the expiration time
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromDays(30),
            });

            app.UseOktaMvc(new OktaMvcOptions()
            {
                OktaDomain = ConfigurationManager.AppSettings["okta:OktaDomain"],
                ClientId = ConfigurationManager.AppSettings["okta:ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["okta:ClientSecret"],
                RedirectUri = ConfigurationManager.AppSettings["okta:RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["okta:PostLogoutRedirectUri"],
                GetClaimsFromUserInfoEndpoint = true,
                Scope = new List<string> { "openid", "profile", "email" },
            });
        }
    }
}
