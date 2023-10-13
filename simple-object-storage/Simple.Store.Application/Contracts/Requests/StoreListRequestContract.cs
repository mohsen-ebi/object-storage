using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Application.Contracts.Requests;

public class StoreListRequestContract : IContract
{
    public List<StoreRequestContract> Files { set; get; }
}