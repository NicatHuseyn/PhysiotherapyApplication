using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result, string? urlAsCreated =null)
        {
            if (result.Status == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            if (result.Status == System.Net.HttpStatusCode.Created)
            {
                return Created(urlAsCreated,result.Data);
            }

            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode()};
        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.Status == System.Net.HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
            }
            return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
        }
    }
}
