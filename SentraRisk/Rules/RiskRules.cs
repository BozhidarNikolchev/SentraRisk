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
                    Condition = input => input.UsesOutdatedPlugins,
                    ScoreImpact = 25,
                    Issue = "Outdated plugins detected",
                    Recommendation = "Update all plugins to avoid vulnerabilities",
                    Severity = "Critical"
                },
                new RiskRule
                {
                    Condition = input => !input.HasBackup,
                    ScoreImpact = 20,
                    Issue = "No backup system",
                    Recommendation = "Set up automatic backups",
                    Severity = "Medium"
                },
                new RiskRule
                {
                    Condition = input => input.HasAdminUser,
                    ScoreImpact = 15,
                    Issue = "Default admin username used",
                    Recommendation = "Change admin username for security",
                    Severity = "Medium"
                },
                new RiskRule
                {
                    Condition = input => !input.HasTwoFactorAuth,
                    ScoreImpact = 15,
                    Issue = "No two-factor authentication",
                    Recommendation = "Enable 2FA for stronger login protection",
                    Severity = "Low"
                }
            };
        }
    }
}