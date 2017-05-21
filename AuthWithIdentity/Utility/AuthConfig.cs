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
        private const string MobileClient = "HiP-Mobile";

        public AuthConfig(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(Scopes.CmsWebApi, Scopes.CmsWebApi)
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

            var jsScopes = new List<string>(standardScopes) {Scopes.CmsWebApi};
            var jsClient = new Client
            {
                ClientId = Scopes.CmsAngularapp,
                ClientName = "JavaScript Client",
                ClientUri = config.CmsAddress,
                RequireConsent = false,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret(config.ClientSecrets.Cms.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = false,

                RedirectUris = { $"{config.CmsAddress}/dashboard" },
                PostLogoutRedirectUris = { $"{config.CmsAddress}/login" },
                AllowedCorsOrigins = { config.CmsAddress, config.WebApiAddress },

                AllowedScopes = jsScopes
            };
            var jsClientRO = new Client // alternative: JS client with resource owner password flow
            {
                ClientId = $"{Scopes.CmsAngularapp}RO",
                ClientName = $"{Scopes.CmsAngularapp}RO",
                ClientSecrets =
                {
                    new Secret(config.ClientSecrets.Cms.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
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
                    new Secret(config.ClientSecrets.Generator.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = false,

                RedirectUris = { $"{config.TokenGeneratorAddress}/callback.html" },
                PostLogoutRedirectUris = { $"{config.TokenGeneratorAddress}/index.html" },
                AllowedCorsOrigins = { config.WebApiAddress, config.TokenGeneratorAddress },

                AllowedScopes = generatorScopes
            };

            var mobileScopes = new List<string>(standardScopes) {Scopes.FeatureToggle, Scopes.DataStore};
            var mobileClient = new Client
            {
                ClientId = MobileClient,
                ClientName = MobileClient,
                ClientSecrets =
                {
                    new Secret(config.ClientSecrets.Mobile.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = mobileScopes
            };

            return new List<Client> { jsClient, jsClientRO, tokenGenerationClient, mobileClient };
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
