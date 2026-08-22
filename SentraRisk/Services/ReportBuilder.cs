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
                    .Select(g =>
{
    var healthy =
        g.Count(f => f.Severity == "Healthy");

    var critical =
        g.Count(f => f.Severity == "Critical");

    var medium =
        g.Count(f => f.Severity == "Medium");

    var low =
        g.Count(f => f.Severity == "Low");

    return new ReportSection
    {
        Name = g.Key,

        HealthyCount = healthy,

        CriticalCount = critical,

        MediumCount = medium,

        LowCount = low,

        Summary =
            SectionSummaryBuilder.Build(
                healthy,
                critical,
                medium,
                low),

        Findings = g.ToList()
    };
})
                    .ToList();

            return report;
        }
    }
}