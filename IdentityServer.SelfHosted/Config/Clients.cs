// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Clients.cs
// ---------------------------------------------------

using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.SelfHosted.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "EPiServer Hybrid Client",
                    ClientId = "episerver.hybrid",
                    Flow = Flows.Hybrid,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("episerver".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "epi_scope"
                    },
                    ClientUri = "https://johanbostrom.se",
                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Reference,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:44300/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44300/"
                    },
                    LogoutUri = "https://localhost:44300/Home/OidcSignOut",
                    LogoutSessionRequired = true
                }
            };
        }
    }
}