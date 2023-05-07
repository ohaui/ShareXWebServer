using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Data;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Models;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;
using ShareXWebClient.Utils;

namespace ShareXWebClient.Services;

public class LinkService : ILinkService
{
    private readonly ApplicationContext _db;
    private readonly ILogger<LinkService> _logger;

    public LinkService(ApplicationContext db, ILogger<LinkService> logger)
    {
        _db = db;
        _logger = logger;
    }

    //it will shorten the link
    public async Task<LinkResponse> PostAsync(LinkQuery query)
    {
        var isLinkContains = await _db.Links.FirstOrDefaultAsync(x => x.ReferenceLink == query.Value);
        if (isLinkContains != default)
        {
            return new LinkResponse(isLinkContains);
        }
        
        string newLink = $"{Settings.BaseURL}/{Guid.NewGuid().ToString()[24..]}";
        
        //there is still a possibility that it won't create a free one, but it's ok
        if (await _db.Links.FirstOrDefaultAsync(x => x.Value == newLink) != null)
        {
            newLink = $"{Settings.BaseURL}/{Guid.NewGuid().ToString()[24..]}"; 
        }

        var resLink = new Link
        {
            Value = newLink,
            ReferenceLink = query.Value
        };
        
        await _db.Links.AddAsync(resLink);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new LinkResponse("Something went wrong. 😒");
        }

        return new LinkResponse(resLink);
    }
}