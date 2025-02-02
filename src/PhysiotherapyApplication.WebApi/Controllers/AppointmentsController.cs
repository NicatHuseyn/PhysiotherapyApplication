using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.BaseCommands.Create;
using PhysiotherapyApplication.Application.Features.BaseCommands.Delete;
using PhysiotherapyApplication.Application.Features.BaseCommands.Update;
using PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;
using PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.WebApi.Filters;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IMediator mediator) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllQueryRequest<AppointmentDto> request) => CreateActionResult(await mediator.Send(request));

        [ServiceFilter(typeof(NotFoundFilter<Appointment>))]
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetById(GetByIdQueryRequest<AppointmentDto> request) => CreateActionResult(await mediator.Send(request));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommandRequest<CreateAppointmentRequestDto,CreateAppointmentResponseDto> request) => CreateActionResult(await mediator.Send(request));

        [ServiceFilter(typeof(NotFoundFilter<Appointment>))]
        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> Update(UpdateCommandRequest<UpdateAppointmentDto> request) => CreateActionResult(await mediator.Send(request));

        [ServiceFilter(typeof(NotFoundFilter<Appointment>))]
        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete(DeleteCommandRequest<Appointment> request) => CreateActionResult(await mediator.Send(request));
    }
}
