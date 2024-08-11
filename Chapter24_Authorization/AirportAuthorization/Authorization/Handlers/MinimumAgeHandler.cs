using AirportAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AirportAuthorization.Authorization.Handlers
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            //this sucks!
            //the claim has to be updated in the cookie for this to work!!!
            int age = int.Parse(context.User.FindFirstValue("Age"));
            if (age < requirement.MinimumAge)
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
