using System.Collections.Generic;

namespace SentraRisk.Models
{
    public class RiskResult
    {
        public int Score { get; set; }
        public string RiskLevel { get; set; }
        public List<string> Issues { get; set; }
        public List<string> Recommendations { get; set; }
    }
}
