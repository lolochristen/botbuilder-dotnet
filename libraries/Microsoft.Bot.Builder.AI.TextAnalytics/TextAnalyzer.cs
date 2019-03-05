using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Configuration.Services;
using Microsoft.Rest;

namespace Microsoft.Bot.Builder.AI.TextAnalytics
{
    public class TextAnalyzer
    {
        private TextAnalyticsClient _client;

        public TextAnalyzer(TextAnalyticsService service, HttpClient httpClient = null)
        {
            _client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials(service.SubscriptionKey), httpClient, true);
            _client.Endpoint = service.Endpoint;
        }

        public async Task<DetectedLanguage> DetectLanguageAsync(ITurnContext turnContext)
        {
            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (turnContext.Activity == null)
            {
                throw new ArgumentNullException(nameof(turnContext.Activity));
            }

            var messageActivity = turnContext.Activity.AsMessageActivity();
            if (messageActivity == null)
            {
                throw new ArgumentException("Activity type is not a message");
            }

            if (string.IsNullOrEmpty(turnContext.Activity.Text))
            {
                throw new ArgumentException("Null or empty text");
            }

            return await DetectLanguageAsync(messageActivity.Text).ConfigureAwait(false);
        }

        public async Task<DetectedLanguage> DetectLanguageAsync(string text)
        {
            var result = await _client.DetectLanguageAsync(new BatchInput(new List<Input>() { new Input("0", text) })).ConfigureAwait(false);

            if (result.Errors != null && result.Errors.Count > 0)
            {
                throw new TextAnalyzerException(string.Join("\n", result.Errors));
            }

            var textResult = result.Documents.FirstOrDefault();

            if (textResult.DetectedLanguages != null && textResult.DetectedLanguages.Count > 0)
            {
                // highest score
                return textResult.DetectedLanguages.First(p => p.Score == textResult.DetectedLanguages.Max(p1 => p1.Score));
            }
            else
            {
                return null;
            }
        }
    }
}
