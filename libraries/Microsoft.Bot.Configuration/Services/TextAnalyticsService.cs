using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Bot.Configuration.Encryption;
using Newtonsoft.Json;

namespace Microsoft.Bot.Configuration.Services
{
    public class TextAnalyticsService : ConnectedService
    {
        public TextAnalyticsService()
            : base(ServiceTypes.TextAnalytics)
        {
            Endpoint = "https://westus.api.cognitive.microsoft.com";
        }

        /// <summary>
        /// Gets or sets subscriptionKey.
        /// </summary>
        [JsonProperty("subscriptionKey")]
        public string SubscriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the endpoint of the TextAnalytics Services.
        /// </summary>
        /// <example>https://westus.api.cognitive.microsoft.com</example>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        /// <inheritdoc/>
        public override void Encrypt(string secret)
        {
            base.Encrypt(secret);

            if (!string.IsNullOrEmpty(this.SubscriptionKey))
            {
                this.SubscriptionKey = this.SubscriptionKey.Encrypt(secret);
            }
        }

        /// <inheritdoc/>
        public override void Decrypt(string secret)
        {
            base.Decrypt(secret);

            if (!string.IsNullOrEmpty(this.SubscriptionKey))
            {
                this.SubscriptionKey = this.SubscriptionKey.Decrypt(secret);
            }
        }
    }
}
