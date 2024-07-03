namespace UBC.Core.Domain.Models.Identity
{
    public class UserTokenResponse
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        
        public IEnumerable<UserClaimResponse> Claims { get; set; }
    }
}
