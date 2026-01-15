using MediatR;
using Microsoft.Extensions.Logging;
using Pedidos.Application.Common;
using Pedidos.Application.Features.Pedidos.Commands.CreatePedido;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.Features.Pedidos.Commands.DeletePedido
{
    public class DeletePedidoHandler : IRequestHandler<DeletePedidoCommand, Result<int>>
    {
        private readonly IPedidoRepository _repository;
        private readonly ILogger<CreatePedidoHandler> _logger;
        public DeletePedidoHandler(IPedidoRepository repository, ILogger<CreatePedidoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<int>> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.obtenerPedidoPorIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<int>.Fail("Producto no encontrado");
            }

            await _repository.EliminarAsync(pedido);

            _logger.LogInformation("Pedido eliminado {@Pedido}", pedido);

            return Result<int>.Ok(pedido.Id, "Pedido eliminado correctamente");
        }
    }
}
