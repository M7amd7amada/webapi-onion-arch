using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestController : ApiController
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Hello, World!");
    }
}