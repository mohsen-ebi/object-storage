using Simple.Object.Storage.Domain.Utils;

namespace Simple.Object.Storage.Domain.Entities;

public class Store : IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public StoreFileTypeEnum FileType { get; set; }
    public string Tag { get; set; }
    public DateTime CreatedDate { get; set; }
}