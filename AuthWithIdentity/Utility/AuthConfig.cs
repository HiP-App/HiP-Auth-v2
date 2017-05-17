using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
    public class AuthConfig
    {
        private AppConfig _appConfig;
        private const string Api = "HiP-CmsWebApi";
        private const string CmsAngularapp = "HiP-CmsAngularApp";
        private const string TokenGenerator = "HiP-TokenGenerator";

        public AuthConfig(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(Api, Api)
            };
        }

        public static IEnumerable<Client> GetClients(AppConfig config)
        {
            var jsClient = new Client
            {
                ClientId = CmsAngularapp,
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

                AllowedScopes = new List<string>
                {
                    "openid", "profile", "email", "roles", "offline_access", Api
                }
            };
            var tokenGenerationClient = new Client
            {
                ClientId = TokenGenerator,
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

                AllowedScopes = new List<string>
                {
                    "openid", "profile", "email", "roles", "offline_access", Api
                }
            };

            return new List<Client> { jsClient, tokenGenerationClient };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
