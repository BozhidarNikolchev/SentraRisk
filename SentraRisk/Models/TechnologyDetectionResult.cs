namespace SentraRisk.Models
{
    public class TechnologyDetectionResult
    {
        public bool CloudflareDetected { get; set; }

        public bool WordPressDetected { get; set; }

        public bool ShopifyDetected { get; set; }

        public bool NginxDetected { get; set; }

        public bool ApacheDetected { get; set; }

        public bool IisDetected { get; set; }  // needs more validation

        public bool AspNetDetected { get; set; }  // needs more validation

        public bool PhpDetected { get; set; } // needs more validation
    }
}