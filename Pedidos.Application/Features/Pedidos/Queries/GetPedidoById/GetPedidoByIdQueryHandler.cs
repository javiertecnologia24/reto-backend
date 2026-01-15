using MediatR;
using Pedidos.Application.DTOs;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.Features.Pedidos.Queries.GetPedidoById
{
    public class GetPedidoByIdQueryHandler: IRequestHandler<GetPedidoByIdQuery, PedidoDto>
    {
        private readonly IPedidoRepository _repository;

        public GetPedidoByIdQueryHandler(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PedidoDto> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.obtenerPedidoPorIdAsync(request.Id);
            if (pedido == null)
            {
                return null;
            }

            return new PedidoDto
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                Cliente = pedido.Cliente,
                Fecha = pedido.Fecha,
                Total = pedido.Total
            };
        }
    }
}
