using MediatR;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Update;

public class UpdateCommandRequest<TEntity,TModel>:IRequest<ServiceResult>
    where TEntity : BaseEntity
{
    public Guid Id { get; set; }
    public TModel Model { get; }

    public UpdateCommandRequest(TModel model)
    {
        Model = model;
    }
}
