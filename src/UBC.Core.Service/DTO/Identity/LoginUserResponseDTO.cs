namespace UBC.Core.Service.DTO.Identity
{
    public class LoginUserResponseDTO
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public string Message { get; set; }

        public UserTokenResponseDTO UserToken { get; set; }
    }
}
