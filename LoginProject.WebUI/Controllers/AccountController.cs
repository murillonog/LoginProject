using AutoMapper;
using LoginProject.Application.Dtos.Request;
using LoginProject.Application.Interfaces;
using LoginProject.WebUI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginProject.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticationService;
        private readonly IUserManagerService _userManagerService;
        public AccountController(IMapper mapper, IAuthenticateService authenticationService, IUserManagerService userManagerService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _userManagerService = userManagerService ?? throw new ArgumentNullException(nameof(userManagerService));
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var providers = await _authenticationService.GetExternalAuthenticationSchemesAsync();
            var login = new LoginViewModel() { ReturnUrl = returnUrl, Providers = providers };
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _authenticationService.Authenticate(loginViewModel.Email, loginViewModel.Password);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginViewModel.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var userRegister = _mapper.Map<UserRegisterDto>(registerViewModel);
            var result = await _userManagerService.RegisterUserAsync(userRegister, registerViewModel.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong.");
                return View(registerViewModel);
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return Redirect("/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl, provider });
            var properties = await _authenticationService.ConfigureExternalAuthenticationProperties(provider, redirectUrl!);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string provider, string returnUrl = null)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            if (result.Succeeded)
            {
                var userIdClaim = result.Principal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var user = await _userManagerService.FindByLoginAsync(provider, userIdClaim.Value);
                    if (user is null)
                    {
                        var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
                        var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
                        await _userManagerService.CreateUserAsync(name, email, provider, userIdClaim.Value);
                    }
                    else
                    {
                        await _authenticationService.SignInAsync(user, false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
