namespace UBC.Core.Service.DTO.Identity
{
    public class UserTokenResponseDTO
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public IEnumerable<UserClaimResponseDTO> Claims { get; set; }
    }
}
