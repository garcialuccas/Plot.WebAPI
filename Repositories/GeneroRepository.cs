using Microsoft.EntityFrameworkCore;
using Plot.WebAPI.BdContextPlot;
using Plot.WebAPI.Interfaces;
using Plot.WebAPI.Models;

namespace Plot.WebAPI.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly PlotContext _ctx;

        public GeneroRepository(PlotContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Atualizar(Guid id, Genero genero)
        {
            var generoBuscado = await _ctx.Genero.FindAsync(id);

            if(generoBuscado != null)
            {
                generoBuscado.Genero1 = genero.Genero1;

                _ctx.Genero.Update(generoBuscado);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Genero?> BuscarPorId(Guid id)
        {
            return await _ctx.Genero.FirstOrDefaultAsync(g => g.IdGenero == id);
        }

        public Task<List<Genero>> BuscarPorIds(IReadOnlyCollection<Guid> ids)
        {
            var idsArray = ids.ToArray();
            return _ctx.Genero.Where(g => idsArray.Contains(g.IdGenero)).ToListAsync();
        }

        public async Task Cadastrar(Genero genero)
        {
            await _ctx.Genero.AddAsync(genero);
            await _ctx.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var generoBuscado = await _ctx.Genero.FindAsync(id);

            if (generoBuscado != null)
            {
                _ctx.Genero.Remove(generoBuscado);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Genero>> Listar()
        {
            return await _ctx.Genero.AsNoTracking().OrderBy(g => g.Genero1).ToListAsync();
        }
    }
}
