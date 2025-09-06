using Sprint1_C_.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sprint1_C_.Application.DTOs.Requests
{
    public class LeitorRfidRequest
    {
        [Required(ErrorMessage = "A localização é obrigatória")]
        public string Localizacao { get; set; }

        [Required(ErrorMessage = "O IP do dispositivo é obrigatório")]
        public string IpDispositivo { get; set; }

        [Required(ErrorMessage = "O ID do pátio é obrigatório")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do pátio deve ser positivo")]
        public int PatioId { get; set; }
        
    }
}
