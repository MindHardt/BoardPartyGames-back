namespace API
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public TimeSpan ClockSkew { get; set; }
    }
}
