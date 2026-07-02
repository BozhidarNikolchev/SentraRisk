namespace SentraRisk.Models
{
    public class SslInfo
    {
        public bool IsValid { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int DaysRemaining { get; set; }
    }
}