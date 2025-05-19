using Sprint1_C_.Domain.Enums;

namespace Sprint1_C_.Application.DTOs.Response
{
    public class MotoResponse
    {
        public string Placa { get; set; }
        public MotoModelo Modelo { get; set; }
        public string Marca { get; set; }
        public MotoStatus Status { get; set; }
        public int PatioId { get; set; }

        // Dados da TagRfid
        public int TagRfidId { get; set; }
        public string CodigoIdentificacao { get; set; }
    }
}
