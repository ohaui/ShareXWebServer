using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using MimeMapping;
using ShareXWebClient.Models.Query;
using ShareXWebClient.Services;
using ShareXWebClient.Utils;

namespace ShareXWebClient.Controllers;

public class ContentController : BaseAPIController
{
    private readonly IContentService _service;
    public ContentController(IContentService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromForm]ContentQuery contentQuery)
    {
        var response = await _service.PostAsync(contentQuery);

        if (!response.Success)
            return BadRequest("Something went wrong");

        return Ok($"{Settings.BaseURL}/api/Content/{response.Resource.PublicId}");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _service.GetAsync(id);

        var filePath = response.Resource.Path;

        var contentType = MimeUtility.GetMimeMapping(filePath);

        if (!response.Success)
            return NotFound(response.Message);

        var returnFile = System.IO.File.OpenRead(filePath);

        return File(returnFile, contentType, Path.GetFileName(filePath));
    }
}