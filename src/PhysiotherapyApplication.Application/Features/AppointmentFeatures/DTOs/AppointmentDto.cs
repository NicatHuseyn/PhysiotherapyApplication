namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs
{
    public record AppointmentDto
        (
        Guid Id,
        DateTime CreateDate, 
        DateTime? UpdateDate, 
        DateTime? DeleteDate, 
        string PatientId, 
        string PatientName, 
        DateTime? AppointmentDateTime, 
        TimeSpan Duration, 
        string Status, 
        string Notes, 
        string CancellationReason, 
        decimal? ConsultationFee, 
        bool isPaid,
        string TreatmentName);
}
