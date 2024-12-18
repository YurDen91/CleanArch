using System.Security.Claims;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using Throw;

namespace GymManagement.Api.Services;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        // could be lived out, but it's not a big deal
        _httpContextAccessor.HttpContext.ThrowIfNull(nameof(_httpContextAccessor.HttpContext));

        var id = GetClaimValues("id")
            .Select(value => Guid.Parse(value))
            .First();

        var permissions = GetClaimValues("permissions");

        var roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUser(Id: id, Permissions: permissions, Roles: roles);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType)
        => _httpContextAccessor.HttpContext.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
}