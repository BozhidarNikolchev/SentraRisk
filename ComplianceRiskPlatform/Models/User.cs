using System;
using System.Collections.Generic;

namespace ComplianceRiskPlatform.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //  One-to-many relationship
        public ICollection<Risk> Risks { get; set; } = new List<Risk>();
    }
}
