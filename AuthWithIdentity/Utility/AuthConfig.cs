using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using PaderbornUniversity.SILab.Hip.Auth.Models;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
    public class AuthConfig
    {
        private const string CmsAngularapp = "HiP-CmsAngularApp";
        private const string TokenGenerator = "HiP-TokenGenerator";
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
                new ApiResource(Scopes.CmsWebApi, Scopes.CmsWebApi),
                new ApiResource(Scopes.DataStore, Scopes.DataStore),
                new ApiResource(Scopes.FeatureToggle, Scopes.FeatureToggle),
                new ApiResource(Scopes.OnlyOffice, Scopes.OnlyOffice)
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

            var jsScopes = new List<string>(standardScopes) {Scopes.CmsWebApi, Scopes.OnlyOffice};
            var jsClientResOwner = new Client // JS client with resource owner password flow
            {
                ClientId = CmsAngularapp,
                ClientName = CmsAngularapp,
                ClientSecrets =
                {
                    new Secret(config.ClientSecrets.Cms.Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = jsScopes,
                AllowedCorsOrigins = { config.CmsAddress }
            };

            var generatorScopes = new List<string>(standardScopes);
            generatorScopes.AddRange(Scopes.All); // Token-generated scopes can access any API
            var tokenGenerationClient = new Client
            {
                ClientId = TokenGenerator,
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

            return new List<Client> { jsClientResOwner, tokenGenerationClient, mobileClient };
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
