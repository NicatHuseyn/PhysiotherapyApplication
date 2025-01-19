namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Queries.GetAllQuery;

public class GetAllAppointmentQueryResponse
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public DateTime? AppointmentDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public string CancellationReason { get; set; }
    public decimal? ConsultationFee { get; set; }
    public bool IsPaid { get; set; }
    public Guid TreatmentId { get; set; }
    public string TreatmentName { get; set; }
}

