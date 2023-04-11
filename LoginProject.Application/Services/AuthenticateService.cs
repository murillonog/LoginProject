﻿using AutoMapper;
using LoginProject.Application.Dtos.Request;
using LoginProject.Application.Interfaces;
using LoginProject.Domain.Entities;
using LoginProject.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateRepository _authenticateRepository;

        public AuthenticateService(IMapper mapper, IAuthenticateRepository authenticateRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authenticateRepository = authenticateRepository ?? throw new ArgumentNullException(nameof(authenticateRepository));
        }

        public async Task<SignInResult> Authenticate(string email, string password)
            => await _authenticateRepository.Authenticate(email, password);

        public async Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
            => await _authenticateRepository.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        public async Task SignInAsync(UserRegisterDto userRegisterDto, bool isPersistent)
            => await _authenticateRepository.SignInAsync(_mapper.Map<ApplicationUser>(userRegisterDto), isPersistent);

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
            => await _authenticateRepository.GetExternalAuthenticationSchemesAsync();

        public async Task Logout()
            => await _authenticateRepository.Logout();
    }
}
