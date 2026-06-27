using Microsoft.AspNetCore.Mvc;
using SentraRisk.Models;
using SentraRisk.Logic;

namespace SentraRisk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskController : ControllerBase
    {
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] WebsiteInput input)
        {
            var calculator = new RiskCalculator();
            var result = calculator.Calculate(input);

            return Ok(result);
        }
    }
}