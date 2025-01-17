
using Microsoft.EntityFrameworkCore.Storage;
using PhysiotherapyApplication.Persistence.Contexts;

namespace PhysiotherapyApplication.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly PhysiotherapyApplicationDbContext _context;
    private readonly IDbContextTransaction _transaction;

    public UnitOfWork(PhysiotherapyApplicationDbContext context, IDbContextTransaction transaction)
    {
        _context = context;
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task<bool> CommitAsync(bool state = true)
    {
        try
        {
            if (state)
                await _transaction.CommitAsync();

            return true;
        }
        catch 
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
        _transaction?.Dispose();
    }


}
