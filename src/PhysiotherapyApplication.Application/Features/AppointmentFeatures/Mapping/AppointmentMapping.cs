using AutoMapper;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Mapping;

public class AppointmentMapping : Profile
{
    public AppointmentMapping()
    {
        CreateMap<Appointment, CreateAppointmentRequestDto>()
        .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
        .ForMember(dest => dest.AppointmentDateTime, opt => opt.MapFrom(src => src.AppointmentDateTime))
        .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.ConsultationFee, opt => opt.MapFrom(src => src.ConsultationFee))
        .ReverseMap();
    }
}
