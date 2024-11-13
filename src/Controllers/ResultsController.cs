using Microsoft.AspNetCore.Mvc;
using Services.ResultsService;

[ApiController]
public class ResultsController : ControllerBase
{
    private readonly ResultsService _resultsService;
    
    public ResultsController(ResultsService resultsService)
    {
        _resultsService = resultsService;
    }

    [HttpGet]
    [Route("api/results")]
    public IActionResult Health()
    {
        return Ok("Ok!");
    }
}
