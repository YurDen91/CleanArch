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

        var idClaim = _httpContextAccessor.HttpContext.User.Claims
            .First(claim => claim.Type == "id");

        var permissions = _httpContextAccessor.HttpContext.User.Claims
            .Where(claim => claim.Type == "permissions")
            .Select(claim => claim.Value)
            .ToList();

        return new CurrentUser(
            Guid.Parse(idClaim.Value),
            permissions,
            null);
    }
}