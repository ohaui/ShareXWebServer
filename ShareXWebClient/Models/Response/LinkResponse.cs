namespace ShareXWebClient.Models.Response;

public class LinkResponse : BaseResponse<Link>
{
    public LinkResponse(Link resource) : base(resource)
    {
    }

    public LinkResponse(string message) : base(message)
    {
    }
}