using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UBC.Core.Service.DTO.Identity
{
    /// <summary>
    /// Login
    /// </summary>
    public class LoginUserRequestDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "Nome de usuário tem que ser em formato de E-mail.")]
        [Display(Name = "Nome de usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}