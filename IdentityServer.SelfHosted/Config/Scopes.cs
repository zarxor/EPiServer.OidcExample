// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Scopes.cs
// ---------------------------------------------------

using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.SelfHosted.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Address,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,
                StandardScopes.AllClaims,
                new Scope
                {
                    Name = "epi_scope",
                    DisplayName = "EPiServer Scope",
                    Emphasize = true,
                    ShowInDiscoveryDocument = false,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim(Constants.ClaimTypes.Name),
                        new ScopeClaim(Constants.ClaimTypes.Role)
                    }
                }
            };
        }
    }
}