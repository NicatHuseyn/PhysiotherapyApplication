using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.WebApi.Common;

public class CriticExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorAsDto = ServiceResult.Fail(exception.Message,System.Net.HttpStatusCode.InternalServerError);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken:cancellationToken);

        return true;
    }
}
