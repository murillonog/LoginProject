using Microsoft.AspNetCore.Identity;

namespace LoginProject.Infra.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }

        public ApplicationUser GetUserDefault(string name, string email)
          => new()
          {
              UserName = email,
              Name = name,
              Email = email,
              NormalizedUserName = email.ToUpper(),
              NormalizedEmail = email.ToUpper(),
              Address = "Address Default",
              Birth = new DateTime(2000, 1, 1),
              Gender = 3,
              PhoneNumber = "11111111111",
              Age = DateTime.Now.Subtract(new DateTime(2000, 1, 1)).Days / 365,
              EmailConfirmed = true,
              LockoutEnabled = false,
              SecurityStamp = Guid.NewGuid().ToString()
          };
    }
}
