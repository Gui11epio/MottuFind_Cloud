using AutoMapper;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace Sprint1_C_.Application.Services
{
    public class FilialService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilialService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FilialResponse> ObterTodos()
        {
            var filiais = _context.Filiais.ToList();
            return _mapper.Map<List<FilialResponse>>(filiais);
        }

        public FilialResponse? ObterPorId(int id)
        {
            var filial = _context.Filiais.Find(id);
            return filial == null ? null : _mapper.Map<FilialResponse>(filial);
        }

        public FilialResponse Criar(FilialRequest request)
        {
            var novaFilial = _mapper.Map<Filial>(request);

            _context.Filiais.Add(novaFilial);
            _context.SaveChanges();

            return _mapper.Map<FilialResponse>(novaFilial);
        }

        public bool Atualizar(int id, FilialRequest request)
        {
            var filial = _context.Filiais.Find(id);
            if (filial == null) return false;

            _mapper.Map(request, filial);

            _context.Filiais.Update(filial);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var filial = _context.Filiais.Find(id);
            if (filial == null) return false;

            _context.Filiais.Remove(filial);
            _context.SaveChanges();
            return true;
        }
    }
}
