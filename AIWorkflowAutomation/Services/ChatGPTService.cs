using AIWorkflowAutomation.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AIWorkflowAutomation.Services
{
    public class ChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly SentimentAnalysisService _sentimentService;
        private readonly TicketService _ticketService;

        public ChatGPTService(IConfiguration configuration, TicketService ticketService)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["ChatGPT:ApiKey"];
            _sentimentService = new SentimentAnalysisService();
            _ticketService = ticketService;
        }

        public async Task<string> GetResponseAsync(CustomerQuery customerQuery)
        {
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "user", content = customerQuery.Query }
        },
                max_tokens = 50
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            const int maxRetries = 3;
            int delay = 1000; // Start with a 1-second delay

            for (int retry = 0; retry < maxRetries; retry++)
            {
                var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();
                    var responseText = result.choices[0].message.content.ToString().Trim();

                    // Analyze sentiment and create a ticket
                    customerQuery.Sentiment = _sentimentService.AnalyzeSentiment(customerQuery.Query);
                    _ticketService.AddTicket(new Ticket
                    {
                        Query = customerQuery.Query,
                        Response = responseText,
                        Category = customerQuery.Category,
                        Sentiment = customerQuery.Sentiment
                    });

                    return responseText;
                }

                if (response.StatusCode == (HttpStatusCode)429) // Too Many Requests
                {
                    await Task.Delay(delay);
                    delay *= 2; // Double the delay for exponential backoff
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }

            throw new HttpRequestException("Request failed after maximum retries due to rate limit.");
        }

    }


}
