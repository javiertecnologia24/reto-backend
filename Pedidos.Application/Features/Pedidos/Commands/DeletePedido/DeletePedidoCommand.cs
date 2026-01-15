using MediatR;
using Pedidos.Application.Common;

namespace Pedidos.Application.Features.Pedidos.Commands.DeletePedido
{
    public class DeletePedidoCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
}
