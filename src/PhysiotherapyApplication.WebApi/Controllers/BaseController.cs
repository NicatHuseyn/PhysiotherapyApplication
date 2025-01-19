using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.Status == System.Net.HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
            }
        }
    }
}
