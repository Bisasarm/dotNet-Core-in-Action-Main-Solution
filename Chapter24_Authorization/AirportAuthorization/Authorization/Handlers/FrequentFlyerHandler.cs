using AirportAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace AirportAuthorization.Authorization.Handlers
{
    public class FrequentFlyerHandler : AuthorizationHandler<LoungeAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoungeAccessRequirement requirement)
        {
            var user = context.User;
            if (user.HasClaim(Claims.FrequentFlyerClass, "Gold"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
