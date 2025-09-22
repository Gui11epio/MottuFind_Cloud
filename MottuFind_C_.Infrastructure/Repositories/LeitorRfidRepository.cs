using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace MottuFind_C_.Infrastructure.Repositories
{
    public class LeitorRfidRepository : ILeitorRfidRepository
    {

        private readonly AppDbContext _context;

        public LeitorRfidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeitorRfid>> ObterTodosAsync()
        {
            return await _context.LeitorRfid.ToListAsync();
        }

        public async Task<LeitorRfid?> ObterPorIdAsync(int id)
        {
            return await _context.LeitorRfid.FindAsync(id);
        }

        public async Task<(List<LeitorRfid> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.LeitorRfid.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query.Skip((numeroPag - 1) * tamanhoPag).Take(tamanhoPag).ToListAsync();
            return (itens, total);
        }

        public async Task<LeitorRfid> CriarAsync(LeitorRfid leitor)
        {
            _context.LeitorRfid.Add(leitor);
            await _context.SaveChangesAsync();
            return leitor;
        }


        public async Task<bool> AtualizarAsync(LeitorRfid leitor)
        {
            _context.LeitorRfid.Update(leitor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var leitor = await _context.LeitorRfid.FindAsync(id);
            if (leitor == null) return false;

            _context.LeitorRfid.Remove(leitor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
