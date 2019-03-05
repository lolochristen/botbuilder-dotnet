using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace Microsoft.Bot.Builder.AI.TextAnalytics
{
    public class TextAnalyticsOptions
    {
        public TextAnalyticsOptions()
        {
            DefaultLanguage = new DetectedLanguage("English", "en");
            Endpoint = "https://westus.api.cognitive.microsoft.com";
        }

        public string Key { get; set; }

        public DetectedLanguage DefaultLanguage { get; set; }

        public string Endpoint { get; set; }
    }
}
