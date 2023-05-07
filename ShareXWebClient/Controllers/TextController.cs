using Microsoft.AspNetCore.Mvc;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Utils;

namespace ShareXWebClient.Controllers;

public class TextController : BaseAPIController
{
    private readonly ITextService _service;

    public TextController(ITextService service)
    {
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

        return Ok($"{Settings.BaseURL}/api/Text/{response.Resource.PublicId}");
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