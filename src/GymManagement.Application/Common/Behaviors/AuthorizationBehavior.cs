using System.Reflection;
using ErrorOr;
using FluentValidation;
using GymManagement.Api.Common.Authorization;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();
        
        if (authorizeAttributes.Count == 0)
        {
            return next();
        }

        var requiredPermissions = authorizeAttributes
            .SelectMany(authorizeAttribute => authorizeAttribute.Permissions?.Split(',') ?? [])
            .ToList();
        
        var currentUser = _currentUserProvider.GetCurrentUser();
        
        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action.");
        }
        
        return next();
    }
}