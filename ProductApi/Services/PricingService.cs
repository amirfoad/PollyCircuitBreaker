using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;
using ProductApi.Contracts;
using ProductApi.Models;
using System.Text.Json;

namespace ProductApi.Services
{
    public class PricingService : IPricingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PricingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static readonly Random Jitterer = new();

        private static readonly AsyncRetryPolicy<HttpResponseMessage> _transientErrorRetryPolicy =
            Policy.HandleResult<HttpResponseMessage>(
                message => ((int)message.StatusCode) == 429 || (int)message.StatusCode >= 500)
            .WaitAndRetryAsync(2, retryAttemp =>
            {
                Console.WriteLine($"Retrying becaus of transient error. Attempt {retryAttemp}");
                return TimeSpan.FromSeconds(Math.Pow(2, retryAttemp))
                        + TimeSpan.FromMilliseconds(Jitterer.Next(0, 50));
            });

        private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
            Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503)
            .AdvancedCircuitBreakerAsync(0.5,
                TimeSpan.FromMinutes(1),
                100,
                TimeSpan.FromMinutes(1));

        //.CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

        private readonly AsyncPolicyWrap<HttpResponseMessage> _resilientPolicy =
            _circuitBreakerPolicy.WrapAsync(_transientErrorRetryPolicy);

        public async Task<PricingDetails> GetPricingForProductAsync(Guid productId, string currencyCode)
        {
            if (_circuitBreakerPolicy.CircuitState == CircuitState.Open)
                throw new Exception("Service is currently unavailable");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await _resilientPolicy.ExecuteAsync(() =>
            httpClient.GetAsync($"https://localhost:7193/products/{productId}/currencies/{currencyCode}"));

            if (!response.IsSuccessStatusCode)
                throw new Exception("Service is currently unavailable");

            var responseText = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PricingDetails>(responseText);
        }
    }
}