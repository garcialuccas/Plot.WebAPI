using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Plot.WebAPI.DTO
{
    public class UsuarioAtualizacaoDTO : IValidatableObject
    {
        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MinLength(8)]
        public string Senha {  get; set; }

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
