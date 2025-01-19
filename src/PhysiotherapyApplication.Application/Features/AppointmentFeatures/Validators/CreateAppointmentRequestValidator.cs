using FluentValidation;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Validators;

public class CreateAppointmentRequestValidator:AbstractValidator<CreateAppointmentRequestDto>
{
    public CreateAppointmentRequestValidator()
    {
        RuleFor(a=>a.Notes)
            .NotEmpty().WithMessage("Appointment note cannot be empty")
            .Length(3,1000)
            .WithMessage("Appointmnet Note must be between 3 and 1000 characters.");

        RuleFor(a => a.ConsultationFee)
            .GreaterThan(0)
            .WithMessage("ConsultationFee must be greater than 0");

    }
}
