namespace ShareXWebClient.Models.Response;

public class TextResponse : BaseResponse<Text>
{
    public TextResponse(Text resource) : base(resource) { }

    public TextResponse(string message) : base(message) { }
}