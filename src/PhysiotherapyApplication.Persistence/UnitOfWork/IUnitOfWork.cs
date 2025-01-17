namespace PhysiotherapyApplication.Persistence.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<bool> CommitAsync(bool state = true);

    Task<int> SaveChangesAsync();
}
