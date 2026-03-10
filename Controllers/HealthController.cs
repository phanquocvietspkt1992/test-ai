using Microsoft.AspNetCore.Mvc;

namespace test_ai.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    public IActionResult Get() =>
        Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
}
