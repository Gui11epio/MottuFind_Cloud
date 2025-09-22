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
    public class LeituraRfidService
    {
        private readonly ILeituraRfidRepository _repo;
        private readonly IMapper _mapper;

        public LeituraRfidService(ILeituraRfidRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<LeituraRfidResponse>> ObterTodos()
        {
            var leituras = await _repo.ObterTodosAsync();
            return _mapper.Map<List<LeituraRfidResponse>>(leituras);
        }

        public async Task<LeituraRfidResponse?> ObterPorId(int id)
        {
            var leitura = await _repo.ObterPorIdAsync(id);
            return leitura == null ? null : _mapper.Map<LeituraRfidResponse>(leitura);
        }

        public async Task<PagedResult<LeituraRfidResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
        {
            var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

            return new PagedResult<LeituraRfidResponse>
            {
                Numeropag = numeroPag,
                Tamnhopag = tamanhoPag,
                Total = total,
                Itens = _mapper.Map<List<LeituraRfidResponse>>(itens)
            };
        }

        public async Task<LeituraRfidResponse> Criar(LeituraRfidRequest request)
        {
            var novaLeitura = _mapper.Map<LeituraRfid>(request);
            await _repo.CriarAsync(novaLeitura);
            return _mapper.Map<LeituraRfidResponse>(novaLeitura);
        }

        public async Task<bool> Atualizar(int id, LeituraRfidRequest request)
        {
            var leitura = await _repo.ObterPorIdAsync(id);
            if (leitura == null) return false;

            _mapper.Map(request, leitura);
            return await _repo.AtualizarAsync(leitura);
        }

        public async Task<bool> Remover(int id)
        {
            return await _repo.RemoverAsync(id);
        }
    }
}
