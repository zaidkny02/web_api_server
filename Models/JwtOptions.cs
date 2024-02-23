namespace webapi_test211223.Models
{
    public class JwtOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SigningKey { get; set; }
    }
}
