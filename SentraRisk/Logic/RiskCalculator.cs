using System.Collections.Generic;
using SentraRisk.Models;

namespace SentraRisk.Logic
{
    public class RiskCalculator
    {
        public RiskResult Calculate(WebsiteInput input)
        {
            int score = 0;
            List<string> issues = new List<string>();
            List<string> recommendations = new List<string>();

            if (!input.UsesHttps)
            {
                score += 30;
                issues.Add("No HTTPS encryption");
                recommendations.Add("Enable HTTPS to protect data");
            }

            if (input.UsesOutdatedPlugins)
            {
                score += 25;
                issues.Add("Outdated plugins detected");
                recommendations.Add("Update all plugins regularly");
            }

            if (!input.HasBackup)
            {
                score += 20;
                issues.Add("No backup system");
                recommendations.Add("Set up automatic backups");
            }

            if (input.HasAdminUser)
            {
                score += 15;
                issues.Add("Default admin username used");
                recommendations.Add("Change admin username");
            }

            if (!input.HasTwoFactorAuth)
            {
                score += 15;
                issues.Add("No two-factor authentication");
                recommendations.Add("Enable 2FA to improve security");
            }

            string level = score < 30 ? "Low"
                         : score < 60 ? "Medium"
                         : "High";

            return new RiskResult
            {
                Score = score,
                RiskLevel = level,
                Issues = issues,
                Recommendations = recommendations
            };
        }
    }
}
