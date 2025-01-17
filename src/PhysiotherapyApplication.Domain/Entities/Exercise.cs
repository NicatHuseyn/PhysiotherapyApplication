using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class Exercise : BaseEntity
{
    public Guid TreatmentId { get; set; }
    public virtual Treatment Treatment { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string Duration { get; set; }
    public int? RepetitionsCount { get; set; }
    public int? SetsCount { get; set; }

    public string? VideoUrl { get; set; }
    public string? ImageUrl { get; set; }
}
