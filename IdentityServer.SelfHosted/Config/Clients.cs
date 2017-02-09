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
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Roles
                    },
                    ClientUri = "https://johanbostrom.se",
                    RequireConsent = false,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:64286/",
                        "http://localhost:64286/episerver",
                        "http://localhost:64286/login"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:64286/"
                    },
                    LogoutSessionRequired = true
                }
            };
        }
    }
}