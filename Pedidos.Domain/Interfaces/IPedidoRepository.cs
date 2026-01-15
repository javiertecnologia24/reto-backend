using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> obtenerListaPedidosAsync();
        Task<Pedido?> obtenerPedidoPorIdAsync(int id);
        Task AgregarAsync(Pedido pedido);
        Task ActualizarAsync(Pedido pedido);
        Task EliminarAsync(Pedido pedido);
        Task<bool> NumeroPedidoExisteAsync(string numeroPedido, int idPedidoOmitir);
    }
}
