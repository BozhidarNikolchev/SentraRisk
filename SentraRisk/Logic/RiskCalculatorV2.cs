using System.Collections.Generic;
using System.Linq;
using SentraRisk.Models;
using SentraRisk.Services;



namespace SentraRisk.Logic
{
    public class RiskCalculatorV2
    {
        public RiskResult Calculate(
     WebsiteAssessment assessment)
        {
            string httpsStatus = "";
            string httpsBusinessImpact = "";
            string httpsFixInstructions = "";
            string httpsRecommendedSolution = "";

            string redirectStatus = "";
            string redirectBusinessImpact = "";
            string redirectFixInstructions = "";
            string redirectRecommendedSolution = "";


            string sslStatus = "";
            string sslBusinessImpact = "";

            if (assessment.SslInfo == null)
            {
                sslStatus = "Missing";

                sslBusinessImpact =
                    "Visitors may see browser security warnings and lose trust in the website.";
            }
            else if (assessment.SslInfo.IsSelfSigned)
            {
                sslStatus = "Self-Signed";

                sslBusinessImpact =
                    "Visitors may see browser security warnings because the certificate was not issued by a trusted authority.";
            }
            else if (assessment.SslInfo.IsCritical)
            {
                sslStatus = "Critical";

                sslBusinessImpact =
                    "The website may soon display certificate warnings that can reduce customer trust and impact sales.";
            }
            else if (assessment.SslInfo.IsExpiringSoon)
            {
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



            if (!assessment.UsesHttps)
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

            if (assessment.UsesHttps)
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

            var detectedTechnologies =
    new List<string>();

            if (assessment.Technologies != null)
            {
                if (assessment.Technologies.WordPressDetected)
                {
                    detectedTechnologies.Add("WordPress");
                }

                if (assessment.Technologies.ShopifyDetected)
                {
                    detectedTechnologies.Add("Shopify");
                }

                if (assessment.Technologies.CloudflareDetected)
                {
                    detectedTechnologies.Add("Cloudflare");
                }

                if (assessment.Technologies.GitHubDetected)
                {
                    detectedTechnologies.Add("GitHub");
                }

                if (assessment.Technologies.NginxDetected)
                {
                    detectedTechnologies.Add("Nginx");
                }

                if (assessment.Technologies.ApacheDetected)
                {
                    detectedTechnologies.Add("Apache");
                }

                if (assessment.Technologies.AspNetDetected)
                {
                    detectedTechnologies.Add("ASP.NET");
                }

                if (assessment.Technologies.IisDetected)
                {
                    detectedTechnologies.Add("IIS");
                }

                if (assessment.Technologies.PhpDetected)
                {
                    detectedTechnologies.Add("PHP");
                }
            }

            if (score > 100)
            {
                score = 100;
            }

            recommendations =
    recommendations
        .Distinct()
        .ToList();

            var priority = new List<string>();

            if (critical.Count > 0)
            {
                priority.Add(
                    "Fix critical security issues immediately");
            }

            if (medium.Count > 0)
            {
                priority.Add(
                    "Address medium risks to stabilize system");
            }

            if (low.Count > 0)
            {
                priority.Add(
                    "Improve overall security with low-risk fixes");
            }



            var findingBuilder = new FindingBuilder();


            var findings =
                findingBuilder.Build(assessment);


            var executiveSummary =
ExecutiveSummaryBuilder.Build(findings);


            var report =
                ReportBuilder.Build(findings);


            return new RiskResult
            {
                Score = score,

                RiskLevel =
         critical.Count > 0
             ? "High"
             : score >= 30
                 ? "Medium"
                 : "Low",

                Findings = findings,

                CriticalIssues = critical,

                MediumIssues = medium,

                LowIssues = low,

                Recommendations = recommendations,



                Summary =
    critical.Count > 0
        ? "Your system has critical vulnerabilities that must be addressed immediately."
        : medium.Count > 0
            ? "Your system has moderate risks that should be resolved soon."
            : low.Count > 0
                ? "Your system is relatively safe but can be improved."
                : "Your system is secure.",



                TopIssue =
    critical.Count > 0
        ? critical[0]
        : medium.Count > 0
            ? medium[0]
            : low.Count > 0
                ? low[0]
                : "No major risks detected",



                HasSslCertificate =
         assessment.SslInfo != null,

                IsSslValid =
         assessment.SslInfo?.IsValid ?? false,

                SslExpirationDate =
         assessment.SslInfo?.ExpirationDate,

                SslDaysRemaining =
         assessment.SslInfo?.DaysRemaining,

                SslIssuer =
         assessment.SslInfo?.Issuer ?? "",

                IsSslSelfSigned =
         assessment.SslInfo?.IsSelfSigned ?? false,



                SslStatus = sslStatus,

                SslBusinessImpact =
    sslBusinessImpact,

                HttpsStatus =
    httpsStatus,

                HttpsBusinessImpact =
    httpsBusinessImpact,

                HttpsFixInstructions =
    httpsFixInstructions,

                HttpsRecommendedSolution =
    httpsRecommendedSolution,

                RedirectStatus =
    redirectStatus,

                RedirectBusinessImpact =
    redirectBusinessImpact,

                RedirectFixInstructions =
    redirectFixInstructions,

                RedirectRecommendedSolution =
    redirectRecommendedSolution,

                PriorityActions = priority,

                DetectedTechnologies =
    detectedTechnologies,

                ExecutiveSummary =
    executiveSummary,

                Report =
    report,



                CriticalFindingCount =
    findings.Count(f => f.Priority == "Critical"),

                MediumFindingCount =
    findings.Count(f => f.Priority == "Medium"),

                LowFindingCount =
    findings.Count(f => f.Priority == "Low"),

                HealthyFindingCount =
    findings.Count(f => f.IsHealthy)
            };
        }
    }
}