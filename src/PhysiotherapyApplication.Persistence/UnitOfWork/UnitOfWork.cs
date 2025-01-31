
using Microsoft.EntityFrameworkCore.Storage;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Persistence.Contexts;

namespace PhysiotherapyApplication.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly PhysiotherapyApplicationDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(PhysiotherapyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<bool> CommitAsync(bool state = true)
    {
        try
        {
            await _context.SaveChangesAsync();
            if (state && _transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            return true;
        }
        catch
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
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

