namespace PhysiotherapyApplication.Domain.Entities.Common;

public class BaseEntity:IBaseEntity<Guid>, IEntityTimeStamps
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
}
