using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.WebApi.Filters;

public class NotFoundFilter<T>(IGenericRepository<T> repository) : Attribute, IAsyncActionFilter
    where T : BaseEntity
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Start Action Method

        var idValue = context.ActionArguments.TryGetValue("id", out var id) ? id: null;

        if (idValue is null)
        {
            await next();
            return;
        }

        var guidIdValue = Guid.Parse(idValue.ToString()!);

        var isExistEntityModel = await repository.AnyAsync(x=>x.Id == guidIdValue);

        if (!isExistEntityModel)
        {
            await next();
            return;

        }

        // End Action Method

        var entityName = typeof(T).Name;

        var result = ServiceResult.Fail($"{entityName} Not Found", System.Net.HttpStatusCode.NotFound);

        context.Result = new NotFoundObjectResult(result);

    }
}
