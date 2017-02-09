// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Scopes.cs
// ---------------------------------------------------

using System.Collections.Generic;
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
                StandardScopes.AllClaims
            };
        }
    }
}