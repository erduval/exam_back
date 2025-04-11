using Examen.DTO.Auth;
using Microsoft.AspNetCore.Identity;

namespace Examen.Service.Auth
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterTechAsync (RegisterDTO dto);
        Task<IdentityResult> RegisterClientAsync (RegisterDTO dto);
        Task<string?> LoginAsync (LoginDTO dto);
    }
}
