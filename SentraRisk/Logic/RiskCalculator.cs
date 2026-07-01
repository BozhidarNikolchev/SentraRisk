using System.Collections.Generic;
using System.Linq;
using SentraRisk.Models;
using SentraRisk.Rules;

namespace SentraRisk.Logic
{
    public class RiskCalculator
    {
        public RiskResult Calculate(WebsiteInput input)
        {
            if (!input.IsReachable)
            {
                return new RiskResult
                {
                    Score = 0,
                    RiskLevel = "Unavailable",

                    CriticalIssues = new List<string>(),
                    MediumIssues = new List<string>(),
                    LowIssues = new List<string>(),

                    Recommendations = new List<string>
                    {
                        "Verify the website URL.",
                        "Ensure the website is online and accessible."
                    },

                    PriorityActions = new List<string>
                    {
                        "Fix website availability before performing a risk assessment."
                    },

                    Summary = "The website could not be reached. A full assessment could not be completed.",

                    TopIssue = "Website unreachable"
                };
            }

            int score = 0;

            List<string> critical = new List<string>();
            List<string> medium = new List<string>();
            List<string> low = new List<string>();
            List<string> recommendations = new List<string>();

            var rules = RiskRules.GetAll();

            foreach (var rule in rules)
            {
                if (rule.Condition(input))
                {
                    score += rule.ScoreImpact;

                    if (rule.Severity == "Critical")
                        critical.Add(rule.Issue);
                    else if (rule.Severity == "Medium")
                        medium.Add(rule.Issue);
                    else
                        low.Add(rule.Issue);

                    recommendations.Add(rule.Recommendation);
                }
            }

            string topIssue = critical.Count > 0 ? critical[0]
                  : medium.Count > 0 ? medium[0]
                  : low.Count > 0 ? low[0]
                  : "No major risks detected";

            recommendations = recommendations.Distinct().ToList();

            List<string> priority = new List<string>();

            if (critical.Count > 0)
                priority.Add("Fix critical security issues immediately");

            if (medium.Count > 0)
                priority.Add("Address medium risks to stabilize system");

            if (low.Count > 0)
                priority.Add("Improve overall security with low-risk fixes");

            if (score > 100)
                score = 100;

            string level;

            if (critical.Count > 0)
            {
                level = "High";
            }
            else if (score >= 30)
            {
                level = "Medium";
            }
            else
            {
                level = "Low";
            }

            string summary;

            if (critical.Count > 0)
            {
                summary = "Your system has critical vulnerabilities that must be addressed immediately.";
            }
            else if (medium.Count > 0)
            {
                summary = "Your system has moderate risks that should be resolved soon.";
            }
            else if (low.Count > 0)
            {
                summary = "Your system is relatively safe but can be improved.";
            }
            else
            {
                summary = "Your system is secure.";
            }

            return new RiskResult
            {
                Score = score,
                RiskLevel = level,
                CriticalIssues = critical,
                MediumIssues = medium,
                LowIssues = low,
                Recommendations = recommendations,
                PriorityActions = priority,
                Summary = summary,
                TopIssue = topIssue
            };
        }
    }
}