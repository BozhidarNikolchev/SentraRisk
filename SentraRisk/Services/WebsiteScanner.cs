using System.Net.Http;

namespace SentraRisk.Services
{
    public class WebsiteScanner
    {
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
    }
}