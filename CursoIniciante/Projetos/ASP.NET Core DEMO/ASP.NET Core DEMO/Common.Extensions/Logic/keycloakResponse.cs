namespace Common.Extensions.Logic
{
    public class keycloakResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string session_state { get; set; }
    }
}
