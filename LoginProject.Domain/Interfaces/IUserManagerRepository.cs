using LoginProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Domain.Interfaces
{
    public interface IUserManagerRepository
    {
        Task<bool> RegisterUser(ApplicationUser applicationUser, string password);
        Task<ApplicationUser?> FindByLoginAsync(string provider, string providerKey);
        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password, UserLoginInfo userLoginInfo);
    }
}
