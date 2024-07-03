namespace UBC.Core.Domain.Models.Identity
{
    public class LoginUserResponse
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public string Message { get; set; }

        public UserTokenResponse UserToken { get; set; }
    }
}
