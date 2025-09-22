using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface ILeitorRfidRepository
    {
        Task<List<LeitorRfid>> ObterTodosAsync();
        Task<LeitorRfid?> ObterPorIdAsync(int id);
        Task<(List<LeitorRfid> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<LeitorRfid> CriarAsync(LeitorRfid leitor);
        Task<bool> AtualizarAsync(LeitorRfid leitor);
        Task<bool> RemoverAsync(int id);
    }
}
