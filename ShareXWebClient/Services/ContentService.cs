using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Data;
using ShareXWebClient.Models;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;
using ShareXWebClient.Utils;

namespace ShareXWebClient.Services;

public class ContentService : IContentService
{
    private readonly ApplicationContext _db;
    private readonly ILogger<ContentService> _logger;

    public ContentService(ApplicationContext db, ILogger<ContentService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<FileResponse> PostAsync(ContentQuery contentQuery)
    {
        string name = Guid.NewGuid().ToString()[24..];
        string path = Path.Combine(Settings.ContentPath, name + Path.GetExtension(contentQuery.Content.FileName));
        
        var file = new Content
        {
            PublicId = name,
            Path = path
        };
        
        await _db.AddAsync(file);
        
        try
        {
            await using (Stream stream = new FileStream(path, FileMode.CreateNew))
            {
                await contentQuery.Content.CopyToAsync(stream);
            }

            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new FileResponse("Something went wrong. Try again later.");
        }

        return new FileResponse(file);
    }

    public async Task<FileResponse> GetAsync(string query)
    {
        var file = await _db.Contents.FirstOrDefaultAsync(x => x.PublicId == query);
        if (file == null)
            return new FileResponse("Content is not found.");

        return new FileResponse(file);
    }
}