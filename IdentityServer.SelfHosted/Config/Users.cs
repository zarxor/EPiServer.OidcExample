// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Users.cs
// ---------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServer.SelfHosted.Config
{
    internal static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var users = new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "dae962db-f092-4df0-8c6d-34c16ee78c98",
                    Username = "admin",
                    Password = "admin",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Leia Organa"),
                        new Claim(Constants.ClaimTypes.GivenName, "Leia"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Organa"),
                        new Claim(Constants.ClaimTypes.Email, "leia.organa@johanbostrom.se"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Administrators")
                    }
                },
                new InMemoryUser
                {
                    Subject = "903306c0-45ad-4ed5-904f-8f6c8c95fcf1",
                    Username = "user",
                    Password = "user",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Carrie Fisher"),
                        new Claim(Constants.ClaimTypes.GivenName, "Carrie"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Fisher"),
                        new Claim(Constants.ClaimTypes.Email, "carrie.fisher@johanbostrom.se"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "WebEditors")
                    }
                }
            };

            return users;
        }
    }
}