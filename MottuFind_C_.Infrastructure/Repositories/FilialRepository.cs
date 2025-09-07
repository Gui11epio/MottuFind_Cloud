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
    public class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _context;

        public FilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Filial>> ObterTodosAsync()
        {
            return await _context.Filial.ToListAsync();
        }

        public async Task<Filial?> ObterPorIdAsync(int id)
        {
            return await _context.Filial.FindAsync(id);
        }

        public async Task<(List<Filial> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.Filial.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query.Skip((numeroPag - 1) * tamanhoPag).Take(tamanhoPag).ToListAsync();
            return (itens, total);
        }

        public async Task<Filial> CriarAsync(Filial filial)
        {
            _context.Filial.Add(filial);
            await _context.SaveChangesAsync();
            return filial;
        }

        public async Task<bool> AtualizarAsync(Filial filial)
        {
            _context.Filial.Update(filial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            if (filial == null) return false;

            _context.Filial.Remove(filial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
