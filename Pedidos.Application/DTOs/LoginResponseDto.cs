namespace Pedidos.Application.DTOs
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
