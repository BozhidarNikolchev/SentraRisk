namespace SentraRisk.Models
{
    public class Finding
    {
        public string Title { get; set; } = "";

        public string Severity { get; set; } = "";

        public string WhatIsThis { get; set; } = "";

        public string WhatWasChecked { get; set; } = "";

        public string WhatWasFound { get; set; } = "";

        public string BusinessImpact { get; set; } = "";

        public string Priority { get; set; } = "";
    }
}