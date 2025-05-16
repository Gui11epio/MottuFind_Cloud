using AutoMapper;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace Sprint1_C_.Application.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MotoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MotoResponse> ObterTodos()
        {
            var motos = _context.Motos.ToList();
            return _mapper.Map<List<MotoResponse>>(motos);
        }

        public MotoResponse? ObterPorPlaca(string placa)
        {
            var moto = _context.Motos.FirstOrDefault(m => m.Placa == placa);
            return moto == null ? null : _mapper.Map<MotoResponse>(moto);
        }

        public MotoResponse Criar(MotoRequest request)
        {
            var novaTag = new TagRfid
            {
                CodigoIdentificacao = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()
            };
            _context.TagsRfid.Add(novaTag);
            _context.SaveChanges();

            var novaMoto = _mapper.Map<Moto>(request);
            novaMoto.TagId = novaTag.Id;

            _context.Motos.Add(novaMoto);
            _context.SaveChanges();

            var response = _mapper.Map<MotoResponse>(novaMoto);
            response.Tag = _mapper.Map<TagRfidResponse>(novaTag);
            return response;
        }

        public bool Atualizar(string placa, MotoRequest request)
        {
            var moto = _context.Motos.Find(placa);
            if (moto == null) return false;

            _mapper.Map(request, moto); // Atualiza a instância existente

            _context.Motos.Update(moto);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(string placa)
        {
            var moto = _context.Motos.Find(placa);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            _context.SaveChanges();
            return true;
        }
    }
}
