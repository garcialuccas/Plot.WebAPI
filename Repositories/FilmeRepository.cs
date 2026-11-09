using Microsoft.EntityFrameworkCore;
using Plot.WebAPI.BdContextPlot;
using Plot.WebAPI.Interfaces;
using Plot.WebAPI.Models;

namespace Plot.WebAPI.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly PlotContext _ctx;

        public FilmeRepository(PlotContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Atualizar(Guid id, Filme filme)
        {
            var filmeBuscado = await _ctx.Filme.Include(f => f.IdGenero).FirstOrDefaultAsync(f => f.IdFilme == id);

            if (filmeBuscado != null)
            {
                filmeBuscado.Titulo = filme.Titulo;
                filmeBuscado.Sinopse = filme.Sinopse;
                filmeBuscado.Duracao = filme.Duracao;
                filmeBuscado.DataLancamento = filme.DataLancamento;
                filmeBuscado.ImagemCapa = filme.ImagemCapa;
                filmeBuscado.IdGenero = filme.IdGenero;

                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Filme?> BuscarPorId(Guid id)
        {
            return await _ctx.Filme.AsNoTracking().Include(f => f.IdGenero).FirstOrDefaultAsync(f => f.IdFilme == id);
        }

        public async Task Cadastrar(Filme filme)
        {
            await _ctx.Filme.AddAsync(filme);
            await _ctx.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var filmeBuscado = await _ctx.Filme.FirstOrDefaultAsync(f => f.IdFilme == id);

            if(filmeBuscado != null)
            {
                _ctx.Filme.Remove(filmeBuscado);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<(List<Filme> filmes, int total)> ListarPaginado(int pagina = 1, int tamanhoPagina = 10, string? busca = null)
        {
            var query = _ctx.Filme.AsNoTracking().Include(f => f.IdGenero).AsQueryable();

            if(!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(f => f.Titulo.Contains(busca));
            }

            int total = await query.CountAsync();

            var items = await query
                .OrderBy(f => f.Titulo).ThenBy(f => f.IdFilme)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (items, total);
        }
    }
}
