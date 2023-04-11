using LoginProject.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Domain.Interfaces
{
    public interface IAuthenticateRepository
    {
        Task<SignInResult> Authenticate(string email, string password);
        Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task SignInAsync(ApplicationUser applicationUser, bool isPersistent);
        Task Logout();
    }
}
