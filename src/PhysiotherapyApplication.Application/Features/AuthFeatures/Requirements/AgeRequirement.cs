using Microsoft.AspNetCore.Authorization;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.Requirements;

public class AgeRequirement:IAuthorizationRequirement
{
    public int Age { get; set; }

    public AgeRequirement(int age)
    {
        Age = age;
    }
}


public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
    {
        var userAge = context.User.FindFirst("age");

        if (userAge == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var userAgeByInt = Convert.ToInt32(userAge);

        if (userAgeByInt > requirement.Age)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
