using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Update;

public class UpdateCommandRequest<TRequest>:IRequest<ServiceResult>
{
    public Guid Id { get; set; }
    public TRequest Model { get; }

    public UpdateCommandRequest(TRequest model)
    {
        Model = model;
    }
}


public class UpdateCommandHandler<TEntity, TRequest>(IGenericRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommandRequest<TRequest>, ServiceResult>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult> Handle(UpdateCommandRequest<TRequest> request, CancellationToken cancellationToken)
    {
        try
        {
            var entityModel = await repository.GetByIdAsync(request.Id);
            if (entityModel == null)
                return ServiceResult.Fail("Data Not Found", System.Net.HttpStatusCode.NotFound);

            var dtoAsEntity = mapper.Map(request, entityModel);

            repository.Udpated(dtoAsEntity);
            await unitOfWork.CommitAsync();

            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }
}
