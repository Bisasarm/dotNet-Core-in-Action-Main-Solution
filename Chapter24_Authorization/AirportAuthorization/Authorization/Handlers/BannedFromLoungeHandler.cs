using AirportAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace AirportAuthorization.Authorization.Handlers
{
    public class BannedFromLoungeHandler : AuthorizationHandler<LoungeAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoungeAccessRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == Claims.IsBannedFromLounge))
            {
                //this can only fail the requirement, but not satisfy it
                context.Fail();
            }
            //this is necessary to satisfy the requirement, but this will be taken out later on
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
