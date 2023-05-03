using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;

namespace ShareXWebClient.Interfaces.Service;

public interface ITextService
{
    public Task<TextResponse> PostTextAsync(TextQuery query);

    public Task<TextResponse> GetTextAsync(string query);
}