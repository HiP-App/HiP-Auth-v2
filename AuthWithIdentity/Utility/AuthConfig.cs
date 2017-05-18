using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Cors.Infrastructure;
using PaderbornUniversity.SILab.Hip.Auth.Models;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
    public class AuthConfig
    {
        private AppConfig _appConfig;

        public AuthConfig(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(Scopes.Api, Scopes.Api)
            };
        }

        public static IEnumerable<Client> GetClients(AppConfig config)
        {
            var standardScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            };

            var jsScopes = new List<string>(standardScopes) {Scopes.Api};
            var jsClient = new Client
            {
                ClientId = Scopes.CmsAngularapp,
                ClientName = "JavaScript Client",
                ClientUri = "http://localhost:3000",
                RequireConsent = false,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret(config.UserSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RedirectUris = { "http://localhost:3000/dashboard" },
                PostLogoutRedirectUris = { "http://localhost:3000/login" },
                AllowedCorsOrigins = { "http://localhost:3000", "http://localhost:5000" },

                AllowedScopes = jsScopes
            };

            var generatorScopes = new List<string>(standardScopes);
            generatorScopes.AddRange(Scopes.All); // Token-generated scopes can access any API
            var tokenGenerationClient = new Client
            {
                ClientId = Scopes.TokenGenerator,
                ClientName = "Token Generator Client",
                ClientUri = "http://localhost:7017",
                RequireConsent = false,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RedirectUris = { "http://localhost:7017/callback.html" },
                PostLogoutRedirectUris = { "http://localhost:7017/index.html" },
                AllowedCorsOrigins = { "http://localhost:5000", "http://localhost:7017" },

                AllowedScopes = generatorScopes
            };
            /*var apiClient = new Client
            {
                ClientId = "HiP-CmsWebApi",
                ClientName = "CMS Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // where to redirect to after login
                RedirectUris = {"http://localhost:5001/signin-oidc"},

                // where to redirect to after logout
                PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc"},

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    _scope
                }
            };*/

            return new List<Client> { jsClient, tokenGenerationClient };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
