namespace LoginProject.Application.Extensions
{
    public static class MapExtensions
    {
        public static int GetAge(this DateTime birthDay)
            => DateTime.Now.Subtract(birthDay).Days % 365 == 0 ?
                DateTime.Now.Subtract(birthDay).Days / 365 : (DateTime.Now.Subtract(birthDay).Days / 365) + 1;
    }
}
