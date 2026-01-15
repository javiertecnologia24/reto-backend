using MediatR;
using Pedidos.Application.DTOs;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.Features.Pedidos.Queries.GetAllPedidos
{
    public class GetAllPedidosHandler : IRequestHandler<GetAllPedidosQuery, List<PedidoDto>>
    {
        private readonly IPedidoRepository _repository;

        public GetAllPedidosHandler(IPedidoRepository repository)
        {
           _repository = repository; 
        }

        public async Task<List<PedidoDto>> Handle(GetAllPedidosQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _repository.obtenerListaPedidosAsync();

            return pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                NumeroPedido = p.NumeroPedido,
                Cliente = p.Cliente,
                Fecha = p.Fecha,
                Total = p.Total
            }).ToList();
        }
    }
}
