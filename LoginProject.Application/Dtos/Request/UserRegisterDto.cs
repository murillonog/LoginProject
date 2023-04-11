using LoginProject.Application.Enums;

namespace LoginProject.Application.Dtos.Request
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birth { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
    }
}
