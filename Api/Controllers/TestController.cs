using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestController : ApiController
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Hello, World!");
    }
}