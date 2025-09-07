using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface IPatioRepository
    {
        Task<List<Patio>> ObterTodosAsync();
        Task<Patio?> ObterPorIdAsync(int id);
        Task<(List<Patio> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<Patio> CriarAsync(Patio patio);
        Task<bool> AtualizarAsync(Patio patio);
        Task<bool> RemoverAsync(int id);
    }
}
