namespace DevFreela.API.Model
{
    public class JwtOption
    {
        public const string JWT = "Jwt";
        public string  Issuer { get; set; }
        public string  Audience { get; set; }
        public string   Key { get; set; }
    }
}
