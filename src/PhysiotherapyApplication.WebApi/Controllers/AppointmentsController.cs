using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;
using PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllQueryRequest<Appointment, AppointmentDto> request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(GetByIdQueryRequest<Appointment, AppointmentDto> request) => Ok(await mediator.Send(request));
    }
}
