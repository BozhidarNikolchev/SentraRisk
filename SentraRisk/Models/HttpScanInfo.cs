namespace SentraRisk.Models
{
    public class HttpScanInfo
    {
        public string OriginalUrl { get; set; } = "";

        public string FinalUrl { get; set; } = "";

        public int StatusCode { get; set; }

        public bool UsesHttps { get; set; }

        public bool Redirected { get; set; }
    }
}