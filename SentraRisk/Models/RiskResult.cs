using System.Collections.Generic;

namespace SentraRisk.Models
{
    public class RiskResult
    {
        public int Score { get; set; }
        public string RiskLevel { get; set; } = "";

        public List<string> CriticalIssues { get; set; } = new();
        public List<string> MediumIssues { get; set; } = new();
        public List<string> LowIssues { get; set; } = new();

        public List<string> Recommendations { get; set; } = new();

        public List<string> PriorityActions { get; set; } = new();

        public string Summary { get; set; } = "";
        public string TopIssue { get; set; } = "";

    }
}
