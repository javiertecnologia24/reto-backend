using MediatR;
using Microsoft.Extensions.Logging;
using Pedidos.Application.Common;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.Features.Pedidos.Commands.CreatePedido
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoCommand, Result<int>>
    {
        private readonly IPedidoRepository _repository;
        private readonly ILogger<CreatePedidoHandler> _logger;

        public CreatePedidoHandler(IPedidoRepository repository, ILogger<CreatePedidoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<int>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            if (request.Total <= 0) {
                return Result<int>.Fail("El total del pedido debe ser mayor a 0");
            }

            if (await _repository.NumeroPedidoExisteAsync(request.NumeroPedido, -1)) {
                return Result<int>.Fail("El numero de pedido ya existe");
            }


            var pedido = new Pedido()
            {
                NumeroPedido = request.NumeroPedido,
                Cliente = request.Cliente,
                Fecha = request.Fecha,
                Total = request.Total
            };

            await _repository.AgregarAsync(pedido);

            _logger.LogInformation("Pedido creado {@Pedido}", pedido);

            return Result<int>.Ok(pedido.Id, "Pedido creado correctamente");
        }
    }
}
