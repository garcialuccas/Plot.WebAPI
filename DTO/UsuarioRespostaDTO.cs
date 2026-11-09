using Microsoft.Extensions.Primitives;
using Plot.WebAPI.Models;

namespace Plot.WebAPI.DTO
{
    public class UsuarioRespostaDTO(Guid idUsuario, string nome, string email, string tipoUsuario, bool ativo, string? fotoPerfil)
    {
        public static UsuarioRespostaDTO Converter(Usuario u) => new(u.IdUsuario, u.Nome, u.Email, u.TipoUsuario, u.Ativo, u.FotoPerfil);
    }
}
