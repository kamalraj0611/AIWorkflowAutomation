using Microsoft.AspNetCore.Mvc;
using AIWorkflowAutomation.Services;
using System.Threading.Tasks;
using AIWorkflowAutomation.Models;

namespace AIWorkflowAutomation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly ChatGPTService _chatGPTService;
        private readonly TicketService _ticketService;

        public SupportController(ChatGPTService chatGPTService, TicketService ticketService)
        {
            _chatGPTService = chatGPTService;
            _ticketService = ticketService;
        }

        [HttpPost("query")]
        public async Task<IActionResult> HandleCustomerQuery([FromBody] CustomerQuery customerQuery)
        {
            // Check for FAQ (mock implementation)
            var faqResponses = new Dictionary<string, string>
        {
            { "What are your hours?", "Our support hours are from 9 AM to 5 PM." },
            { "How can I reset my password?", "You can reset your password by clicking 'Forgot Password' on the login page." }
        };

            if (faqResponses.TryGetValue(customerQuery.Query, out var faqResponse))
            {
                return Ok(new { Response = faqResponse });
            }

            // If not in FAQs, use ChatGPT
            var response = await _chatGPTService.GetResponseAsync(customerQuery);
            return Ok(new { Response = response });
        }

        [HttpGet("report")]
        public IActionResult GetReport()
        {
            var report = _ticketService.GenerateReport();
            return Ok(report);
        }
    }


}
