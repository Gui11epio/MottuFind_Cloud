using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure;
using Sprint1_C_.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sprint1_C_.Application.Services
{
    public class FilialService
    {
        private readonly AppDbContext _context;

        public FilialService(AppDbContext context)
        {
            _context = context;
        }

        public List<FilialResponse> ObterTodos()
        {
            return _context.Filiais.Select(f => new FilialResponse
            {
                Id = f.Id,
                Nome = f.Nome,
                Cidade = f.Cidade,
                Pais = f.Pais
            }).ToList();
        }

        public FilialResponse? ObterPorId(int id)
        {
            var filial = _context.Filiais.Find(id);
            if (filial == null) return null;

            return new FilialResponse
            {
                Id = filial.Id,
                Nome = filial.Nome,
                Cidade = filial.Cidade,
                Pais = filial.Pais
            };
        }

        public FilialResponse Criar(FilialRequest request)
        {
            var nova = new Filial
            {
                Nome = request.Nome,
                Cidade = request.Cidade,
                Pais = request.Pais
            };

            _context.Filiais.Add(nova);
            _context.SaveChanges();

            return new FilialResponse
            {
                Id = nova.Id,
                Nome = nova.Nome,
                Cidade = nova.Cidade,
                Pais = nova.Pais
            };
        }

        public bool Atualizar(int id, FilialRequest request)
        {
            var filial = _context.Filiais.Find(id);
            if (filial == null) return false;

            filial.Nome = request.Nome;
            filial.Cidade = request.Cidade;
            filial.Pais = request.Pais;

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
