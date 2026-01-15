using MediatR;
using Pedidos.Application.DTOs;

namespace Pedidos.Application.Features.Pedidos.Queries.GetPedidoById
{
    public class GetPedidoByIdQuery : IRequest<PedidoDto>
    {
        public int Id { get; set; }
    }
}
