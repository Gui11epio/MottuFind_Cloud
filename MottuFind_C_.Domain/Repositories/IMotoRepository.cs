using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface IMotoRepository
    {
        Task<List<Moto>> ObterTodosAsync();
        Task<Moto?> ObterPorPlacaAsync(string placa);
        Task<(List<Moto> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<Moto> CriarAsync(Moto moto, TagRfid tag);
        Task<bool> AtualizarAsync(Moto moto);
        Task<bool> RemoverAsync(string placa);
    }
}
