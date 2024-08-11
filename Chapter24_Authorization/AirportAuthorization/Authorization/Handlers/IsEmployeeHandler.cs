using AirportAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace AirportAuthorization.Authorization.Handlers
{
    public class IsEmployeeHandler : AuthorizationHandler<LoungeAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoungeAccessRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == Claims.EmployeeNumber))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
