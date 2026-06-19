using System;
using SentraRisk.Models;

namespace SentraRisk.Rules
{
    public class RiskRule
    {
        public Func<WebsiteInput, bool> Condition { get; set; } = _ => false;
        public int ScoreImpact { get; set; }

        public string Issue { get; set; } = "";
        public string Recommendation { get; set; } = "";
        public string Severity { get; set; } = "";
    }
}
