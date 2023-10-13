using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Application.Contracts.Responses;

public class StoreTestResponseContract : IContract
{
    public string Result { get; set; }
}