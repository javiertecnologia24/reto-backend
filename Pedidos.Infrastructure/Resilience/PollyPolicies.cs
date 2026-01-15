using Polly;

namespace Pedidos.Infrastructure.Resilience
{
    public class PollyPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> RetryPolicy =>
            Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(msg => !msg.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));
        
        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy =>
            Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(msg => !msg.IsSuccessStatusCode)
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
    }
}
