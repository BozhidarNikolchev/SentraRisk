using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using SentraRisk.Models;

namespace SentraRisk.Services
{
    public class WebsiteScanner
    {
        public async Task<bool> IsReachableAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                var host = new Uri(website).Host;

                using var client = new TcpClient();

                var connectTask =
                    client.ConnectAsync(host, 443);

                var timeoutTask =
                    Task.Delay(5000);

                var completedTask =
                    await Task.WhenAny(
                        connectTask,
                        timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return false;
                }

                return client.Connected;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CheckHttpsAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                using var client =
                    new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetAsync(website);

                return response.RequestMessage?.RequestUri?.Scheme == "https";
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckHttpsRedirectAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                var host = new Uri(website).Host;

                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                using var client =
                    new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(10);

                var response =
                    await client.GetAsync($"http://{host}");

                var finalUri =
                    response.RequestMessage?.RequestUri;

                if (finalUri == null)
                {
                    return false;
                }

                Console.WriteLine("HTTP Redirect Test");
                Console.WriteLine("Host: " + host);
                Console.WriteLine("Final URI: " + finalUri);
                Console.WriteLine("Final Scheme: " + finalUri.Scheme);

                return finalUri.Scheme.Equals(
                    "https",
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Redirect Check Error: " +
                    ex.Message);

                return false;
            }
        }

        public async Task<SslInfo?> GetSslInfoAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                website = new Uri(website).Host;

                using var client = new TcpClient();

                await client.ConnectAsync(website, 443);

                using var sslStream = new SslStream(
                    client.GetStream(),
                    false,
                    (sender, certificate, chain, errors) => true);

                await sslStream.AuthenticateAsClientAsync(website);

                if (sslStream.RemoteCertificate == null)
                {
                    return null;
                }

                var certificate =
                    new X509Certificate2(
                        sslStream.RemoteCertificate);

                var chain = new X509Chain();

                var chainTrusted =
                    chain.Build(certificate);

                var expirationDate =
                    certificate.NotAfter;

                return new SslInfo
                {
                    // PRIMARY TRUST METRIC
                    IsValid = chainTrusted,

                    // SUPPORTING EVIDENCE
                    ExpirationDate = expirationDate,

                    DaysRemaining =
                        (expirationDate - DateTime.UtcNow).Days,

                    Issuer = certificate.Issuer,

                    IsSelfSigned =
                        certificate.Subject ==
                        certificate.Issuer
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "SSL ERROR: " +
                    ex.Message);

                return null;
            }
        }
        public async Task<HttpScanInfo?> GetHttpScanInfoAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                using var client =
                    new HttpClient(handler);

                client.Timeout = TimeSpan.FromSeconds(10);

                var response =
                    await client.GetAsync(website);

                var finalUri =
                    response.RequestMessage?.RequestUri;

                return new HttpScanInfo
                {
                    OriginalUrl = website,

                    FinalUrl =
                        finalUri?.ToString() ?? "",

                    StatusCode =
                        (int)response.StatusCode,

                    UsesHttps =
                        finalUri?.Scheme == "https",

                    Redirected =
                        website != finalUri?.ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "HTTP SCAN ERROR: " +
                    ex.Message);

                return null;
            }
        }

        private string NormalizeWebsite(string website)
        {
            website = website.Trim();

            if (!website.StartsWith(
                "http",
                StringComparison.OrdinalIgnoreCase))
            {
                website = "https://" + website;
            }

            website = website.ToLowerInvariant();

            return website;
        }

        public async Task<Dictionary<string, string>> GetSecurityHeadersAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                using var client =
                    new HttpClient(handler);

                client.Timeout =
                    TimeSpan.FromSeconds(10);

                var response =
                    await client.GetAsync(website);

                var headers =
                    new Dictionary<string, string>();

                foreach (var header in response.Headers)
                {
                    headers[header.Key] =
                        string.Join(", ", header.Value);
                }

                foreach (var header in response.Content.Headers)
                {
                    headers[header.Key] =
                        string.Join(", ", header.Value);
                }

                return headers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "SECURITY HEADERS ERROR: " +
                    ex.Message);

                return new Dictionary<string, string>();
            }
        }

        public async Task<TechnologyEvidence>
    GetTechnologyEvidenceAsync(string website)
        {
            var evidence =
                new TechnologyEvidence();

            try
            {
                website = NormalizeWebsite(website);

                var handler =
                    new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true;

                using var client =
                    new HttpClient(handler);

                client.Timeout =
                    TimeSpan.FromSeconds(10);

                var response =
                    await client.GetAsync(website);

                foreach (var header in response.Headers)
                {
                    evidence.Headers[header.Key] =
                        string.Join(", ", header.Value);
                }

                foreach (var header in response.Content.Headers)
                {
                    evidence.Headers[header.Key] =
                        string.Join(", ", header.Value);
                }

                evidence.Html =
                    await response.Content.ReadAsStringAsync();

                if (response.Headers.TryGetValues(
                    "Set-Cookie",
                    out var cookies))
                {
                    evidence.Cookies.AddRange(
                        cookies);
                }

                return evidence;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "TECHNOLOGY EVIDENCE ERROR: "
                    + ex.Message);

                return evidence;
            }
        }

        public bool CheckHsts(Dictionary<string, string> headers)
        {
            if (!headers.TryGetValue(
                "Strict-Transport-Security",
                out var hsts))
            {
                return false;
            }

            return hsts.Contains(
                "max-age=",
                StringComparison.OrdinalIgnoreCase);
        }

        public bool CheckXFrameOptions(
    Dictionary<string, string> headers)
        {
            if (!headers.TryGetValue(
                "X-Frame-Options",
                out var value))
            {
                return false;
            }

            value = value.Trim();

            return value.Equals(
                       "DENY",
                       StringComparison.OrdinalIgnoreCase)
                   ||
                   value.Equals(
                       "SAMEORIGIN",
                       StringComparison.OrdinalIgnoreCase);
        }

        public bool CheckContentTypeProtection(
    Dictionary<string, string> headers)
        {
            if (!headers.TryGetValue(
                "X-Content-Type-Options",
                out var value))
            {
                return false;
            }

            return value.Equals(
                "nosniff",
                StringComparison.OrdinalIgnoreCase);
        }

        public bool CheckReferrerPolicy(
    Dictionary<string, string> headers)
        {
            if (!headers.TryGetValue(
                "Referrer-Policy",
                out var value))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(value);
        }

        public bool CheckCsp(
    Dictionary<string, string> headers)
        {
            return headers.ContainsKey(
                "Content-Security-Policy");
        }

        public bool CheckPermissionsPolicy(
    Dictionary<string, string> headers)
        {
            return headers.ContainsKey(
                "Permissions-Policy");
        }

        public bool CheckCoop(
    Dictionary<string, string> headers)
        {
            return headers.ContainsKey(
                "Cross-Origin-Opener-Policy");
        }

        public bool CheckCorp(
    Dictionary<string, string> headers)
        {
            return headers.ContainsKey(
                "Cross-Origin-Resource-Policy");
        }

        public TechnologyDetectionResult DetectTechnologies(
    TechnologyEvidence evidence)
        {
            var result =
                new TechnologyDetectionResult();

            result.CloudflareDetected =
                evidence.Headers.ContainsKey("CF-RAY")
                ||
                (
                    evidence.Headers.TryGetValue(
                        "Server",
                        out var server)
                    &&
                    server.Contains(
                        "cloudflare",
                        StringComparison.OrdinalIgnoreCase)
                );

            result.WordPressDetected =
                evidence.Html.Contains(
                    "wp-content",
                    StringComparison.OrdinalIgnoreCase)
                ||
                evidence.Html.Contains(
                    "wp-includes",
                    StringComparison.OrdinalIgnoreCase);

            result.ShopifyDetected =
                evidence.Headers.TryGetValue(
                    "Set-Cookie",
                    out var cookieHeader)
                &&
                cookieHeader.Contains(
                    "_shopify_",
                    StringComparison.OrdinalIgnoreCase);

            result.NginxDetected =
                evidence.Headers.TryGetValue(
                    "Server",
                    out var nginxServer)
                &&
                nginxServer.Contains(
                    "nginx",
                    StringComparison.OrdinalIgnoreCase);

            result.ApacheDetected =
                evidence.Headers.TryGetValue(
                    "Server",
                    out var apacheServer)
                &&
                apacheServer.Contains(
                    "apache",
                    StringComparison.OrdinalIgnoreCase);

            result.IisDetected =
                evidence.Headers.TryGetValue(
                    "Server",
                    out var iisServer)
                &&
                iisServer.Contains(
                    "iis",
                    StringComparison.OrdinalIgnoreCase);

            result.AspNetDetected =
                evidence.Headers.ContainsKey(
                    "X-AspNet-Version")
                ||
                (
                    evidence.Headers.TryGetValue(
                        "Set-Cookie",
                        out var aspNetCookie)
                    &&
                    aspNetCookie.Contains(
                        "ASP.NET",
                        StringComparison.OrdinalIgnoreCase)
                );

            result.PhpDetected =
                evidence.Headers.TryGetValue(
                    "X-Powered-By",
                    out var poweredBy)
                &&
                poweredBy.Contains(
                    "php",
                    StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}