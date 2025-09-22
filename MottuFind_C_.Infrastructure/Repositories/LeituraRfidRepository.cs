using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace MottuFind_C_.Infrastructure.Repositories
{
    public class LeituraRfidRepository : ILeituraRfidRepository
    {

        private readonly AppDbContext _context;

        public LeituraRfidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeituraRfid>> ObterTodosAsync()
        {
            return await _context.LeituraRfid.ToListAsync();
        }

        public async Task<LeituraRfid?> ObterPorIdAsync(int id)
        {
            return await _context.LeituraRfid.FindAsync(id);
        }

        public async Task<(List<LeituraRfid> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.LeituraRfid.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query.Skip((numeroPag - 1) * tamanhoPag).Take(tamanhoPag).ToListAsync();
            return (itens, total);
        }

        public async Task<LeituraRfid> CriarAsync(LeituraRfid leitura)
        {
            _context.LeituraRfid.Add(leitura);
            await _context.SaveChangesAsync();
            return leitura;
        }


        public async Task<bool> AtualizarAsync(LeituraRfid leitura)
        {
            _context.LeituraRfid.Update(leitura);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var leitura = await _context.LeituraRfid.FindAsync(id);
            if (leitura == null) return false;

            _context.LeituraRfid.Remove(leitura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
