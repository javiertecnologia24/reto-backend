using MediatR;
using Pedidos.Application.Common;

namespace Pedidos.Application.Features.Pedidos.Commands.UpdatePedido
{
    public class UpdatePedidoCommand: IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
