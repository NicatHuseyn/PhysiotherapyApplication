using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.WebApi.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();

            var result = ServiceResult.Fail(errors);

            context.Result = new BadRequestObjectResult(result);

            return;
        }

        await next();
    }
}
