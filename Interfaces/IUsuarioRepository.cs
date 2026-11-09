using Plot.WebAPI.Models;

namespace Plot.WebAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Cadastrar(Usuario usuario);

        Task Atualizar(Guid id, Usuario usuario);

        Task Excluir(Guid id);

        Task<List<Usuario>> Listar();

        Task<Usuario?> BuscarPorId(Guid id);

        Task<Usuario?> BuscarPorEmailESenha(string email, string senha);
    }
}
