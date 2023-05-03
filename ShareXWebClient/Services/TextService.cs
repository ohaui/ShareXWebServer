using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Data;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Models;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Models.Response;

namespace ShareXWebClient.Services;

public class TextService : ITextService
{
    private readonly ApplicationContext _db;

    public TextService(ApplicationContext db)
    {
        _db = db;
    }
    public async Task<TextResponse> PostTextAsync(TextQuery query)
    {
        var text = new Text { Value = query.Value, PublicId = Guid.NewGuid().ToString()[24..]};
        await _db.Texts.AddAsync(text);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new TextResponse(text);
    }

    public async Task<TextResponse> GetTextAsync(string query)
    {
        var response = await _db.Texts.FirstOrDefaultAsync(x => x.PublicId == query);

        if (response != null)
            return new TextResponse(response);

        return new TextResponse("No such text.");
    }
}