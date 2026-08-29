
namespace SentraRisk.Models
{
    public class DkimResult
    {
        public bool DkimDetected { get; set; }

        public string? SelectorFound { get; set; }

        public string? DkimRecord { get; set; }
    }
}
