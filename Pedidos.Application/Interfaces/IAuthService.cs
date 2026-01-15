using Pedidos.Application.DTOs;

namespace Pedidos.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> AuthenticateAsync(string email, string password);
    }
}
