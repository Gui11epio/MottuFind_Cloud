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
        public int TagId { get; set; }
        public TagRfidResponse Tag { get; set; } = new();
    }
}
