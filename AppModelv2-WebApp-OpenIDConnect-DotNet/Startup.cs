using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(AppModelv2_WebApp_OpenIDConnect_DotNet.Startup))]

namespace AppModelv2_WebApp_OpenIDConnect_DotNet
{
    public class Startup
    {
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        public static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        private static string cookieName = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationType;
        public static readonly string Authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        public static string CookieName
        {
            get { return cookieName; }
        }

        public void Configuration(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                CookieName = ".AspNet." + DefaultAuthenticationTypes.ExternalCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });



            //app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            //CookieAuthenticationOptions cookieOptions = new CookieAuthenticationOptions
            //{
            //    CookieName = CookieName,
            //};
            //app.UseCookieAuthentication(cookieOptions);

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    RedirectUri = redirectUri,
                    Authority = Authority,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                    },
                });
        }    
    }
}
