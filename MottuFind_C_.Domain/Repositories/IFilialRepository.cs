using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Domain.Repositories
{
    public interface IFilialRepository
    {
        Task<List<Filial>> ObterTodosAsync();
        Task<Filial?> ObterPorIdAsync(int id);
        Task<(List<Filial> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag);
        Task<Filial> CriarAsync(Filial filial);
        Task<bool> AtualizarAsync(Filial filial);
        Task<bool> RemoverAsync(int id);
    }
}
