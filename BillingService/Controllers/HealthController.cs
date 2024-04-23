using Microsoft.AspNetCore.Mvc;

namespace BillingService.Controllers
{
    [ApiController]
    [Route("billingservice/[controller]")]
    public class HealthController(ILogger<HealthController> logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            logger.LogInformation($"{nameof(BillingService)}->{nameof(GetHealth)}' was called");
            return Ok(new { status = $"{nameof(BillingService)}: OK" });
        }
    }
}
