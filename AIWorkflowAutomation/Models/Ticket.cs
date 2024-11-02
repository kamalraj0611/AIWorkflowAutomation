namespace AIWorkflowAutomation.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Response { get; set; }
        public string Category { get; set; }
        public string Sentiment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
