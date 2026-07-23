namespace SentraRisk.Models
{
    public class DmarcResult
    {
        public bool DmarcDetected { get; set; }

        public string DmarcRecord { get; set; } = "";
    }
}