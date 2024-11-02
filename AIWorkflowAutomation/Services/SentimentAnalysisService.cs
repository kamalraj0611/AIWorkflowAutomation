namespace AIWorkflowAutomation.Services
{
    public class SentimentAnalysisService
    {
        public string AnalyzeSentiment(string query)
        {
            // Simple sentiment analysis: return "Positive", "Neutral", or "Negative"
            if (query.Contains("good") || query.Contains("great") || query.Contains("love"))
                return "Positive";
            if (query.Contains("bad") || query.Contains("hate") || query.Contains("worst"))
                return "Negative";
            return "Neutral";
        }
    }

}
