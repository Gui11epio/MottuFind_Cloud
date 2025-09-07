using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObterTodosAsync();
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<(List<Usuario> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<Usuario> CriarAsync(Usuario usuario);
        Task<bool> AtualizarAsync(Usuario usuario);
        Task<bool> RemoverAsync(int id);
    }
}
