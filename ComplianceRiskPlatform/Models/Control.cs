using System;
using System.Collections.Generic;

namespace ComplianceRiskPlatform.Models
{
    public class Control
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //  Many-to-many relationship
        public ICollection<RiskControl> RiskControls { get; set; } = new List<RiskControl>();
    }
}
