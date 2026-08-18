using System.Collections.Generic;
using SentraRisk.Models;
using System.Linq;



namespace SentraRisk.Services
{
    public class FindingBuilder
    {
        public List<Finding> Build(
            WebsiteAssessment assessment)
        {
            var findings = new List<Finding>();

            if (!assessment.HstsEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "HSTS Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "HTTP Strict Transport Security (HSTS) tells browsers to always use HTTPS when connecting to a website.",

                    WhatWasChecked =
                        "SentraRisk checked for the Strict-Transport-Security response header.",

                    WhatWasFound =
                        "The HSTS header was not observed.",

                    WhyItMatters =
                        "Without HSTS, visitors may be more vulnerable to accidental HTTP access and protocol downgrade attacks.",

                    BusinessImpact =
                        "Browser-level protections may be reduced, increasing exposure to insecure communication scenarios.",

                    HowToFixIt =
                        "Configure the web server or CDN to send a Strict-Transport-Security header.",

                    WhereToFixIt =
                        "Web server configuration, CDN configuration, reverse proxy configuration, or application response headers.",

                    RecommendedSolution =
                        "Enable HSTS after verifying HTTPS is correctly configured across the website.",

                    SuggestedProviders = new List<string>
        {
            "Cloudflare",
            "Nginx",
            "Apache",
            "Microsoft IIS"
        },

                    Priority = "Medium"
                });
            }



            if (assessment.HstsEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "HSTS Configured",

                    Category = "Transport Security",

                    HowToFixIt =
    "No action required.",

                    WhereToFixIt =
    "No action required.",

                    RecommendedSolution =
    "Current HSTS configuration appears healthy.",

                    Severity = "Healthy",

                    IsHealthy = true,

                    WhatIsThis =
                        "HTTP Strict Transport Security (HSTS) tells browsers to use HTTPS for future visits.",

                    WhatWasChecked =
                        "SentraRisk checked for the Strict-Transport-Security header.",

                    WhatWasFound =
                        "The HSTS header was observed.",

                    WhyItMatters =
                        "HSTS helps prevent accidental insecure connections.",

                    BusinessImpact =
                        "Visitors benefit from stronger browser-level transport security protections.",

                    HealthyStateExplanation =
                        "HSTS was detected and appears to be configured correctly. No action is currently required.",

                    Priority = "Healthy"
                });
            }




            if (!assessment.UsesHttps)
            {
                findings.Add(new Finding
                {
                    Title = "HTTPS Not Enabled",

                    Severity = "Critical",

                    WhatIsThis =
                        "HTTPS encrypts communication between visitors and the website, helping protect data from interception and tampering.",

                    WhatWasChecked =
                        "SentraRisk checked whether the website supports secure HTTPS communication.",

                    WhatWasFound =
                        "The website does not appear to support HTTPS.",

                    WhyItMatters =
                        "Without HTTPS, information exchanged between visitors and the website may be exposed to interception or modification.",

                    BusinessImpact =
                        "Visitors may receive browser security warnings, lose trust in the website, and avoid submitting information.",

                    HowToFixIt =
                        "Install a valid SSL certificate, enable HTTPS, and ensure website traffic is served over encrypted connections.",

                    WhereToFixIt =
                        "Web hosting platform, web server configuration, CDN configuration, or reverse proxy configuration.",

                    RecommendedSolution =
                        "Enable HTTPS site-wide and redirect all HTTP traffic to HTTPS.",

                    SuggestedProviders = new List<string>
        {
            "Let's Encrypt",
            "Cloudflare",
            "Microsoft IIS",
            "Nginx",
            "Apache"
        },

                    Priority = "Critical"
                });
            }



            if (assessment.UsesHttps)
            {
                findings.Add(new Finding
                {
                    Title = "HTTPS Enabled",

                    Category = "Transport Security",

                    HowToFixIt =
    "No action required.",

                    WhereToFixIt =
    "No action required.",

                    RecommendedSolution =
    "Current HTTPS configuration appears healthy.",

                    Severity = "Healthy",

                    IsHealthy = true,

                    WhatIsThis =
                        "HTTPS encrypts communication between visitors and the website.",

                    WhatWasChecked =
                        "SentraRisk checked whether secure HTTPS communication is supported.",

                    WhatWasFound =
                        "HTTPS was successfully detected.",

                    WhyItMatters =
                        "HTTPS protects information exchanged between visitors and the website.",

                    BusinessImpact =
                        "Visitors benefit from encrypted communication and increased trust.",

                    HealthyStateExplanation =
                        "HTTPS appears to be configured correctly and no action is currently required.",

                    Priority = "Healthy"
                });
            }


            if (assessment.UsesHttps &&
    !assessment.RedirectsToHttps)
            {
                findings.Add(new Finding
                {
                    Title = "HTTPS Redirect Not Configured",

                    Severity = "Medium",

                    WhatIsThis =
                        "Visitors should automatically be redirected from HTTP to HTTPS.",

                    WhatWasChecked =
                        "SentraRisk checked whether HTTP visitors ultimately reach a secure HTTPS destination.",

                    WhatWasFound =
                        "Automatic HTTP-to-HTTPS redirection was not observed.",

                    BusinessImpact =
                        "Visitors may continue using an unencrypted connection when accessing the website through HTTP links.",

                    Priority = "Medium"
                });
            }



            if (assessment.UsesHttps &&
    assessment.RedirectsToHttps)
            {
                findings.Add(new Finding
                {
                    Title = "HTTPS Redirect Configured",

                    WhatIsThis =
    "HTTP-to-HTTPS redirection automatically directs visitors from insecure HTTP connections to secure HTTPS connections.",

                    HowToFixIt =
    "No action required.",

                    WhereToFixIt =
    "No action required.",

                    RecommendedSolution =
    "Current redirect configuration appears healthy.",

                    Category = "Transport Security",

                    Severity = "Healthy",

                    IsHealthy = true,

                    WhatWasChecked =
                        "SentraRisk checked whether HTTP visitors are automatically redirected to HTTPS.",

                    WhatWasFound =
                        "Automatic redirection to HTTPS was observed.",

                    WhyItMatters =
                        "Visitors consistently reach the secure version of the website.",

                    BusinessImpact =
                        "Reduced risk of visitors accidentally using insecure HTTP connections.",

                    HealthyStateExplanation =
                        "HTTP-to-HTTPS redirection appears to be configured correctly.",

                    Priority = "Healthy"
                });
            }


            if (assessment.SslInfo == null)
            {
                findings.Add(new Finding
                {
                    Title = "SSL Certificate Missing",

                    Severity = "Critical",

                    WhatIsThis =
                        "SSL certificates establish trusted encrypted communication between visitors and a website.",

                    WhatWasChecked =
                        "SentraRisk checked whether the website presented a valid SSL certificate during the secure connection process.",

                    WhatWasFound =
                        "No SSL certificate was observed.",

                    WhyItMatters =
                        "Without a valid SSL certificate, browsers cannot establish trusted encrypted connections to the website.",

                    BusinessImpact =
                        "Visitors may encounter browser security warnings, lose trust in the website, and abandon transactions or form submissions.",

                    HowToFixIt =
                        "Obtain and install a trusted SSL certificate, then verify that HTTPS is functioning correctly.",

                    WhereToFixIt =
                        "Web server configuration, hosting control panel, CDN configuration, load balancer configuration, or reverse proxy configuration.",

                    RecommendedSolution =
                        "Deploy a trusted SSL certificate and enable HTTPS across the entire website.",

                    SuggestedProviders = new List<string>
        {
            "Let's Encrypt",
            "Sectigo",
            "DigiCert",
            "Cloudflare"
        },

                    Priority = "Critical"
                });
            }



            if (assessment.SslInfo?.IsExpiringSoon == true)
            {
                findings.Add(new Finding
                {
                    Title = "SSL Certificate Expiring Soon",

                    Severity = "Medium",

                    WhatIsThis =
                        "SSL certificates have expiration dates and must be renewed regularly to maintain trusted encrypted communication.",

                    WhatWasChecked =
                        "SentraRisk checked the SSL certificate expiration date and remaining validity period.",

                    WhatWasFound =
                        $"The SSL certificate expires in {assessment.SslInfo.DaysRemaining} days.",

                    WhyItMatters =
                        "Certificates that are not renewed before expiration can suddenly disrupt trusted encrypted communication.",

                    BusinessImpact =
                        "Visitors may eventually receive browser security warnings, potentially reducing trust and impacting business operations.",

                    HowToFixIt =
                        "Schedule certificate renewal before the expiration date and verify deployment after renewal.",

                    WhereToFixIt =
                        "Certificate provider portal, hosting platform, web server configuration, CDN, or reverse proxy.",

                    RecommendedSolution =
                        "Renew the certificate well before expiration and automate renewals when possible.",

                    SuggestedProviders = new List<string>
        {
            "Let's Encrypt",
            "DigiCert",
            "Sectigo",
            "Cloudflare"
        },

                    Priority = "Medium"
                });
            }



            if (assessment.SslInfo?.IsCritical == true)
            {
                findings.Add(new Finding
                {
                    Title = "SSL Certificate Near Expiration",

                    Severity = "Critical",

                    WhatIsThis =
                        "SSL certificates must remain valid to maintain trusted encrypted connections.",

                    WhatWasChecked =
                        "SentraRisk checked the SSL certificate expiration date.",

                    WhatWasFound =
                        $"The SSL certificate expires in {assessment.SslInfo.DaysRemaining} days.",

                    WhyItMatters =
                        "An SSL certificate approaching expiration requires immediate attention to prevent service trust issues.",

                    BusinessImpact =
                        "If the certificate expires, visitors may encounter browser warnings, abandoned transactions, and loss of trust.",

                    HowToFixIt =
                        "Renew and deploy a replacement certificate immediately.",

                    WhereToFixIt =
                        "Certificate provider portal, hosting platform, web server configuration, CDN, or reverse proxy.",

                    RecommendedSolution =
                        "Renew the certificate immediately and implement monitoring or automated renewals.",

                    SuggestedProviders = new List<string>
        {
            "Let's Encrypt",
            "DigiCert",
            "Sectigo",
            "Cloudflare"
        },

                    Priority = "Critical"
                });
            }



            if (assessment.SslInfo?.IsSelfSigned == true)
            {
                findings.Add(new Finding
                {
                    Title = "Self-Signed SSL Certificate",

                    Severity = "Medium",

                    WhatIsThis =
                        "Self-signed certificates are created internally rather than issued by a publicly trusted certificate authority.",

                    WhatWasChecked =
                        "SentraRisk evaluated the certificate trust chain presented by the website.",

                    WhatWasFound =
                        "A self-signed certificate was observed.",

                    WhyItMatters =
                        "Most browsers and devices do not automatically trust self-signed certificates.",

                    BusinessImpact =
                        "Visitors may receive browser security warnings and may not trust the website.",

                    HowToFixIt =
                        "Replace the self-signed certificate with one issued by a trusted certificate authority.",

                    WhereToFixIt =
                        "Certificate management platform, hosting environment, web server configuration, CDN, or reverse proxy.",

                    RecommendedSolution =
                        "Deploy a certificate from a publicly trusted certificate authority.",

                    SuggestedProviders = new List<string>
        {
            "Let's Encrypt",
            "DigiCert",
            "Sectigo",
            "GlobalSign"
        },

                    Priority = "Medium"
                });
            }



            if (assessment.SslInfo != null &&
    assessment.SslInfo.IsValid &&
    !assessment.SslInfo.IsSelfSigned &&
    !assessment.SslInfo.IsCritical &&
    !assessment.SslInfo.IsExpiringSoon)
            {
                findings.Add(new Finding
                {
                    Title = "SSL Certificate Healthy",

                    WhatIsThis =
    "SSL certificates enable trusted encrypted communication between visitors and websites.",

                    HowToFixIt =
    "No action required.",

                    WhereToFixIt =
    "No action required.",

                    RecommendedSolution =
    "Current SSL configuration appears healthy.",

                    Severity = "Healthy",

                    Category = "Transport Security",

                    IsHealthy = true,

                    WhatWasChecked =
                        "SentraRisk evaluated the SSL certificate validity and trust status.",

                    WhatWasFound =
                        "A trusted and valid SSL certificate was observed.",

                    WhyItMatters =
                        "Valid SSL certificates help establish trusted encrypted communication.",

                    BusinessImpact =
                        "Visitors can securely access the website without certificate trust warnings.",

                    HealthyStateExplanation =
                        "The SSL certificate appears valid, trusted, and is not approaching expiration.",

                    Priority = "Healthy"
                });
            }




            if (assessment.Spf != null &&
      !assessment.Spf.SpfDetected)
            {
                findings.Add(new Finding
                {
                    Title = "SPF Record Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "Sender Policy Framework (SPF) identifies which mail servers are authorized to send email for a domain.",

                    WhatWasChecked =
                        "SentraRisk checked DNS records for a valid SPF policy.",

                    WhatWasFound =
                        "No SPF record was observed.",

                    WhyItMatters =
                        "Without SPF, attackers may find it easier to spoof email addresses belonging to the domain.",

                    BusinessImpact =
                        "Customers and employees may be exposed to phishing and email impersonation attempts.",

                    HowToFixIt =
                        "Publish an SPF record that includes legitimate email sending services.",

                    WhereToFixIt =
                        "DNS provider management console.",

                    RecommendedSolution =
                        "Deploy an SPF policy covering all authorized email senders.",

                    SuggestedProviders = new List<string>
        {
            "Cloudflare DNS",
            "Microsoft 365",
            "Google Workspace",
            "GoDaddy DNS"
        },

                    Priority = "Medium"
                });
            }


            if (assessment.Dmarc != null &&
      !assessment.Dmarc.DmarcDetected)
            {
                findings.Add(new Finding
                {
                    Title = "DMARC Policy Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "DMARC helps domain owners control how email providers handle unauthenticated messages claiming to come from their domain.",

                    WhatWasChecked =
                        "SentraRisk checked DNS records for a DMARC policy.",

                    WhatWasFound =
                        "No DMARC policy was observed.",

                    WhyItMatters =
                        "Without DMARC, email providers receive less guidance when handling spoofed messages.",

                    BusinessImpact =
                        "The domain may face increased phishing, spoofing, and brand impersonation risk.",

                    HowToFixIt =
                        "Publish a DMARC DNS record and define a monitoring or enforcement policy.",

                    WhereToFixIt =
                        "DNS provider management console.",

                    RecommendedSolution =
                        "Deploy a DMARC policy and gradually move toward enforcement after monitoring results.",

                    SuggestedProviders = new List<string>
        {
            "Cloudflare DNS",
            "Microsoft 365",
            "Google Workspace",
            "GoDaddy DNS"
        },

                    Priority = "Medium"
                });
            }




            if (!assessment.XFrameProtected)
            {
                findings.Add(new Finding
                {
                    Title = "X-Frame-Options Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "X-Frame-Options helps prevent websites from being embedded inside frames on external websites.",

                    WhatWasChecked =
                        "SentraRisk checked for the X-Frame-Options response header.",

                    WhatWasFound =
                        "The X-Frame-Options header was not observed.",

                    BusinessImpact =
                        "The website may be more vulnerable to clickjacking attacks.",

                    Priority = "Medium"
                });
            }


            if (!assessment.ContentTypeProtected)
            {
                findings.Add(new Finding
                {
                    Title = "X-Content-Type-Options Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "X-Content-Type-Options helps browsers avoid MIME type confusion.",

                    WhatWasChecked =
                        "SentraRisk checked for the X-Content-Type-Options response header.",

                    WhatWasFound =
                        "The X-Content-Type-Options header was not observed.",

                    BusinessImpact =
                        "Browsers may be more likely to incorrectly interpret content types.",

                    Priority = "Medium"
                });
            }


            if (!assessment.ReferrerPolicyEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Referrer Policy Not Detected",

                    Severity = "Low",

                    WhatIsThis =
                        "Referrer-Policy controls how much referral information browsers share with other websites.",

                    WhatWasChecked =
                        "SentraRisk checked for a Referrer-Policy response header.",

                    WhatWasFound =
                        "No Referrer-Policy header was observed.",

                    BusinessImpact =
                        "More information than intended may be shared with third-party websites.",

                    Priority = "Low"
                });
            }


            if (!assessment.CspEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Content Security Policy Not Detected",

                    Severity = "Medium",

                    WhatIsThis =
                        "Content Security Policy (CSP) is a browser security mechanism that helps reduce the risk of cross-site scripting and content injection attacks.",

                    WhatWasChecked =
                        "SentraRisk checked for a Content-Security-Policy response header.",

                    WhatWasFound =
                        "No Content-Security-Policy header was observed.",

                    WhyItMatters =
                        "Without CSP, browsers have fewer restrictions on what content can execute within pages.",

                    BusinessImpact =
                        "The website may have reduced protection against malicious script injection and other browser-based attacks.",

                    HowToFixIt =
                        "Create and deploy a Content-Security-Policy defining approved content sources.",

                    WhereToFixIt =
                        "Web server configuration, CDN configuration, reverse proxy configuration, or application response headers.",

                    RecommendedSolution =
                        "Implement a restrictive Content-Security-Policy and gradually refine it based on legitimate website requirements.",

                    SuggestedProviders = new List<string>
        {
            "Cloudflare",
            "Nginx",
            "Apache",
            "Microsoft IIS"
        },

                    Priority = "Medium"
                });
            }




            if (!assessment.PermissionsPolicyEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Permissions Policy Not Detected",

                    Category = "Security Headers",

                    WhyItMatters =
    "Permissions-Policy helps reduce unnecessary access to browser capabilities.",

                    HowToFixIt =
    "Configure a Permissions-Policy header defining allowed browser features.",

                    WhereToFixIt =
    "Web server configuration, CDN configuration, reverse proxy configuration, or application response headers.",

                    RecommendedSolution =
    "Deploy a Permissions-Policy header following business requirements.",

                    Severity = "Low",

                    WhatIsThis =
                        "Permissions-Policy helps control access to browser features and APIs.",

                    WhatWasChecked =
                        "SentraRisk checked for a Permissions-Policy response header.",

                    WhatWasFound =
                        "No Permissions-Policy header was observed.",

                    BusinessImpact =
                        "Browser capabilities may not be restricted as tightly as intended.",

                    Priority = "Low"
                });
            }


            if (!assessment.CoopEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Cross-Origin Opener Policy Not Detected",

                    Category = "Security Headers",

                    WhyItMatters =
    "COOP helps isolate browser contexts and reduce certain cross-origin attack scenarios.",

                    HowToFixIt =
    "Configure a Cross-Origin-Opener-Policy response header.",

                    WhereToFixIt =
    "Web server, CDN, reverse proxy, or application response configuration.",

                    RecommendedSolution =
    "Deploy a suitable Cross-Origin-Opener-Policy configuration.",

                    Severity = "Low",

                    WhatIsThis =
                        "COOP helps isolate browsing contexts from cross-origin interactions.",

                    WhatWasChecked =
                        "SentraRisk checked for a Cross-Origin-Opener-Policy response header.",

                    WhatWasFound =
                        "No COOP header was observed.",

                    BusinessImpact =
                        "Browser isolation protections may be reduced.",

                    Priority = "Low"
                });
            }


            if (!assessment.CorpEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Cross-Origin Resource Policy Not Detected",

                    Category = "Security Headers",

                    WhyItMatters =
    "CORP helps control which external websites may load resources.",

                    HowToFixIt =
    "Configure a Cross-Origin-Resource-Policy header.",

                    WhereToFixIt =
    "Web server, CDN, reverse proxy, or application response configuration.",

                    RecommendedSolution =
    "Deploy a suitable Cross-Origin-Resource-Policy configuration.",

                    Severity = "Low",

                    WhatIsThis =
                        "CORP helps control how resources may be loaded from other origins.",

                    WhatWasChecked =
                        "SentraRisk checked for a Cross-Origin-Resource-Policy response header.",

                    WhatWasFound =
                        "No CORP header was observed.",

                    BusinessImpact =
                        "Cross-origin resource sharing protections may be weaker than intended.",

                    Priority = "Low"
                });
            }


            findings = findings
    .OrderBy(f =>
        f.Priority == "Critical" ? 1 :
        f.Priority == "Medium" ? 2 :
        f.Priority == "Low" ? 3 :
        4)
    .ToList();


            findings = findings
            .OrderBy(f =>
                f.Priority == "Critical" ? 1 :
                f.Priority == "Medium" ? 2 :
                f.Priority == "Low" ? 3 :
                4)
            .ToList();

            return findings;
        }
    }
}