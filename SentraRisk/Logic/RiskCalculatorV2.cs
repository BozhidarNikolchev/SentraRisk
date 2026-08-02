using System.Collections.Generic;
using SentraRisk.Models;

namespace SentraRisk.Logic
{
    public class RiskCalculatorV2
    {
        public RiskResult Calculate(
     WebsiteAssessment assessment)
        {
            if (!assessment.IsReachable)
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

                    Summary =
                        "The website could not be reached. A full assessment could not be completed.",

                    TopIssue =
                        "Website unreachable"
                };
            }

            int score = 0;

            var critical =
                new List<string>();

            var medium =
                new List<string>();

            var low =
new List<string>();

            var recommendations =
                new List<string>();

            if (assessment.SslInfo == null)
            {
                score += 50;

                critical.Add(
                    "No SSL certificate detected");

                recommendations.Add(
                    "Install a valid SSL certificate.");

                return new RiskResult
                {
                    Score = score,

                    RiskLevel = "High",

                    CriticalIssues = critical,

                    Recommendations = recommendations,

                    Summary =
                        "No SSL certificate was detected.",

                    TopIssue =
                        "No SSL certificate detected"
                };
            }

            if (assessment.SslInfo.IsCritical)
            {
                score += 40;

                critical.Add(
                    $"SSL certificate expires in {assessment.SslInfo.DaysRemaining} days");

                recommendations.Add(
                    "Renew the SSL certificate immediately.");
            }

            else if (assessment.SslInfo.IsExpiringSoon)
            {
                score += 20;

                medium.Add(
                    $"SSL certificate expires in {assessment.SslInfo.DaysRemaining} days");

                recommendations.Add(
                    "Schedule SSL certificate renewal.");
            }

            if (assessment.SslInfo.IsSelfSigned)
            {
                score += 30;

                medium.Add(
                    "Self-signed SSL certificate detected");

                recommendations.Add(
                    "Use a trusted certificate authority.");
            }

            if (!assessment.UsesHttps)
            {
                score += 50;

                critical.Add(
                    "HTTPS is not enabled");

                recommendations.Add(
                    "Enable HTTPS and install a valid SSL certificate.");
            }

            if (assessment.UsesHttps &&
    !assessment.RedirectsToHttps)
            {
                score += 10;

                medium.Add(
                    "HTTP visitors are not automatically redirected to HTTPS");

                recommendations.Add(
                    "Configure automatic HTTP-to-HTTPS redirection.");
            }

            if (assessment.Spf != null &&
    !assessment.Spf.SpfDetected)
            {
                score += 20;

                medium.Add(
                    "No SPF record detected");

                recommendations.Add(
                    "Publish an SPF record to help protect the domain from email spoofing.");
            }

            if (assessment.Dmarc != null &&
    !assessment.Dmarc.DmarcDetected)
            {
                score += 25;

                medium.Add(
                    "No DMARC policy detected");

                recommendations.Add(
                    "Publish a DMARC policy to improve email authentication and reduce phishing risk.");
            }

            if (!assessment.HstsEnabled)
            {
                score += 10;

                medium.Add(
                    "HSTS header not detected");

                recommendations.Add(
                    "Enable Strict-Transport-Security (HSTS).");
            }

            if (!assessment.XFrameProtected)
            {
                score += 5;

                medium.Add(
                    "X-Frame-Options header not detected");

                recommendations.Add(
                    "Enable X-Frame-Options to help prevent clickjacking attacks.");
            }

            if (!assessment.ContentTypeProtected)
            {
                score += 5;

                medium.Add(
                    "X-Content-Type-Options header not detected");

                recommendations.Add(
                    "Enable X-Content-Type-Options to reduce MIME-type confusion risks.");
            }

            if (!assessment.ReferrerPolicyEnabled)
            {
                score += 3;

                low.Add(
                    "Referrer-Policy header not detected");

                recommendations.Add(
                    "Define a Referrer-Policy to limit information leakage.");
            }

            if (!assessment.CspEnabled)
            {
                score += 10;

                medium.Add(
                    "Content-Security-Policy header not detected");

                recommendations.Add(
                    "Implement a Content-Security-Policy to reduce XSS risk.");
            }

            if (!assessment.PermissionsPolicyEnabled)
            {
                score += 2;

                low.Add(
                    "Permissions-Policy header not detected");

                recommendations.Add(
                    "Define a Permissions-Policy to restrict browser features.");
            }

            if (!assessment.CoopEnabled)
            {
                score += 2;

                low.Add(
                    "Cross-Origin-Opener-Policy header not detected");

                recommendations.Add(
                    "Consider enabling COOP for browser isolation.");
            }

            if (!assessment.CorpEnabled)
            {
                score += 2;

                low.Add(
                    "Cross-Origin-Resource-Policy header not detected");

                recommendations.Add(
                    "Consider enabling CORP to control resource sharing.");
            }

            if (assessment.Technologies != null)
            {
                if (assessment.Technologies.WordPressDetected)
                {
                    low.Add(
                        "WordPress detected");
                }

                if (assessment.Technologies.ShopifyDetected)
                {
                    low.Add(
                        "Shopify detected");
                }

                if (assessment.Technologies.CloudflareDetected)
                {
                    low.Add(
                        "Cloudflare detected");
                }

                if (assessment.Technologies.NginxDetected)
                {
                    low.Add(
                        "Nginx detected");
                }

                if (assessment.Technologies.ApacheDetected)
                {
                    low.Add(
                        "Apache detected");
                }

                if (assessment.Technologies.AspNetDetected)
                {
                    low.Add(
                        "ASP.NET detected");
                }
            }



            return new RiskResult
            {
                Score = score,

                RiskLevel =
                    critical.Count > 0
                        ? "High"
                        : score >= 30
                            ? "Medium"
                            : "Low",

                CriticalIssues = critical,

                MediumIssues = medium,

                LowIssues = low,

                Recommendations = recommendations,

                Summary =
    critical.Count > 0
        ? "Critical security issues were detected."
        : medium.Count > 0
            ? "Moderate security issues were detected."
            : "No major security issues were detected.",

                TopIssue =
                    critical.Count > 0
                        ? critical[0]
                        : medium.Count > 0
                            ? medium[0]
                            : "No major risks detected"
            };
        }
    }
}