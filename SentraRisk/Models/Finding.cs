using System.Collections.Generic;

namespace SentraRisk.Models
{
    public class Finding
    {
        public string Title { get; set; } = "";

        public string Severity { get; set; } = "";

        public string WhatIsThis { get; set; } = "";

        public string WhatWasChecked { get; set; } = "";

        public string WhatWasFound { get; set; } = "";

        public string WhyItMatters { get; set; } = "";

        public string BusinessImpact { get; set; } = "";

        public string HowToFixIt { get; set; } = "";

        public string WhereToFixIt { get; set; } = "";

        public string RecommendedSolution { get; set; } = "";

        public List<string> SuggestedProviders { get; set; } = new();

        public string Priority { get; set; } = "";

        public bool IsHealthy { get; set; }

        public string HealthyStateExplanation { get; set; } = "";

        public string Category { get; set; } = "";
    }
}