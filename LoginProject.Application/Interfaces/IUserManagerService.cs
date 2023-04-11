using LoginProject.Application.Dtos.Request;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Application.Interfaces
{
    public interface IUserManagerService
    {
        Task<IdentityResult> CreateUserAsync(string name, string email, string provider, string providerKey);
        Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto, string password);
        Task<UserRegisterDto?> FindByLoginAsync(string provider, string providerKey);
    }
}
