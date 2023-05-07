using Microsoft.AspNetCore.Mvc;
using ShareXWebClient.Data;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Models.Query;

namespace ShareXWebClient.Controllers;

public class LinkController : BaseAPIController
{
    private readonly ILinkService _service;

    public LinkController( ILinkService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(LinkQuery query)
    {
        if (string.IsNullOrEmpty(query.Value))
            return BadRequest("Not a string or an empty query.");

        //it may be stupid because some domains can contain "http"
        if (!query.Value.Contains("http")) 
            query.Value = "//" + query.Value;

        var response = await _service.PostAsync(query);

        if (!response.Success)
            return BadRequest(response.Message);

        return Ok(response.Resource.Value);
    } 
}