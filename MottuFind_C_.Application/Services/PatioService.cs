using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Application.Services
{
    public class PatioService
    {
        private readonly IPatioRepository _repo;
        private readonly IMapper _mapper;

        public PatioService(IPatioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PatioResponse>> ObterTodos()
        {
            var patios = await _repo.ObterTodosAsync();
            return _mapper.Map<List<PatioResponse>>(patios);
        }

        public async Task<PatioResponse?> ObterPorId(int id)
        {
            var patio = await _repo.ObterPorIdAsync(id);
            return patio == null ? null : _mapper.Map<PatioResponse>(patio);
        }

        public async Task<PagedResult<PatioResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

            return new PagedResult<PatioResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = _mapper.Map<List<PatioResponse>>(itens)
            };
        }

        public async Task<PatioResponse> Criar(PatioRequest request)
        {
            var novoPatio = _mapper.Map<Patio>(request);
            await _repo.CriarAsync(novoPatio);
            return _mapper.Map<PatioResponse>(novoPatio);
        }

        public async Task<bool> Atualizar(int id, PatioRequest request)
        {
            var patio = await _repo.ObterPorIdAsync(id);
            if (patio == null) return false;

            _mapper.Map(request, patio);
            return await _repo.AtualizarAsync(patio);
        }

        public async Task<bool> Remover(int id)
        {
            return await _repo.RemoverAsync(id);
        }
    }
}
