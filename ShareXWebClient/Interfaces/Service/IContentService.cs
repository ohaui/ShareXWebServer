using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;

namespace ShareXWebClient.Services;

public interface IContentService
{
    Task<FileResponse> PostAsync(ContentQuery fileQuery);
    Task<FileResponse> GetAsync(string query);
}