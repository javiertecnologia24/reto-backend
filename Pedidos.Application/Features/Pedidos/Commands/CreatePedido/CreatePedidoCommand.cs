using MediatR;
using Pedidos.Application.Common;

namespace Pedidos.Application.Features.Pedidos.Commands.CreatePedido
{
    public class CreatePedidoCommand: IRequest<Result<int>>
    {
        public string NumeroPedido { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
