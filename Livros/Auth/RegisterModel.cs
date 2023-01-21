using System.ComponentModel.DataAnnotations;

namespace Livros.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nome de usuário é um campo obrigatório.")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Email é um campo obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        public string? Password { get; set; }   

    }
}
