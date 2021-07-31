using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazingTrails.Client.Features.Auth
{
    public class CustomUserFactory<TAccount> : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor) { }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            var claimsIdentity = (ClaimsIdentity)user.Identity;

            if (account != null)
            {
                foreach (var kvp in account.AdditionalProperties)
                {
                    var name = kvp.Key;
                    var value = kvp.Value;
                    if (value != null && value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(kvp.Key));

                        var claims = element.EnumerateArray()
                                            .Select(x => new Claim(kvp.Key, x.ToString()));

                        claimsIdentity.AddClaims(claims);
                    }
                }
            }

            return user;
        }
    }
}
