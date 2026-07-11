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
            input.WebsiteUrl = input.WebsiteUrl.Trim();

            var scanner = new WebsiteScanner();

            Console.WriteLine("CONTROLLER HIT");

            var httpInfo =
                await scanner.GetHttpScanInfoAsync(
                    input.WebsiteUrl);

            Console.WriteLine("HTTP INFO FINISHED");

            var technologyEvidence =
    await scanner.GetTechnologyEvidenceAsync(
        input.WebsiteUrl);

            var technologies =
        scanner.DetectTechnologies(
            technologyEvidence);

            Console.WriteLine();
            Console.WriteLine(
                "===== TECHNOLOGY EVIDENCE =====");

            Console.WriteLine(
"CLOUDFLARE DETECTED: " +
technologies.CloudflareDetected);

            Console.WriteLine(
                "WORDPRESS DETECTED: " +
                technologies.WordPressDetected);

            Console.WriteLine(
                "SHOPIFY DETECTED: " +
                technologies.ShopifyDetected);

            Console.WriteLine(
"HEADER COUNT: " +
technologyEvidence.Headers.Count);

            Console.WriteLine(
                "HTML LENGTH: " +
                technologyEvidence.Html.Length);

            Console.WriteLine(
                "COOKIE COUNT: " +
                technologyEvidence.Cookies.Count);

            foreach (var header in
             technologyEvidence.Headers)
            {
                Console.WriteLine(
                    $"{header.Key}: {header.Value}");
            }

            if (technologyEvidence.Html.Contains(
    "wp-content",
    StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "FOUND: wp-content");
            }

            if (technologyEvidence.Html.Contains(
                "wp-includes",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "FOUND: wp-includes");
            }

            if (technologyEvidence.Html.Contains(
                "shopify",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "FOUND: shopify");
            }

            var securityHeaders =
    await scanner.GetSecurityHeadersAsync(
        input.WebsiteUrl);

            var hstsEnabled =
        scanner.CheckHsts(
            securityHeaders);

            Console.WriteLine(
                "HSTS ENABLED: " +
                hstsEnabled);

            var xFrameProtected =
scanner.CheckXFrameOptions(
    securityHeaders);

            Console.WriteLine(
                "X-FRAME PROTECTED: " +
                xFrameProtected);

            var contentTypeProtected =
scanner.CheckContentTypeProtection(
    securityHeaders);

            Console.WriteLine(
                "CONTENT-TYPE PROTECTED: " +
                contentTypeProtected);

            var referrerProtected =
scanner.CheckReferrerPolicy(
    securityHeaders);

            Console.WriteLine(
                "REFERRER POLICY ENABLED: " +
                referrerProtected);

            var cspEnabled =
scanner.CheckCsp(
    securityHeaders);

            Console.WriteLine(
                "CSP ENABLED: " +
                cspEnabled);

            var permissionsEnabled =
scanner.CheckPermissionsPolicy(
    securityHeaders);

            Console.WriteLine(
                "PERMISSIONS POLICY ENABLED: " +
                permissionsEnabled);

            var coopEnabled =
scanner.CheckCoop(
    securityHeaders);

            Console.WriteLine(
                "COOP ENABLED: " +
                coopEnabled);

            var corpEnabled =
scanner.CheckCorp(
    securityHeaders);

            Console.WriteLine(
                "CORP ENABLED: " +
                corpEnabled);


            Console.WriteLine();
            Console.WriteLine(
                "===== SECURITY HEADERS =====");

            foreach (var header in securityHeaders)
            {
                Console.WriteLine(
                    $"{header.Key}: {header.Value}");
            }

            if (httpInfo != null)
            {
                Console.WriteLine("===== HTTP SCAN =====");

                Console.WriteLine(
                    "Original URL: " +
                    httpInfo.OriginalUrl);

                Console.WriteLine(
                    "Final URL: " +
                    httpInfo.FinalUrl);

                Console.WriteLine(
                    "Status Code: " +
                    httpInfo.StatusCode);

                Console.WriteLine(
                    "Uses HTTPS: " +
                    httpInfo.UsesHttps);

                Console.WriteLine(
                    "Redirected: " +
                    httpInfo.Redirected);
            }
            else
            {
                Console.WriteLine("HTTP INFO IS NULL");
            }

            input.IsReachable =
                await scanner.IsReachableAsync(input.WebsiteUrl);

            Console.WriteLine(
"IS REACHABLE: " +
input.IsReachable);

            if (input.IsReachable)
            {
                input.UsesHttps =
                    await scanner.CheckHttpsAsync(input.WebsiteUrl);

                input.RedirectsToHttps =
                    await scanner.CheckHttpsRedirectAsync(
                        input.WebsiteUrl);

                Console.WriteLine(
"RedirectsToHttps: " +
input.RedirectsToHttps);

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