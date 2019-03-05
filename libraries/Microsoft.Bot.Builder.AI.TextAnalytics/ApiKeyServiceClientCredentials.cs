using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Microsoft.Bot.Builder.AI.TextAnalytics
{
    /// <summary>
    /// Container for subscription credentials. Make sure to enter your valid key.
    /// </summary>
    public class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private string _key;

        public ApiKeyServiceClientCredentials(string key)
        {
            _key = key;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _key);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
