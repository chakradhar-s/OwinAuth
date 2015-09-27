using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using System.Threading.Tasks;
using Owin;

[assembly: OwinStartup("ProductionConfiguration", typeof(OwinAuthe.App_Start.OwinStart))]
namespace OwinAuthe.App_Start
{
    public class OwinStart
    {
        public void Configuration(IAppBuilder app)
        {           

            ConfigureAuth(app);
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the External Sign In Cookie.
            app.SetDefaultSignInAsAuthenticationType("GoogleCookie");

            // Enable the Application Sign In Cookie.
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "GoogleCookie",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    LoginPath = new PathString("Account"),
            //    CookieSecure = CookieSecureOption.Always,
            //    LogoutPath = new PathString(""),
            //});


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "External",
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
            });
            var options = new GoogleOAuth2AuthenticationOptions()
            {
                AccessType = "offline",
                ClientId = "501120948406-67s4963er8gnrc44m3os462nft7fdch5.apps.googleusercontent.com",
                ClientSecret = "eeDxAHWFAw8hZFRCTFsATFVf",
                CallbackPath = new PathString("/oauth-redirect/google"),
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;

                        // Retrieve the name of the user in Google
                        string googleName = context.Name;

                        // Retrieve the user's email address
                        string googleEmailAddress = context.Email;

                        // You can even retrieve the full JSON-serialized user
                        var serializedUser = context.User;
                        context.Identity.AddClaim(new Claim("urn:token:google", context.AccessToken));

                        
                    }
                }

            };


            //            // Enable Google authentication.
            app.UseGoogleAuthentication(options);

            //            // Setup Authorization Server
            //            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            //            {
            //                AuthorizeEndpointPath = new PathString(@"\Enterprise\Account"),
            //                TokenEndpointPath = new PathString(""),
            //                ApplicationCanDisplayErrors = true,
            //#if DEBUG
            //                AllowInsecureHttp = true,
            //#endif
            //                // Authorization server provider which controls the lifecycle of Authorization Server
            //                Provider = new OAuthAuthorizationServerProvider
            //                {
            //                    OnValidateClientRedirectUri = ValidateClientRedirectUri,
            //                    OnValidateClientAuthentication = ValidateClientAuthentication,
            //                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
            //                    OnGrantClientCredentials = GrantClientCredetails
            //                },

            //                // Authorization code provider which creates and receives the authorization code.
            //                AuthorizationCodeProvider = new AuthenticationTokenProvider
            //                {
            //                    OnCreate = CreateAuthenticationCode,
            //                    OnReceive = ReceiveAuthenticationCode,
            //                },

            //                // Refresh token provider which creates and receives refresh token.
            //                RefreshTokenProvider = new AuthenticationTokenProvider
            //                {
            //                    OnCreate = CreateRefreshToken,
            //                    OnReceive = ReceiveRefreshToken,
            //                }
            //            });
        }
    }
}