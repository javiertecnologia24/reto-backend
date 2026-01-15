using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Common;
using Pedidos.Application.DTOs;
using Pedidos.Application.Features.Pedidos.Commands.CreatePedido;
using Pedidos.Application.Features.Pedidos.Commands.DeletePedido;
using Pedidos.Application.Features.Pedidos.Commands.UpdatePedido;
using Pedidos.Application.Features.Pedidos.Queries.GetAllPedidos;
using Pedidos.Application.Features.Pedidos.Queries.GetPedidoById;

namespace Pedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;

        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllPedidosQuery()));
        }

        
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetPedidoByIdQuery() { Id = id }));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Grabar(PedidoDto dto)
        {
            var command = new CreatePedidoCommand
            {
                NumeroPedido = dto.NumeroPedido,
                Cliente = dto.Cliente,
                Fecha = dto.Fecha,
                Total = dto.Total
            };

            var result = await _mediator.Send(command);

            if (!result.Success) { 
                return UnprocessableEntity(result);
            }
 
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(PedidoDto dto)
        {
            var command = new UpdatePedidoCommand
            {
                Id = dto.Id,
                NumeroPedido = dto.NumeroPedido,
                Cliente = dto.Cliente,
                Fecha = dto.Fecha,
                Total = dto.Total
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {

            var result = await _mediator.Send(new DeletePedidoCommand { Id = id });
            if (!result.Success)
            {
                return UnprocessableEntity(result);
            }

            return Ok(result);
        }
    }
}
