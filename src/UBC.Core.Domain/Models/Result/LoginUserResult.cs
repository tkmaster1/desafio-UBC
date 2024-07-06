namespace UBC.Core.Domain.Models.Result
{
    public class LoginUserResult
    {
        public bool Succeeded { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsNotAllowed { get; set; }

        public bool RequiresTwoFactor { get; set; }

        public string Message { get; set; }
    }
}