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

Console.WriteLine($"Risk Score: {result.Score} ({result.RiskLevel})");

Console.WriteLine("\nIssues:");
foreach (var issue in result.Issues)
{
    Console.WriteLine($"- {issue}");
}

Console.WriteLine("\nRecommendations:");
foreach (var rec in result.Recommendations)
{
    Console.WriteLine($"- {rec}");
}
