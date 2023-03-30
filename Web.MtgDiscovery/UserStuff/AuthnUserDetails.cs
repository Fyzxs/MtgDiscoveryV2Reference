using System.Security.Claims;

namespace Web.MtgDiscovery.UserStuff;

public interface IAuthnDetails
{
    bool MissingId();
    bool IsNotAuthenticated();
    string CollectorId();
}

public sealed class AuthnUserDetails : IAuthnDetails
{
    private readonly ClaimsPrincipal _user;

    public AuthnUserDetails(ClaimsPrincipal user) => _user = user;

    private const string ObjectClaim = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    public bool MissingId() => _user.HasClaim(c => c.Type == ObjectClaim) is false;
    public bool IsNotAuthenticated() => _user.Identity is { IsAuthenticated: false };
    public string CollectorId() => _user.FindFirstValue(ObjectClaim);
}
