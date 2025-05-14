using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;



namespace Sprint1_C_.Application.Services
{
    public class PatioService
    {
        private readonly AppDbContext _context;

        public PatioService(AppDbContext context)
        {
            _context = context;
        }

        public List<PatioResponse> ObterTodos()
        {
            return _context.Patios.Select(p => new PatioResponse
            {
                Id = p.Id,
                Nome = p.Nome,
                Largura = p.Largura,
                Comprimento = p.Comprimento,
                FilialId = p.FilialId
            }).ToList();
        }

        public PatioResponse? ObterPorId(int id)
        {
            var patio = _context.Patios.Find(id);
            if (patio == null) return null;

            return new PatioResponse
            {
                Id = patio.Id,
                Nome = patio.Nome,
                Largura = patio.Largura,
                Comprimento = patio.Comprimento,
                FilialId = patio.FilialId
            };
        }

        public PatioResponse Criar(PatioRequest request)
        {
            var novo = new Patio
            {
                Nome = request.Nome,
                Largura = request.Largura,
                Comprimento = request.Comprimento,
                FilialId = request.FilialId
            };

            _context.Patios.Add(novo);
            _context.SaveChanges();

            return new PatioResponse
            {
                Id = novo.Id,
                Nome = novo.Nome,
                Largura = novo.Largura,
                Comprimento = novo.Comprimento,
                FilialId = novo.FilialId
            };
        }

        public bool Atualizar(int id, PatioRequest request)
        {
            var patio = _context.Patios.Find(id);
            if (patio == null) return false;

            patio.Nome = request.Nome;
            patio.Largura = request.Largura;
            patio.Comprimento = request.Comprimento;
            patio.FilialId = request.FilialId;

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
