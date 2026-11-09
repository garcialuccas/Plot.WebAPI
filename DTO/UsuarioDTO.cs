using System.ComponentModel.DataAnnotations;

namespace Plot.WebAPI.DTO
{
    public class UsuarioDTO : IValidatableObject
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(8)]
        public string Senha { get; set; } = string.Empty;

        public string? FotoPerfil { get; set; }

        public IFormFile? ArquivoImagem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArquivoImagem is not null && !string.IsNullOrWhiteSpace(FotoPerfil))
            {
                yield return new ValidationResult("Envie ArquivoImagem ou ImagemCapa, não os dois", new[] { nameof(ArquivoImagem), nameof(FotoPerfil) });
            }
        }
    }
}
