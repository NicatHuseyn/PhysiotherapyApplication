namespace PhysiotherapyApplication.Domain.Entities.Common;

public interface IBaseEntity<TId>
{
    public TId Id { get; set; }
}
