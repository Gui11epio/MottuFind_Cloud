using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace MottuFind_C_.Infrastructure.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly AppDbContext _context;

        public PatioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patio>> ObterTodosAsync()
        {
            return await _context.Patio.ToListAsync();
        }

        public async Task<Patio?> ObterPorIdAsync(int id)
        {
            return await _context.Patio.FindAsync(id);
        }

        public async Task<(List<Patio> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.Patio.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query.Skip((numeroPag - 1) * tamanhoPag).Take(tamanhoPag).ToListAsync();
            return (itens, total);
        }

        public async Task<Patio> CriarAsync(Patio patio)
        {
            _context.Patio.Add(patio);
            await _context.SaveChangesAsync();
            return patio;
        }

        public async Task<bool> AtualizarAsync(Patio patio)
        {
            _context.Patio.Update(patio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var patio = await _context.Patio.FindAsync(id);
            if (patio == null) return false;
            _context.Patio.Remove(patio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
