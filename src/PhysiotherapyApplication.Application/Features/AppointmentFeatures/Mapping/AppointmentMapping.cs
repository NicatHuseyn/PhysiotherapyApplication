using AutoMapper;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Create;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Delete;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Update;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Queries.GetAllQuery;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Mapping;

public class AppointmentMapping : Profile
{
    public AppointmentMapping()
    {
        CreateMap<Appointment,AppointmentDto>().ReverseMap();
        CreateMap<Appointment,CreateAppointmentRequestDto>().ReverseMap();
        CreateMap<Appointment,CreateAppointmentResponseDto>().ReverseMap();
        CreateMap<Appointment,UpdateAppointmentDto>().ReverseMap();

        CreateMap<Appointment,CreateAppointmetCommandRequest>().ReverseMap();
        CreateMap<Appointment,CreateAppointmentCommandResponse>().ReverseMap();

        CreateMap<Appointment,UpdateAppointmentCommandRequest>().ReverseMap();
        CreateMap<Appointment,DeleteAppointmentCommandRequest>().ReverseMap();

        CreateMap<Appointment,GetAllAppointmentQueryRequest>().ReverseMap();
        CreateMap<Appointment, GetAllAppointmentQueryResponse>().ReverseMap();
    }
}
