namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;

public class GetAllQueryResponse<TResponseModel>
{
    public TResponseModel Model { get; }

    public GetAllQueryResponse(TResponseModel model)
    {
        Model = model;
    }
}
