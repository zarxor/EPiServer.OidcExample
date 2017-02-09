// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Startup.cs
// ---------------------------------------------------

using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using EPiServer.OicExample;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EPiServer.OicExample
{
    public class Startup
    {
        private const string UrlLogout = "/util/logout.aspx";
        private const string UrlLogin = "/login";

        private const string OicClientId = "episerver.hybrid";
        private const string OicAuthority = "https://localhost:44333/core";
        private const string OicScopes = "openid roles profile email";

        private const string OicResponseType = "code id_token token";
        // Used for hybrid flow, for just imlicit flow just use id_token

        private const string OicPostLogoutRedirectUri = "http://localhost:64286/";

        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = OicClientId,
                Authority = OicAuthority,
                PostLogoutRedirectUri = OicPostLogoutRedirectUri,
                ResponseType = OicResponseType,
                Scope = OicScopes,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                },
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Write(context.Exception.Message);
                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = context =>
                    {
                        if (context.ProtocolMessage.RedirectUri == null)
                        {
                            var currentUrl = SiteDefinition.Current.SiteUrl;
                            context.ProtocolMessage.RedirectUri = new UriBuilder(
                                currentUrl.Scheme,
                                currentUrl.Host,
                                currentUrl.Port,
                                HttpContext.Current.Request.Url.AbsolutePath).ToString();
                        }

                        if (context.OwinContext.Response.StatusCode == 401 &&
                            context.OwinContext.Authentication.User.Identity.IsAuthenticated)
                        {
                            context.OwinContext.Response.StatusCode = 403;
                            context.HandleResponse();
                        }
                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = context =>
                    {
                        var redirectUri = new Uri(context.AuthenticationTicket.Properties.RedirectUri,
                            UriKind.RelativeOrAbsolute);
                        if (redirectUri.IsAbsoluteUri)
                            context.AuthenticationTicket.Properties.RedirectUri = redirectUri.PathAndQuery;

                        ServiceLocator.Current.GetInstance<ISynchronizingUserService>()
                            .SynchronizeAsync(context.AuthenticationTicket.Identity);

                        return Task.FromResult(0);
                    }
                }
            });

            app.UseStageMarker(PipelineStage.Authenticate);

            app.Map(UrlLogin, config =>
            {
                config.Run(context =>
                {
                    if (context.Authentication.User == null || !context.Authentication.User.Identity.IsAuthenticated)
                        context.Response.StatusCode = 401;
                    else
                        context.Response.Redirect("/");
                    return Task.FromResult(0);
                });
            });

            app.Map(UrlLogout, config =>
            {
                config.Run(context =>
                {
                    context.Authentication.SignOut();
                    return Task.FromResult(0);
                });
            });

            // If the application throws an antiforgery token exception like “AntiForgeryToken: A Claim of Type NameIdentifier or IdentityProvider Was Not Present on Provided ClaimsIdentity,” 
            // use this:
            // AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}