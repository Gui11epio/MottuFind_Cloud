using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Application.Services
{
    public class FilialService
    {
        private readonly IFilialRepository _repo;
        private readonly IMapper _mapper;

        public FilialService(IFilialRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<FilialResponse>> ObterTodos()
        {
            var filiais = await _repo.ObterTodosAsync();
            return _mapper.Map<List<FilialResponse>>(filiais);
        }

        public async Task<FilialResponse?> ObterPorId(int id)
        {
            var filial = await _repo.ObterPorIdAsync(id);
            return filial == null ? null : _mapper.Map<FilialResponse>(filial);
        }

        public async Task<PagedResult<FilialResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

            return new PagedResult<FilialResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = _mapper.Map<List<FilialResponse>>(itens)
            };
        }

        public async Task<FilialResponse> Criar(FilialRequest request)
        {
            var novaFilial = _mapper.Map<Filial>(request);
            await _repo.CriarAsync(novaFilial);
            return _mapper.Map<FilialResponse>(novaFilial);
        }

        public async Task<bool> Atualizar(int id, FilialRequest request)
        {
            var filial = await _repo.ObterPorIdAsync(id);
            if (filial == null) return false;

            _mapper.Map(request, filial);
            return await _repo.AtualizarAsync(filial);
        }

        public async Task<bool> Remover(int id)
        {
            return await _repo.RemoverAsync(id);
        }
    }
}
