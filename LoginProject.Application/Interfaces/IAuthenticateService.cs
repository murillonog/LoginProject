using LoginProject.Application.Dtos.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<SignInResult> Authenticate(string email, string password);
        Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task SignInAsync(UserRegisterDto userRegisterDto, bool isPersistent);
        Task Logout();
    }
}
