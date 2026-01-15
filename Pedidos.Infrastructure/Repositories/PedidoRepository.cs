using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;
using Pedidos.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        const string pedidoEliminado = "E";
        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Pedido pedido)
        {
            _context.Pedido.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Pedido pedido)
        {
            await _context.Pedido.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Pedido pedido)
        {
            pedido.Estado = pedidoEliminado;
            await _context.SaveChangesAsync();

        }

        public async Task<bool> NumeroPedidoExisteAsync(string numeroPedido, int idPedidoOmitir)
        {
            return await _context.Pedido.AnyAsync(s => s.NumeroPedido == numeroPedido && s.Id != idPedidoOmitir);
        }

        public async Task<List<Pedido>> obtenerListaPedidosAsync()
        {
            return await _context.Pedido.Where(a => !a.Estado.Equals(pedidoEliminado)).ToListAsync();
        }

        public async Task<Pedido?> obtenerPedidoPorIdAsync(int id)
        {
            return await _context.Pedido.FindAsync(id);
        }
    }
}
