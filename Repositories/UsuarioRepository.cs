using Microsoft.EntityFrameworkCore;
using Plot.WebAPI.BdContextPlot;
using Plot.WebAPI.Interfaces;
using Plot.WebAPI.Models;
using Plot.WebAPI.Utils;

namespace Plot.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PlotContext _ctx;

        public UsuarioRepository(PlotContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Atualizar(Guid id, Usuario usuario)
        {
            var usuarioBuscado = await _ctx.Usuario.FindAsync(id);

            if(usuarioBuscado != null)
            {
                usuarioBuscado.Nome = usuario.Nome;
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.TipoUsuario = usuario.TipoUsuario;
                usuarioBuscado.Ativo = usuario.Ativo;
                usuarioBuscado.FotoPerfil = usuario.FotoPerfil;
                if(!string.IsNullOrEmpty(usuario.Senha))
                {
                    usuarioBuscado.Senha = Criptografia.GerarHash(usuario.Senha);
                }

                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> BuscarPorEmailESenha(string email, string senha)
        {
            var usuarioBuscado = await _ctx.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioBuscado == null) return null;

            bool senhaValida = Criptografia.CompararHash(senha, usuarioBuscado.Senha);

            if (!senhaValida) return null;

            return usuarioBuscado;
        }

        public async Task<Usuario?> BuscarPorId(Guid id)
        {
            return await _ctx.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            usuario.Senha = Criptografia.GerarHash(usuario.Senha);

            await _ctx.Usuario.AddAsync(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var u = await _ctx.Usuario.FindAsync(id);

            if (u != null)
            {
                _ctx.Usuario.Remove(u);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _ctx.Usuario.AsNoTracking().ToListAsync();
        }
    }
}
