namespace kms.Models
{
    public class Jwt
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
    }
}
