using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;


namespace Sprint1_C_.Application.Services
{
    public class MotoService
    {
        private readonly IMotoRepository _repo;
        private readonly IMapper _mapper;

        public MotoService(IMotoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<MotoResponse>> ObterTodos()
        {
            var motos = await _repo.ObterTodosAsync();
            return _mapper.Map<List<MotoResponse>>(motos);
        }

        public async Task<MotoResponse?> ObterPorPlaca(string placa)
        {
            var moto = await _repo.ObterPorPlacaAsync(placa);
            return moto == null ? null : _mapper.Map<MotoResponse>(moto);
        }

        public async Task<PagedResult<MotoResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

            return new PagedResult<MotoResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = _mapper.Map<List<MotoResponse>>(itens)
            };
        }

        public async Task<MotoResponse> Criar(MotoRequest request)
        {
            var novaMoto = _mapper.Map<Moto>(request);

            var novaTag = new TagRfid
            {
                CodigoIdentificacao = "TAG-" + request.Placa,
                Moto = novaMoto
            };

            await _repo.CriarAsync(novaMoto, novaTag);

            return _mapper.Map<MotoResponse>(novaMoto);
        }

        public async Task<bool> Atualizar(string placa, MotoRequest request)
        {
            var moto = await _repo.ObterPorPlacaAsync(placa);
            if (moto == null) return false;

            _mapper.Map(request, moto);
            return await _repo.AtualizarAsync(moto);
        }

        public async Task<bool> Remover(string placa)
        {
            return await _repo.RemoverAsync(placa);
        }
    }
}
