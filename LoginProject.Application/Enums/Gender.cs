using System.ComponentModel;

namespace LoginProject.Application.Enums
{
    public enum Gender
    {
        [Description("Male")]
        Male = 0,
        [Description("Female")]
        Female = 1,
        [Description("Others")]
        Others = 2,
    }
}
