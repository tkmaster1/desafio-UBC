namespace UBC.Core.Service.DTO.Identity
{
    public class UserDTO
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string CodeUser { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}