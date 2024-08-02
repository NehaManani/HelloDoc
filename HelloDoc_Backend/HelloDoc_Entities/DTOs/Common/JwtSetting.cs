namespace HelloDoc_Entities.DTOs.Common
{
    public class JwtSetting
    {
        public string Issuer { get; set; } = null!;

        public string Audience { get; set; } = null!;

        public int ExpiryMinutes { get; set; }

        public string Key { get; set; } = null!;
    }
}