using LoginProject.Domain.Entities;
using LoginProject.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoginProject.Infra.IoC
{
    public static class DependencyInjectionAuth
    {
        public static IServiceCollection AddAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.Cookie.IsEssential = true;
                })
            .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                var microsoftAuth = configuration.GetSection("Authentication:Microsoft");

                options.ClientId = microsoftAuth["ClientId"];
                options.ClientSecret = microsoftAuth["ClientSecret"];
                options.SignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                var googleAuth = configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuth["ClientId"];
                options.ClientSecret = googleAuth["ClientSecret"];
                options.SignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
            {
                var facebookAuth = configuration.GetSection("Authentication:Facebook");

                options.AppId = facebookAuth["AppId"];
                options.AppSecret = facebookAuth["AppSecret"];
                options.SignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddTwitter(TwitterDefaults.AuthenticationScheme, options =>
            {
                var twitterAuth = configuration.GetSection("Authentication:Twitter");

                options.ConsumerKey = twitterAuth["ConsumerAPIKey"];
                options.ConsumerSecret = twitterAuth["ConsumerSecret"];
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });

            services.ConfigureApplicationCookie(options =>
                 options.AccessDeniedPath = "/Account/Login");

            return services;
        }
    }
}
