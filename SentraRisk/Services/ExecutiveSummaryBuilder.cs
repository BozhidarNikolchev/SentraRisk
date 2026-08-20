using SentraRisk.Models;

namespace SentraRisk.Services
{
    public static class ExecutiveSummaryBuilder
    {
        public static ExecutiveSummary Build(List<Finding> findings)
        {
            var summary = new ExecutiveSummary();

            summary.HealthyControls =
                findings.Count(f => f.IsHealthy);

            summary.FindingsRequiringAttention =
                findings.Count(f => !f.IsHealthy);


            summary.KeyStrengths =
findings
    .Where(f => f.IsHealthy)
    .Select(f => f.Title)
    .Take(5)
    .ToList();


            summary.PriorityConcerns =
            findings
                .Where(f =>
                    f.Priority == "Critical" ||
                    f.Priority == "Medium")
                .Select(f => f.Title)
                .Take(5)
                .ToList();


            summary.ImmediateActions =
findings
    .Where(f => !f.IsHealthy)
    .Select(f => f.RecommendedSolution)
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Distinct()
    .Take(5)
    .ToList();



            if (summary.FindingsRequiringAttention == 0)
            {
                summary.OverallAssessment =
                    "The website demonstrates strong security controls and no significant risks were identified.";
            }
            else if (findings.Any(f => f.Priority == "Critical"))
            {
                summary.OverallAssessment =
                    "Critical security issues were identified and should be addressed immediately.";
            }
            else if (findings.Any(f => f.Priority == "Medium"))
            {
                summary.OverallAssessment =
                    "The website has moderate security weaknesses that should be resolved in the near term.";
            }
            else
            {
                summary.OverallAssessment =
                    "The website is generally secure but several low-risk improvements are recommended.";
            }

            return summary;
        }
    }
}