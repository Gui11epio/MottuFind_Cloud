using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
            var motos = _context.Moto
                .Include(m => m.TagRfid)
                .ToList();

            return _mapper.Map<List<MotoResponse>>(motos);
        }

        public MotoResponse? ObterPorPlaca(string placa)
        {
            var moto = _context.Moto
                .Include(m => m.TagRfid) 
                .FirstOrDefault(m => m.Placa == placa);

            return moto == null ? null : _mapper.Map<MotoResponse>(moto);
        }

        public async Task<PagedResult<MotoResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var query = _context.Moto
                .Include(m => m.TagRfid) // <- Include aqui
                .AsQueryable();


            var total = await query.CountAsync();
            var itens = await query
                .Skip((numeroPag - 1) * tamanhoPag)
                .Take(tamanhoPag)
                .ProjectTo<MotoResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<MotoResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = itens
            };
        }


        public MotoResponse Criar(MotoRequest request)
        {
            
            // Mapeia a moto (sem tag ainda)
            var novaMoto = _mapper.Map<Moto>(request);

            // Cria automaticamente a TagRfid
            var novaTag = new TagRfid
            {
                CodigoIdentificacao = "TAG-" + request.Placa,
                Moto = novaMoto // Associa a moto à tag
            };

            // Associa a tag à moto
            novaMoto.TagRfid = novaTag;

            // Adiciona e salva (em cascata)
            _context.Moto.Add(novaMoto); // A tag será salva junto, pois está referenciada
            _context.SaveChanges();

            return _mapper.Map<MotoResponse>(novaMoto);
        }

        public bool Atualizar(string placa, MotoRequest request)
        {
            var moto = _context.Moto.Find(placa);
            if (moto == null) return false;

            _mapper.Map(request, moto);

            _context.Moto.Update(moto);
            _context.SaveChanges();
            return true;
        }

        public bool Remover(string placa)
        {
            var moto = _context.Moto.Find(placa);
            if (moto == null) return false;

            _context.Moto.Remove(moto);
            _context.SaveChanges();
            return true;
        }
    }
}
