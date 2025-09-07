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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<(List<Usuario> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var query = _context.Usuario.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query.Skip((numeroPag - 1) * tamanhoPag).Take(tamanhoPag).ToListAsync();
            return (itens, total);
        }

        public async Task<Usuario> CriarAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> AtualizarAsync(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var usuario = await _context.Patio.FindAsync(id);
            if (usuario == null) return false;
            _context.Patio.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
