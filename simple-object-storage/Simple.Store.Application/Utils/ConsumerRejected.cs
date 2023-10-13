namespace Simple.Object.Storage.Application.Utils;

public class ConsumerRejected
{
    public ConsumerStatusCode StatusCode { get; set; }

    public string Reason { set; get; }

    public List<string>? Errors { set; get; }
}

public enum ConsumerStatusCode
{
    Success = 200, 
    Update = 201, 
    BadRequest = 400, 
    UnAuthorized = 401, 
    AccessRestrict = 403,
    NotFound = 404, 
    Conflict = 409, 
}