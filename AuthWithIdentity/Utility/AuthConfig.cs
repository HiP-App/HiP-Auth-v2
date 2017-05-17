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
                ClientUri = config.CmsAddress,
                RequireConsent = false,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret(config.UserSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RedirectUris = { $"{config.CmsAddress}/dashboard" },
                PostLogoutRedirectUris = { $"{config.CmsAddress}/login" },
                AllowedCorsOrigins = { config.CmsAddress, config.WebApiAddress },

                AllowedScopes = jsScopes
            };

            var generatorScopes = new List<string>(standardScopes);
            generatorScopes.AddRange(Scopes.All); // Token-generated scopes can access any API
            var tokenGenerationClient = new Client
            {
                ClientId = Scopes.TokenGenerator,
                ClientName = "Token Generator Client",
                ClientUri = config.TokenGeneratorAddress,
                RequireConsent = false,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RedirectUris = { $"{config.TokenGeneratorAddress}/callback.html" },
                PostLogoutRedirectUris = { $"{config.TokenGeneratorAddress}/index.html" },
                AllowedCorsOrigins = { config.WebApiAddress, config.TokenGeneratorAddress },

                AllowedScopes = generatorScopes
            };

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
