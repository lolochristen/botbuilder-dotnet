using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Configuration.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace Microsoft.Bot.Builder.AI.TextAnalytics.Tests
{
    [TestClass]
    public class TextAnalyticsTests
    {
        private const string _key = "dummy";

        [TestMethod]
        [TestCategory("AI")]
        [TestCategory("TextAnalytics")]
        public async Task TextAnalyzer_DetectLanguage()
        {
            var service = new TextAnalyticsService()
            {
                SubscriptionKey = _key,
                Endpoint = "https://westeurope.api.cognitive.microsoft.com",
            };


            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, service.Endpoint + "*")
                .Respond("application/json", GetResponse("DetectLanguage_German.json"));

            var textAnalyzer = new TextAnalyzer(service, mockHttp.ToHttpClient());

            var result = await textAnalyzer.DetectLanguageAsync("Das ist ein Text.");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Iso6391Name, "de");
        }

        [TestMethod]
        [TestCategory("AI")]
        [TestCategory("TextAnalytics")]
        public async Task TextAnalyzer_DetectLanguageContext()
        {
            var service = new TextAnalyticsService()
            {
                SubscriptionKey = _key,
                Endpoint = "https://westeurope.api.cognitive.microsoft.com",
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, service.Endpoint + "*")
                .Respond("application/json", GetResponse("DetectLanguage_German.json"));

            var textAnalyzer = new TextAnalyzer(service, mockHttp.ToHttpClient());

            var context = new TurnContext(new TestAdapter(), new Schema.Activity() { Type = "message", Text = "Ich bin ein Berliner." });
            var result = await textAnalyzer.DetectLanguageAsync(context);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Iso6391Name, "de");
        }

        private const string _testData = @"..\..\..\TestData\";

        private Stream GetResponse(string fileName)
        {
            var path = Path.Combine(_testData, fileName);
            return File.OpenRead(path);
        }
    }
}
