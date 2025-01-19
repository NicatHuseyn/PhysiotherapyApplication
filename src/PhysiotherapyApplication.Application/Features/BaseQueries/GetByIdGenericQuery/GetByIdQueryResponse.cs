namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;

public class GetByIdQueryResponse<TResponseModel>
{
    public TResponseModel Model { get; }

    public GetByIdQueryResponse(TResponseModel model)
    {
        Model = model;
    }
}
