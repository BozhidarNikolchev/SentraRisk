using System.Collections.Generic;
using SentraRisk.Models;

namespace SentraRisk.Rules
{
    public static class RiskRules
    {
        public static List<RiskRule> GetAll()
        {
            return new List<RiskRule>
            {
                new RiskRule
                {
                    Condition = input => !input.UsesHttps,
                    ScoreImpact = 25,
                    Issue = "No HTTPS encryption",
                    Recommendation = "Enable HTTPS immediately to protect data",
                    Severity = "Critical"
                },

                new RiskRule
                {
                    Condition = input =>
                        input.UsesHttps &&
                        !input.RedirectsToHttps,

                    ScoreImpact = 10,

                    Issue = "HTTP visitors are not automatically redirected to HTTPS",

                    Recommendation = "Configure automatic HTTP-to-HTTPS redirection",

                    Severity = "Medium"
                },
            };
        }
    }
}