using MediatR;
using Microsoft.Extensions.Logging;
using Pedidos.Application.Common;
using Pedidos.Application.Features.Pedidos.Commands.CreatePedido;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.Features.Pedidos.Commands.UpdatePedido
{
    public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoCommand, Result<int>>
    {
        private readonly IPedidoRepository _repository;
        private readonly ILogger<CreatePedidoHandler> _logger;

        public UpdatePedidoHandler(IPedidoRepository repository, ILogger<CreatePedidoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<int>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.obtenerPedidoPorIdAsync(request.Id);
            if (pedido == null)
            {
                return Result<int>.Fail("Producto no encontrado");
            }

            if (request.Total <= 0)
            {
                return Result<int>.Fail("El total del pedido debe ser mayor a 0");
            }

            if (await _repository.NumeroPedidoExisteAsync(request.NumeroPedido, request.Id))
            {
                return Result<int>.Fail("El numero de pedido ya existe");
            }

            pedido.NumeroPedido = request.NumeroPedido;
            pedido.Cliente = request.Cliente;
            pedido.Fecha = request.Fecha;
            pedido.Total = request.Total;

            await _repository.ActualizarAsync(pedido);

            _logger.LogInformation("Pedido actualizado {@Pedido}", pedido);

            return Result<int>.Ok(pedido.Id, "Pedido actualizado correctamente");

        }
    }
}
