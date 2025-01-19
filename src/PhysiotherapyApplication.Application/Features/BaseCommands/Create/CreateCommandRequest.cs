using MediatR;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Create
{
    public class CreateCommandRequest<TRequest, TEntity, TId>:IRequest<ServiceResult<CreateCommandResponse<TId>>>
        where TEntity : BaseEntity
    {
        public TRequest Model { get; }

        public CreateCommandRequest(TRequest model)
        {
            Model = model;
        }
    }
}
