
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShareXWebClient.Models;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;

namespace ShareXWebClient.Interfaces.Service;

public interface ILinkService
{
    public Task<LinkResponse> PostAsync(LinkQuery query);
}