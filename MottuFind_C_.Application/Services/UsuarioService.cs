namespace Sprint1_C_.Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

public class UsuarioService
{
    private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UsuarioResponse> ObterTodos()
        {
            var usuarios = _context.Usuario.ToList();
            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }

        public UsuarioResponse? ObterPorId(int id)
        {
            var usuario = _context.Usuario.Find(id);
            return usuario == null ? null : _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<PagedResult<UsuarioResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var query = _context.Usuario.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query
                .Skip((numeroPag - 1) * tamanhoPag)
                .Take(tamanhoPag)
                .ProjectTo<UsuarioResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<UsuarioResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = itens
            };
        }

        public UsuarioResponse Criar(UsuarioRequest request)
        {
            var novoUsuario = _mapper.Map<Usuario>(request);

            _context.Usuario.Add(novoUsuario);
            _context.SaveChanges();

            return _mapper.Map<UsuarioResponse>(novoUsuario);
        }

        public bool Atualizar(int id, UsuarioRequest request)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null) return false;

            _mapper.Map(request, usuario);

            _context.Usuario.Update(usuario);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null) return false;

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
}