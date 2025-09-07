namespace Sprint1_C_.Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Domain.Repositories;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;


public class UsuarioService
{
    private readonly IUsuarioRepository _repo;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<UsuarioResponse>> ObterTodos()
    {
        var usuarios = await _repo.ObterTodosAsync();
        return _mapper.Map<List<UsuarioResponse>>(usuarios);
    }

    public async Task<UsuarioResponse?> ObterPorId(int id)
    {
        var usuario = await _repo.ObterPorIdAsync(id);
        return usuario == null ? null : _mapper.Map<UsuarioResponse>(usuario);
    }

    public async Task<PagedResult<UsuarioResponse>> ObterPorPagina(int numeroPag, int tamanhoPag)
    {
        var (itens, total) = await _repo.ObterPorPaginaAsync(numeroPag, tamanhoPag);

        return new PagedResult<UsuarioResponse>
        {
            Numeropag = numeroPag,
            Tamnhopag = tamanhoPag,
            Total = total,
            Itens = _mapper.Map<List<UsuarioResponse>>(itens)
        };
    }

    public async Task<UsuarioResponse> Criar(UsuarioRequest request)
    {
        var novoUsuario = _mapper.Map<Usuario>(request);
        await _repo.CriarAsync(novoUsuario);
        return _mapper.Map<UsuarioResponse>(novoUsuario);
    }

    public async Task<bool> Atualizar(int id, UsuarioRequest request)
    {
        var usuario = await _repo.ObterPorIdAsync(id);
        if (usuario == null) return false;

        _mapper.Map(request, usuario);
        return await _repo.AtualizarAsync(usuario);
    }

    public async Task<bool> Remover(int id)
    {
        return await _repo.RemoverAsync(id);
    }
}