using Sprint1_C_.Domain.Entities;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeitorRfidRequest
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public string IpDispositivo { get; set; }

        public int PatioId { get; set; }
        
    }
}
