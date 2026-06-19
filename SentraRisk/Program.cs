using SentraRisk.Models;
using SentraRisk.Logic;

var input = new WebsiteInput
{
    UsesHttps = false,
    HasBackup = false,
    UsesOutdatedPlugins = true,
    HasAdminUser = true,
    HasTwoFactorAuth = false
};

var calculator = new RiskCalculator();
var result = calculator.Calculate(input);

Console.WriteLine("\n=== SENTRARISK REPORT ===\n");

Console.WriteLine($"📊 Summary: {result.Summary}");

Console.WriteLine($"\n🚨 Top Risk: {result.TopIssue}");

Console.WriteLine($"\nRisk Score: {result.Score}/100 ({result.RiskLevel})");

Console.WriteLine("\n🚨 Critical Issues:");
foreach (var issue in result.CriticalIssues)
{
    Console.WriteLine($"- {issue}");
}

Console.WriteLine("\n⚠ Medium Issues:");
foreach (var issue in result.MediumIssues)
{
    Console.WriteLine($"- {issue}");
}

Console.WriteLine("\nℹ Low Issues:");
foreach (var issue in result.LowIssues)
{
    Console.WriteLine($"- {issue}");
}

Console.WriteLine("\n🚀 Priority Actions:");
foreach (var action in result.PriorityActions)
{
    Console.WriteLine($"- {action}");
}

Console.WriteLine("\n✅ Recommendations:");
foreach (var rec in result.Recommendations)
{
    Console.WriteLine($"- {rec}");
}
