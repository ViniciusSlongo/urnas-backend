using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/health")]
public class ApuracaoController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Health()
    {
        return Ok("Ok!");
    }
}
