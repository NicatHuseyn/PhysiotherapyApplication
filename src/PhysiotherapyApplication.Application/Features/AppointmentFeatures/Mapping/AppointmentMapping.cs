using AutoMapper;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
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
    }
}
