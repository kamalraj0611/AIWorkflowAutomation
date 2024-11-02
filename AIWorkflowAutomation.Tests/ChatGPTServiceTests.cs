using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIWorkflowAutomation.Services;
using Microsoft.Extensions.Configuration;
using Xunit;


namespace AIWorkflowAutomation.Tests
{
    
    public class ChatGPTServiceTests
    {
        [Fact]
        public void AnalyzeSentiment_ShouldReturnPositive()
        {
            var service = new SentimentAnalysisService();
            var sentiment = service.AnalyzeSentiment("This product is great!");
            Assert.Equal("Positive", sentiment);
        }
    }

}
