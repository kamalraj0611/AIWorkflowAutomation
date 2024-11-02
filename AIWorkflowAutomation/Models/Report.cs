namespace AIWorkflowAutomation.Models
{
    public class Report
    {
        public int TotalTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public Dictionary<string, int> SentimentCounts { get; set; }
    }

}
