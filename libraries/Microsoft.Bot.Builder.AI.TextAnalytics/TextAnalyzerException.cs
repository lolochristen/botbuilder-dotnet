using System;

namespace Microsoft.Bot.Builder.AI.TextAnalytics
{
    public class TextAnalyzerException : Exception
    {
        public TextAnalyzerException()
            : base()
        {
        }

        public TextAnalyzerException(string message)
            : base(message)
        {
        }

        public TextAnalyzerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
