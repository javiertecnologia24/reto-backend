using MediatR;
using Pedidos.Application.DTOs;

namespace Pedidos.Application.Features.Pedidos.Queries.GetAllPedidos
{
    public class GetAllPedidosQuery: IRequest<List<PedidoDto>>
    {
    }
}
