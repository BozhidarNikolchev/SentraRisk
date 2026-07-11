namespace SentraRisk.Models
{
    public class TechnologyDetectionResult
    {
        public bool CloudflareDetected { get; set; }

        public bool WordPressDetected { get; set; }

        public bool ShopifyDetected { get; set; }
    }
}