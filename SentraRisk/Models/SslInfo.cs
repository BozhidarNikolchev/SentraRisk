namespace SentraRisk.Models
{
    public class SslInfo
    {
        public bool IsValid { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int DaysRemaining { get; set; }

        public bool IsExpiringSoon => DaysRemaining <= 30;

        public bool IsCritical => DaysRemaining <= 7;

        public string Issuer { get; set; } = "";

        public bool IsSelfSigned { get; set; }
    }
}