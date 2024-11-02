using AIWorkflowAutomation.Models;
using System.Collections.Generic;
using System.Linq;


namespace AIWorkflowAutomation.Services
{
    

    public class TicketService
    {
        private readonly List<Ticket> _tickets = new List<Ticket>();

        public void AddTicket(Ticket ticket)
        {
            ticket.CreatedAt = DateTime.Now;
            _tickets.Add(ticket);
        }

        public IEnumerable<Ticket> GetAllTickets() => _tickets;

        public Report GenerateReport()
        {
            return new Report
            {
                TotalTickets = _tickets.Count,
                ResolvedTickets = _tickets.Count(t => !string.IsNullOrEmpty(t.Response)),
                SentimentCounts = _tickets.GroupBy(t => t.Sentiment)
                                          .ToDictionary(g => g.Key, g => g.Count())
            };
        }
    }

}
