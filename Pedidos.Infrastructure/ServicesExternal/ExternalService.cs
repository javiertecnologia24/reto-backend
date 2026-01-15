using Pedidos.Domain.Entities;
using Pedidos.Infrastructure.Resilience;

namespace Pedidos.Infrastructure.ServicesExternal
{
    public class ExternalService
    {
        private readonly HttpClient _httpClient;
        public ExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<bool> ValidarPedidoExternoAsync(Pedido pedido)
        {
            var response = await PollyPolicies.RetryPolicy.ExecuteAsync(
                () => PollyPolicies.CircuitBreakerPolicy.ExecuteAsync(
                () => _httpClient.GetAsync($"https://servicio-externo-aaa-bbb.com//" + pedido.Id)
                )
            );

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }                
            return true;
        }
    }
}
