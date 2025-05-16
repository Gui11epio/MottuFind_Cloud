using AutoMapper;
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
            var patios = _context.Patios.ToList();
            return _mapper.Map<List<PatioResponse>>(patios);
        }

        public PatioResponse? ObterPorId(int id)
        {
            var patio = _context.Patios.Find(id);
            return patio == null ? null : _mapper.Map<PatioResponse>(patio);
        }

        public PatioResponse Criar(PatioRequest request)
        {
            var novoPatio = _mapper.Map<Patio>(request);

            _context.Patios.Add(novoPatio);
            _context.SaveChanges();

            return _mapper.Map<PatioResponse>(novoPatio);
        }

        public bool Atualizar(int id, PatioRequest request)
        {
            var patio = _context.Patios.Find(id);
            if (patio == null) return false;

            _mapper.Map(request, patio); // Atualiza propriedades

            _context.Patios.Update(patio);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var patio = _context.Patios.Find(id);
            if (patio == null) return false;

            _context.Patios.Remove(patio);
            _context.SaveChanges();
            return true;
        }
    }
}
