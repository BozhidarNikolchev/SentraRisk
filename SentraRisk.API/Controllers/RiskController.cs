using Microsoft.AspNetCore.Mvc;
using SentraRisk.Models;
using SentraRisk.Logic;
using SentraRisk.Services;

namespace SentraRisk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskController : ControllerBase
    {
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] WebsiteInput input)
        {
            var scanner = new WebsiteScanner();

            input.IsReachable =
                await scanner.IsReachableAsync(input.WebsiteUrl);

            if (input.IsReachable)
            {
                input.UsesHttps =
                    await scanner.CheckHttpsAsync(input.WebsiteUrl);

                input.RedirectsToHttps =
                    await scanner.CheckHttpsRedirectAsync(
                        input.WebsiteUrl);

                input.SslInfo =
                    await scanner.GetSslInfoAsync(
                        input.WebsiteUrl);
            }

            var calculator = new RiskCalculator();

            var result = calculator.Calculate(input);

            return Ok(result);
        }
    }
}