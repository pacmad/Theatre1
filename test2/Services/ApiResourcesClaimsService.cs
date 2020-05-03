using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace test2.Services
{
    public class ApiResourcesClaimsService
    {
        protected readonly ApiAuthorizationOptions options;

        public ApiResourcesClaimsService(ApiAuthorizationOptions options)
        {
            this.options = options;
        }

        public void Build()
        {
            var apiResource = options.ApiResources["test2API"];
            apiResource.UserClaims = new[] { JwtClaimTypes.Role };
            var identityResource = new IdentityResource
            {
                Name = "test2Profile",
                DisplayName = " Test 2 profile",
                UserClaims = new[] { JwtClaimTypes.Role },
            };
            options.IdentityResources.Add(identityResource);
        }
    }
}
