using MediatR;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Delete;

public class DeleteCommandRequest<TEntity>:IRequest<ServiceResult>
    where TEntity : BaseEntity
{
    public Guid Id { get; set; }

    public DeleteCommandRequest(Guid id)
    {
        Id = id;
    }
}
