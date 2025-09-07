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
    public class MotoRepository : IMotoRepository
    {
        private readonly AppDbContext _context;

        public MotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moto>> ObterTodosAsync()
        {
            return await _context.Moto
                .Include(m => m.TagRfid)
                .ToListAsync();
        }

        public async Task<Moto?> ObterPorPlacaAsync(string placa)
        {
            return await _context.Moto
                .Include(m => m.TagRfid)
                .FirstOrDefaultAsync(m => m.Placa.Numero == placa);
        }

        public async Task<(List<Moto> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.Moto.Include(m => m.TagRfid).AsQueryable();

            var total = await query.CountAsync();
            var itens = await query
                .Skip((numeroPag - 1) * tamanhoPag)
                .Take(tamanhoPag)
                .ToListAsync();

            return (itens, total);
        }

        public async Task<Moto> CriarAsync(Moto moto, TagRfid tag)
        {
            moto.TagRfid = tag;
            _context.Moto.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<bool> AtualizarAsync(Moto moto)
        {
            _context.Moto.Update(moto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(string placa)
        {
            var moto = await _context.Moto.FindAsync(placa);
            if (moto == null) return false;

            _context.Moto.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
