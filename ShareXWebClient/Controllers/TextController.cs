using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace ShareXWebClient.Controllers;

public class TextController : BaseAPIController
{
    public TextController()
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] string text)
    {
        Console.WriteLine(text);
        return Ok(text);
    }
}