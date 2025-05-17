using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace Sprint1_C_.Application.Services
{
    public class PatioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PatioResponse> ObterTodos()
        {
            var patios = _context.Patio.ToList();
            return _mapper.Map<List<PatioResponse>>(patios);
        }

        public PatioResponse? ObterPorId(int id)
        {
            var patio = _context.Patio.Find(id);
            return patio == null ? null : _mapper.Map<PatioResponse>(patio);
        }

        public async Task<PagedResult<PatioResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var query = _context.Patio.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query
                .Skip((numeroPag - 1) * tamanhoPag)
                .Take(tamanhoPag)
                .ProjectTo<PatioResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<PatioResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = itens
            };
        }

        public PatioResponse Criar(PatioRequest request)
        {
            var novoPatio = _mapper.Map<Patio>(request);

            _context.Patio.Add(novoPatio);
            _context.SaveChanges();

            return _mapper.Map<PatioResponse>(novoPatio);
        }

        public bool Atualizar(int id, PatioRequest request)
        {
            var patio = _context.Patio.Find(id);
            if (patio == null) return false;

            _mapper.Map(request, patio); // Atualiza propriedades

            _context.Patio.Update(patio);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var patio = _context.Patio.Find(id);
            if (patio == null) return false;

            _context.Patio.Remove(patio);
            _context.SaveChanges();
            return true;
        }
    }
}
