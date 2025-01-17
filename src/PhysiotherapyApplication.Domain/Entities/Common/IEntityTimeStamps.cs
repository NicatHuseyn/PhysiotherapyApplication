namespace PhysiotherapyApplication.Domain.Entities.Common;

public interface IEntityTimeStamps
{
    DateTime CreateDate { get; set; }
    DateTime? UpdateDate { get; set; }
    DateTime? DeleteDate { get; set; }
}
