namespace SentraRisk.Models
{
    public class ReportSection
    {
        public string Name { get; set; } = "";

        public int HealthyCount { get; set; }

        public int CriticalCount { get; set; }

        public int MediumCount { get; set; }

        public int LowCount { get; set; }

        public string Summary { get; set; } = "";

        public List<Finding> Findings { get; set; } = new();
    }
}