namespace SentraRisk.Models
{
    public class TechnologyEvidence
    {
        public Dictionary<string, string> Headers { get; set; }
            = new();

        public string Html { get; set; } = "";

        public List<string> Cookies { get; set; }
            = new();
    }
}