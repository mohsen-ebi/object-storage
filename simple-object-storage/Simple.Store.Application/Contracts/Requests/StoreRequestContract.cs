using Microsoft.AspNetCore.Http;
using Simple.Object.Storage.Domain.Utils;

namespace Simple.Object.Storage.Application.Contracts.Requests;

public class StoreRequestContract
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
    public StoreFileTypeEnum FileType { get; set; }
    public IEnumerable<string> Tag { get; set; }
}