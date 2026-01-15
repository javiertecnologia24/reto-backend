using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El numero de pedido es obligatorio")]
        public string NumeroPedido { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
