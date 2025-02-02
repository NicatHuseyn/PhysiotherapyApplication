using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Delete;

public class DeleteCommandRequest<TEntity>:IRequest<ServiceResult>
    where TEntity :BaseEntity
{
    public Guid Id { get; set; }

    public DeleteCommandRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteCommandHandler<TEntity> : IRequestHandler<DeleteCommandRequest<TEntity>, ServiceResult> where TEntity : BaseEntity
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommandHandler(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult> Handle(DeleteCommandRequest<TEntity> request, CancellationToken cancellationToken)
    {
        try
        {
            var model = await _repository.GetByIdAsync(request.Id);

            await _repository.DeleteAsync(model);
            await _unitOfWork.CommitAsync();

            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message, System.Net.HttpStatusCode.InternalServerError);
        }
    }
}