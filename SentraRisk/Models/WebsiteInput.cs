namespace SentraRisk.Models
{
    public class WebsiteInput
    {
        public string WebsiteUrl { get; set; } = "";
        public bool UsesHttps { get; set; }
        public bool HasBackup { get; set; }
        public bool UsesOutdatedPlugins { get; set; }
        public bool HasAdminUser { get; set; }
        public bool HasTwoFactorAuth { get; set; }
    }
}
