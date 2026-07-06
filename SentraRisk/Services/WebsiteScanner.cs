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

                using var client = new HttpClient();

                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetAsync(website);

                return true;
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

                using var client = new HttpClient();

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

                using var client = new HttpClient();

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
                    new X509Certificate2(sslStream.RemoteCertificate);

                var expirationDate = certificate.NotAfter;

                return new SslInfo
                {
                    IsValid = expirationDate > DateTime.UtcNow,

                    ExpirationDate = expirationDate,

                    DaysRemaining =
         (expirationDate - DateTime.UtcNow).Days,

                    Issuer = certificate.Issuer,

                    IsSelfSigned =
         certificate.Subject == certificate.Issuer
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<HttpScanInfo?> GetHttpScanInfoAsync(string website)
        {
            try
            {
                website = NormalizeWebsite(website);

                using var client = new HttpClient();

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
            catch
            {
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
    }
}