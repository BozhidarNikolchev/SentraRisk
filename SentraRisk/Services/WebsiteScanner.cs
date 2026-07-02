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
                if (!website.StartsWith("http"))
                {
                    website = "https://" + website;
                }

                using var client = new HttpClient();

                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetAsync(website);

                return response.IsSuccessStatusCode;
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
                if (!website.StartsWith("http"))
                {
                    website = "https://" + website;
                }

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
                website = website
                    .Replace("https://", "")
                    .Replace("http://", "");

                using var client = new HttpClient(
                    new HttpClientHandler
                    {
                        AllowAutoRedirect = false
                    });

                client.Timeout = TimeSpan.FromSeconds(10);

                var response =
                    await client.GetAsync($"http://{website}");

                if (response.Headers.Location != null)
                {
                    return response.Headers.Location
                        .ToString()
                        .StartsWith("https://");
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SslInfo?> GetSslInfoAsync(string website)
        {
            try
            {
                website = website
                    .Replace("https://", "")
                    .Replace("http://", "")
                    .Split('/')[0];

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
                        (expirationDate - DateTime.UtcNow).Days
                };
            }
            catch
            {
                return null;
            }
        }
    }
}