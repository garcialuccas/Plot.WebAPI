using Plot.WebAPI.Models;

namespace Plot.WebAPI.Interfaces
{
    public interface IGeneroRepository
    {
        Task Cadastrar(Genero genero);

        Task Atualizar(Guid id, Genero genero);

        Task Excluir(Guid id);

        Task<List<Genero>> Listar();

        Task<Genero?> BuscarPorId(Guid id);

        Task<List<Genero>> BuscarPorIds(IReadOnlyCollection<Guid> ids);
    }
}
