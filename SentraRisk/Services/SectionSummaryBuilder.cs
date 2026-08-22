using SentraRisk.Models;

namespace SentraRisk.Services
{
    public static class SectionSummaryBuilder
    {
        public static string Build(
            int healthy,
            int critical,
            int medium,
            int low)
        {
            var issues = critical + medium + low;

            if (issues == 0)
            {
                return $"All {healthy} controls in this category appear healthy.";
            }

            return $"{issues} findings require attention including {critical} critical, {medium} medium, and {low} low risk issues.";
        }
    }
}