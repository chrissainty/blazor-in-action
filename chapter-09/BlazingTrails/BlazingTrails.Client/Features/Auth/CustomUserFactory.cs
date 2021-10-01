using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace BlazingTrails.Client.Features.Auth;

public class CustomUserFactory<TAccount> : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor) { }

    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);

        if (initialUser?.Identity?.IsAuthenticated ?? false)
        {
            var userIdentity = (ClaimsIdentity)initialUser.Identity;

            account.AdditionalProperties.TryGetValue(ClaimTypes.Role, out var roleClaimValue);

            if (roleClaimValue is not null && roleClaimValue is JsonElement element && element.ValueKind == JsonValueKind.Array)
            {
                userIdentity.RemoveClaim(userIdentity.FindFirst(ClaimTypes.Role));

                var claims = element.EnumerateArray()
                                    .Select(x => new Claim(ClaimTypes.Role, x.ToString()));

                userIdentity.AddClaims(claims);
            }
        }

        return initialUser ?? new ClaimsPrincipal();
    }
}
