using Microsoft.AspNetCore.Mvc;
using ShareXWebClient.Data;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Services;

namespace ShareXWebClient.Controllers;

public class TextController : BaseAPIController
{
    private readonly ApplicationContext _db;
    private readonly ITextService _service;

    public TextController(ApplicationContext db, ITextService service)
    {
        _db = db;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TextQuery request)
    {
        if (string.IsNullOrEmpty(request.Value))
            return BadRequest("Not a string or an empty query.");

        var response = await _service.PostTextAsync(request);

        if (!response.Success)
            return BadRequest(response.Message);

        return Ok($"http://localhost:5179/api/Text/{response.Resource.PublicId}");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _service.GetTextAsync(id);
        
        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response.Resource.Value);
    }
}