using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;

namespace MottuFind_C_.Application.Services
{
    public class LeitorRfidService
    {
        private readonly ILeitorRfidRepository _repo;
        private readonly IMapper _mapper;

        public LeitorRfidService(ILeitorRfidRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<LeitorRfidResponse>> ObterTodos()
        {
            var leitors = await _repo.ObterTodosAsync();
            return _mapper.Map<List<LeitorRfidResponse>>(leitors);
        }

        public async Task<LeitorRfidResponse?> ObterPorId(int id)
        {
            var leitor = await _repo.ObterPorIdAsync(id);
            return leitor == null ? null : _mapper.Map<LeitorRfidResponse>(leitor);
        }

        public async Task<PagedResult<LeitorRfidResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

            return new PagedResult<LeitorRfidResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = _mapper.Map<List<LeitorRfidResponse>>(itens)
            };
        }

        public async Task<LeitorRfidResponse> Criar(LeitorRfidRequest request)
        {
            var novoLeitor = _mapper.Map<LeitorRfid>(request);
            await _repo.CriarAsync(novoLeitor);
            return _mapper.Map<LeitorRfidResponse>(novoLeitor);
        }

        public async Task<bool> Atualizar(int id, LeitorRfidRequest request)
        {
            var leitor = await _repo.ObterPorIdAsync(id);
            if (leitor == null) return false;

            _mapper.Map(request, leitor);
            return await _repo.AtualizarAsync(leitor);
        }

        public async Task<bool> Remover(int id)
        {
            return await _repo.RemoverAsync(id);
        }
    }
}
