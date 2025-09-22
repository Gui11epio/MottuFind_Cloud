using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface ILeituraRfidRepository
    {
        Task<List<LeituraRfid>> ObterTodosAsync();
        Task<LeituraRfid?> ObterPorIdAsync(int id);
        Task<(List<LeituraRfid> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<LeituraRfid> CriarAsync(LeituraRfid leitura);
        Task<bool> AtualizarAsync(LeituraRfid leitura);
        Task<bool> RemoverAsync(int id);
    }
}
