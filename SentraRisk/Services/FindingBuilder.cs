using System.Collections.Generic;
using SentraRisk.Models;

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
                        "HTTP Strict Transport Security (HSTS) tells browsers to always use encrypted HTTPS connections.",

                    WhatWasChecked =
                        "SentraRisk checked for the Strict-Transport-Security response header.",

                    WhatWasFound =
                        "The HSTS header was not observed in the website response.",

                    BusinessImpact =
                        "Visitors may be more vulnerable to accidental HTTP access and protocol downgrade attacks.",

                    Priority = "Medium"
                });
            }


            if (!assessment.UsesHttps)
            {
                findings.Add(new Finding
                {
                    Title = "HTTPS Not Enabled",

                    Severity = "Critical",

                    WhatIsThis =
                        "HTTPS encrypts traffic between visitors and the website.",

                    WhatWasChecked =
                        "SentraRisk checked whether encrypted HTTPS communication could be established.",

                    WhatWasFound =
                        "The website does not appear to support HTTPS.",

                    BusinessImpact =
                        "Visitors may receive browser warnings and information exchanged with the website may be exposed to interception.",

                    Priority = "Critical"
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


            if (assessment.SslInfo == null)
            {
                findings.Add(new Finding
                {
                    Title = "SSL Certificate Missing",

                    Severity = "Critical",

                    WhatIsThis =
                        "SSL certificates help establish trusted encrypted communication.",

                    WhatWasChecked =
                        "SentraRisk checked whether the website presented an SSL certificate.",

                    WhatWasFound =
                        "No SSL certificate was observed.",

                    BusinessImpact =
                        "Visitors may encounter security warnings and lose trust in the website.",

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
                        "SSL certificates have expiration dates and must be renewed periodically.",

                    WhatWasChecked =
                        "SentraRisk checked the certificate expiration date and calculated the remaining validity period.",

                    WhatWasFound =
                        $"The SSL certificate expires in {assessment.SslInfo.DaysRemaining} days.",

                    BusinessImpact =
                        "If the certificate expires, visitors may receive browser security warnings and lose trust in the website.",

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
                        "SSL certificates must remain valid to maintain trusted encrypted communication.",

                    WhatWasChecked =
                        "SentraRisk checked the certificate expiration date.",

                    WhatWasFound =
                        $"The SSL certificate expires in {assessment.SslInfo.DaysRemaining} days.",

                    BusinessImpact =
                        "An expired certificate can trigger browser warnings, damage visitor trust, and potentially disrupt business operations.",

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
                        "Self-signed certificates are not issued by a publicly trusted certificate authority.",

                    WhatWasChecked =
                        "SentraRisk evaluated the SSL certificate trust chain.",

                    WhatWasFound =
                        "A self-signed certificate was observed.",

                    BusinessImpact =
                        "Visitors may receive browser security warnings and may not trust the website.",

                    Priority = "Medium"
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
                        "Sender Policy Framework (SPF) helps define which mail servers are authorized to send email on behalf of a domain.",

                    WhatWasChecked =
                        "SentraRisk checked DNS records for a valid SPF policy.",

                    WhatWasFound =
                        "No SPF record was observed.",

                    BusinessImpact =
                        "Attackers may find it easier to impersonate the domain in email messages, increasing phishing and spoofing risk.",

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
                        "DMARC helps domain owners define how email providers should handle unauthenticated messages.",

                    WhatWasChecked =
                        "SentraRisk checked for a DMARC DNS record.",

                    WhatWasFound =
                        "No DMARC policy was observed.",

                    BusinessImpact =
                        "Email providers may have less guidance on handling spoofed messages claiming to come from the domain.",

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
                        "Content Security Policy helps reduce the risk of cross-site scripting and content injection attacks.",

                    WhatWasChecked =
                        "SentraRisk checked for a Content-Security-Policy response header.",

                    WhatWasFound =
                        "No Content-Security-Policy header was observed.",

                    BusinessImpact =
                        "The website may have fewer browser-level protections against injected content.",

                    Priority = "Medium"
                });
            }


            if (!assessment.PermissionsPolicyEnabled)
            {
                findings.Add(new Finding
                {
                    Title = "Permissions Policy Not Detected",

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

            return findings;
        }
    }
}