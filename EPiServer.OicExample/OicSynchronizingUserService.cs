// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: OicSynchronizingUserService.cs
// ---------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace EPiServer.OicExample
{
    [ServiceConfiguration(typeof(ISynchronizingUserService))]
    public class OicSynchronizingUserService : ISynchronizingUserService
    {
        public Task SynchronizeAsync(ClaimsIdentity identity, IEnumerable<string> additionalClaimsToSync)
        {
            // Do claim transforms here
            return Task.FromResult(0);
        }
    }
}