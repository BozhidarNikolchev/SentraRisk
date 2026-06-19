namespace SentraRisk.Models
{
    public class WebsiteInput
    {
        public bool UsesHttps { get; set; }
        public bool HasBackup { get; set; }
        public bool UsesOutdatedPlugins { get; set; }
        public bool HasAdminUser { get; set; }
        public bool HasTwoFactorAuth { get; set; }
    }
}
