using System;
using System.Collections.Generic;

namespace SentraRisk.Models
{
    public class RiskResult
    {
        public int Score { get; set; }

        public string RiskLevel { get; set; } = "";

        public List<string> CriticalIssues { get; set; } = new();

        public List<string> MediumIssues { get; set; } = new();

        public List<string> LowIssues { get; set; } = new();

        public List<string> Recommendations { get; set; } = new();

        public List<string> PriorityActions { get; set; } = new();

        public string Summary { get; set; } = "";

        public string TopIssue { get; set; } = "";

        public bool HasSslCertificate { get; set; }

        public bool IsSslValid { get; set; }

        public DateTime? SslExpirationDate { get; set; }

        public int? SslDaysRemaining { get; set; }

        public string SslStatus { get; set; } = "";

        public string SslBusinessImpact { get; set; } = "";

        public string SslIssuer { get; set; } = "";

        public bool IsSslSelfSigned { get; set; }

        public string HttpsStatus { get; set; } = "";

        public string HttpsBusinessImpact { get; set; } = "";

        public string HttpsFixInstructions { get; set; } = "";

        public string HttpsRecommendedSolution { get; set; } = "";

        public string RedirectStatus { get; set; } = "";

        public string RedirectBusinessImpact { get; set; } = "";

        public string RedirectFixInstructions { get; set; } = "";

        public string RedirectRecommendedSolution { get; set; } = "";
    }
}