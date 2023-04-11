using LoginProject.Domain.Entities;
using LoginProject.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LoginProject.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager,
              UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public void SeedUsers()
        {
            string password = "Numsey#2023";
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "usuario@localhost",
                    Email = "usuario@localhost",
                    NormalizedUserName = "USUARIO@LOCALHOST",
                    NormalizedEmail = "USUARIO@LOCALHOST",
                    Name = "Usuario",
                    Birth = DateTime.Now.AddYears(-20),
                    Age = 20,
                    Address = "street A",
                    Gender = 0,
                    PhoneNumber = "11111111111",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "User").Wait();
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "admin@localhost",
                    Email = "admin@localhost",
                    NormalizedUserName = "ADMIN@LOCALHOST",
                    NormalizedEmail = "ADMIN@LOCALHOST",
                    Name = "Admin",
                    Birth = DateTime.Now.AddYears(-20),
                    Age = 20,
                    Address = "street A",
                    Gender = 0,
                    PhoneNumber = "11111111111",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
                _roleManager.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "USER" });

            if (!_roleManager.RoleExistsAsync("Admin").Result)
                _roleManager.CreateAsync(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
        }
    }
}
