namespace SentraRisk.Models
{
    public class WebsiteAssessment
    {
        public bool IsReachable { get; set; }

        public bool UsesHttps { get; set; }

        public bool RedirectsToHttps { get; set; }

        public SslInfo? SslInfo { get; set; }

        public TechnologyDetectionResult? Technologies { get; set; }

        public SpfResult? Spf { get; set; }

        public DmarcResult? Dmarc { get; set; }

        public DkimResult? Dkim { get; set; }

        public bool HstsEnabled { get; set; }

        public bool XFrameProtected { get; set; }

        public bool ContentTypeProtected { get; set; }

        public bool ReferrerPolicyEnabled { get; set; }

        public bool CspEnabled { get; set; }

        public bool PermissionsPolicyEnabled { get; set; }

        public bool CoopEnabled { get; set; }

        public bool CorpEnabled { get; set; }

        public Dictionary<string, string> SecurityHeaders
        {
            get;
            set;
        }
= new();
    }
}