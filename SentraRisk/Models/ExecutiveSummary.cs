namespace SentraRisk.Models
{
    public class ExecutiveSummary
    {
        public string OverallAssessment { get; set; } = "";

        public List<string> KeyStrengths { get; set; } = new();

        public List<string> PriorityConcerns { get; set; } = new();

        public List<string> ImmediateActions { get; set; } = new();

        public int HealthyControls { get; set; }

        public int FindingsRequiringAttention { get; set; }
    }
}