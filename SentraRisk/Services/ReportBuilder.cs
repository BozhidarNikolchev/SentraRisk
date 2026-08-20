using SentraRisk.Models;

namespace SentraRisk.Services
{
    public static class ReportBuilder
    {
        public static AssessmentReport Build(List<Finding> findings)
        {
            var report = new AssessmentReport();

            report.Sections =
                findings
                    .GroupBy(f => f.Category)
                    .Select(g => new ReportSection
                    {
                        Name = g.Key,
                        Findings = g.ToList()
                    })
                    .ToList();

            return report;
        }
    }
}