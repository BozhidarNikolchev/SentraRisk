namespace SentraRisk.Models
{
    public class ReportSection
    {
        public string Name { get; set; } = "";

        public List<Finding> Findings { get; set; } = new();
    }
}