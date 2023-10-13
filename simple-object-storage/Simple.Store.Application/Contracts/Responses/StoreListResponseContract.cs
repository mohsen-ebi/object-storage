using Simple.Object.Storage.Application.Utils;

namespace Simple.Object.Storage.Application.Contracts.Responses;

public class StoreListResponseContract : IContract

{
    public int TotalFileReceived { get; set; }
}