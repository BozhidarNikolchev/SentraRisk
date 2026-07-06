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

            List<string> critical = new();
            List<string> medium = new();
            List<string> low = new();
            List<string> recommendations = new();

            string sslStatus = "";
            string sslBusinessImpact = "";

            string httpsStatus = "";
            string httpsBusinessImpact = "";
            string httpsFixInstructions = "";
            string httpsRecommendedSolution = "";

            string redirectStatus = "";
            string redirectBusinessImpact = "";
            string redirectFixInstructions = "";
            string redirectRecommendedSolution = "";

            if (input.SslInfo == null)
            {
                score += 50;

                critical.Add("No SSL certificate detected");

                recommendations.Add(
                    "Install a valid SSL certificate immediately.");

                sslStatus = "Missing";

                sslBusinessImpact =
                    "Visitors may see browser security warnings and lose trust in the website.";
            }
            else if (input.SslInfo.IsCritical)
            {
                score += 40;

                critical.Add(
                    $"SSL certificate expires in {input.SslInfo.DaysRemaining} days");

                recommendations.Add(
                    "Renew the SSL certificate immediately.");

                sslStatus = "Critical";

                sslBusinessImpact =
                    "The website may soon display certificate warnings that can reduce customer trust and impact sales.";
            }
            else if (input.SslInfo.IsExpiringSoon)
            {
                score += 20;

                medium.Add(
                    $"SSL certificate expires in {input.SslInfo.DaysRemaining} days");

                recommendations.Add(
                    "Schedule SSL certificate renewal.");

                sslStatus = "Expiring Soon";

                sslBusinessImpact =
                    "The certificate should be renewed soon to avoid service interruptions and security warnings.";
            }
            else
            {
                sslStatus = "Healthy";

                sslBusinessImpact =
                    "The SSL certificate is valid and does not currently present a business risk.";
            }

            if (input.SslInfo?.IsSelfSigned == true)
            {
                score += 30;

                medium.Add("Self-signed SSL certificate detected");

                recommendations.Add(
                    "Use a certificate issued by a trusted certificate authority.");

                sslStatus = "Self-Signed";

                sslBusinessImpact =
                    "Visitors may see browser security warnings because the certificate was not issued by a trusted authority.";
            }

            if (!input.UsesHttps)
            {
                score += 50;

                critical.Add("HTTPS is not enabled");

                recommendations.Add(
                    "Enable HTTPS and use a valid SSL certificate.");

                httpsStatus = "Not Enabled";

                httpsBusinessImpact =
                    "Visitors may see browser security warnings and lose trust in the website. Data submitted through forms may not be adequately protected.";

                httpsFixInstructions =
                    "1. Contact your hosting provider.\n" +
                    "2. Enable HTTPS.\n" +
                    "3. Install a valid SSL certificate.\n" +
                    "4. Redirect HTTP traffic to HTTPS.\n" +
                    "5. Test the website after deployment.";

                httpsRecommendedSolution =
                    "Hosting Provider, Let's Encrypt, Cloudflare";
            }
            else
            {
                httpsStatus = "Enabled";

                httpsBusinessImpact =
                    "The website uses HTTPS and provides encrypted communication between visitors and the website.";

                httpsFixInstructions =
                    "No action required. HTTPS is configured correctly.";

                httpsRecommendedSolution =
                    "Current configuration appears healthy.";
            }

            if (input.UsesHttps)
            {
                redirectStatus = "Healthy";

                redirectBusinessImpact =
                    "Visitors ultimately reach a secure HTTPS destination, helping ensure encrypted communication.";

                redirectFixInstructions =
                    "No action required. Visitors are successfully reaching a secure HTTPS version of the website.";

                redirectRecommendedSolution =
                    "Current configuration appears healthy.";
            }
            else
            {
                redirectStatus = "Redirect Missing";

                redirectBusinessImpact =
                    "Visitors may remain on an unencrypted HTTP connection.";

                redirectFixInstructions =
                    "1. Configure HTTP-to-HTTPS redirection.\n" +
                    "2. Ensure visitors are directed to HTTPS.\n" +
                    "3. Test after deployment.";

                redirectRecommendedSolution =
                    "Cloudflare, Apache Redirect Rules, Nginx Redirect Rules, IIS URL Rewrite";
            }

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

            string topIssue =
                critical.Count > 0 ? critical[0]
                : medium.Count > 0 ? medium[0]
                : low.Count > 0 ? low[0]
                : "No major risks detected";

            recommendations = recommendations.Distinct().ToList();

            List<string> priority = new();

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
                TopIssue = topIssue,

                HasSslCertificate = input.SslInfo != null,
                IsSslValid = input.SslInfo?.IsValid ?? false,
                SslExpirationDate = input.SslInfo?.ExpirationDate,
                SslDaysRemaining = input.SslInfo?.DaysRemaining,

                SslIssuer = input.SslInfo?.Issuer ?? "",

                IsSslSelfSigned =
        input.SslInfo?.IsSelfSigned ?? false,

                SslStatus = sslStatus,
                SslBusinessImpact = sslBusinessImpact,

                HttpsStatus = httpsStatus,
                HttpsBusinessImpact = httpsBusinessImpact,

                HttpsFixInstructions = httpsFixInstructions,
                HttpsRecommendedSolution = httpsRecommendedSolution,

                RedirectStatus = redirectStatus,
                RedirectBusinessImpact = redirectBusinessImpact,

                RedirectFixInstructions = redirectFixInstructions,
                RedirectRecommendedSolution = redirectRecommendedSolution
            };
        }
    }
}