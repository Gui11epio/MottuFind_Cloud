using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;



namespace Sprint1_C_.Application.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public List<MotoResponse> ObterTodos()
        {
            return _context.Motos.Select(m => new MotoResponse
            {
                Placa = m.Placa,
                Modelo = m.Modelo,
                Marca = m.Marca,
                Status = m.Status,
                PatioId = m.PatioId,
                TagId = m.TagId
            }).ToList();
        }

       

        public MotoResponse? ObterPorPlaca(string placa)
        {
            var moto = _context.Motos.FirstOrDefault(m => m.Placa == placa);
            if (moto == null) return null;

            return new MotoResponse
            {
                
                Placa = moto.Placa,
                Modelo = moto.Modelo,
                Marca = moto.Marca,
                Status = moto.Status,
                PatioId = moto.PatioId,
                TagId = moto.TagId
            };
        }

        public MotoResponse Criar(MotoRequest request)
        {

            var novaTag = new TagRfid
            {
                CodigoIdentificacao = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()
            };
            _context.TagsRfid.Add(novaTag);
            _context.SaveChanges();
            var novaMoto = new Moto

            {
                Placa = request.Placa,
                Modelo = request.Modelo,
                Marca = request.Marca,
                Status = request.Status,
                PatioId = request.PatioId,
                TagId = request.TagId
            };

            _context.Motos.Add(novaMoto);
            _context.SaveChanges();

            return new MotoResponse
            {

                Placa = novaMoto.Placa,
                Modelo = novaMoto.Modelo,
                Marca = novaMoto.Marca,
                Status = novaMoto.Status,
                PatioId = novaMoto.PatioId,
                TagId = novaTag.Id,
                Tag = new TagRfidResponse
                {
                    Id = novaTag.Id,
                    CodigoIdentificacao = novaTag.CodigoIdentificacao
                }
            };
        }

        public bool Atualizar(string placa, MotoRequest request)
        {
            var moto = _context.Motos.Find(placa);
            if (moto == null) return false;

            moto.Placa = request.Placa;
            moto.Modelo = request.Modelo;
            moto.Marca = request.Marca;
            moto.Status = request.Status;
            moto.PatioId = request.PatioId;
            moto.TagId = request.TagId;

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
