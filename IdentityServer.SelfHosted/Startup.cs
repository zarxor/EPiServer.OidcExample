// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Startup.cs
// ---------------------------------------------------

using IdentityServer.SelfHosted.Config;
using IdentityServer3.Core.Configuration;
using Owin;

namespace IdentityServer.SelfHosted
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = new IdentityServerServiceFactory();
            factory
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get())
                .UseInMemoryUsers(Users.Get());

            var options = new IdentityServerOptions
            {
                SiteName = "EPiServer friendly IdentityServer",
                SigningCertificate = Certificate.Get(),
                Factory = factory
            };

            appBuilder.UseIdentityServer(options);
        }
    }
}