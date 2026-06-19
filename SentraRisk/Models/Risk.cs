using System;
using System.Collections.Generic;

namespace ComplianceRiskPlatform.Models
{
    public class Risk
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Likelihood { get; set; }

        public int Impact { get; set; }

        //  Dynamic Severity (core business logic)
        public int Severity => Likelihood * Impact;

        public string Status { get; set; } = string.Empty;

        //  Foreign key
        public int CreatedById { get; set; }

        //  Navigation property (IMPORTANT)
        public User CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //  Many-to-many relationship
        public ICollection<RiskControl> RiskControls { get; set; } = new List<RiskControl>();
    }
}