using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class Document : BaseEntity
{
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public Guid? TreatmentId { get; set; }
    public virtual Treatment Treatment { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string FileUrl { get; set; }
    public string Description { get; set; }
}
